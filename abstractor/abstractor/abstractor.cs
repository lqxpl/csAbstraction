using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.ModularInstruments.Interop;
using NationalInstruments.RFToolkits.Interop;

/*
 * Settings received from ini file:
 * number of receive channels: int32 rxChan
 * acquisition length: double acqLength
 * channel bandwidth: double chanBW
 * amplitude tracking enabled/disabled: bool ampTrack
 * phase tracking enabled/disabled: bool phaseTrack
 * time tracking enabled/disabled:  bool timeTrack
 * channel tracking enabled/disable:  bool channelTrack
 * low pass filter enabled/disabled:  bool lowpass
 * carrier frequency: double carrierFreq
 * IQ Power Edge Trigger Level:  double IQTrigLevel
 */

namespace abstractor
{
    public class WLAN_abstractor
    {
        //required Sessions - Since this will need to be called from Python, some less-than-best-practices will be used.
        /*Many of these don't need to be declared here. Having the here serves 2 purposes:
         * -Mental bookkeeping for when I start working on the python script that reads an ini file for arguments to be provided to this assembly
         * -Future proofing against a scenario where we need to populate 
         */
        niRFSA[] rfsaSession;
        niWLANA wlanaSession;
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

        Hashtable msmtResults = new Hashtable();

        private void populateArgs(bool ampTrack, bool phaseTrack, bool timeTrack, bool channelTrack, bool lowpass) {
            try
            {
                //when an unusual data type is needed, it will be 'translated' from a more universal data type into the needed type here
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

        //hardcoded for the time being, but will allow for some future flexibility.
        const int MaxSegments = 2;
        const int MaxChannels = 4;
        HardwareSettings[] hardware_settings;

        public void configureHardware(Int32 rxChan,double acqLength,double chanBW,
            double carrierFrequency, double IQTrig)
        {
            try
            {
                carrierFreq = carrierFrequency;
                IQTrigLevel = IQTrig;
                //populateArgs(ampTrack,phaseTrack,timeTrack, channelTrack, lowpass);

                if (hardware_settings == null)
                {
                    hardware_settings = new HardwareSettings[MaxSegments * MaxChannels];
                }

                #region hardware settings array
                for (int ii = 0; ii < MaxSegments * MaxChannels; ii++)
                {
                    hardware_settings[ii].resourceName = "RIO" + ii.ToString();
                    hardware_settings[ii].autoReflevel = 0;
                    hardware_settings[ii].externalAttenuation = 0;
                    hardware_settings[ii].refLevel = 3;
                }
                #endregion

                #region wlana settings
                if (wlanaSession == null)
                    wlanaSession = new niWLANA(niWLANAConstants.CompatibilityVersion030000);

                wlanaSession.SetStandard(null, (int)niWLANAConstants.Standard80211agjpOfdm);
                wlanaSession.SetNumberOfReceiveChannels(null, rxChan);
                wlanaSession.SetAcquisitionLength(null, acqLength);
                wlanaSession.SetChannelBandwidth(null, chanBW);

                wlanaSession.SetOfdmDemodEnabled(null, niWLANAConstants.True);
                wlanaSession.SetOfdmDemodAllTracesEnabled(null, niWLANAConstants.True);
                wlanaSession.SetOfdmDemodGatedPowerEnabled(null, niWLANAConstants.True);

                //start and stop times are hard-coded can change this later if the need arises
                wlanaSession.SetOfdmDemodGatedPowerStartTime(null, 0.0);
                wlanaSession.SetOfdmDemodGatedPowerStopTime(null, 0.000064);

                wlanaSession.SetOfdmDemodNumberOfAverages(null, 1);  //hardcoded, time-being yada yada

                wlanaSession.SetOfdmDemodHeaderDetectionEnabled(null, niWLANAConstants.True);
                wlanaSession.SetOfdmDemodMacFrameCheckSequenceCheckEnabled(null, niWLANAConstants.True);

                wlanaSession.SetOfdmDemodMeasurementStartLocation(null, 0);
                wlanaSession.SetOfdmDemodMaximumSymbolsUsed(null, 16);

                wlanaSession.SetOfdmDemodCfoEstimationMethod(null, niWLANAConstants.CfoEstimationMethodPreambleAndData);

                wlanaSession.SetOfdmDemod80211nPlcpFrameDetectionEnabled(null, niWLANAConstants.True);

                wlanaSession.SetOfdmDemodAmplitudeTrackingEnabled(null, AmplitudeTrackingState);
                wlanaSession.SetOfdmDemodPhaseTracking(null, PhaseTrackingState);
                wlanaSession.SetOfdmDemodTimeTrackingEnabled(null, TimeTrackingState);
                wlanaSession.SetOfdmDemodLowpassFilteringEnabled(null, LowpassFilterState);
                wlanaSession.SetOfdmDemodSymbolTimingAdjustment(null, -0.000000200);  //hardcoded, yada yada

                wlanaSession.SetTriggerDelay(null, 0.0);  //hardcoded ""
                #endregion

                #region rfsa settings
                int numchannels = (int)rxChan;

                for (int ii = 0; ii < numchannels; ii++)
                {
                    if (rfsaSession[ii] == null)
                        rfsaSession[ii] = new niRFSA(hardware_settings[ii].resourceName, true, true);

                    rfsaSession[ii].ConfigureIQCarrierFrequency(null, carrierFrequency);
                    rfsaSession[ii].SetDouble(niRFSAProperties.ExternalGain, 0.0);
                    rfsaSession[ii].SetInt32((niRFSAProperties)1150187, 1901);

                    rfsaSession[ii].ConfigureRefClock(niRFSAConstants.PxiClkStr, 10000000.00);

                    double autoRefLevel;
                    autoRefLevel = (double)hardware_settings[ii].refLevel;
                    hardware_settings[ii].autoReflevel = (decimal)autoRefLevel;

                    rfsaSession[ii].ConfigureIQPowerEdgeRefTrigger("0", IQTrig, niRFSAConstants.RisingSlope, 0);

                }

                #endregion

                //Moved to 'initiate' function
                /*int standard = niWLANAConstants.Standard80211agjpOfdm;
                //since the hardcoded standard isn't MIMO, the following block won't ever execute - but should we eventually need it, it is included.
                
                #region inactive code (unless MIMO setup)
                if (standard == niWLANAConstants.Standard80211acMimoOfdm)
                {
                    for (int ii = 0; ii < rxChan; ii++)
                    {
                        long samples;
                        wlanaSession.RFSAConfigureHardware(rfsaSession[ii].Handle, "", out samples);

                        if (ii == 0)
                        {
                            rfsaSession[ii].SetInt32(niRFSAProperties.StartTriggerType, niRFSAConstants.SoftwareEdge);
                            rfsaSession[ii].SetBoolean((niRFSAProperties)1150176, true);
                            rfsaSession[ii].SetBoolean((niRFSAProperties)1150178, true);
                            rfsaSession[ii].SetString((niRFSAProperties)1150177, niRFSAConstants.PxiTrig1Str);
                            rfsaSession[ii].SetString((niRFSAProperties)1150179, niRFSAConstants.PxiTrig2Str);
                            rfsaSession[ii].Commit();
                        }
                        else
                        {
                            rfsaSession[ii].SetInt32(niRFSAProperties.StartTriggerType, niRFSAConstants.DigitalEdge);
                            rfsaSession[ii].SetString(niRFSAProperties.DigitalEdgeStartTriggerSource, "sync_start");
                            rfsaSession[ii].SetString(niRFSAProperties.DigitalEdgeRefTriggerSource, "sync_ref");
                            rfsaSession[ii].SetBoolean((niRFSAProperties)1150176, false);
                            rfsaSession[ii].SetBoolean((niRFSAProperties)1150178, false);
                            rfsaSession[ii].SetString((niRFSAProperties)1150177, niRFSAConstants.PxiTrig1Str);
                            rfsaSession[ii].SetString((niRFSAProperties)1150179, niRFSAConstants.PxiTrig2Str);
                        }
                    }

                    for (int ii = 0; ii < rxChan; ii++) //Sidenote:  still inside of MIMO setup code.
                    {
                        rfsaSession[numchannels - 1 - ii].Initiate(); //All of the settings above this point:  apply them.  This will take some time to execute.
                        if (numchannels - 1 == ii)
                        {
                            rfsaSession[numchannels - 1 - ii].SendSoftwareEdgeTrigger(niRFSAConstants.StartTrigger, "");
                        }
                    }
                    double postTriggerDelay;
                    wlanaSession.GetRecommendedIqPostTriggerDelay("", out postTriggerDelay);

                    long index = 0;
                    while (true)
                    {
                        niComplexNumber[][] data = new niComplexNumber[numchannels][];
                        niRFSA_wfmInfo[] wfmInfo = new niRFSA_wfmInfo[numchannels];
                        long totalSamples = 0;
                        for (int ii = 0; ii < numchannels; ii++)
                        {
                            long numOfSamples;
                            rfsaSession[ii].GetInt64(niRFSAProperties.NumberOfSamples, out numOfSamples);
                            totalSamples += numOfSamples;
                            data[ii] = new niComplexNumber[numOfSamples];
                            rfsaSession[ii].FetchIQSingleRecordComplexF64("", index, numOfSamples, 10, data[ii], out wfmInfo[ii]);
                            wfmInfo[ii].relativeInitialX = postTriggerDelay;
                        }

                        niComplexNumber[] tempdata = new niComplexNumber[totalSamples];
                        long totalLength = 0;
                        for (int i = 0; i < numchannels; i++)
                        {
                            long length = data[i].LongLength;
                            for (long j = 0; j < length; j++)
                            {
                                tempdata[j + totalLength] = data[i][j];
                            }
                            totalLength = totalLength + length;
                        }

                        int averagingDone;
                        bool reset = (index == 0);
                        wlanaSession.AnalyzeMIMOIQComplexF64(Array.ConvertAll<niRFSA_wfmInfo, double>(wfmInfo, x => x.relativeInitialX),
                            Array.ConvertAll<niRFSA_wfmInfo, double>(wfmInfo, x => x.xIncrement),
                            tempdata, numchannels, (int)(totalSamples / (numchannels)), Convert.ToInt32(reset), out averagingDone);
                        if (averagingDone == 1)
                            break;
                    }
                }
                #endregion
                else //exiting code for MIMO setup
                {
                    //reminder:  standard is hardcoded as 80211agjpOfdm.  For the time being, this case will always run.
                    wlanaSession.RFSAMeasure(rfsaSession[0].Handle, null, 10);
                }*/
            }
            catch (Exception exception)
            {
                if (!String.IsNullOrEmpty(exception.Message))
                    Console.WriteLine(exception.Message);
                else
                    Console.WriteLine("\nAn unidentified error occured during Hardware Configuration.\n");
            }
        }

        public void initiate() //This call applies hardware settings setup in Configurehardware.  This call will take a little bit of time to return.
        {
            try
            {
                int standard = niWLANAConstants.Standard80211agjpOfdm;
                int numchannels = (int)rxChan;
                //since the hardcoded standard isn't MIMO, the following block won't ever execute - but should we eventually need it, it is included.

                #region inactive code (unless MIMO setup)
                if (standard == niWLANAConstants.Standard80211acMimoOfdm)
                {
                    for (int ii = 0; ii < rxChan; ii++)
                    {
                        long samples;
                        wlanaSession.RFSAConfigureHardware(rfsaSession[ii].Handle, "", out samples);

                        if (ii == 0)
                        {
                            rfsaSession[ii].SetInt32(niRFSAProperties.StartTriggerType, niRFSAConstants.SoftwareEdge);
                            rfsaSession[ii].SetBoolean((niRFSAProperties)1150176, true);
                            rfsaSession[ii].SetBoolean((niRFSAProperties)1150178, true);
                            rfsaSession[ii].SetString((niRFSAProperties)1150177, niRFSAConstants.PxiTrig1Str);
                            rfsaSession[ii].SetString((niRFSAProperties)1150179, niRFSAConstants.PxiTrig2Str);
                            rfsaSession[ii].Commit();
                        }
                        else
                        {
                            rfsaSession[ii].SetInt32(niRFSAProperties.StartTriggerType, niRFSAConstants.DigitalEdge);
                            rfsaSession[ii].SetString(niRFSAProperties.DigitalEdgeStartTriggerSource, "sync_start");
                            rfsaSession[ii].SetString(niRFSAProperties.DigitalEdgeRefTriggerSource, "sync_ref");
                            rfsaSession[ii].SetBoolean((niRFSAProperties)1150176, false);
                            rfsaSession[ii].SetBoolean((niRFSAProperties)1150178, false);
                            rfsaSession[ii].SetString((niRFSAProperties)1150177, niRFSAConstants.PxiTrig1Str);
                            rfsaSession[ii].SetString((niRFSAProperties)1150179, niRFSAConstants.PxiTrig2Str);
                        }
                    }

                    for (int ii = 0; ii < rxChan; ii++) //Sidenote:  still inside of MIMO setup code.
                    {
                        rfsaSession[numchannels - 1 - ii].Initiate(); //All of the settings above this point:  apply them.  This will take some time to execute.
                        if (numchannels - 1 == ii)
                        {
                            rfsaSession[numchannels - 1 - ii].SendSoftwareEdgeTrigger(niRFSAConstants.StartTrigger, "");
                        }
                    }
                    double postTriggerDelay;
                    wlanaSession.GetRecommendedIqPostTriggerDelay("", out postTriggerDelay);

                    long index = 0;
                    while (true)
                    {
                        niComplexNumber[][] data = new niComplexNumber[numchannels][];
                        niRFSA_wfmInfo[] wfmInfo = new niRFSA_wfmInfo[numchannels];
                        long totalSamples = 0;
                        for (int ii = 0; ii < numchannels; ii++)
                        {
                            long numOfSamples;
                            rfsaSession[ii].GetInt64(niRFSAProperties.NumberOfSamples, out numOfSamples);
                            totalSamples += numOfSamples;
                            data[ii] = new niComplexNumber[numOfSamples];
                            rfsaSession[ii].FetchIQSingleRecordComplexF64("", index, numOfSamples, 10, data[ii], out wfmInfo[ii]);
                            wfmInfo[ii].relativeInitialX = postTriggerDelay;
                        }

                        niComplexNumber[] tempdata = new niComplexNumber[totalSamples];
                        long totalLength = 0;
                        for (int i = 0; i < numchannels; i++)
                        {
                            long length = data[i].LongLength;
                            for (long j = 0; j < length; j++)
                            {
                                tempdata[j + totalLength] = data[i][j];
                            }
                            totalLength = totalLength + length;
                        }

                        int averagingDone;
                        bool reset = (index == 0);
                        wlanaSession.AnalyzeMIMOIQComplexF64(Array.ConvertAll<niRFSA_wfmInfo, double>(wfmInfo, x => x.relativeInitialX),
                            Array.ConvertAll<niRFSA_wfmInfo, double>(wfmInfo, x => x.xIncrement),
                            tempdata, numchannels, (int)(totalSamples / (numchannels)), Convert.ToInt32(reset), out averagingDone);
                        if (averagingDone == 1)
                            break;
                    }
                }
                #endregion
                else //exiting code for MIMO setup
                {
                    //reminder:  standard is hardcoded as 80211agjpOfdm.  For the time being, this case will always run.
                    wlanaSession.RFSAMeasure(rfsaSession[0].Handle, null, 10);
                }
            }
            catch (Exception exception)
            {
                if (!string.IsNullOrEmpty(exception.Message))
                    Console.WriteLine(exception.Message);
                else
                    Console.WriteLine("\nAn unidentified error occured during initiation.\n");
            }
        }

        public void takeMeasurements()
        {
            int intRetVal;
            double doubleRetVal;
            string stringRetVal;
            int numChannels = (int)rxChan;

            int standard = niWLANAConstants.Standard80211agjpOfdm;
            try
            {
                #region msmts for MIMO
                if(standard == niWLANAConstants.Standard80211nMimoOfdm)
                {
                    //flesh out later.  have churned out enough code that will probably not be used i nthis iteration.
                }
                #endregion
                else
                {
                    wlanaSession.GetResultOfdmDemodDataRate(null, out intRetVal);
                    msmtResults.Add("Demod Data Rate",intRetVal);
                    
                    wlanaSession.GetResultOfdmDemodEffectiveDataRate(null,out doubleRetVal);
                    msmtResults.Add("Effective Data Rate",doubleRetVal);

                    wlanaSession.GetResultOfdmDemodSpectralFlatnessMargin(null, out doubleRetVal);
                    msmtResults.Add("Spectral Flatness Margin",doubleRetVal);

                    for(int jj = 0;jj<numChannels;jj++)
                    {
                        double value;
                        string aChannel = "";
                        
                        wlanaSession.GetResultOfdmDemodRmsEvm(aChannel, out value);
                        msmtResults.Add("RMS EVM",value);

                        wlanaSession.GetResultOfdmDemodDataRmsEvm(aChannel, out value);  //something wrong with underlying xml here.  >:-|
                        msmtResults.Add("Data RMS EVM",value);

                        wlanaSession.GetResultOfdmDemodPilotRmsEvm(aChannel, out value); //here too.
                        msmtResults.Add("Pilot RMS EVM", value);

                        wlanaSession.GetResultOfdmDemodAverageGatedPower(aChannel, out value);
                        msmtResults.Add("AVG Gated Power", value);

                        wlanaSession.GetResultOfdmDemodCarrierFrequencyLeakage(aChannel, out value);
                        msmtResults.Add("Carrier Freq Leakage", value);

                        wlanaSession.GetResultOfdmDemodIqGainImbalance(aChannel, out value);
                        msmtResults.Add("IQ Gain Imbalance", value);

                        wlanaSession.GetResultOfdmDemodQuadratureSkew(aChannel, out value);
                        msmtResults.Add("Quadrature Skew", value);
                    }

                    wlanaSession.GetResultOfdmDemodPayloadLength(null, out intRetVal);
                    msmtResults.Add("Payload Length", intRetVal);

                    wlanaSession.GetResultOfdmDemodCarrierFrequencyOffset(null, out doubleRetVal);
                    msmtResults.Add("Carrier Freq Offset", doubleRetVal);

                    wlanaSession.GetResultOfdmDemodSampleClockOffset(null, out doubleRetVal);
                    msmtResults.Add("Sample Clock Offset", doubleRetVal);
                }
            }
            catch (Exception exception)
            {
                if (!string.IsNullOrEmpty(exception.Message))
                    Console.WriteLine(exception.Message);
                else
                    Console.WriteLine("\nAn unidentified error occurred at takeMeasurements.\n");
            }
        }

        public void getMeasurement(string msmtType)
        {
            try
            {
                msmtType = msmtType.ToLower(); //simplify handling for switchcase
                switch (msmtType) { 
                    case "all": //return all msmts
                        Console.WriteLine("\nReturning all measurement results:\n");
                        foreach (DictionaryEntry entry in msmtResults)
                        {
                            Console.WriteLine("\n{0} : {1}",entry.Key,entry.Value);
                        }
                        break;
                    case "ddr": //Demod Data Rate
                        break;
                    case "edr": //Effective Data Rate
                        break;
                    case "sfm":  //Spectral Flatness Margin
                        break;
                    case "re":  //RMS EVM
                        break;
                    case "dre":  //Data RMS EVM
                        break;
                    case "pre": //Pilot Channel RMS EVM
                        break;
                    case "agd": //Average Gated Power
                        break;
                    case "cfl":  //Carrier Frequency Leakage
                        break;
                    case "igi":  //IQ Gain Imbalance
                        break;
                    case "qs":  //Quadrature Skew
                        break;
                    case "pl":  //Payload Length
                        break;
                    case "cfo":  //Carrier Frequency Offset
                        break;
                    case "sco": //Sample Clock Offset
                        break;
                    default: //need to message "unidentified msmt type specified"
                        throw new Exception("\nInvalid measurement type specified in getMeasurement.\n");
                        //break;
                }
                
            }
            catch (Exception exception)
            {
                if (!string.IsNullOrEmpty(exception.Message))
                    Console.WriteLine(exception.Message);
                else
                    Console.WriteLine("\nAn unidentified error occurred at getmeasurement.\n");
            }
        }

        public void closeSessions()
        {
            for (int ii = 0; ii < (int)rxChan; ii++)
            {
                if (rfsaSession[ii] != null)
                {
                    rfsaSession[ii].Close();
                    rfsaSession[ii] = null;
                }
            }

            if (wlanaSession != null)
            {
                wlanaSession.Close();
                wlanaSession = null;
            }
        }

   }
   
    public struct HardwareSettings
    {
        public string resourceName;
        public decimal autoReflevel;
        public decimal externalAttenuation;
        public decimal refLevel;
    }
}
