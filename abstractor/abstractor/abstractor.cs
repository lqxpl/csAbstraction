using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.ModularInstruments.Interop;
using NationalInstruments.RFToolkits.Interop;

/*
 * A previous iteration of this code contained a base framework for MIMO measurements (future-proofing)
 * To speed up delivery, those have been stripped.
 */

/*
 * Settings received from ini file:
 * number of receive channels: int32 rxChan
 * acquisition length: double acqLength
 * channel bandwidth: double chanBW
 * amplitude tracking enabled/disabled: bool ampTrack
 * phase tracking enabled/disabled:  bool  phaseTrack
 * time tracking enabled/disabled:  boot timeTrack
 * channel tracking enabled/disabled:  bool channelTrack
 * low pass filter enabled/disabled: bool lowpass
 * carrier frequency:  double carrierFreq
 * IQ Power Edge Trigger level:  double IQTrigLevel
 */

namespace abstractor
{
    public class Wlan_Abstractor
    {
        niRFSA rfsaSession;
        niWLANA wlanaSession;
        public string hwResourceName;
        public Int32 rxChan;
        public double acqLength;
        public double chanBW;
        int AmplitudeTrackingState;
        int PhaseTrackingState;
        int TimeTrackingState;
        int ChannelTrackingState;
        int LowpassFilterState;
        double carrierFreq;
        double IQTrigLevel;
        
        HardwareSettings hardware_settings;
        Hashtable msmtResults;

        /*
         * Instead of having expecting the user to remember to pass in 1901 / 1900 for enabled/disabled:
         * give them the ability to pass in true/false and have the code map that to the weird values used
         * by our drivers to enable or disable a feature.
         */
        private void populateArgs(bool ampTrack, bool phaseTrack, bool timeTrack, bool channelTrack, bool lowpass)
        {
            try
            {
                AmplitudeTrackingState = (ampTrack) ? niWLANAConstants.True : niWLANAConstants.False;
                PhaseTrackingState = (phaseTrack) ? niWLANAConstants.True : niWLANAConstants.False;
                TimeTrackingState = (timeTrack) ? niWLANAConstants.True : niWLANAConstants.False;
                ChannelTrackingState = (channelTrack) ? niWLANAConstants.True : niWLANAConstants.False;
                LowpassFilterState = (lowpass) ? niWLANAConstants.True : niWLANAConstants.False;
            }
            catch (Exception exception)
            {
                if (!string.IsNullOrEmpty(exception.Message))
                    Console.WriteLine(exception.Message);
                else
                    Console.WriteLine("\nAn unidentified error occured in populateArgs\n");
            }
        }

        /*
         * Caution:  the code doesn't check for a valid/invalid session reference for RFSA/WLANA, it just assigns new space
         * for a new reference.  Calling configureHardware repeatedly could potentially create a reference/memory leak.
         */
        public void configureHardware(string hwRsrc, Int32 rxChan, double acqLength, double chanBW,
            bool ampTrack, bool phaseTrack, bool timeTrack, bool channelTrack, bool lowpass,
            double carrierFrequency, double IQTrig)
        {
            try
            {
                msmtResults = new Hashtable();
                carrierFreq = carrierFrequency;
                IQTrigLevel = IQTrig;
                populateArgs(ampTrack, phaseTrack, timeTrack, channelTrack, lowpass);
                //A little wordy and ham-handed, but keeping the following settings in a single struct is useful later on.
                hwResourceName = hwRsrc;
                hardware_settings.resourceName = hwResourceName;
                hardware_settings.autoRefLevel = 0;
                hardware_settings.externalAttenuation = 0;
                hardware_settings.refLevel = 3;

                #region wlana settings
                //most of these settings are hardcoded.  If greater flexibility is required, this can be altered.
                wlanaSession = new niWLANA(niWLANAConstants.CompatibilityVersion030000);
                wlanaSession.SetStandard(null, (int)niWLANAConstants.Standard80211agjpOfdm);
                wlanaSession.SetNumberOfReceiveChannels(null, rxChan);
                wlanaSession.SetAcquisitionLength(null, acqLength);
                wlanaSession.SetChannelBandwidth(null, chanBW);

                wlanaSession.SetOfdmDemodEnabled(null, niWLANAConstants.True);
                wlanaSession.SetOfdmDemodAllTracesEnabled(null, niWLANAConstants.True);
                wlanaSession.SetOfdmDemodGatedPowerEnabled(null, niWLANAConstants.True);

                wlanaSession.SetOfdmDemodGatedPowerStartTime(null, 0.0);
                wlanaSession.SetOfdmDemodGatedPowerStopTime(null, 0.000064);

                wlanaSession.SetOfdmDemodNumberOfAverages(null, 1);

                wlanaSession.SetOfdmDemodHeaderDetectionEnabled(null, niWLANAConstants.True);
                wlanaSession.SetOfdmDemodMacFrameCheckSequenceCheckEnabled(null, niWLANAConstants.True);

                wlanaSession.SetOfdmDemodMeasurementStartLocation(null, 0);
                wlanaSession.SetOfdmDemodMaximumSymbolsUsed(null, 16);

                wlanaSession.SetOfdmDemodCfoEstimationMethod(null, niWLANAConstants.CfoEstimationMethodPreambleAndData);

                wlanaSession.SetOfdmDemod80211nPlcpFrameDetectionEnabled(null, niWLANAConstants.True);

                wlanaSession.SetOfdmDemodAmplitudeTrackingEnabled(null, AmplitudeTrackingState);
                wlanaSession.SetOfdmDemodPhaseTracking(null, PhaseTrackingState);
                wlanaSession.SetOfdmDemodTimeTrackingEnabled(null, TimeTrackingState);
                wlanaSession.SetOfdmDemodChannelTrackingEnabled(null, ChannelTrackingState);
                wlanaSession.SetOfdmDemodLowpassFilteringEnabled(null, LowpassFilterState);
                wlanaSession.SetOfdmDemodSymbolTimingAdjustment(null, -0.000000200);

                wlanaSession.SetTriggerDelay(null, 0.0);
                #endregion

                #region rfsa settings
                int numchannels = (int)rxChan;
                rfsaSession = new niRFSA(hardware_settings.resourceName, true, true);
                rfsaSession.ConfigureIQCarrierFrequency(null, carrierFrequency);
                rfsaSession.SetDouble(niRFSAProperties.ExternalGain, 0.0);
                rfsaSession.SetInt32((niRFSAProperties)1150187, 1901);

                rfsaSession.ConfigureRefClock(niRFSAConstants.PxiClkStr, 10000000.0);

                double autorefLevel;
                //autorefLevel = (double)hardware_settings.refLevel;
                //hardware_settings.autoRefLevel = (decimal)autorefLevel;
                wlanaSession.RFSAAutoLevel(rfsaSession.Handle, "", 20000000.0, 0.01, 5, out autorefLevel);
                rfsaSession.ConfigureReferenceLevel("", autorefLevel);
                rfsaSession.ConfigureIQPowerEdgeRefTrigger("0", IQTrig, niRFSAConstants.RisingSlope, 0);
                #endregion

            }
            catch (Exception exception)
            {
                if (!string.IsNullOrEmpty(exception.Message))
                    Console.WriteLine(exception.Message);
                else
                    Console.WriteLine("\nAn unidentified error occured in configureHardware\n");
            }
        }

        /*
         * This call applies the settings from Configure to the hardware.  It will take a non-trivial amount of time to execute,
         * so be sure of settings before calling it.
         */
        public void initiate()
        {
            try
            {
                int standard = niWLANAConstants.Standard80211agjpOfdm; //vestigial call from prior iteration.  May remove.
                int numchannels = (int)rxChan;
                wlanaSession.RFSAMeasure(rfsaSession.Handle, null, 10);
            }
            catch (Exception exception)
            { 
                if(!string.IsNullOrEmpty(exception.Message))
                    Console.WriteLine(exception.Message);
                else
                    Console.WriteLine("\nAn unidentified error occured during hardware initiation\n");
            }
        }

        /*
         *
         */
        public void takeMeasurements()
        {
            int intRetVal;
            double doubleRetVal;
            string stringRetVal; //vestigial
            int numChannels = (int)rxChan;
            string aChannel = "";
            int standard = niWLANAConstants.Standard80211agjpOfdm; //also vestigial

            try
            {
                wlanaSession.GetResultOfdmDemodDataRate(null, out intRetVal);
                wlanaSession.GetResultOfdmDemodEffectiveDataRate(null, out doubleRetVal);
                msmtResults.Add("Effective Data Rate", doubleRetVal);

                wlanaSession.GetResultOfdmDemodSpectralFlatnessMargin(null, out doubleRetVal);
                msmtResults.Add("Spectral Flatness Margin", doubleRetVal);

                wlanaSession.GetResultOfdmDemodRmsEvm(aChannel, out doubleRetVal);
                msmtResults.Add("RMS EVM", doubleRetVal);

                wlanaSession.GetResultOfdmDemodPilotRmsEvm(aChannel, out doubleRetVal);
                msmtResults.Add("Pilot RMS EVM", doubleRetVal);

                wlanaSession.GetResultOfdmDemodAverageGatedPower(aChannel, out doubleRetVal);
                msmtResults.Add("AVG Gated Power", doubleRetVal);

                wlanaSession.GetResultOfdmDemodCarrierFrequencyLeakage(aChannel, out doubleRetVal);
                msmtResults.Add("Carrier Freq Leakage", doubleRetVal);

                wlanaSession.GetResultOfdmDemodIqGainImbalance(aChannel, out doubleRetVal);
                msmtResults.Add("IQ Gain Imbalance", doubleRetVal);

                wlanaSession.GetResultOfdmDemodQuadratureSkew(aChannel, out doubleRetVal);
                msmtResults.Add("Quadrature Skew", doubleRetVal);

                wlanaSession.GetResultOfdmDemodPayloadLength(null, out intRetVal);
                msmtResults.Add("Payload Length", intRetVal);

                wlanaSession.GetResultOfdmDemodCarrierFrequencyOffset(null, out doubleRetVal);
                msmtResults.Add("Carrier Frequency Offset", doubleRetVal);

                wlanaSession.GetResultOfdmDemodSampleClockOffset(null, out doubleRetVal);
                msmtResults.Add("Sample Clock Offset",doubleRetVal);

                
                
            }
            catch (Exception exception)
            {
                if (!string.IsNullOrEmpty(exception.Message))
                    Console.WriteLine(exception.Message);
                else
                    Console.WriteLine("\nAn unidentified error occured while taking measurements\n");
            }

        }

        public void getmeasurement(string msmtType)
        {
            try
            {
                msmtType = msmtType.ToLower();
                switch (msmtType)
                { 
                    case "all": //return all measurements
                        foreach (DictionaryEntry entry in msmtResults)
                        {
                            Console.WriteLine("\n{0} : {1}", entry.Key, entry.Value);
                        }
                        break;
                    case "edr" : //Effective Data Rate
                        Console.WriteLine("\n{0}",msmtResults["Effective Data Rate"]);
                        break;
                    case "sfm:" : //Spectral Flatness Margin
                        Console.WriteLine("\n{0}",msmtResults["Spectral Flatness Margin"]);
                        break;
                    case "re": //RMS EVM
                        Console.WriteLine("\n{0}",msmtResults["RMS EVM"]);
                        break;
                    case "pre": //Pilot Channel RMS EVM
                        Console.WriteLine("\n{0}", msmtResults["Pilot RMS EVM"]);
                        break;
                    case "agd": //Average Gated Power
                        Console.WriteLine("\n{0}", msmtResults["AVG Gated Power"]);
                        break;
                    case "cfl": //Carrier Frequency Leakage
                        Console.WriteLine("\n{0}", msmtResults["Carrier Freq Leakage"]);
                        break;
                    case "cfo": //Carrier Frequency Offset
                        Console.WriteLine("\n{0}",msmtResults["Carrier Frequency Offset"]);
                        break;
                    case "igi": //IQ Gain Imbalance
                        Console.WriteLine("\n{0}", msmtResults["IQ Gain Imbalance"]);
                        break;
                    case "qs": //Quadrature Skew
                        Console.WriteLine("\n{0}", msmtResults["Quadrature Skew"]);
                        break;
                    case "pl": //Payload Length
                        Console.WriteLine("\n{0}", msmtResults["Payload Length"]);
                        break;
                    case "sfo": //Sample Clock Offset
                        Console.WriteLine("\n{0}", msmtResults["Sample Clock Offset"]);
                        break;
                    default:
                        Console.WriteLine("\nInvalid Measurement Type Specified in getmeasurement.\n");
                        break;
                }
                
            }
            catch (Exception exception)
            { 
                if(!string.IsNullOrEmpty(exception.Message))
                    Console.WriteLine(exception.Message);
                else
                    Console.WriteLine("\nAn unidentified error occured while returning measurments\n");
            }
        }

        public void closeReferences()
        {
            try
            {
                wlanaSession.Close();
                rfsaSession.Close();
            }
            catch (Exception exception)
            {
                if (!string.IsNullOrEmpty(exception.Message))
                    Console.WriteLine(exception.Message);
                else
                    Console.WriteLine("\nAn unidentified error occured while closing references.\n");
            }
        }

        public struct HardwareSettings
        {
            public string resourceName;
            public decimal autoRefLevel;
            public decimal externalAttenuation;
            public decimal refLevel;
        }
    }


}
