using System;
using System.Runtime.InteropServices;
using System.Text;
using NationalInstruments.ModularInstruments.Interop;

namespace NationalInstruments.RFToolkits.Interop
{
    public class niWLANA : object, IDisposable
    {
        private System.Runtime.InteropServices.HandleRef _handle;

        private bool _isNamedSession;

        ~niWLANA()
        {
            Dispose(false);
        }

        /// <summary>
        /// Returns a reference to a new or existing niWLAN analysis session.<p>
        /// 
        /// </summary>
        ///<param name = "compatibilityVersion">
        /// compatibilityVersion
        /// int32
        /// Specifies the toolkit compatibility version. 
        ///                         NIWLANA_VAL_COMPATIBILITY_VERSION_010000(10000)
        /// Specifies that the toolkit exhibits version 1.0 behavior and all new features in later releases are unavailable. Select this option if you purchased version 1.0 and want to maintain functional behavior. Refer to the NI WLAN Analysis Toolkit Readme for a list of changes between versions 1.0 and 2.0.
        ///  NIWLANA_VAL_COMPATIBILITY_VERSION_020000(20000)
        /// Specifies that the toolkit exhibits version 2.0 behavior. Select this option to if you want 2.0 behavior and access to new features and bug fixes. Refer to the NI WLAN Analysis Toolkit Readme for a list of changes between versions 1.0 and 2.0.
        /// 
        ///</param>
        public niWLANA(int toolkitCompatibilityVersion)
        {
            System.IntPtr handle;
            int isNewSession;
            int pInvokeResult = PInvoke.niWLANA_OpenSession(String.Empty, toolkitCompatibilityVersion, out handle, out isNewSession);
            TestForError(pInvokeResult);
            _handle = new HandleRef(this, handle);
            _isNamedSession = false;
        }
        /// <summary>
        /// Returns a reference to a new or existing niWLAN analysis session.<p>
        /// 
        /// </summary>
        ///<param name = "sessionName">
        /// sessionName
        /// char[]
        /// Specifies the name of the session that you are looking up or creating. If a session with the same name already exists, this function returns a reference to that session. If you want to get reference to an already opened session x, specify x as the session name. You can obtain a reference to an already existing session multiple times if you have not called the niWLANA_CloseSession function in that session. You do not need to close the session multiple times. To create an unnamed session, pass an empty string or NULL to the sessionName parameter.
        /// Tip  National Instruments recommends that you call the niWLANA_CloseSession function for each uniquely-named instance of the niWLANA_OpenSession function or each instance of the niWLANA_OpenSession function with an unnamed session.
        /// 
        ///</param>
        ///<param name = "compatibilityVersion">
        /// compatibilityVersion
        /// int32
        /// Specifies the toolkit compatibility version. 
        ///                         NIWLANA_VAL_COMPATIBILITY_VERSION_010000(10000)
        /// Specifies that the toolkit exhibits version 1.0 behavior and all new features in later releases are unavailable. Select this option if you purchased version 1.0 and want to maintain functional behavior. Refer to the NI WLAN Analysis Toolkit Readme for a list of changes between versions 1.0 and 2.0.
        ///  NIWLANA_VAL_COMPATIBILITY_VERSION_020000(20000)
        /// Specifies that the toolkit exhibits version 2.0 behavior. Select this option to if you want 2.0 behavior and access to new features and bug fixes. Refer to the NI WLAN Analysis Toolkit Readme for a list of changes between versions 1.0 and 2.0.
        /// 
        ///</param>
        ///<param name = "isNewSession">
        /// isNewSession
        /// int32*
        /// Returns NIWLANA_VAL_TRUE if the function creates a new session. This attribute returns NIWLANA_VAL_FALSE if the function returns a reference to an existing session. 
        /// 
        ///</param>
        public niWLANA(string sessionName, int toolkitCompatibilityVersion, out int isNewSession)
        {
            System.IntPtr handle;
            int pInvokeResult = PInvoke.niWLANA_OpenSession(sessionName, toolkitCompatibilityVersion, out handle, out isNewSession);
            TestForError(pInvokeResult);
            _handle = new HandleRef(this, handle);
            if (String.IsNullOrEmpty(sessionName))
                _isNamedSession = false;
            else
                _isNamedSession = true;
        }

        public HandleRef Handle
        {
            get
            {
                return _handle;
            }
        }

        /// <summary>
        /// 
        /// Performs time-domain, orthogonal frequency division multiplexing (OFDM), and direct sequence spread spectrum (DSSS) demodulation measurements on the complex-valued signal specified by the data parameter. Time-domain power measurement results include average and peak burst power. OFDM demodulation results include error vector magnitude (EVM), header information, and impairments. DSSS demodulation results include EVM, header parameters, packet status, gated power, and impairments. You also can use this function to measure the ramp-up and ramp-down times for DSSS bursts. 
        /// Call an NI-RFSA function that performs acquisition and this function in a loop for the number of times equal to the NIWLANA_NUMBER_OF_ITERATIONS attribute. When the specified number of averages is complete, this function sets the averagingDone parameter to NIWLANA_VAL_TRUE.
        /// After all averages are complete, you can query the results computed by this function. You cannot query results while averaging is in progress. If the function encounters inconsistent values during the averaging process, the NIWLANA_RESULT_DSSS_DEMOD_DATA_RATE and NIWLANA_RESULT_OFDM_DEMOD_DATA_RATE attributes return values of NIWLANA_VAL_RESULTS_DSSS_DATA_RATE_VARIOUS and NIWLANA_VAL_RESULTS_OFDM_DATA_RATE_VARIOUS, respectively, and the NIWLANA_RESULT_DSSS_DEMOD_SFD_FOUND and NIWLANA_RESULT_DSSS_DEMOD_HEADER_CHECKSUM_PASSED attributes return values of NIWLANA_VAL_DSSS_DEMOD_SFD_FOUND_VARIOUS and NIWLANA_VAL_DSSS_DEMOD_HEADER_CHECKSUM_PASSED_VARIOUS, respectively. The NIWLANA_RESULT_OFDM_DEMOD_PAYLOAD_LENGTH and NIWLANA_RESULT_DSSS_DEMOD_PAYLOAD_LENGTH attributes return -1 if the function encounters various data lengths during the averaging process.
        /// 
        /// </summary>
        ///<param name = "t0">
        /// t0
        /// float64
        /// Specifies the trigger (start) time of the data array.
        /// 
        ///</param>
        ///<param name = "dt">
        /// dt
        /// float64
        /// Specifies the time interval between data points in the data array.
        /// 
        ///</param>
        ///<param name = "data">
        /// waveform
        /// niComplexNumber[]
        /// Specifies the acquired complex-valued signal. The real and imaginary parts of this complex array correspond to the in-phase (I) and quadrature-phase (Q) data, respectively.
        /// 
        ///</param>
        ///<param name = "numberofSamples">
        /// numberOfSamples
        /// int32
        /// Specifies the number of complex samples in the data array.
        /// 
        ///</param>
        ///<param name = "reset">
        /// reset
        /// int32
        /// Specifies whether to reset the function. If you set reset to NIWLANA_VAL_TRUE, the results of previous measurements are overwritten.
        /// 
        ///</param>
        ///<param name = "averagingDone">
        /// averagingDone
        /// int32*
        /// Indicates whether the function has completed averaging.
        /// 
        ///</param>
        ///<returns>
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.
        /// The general meaning of the status code is as follows:
        /// Value           Meaning
        /// 0               Success
        /// Positive Values Warnings
        /// Negative Values Errors
        ///
        /// </returns>
        public int AnalyzeIQComplexF64(double t0, double dt, niComplexNumber[] data, int numberofSamples, int reset, out int averagingDone)
        {
            int pInvokeResult = PInvoke.niWLANA_AnalyzeIQComplexF64(Handle, t0, dt, data, numberofSamples, reset, out averagingDone);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Performs time-domain, orthogonal frequency division multiplexing (OFDM) demodulation measurements on the waveform parameter. Time-domain power measurement results include average and peak burst power. OFDM demodulation results include error vector magnitude (EVM), header information, and impairments.
        /// Note&#160;&#160;To use this function, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/n license and set the standard parameter of the niWLANA_SetStandard function to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        /// 
        /// </summary>
        ///<param name = "t0">
        /// t0
        /// float64[]
        /// Specifies the trigger (start) time of the data array. This array should at least be of size specified in numberOfchannels parameter.
        /// 
        ///</param>
        ///<param name = "dt">
        /// dt
        /// float64[]
        /// Specifies the time interval between data points in the data array. This array should at least be of size specified in numberOfchannels parameter.
        /// 
        ///</param>
        ///<param name = "waveforms">
        /// waveform
        /// niComplexNumber*
        /// Specifies the acquired complex-valued signal. The real and imaginary parts of this complex array correspond to the in-phase (I) and quadrature-phase (Q) data, respectively.
        /// You should sequentially append the waveforms from each channel before passing it to analyzer. Size of this array must at least be numberOfchannels times numberOfSamplesInEachWfm.
        /// 
        ///</param>
        ///<param name = "numberofChannels">
        /// numberOfChannels
        /// int32
        /// Specifies the number of channels (waveforms) to process during analysis measurements. The number of receive channels must be less than or equal to the number of channels acquired.
        /// 
        ///</param>
        ///<param name = "numberofSamplesInEachWfm">
        /// numberOfSamplesInEachWfm
        /// int32
        /// Specifies the number of complex samples in each WLAN channel waveform. You can obtain the size of waveform per channel from the samplesPerRecord output of niWLANA_RFSAConfigure function. 
        /// 
        ///</param>
        ///<param name = "reset">
        /// reset
        /// int32
        /// Specifies whether to reset the function. If you set reset to NIWLANA_VAL_TRUE, the results of previous measurements are overwritten.
        /// 
        ///</param>
        ///<param name = "averagingDone">
        /// averagingDone
        /// int32*
        /// Indicates whether the function has completed averaging.
        /// 
        ///</param>
        ///<returns>
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.
        /// The general meaning of the status code is as follows:
        /// Value           Meaning
        /// 0               Success
        /// Positive Values Warnings
        /// Negative Values Errors
        ///
        /// </returns>
        public int AnalyzeMIMOIQComplexF64(double[] t0, double[] dt, niComplexNumber[] waveforms, int numberofRxChains, int numberofSamplesInEachWfm, int reset, out int averagingDone)
        {
            int pInvokeResult = PInvoke.niWLANA_AnalyzeMIMOIQComplexF64(Handle, t0, dt, waveforms, numberofRxChains, numberofSamplesInEachWfm, reset, out averagingDone);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Performs spectral measurements on the input power spectrum. Pass the array of powerSpectrumData parameter from the niRFSA_ReadPowerSpectrumF64 function or niWLANA_RFSAReadGatedPowerSpectrum function to this function.  The toolkit configures hardware for the specified number of spectral averages. Spectral measurements results include spectral mask margin, maximum spectral density, and occupied bandwidth. 
        /// Note&#160;&#160;To use this function, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/n license and set the standard parameter of the niWLANA_SetStandard function to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        /// 
        /// </summary>
        ///<param name = "f0">
        /// f0
        /// float64[]
        /// Specifies the start frequency of the spectrum, in hertz (Hz). This array should at least be of size specified in numberOfchannels parameter.
        /// 
        ///</param>
        ///<param name = "df">
        /// df
        /// float64[]
        /// Specifies the frequency interval between data points in the spectrum. This array should at least be of size specified in numberOfchannels parameter.
        /// 
        ///</param>
        ///<param name = "powerSpectra">
        /// powerSpectra
        /// float64*
        /// Specifies the real-value power spectrum. You should sequentially append the spectrums from each channel before passing it to analyzer. Size of this array must at least be numberOfchannels times numberOfSamplesInEachSpectrum.
        /// 
        ///</param>
        ///<param name = "numberofChannels">
        /// numberOfChannels
        /// int32
        /// Specifies the number of channels (waveforms) to process during analysis measurements. The number of receive channels must be less than or equal to the number of channels acquired.
        /// 
        ///</param>
        ///<param name = "numofSamplesInEachSpectrum">
        /// numberOfSamplesInEachSpectrum
        /// int32
        /// Specifies the number of samples in each WLAN channel spectrum.
        /// 
        ///</param>
        ///<returns>	
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.	
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.	
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.	
        /// The general meaning of the status code is as follows:	
        /// Value           Meaning	
        /// 0               Success	
        /// Positive Values Warnings	
        /// Negative Values Errors	
        ///	
        /// </returns>
        public int AnalyzeMIMOPowerSpectrum(double[] f0, double[] df, double[] powerSpectra, int numberofRxChains, int numofSamplesInEachSpectrum)
        {
            int pInvokeResult = PInvoke.niWLANA_AnalyzeMIMOPowerSpectrum(Handle, f0, df, powerSpectra, numberofRxChains, numofSamplesInEachSpectrum);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Performs spectral measurements on the input power spectrum. Pass the powerSpectrumData parameter from the niRFSA_ReadPowerSpectrumF64 function or niWLANA_RFSAReadGatedPowerSpectrum
        /// function to this function. The toolkit configures hardware for the specified number of spectral averages. 
        /// After this measurement is complete, you can query spectral measurements such as occupied bandwidth (OBW), maximum spectral density, and spectral mask margin. You also can fetch the spectral mask trace.
        /// To compute more than one spectral measurement with a single spectral acquisition, set the Number of Averages attribute to be the same for all measurements.
        /// 
        /// </summary>
        ///<param name = "f0">
        /// f0
        /// float64
        /// Specifies the start frequency of the spectrum, in hertz (Hz).
        /// 
        ///</param>
        ///<param name = "df">
        /// df
        /// float64
        /// Specifies the frequency interval between data points in the spectrum.
        /// 
        ///</param>
        ///<param name = "powerSpectrumData">
        /// powerSpectrumData
        /// float64[]
        /// Contains the real-value power spectrum.
        /// 
        ///</param>
        ///<param name = "dataArraySize">
        /// powerSpectrumDataArraySize
        /// int32
        /// Specifies the size of the powerSpectrumData data array.
        /// 
        ///</param>
        ///<returns>	
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.	
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.	
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.	
        /// The general meaning of the status code is as follows:	
        /// Value           Meaning	
        /// 0               Success	
        /// Positive Values Warnings	
        /// Negative Values Errors	
        ///	
        /// </returns>
        public int AnalyzePowerSpectrum(double f0, double df, double[] powerSpectrumData, int dataArraySize)
        {
            int pInvokeResult = PInvoke.niWLANA_AnalyzePowerSpectrum(Handle, f0, df, powerSpectrumData, dataArraySize);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// Returns the constellation trace of all modulated symbols, which are used to compute error vector magnitude (EVM), after applying all relevant corrections. Enable the NIWLANA_OFDM_DEMOD_ENABLED and NIWLANA_OFDM_DEMOD_CONSTELLATION_TRACE_ENABLED attributes to get the orthogonal frequency division multiplexing (OFDM) constellation trace. Enable the NIWLANA_DSSS_DEMOD_ENABLED and NIWLANA_DSSS_DEMOD_CONSTELLATION_TRACE_ENABLED attributes to get the direct sequence spread spectrum (DSSS) constellation trace.
        /// If you set the standard parameter of the niWLANA_SetStandard function to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use streamx active channel  string to configure this function. 
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// Specifies the WLAN channel for which you want to fetch the measurement.
        /// 
        ///</param>
        ///<param name = "iData">
        /// IData
        /// float64[]
        /// Returns the real part of the constellation.
        /// 
        ///</param>
        ///<param name = "qData">
        /// QData
        /// float64[]
        /// Returns the imaginary part of the constellation. If you pass NULL to IData and QData array top the function returns the size of arrays in the actulaNumDataArrayElements parameter.
        /// 
        ///</param>
        ///<param name = "dataArraySize">
        /// dataArraySize
        /// int32
        /// Specifies the size of the data array.
        /// 
        ///</param>
        ///<param name = "actualNumDataArrayElements">
        /// actualNumDataArrayElements
        /// int32*
        /// Returns the number of elements in the data array. If the IData or the QData arrays are not large enough to hold all the samples, the function returns an error and this parameter returns the minimum expected size of the output array.
        /// 
        ///</param>
        ///<returns>	
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.	
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.	
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.	
        /// The general meaning of the status code is as follows:	
        /// Value           Meaning	
        /// 0               Success	
        /// Positive Values Warnings	
        /// Negative Values Errors	
        ///	
        /// </returns>
        [Obsolete]
        public int GetCurrentIterationConstellation(string channelString, double[] iData, double[] qData, int dataArraySize, out int actualNumDataArrayElements)
        {
            int pInvokeResult = PInvoke.niWLANA_GetCurrentIterationConstellation(Handle, channelString, iData, qData, dataArraySize, out actualNumDataArrayElements);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Returns the EVM per symbol number, in dB, for each iteration when the toolkit processes the acquired burst. The toolkit obtains this trace from the EVM per symbol per subcarrier trace by averaging over the subcarriers.
        /// Set the NIWLANA_OFDM_DEMOD_ENABLED and NIWLANA_OFDM_DEMOD_EVM_PER_SYMBOL_TRACE_ENABLED 
        /// attributes to NIWLANA_VAL_TRUE to get the current iteration EVM per symbol for signals that conform to IEEE Standard 802.11a-1999, IEEE Standard 802.11g-2003, and IEEE Standard 802.11n-2009.
        /// Set the NIWLANA_DSSS_DEMOD_ENABLED and NIWLANA_DSSS_DEMOD_EVM_PER_SYMBOL_TRACE_ENABLED attributes to NIWLANA_VAL_TRUE to get the current iteration EVM per symbol for signals that conform to IEEE Standard 802.11b-1999.
        /// If you set the standard parameter of the niWLANA_SetStandard function to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use channelx or streamx active channel  string to configure this function. 
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// Specifies the WLAN channel for which you want to fetch the trace.
        /// 
        ///</param>
        ///<param name = "eVMperSymbol">
        /// evmPerSymbol
        /// float64[]
        /// Returns the EVM per symbol, in dB, for each iteration during processing of the acquired burst. You can pass NULL to evmPerSymbol parameter to get size of the array in actualArraySize parameter.
        /// 
        ///</param>
        ///<param name = "eVMperSymbolArraySize">
        /// evmPerSymbolArraySize
        /// int32
        /// Specifies the size of the evmPerSymbol array.
        /// 
        ///</param>
        ///<param name = "actualArraySize">
        /// actualArraySize
        /// int32*
        /// Returns the number of elements in the evmPerSymbol array. If the evmPerSymbol array is not large
        /// enough to hold all the samples, the function returns an error and this parameter returns the minimum
        /// expected size of the output array.
        /// 
        ///</param>
        ///<returns>
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.
        /// The general meaning of the status code is as follows:
        /// Value           Meaning
        /// 0               Success
        /// Positive Values Warnings
        /// Negative Values Errors
        ///
        /// </returns>
        [Obsolete]
        public int GetCurrentIterationEVMPerSymbol(string channelString, double[] eVMperSymbol, int eVMperSymbolArraySize, out int actualArraySize)
        {
            int pInvokeResult = PInvoke.niWLANA_GetCurrentIterationEVMPerSymbol(Handle, channelString, eVMperSymbol, eVMperSymbolArraySize, out actualArraySize);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Returns the number of spatial streams according to the modulation and coding scheme (MCS) index detected from the header or provided by the user.
        /// Note&#160;&#160;To use this function, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/n license and set the standard parameter of the niWLANA_SetStandard function to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// Set this parameter to "" (empty string) or NULL.
        ///  No help available
        /// 
        ///</param>
        ///<param name = "numStreams">
        /// numStreams
        /// int32*
        /// Returns the number of spatial streams according to the MCS index detected from the header or provided by the user. 
        /// 
        ///</param>
        ///<returns>	
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.	
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.	
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.	
        /// The general meaning of the status code is as follows:	
        /// Value           Meaning	
        /// 0               Success	
        /// Positive Values Warnings	
        /// Negative Values Errors	
        ///	
        /// </returns>
        public int GetCurrentIterationOFDMDemodNumberOfSpatialStreams(string channelString, out int numStreams)
        {
            int pInvokeResult = PInvoke.niWLANA_GetCurrentIterationOFDMDemodNumberOfSpatialStreams(Handle, channelString, out numStreams);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// Returns the EVM per subcarrier number, in dB, for each iteration when the toolkit processes the acquired burst. The toolkit obtains this trace from the EVM per symbol per subcarrier trace by averaging over the symbols.
        /// Set the NIWLANA_OFDM_DEMOD_ENABLED and NIWLANA_OFDM_DEMOD_EVM_PER_SUBCARRIER_TRACE_ENABLED 
        /// attributes to NIWLANA_VAL_TRUE to get the current iteration EVM per subcarrier.
        /// If you set the standard parameter of the niWLANA_SetStandard function to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use channelx or streamx active channel  string to configure this function. 
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// Specifies the WLAN channel for which you want to fetch the trace.
        /// 
        ///</param>
        ///<param name = "eVMperSubcarrier">
        /// evmPerSubcarrier
        /// float64[]
        /// Returns the EVM per subcarrier, in dB, for each iteration during processing of the acquired burst. You can pass NULL to evmPerSubcarrier parameter to get size of the array in actualArraySize parameter. 
        /// 
        ///</param>
        ///<param name = "eVMperSubcarrierArraySize">
        /// evmPerSubcarrierArraySize
        /// int32
        /// Specifies the size of the evmPerSubcarrier array.
        /// 
        ///</param>
        ///<param name = "actualArraySize">
        /// actualArraySize
        /// int32*
        /// Returns the number of elements in the evmPerSubcarrier array. If the evmPerSubcarrier array is not large
        /// enough to hold all the samples, the function returns an error and this parameter returns the minimum
        /// expected size of the output array. 
        /// 
        ///</param>
        ///<returns>	
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.	
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.	
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.	
        /// The general meaning of the status code is as follows:	
        /// Value           Meaning	
        /// 0               Success	
        /// Positive Values Warnings	
        /// Negative Values Errors	
        ///	
        /// </returns>
        [Obsolete]
        public int GetCurrentIterationOFDMEVMPerSubcarrier(string channelString, double[] eVMperSubcarrier, int eVMperSubcarrierArraySize, out int actualArraySize)
        {
            int pInvokeResult = PInvoke.niWLANA_GetCurrentIterationOFDMEVMPerSubcarrier(Handle, channelString, eVMperSubcarrier, eVMperSubcarrierArraySize, out actualArraySize);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// Returns the EVM per symbol per subcarrier number, in dB, for each iteration when the toolkit processes the acquired burst.
        /// Set the NIWLANA_OFDM_DEMOD_ENABLED and NIWLANA_OFDM_DEMOD_EVM_PER_SYMBOL_PER_SUBCARRIER_TRACE_ENABLED 
        /// attributes to NIWLANA_VAL_TRUE to get the current iteration EVM per symbol per subcarrier.
        /// If you set the standard parameter of the niWLANA_SetStandard function to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use channelx or streamx active channel  string to configure this function. 
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// Specifies the WLAN channel for which you want to fetch the measurement.
        /// 
        ///</param>
        ///<param name = "eVMTrace">
        /// evmTrace
        /// float64*
        /// Returns the EVM per symbol per subcarrier, in dB, for each iteration during processing of the acquired burst. The size of this array should at least be numRows times numColumns. You can pass NULL to get actual size in actualNumRows and actualNumColumns parameters.
        /// 
        ///</param>
        ///<param name = "numRows">
        /// numRows
        /// int32
        /// Specifies the number of rows.
        /// 
        ///</param>
        ///<param name = "numColumns">
        /// numColumns
        /// int32
        /// Specifies the number of columns.
        /// 
        ///</param>
        ///<param name = "actualNumRows">
        /// actualNumRows
        /// int32*
        /// Returns the actual number of rows. If the actualNumRows array is not large
        /// enough to hold all the samples, the function returns an error and this parameter returns the expected number of rows of the evmTrace. 
        /// 
        ///</param>
        ///<param name = "actualNumColumns">
        /// actualNumColumns
        /// int32*
        /// Returns the actual number of columns. If the actualNumColumns array is not large
        /// enough to hold all the samples, the function returns an error and this parameter returns the expected number of columns of the evmTrace.
        /// 
        ///</param>
        ///<returns>	
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.	
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.	
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.	
        /// The general meaning of the status code is as follows:	
        /// Value           Meaning	
        /// 0               Success	
        /// Positive Values Warnings	
        /// Negative Values Errors	
        ///	
        /// </returns>
        [Obsolete]
        public int GetCurrentIterationOFDMEVMPerSymbolPerSubcarrier(string channelString, out double eVMTrace, int numRows, int numColumns, out int actualNumRows, out int actualNumColumns)
        {
            int pInvokeResult = PInvoke.niWLANA_GetCurrentIterationOFDMEVMPerSymbolPerSubcarrier(Handle, channelString, out eVMTrace, numRows, numColumns, out actualNumRows, out actualNumColumns);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// Returns a power versus time (PvT) trace, in dBm, for the acquired burst. Set the NIWLANA_TXPOWER_MEASUREMENTS_ENABLED and NIWLANA_TXPOWER_MEASUREMENTS_PVT_TRACE_ENABLED attributes to NIWLANA_VAL_TRUE to get the current iteration PvT.
        /// If you set the standard parameter of the niWLANA_SetStandard function to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use channelx active channel  string to configure this function. 
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// Specifies the WLAN channel for which you want to fetch the measurement.
        /// 
        ///</param>
        ///<param name = "t0">
        /// t0
        /// float64*
        /// Returns the time of the first value in the data array.
        /// 
        ///</param>
        ///<param name = "dt">
        /// dt
        /// float64*
        /// Returns the time difference between the values in the data array.
        /// 
        ///</param>
        ///<param name = "data">
        /// data
        /// float64[]
        /// Returns the PvT array. You can pass NULL to data parameter to get size of the array in actualArraySize parameter.
        /// 
        ///</param>
        ///<param name = "dataArraySize">
        /// dataArraySize
        /// int32
        /// Specifies the size of the data array. If the array is not large enough to hold all the samples, the function returns error cand this parameter returns the minimum expected size of the output array.
        /// 
        ///</param>
        ///<param name = "actualNumDataArrayElements">
        /// actualNumDataArrayElements
        /// int32*
        /// Returns the number of elements in the data array. If the data array is not large
        /// enough to hold all the samples, the function returns an error and this parameter returns the minimum
        /// expected size of the output array.
        /// 
        ///</param>
        ///<returns>	
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.	
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.	
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.	
        /// The general meaning of the status code is as follows:	
        /// Value           Meaning	
        /// 0               Success	
        /// Positive Values Warnings	
        /// Negative Values Errors	
        ///	
        /// </returns>
        [Obsolete]
        public int GetCurrentIterationPvT(string channelString, out double t0, out double dt, double[] data, int dataArraySize, out int actualNumDataArrayElements)
        {
            int pInvokeResult = PInvoke.niWLANA_GetCurrentIterationPvT(Handle, channelString, out t0, out dt, data, dataArraySize, out actualNumDataArrayElements);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Takes the error code returned by niWLAN Analysis functions and returns the interpretation as a user-readable string.
        /// 
        /// </summary>
        ///<param name = "errorCode">
        /// errorCode
        /// int32
        /// Identifies the error code that is returned from any of the WLAN Analysis functions.
        /// 
        ///</param>
        ///<param name = "errorMessage">
        /// errorMessage
        /// char[]
        /// Returns the user-readable message string that corresponds to the error code you specify. The errorMessage buffer must have at least as many elements as are indicated in errorMessageLen.
        /// 
        ///</param>
        ///<param name = "errorMessageLength">
        /// errorMessageLen
        /// int32
        /// Specifies the length of the errorMessage buffer.
        /// 
        ///</param>
        ///<returns>
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.
        /// The general meaning of the status code is as follows:
        /// Value           Meaning
        /// 0               Success
        /// Positive Values Warnings
        /// Negative Values Errors
        ///
        /// </returns>
        public int GetErrorString(int status, StringBuilder msg)
        {
            int size = PInvoke.niWLANA_GetErrorString(Handle, status, null, 0);
            if ((size >= 0))
            {
                msg.Capacity = size;
                PInvoke.niWLANA_GetErrorString(Handle, status, msg, size);
            }
            return status;
        }
        /// <summary>
        /// 
        /// Queries the value of an niWLAN Analysis 64-bit floating point number (float64) attribute.
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// If the attribute is channel-based, this parameter specifies the channel to which the attribute applies. If the attribute is not channel-based, set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "attributeID">
        /// attributeID
        /// niWLANA_Attr
        /// Specifies the ID of a float64 niWLAN Analysis attribute.
        /// 
        ///</param>
        ///<param name = "attributeValue">
        /// attributeValue
        /// float64*
        /// Returns the current value of an attribute.
        /// 
        ///</param>
        ///<returns>
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.
        /// The general meaning of the status code is as follows:
        /// Value           Meaning
        /// 0               Success
        /// Positive Values Warnings
        /// Negative Values Errors
        ///
        /// </returns>
        public int GetScalarAttributeF64(string channelString, niWLANAProperties attributeID, out double attributeValue)
        {
            int pInvokeResult = PInvoke.niWLANA_GetScalarAttributeF64(Handle, channelString, attributeID, out attributeValue);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Queries the value of an niWLAN Analysis 32-bit integer (int32) attribute.
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// If the attribute is channel-based, this parameter specifies the channel to which the attribute applies. If the attribute is not channel-based, set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "attributeID">
        /// attributeID
        /// niWLANA_Attr
        /// Specifies the ID of an int32 niWLAN Analysis attribute.
        /// 
        ///</param>
        ///<param name = "attributeValue">
        /// attributeValue
        /// int32*
        /// Returns the current value of an attribute.
        /// 
        ///</param>
        ///<returns>
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.
        /// The general meaning of the status code is as follows:
        /// Value           Meaning
        /// 0               Success
        /// Positive Values Warnings
        /// Negative Values Errors
        ///
        /// </returns>
        public int GetScalarAttributeI32(string channelString, niWLANAProperties attributeID, out int attributeValue)
        {
            int pInvokeResult = PInvoke.niWLANA_GetScalarAttributeI32(Handle, channelString, attributeID, out attributeValue);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Returns the spectral mask and power spectral density (PSD) spectrum, as defined in section 18.4.7.3 of IEEE Standard 802.11b-1999, section 17.9.3.2 of IEEE Standard 802.11a-1999, and IEEE Standard 802.11n-2009. Set the NIWLANA_SPECTRAL_MASK_TRACE_ENABLED attribute and either the NIWLANA_SPECTRAL_MEASUREMENTS_ALL_ENABLED or the NIWLANA_SPECTRAL_MASK_ENABLED attribute to NIWLANA_VAL_TRUE to get the current iteration spectral mask trace.
        /// The first element of the spectralMask array contains the spectral mask. The second element of the array contains the PSD trace.
        /// If you set the standard parameter of the niWLANA_SetStandard function to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use channelx active channel  string to configure this function. 
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// Specifies the WLAN channel for which you want to fetch the measurement.
        /// 
        ///</param>
        ///<param name = "f0">
        /// f0
        /// float64*
        /// Returns the initial frequency of the spectrum, in hertz (Hz).
        /// 
        ///</param>
        ///<param name = "df">
        /// df
        /// float64*
        /// Returns the frequency intervals between data points in the spectrum.
        /// 
        ///</param>
        ///<param name = "spectralMask">
        /// spectralMask
        /// float64[]
        /// Returns the spectral mask as the first element of the output array. The second element contains the PSD trace.
        /// 
        ///</param>
        ///<param name = "spectrum">
        /// spectrum
        /// float64[]
        /// Returns the spectral mask with the PSD spectrum superimposed on it. You can pass NULL to spectrum and spectralMask parameter to get size of the array in actualNumDataArrayElements parameter.
        /// 
        ///</param>
        ///<param name = "dataArraySize">
        /// dataArraySize
        /// int32
        /// Specifies the size of the data array.
        /// 
        ///</param>
        ///<param name = "actualNumDataArrayElements">
        /// actualNumDataArrayElements
        /// int32*
        /// Returns the number of elements in the data array. If the spectrum and spectralMask arrays are not large
        /// enough to hold all the samples, the function returns an error and this parameter returns the minimum
        /// expected size of the output array.
        /// 
        ///</param>
        ///<returns>
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.
        /// The general meaning of the status code is as follows:
        /// Value           Meaning
        /// 0               Success
        /// Positive Values Warnings
        /// Negative Values Errors
        ///
        /// </returns>
        [Obsolete]
        public int GetSpectralMask(string channelString, out double f0, out double df, double[] spectralMask, double[] spectrum, int dataArraySize, out int actualNumDataArrayElements)
        {
            int pInvokeResult = PInvoke.niWLANA_GetSpectralMask(Handle, channelString, out f0, out df, spectralMask, spectrum, dataArraySize, out actualNumDataArrayElements);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Queries the value of an niWLAN analysis 64-bit floating point number (float64) array attribute.
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// If the attribute is channel-based, this parameter specifies the channel to which the attribute applies. If the attribute is not channel-based, set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "attributeID">
        /// attributeID
        /// niWLANA_Attr
        /// Specifies the ID of a float64 niWLAN analysis vector attribute.
        /// 
        ///</param>
        ///<param name = "data">
        /// data
        /// float64
        /// Returns the current value of a float64 vector attribute. The array must have at least as many elements as are indicated in the dataArraySize parameter.
        /// 
        ///</param>
        ///<param name = "dataArraySize">
        /// dataArraySize
        /// int32
        /// Specifies the number of elements in the data array.
        /// 
        ///</param>
        ///<param name = "actualNumDataArrayElements">
        /// actualNumDataArrayElements
        /// int32*
        /// Returns the actual number of elements populated in data array attribute. If the array is not large enough to hold all the samples, the function returns an error and this parameter returns the minimum expected size of the output array.
        /// 
        ///</param>
        ///<returns>
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.
        /// The general meaning of the status code is as follows:
        /// Value           Meaning
        /// 0               Success
        /// Positive Values Warnings
        /// Negative Values Errors
        ///
        /// </returns>
        public int GetVectorAttributeF64(string channelString, niWLANAProperties attributeID, double[] data, int dataArraySize, out int actualNumDataArrayElements)
        {
            int pInvokeResult = PInvoke.niWLANA_GetVectorAttributeF64(Handle, channelString, attributeID, data, dataArraySize, out actualNumDataArrayElements);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Resets all attributes to the toolkit default values. 
        /// 
        /// </summary>
        ///<returns>	
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.	
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.	
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.	
        /// The general meaning of the status code is as follows:	
        /// Value           Meaning	
        /// 0               Success	
        /// Positive Values Warnings	
        /// Negative Values Errors	
        ///	
        /// </returns>
        public int ResetSession()
        {
            int pInvokeResult = PInvoke.niWLANA_ResetSession(Handle);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Performs an acquisition and sets the best reference level for the instrument, based on the peak power of the measured signal.
        /// 
        /// </summary>
        ///<param name = "rFSASession">
        /// rfsaSession
        /// ViSession
        /// Specifies a reference to an NI-RFSA instrument session. This parameter is obtained from the niRFSA_init or niRFSA_InitWithOptions function and identifies a particular instrument session. 
        /// 
        ///</param>
        ///<param name = "hardwareChannelString">
        /// hwChannelString[]
        /// char
        /// Specifies the RFSA device channel. Set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "carrierFrequency">
        /// carrierFrequency
        /// double
        /// Specifies the carrier frequency, in hertz (Hz), around which the RF signal analyzer acquires a WLAN signal. Set this value equal to the carrier frequency of the transmitting device under test (DUT). 
        /// 
        ///</param>
        ///<param name = "bandwidth">
        /// bandwidth
        /// double
        /// Specifies the bandwidth, in hertz (Hz), of the signal to be analyzed. 
        /// 
        ///</param>
        ///<param name = "measurementInterval">
        /// measurementInterval
        /// double
        /// Specifies the acquisition length in seconds. This value is used to compute the number of samples to acquire from NI RF signal analyzer. 
        /// 
        ///</param>
        ///<param name = "maxNumberofIterations">
        /// maxNumberOfIterations
        /// int32
        /// Specifies the maximum number of iterations to perform while computing the reference level to be set on NI RF Signal Analyzer. 
        /// 
        ///</param>
        ///<param name = "resultantReferenceLevel">
        /// resultantReferenceLevel
        /// double*
        /// Returns the reference level, in dBm/Hz, that the toolkit uses for spectral mask measurements. 
        /// 
        ///</param>
        ///<returns>	
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.	
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.	
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.	
        /// The general meaning of the status code is as follows:	
        /// Value           Meaning	
        /// 0               Success	
        /// Positive Values Warnings	
        /// Negative Values Errors	
        ///	
        /// </returns>

        public int RFSAAutoLevel(HandleRef rFSASession, string hardwareChannelString, double carrierFrequency, double bandwidth, double measurementInterval, int maxNumberofIterations, out double resultantReferenceLevel)
        {
            int pInvokeResult = PInvoke.niWLANA_RFSAAutoLevel(rFSASession, hardwareChannelString, carrierFrequency, bandwidth, measurementInterval, maxNumberofIterations, out resultantReferenceLevel);
            TestForError(pInvokeResult, rFSASession);
            return pInvokeResult;
        }

        public int RFSAAutoLevel(HandleRef rFSASession, string hardwareChannelString, double bandwidth, double measurementInterval, int maxNumberofIterations, out double resultantReferenceLevel)
        {
            int pInvokeResult = PInvoke.niWLANA_RFSAAutoLevel(rFSASession, hardwareChannelString, bandwidth, measurementInterval, maxNumberofIterations, out resultantReferenceLevel);
            TestForError(pInvokeResult, rFSASession);
            return pInvokeResult;
        }

        /// <summary>
        /// 
        /// Analyzes the incoming signal to measure the maximum input power and acquisition length of the burst and sets these values to the NIWLANA_RESULT_AUTORANGE_MAX_INPUT_POWER and NIWLANA_AUTORANGE_MAX_ACQUISITION_LENGTH attributes, respectively.
        /// You must configure the niRFSA NIWLANA_AUTORANGE_MAX_ACQUISITION_LENGTH attribute before using this function. This function queries the IQ Rate attribute, multiplies this value by the NIWLANA_AUTORANGE_MAX_ACQUISITION_LENGTH value, and sets the value equal to the niRFSA Number Of Samples attribute.
        /// You must configure the NIWLANA_AUTORANGE_MAX_IDLE_TIME attribute before using this function. This function sets the timeout value on the niRFSA Fetch IQ function equal to this parameter.
        /// This function is not available for non-gated spectrum mode.
        /// 
        /// </summary>
        ///<param name = "wLANChannelString">
        /// wlanChannelString
        /// char[]
        /// Specifies the WLAN channel to be used for configuration.
        /// 
        ///</param>
        ///<param name = "rFSASession">
        /// rfsaSession
        /// ViSession
        /// Specifies a reference to an NI-RFSA instrument session. This parameter is obtained from the niRFSA_init or niRFSA_InitWithOptions function and identifies a particular instrument session. 
        /// 
        ///</param>
        ///<param name = "hardwareChannelString">
        /// hwChannelString
        /// char[]
        /// Specifies the RFSA device channel. Set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<returns>
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.
        /// The general meaning of the status code is as follows:
        /// Value           Meaning
        /// 0               Success
        /// Positive Values Warnings
        /// Negative Values Errors
        ///
        /// </returns>
        public int RFSAAutoRange(string wLANChannelString, HandleRef rFSASession, string hardwareChannelString)
        {
            int pInvokeResult = PInvoke.niWLANA_RFSAAutoRange(Handle, wLANChannelString, rFSASession, hardwareChannelString);

            TestForError(pInvokeResult, rFSASession);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Configures the NI&#160;PXIe-5663 and NI&#160;PXIe-5663E for the type of WLAN measurement you specify. If the resetHardware parameter is set to NIWLANA_VAL_TRUE, this function resets the device to a default initialization state.
        /// Note&#160;&#160;For signals conforming to IEEE Standard 802.11a-1999, IEEE Standard 802.11b-1999, or IEEE Standard 802.11g-2003, if the compatibilityVersion parameter of the 
        /// niWLANA_OpenSession function is set to NIWLANA_VAL_COMPATIBILITY_VERSION_010000, the toolkit supports NI PXI-5661, NI PXIe-5663, and NI PXIe-5663E. For signals conforming to IEEE Standard 802.11a-1999, IEEE Standard 802.11b-1999, IEEE Standard 802.11g-2003, or IEEE Standard 802.11n-2009, if the compatibilityVersion parameter is set to NIWLANA_VAL_COMPATIBILITY_VERSION_020000, the toolkit supports NI PXIe-5663 and NI PXIe-5663E.
        /// If you use an RF signal analyzer other than the NI PXIe-5663 or NI PXIe-5663E with this function, the function returns an error. If any of the spectral measurements are enabled, this function configures the analyzer for spectral acquisition. Otherwise, this function configures the analyzer for I/Q acquisition. For NI PXIe-5663 and NI PXIe-5663E, this function configures the NIRFSA_ATTR_REFERENCE_LEVEL attribute as x-overdrive, where x is the NIWLANA_MAX_INPUT_POWER property and overdrive value is chosen to be 6 dB. 
        /// This function configures the NIRFSA_ATTR_REFERENCE_LEVEL attribute according to the following table.
        /// This function also configures the following NI-RFSA attributes:
        /// Resets the NIRFSA_ATTR_IQ_CARRIER_FREQUENCY and sets it to the user-specified value.
        /// Sets the NIRFSA_ATTR_ACQUISITION_TYPE to NIRFSA_VAL_IQ.
        /// Sets the NIRFSA_ATTR_REF_TRIGGER_TYPE to NIRFSA_VAL_IQ_POWER_EDGE if the NIWLANA_IQ_POWER_EDGE_REFERENCE_TRIGGER_ENABLED attribute is set to NIWLANA_VAL_TRUE.
        /// Sets the NIRFSA_ATTR_IQ_POWER_EDGE_REF_TRIGGER_LEVEL attribute to approximately 30&#160;dB below the user-specified mean power.
        /// Sets the NIRFSA_ATTR_IQ_RATE attribute according to the following table. 
        /// Device
        /// Value of the NIWLANA_STANDARD Attribute
        /// Value of the NIRFSA_ATTR_IQ_RATE Attribute
        /// NI PXIe-5663
        /// NIWLANA_VAL_80211AG_OFDM
        /// 40M
        /// NI PXIe-5663
        /// NIWLANA_VAL_80211BG_DSSS
        /// 44M
        /// NI PXIe-5663
        /// NIWLANA_VAL_80211G_DSSS_OFDM
        /// 25M
        /// NI PXIe-5663
        /// NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM
        /// 40M, if channel bandwidth is 20; 50M, if channel bandwidth is 40M
        /// Sets the NIRFSA_ATTR_REF_TRIGGER_PRETRIGGER_SAMPLES attribute to ceil(20 ?s ? sample rate).
        /// Sets the NIRFSA_ATTR_IQ_POWER_EDGE_REF_TRIGGER_MINIMUM_QUIET_TIME attribute to 8 ?s.
        /// Sets the NIRFSA_ATTR_NUM_RECORDS attribute to the maximum of all enabled number of averages, as defined in the NIWLANA_NUMBER_OF_ITERATIONS attribute.
        /// Sets the NIRFSA_ATTR_NUM_RECORDS_IS_FINITE attribute to NIWLANA_VAL_TRUE. 
        /// Sets the NIRFSA_ATTR_NUM_SAMPLES attribute to to IQ Rate*(Acquisition Length+Pretrigger Samples+10 ?s). 
        /// For spectral measurements, this function configures NI-RFSA attributes as follows:
        /// Resets the previous trigger configuration. 
        /// Sets the NIRFSA_ATTR_ACQUISITION_TYPE attribute to NIRFSA_VAL_SPECTRUM.
        /// Sets the NIRFSA_ATTR_RESOLUTION_BANDWIDTH attribute a user-specified value. If the NIWLANA_SPECTRAL_MASK_ENABLED attribute is set to NIWLANA_VAL_TRUE and the NIWLANA_STANDARD attribute is set to NIWLANA_VAL_80211AG_OFDM, NIWLANA_VAL_80211G_DSSS_OFDM, or NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, the toolkit multiplies the number of spectral averages by three to emulate the effects of video bandwidth (VBW).
        /// Sets the NIRFSA_ATTR_SPECTRUM_SPAN attribute  to a user-specified value.
        /// Sets the NIRFSA_ATTR_SPECTRUM_CENTER_FREQUENCY, NIRFSA_ATTR_POWER_SPECTRUM_UNITS, NIRFSA_ATTR_FFT_WINDOW_TYPE, and NIRFSA_ATTR_SPECTRUM_NUMBER_OF_AVERAGES attributes to user-specified values.
        /// 
        /// </summary>
        ///<param name = "wLANChannelString">
        /// wlanChannelString
        /// char[]
        /// Specifies the WLAN channel to be used for configuration.
        /// 
        ///</param>
        ///<param name = "rFSASession">
        /// rfsaSession
        /// ViSession
        /// Specifies a reference to an NI-RFSA instrument session. This parameter is obtained from the niRFSA_init or niRFSA_InitWithOptions function and identifies a particular instrument session. 
        /// 
        ///</param>
        ///<param name = "hardwareChannelString">
        /// hwChannelString
        /// char[]
        /// Specifies the RFSA device channel. Set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "resetHardware">
        /// resetHardware
        /// int32
        /// Specifies whether the NI RF signal analyzer is reset. Set this parameter to TRUE to reset the hardware.
        /// 
        ///</param>
        ///<param name = "samplesPerRecord">
        /// samplesPerRecord
        /// int64*
        /// Returns the number of samples per record configured for the NI-RFSA session.
        /// 
        ///</param>
        ///<returns>
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.
        /// The general meaning of the status code is as follows:
        /// Value           Meaning
        /// 0               Success
        /// Positive Values Warnings
        /// Negative Values Errors
        ///
        /// </returns>
        public int RFSAConfigure(string wLANChannelString, HandleRef rFSASession, string hardwareChannelString, int resetHardware, out long samplesPerRecord)
        {
            int pInvokeResult = PInvoke.niWLANA_RFSAConfigure(Handle, wLANChannelString, rFSASession, hardwareChannelString, resetHardware, out samplesPerRecord);

            TestForError(pInvokeResult, rFSASession);

            return pInvokeResult;
        }
        /// <summary>
        /// Performs measurements on signals conforming to IEEE Standard 802.11a-1999, IEEE Standard 802.11b-1999, or IEEE Standard 802.11g-2003.
        /// 
        /// </summary>
        ///<param name = "rFSASession">
        /// Specifies a reference to an NI-RFSA instrument session. This parameter is obtained from the niRFSA_init or niRFSA_InitWithOptions function and identifies a particular instrument session. 
        /// 
        ///</param>
        ///<param name = "hardwareChannelString">
        /// Specifies the RF signal analyzer channel. Set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "timeout">
        /// Specifies the time, in seconds, allotted for the function to complete before returning a timeout error. A value of &#45;1 specifies the function waits until all data is available. The default value is 10. 
        /// 
        ///</param>
        /// <returns>
        /// Returns the status code of this operation. The status code either indicates success or describes an error or warning condition. Examine the status code from each call to an niGSM generation function to determine if an error has occurred.
        /// To obtain a text description of the status code and additional information about the error condition, call the GetErrorString function.
        /// The general meaning of the status code is as follows:
        /// 
        /// Value           Meaning
        /// ----------------------------------------
        /// 0               Success 
        /// Positive Values Warnings 
        /// Negative Values Exception will be thrown
        /// </returns>
        public int RFSAMeasure(HandleRef rFSASession, string hardwareChannelString, double timeout)
        {
            int pInvokeResult = PInvoke.niWLANA_RFSAMeasure(Handle, rFSASession, hardwareChannelString, timeout);
            TestForError(pInvokeResult, rFSASession);
            return pInvokeResult;
        }
        /// <summary>
        /// Performs measurements on signals conforming to IEEE Standard 802.11n-2009.
        /// 
        /// </summary>
        ///<param name = "rFSASessions">
        /// Specifies an array of references to multiple NI-RFSA instrument sessions. This parameter is obtained from the niRFSA_init or niRFSA_InitWithOptions function and identifies a particular instrument session. 
        /// 
        ///</param>
        ///<param name = "hardwareChannelStrings">
        /// Specifies the RF signal analyzer channels. Set this parameter to NULL.
        /// 
        ///</param>
        ///<param name = "numberofChannels">
        /// Specifies the number of channels (waveforms) to process during analysis measurements. The number of receive channels must be less than or equal to the number of channels acquired.
        /// 
        ///</param>
        ///<param name = "timeout">
        /// Specifies the time, in seconds, allotted for the function to complete before returning a timeout error. A value of &#45;1 specifies the function waits until all data is available. The default value is 10. 
        /// 
        ///</param>
        /// <returns>
        /// Returns the status code of this operation. The status code either indicates success or describes an error or warning condition. Examine the status code from each call to an niGSM generation function to determine if an error has occurred.
        /// To obtain a text description of the status code and additional information about the error condition, call the GetErrorString function.
        /// The general meaning of the status code is as follows:
        /// 
        /// Value           Meaning
        /// ----------------------------------------
        /// 0               Success 
        /// Positive Values Warnings 
        /// Negative Values Exception will be thrown
        /// </returns>
        public int RFSAMIMOMeasure(HandleRef[] rFSASessions, string[] hardwareChannelStrings, int numberofRxChains, double timeout)
        {
            IntPtr[] rfsaIntPtrHandles = new IntPtr[rFSASessions.Length];
            for (int i = 0; i < rFSASessions.Length; i++)
            {
                rfsaIntPtrHandles[i] = rFSASessions[i].Handle;
            }
            int pInvokeResult = PInvoke.niWLANA_RFSAMIMOMeasure(Handle, rfsaIntPtrHandles, hardwareChannelStrings, numberofRxChains, timeout);
            // Rfsa handles are passed into PInvokes as IntPtr[], so we need to make sure that the rfsa wrapper object 
            // is not eligible for garbage collection.
            // TestForError call below, uses HandleRef[] and HandleRef contains the wrapper object hence in this case wrapper object is kept alive.
            // If handles are not used after the PInvoke, unlike this case, then you need to GC.KeepAlive to keep the wrapper object alive.
            TestForError(pInvokeResult, rFSASessions);

            return pInvokeResult;
        }

        /// <summary>
        /// 
        /// Initiates a gated spectrum acquisition for a channel that conforms to IEEE Standard 802.11a-1999, IEEE Standard 802.11b-1999, or IEEE Standard 802.11g-2003 and returns averaged power spectrum data, in volts squared. 
        /// Gated spectrum is the spectrum of the signal acquired in a specified time interval. This acquisition may be initiated by the transition of the signal from an idle state to an active state. Set the NIWLANA_IQ_POWER_EDGE_REFERENCE_TRIGGER_ENABLED attribute to NIWLANA_VAL_TRUE when the acquisition of the signal is triggered. Set the NIWLANA_GATED_SPECTRUM_ENABLED attribute to NIWLANA_VAL_TRUE before calling this function.
        /// 
        /// </summary>
        ///<param name = "wLANChannelString">
        /// wlanChannelString
        /// char[]
        /// Set this parameter to "" (empty string) or NULL.
        ///  No help available
        /// 
        ///</param>
        ///<param name = "rFSASession">
        /// rfsaSession
        /// ViSession
        /// Specifies a reference to an NI-RFSA instrument session. This parameter is obtained from the niRFSA_init or niRFSA_InitWithOptions function and identifies a particular instrument session. 
        /// 
        ///</param>
        ///<param name = "hardwareChannelString">
        /// hwChannelString
        /// char[]
        /// Specifies the RFSA device channel. Set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "timeout">
        /// timeout
        /// float64
        /// Specifies the time, in seconds, allotted for the function to complete before returning a timeout error. A value of &#45;1 specifies the function waits until all data is available. The default value is 10. 
        /// 
        ///</param>
        ///<param name = "f0">
        /// f0
        /// float64*
        /// Returns the start frequency of the spectrum, in hertz (Hz). 
        /// 
        ///</param>
        ///<param name = "df">
        /// df
        /// float64*
        /// Returns the frequency interval between data points in the spectrum.
        /// 
        ///</param>
        ///<param name = "powerSpectrum">
        /// powerSpectrum
        /// float64[]
        /// Returns the real-value power spectrum.
        /// 
        ///</param>
        ///<param name = "powerSpectrumArraySize">
        /// powerSpectrumArraySize
        /// int32
        /// Specifies the size of the powerSpectrum array. The actualNumPowerSpectrumElements parameter contains the size of the spectrum. To obtain the size of the power spectrum, pass NULL to the powerSpectrum parameter. 
        /// 
        ///</param>
        ///<param name = "actualNumPowerSpectrumElement">
        /// actualNumPowerSpectrumElements
        /// int32*
        /// Returns the actual number of elements populated in the powerSpectrum array. If the powerSpectrum array is not large enough to hold all the samples, the function returns error and this parameter returns the minimum expected size of the output array.
        /// 
        ///</param>
        ///<returns>
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.
        /// The general meaning of the status code is as follows:
        /// Value           Meaning
        /// 0               Success
        /// Positive Values Warnings
        /// Negative Values Errors
        ///
        /// </returns>
        public int RFSAReadGatedPowerSpectrum(string wLANChannelString, HandleRef rFSASession, string hardwareChannelString, double timeout, out double f0, out double df, double[] powerSpectrum, int powerSpectrumArraySize, out int actualNumPowerSpectrumElement)
        {
            int pInvokeResult = PInvoke.niWLANA_RFSAReadGatedPowerSpectrum(Handle, wLANChannelString, rFSASession, hardwareChannelString, timeout, out f0, out df, powerSpectrum, powerSpectrumArraySize, out actualNumPowerSpectrumElement);

            TestForError(pInvokeResult, rFSASession);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Initiates a gated spectrum acquisition for one or more IEEE Standard 802.11n-2009 channels and returns averaged power spectrum data, in volts squared. 
        /// Gated spectrum is the spectrum of the signal acquired in a specified time interval. This acquisition may be initiated by the transition of the signal from an idle state to an active state. Set the NIWLANA_IQ_POWER_EDGE_REFERENCE_TRIGGER_ENABLED attribute to NIWLANA_VAL_TRUE when the acquisition of the signal is triggered. Set the NIWLANA_GATED_SPECTRUM_ENABLED attribute to NIWLANA_VAL_TRUE before calling this function.
        /// 
        /// </summary>
        ///<param name = "wLANChannelStrings">
        /// wlanChannelStrings
        /// char*[]
        /// Set this parameter to NULL.
        /// 
        ///</param>
        ///<param name = "rFSASessions">
        /// rfsaSessions
        /// ViSession[]
        /// Specifies array of references to multiple NI-RFSA instrument sessions. This parameter is obtained from the niRFSA_init or niRFSA_InitWithOptions function and identifies a particular instrument session. 
        /// 
        ///</param>
        ///<param name = "hwChannelStrings">
        /// hwChannelStrings
        /// char*[]
        /// Specifies the RFSA device channel. Set this parameter to NULL.
        /// 
        ///</param>
        ///<param name = "timeout">
        /// timeout
        /// float64
        /// Specifies the time, in seconds, allotted for the function to complete before returning a timeout error. A value of &#45;1 specifies the function waits until all data is available. The default value is 10. 
        /// 
        ///</param>
        ///<param name = "f0">
        /// f0
        /// float64[]
        /// Returns the start frequency of the spectrum on each channel, in hertz (Hz). This array should at least be of size specified in numberOfChannels parameter.
        /// 
        ///</param>
        ///<param name = "df">
        /// df
        /// float64[]
        /// Returns the per channel frequency interval between data points in the spectrum. This array should at least be of size specified in numberOfChannels parameter.
        /// 
        ///</param>
        ///<param name = "powerSpectra">
        /// powerSpectra
        /// double*
        /// Returns the real-value power spectra. The spectrums are written sequentially in the array. Allocate an array at least as large as numberOfChannels times individualspectrumSize for this parameter.
        /// 
        ///</param>
        ///<param name = "numberofChannels">
        /// numberOfChannels
        /// int32
        /// Specifies the number of channels (waveforms) to process during analysis measurements. The number of receive channels must be less than or equal to the number of channels acquired.
        /// 
        ///</param>
        ///<param name = "individualSpectrumSize">
        /// individualSpectrumSize
        /// int32
        /// Specifies size of spectrum per WLAN channel. The actualNumSamplesInEachSpectrum parameter contains the size of the spectrum. To obtain the size of the power spectrum, pass NULL to the powerSpectra parameter. 
        /// 
        ///</param>
        ///<param name = "actualNumSamplesInEachSpec">
        /// actualNumSamplesInEachSpectrum
        /// int32*
        /// Returns the actual number of elements for each WLAN channel power spectrum. If the powerSpectra array is not large enough to hold all the samples, the function returns error and this parameter returns the per channel minimum expected size of the output array.
        /// 
        ///</param>
        ///<returns>
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.
        /// The general meaning of the status code is as follows:
        /// Value           Meaning
        /// 0               Success
        /// Positive Values Warnings
        /// Negative Values Errors
        ///
        /// </returns>
        public int RFSAReadMIMOGatedPowerSpectrum(string wLANChannelStrings, HandleRef[] rFSASessions, string hwChannelStrings, double timeout, double[] f0, double[] df, double[] powerSpectra, int numberofRxChains, int individualSpectrumSize, out int actualNumSamplesInEachSpec)
        {
            IntPtr[] rfsaIntPtrHandles = new IntPtr[rFSASessions.Length];
            for (int i = 0; i < rFSASessions.Length; i++)
            {
                rfsaIntPtrHandles[i] = rFSASessions[i].Handle;
            }
            int pInvokeResult = PInvoke.niWLANA_RFSAReadMIMOGatedPowerSpectrum(Handle, wLANChannelStrings, rfsaIntPtrHandles, hwChannelStrings, timeout, f0, df, powerSpectra, numberofRxChains, individualSpectrumSize, out actualNumSamplesInEachSpec);
            // Rfsa handles are passed into PInvokes as IntPtr[], so we need to make sure that the rfsa wrapper object 
            // is not eligible for garbage collection.
            // TestForError call below, uses HandleRef[] and HandleRef contains the wrapper object hence in this case wrapper object is kept alive.
            // If handles are not used after the PInvoke, unlike this case, then you need to GC.KeepAlive to keep the wrapper object alive.
            TestForError(pInvokeResult, rFSASessions);
            return pInvokeResult;
        }

        /// <summary>
        /// 
        /// Enables either demodulation-based or spectral-based measurements.
        /// 
        /// </summary>
        ///<param name = "measurement">
        /// measurement
        /// uInt32
        /// Specifies the measurement that you need to perform. You can specify one of the following measurements.
        /// NIWLANA_VAL_OFDM_DEMOD_MEASUREMENT (0x00000001)
        /// Enables OFDM demodulation-based measurements.
        /// NIWLANA_VAL_OFDM_DEMOD_WITH_GATED_POWER_MEASUREMENT (0x00000002)
        /// Enables OFDM demodulation with gated power measurements.
        /// NIWLANA_VAL_DSSS_DEMOD_MEASUREMENT (0x00000004)
        /// Enables DSSS demodulation-based measurements.
        /// NIWLANA_VAL_DSSS_DEMOD_WITH_GATED_POWER_MEASUREMENT (0x00000008)
        /// Enables DSSS demodulation with gated power measurements.
        /// NIWLANA_VAL_DSSS_POWER_RAMP_MEASUREMENT (0x00000010)
        /// Enables DSSS power ramp measurements.
        /// NIWLANA_VAL_TXPOWER_MEASUREMENT (0x00000020)
        /// Enables Tx power measurements.
        /// NIWLANA_VAL_SPECTRAL_MASK_MEASUREMENT (0x00000040)
        /// Enables spectral mask measurements.
        /// NIWLANA_VAL_OBW_MEASUREMENT (0x00000080)
        /// Enables occupied bandwidth measurements.
        /// NIWLANA_VAL_MAX_SPECTRAL_DENSITY_MEASUREMENT (0x00000100)
        /// Enables maximum spectral density measurements.
        /// 
        ///</param>
        ///<returns>
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.
        /// The general meaning of the status code is as follows:
        /// Value           Meaning
        /// 0               Success
        /// Positive Values Warnings
        /// Negative Values Errors
        ///
        /// </returns>
        public int SelectMeasurements(uint measurement)
        {
            int pInvokeResult = PInvoke.niWLANA_SelectMeasurements(Handle, measurement);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Specifies the duration, in seconds, for which the RF signal analyzer acquires the signal after being triggered. If you do not specify the acquisition length but configure any of the following attributes, the toolkit calculates the acquisition length based on the attributes. 
        /// IEEE Standard 802.11n-2009
        /// NIWLANA_OFDM_PAYLOAD_LENGTH
        /// NIWLANA_GUARD_INTERVAL 
        /// NIWLANA_80211N_PLCP_FRAME_FORMAT
        /// NIWLANA_NUMBER_OF_EXTENSION_SPATIAL_STREAMS
        /// NIWLANA_MCS_INDEX
        /// IEEE Standard 802.11a-1999, IEEE Standard 802.11b-1999, and IEEE Standard 802.11g-2003
        /// NIWLANA_OFDM_PAYLOAD_LENGTH/NIWLANA_DSSS_PAYLOAD_LENGTH
        /// NIWLANA_OFDM_DATA_RATE/NIWLANA_DSSS_DATA_RATE
        /// The toolkit calculates the acquisition length based on the specified parameters. For the attributes that you do not specify, toolkit uses default values. 
        /// Note&#160;&#160;If you only specify one of the above attributes, the recommended acquisition length may not be optimal because the toolkit uses default values for the other attributes.
        ///  If the NIWLANA_GATED_SPECTRUM_MODE attribute is set to NIWLANA_VAL_GATED_SPECTRUM_MODE_ACQUISITION_LENGTH or NIWLANA_VAL_GATED_SPECTRUM_MODE_RBW_AND_ACQUISITION_LENGTH, you must configure the acquisition length parameter before calling the niWLANA_RFSAReadGatedPowerSpectrum or niWLANA_RFSAReadMIMOGatedPowerSpectrum function. The acquisitionLength parameter sets the gate length for the gated spectrum acquisition.
        /// Note&#160;&#160;If you do not set the acquisitionLength parameter, you can set this value using the niWLANA_RFSAAutoRange  function for demodulation and power measurements.
        /// The default value is 1m.
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// Set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "acquisitionLength">
        /// acquisitionLength
        /// float64
        /// Specifies the duration, in seconds, for which the RF signal analyzer acquires the signal after being triggered.
        /// 
        ///</param>
        ///<returns>
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.
        /// The general meaning of the status code is as follows:
        /// Value           Meaning
        /// 0               Success
        /// Positive Values Warnings
        /// Negative Values Errors
        ///
        /// </returns>
        public int SetAcquisitionLength(string channelString, double acquisitionLength)
        {
            int pInvokeResult = PInvoke.niWLANA_SetAcquisitionLength(Handle, channelString, acquisitionLength);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Specifies the carrier frequency, in hertz (Hz), around which the RF signal analyzer acquires a WLAN signal. Set this value equal to the carrier frequency of the transmitting device under test (DUT).
        /// The default value is 2.412G.
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// Set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "carrierFrequency">
        /// carrierFrequency
        /// float64
        /// Specifies the carrier frequency, in Hz, at which the RF signal analyzer acquires a WLAN signal. Set this value equal to the carrier frequency of the transmitting device under test (DUT). 
        /// 
        ///</param>
        ///<returns>	
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.	
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.	
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.	
        /// The general meaning of the status code is as follows:	
        /// Value           Meaning	
        /// 0               Success	
        /// Positive Values Warnings	
        /// Negative Values Errors	
        ///	
        /// </returns>
        [Obsolete]
        public int SetCarrierFrequency(string channelString, double carrierFrequency)
        {
            int pInvokeResult = PInvoke.niWLANA_SetCarrierFrequency(Handle, channelString, carrierFrequency);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// Specifies the bandwidth, in hertz (Hz), of the high throughput (HT) mixed format or Greenfield format signal to be analyzed. The toolkit uses this value in the niWLANA_RFSAConfigure function to determine the appropriate sampling rate and symbol structure for demodulation purposes.
        /// Note&#160;&#160;To use this function, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/n license and set the standard parameter of the niWLANA_SetStandard function to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        /// The default value is 20M. Valid values are 20M and 40M.
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// Set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "channelBandwidth">
        /// channelBandwidth
        /// float64
        /// Specifies the bandwidth, in Hz, of the HT mixed format or Greenfield format signal to be analyzed.
        /// 
        ///</param>
        ///<returns>	
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.	
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.	
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.	
        /// The general meaning of the status code is as follows:	
        /// Value           Meaning	
        /// 0               Success	
        /// Positive Values Warnings	
        /// Negative Values Errors	
        ///	
        /// </returns>
        public int SetChannelBandwidth(string channelString, double channelBandwidth)
        {
            int pInvokeResult = PInvoke.niWLANA_SetChannelBandwidth(Handle, channelString, channelBandwidth);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Specifies whether to enable demodulation-based measurements for 802.11b and 802.11g direct sequence spread spectrum (DSSS) signals.
        /// The default value is NIWLANA_VAL_FALSE.
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// Set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "enabled">
        /// enable
        /// int32
        /// Specifies whether to enable demodulation-based measurements for 802.11b and 802.11g DSSS signals.
        /// NIWLANA_VAL_TRUE (1)
        /// Enables DSSS demodulation-based measurements.
        /// NIWLANA_VAL_FALSE (0)
        /// Disables DSSS demodulation-based measurements. This value is the default.
        /// 
        ///</param>
        ///<returns>
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.
        /// The general meaning of the status code is as follows:
        /// Value           Meaning
        /// 0               Success
        /// Positive Values Warnings
        /// Negative Values Errors
        ///
        /// </returns>
        public int SetDSSSDemodEnabled(string channelString, int enabled)
        {
            int pInvokeResult = PInvoke.niWLANA_SetDSSSDemodEnabled(Handle, channelString, enabled);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Specifies the number of iterations over which the toolkit averages direct sequence spread spectrum (DSSS) demodulation-based measurements.
        /// If you increase the number of averages, the toolkit provides smoother values but takes a longer time to compute the values.
        /// The default value is 1. Valid values are 1 to 1,000 (inclusive).
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// Set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "numberofAverages">
        /// numberOfAverages
        /// int32
        /// Specifies the number of iterations over which the toolkit averages DSSS demodulation-based measurements.
        /// 
        ///</param>
        ///<returns>
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.
        /// The general meaning of the status code is as follows:
        /// Value           Meaning
        /// 0               Success
        /// Positive Values Warnings
        /// Negative Values Errors
        ///
        /// </returns>
        public int SetDSSSDemodNumberOfAverages(string channelString, int numberofAverages)
        {
            int pInvokeResult = PInvoke.niWLANA_SetDSSSDemodNumberOfAverages(Handle, channelString, numberofAverages);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Specifies whether to enable measurement of the power ramp-up or ramp-down time for 802.11b and 802.11g direct sequence spread spectrum (DSSS) signals.
        /// You must set the standard parameter of the niWLANA_SetStandard function to NIWLANA_VAL_80211BG_DSSS to use the niWLANA_SetDSSSPowerRampMeasurementEnabled function. If the standard parameter of the niWLANA_SetStandard function is set to any other value, the toolkit returns an error.
        /// The default value is NIWLANA_VAL_FALSE.
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// Set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "enabled">
        /// enable
        /// int32
        /// Specifies whether to enable measurement of the power ramp-up or ramp-down time.
        /// NIWLANA_VAL_TRUE (1)
        /// Enables DSSS power ramp measurements.
        /// NIWLANA_VAL_FALSE (0)
        /// Disables DSSS power ramp measurements. This value is the default.
        /// 
        ///</param>
        ///<returns>
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.
        /// The general meaning of the status code is as follows:
        /// Value           Meaning
        /// 0               Success
        /// Positive Values Warnings
        /// Negative Values Errors
        ///
        /// </returns>
        public int SetDSSSPowerRampMeasurementEnabled(string channelString, int enabled)
        {
            int pInvokeResult = PInvoke.niWLANA_SetDSSSPowerRampMeasurementEnabled(Handle, channelString, enabled);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Specifies the number of iterations over which the toolkit averages direct sequence spread spectrum (DSSS) power ramp measurements.
        /// If you increase the number of averages, the toolkit provides smoother values but takes a longer time to compute the values.
        /// Valid values are 1 to 1,000 (inclusive).
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// Set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "numberofAverages">
        /// numberOfAverages
        /// int32
        /// Specifies the number of iterations over which the toolkit averages power ramp measurements.
        /// 
        ///</param>
        ///<returns>	
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.	
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.	
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.	
        /// The general meaning of the status code is as follows:	
        /// Value           Meaning	
        /// 0               Success	
        /// Positive Values Warnings	
        /// Negative Values Errors	
        ///	
        /// </returns>
        public int SetDSSSPowerRampMeasurementNumberOfAverages(string channelString, int numberofAverages)
        {
            int pInvokeResult = PInvoke.niWLANA_SetDSSSPowerRampMeasurementNumberOfAverages(Handle, channelString, numberofAverages);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Specifies the number of channels (waveforms) to process during analysis measurements. The number of receive channels must be less than or equal to the number of channels acquired. You can process fewer number of waveforms than acquired waveforms using the numberOfReceiveChannels parameter. 
        /// Note&#160;&#160;To use this function, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/n license and set the standard parameter of the niWLANA_SetStandard function to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        /// Tip  Set the numberOfReceiveChannels parameter equal to number of transmit antennas of the device under test (DUT).
        /// The default value is 1. Valid values are 1 to 4, inclusive.
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// Set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "numberofReceiveChannels">
        /// numberOfReceiveChannels
        /// int32
        /// Specifies the number of channels (waveforms) to process during analysis measurements. 
        /// 
        ///</param>
        ///<returns>	
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.	
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.	
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.	
        /// The general meaning of the status code is as follows:	
        /// Value           Meaning	
        /// 0               Success	
        /// Positive Values Warnings	
        /// Negative Values Errors	
        ///	
        /// </returns>
        public int SetNumberOfReceiveChannels(string channelString, int numberofReceiveChannels)
        {
            int pInvokeResult = PInvoke.niWLANA_SetNumberOfReceiveChannels(Handle, channelString, numberofReceiveChannels);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Specifies the number of iterations over which the toolkit averages orthogonal frequency division multiplexing (OFDM) demodulation-based measurements.
        /// If you increase the number of averages, the toolkit provides smoother values but takes a longer time to compute the values.
        /// The default value is 1. Valid values are 1 to 1,000 (inclusive).
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// Set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "numberofAverages">
        /// numberOfAverages
        /// int32
        /// Specifies the number of iterations over which the toolkit averages OFDM demodulation-based measurements.
        /// 
        ///</param>
        ///<returns>
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.
        /// The general meaning of the status code is as follows:
        /// Value           Meaning
        /// 0               Success
        /// Positive Values Warnings
        /// Negative Values Errors
        ///
        /// </returns>
        public int SetOFDMDemodNumberOfAverages(string channelString, int numberofAverages)
        {
            int pInvokeResult = PInvoke.niWLANA_SetOFDMDemodNumberOfAverages(Handle, channelString, numberofAverages);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Specifies the expected maximum input power level, in dBm, of the incoming signal at the input of the RF signal analyzer. 
        /// If you set the standard parameter of the niWLANA_SetStandard function to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use an active channel  string to configure this function. 
        /// Note&#160;&#160;If you do not know what power level to set, use the niWLANA_RFSAAutoRange or niWLANA_RFSAAutoLevel function.
        /// Note&#160;&#160;In version 1.0, power was specified as average power. In version 2.0, we specify power level as maximum input power.
        /// Note&#160;&#160;If the toolkit compatibility version is NIWLANA_VAL_COMPATIBILITY_VERSION_010000, do not use this function to set the power level. Instead, use niWLANA_SetScalarAttributeF64
        /// function with the attributeId parameter set to  NIWLANAV1_POWER_LEVEL.
        /// Tip  Measurements might not be accurate if the incoming burst has an average power of less than -30 dBm. Consider using a preamplifier to better use the dynamic range of the signal analyzer.
        /// The default value is 0.
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// Specifies the WLAN channel to be used for configuration.
        /// 
        ///</param>
        ///<param name = "maxInputPower">
        /// maxInputPower
        /// float64
        /// Specifies the expected maximum power level, in dBm, of the incoming signal at the input of the RF signal analyzer.
        /// 
        ///</param>
        ///<returns>
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.
        /// The general meaning of the status code is as follows:
        /// Value           Meaning
        /// 0               Success
        /// Positive Values Warnings
        /// Negative Values Errors
        ///
        /// </returns>
        [Obsolete]
        public int SetPowerLevel(string channelString, double maxInputPower)
        {
            int pInvokeResult = PInvoke.niWLANA_SetPowerLevel(Handle, channelString, maxInputPower);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Sets the value of an niWLAN Analysis 64-bit floating point number (float64) attribute.
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// If the attribute is channel-based, this parameter specifies the channel to which the attribute applies. If the attribute is not channel-based, set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "attributeID">
        /// attributeID
        /// niWLANA_Attr
        /// Specifies the ID of a float64 niWLAN Analysis attribute.
        /// 
        ///</param>
        ///<param name = "attributeValue">
        /// attributeValue
        /// float64
        /// Specifies the value to which you want to set the attribute.
        /// 
        ///</param>
        ///<returns>
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.
        /// The general meaning of the status code is as follows:
        /// Value           Meaning
        /// 0               Success
        /// Positive Values Warnings
        /// Negative Values Errors
        ///
        /// </returns>
        public int SetScalarAttributeF64(string channelString, niWLANAProperties attributeID, double attributeValue)
        {
            int pInvokeResult = PInvoke.niWLANA_SetScalarAttributeF64(Handle, channelString, attributeID, attributeValue);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Sets the value of an niWLAN Analysis 32-bit integer (int32) attribute.
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// If the attribute is channel-based, this parameter specifies the channel to which the attribute applies. If the attribute is not channel-based, set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "attributeID">
        /// attributeID
        /// niWLANA_Attr
        /// Specifies the ID of an int32 niWLAN Analysis attribute.
        /// 
        ///</param>
        ///<param name = "attributeValue">
        /// attributeValue
        /// int32
        /// Specifies the value to which you want to set the attribute.
        /// 
        ///</param>
        ///<returns>
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.
        /// The general meaning of the status code is as follows:
        /// Value           Meaning
        /// 0               Success
        /// Positive Values Warnings
        /// Negative Values Errors
        ///
        /// </returns>
        public int SetScalarAttributeI32(string channelString, niWLANAProperties attributeID, int attributeValue)
        {
            int pInvokeResult = PInvoke.niWLANA_SetScalarAttributeI32(Handle, channelString, attributeID, attributeValue);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Specifies whether to enable spectral mask-related measurements.
        /// Refer to section 17.9.3.2 of IEEE Standard 802.11a-1999, section 18.4.7.3 of IEEE Standard 802.11b-1999, section 19.5.4 of IEEE Standard 802.11g-2003, and section 20.3.21.1 of IEEE Standard 802.11n-2009 for details.
        /// The default value is NIWLANA_VAL_FALSE.
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// Set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "enabled">
        /// enable
        /// int32
        /// Specifies whether to enable spectral mask-related measurements.
        /// NIWLANA_VAL_TRUE (1)
        /// Enables the following measurements:
        /// NIWLANA_RESULT_SPECTRAL_MASK_MARGIN
        /// NIWLANA_RESULT_SPECTRAL_MASK_REFERENCE_LEVEL
        /// Spectral mask trace, if NIWLANA_SPECTRAL_MASK_TRACE_ENABLED is enabled.
        /// NIWLANA_VAL_FALSE (0)
        /// Disables spectral mask-related measurements. This value is the default.
        /// 
        ///</param>
        ///<returns>
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.
        /// The general meaning of the status code is as follows:
        /// Value           Meaning
        /// 0               Success
        /// Positive Values Warnings
        /// Negative Values Errors
        ///
        /// </returns>
        public int SetSpectralMaskEnabled(string channelString, int enabled)
        {
            int pInvokeResult = PInvoke.niWLANA_SetSpectralMaskEnabled(Handle, channelString, enabled);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Specifies the number of iterations over which the toolkit averages spectral mask measurements.
        /// If you increase the number of averages, the toolkit provides smoother values but takes a longer time to compute the values.
        /// The default value is 10. Valid values are 1 to 1,000 (inclusive).
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// Set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "numberofAverages">
        /// numberOfAverages
        /// int32
        /// Specifies the number of iterations over which the toolkit averages spectral mask measurements. 
        /// 
        ///</param>
        ///<returns>	
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.	
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.	
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.	
        /// The general meaning of the status code is as follows:	
        /// Value           Meaning	
        /// 0               Success	
        /// Positive Values Warnings	
        /// Negative Values Errors	
        ///	
        /// </returns>
        public int SetSpectralMaskNumberOfAverages(string channelString, int numberofAverages)
        {
            int pInvokeResult = PInvoke.niWLANA_SetSpectralMaskNumberOfAverages(Handle, channelString, numberofAverages);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Specifies the standard for measurements.
        /// Note&#160;&#160;If you do not select a standard, the toolkit returns an error.
        /// The default value is NIWLANA_VAL_STANDARD_80211AG_OFDM.
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// Set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "standard">
        /// standard
        /// int32
        /// Specifies the standard for measurements.
        /// NIWLANA_VAL_STANDARD_80211AG_OFDM (0)
        /// Corresponds to IEEE Standard 802.11a-1099 and the extended rate physical layer-orthogonal frequency division multiplexing (ERP-OFDM) mode defined in IEEE Standard 802.11g-2003. This value is the default.
        /// NIWLANA_VAL_STANDARD_80211BG_DSSS (1)
        /// Corresponds to all the compulsory and optional modes defined in IEEE Standard 802.11b-1999 and the ERP-packet binary convolutional coding (ERP-PBCC) mode in IEEE Standard 802.11g-2003.
        /// NIWLANA_VAL_STANDARD_80211G_DSSS_OFDM (2)
        /// Corresponds to the optional direct sequence spread spectrum-OFDM (DSSS-OFDM) mode defined in IEEE Standard 802.11g-2003.
        /// NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM (3)
        /// Corresponds to IEEE Standard 802.11n-2009. To use this option, you must set the compatibilityVersion parameter of the niWLANA_OpenSession function to NIWLANA_VAL_COMPATIBILITY_VERSION_020000.
        /// 
        ///</param>
        ///<returns>	
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.	
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.	
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.	
        /// The general meaning of the status code is as follows:	
        /// Value           Meaning	
        /// 0               Success	
        /// Positive Values Warnings	
        /// Negative Values Errors	
        ///	
        /// </returns>
        public int SetStandard(string channelString, int standard)
        {
            int pInvokeResult = PInvoke.niWLANA_SetStandard(Handle, channelString, standard);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Specifies whether to enable measurement of peak power and average power in the acquired burst, in dBm. The toolkit automatically detects the start and end of a valid burst corresponding to a WLAN packet.
        /// If the toolkit cannot automatically determine the start of the burst, the toolkit returns an error. If the toolkit cannot determine the end of the burst, the toolkit uses the whole acquired waveform.
        /// The toolkit detects the start of the burst by determining the position at which the total power of a nonoverlapping moving window increases at least 12&#160;dB between two consecutive windows, as well as two windows separated by one window and two window lengths. The toolkit detects the end of the burst by determining the position at which the total power of a moving window decreases at least 12&#160;dB between two consecutive windows, as well as two windows separated by one window.
        /// The default value is NIWLANA_VAL_FALSE.
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// Set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "enabled">
        /// enable
        /// int32
        /// Specifies whether to enable measurement of peak power and average power in the acquired burst.
        /// NIWLANA_VAL_TRUE (1)
        /// Enables measurement of peak power and average power in the acquired burst.
        /// NIWLANA_VAL_FALSE (0)
        /// Disables measurement of peak power and average power in the acquired burst. This value is the default.
        /// 
        ///</param>
        ///<returns>	
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.	
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.	
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.	
        /// The general meaning of the status code is as follows:	
        /// Value           Meaning	
        /// 0               Success	
        /// Positive Values Warnings	
        /// Negative Values Errors	
        ///	
        /// </returns>
        public int SetTxPowerMeasurementEnabled(string channelString, int enabled)
        {
            int pInvokeResult = PInvoke.niWLANA_SetTxPowerMeasurementEnabled(Handle, channelString, enabled);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Specifies the number of iterations over which the toolkit averages burst power measurements.
        /// If you increase the number of averages, the toolkit provides smoother values but takes a longer time to compute the values.
        /// The default value is 1. Valid values are 1 to 1,000 (inclusive).
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// Set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "numberofAverages">
        /// numberOfAverages
        /// int32
        /// Specifies the number of iterations over which the toolkit averages burst power measurements.
        /// 
        ///</param>
        ///<returns>	
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.	
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.	
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.	
        /// The general meaning of the status code is as follows:	
        /// Value           Meaning	
        /// 0               Success	
        /// Positive Values Warnings	
        /// Negative Values Errors	
        ///	
        /// </returns>
        public int SetTxPowerMeasurementNumberOfAverages(string channelString, int numberofAverages)
        {
            int pInvokeResult = PInvoke.niWLANA_SetTxPowerMeasurementNumberOfAverages(Handle, channelString, numberofAverages);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// 
        /// Sets the value of an niWLAN analysis 64-bit floating point number (float64) array attribute.
        /// 
        /// </summary>
        ///<param name = "channelString">
        /// channelString
        /// char[]
        /// If the attribute is channel-based, this parameter specifies the channel to which the attribute applies. If the attribute is not channel-based, set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "attributeID">
        /// attributeID
        /// niWLANA_Attr
        /// Specifies the ID of a float64 niWLAN analysis vector attribute.
        /// 
        ///</param>
        ///<param name = "data">
        /// data
        /// float64[]
        /// Specifies the float64 array to which you want to set the attribute.
        /// 
        ///</param>
        ///<param name = "dataArraySize">
        /// dataArraySize
        /// int32
        /// Specifies the number of elements in the data array.
        /// 
        ///</param>
        ///<returns>
        /// Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition.
        /// Examine the status code from each call to an niWLAN function to determine if an error has occurred.
        /// To obtain a text description of the status code and additional information about the error condition, call the niWLANA_GetErrorString function.
        /// The general meaning of the status code is as follows:
        /// Value           Meaning
        /// 0               Success
        /// Positive Values Warnings
        /// Negative Values Errors
        ///
        /// </returns>
        public int SetVectorAttributeF64(string channelString, niWLANAProperties attributeID, double[] data, int dataArraySize)
        {
            int pInvokeResult = PInvoke.niWLANA_SetVectorAttributeF64(Handle, channelString, attributeID, data, dataArraySize);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// Enables either demodulation-based or spectral-based measurements with the option to enable all traces relevant to the selected measurements.
        /// 
        /// </summary>
        ///<param name = "session">
        ///</param>
        ///<param name = "measurement">
        /// Specifies the measurement to enable.
        /// Note&#160;&#160;Do not select demodulation and spectral measurements simultaneously. Refer to Simultaneous Measurements for more information about compatible measurements.
        /// NIWLANA_VAL_OFDM_DEMOD_MEASUREMENT (0x00000001)
        /// Enables OFDM demodulation-based measurements.
        /// NIWLANA_VAL_OFDM_DEMOD_WITH_GATED_POWER_MEASUREMENT (0x00000002)
        /// Enables OFDM demodulation with gated power measurements.
        /// NIWLANA_VAL_DSSS_DEMOD_MEASUREMENT (0x00000004)
        /// Enables DSSS demodulation-based measurements.
        /// NIWLANA_VAL_DSSS_DEMOD_WITH_GATED_POWER_MEASUREMENT (0x00000008)
        /// Enables DSSS demodulation with gated power measurements.
        /// NIWLANA_VAL_DSSS_POWER_RAMP_MEASUREMENT (0x00000010)
        /// Enables DSSS power ramp measurements.
        /// NIWLANA_VAL_TXPOWER_MEASUREMENT (0x00000020)
        /// Enables transmit power measurements.
        /// NIWLANA_VAL_SPECTRAL_MASK_MEASUREMENT (0x00000040)
        /// Enables spectral mask measurements.
        /// NIWLANA_VAL_OBW_MEASUREMENT (0x00000080)
        /// Enables occupied bandwidth measurements.
        /// NIWLANA_VAL_MAX_SPECTRAL_DENSITY_MEASUREMENT (0x00000100)
        /// Enables maximum spectral density measurements.
        /// 
        ///</param>
        ///<param name = "enableTraces">
        /// Specifies whether to enable all traces relevant to the selected measurement. The default value is NIWLANA_VAL_FALSE.
        /// 
        ///</param>
        ///<returns>
        /// 
        ///niWLANA_SelectMeasurementsWithTraces
        /// int32 __stdcall niWLANA_SelectMeasurementsWithTraces (niWLANAnalysisSession session, uInt32 measurement, int32 tracesEnabled);
        /// Purpose
        /// Enables either demodulation-based or spectral-based measurements with the option to enable all traces relevant to the selected measurements.
        /// 
        ///</returns>
        public int SelectMeasurementsWithTraces(int measurement, int enableTraces)
        {
            int pInvokeResult = PInvoke.niWLANA_SelectMeasurementsWithTraces(Handle, measurement, enableTraces);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// Returns the number of space-time streams according to the modulation and coding scheme (MCS) index and space-time block coding (STBC) index detected from the signal header in the current iteration or from information you provide.
        /// Note&#160;&#160;To use this function, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/j/n/p license and set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        /// 
        /// </summary>
        ///<param name = "session">
        ///</param>
        ///<param name = "channelString">
        /// Set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "numStreams">
        /// Returns the number of space-time streams according to the MCS index  and STBC index detected from the header or from the information you provide. 
        /// 
        ///</param>
        ///<returns>
        /// 
        ///niWLANA_GetCurrentIterationOFDMDemodNumberOfSpaceTimeStreams
        /// int32 __stdcall niWLANA_GetCurrentIterationOFDMDemodNumberOfSpaceTimeStreams(
        ///     niWLANAnalysisSession session,
        ///     char channelString[],
        ///     int32 *numStreams);
        /// Purpose
        /// Returns the number of space-time streams according to the modulation and coding scheme (MCS) index and space-time block coding (STBC) index detected from the signal header in the current iteration or from information you provide.
        /// Note&#160;&#160;To use this function, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/j/n/p license and set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        /// 
        ///</returns>
        public int GetCurrentIterationOFDMDemodNumberOfSpaceTimeStreams(string channelString, out int numStreams)
        {
            int pInvokeResult = PInvoke.niWLANA_GetCurrentIterationOFDMDemodNumberOfSpaceTimeStreams(Handle, channelString, out numStreams);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// Returns the sequence of demodulated complex symbols after applying all relevant corrections for the current iteration. These complex symbols are used to compute error vector magnitude (EVM).
        /// To get the constellation trace, enable the demodulation measurements and the corresponding constellation trace before performing signal analysis. 
        /// If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211AGJP_OFDM, NIWLANA_VAL_STANDARD_80211G_DSSS_OFDM, or NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, set the NIWLANA_OFDM_DEMOD_ENABLED attribute and NIWLANA_OFDM_DEMOD_ALL_TRACES_ENABLED attribute or NIWLANA_OFDM_DEMOD_CONSTELLATION_TRACE_ENABLED attribute to NIWLANA_VAL_TRUE.
        ///  If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211BG_DSSS, set the NIWLANA_DSSS_DEMOD_ENABLED attribute and NIWLANA_DSSS_DEMOD_ALL_TRACES_ENABLED attribute or NIWLANA_DSSS_DEMOD_CONSTELLATION_TRACE_ENABLED attribute to NIWLANA_VAL_TRUE.
        /// If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use a streamx active channel string to configure this function, which returns the constellation trace for the corresponding stream. 
        /// 
        /// </summary>
        ///<param name = "session">
        ///</param>
        ///<param name = "channelString">
        /// Specifies the WLAN channel for which you want to fetch the measurement.
        /// 
        ///</param>
        ///<param name = "iData">
        /// Returns the real part of the constellation point. If you pass NULL to the IData and QData array top, the function returns the size of arrays in the actualArraySize parameter.
        /// 
        ///</param>
        ///<param name = "qData">
        /// Returns the imaginary part of the constellation point. If you pass NULL to IData and QData array top, the function returns the size of arrays in the actualArraySize parameter.
        /// 
        ///</param>
        ///<param name = "dataArraySize">
        /// Specifies the size of the IData and QData arrays.
        /// 
        ///</param>
        ///<param name = "actualArraySize">
        /// Returns the number of elements in the data array. If the IData or the QData arrays are not large enough to hold all the samples, the function returns an error and this parameter returns the minimum expected size of the output array.
        /// 
        ///</param>
        ///<returns>
        /// 
        ///niWLANA_GetCurrentIterationConstellationTrace
        /// int32 __stdcall niWLANA_GetCurrentIterationConstellationTrace(
        ///     niWLANAnalysisSession session,
        ///     char channelString[], 
        ///     float64 IData[],
        ///     float64 QData[],
        ///     int32 dataArraySize,
        ///     int32 *actualArraySize);
        /// Purpose
        /// Returns the sequence of demodulated complex symbols after applying all relevant corrections for the current iteration. These complex symbols are used to compute error vector magnitude (EVM).
        /// To get the constellation trace, enable the demodulation measurements and the corresponding constellation trace before performing signal analysis. 
        /// If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211AGJP_OFDM, NIWLANA_VAL_STANDARD_80211G_DSSS_OFDM, or NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, set the NIWLANA_OFDM_DEMOD_ENABLED attribute and NIWLANA_OFDM_DEMOD_ALL_TRACES_ENABLED attribute or NIWLANA_OFDM_DEMOD_CONSTELLATION_TRACE_ENABLED attribute to NIWLANA_VAL_TRUE.
        ///  If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211BG_DSSS, set the NIWLANA_DSSS_DEMOD_ENABLED attribute and NIWLANA_DSSS_DEMOD_ALL_TRACES_ENABLED attribute or NIWLANA_DSSS_DEMOD_CONSTELLATION_TRACE_ENABLED attribute to NIWLANA_VAL_TRUE.
        /// If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use a streamx active channel string to configure this function, which returns the constellation trace for the corresponding stream. 
        /// 
        ///</returns>
        public int GetCurrentIterationConstellationTrace(string channelString, double[] iData, double[] qData, int dataArraySize, out int actualArraySize)
        {
            int pInvokeResult = PInvoke.niWLANA_GetCurrentIterationConstellationTrace(Handle, channelString, iData, qData, dataArraySize, out actualArraySize);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }
        /// <summary>
        /// Returns a power versus time (PvT) trace, in dBm, for the acquired burst. 
        /// Before performing signal analysis, set the NIWLANA_TXPOWER_MEASUREMENTS_ENABLED and NIWLANA_TXPOWER_MEASUREMENTS_PVT_TRACE_ENABLED attributes to NIWLANA_VAL_TRUE to get the PvT trace for the current iteration.
        /// If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use a channelx active channel string to configure this function, which returns the PvT trace for the corresponding channel. 
        /// 
        /// </summary>
        ///<param name = "session">
        ///</param>
        ///<param name = "channelString">
        /// Specifies the WLAN channel for which you want to fetch the measurement.
        /// 
        ///</param>
        ///<param name = "t0">
        /// Returns the time of the first value in the data array.
        /// 
        ///</param>
        ///<param name = "dt">
        /// Returns the time difference between the values in the data array.
        /// 
        ///</param>
        ///<param name = "pvT">
        /// Returns the PvT trace, in dBm, for the acquired burst. You can pass NULL to the data parameter to get the size of the array in the actualArraySize parameter.
        /// 
        ///</param>
        ///<param name = "dataArraySize">
        /// Specifies the size of the data array. If the array is not large enough to hold all the samples, the function returns an error and this parameter returns the minimum expected size of the output array.
        /// 
        ///</param>
        ///<param name = "actualArraySize">
        /// Returns the number of elements in the data array. If the data array is not large
        /// enough to hold all the samples, the function returns an error and this parameter returns the minimum
        /// expected size of the output array.
        /// 
        ///</param>
        ///<returns>
        /// 
        ///niWLANA_GetCurrentIterationPvTTrace
        /// int32 __stdcall niWLANA_GetCurrentIterationPvTTrace(
        ///     niWLANAnalysisSession session, 
        ///     char channelString[], 
        ///     float64 *t0, 
        ///     float64 *dt,
        ///     float64 pvT[],
        ///     int32 dataArraySize, 
        ///     int32 *actualArraySize);
        /// Purpose
        /// Returns a power versus time (PvT) trace, in dBm, for the acquired burst. 
        /// Before performing signal analysis, set the NIWLANA_TXPOWER_MEASUREMENTS_ENABLED and NIWLANA_TXPOWER_MEASUREMENTS_PVT_TRACE_ENABLED attributes to NIWLANA_VAL_TRUE to get the PvT trace for the current iteration.
        /// If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use a channelx active channel string to configure this function, which returns the PvT trace for the corresponding channel. 
        /// 
        ///</returns>
        public int GetCurrentIterationPvTTrace(string channelString, out double t0, out double dt, double[] pvT, int dataArraySize, out int actualArraySize)
        {
            int pInvokeResult = PInvoke.niWLANA_GetCurrentIterationPvTTrace(Handle, channelString, out t0, out dt, pvT, dataArraySize, out actualArraySize);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// Returns the error vector magnitude (EVM) per symbol/chip number, in dB, for each iteration when the toolkit processes the acquired burst.
        /// If you set the NIWLANA_STANDARD attribute to 80211AGJP_OFDM, 80211G_DSSS_OFDM, or 80211N_MIMO_OFDM, set the NIWLANA_OFDM_DEMOD_ENABLED and NIWLANA_OFDM_DEMOD_EVM_PER_SYMBOL_TRACE_ENABLED 
        /// attributes to NIWLANA_VAL_TRUE to get the current iteration EVM per symbol. The toolkit obtains this trace from the niWLANA_GetCurrentIterationOFDMDemodEVMPerSymbolPerSubcarrierTrace function by averaging over the subcarriers for each symbol.
        /// If you set the NIWLANA_STANDARD attribute to 80211BG_DSSS, set the NIWLANA_DSSS_DEMOD_ENABLED and NIWLANA_DSSS_DEMOD_EVM_PER_CHIP_TRACE_ENABLED attributes to NIWLANA_VAL_TRUE to get the current iteration EVM per chip.
        /// If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use a channelx or streamx active channel  string to configure this function. If you configure this function using channelx active channel string, this function returns the EVM per symbol trace for the corresponding channel. If you configure this function using the a streamx active channel string, this function returns the EVM per symbol trace for the corresponding stream.
        /// 
        /// </summary>
        ///<param name = "session">
        ///</param>
        ///<param name = "channelString">
        /// Specifies the WLAN channel for which you want to fetch the trace.
        /// 
        ///</param>
        ///<param name = "index">
        /// Returns the indices of the subcarriers.
        /// 
        ///</param>
        ///<param name = "eVMperSymbol">
        /// Returns the EVM per symbol, in dB, for each iteration during processing of the acquired burst. You can pass NULL to evmPerSymbol parameter to get size of the array in actualArraySize parameter.
        /// 
        ///</param>
        ///<param name = "dataArraySize">
        /// Specifies the size of the evmPerSymbol array.
        /// 
        ///</param>
        ///<param name = "actualArraySize">
        /// Returns the number of elements in the evmPerSymbol array. If the evmPerSymbol array is not large
        /// enough to hold all the samples, the function returns an error and this parameter returns the minimum
        /// expected size of the output array.
        /// 
        ///</param>
        ///<returns>
        /// 
        ///niWLANA_GetCurrentIterationEVMPerSymbolTrace
        /// int32 __stdcall niWLANA_GetCurrentIterationEVMPerSymbolTrace(
        ///     niWLANAnalysisSession session, 
        ///     char channelString[],
        ///     int32 index[],
        ///     float64 evmPerSymbol[], 
        ///     int32 dataArraySize,
        ///     int32 *actualArraySize);
        /// Purpose
        /// Returns the error vector magnitude (EVM) per symbol/chip number, in dB, for each iteration when the toolkit processes the acquired burst.
        /// If you set the NIWLANA_STANDARD attribute to 80211AGJP_OFDM, 80211G_DSSS_OFDM, or 80211N_MIMO_OFDM, set the NIWLANA_OFDM_DEMOD_ENABLED and NIWLANA_OFDM_DEMOD_EVM_PER_SYMBOL_TRACE_ENABLED 
        /// attributes to NIWLANA_VAL_TRUE to get the current iteration EVM per symbol. The toolkit obtains this trace from the niWLANA_GetCurrentIterationOFDMDemodEVMPerSymbolPerSubcarrierTrace function by averaging over the subcarriers for each symbol.
        /// If you set the NIWLANA_STANDARD attribute to 80211BG_DSSS, set the NIWLANA_DSSS_DEMOD_ENABLED and NIWLANA_DSSS_DEMOD_EVM_PER_CHIP_TRACE_ENABLED attributes to NIWLANA_VAL_TRUE to get the current iteration EVM per chip.
        /// If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use a channelx or streamx active channel  string to configure this function. If you configure this function using channelx active channel string, this function returns the EVM per symbol trace for the corresponding channel. If you configure this function using the a streamx active channel string, this function returns the EVM per symbol trace for the corresponding stream.
        /// 
        ///</returns>
        public int GetCurrentIterationEVMPerSymbolTrace(string channelString, int[] index, double[] eVMperSymbol, int dataArraySize, out int actualArraySize)
        {
            int pInvokeResult = PInvoke.niWLANA_GetCurrentIterationEVMPerSymbolTrace(Handle, channelString, index, eVMperSymbol, dataArraySize, out actualArraySize);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        /// <summary>
        /// Returns the sequence of bits obtained from the signal after demodulation and decoding for each iteration when the toolkit processes the acquired burst.
        /// If the NIWLANA_STANDARD attribute is set to 80211AGJP_OFDM, 80211G_DSSS_OFDM, or 80211N_MIMO_OFDM, set the NIWLANA_OFDM_DEMOD_ENABLED and NIWLANA_OFDM_DEMOD_DECODED_BITS_TRACE_ENABLED attributes to NIWLANA_VAL_TRUE to get the current iteration decoded bits trace.
        /// If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, set the NIWLANA_DSSS_DEMOD_ENABLED and NIWLANA_DSSS_DEMOD_DECODED_BITS_TRACE_ENABLED attributes to NIWLANA_VAL_TRUE to get the current iteration decoded bits trace.  You must use channelx active channel string to configure this function if you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM. 
        /// Note&#160;&#160;The toolkit does not support decoding of received bits for DSSS signals with a data rate of 33 Mbps, and the toolkit cannot return the decoded bits trace. The toolkit returns an error if you call this function after performing demodulation measurements on DSSS signals with a data rate of 33 Mbps.
        /// 
        /// </summary>
        ///<param name = "session">
        ///</param>
        ///<param name = "decodedbits">
        /// Returns the sequence of bits obtained from the signal after demodulation and decoding for each iteration while the toolkit processes the acquired burst.
        /// 
        ///</param>
        ///<param name = "dataArraySize">
        /// Specifies the size of the decodedBits array.
        /// 
        ///</param>
        ///<param name = "actualArraySize">
        /// Returns the number of elements in the data array. If the decodedBits array is not large
        /// enough to hold all the samples, the function returns an error and this parameter returns the minimum
        /// expected size of the output array.
        /// 
        ///</param>
        ///<returns>
        /// 
        ///niWLANA_GetCurrentIterationDecodedBitsTrace
        /// int32 __stdcall niWLANA_GetCurrentIterationDecodedBitsTrace(
        ///     niWLANAnalysisSession session,
        ///     int32 decodedBits[],
        ///     int32 dataArraySize,
        ///     int32 *actualArraySize);
        /// Purpose
        /// Returns the sequence of bits obtained from the signal after demodulation and decoding for each iteration when the toolkit processes the acquired burst.
        /// If the NIWLANA_STANDARD attribute is set to 80211AGJP_OFDM, 80211G_DSSS_OFDM, or 80211N_MIMO_OFDM, set the NIWLANA_OFDM_DEMOD_ENABLED and NIWLANA_OFDM_DEMOD_DECODED_BITS_TRACE_ENABLED attributes to NIWLANA_VAL_TRUE to get the current iteration decoded bits trace.
        /// If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, set the NIWLANA_DSSS_DEMOD_ENABLED and NIWLANA_DSSS_DEMOD_DECODED_BITS_TRACE_ENABLED attributes to NIWLANA_VAL_TRUE to get the current iteration decoded bits trace.  You must use channelx active channel string to configure this function if you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM. 
        /// Note&#160;&#160;The toolkit does not support decoding of received bits for DSSS signals with a data rate of 33 Mbps, and the toolkit cannot return the decoded bits trace. The toolkit returns an error if you call this function after performing demodulation measurements on DSSS signals with a data rate of 33 Mbps.
        /// 
        ///</returns>
        public int GetCurrentIterationDecodedBitsTrace(int[] decodedbits, int dataArraySize, out int actualArraySize)
        {
            int pInvokeResult = PInvoke.niWLANA_GetCurrentIterationDecodedBitsTrace(Handle, decodedbits, dataArraySize, out actualArraySize);

            TestForError(pInvokeResult);

            return pInvokeResult;
        }
        /// <summary>
        /// Returns the spectral mask and power spectral density (PSD) spectrum, as defined in section 18.4.7.3 of IEEE Standard 802.11b-1999, section 17.9.3.2 of IEEE Standard 802.11a-1999, and IEEE Standard 802.11n-2009.
        /// Set the NIWLANA_SPECTRAL_MASK_TRACE_ENABLED attribute and either the NIWLANA_SPECTRAL_MEASUREMENTS_ALL_ENABLED or the NIWLANA_SPECTRAL_MASK_ENABLED attribute to NIWLANA_VAL_TRUE to get the spectral mask trace.
        /// The first element of the spectralMask array contains the spectral mask. The second element of the array contains the PSD trace.
        /// If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use a channelx active channel  string to configure this function. 
        /// 
        /// </summary>
        ///<param name = "session">
        ///</param>
        ///<param name = "channelString">
        /// Specifies the WLAN channel for which you want to fetch the measurement.
        /// 
        ///</param>
        ///<param name = "f0">
        /// Indicates the start frequency of the spectrum, in hertz (Hz).
        /// 
        ///</param>
        ///<param name = "df">
        /// Indicates the frequency intervals between data points in the spectrum.
        /// 
        ///</param>
        ///<param name = "spectralMask">
        /// Returns the spectral mask as the first element of the output array. The second element contains the PSD trace.
        /// 
        ///</param>
        ///<param name = "spectrum">
        /// Returns the spectral mask with the PSD spectrum superimposed on it. You can pass NULL to the spectrum and spectralMask parameters to get the size of the array in the actualArraySize parameter.
        /// 
        ///</param>
        ///<param name = "dataArraySize">
        /// Specifies the size of the spectralMask and spectrum data arrays.
        /// 
        ///</param>
        ///<param name = "actualArraySize">
        /// Returns the number of elements in the data array. If the spectrum and spectralMask arrays are not large
        /// enough to hold all the samples, the function returns an error and this parameter returns the minimum
        /// expected size of the output array.
        /// 
        ///</param>
        ///<returns>
        /// 
        ///niWLANA_GetSpectralMaskTrace
        /// int32 __stdcall niWLANA_GetSpectralMaskTrace(
        ///     niWLANAnalysisSession session,
        ///     char channelString[], 
        ///     float64 *f0,
        ///     float64 *df,
        ///     float64 spectralMask[],
        ///     float64 spectrum[],
        ///     int32 dataArraySize,
        ///     int32 *actualArraySize);
        /// Purpose
        /// Returns the spectral mask and power spectral density (PSD) spectrum, as defined in section 18.4.7.3 of IEEE Standard 802.11b-1999, section 17.9.3.2 of IEEE Standard 802.11a-1999, and IEEE Standard 802.11n-2009.
        /// Set the NIWLANA_SPECTRAL_MASK_TRACE_ENABLED attribute and either the NIWLANA_SPECTRAL_MEASUREMENTS_ALL_ENABLED or the NIWLANA_SPECTRAL_MASK_ENABLED attribute to NIWLANA_VAL_TRUE to get the spectral mask trace.
        /// The first element of the spectralMask array contains the spectral mask. The second element of the array contains the PSD trace.
        /// If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use a channelx active channel  string to configure this function. 
        /// 
        ///</returns>
        public int GetSpectralMaskTrace(string channelString, out double f0, out double df, double[] spectralMask, double[] spectrum, int dataArraySize, out int actualArraySize)
        {
            int pInvokeResult = PInvoke.niWLANA_GetSpectralMaskTrace(Handle, channelString, out f0, out df, spectralMask, spectrum, dataArraySize, out actualArraySize);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }


        /// <summary>
        /// Returns the error vector magnitude (EVM) per symbol per subcarrier number, in dB, for each iteration when the toolkit processes the acquired burst.
        /// Set the NIWLANA_OFDM_DEMOD_ENABLED and NIWLANA_OFDM_DEMOD_EVM_PER_SYMBOL_PER_SUBCARRIER_TRACE_ENABLED 
        /// attributes to NIWLANA_VAL_TRUE to get the current iteration EVM per symbol per subcarrier.
        /// If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use a channelx or streamx active channel string to configure this function. If you configure this function using a channelx active channel string, this function returns the EVM per symbol per subcarrier trace for the corresponding channel. If you configure this function using the streamx active channel string, this function returns the EVM per symbol per subcarrier trace for the corresponding stream.
        /// 
        /// </summary>
        ///<param name = "session">
        ///</param>
        ///<param name = "channelString">
        /// Specifies the WLAN channel for which you want to fetch the measurement.
        /// 
        ///</param>
        ///<param name = "index">
        /// Returns the indices of the subcarriers.
        /// 
        ///</param>
        ///<param name = "eVMTrace">
        /// Returns the EVM per symbol per subcarrier, in dB, for each iteration during processing of the acquired burst. The size of this array should at least be numRows times numColumns. You can pass NULL to get actual size in the actualNumRows and actualNumColumns parameters.
        /// 
        ///</param>
        ///<param name = "numRows">
        /// Specifies the number of rows for the evmTrace data array.
        /// 
        ///</param>
        ///<param name = "numColumns">
        /// Specifies the number of columns for the evmTrace data array.
        /// 
        ///</param>
        ///<param name = "actualNumRows">
        /// Returns the actual number of rows. If the actualNumRows array is not large
        /// enough to hold all the samples, the function returns an error and this parameter returns the expected number of rows of the evmTrace. 
        /// 
        ///</param>
        ///<param name = "actualNumColumns">
        /// Returns the actual number of columns. If the actualNumColumns array is not large
        /// enough to hold all the samples, the function returns an error and this parameter returns the expected number of columns of the evmTrace.
        /// 
        ///</param>
        ///<returns>
        /// 
        ///niWLANA_GetCurrentIterationOFDMDemodEVMPerSymbolPerSubcarrierTrace
        /// int32 __stdcall niWLANA_GetCurrentIterationOFDMDemodEVMPerSymbolPerSubcarrierTrace(
        ///     niWLANAnalysisSession session,
        ///     char channelString[],
        ///     int32 index[],
        ///     float64 *evmTrace,
        ///     int32 numRows,  
        ///     int32 numColumns, 
        ///     int32 *actualNumRows,
        ///     int32 *actualNumColumns);
        /// Purpose
        /// Returns the error vector magnitude (EVM) per symbol per subcarrier number, in dB, for each iteration when the toolkit processes the acquired burst.
        /// Set the NIWLANA_OFDM_DEMOD_ENABLED and NIWLANA_OFDM_DEMOD_EVM_PER_SYMBOL_PER_SUBCARRIER_TRACE_ENABLED 
        /// attributes to NIWLANA_VAL_TRUE to get the current iteration EVM per symbol per subcarrier.
        /// If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use a channelx or streamx active channel string to configure this function. If you configure this function using a channelx active channel string, this function returns the EVM per symbol per subcarrier trace for the corresponding channel. If you configure this function using the streamx active channel string, this function returns the EVM per symbol per subcarrier trace for the corresponding stream.
        /// 
        ///</returns>
        public int GetCurrentIterationOFDMDemodEVMPerSymbolPerSubcarrierTrace(string channelString, int[] index, out double eVMTrace, int numRows, int numColumns, out int actualNumRows, out int actualNumColumns)
        {
            int pInvokeResult = PInvoke.niWLANA_GetCurrentIterationOFDMDemodEVMPerSymbolPerSubcarrierTrace(Handle, channelString, index, out eVMTrace, numRows, numColumns, out actualNumRows, out actualNumColumns);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        /// <summary>
        /// Returns the error vector magnitude (EVM) per subcarrier number, in dB, for each iteration when the toolkit processes the acquired burst. The toolkit obtains this trace from the niWLANA_GetCurrentIterationOFDMDemodEVMPerSymbolPerSubcarrierTrace function by averaging over the symbols.
        /// Set the NIWLANA_OFDM_DEMOD_ENABLED and NIWLANA_OFDM_DEMOD_EVM_PER_SUBCARRIER_TRACE_ENABLED 
        /// attributes to NIWLANA_VAL_TRUE to get the current iteration EVM per subcarrier.
        /// If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use a channelx or streamx active channel string to configure this function. If you configure this function using a channelx active channel string, this function returns the EVM per subcarrier trace for the corresponding channel. If you configure this function using the streamx active channel string, this function returns the EVM per subcarrier trace for the corresponding stream.
        /// 
        /// </summary>
        ///<param name = "session">
        ///</param>
        ///<param name = "channelString">
        /// Specifies the WLAN channel for which you want to fetch the trace.
        /// 
        ///</param>
        ///<param name = "index">
        /// Returns the indices of the subcarriers. 
        /// 
        ///</param>
        ///<param name = "eVMperSubcarrier">
        /// Returns the EVM per subcarrier, in dB, for each iteration during processing of the acquired burst. You can pass NULL to evmPerSubcarrier parameter to get size of the array in actualArraySize parameter. 
        /// 
        ///</param>
        ///<param name = "dataArraySize">
        /// Specifies the size of the evmPerSubcarrier array.
        /// 
        ///</param>
        ///<param name = "actualArraySize">
        /// Returns the number of elements in the evmPerSubcarrier array. If the evmPerSubcarrier array is not large
        /// enough to hold all the samples, the function returns an error and this parameter returns the minimum
        /// expected size of the output array. 
        /// 
        ///</param>
        ///<returns>
        /// 
        ///niWLANA_GetCurrentIterationOFDMDemodEVMPerSubcarrierTrace
        /// int32 __stdcall niWLANA_GetCurrentIterationOFDMDemodEVMPerSubcarrierTrace(
        ///     niWLANAnalysisSession session, 
        ///     char channelString[],
        ///     int32 index[],
        ///     float64 evmPerSubcarrier[], 
        ///     int32 dataArraySize,
        ///     int32 *actualArraySize);
        /// Purpose
        /// Returns the error vector magnitude (EVM) per subcarrier number, in dB, for each iteration when the toolkit processes the acquired burst. The toolkit obtains this trace from the niWLANA_GetCurrentIterationOFDMDemodEVMPerSymbolPerSubcarrierTrace function by averaging over the symbols.
        /// Set the NIWLANA_OFDM_DEMOD_ENABLED and NIWLANA_OFDM_DEMOD_EVM_PER_SUBCARRIER_TRACE_ENABLED 
        /// attributes to NIWLANA_VAL_TRUE to get the current iteration EVM per subcarrier.
        /// If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use a channelx or streamx active channel string to configure this function. If you configure this function using a channelx active channel string, this function returns the EVM per subcarrier trace for the corresponding channel. If you configure this function using the streamx active channel string, this function returns the EVM per subcarrier trace for the corresponding stream.
        /// 
        ///</returns>
        public int GetCurrentIterationOFDMDemodEVMPerSubcarrierTrace(string channelString, int[] index, double[] eVMperSubcarrier, int dataArraySize, out int actualArraySize)
        {
            int pInvokeResult = PInvoke.niWLANA_GetCurrentIterationOFDMDemodEVMPerSubcarrierTrace(Handle, channelString, index, eVMperSubcarrier, dataArraySize, out actualArraySize);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        /// <summary>
        /// Returns the error vector magnitude (EVM) per symbol number, in dB, for data subcarriers for each iteration when the toolkit processes the acquired burst.
        /// The toolkit obtains this trace from the niWLANA_GetCurrentIterationOFDMDemodEVMPerSymbolPerSubcarrierTrace function by averaging over the data subcarriers for each symbol.
        /// Set the NIWLANA_OFDM_DEMOD_ENABLED and NIWLANA_OFDM_DEMOD_EVM_PER_SYMBOL_TRACE_ENABLED 
        /// attributes to NIWLANA_VAL_TRUE to get the current iteration data EVM per symbol.
        /// If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use a channelx or streamx active channel string to configure this function. If you configure this function using a channelx active channel string, this function returns the data EVM per symbol trace for the corresponding channel. If you configure this function using a streamx active channel string, this function returns the data EVM per symbol trace for the corresponding stream.
        /// 
        /// </summary>
        ///<param name = "session">
        ///</param>
        ///<param name = "channelString">
        /// Specifies the WLAN channel for which you want to fetch the trace.
        /// 
        ///</param>
        ///<param name = "index">
        /// Returns the indices of the symbols.
        /// 
        ///</param>
        ///<param name = "dataEVMperSymbol">
        /// Returns the data EVM, in dB, of symbols and corresponding symbol indices.
        /// 
        ///</param>
        ///<param name = "dataArraySize">
        /// Specifies the size of the evmPerSymbol array.
        /// 
        ///</param>
        ///<param name = "actualArraySize">
        /// Returns the number of elements in the evmPerSymbol array. If the evmPerSymbol array is not large
        /// enough to hold all the samples, the function returns an error and this parameter returns the minimum
        /// expected size of the output array.
        /// 
        ///</param>
        ///<returns>
        /// 
        ///niWLANA_GetCurrentIterationOFDMDemodDataEVMPerSymbolTrace
        /// int32 __stdcall niWLANA_GetCurrentIterationOFDMDemodDataEVMPerSymbolTrace(
        ///     niWLANAnalysisSession session,
        ///     char channelString[], 
        ///     int32 index[],
        ///     float64 dataEVMPerSymbol[],
        ///     int32 dataArraySize,
        ///     int32 *actualArraySize);
        /// Purpose
        /// Returns the error vector magnitude (EVM) per symbol number, in dB, for data subcarriers for each iteration when the toolkit processes the acquired burst.
        /// The toolkit obtains this trace from the niWLANA_GetCurrentIterationOFDMDemodEVMPerSymbolPerSubcarrierTrace function by averaging over the data subcarriers for each symbol.
        /// Set the NIWLANA_OFDM_DEMOD_ENABLED and NIWLANA_OFDM_DEMOD_EVM_PER_SYMBOL_TRACE_ENABLED 
        /// attributes to NIWLANA_VAL_TRUE to get the current iteration data EVM per symbol.
        /// If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use a channelx or streamx active channel string to configure this function. If you configure this function using a channelx active channel string, this function returns the data EVM per symbol trace for the corresponding channel. If you configure this function using a streamx active channel string, this function returns the data EVM per symbol trace for the corresponding stream.
        /// 
        ///</returns>
        public int GetCurrentIterationOFDMDemodDataEVMPerSymbolTrace(string channelString, int[] index, double[] dataEVMperSymbol, int dataArraySize, out int actualArraySize)
        {
            int pInvokeResult = PInvoke.niWLANA_GetCurrentIterationOFDMDemodDataEVMPerSymbolTrace(Handle, channelString, index, dataEVMperSymbol, dataArraySize, out actualArraySize);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        /// <summary>
        /// Returns the pilot error vector magnitude (EVM) per symbol number, in dB, for each iteration when the toolkit processes the acquired burst. The toolkit obtains this trace from the niWLANA_GetCurrentIterationOFDMDemodEVMPerSymbolPerSubcarrierTrace function by averaging over the pilot subcarrier for each symbol.
        /// Set the NIWLANA_OFDM_DEMOD_ENABLED and NIWLANA_OFDM_DEMOD_EVM_PER_SYMBOL_TRACE_ENABLED 
        /// attributes to NIWLANA_VAL_TRUE to get the current iteration pilot EVM per symbol trace.
        /// If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use a channelx or streamx active channel string to configure this function. If you configure this function using a channelx active channel string, this function returns the pilot EVM per symbol trace for the corresponding channel. If you configure this function using a streamx active channel string, this function returns the pilot EVM per symbol trace for the corresponding stream.
        /// 
        /// </summary>
        ///<param name = "session">
        ///</param>
        ///<param name = "channelString">
        /// Specifies the WLAN channel for which you want to fetch the trace.
        /// 
        ///</param>
        ///<param name = "index">
        /// Returns the indices of the symbols.
        /// 
        ///</param>
        ///<param name = "pilotEVMperSymbol">
        /// Returns the pilot EVM per symbol, in dB, for each iteration during processing of the acquired burst.
        /// 
        ///</param>
        ///<param name = "dataArraySize">
        /// Specifies the size of the pilotEvmPerSymbol array.
        /// 
        ///</param>
        ///<param name = "actualArraySize">
        /// Returns the number of elements in the pilotEvmPerSymbol array. If the pilotEvmPerSymbol array is not large
        /// enough to hold all the samples, the function returns an error and this parameter returns the minimum
        /// expected size of the output array.
        /// 
        ///</param>
        ///<returns>
        /// 
        ///niWLANA_GetCurrentIterationOFDMDemodPilotEVMPerSymbolTrace
        /// int32 __stdcall niWLANA_GetCurrentIterationOFDMDemodPilotEVMPerSymbolTrace(
        ///     niWLANAnalysisSession session,
        ///     char channelString[], 
        ///     int32 index[],
        ///     float64 pilotEVMPerSymbol[],
        ///     int32 dataArraySize,
        ///     int32 *actualArraySize);
        /// Purpose
        /// Returns the pilot error vector magnitude (EVM) per symbol number, in dB, for each iteration when the toolkit processes the acquired burst. The toolkit obtains this trace from the niWLANA_GetCurrentIterationOFDMDemodEVMPerSymbolPerSubcarrierTrace function by averaging over the pilot subcarrier for each symbol.
        /// Set the NIWLANA_OFDM_DEMOD_ENABLED and NIWLANA_OFDM_DEMOD_EVM_PER_SYMBOL_TRACE_ENABLED 
        /// attributes to NIWLANA_VAL_TRUE to get the current iteration pilot EVM per symbol trace.
        /// If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use a channelx or streamx active channel string to configure this function. If you configure this function using a channelx active channel string, this function returns the pilot EVM per symbol trace for the corresponding channel. If you configure this function using a streamx active channel string, this function returns the pilot EVM per symbol trace for the corresponding stream.
        /// 
        ///</returns>
        public int GetCurrentIterationOFDMDemodPilotEVMPerSymbolTrace(string channelString, int[] index, double[] pilotEVMperSymbol, int dataArraySize, out int actualArraySize)
        {
            int pInvokeResult = PInvoke.niWLANA_GetCurrentIterationOFDMDemodPilotEVMPerSymbolTrace(Handle, channelString, index, pilotEVMperSymbol, dataArraySize, out actualArraySize);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        /// <summary>
        /// Returns the channel frequency response magnitude, in dB, and phase for each subcarrier for each iteration when the toolkit processes the acquired burst. The portion of the signal that the toolkit uses to obtain this trace is specified by the NIWLANA_OFDM_DEMOD_CHANNEL_TRACKING_ENABLED attribute.
        /// Set the NIWLANA_OFDM_DEMOD_ENABLED and NIWLANA_OFDM_DEMOD_CHANNEL_FREQUENCY_RESPONSE_TRACE_ENABLED 
        /// attributes to NIWLANA_VAL_TRUE to get the current iteration channel frequency response.
        /// If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use a channelx or streamx active channel string to configure this function, which returns the channel frequency response for the corresponding stream of the corresponding channel. 
        /// 
        /// </summary>
        ///<param name = "session">
        ///</param>
        ///<param name = "channelString">
        /// Specifies the WLAN channel for which you want to fetch the trace.
        /// 
        ///</param>
        ///<param name = "index">
        /// Returns the indices of the subcarriers. 
        /// 
        ///</param>
        ///<param name = "magnitude">
        /// Returns the magnitude, in dB, of the channel frequency response. 
        /// 
        ///</param>
        ///<param name = "phase">
        /// Returns the phase of the channel frequency response. 
        /// 
        ///</param>
        ///<param name = "dataArraySize">
        /// Specifies the size of the data array.
        /// 
        ///</param>
        ///<param name = "actualArraySize">
        /// Returns the number of elements in the CFRMagnitude and CFRPhase arrays. If the arrays are not large
        /// enough to hold all the samples, the function returns an error and this parameter returns the minimum
        /// expected size of the output arrays. 
        /// 
        ///</param>
        ///<returns>
        /// 
        ///niWLANA_GetCurrentIterationOFDMDemodChannelFrequencyResponseTrace
        /// int32 __stdcall niWLANA_GetCurrentIterationOFDMDemodChannelFrequencyResponseTrace(
        ///     niWLANAnalysisSession session,
        ///     char channelString[], 
        ///     int32 index[],
        ///     float64 CFRMagnitude[],
        ///     float64 CFRPhase[],
        ///     int32 dataArraySize,
        ///     int32 *actualArraySize);
        /// Purpose
        /// Returns the channel frequency response magnitude, in dB, and phase for each subcarrier for each iteration when the toolkit processes the acquired burst. The portion of the signal that the toolkit uses to obtain this trace is specified by the NIWLANA_OFDM_DEMOD_CHANNEL_TRACKING_ENABLED attribute.
        /// Set the NIWLANA_OFDM_DEMOD_ENABLED and NIWLANA_OFDM_DEMOD_CHANNEL_FREQUENCY_RESPONSE_TRACE_ENABLED 
        /// attributes to NIWLANA_VAL_TRUE to get the current iteration channel frequency response.
        /// If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use a channelx or streamx active channel string to configure this function, which returns the channel frequency response for the corresponding stream of the corresponding channel. 
        /// 
        ///</returns>
        public int GetCurrentIterationOFDMDemodChannelFrequencyResponseTrace(string channelString, int[] index, double[] magnitude, double[] phase, int dataArraySize, out int actualArraySize)
        {
            int pInvokeResult = PInvoke.niWLANA_GetCurrentIterationOFDMDemodChannelFrequencyResponseTrace(Handle, channelString, index, magnitude, phase, dataArraySize, out actualArraySize);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        /// <summary>
        /// Returns the relative channel energy, in dB, for each subcarrier for each iteration when the toolkit processes the acquired burst. This trace is derived from the niWLANA_GetCurrentIterationOFDMDemodChannelFrequencyResponseTrace function, as defined in section 17.3.9.6.2 of the IEEE Standard 802.11-2007 and section 20.3.21.2 of the IEEE Standard 802.11n-2009.
        /// Set the NIWLANA_OFDM_DEMOD_ENABLED and NIWLANA_OFDM_DEMOD_SPECTRAL_FLATNESS_TRACE_ENABLED 
        /// attributes to NIWLANA_VAL_TRUE to get the current iteration spectral flatness trace.
        /// If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use a channelx/streamy active channel string to configure this function, which returns the spectral flatness trace for the corresponding stream of the corresponding channel that is derived from the niWLANA_GetCurrentIterationOFDMDemodChannelFrequencyResponseTrace function for a channelx/streamy active channel string. 
        /// 
        /// </summary>
        ///<param name = "session">
        ///</param>
        ///<param name = "channelString">
        /// Specifies the WLAN channel for which you want to fetch the trace.
        /// 
        ///</param>
        ///<param name = "index">
        /// Returns the indices of the subcarrier. 
        /// 
        ///</param>
        ///<param name = "upperMask">
        /// Returns the upper limits on the relative channel energy for each subcarrier. 
        /// 
        ///</param>
        ///<param name = "spectralFlatness">
        /// Returns the relative channel energy for each subcarrier. 
        /// 
        ///</param>
        ///<param name = "lowerMask">
        /// Returns the lower limits on the relative channel energy for each subcarrier. 
        /// 
        ///</param>
        ///<param name = "dataArraySize">
        /// Specifies the size of the data array.
        /// 
        ///</param>
        ///<param name = "actualArraySize">
        /// Returns the number of elements in the upperMask, spectralFlatness, and lowerMask arrays. If the arrays are not large enough to hold all the samples, the function returns an error and this parameter returns the minimum
        /// expected size of the output arrays. 
        /// 
        ///</param>
        ///<returns>
        /// 
        ///niWLANA_GetCurrentIterationOFDMDemodSpectralFlatnessTrace
        /// int32 __stdcall niWLANA_GetCurrentIterationOFDMDemodSpectralFlatnessTrace(
        ///     niWLANAnalysisSession session,
        ///     char channelString[],
        ///     int32 index[],
        ///     float64 upperMask[],
        ///     float64 spectralFlatness[],
        ///     float64 lowerMask[],
        ///     int32 dataArraySize,
        ///     int32 *actualArraySize);
        /// Purpose
        /// Returns the relative channel energy, in dB, for each subcarrier for each iteration when the toolkit processes the acquired burst. This trace is derived from the niWLANA_GetCurrentIterationOFDMDemodChannelFrequencyResponseTrace function, as defined in section 17.3.9.6.2 of the IEEE Standard 802.11-2007 and section 20.3.21.2 of the IEEE Standard 802.11n-2009.
        /// Set the NIWLANA_OFDM_DEMOD_ENABLED and NIWLANA_OFDM_DEMOD_SPECTRAL_FLATNESS_TRACE_ENABLED 
        /// attributes to NIWLANA_VAL_TRUE to get the current iteration spectral flatness trace.
        /// If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use a channelx/streamy active channel string to configure this function, which returns the spectral flatness trace for the corresponding stream of the corresponding channel that is derived from the niWLANA_GetCurrentIterationOFDMDemodChannelFrequencyResponseTrace function for a channelx/streamy active channel string. 
        /// 
        ///</returns>
        public int niWLANA_GetCurrentIterationOFDMDemodSpectralFlatnessTrace(string channelString, int[] index, double[] upperMask, double[] spectralFlatness, double[] lowerMask, int dataArraySize, out int actualArraySize)
        {
            int pInvokeResult = PInvoke.niWLANA_GetCurrentIterationOFDMDemodSpectralFlatnessTrace(Handle, channelString, index, upperMask, spectralFlatness, lowerMask, dataArraySize, out actualArraySize);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        /// <summary>
        /// Saves all attributes of the session to a file located at a specified path. This file can be used to save the current state of the toolkit.
        /// 
        /// </summary>
        ///<param name = "session">
        ///</param>
        ///<param name = "filePath">
        /// Specifies the complete path to the file that the toolkit uses to save the session properties.
        /// 
        ///</param>
        ///<param name = "operation">
        /// Specifies the operation to perform on the file.
        ///  NIWLANA_VAL_FILE_OPERATION_MODE_OPEN(0)
        /// Opens an existing file.
        /// NIWLANA_VAL_FILE_OPERATION_MODE_OPEN_OR_CREATE(1)
        /// Opens an existing file or creates a new file if the file does not exist.
        /// NIWLANA_VAL_FILE_OPERATION_MODE_CREATE_OR_REPLACE(2)
        /// Creates a new file or replaces a file if it exists.
        /// NIWLANA_VAL_FILE_OPERATION_MODE_CREATE(3)
        /// Creates a new file.
        /// </td>
        /// 
        ///</param>
        ///<returns>
        /// 
        ///niWLANA_SaveConfigurationToFile
        /// int32 __stdcall niWLANA_SaveConfigurationToFile(
        ///     niWLANAnalysisSession session,
        ///     char filePath[],
        ///     int32 operation);
        /// Purpose
        /// Saves all attributes of the session to a file located at a specified path. This file can be used to save the current state of the toolkit.
        /// 
        ///</returns>
        public int SaveConfigurationToFile(string filePath, int operation)
        {
            int pInvokeResult = PInvoke.niWLANA_SaveConfigurationToFile(Handle, filePath, operation);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        /// <summary>
        /// Loads the attributes of a session saved in a file and, restores the previous state of the toolkit.
        /// 
        /// </summary>
        ///<param name = "session">
        ///</param>
        ///<param name = "filePath">
        /// Specifies the complete path to the file that the toolkit uses to save the session properties.
        /// 
        ///</param>
        ///<param name = "reset">
        /// Specifies whether to reset all the attributes of the session before loading the settings from a file.
        /// 
        ///</param>
        ///<returns>
        /// 
        ///niWLANA_LoadConfigurationFromFile
        /// int32 __stdcall niWLANA_LoadConfigurationFromFile(
        ///     niWLANAnalysisSession session,
        ///     char filePath[],
        ///     int32 reset);
        /// Purpose
        /// Loads the attributes of a session saved in a file and, restores the previous state of the toolkit.
        /// 
        ///</returns>
        public int LoadConfigurationFromFile(string filePath, int reset)
        {
            int pInvokeResult = PInvoke.niWLANA_LoadConfigurationFromFile(Handle, filePath, reset);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        /// <summary>
        /// Reads a waveform from the file saved using the WLAN Analysis Soft Front Panel, located at Start&#187;All Programs&#187;National Instruments&#187;NI WLAN Analysis Toolkit.
        /// This function returns waveform data that you can use with the niWLANA_AnalyzeIQComplexF64 or niWLANA_AnalyzeMIMOIQComplexF64 functions. If you use the technical data management streaming (TDMS) file format, the file may have more than one waveform depending on the Number of Averages used for measurements.
        /// Use the following string format for the waveformName parameter when reading a specific acquisition waveform:
        /// 80211a/b/g/j/p: "IQwfmy"
        /// 80211n: "channelx::IQwfmy"
        /// <p class="Body">where x represents an identified number for the channel index and y represents an identified number for the waveform index. For example, IQwfm0 or channel1::IQwfm2. 
        /// 
        /// </summary>
        ///<param name = "filePath">
        /// Specifies the complete path to the file from which the toolkit reads the waveform.
        /// 
        ///</param>
        ///<param name = "waveformName">
        /// Specifies the name of the waveform to read from the file.
        /// 
        ///</param>
        ///<param name = "offset">
        /// Specifies the number of samples into the waveform at which the function begins reading the I/Q data. The default value is 0. If you set the count parameter to 1,000 and the offset parameter to 2, the function returns 1,000 samples, starting from index 2 and ending at index 1,002.
        /// 
        ///</param>
        ///<param name = "count">
        /// Specifies the maximum number of samples of the I/Q complex waveform to read from the file. The default value is &#8211;1, which returns all samples. If you set the count parameter to 1,000 and the offset parameter to 2, the function returns 1,000 samples, starting from index 2 and ending at index 1,002.
        /// 
        ///</param>
        ///<param name = "t0">
        /// Returns the start parameter.
        /// 
        ///</param>
        ///<param name = "dt">
        /// Returns the delta parameter.
        /// 
        ///</param>
        ///<param name = "waveform">
        /// Returns the baseband time-domain waveform.
        /// 
        ///</param>
        ///<param name = "waveformSize">
        /// Specifies the size of the waveform to read from the file.
        /// 
        ///</param>
        ///<param name = "actualSize">
        /// Returns the size of the data array. You can pass NULL to the waveform parameter to obtain the size of the waveform.
        /// 
        ///</param>
        ///<param name = "eOF">
        /// Indicates whether the end of the file has been reached.
        /// 
        ///</param>
        ///<returns>
        /// 
        ///niWLANA_ReadWaveformFromFile
        /// int32 __stdcall niWLANA_ReadWaveformFromFile(
        ///     char filePath[], 
        ///     char waveformName[], 
        ///     int64 offset, 
        ///     int64 count, 
        ///     float64 *t0, 
        ///     float64 *dt, 
        ///     NIComplexNumber waveform[], 
        ///     int32 waveformSize,
        ///     int32 *actualArraySize,
        ///     int32 *eof);
        /// Purpose
        /// Reads a waveform from the file saved using the WLAN Analysis Soft Front Panel, located at Start&#187;All Programs&#187;National Instruments&#187;NI WLAN Analysis Toolkit.
        /// This function returns waveform data that you can use with the niWLANA_AnalyzeIQComplexF64 or niWLANA_AnalyzeMIMOIQComplexF64 functions. If you use the technical data management streaming (TDMS) file format, the file may have more than one waveform depending on the Number of Averages used for measurements.
        /// Use the following string format for the waveformName parameter when reading a specific acquisition waveform:
        /// 80211a/b/g/j/p: "IQwfmy"
        /// 80211n: "channelx::IQwfmy"
        /// <p class="Body">where x represents an identified number for the channel index and y represents an identified number for the waveform index. For example, IQwfm0 or channel1::IQwfm2. 
        /// 
        ///</returns>
        public int ReadWaveformFromFile(string filePath, string waveformName, int offset, int count, out double t0, out double dt, out niComplexNumber waveform, int waveformSize, out int actualSize, out int eOF)
        {
            int pInvokeResult = PInvoke.niWLANA_ReadWaveformFromFile(filePath, waveformName, offset, count, out t0, out dt, out waveform, waveformSize, out actualSize, out eOF);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        /// <summary>
        /// Calculates the frequency according to the numbering scheme by converting a set of input parameters into the carrier frequency as specified in sections 18.4.6 and 17.3.8.3 of IEEE Standard 802.11-2007, and sections 20.3.15.1 and 20.3.15.2  of IEEE Standard 802.11-2009.
        /// 
        /// </summary>
        ///<param name = "frequencyBand">
        /// Specifies whether to use the 2.4 GHz or the 5 GHz frequency band.
        /// NIWLANA_VAL_FREQUENCY_BAND_2p4GHZ (0)
        /// Specifies a frequency band of 2.4 GHz.
        /// NIWLANA_VAL_FREQUENCY_BAND_5GHZ (1)
        /// Specifies a frequency band of 5 GHz.
        /// 
        ///</param>
        ///<param name = "channelBandwidth">
        /// Specifies the channel bandwidth. You can choose a 5 MHz, 10 MHz, 20 MHz, or 40 MHz channel.
        /// 
        ///</param>
        ///<param name = "channelNumber">
        /// Specifies the offset of the center frequency, in increments of 5 MHz, above the starting frequency of the channel. 
        /// When the channelBandwidth parameter is set to 40 MHz, the channelNumber parameter is the primary channel number and the corresponding channel center frequency is the primary channel center frequency.
        /// The toolkit calculates the center frequency using the following formula:
        /// channel center frequency (Hz) = channel starting frequency (Hz) + (channel number * 5 MHz)
        /// 
        ///</param>
        ///<param name = "secondaryFactor">
        /// Specifies whether the secondary channel is above or below the primary channel when the
        /// channelBandwidth parameter is set to 40 MHz.
        /// The toolkit creats a 40 MHz channel by combining the primary channel and the secondary channel, each with a 20 MHz bandwidth.
        /// The secondary channel number is given by the following formula:
        /// secondary channel number = primary channel number + (4&#160;*&#160;secondaryFactor)
        /// The secondary channel center frequency is given by the following formula:
        /// secondary channel center frequency (Hz) = channel starting frequency (Hz) + (secondary channel number * 5&#160;MHz)
        /// Valid values are &#8211;1 and +1.
        /// 
        ///</param>
        ///<param name = "channelStartingFactor">
        /// Specifies the value used to define the baseline frequency.
        /// The toolkit calculates the channel starting frequency using the following equation:
        /// channel starting frequency (Hz) = (channelStartingFactor * 500 kHz)
        /// 
        ///</param>
        ///<param name = "carrierFrequency">
        /// Returns the carrier frequency, in hertz (Hz).
        /// 
        ///</param>
        ///<returns>
        /// 
        ///niWLANA_ChannelNumberToCarrierFrequency
        /// int32 __stdcall niWLANA_ChannelNumberToCarrierFrequency(
        ///     uInt16 frequencyBand,
        ///     float64 channelBandwidth,
        ///     int32 channelNumber,
        ///     int32 secondaryFactor, 
        ///     float64 channelStartingFactor,
        ///     float64 *carrierFrequency);
        /// Purpose
        /// Calculates the frequency according to the numbering scheme by converting a set of input parameters into the carrier frequency as specified in sections 18.4.6 and 17.3.8.3 of IEEE Standard 802.11-2007, and sections 20.3.15.1 and 20.3.15.2  of IEEE Standard 802.11-2009.
        /// 
        ///</returns>
        public int ChannelNumberToCarrierFrequency(int frequencyBand, double channelBandwidth, int channelNumber, int secondaryFactor, double channelStartingFactor, out double carrierFrequency)
        {
            int pInvokeResult = PInvoke.niWLANA_ChannelNumberToCarrierFrequency(frequencyBand, channelBandwidth, channelNumber, secondaryFactor, channelStartingFactor, out carrierFrequency);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }
        /// <summary>
        /// Returns the estimated carrier frequency offset, in hertz (Hz), of the transmitting device under test (DUT). For example, if during acquisition you set the NI-RFSA IQ Carrier Frequency attribute to 2.412 GHz and the toolkit measures the carrier frequency of the DUT to be 2.413 GHz, the carrier frequency offset is 1 MHz. This measurement follows section 17.3.9.4 of IEEE Standard 802.11a-1999 and section 20.3.21.4 of IEEE Standard 802.11n-2009.
        /// 
        /// </summary>
        ///<param name = "session">
        ///</param>
        ///<param name = "channelString">
        /// Set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "carrierFrequencyOffset">
        /// Returns the estimated carrier frequency offset, in Hz, of the transmitting DUT.
        /// 
        ///</param>
        ///<returns>
        /// 
        ///niWLANA_GetDSSSDemodCarrierFrequencyOffset
        /// int32 __stdcall niWLANA_GetDSSSDemodCarrierFrequencyOffset(niWLANAnalysisSession session,
        ///     char channelString[],
        ///     float64 *carrierFrequencyOffset);
        /// Purpose
        /// Returns the estimated carrier frequency offset, in hertz (Hz), of the transmitting device under test (DUT). For example, if during acquisition you set the NI-RFSA IQ Carrier Frequency attribute to 2.412 GHz and the toolkit measures the carrier frequency of the DUT to be 2.413 GHz, the carrier frequency offset is 1 MHz. This measurement follows section 17.3.9.4 of IEEE Standard 802.11a-1999 and section 20.3.21.4 of IEEE Standard 802.11n-2009.
        /// 
        ///</returns>
        public int GetDSSSDemodCarrierFrequencyOffset(string channelString, out double carrierFrequencyOffset)
        {
            int pInvokeResult = PInvoke.niWLANA_GetDSSSDemodCarrierFrequencyOffset(Handle, channelString, out carrierFrequencyOffset);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }
        /// <summary>
        /// Configures NI RF vector signal analyzers for either I/Q acquisition or spectral acquisition depending on the type of WLAN measurement you specify.
        /// If the NIWLANA_RECOMMENDED_ACQUISITION_TYPE attribute returns NIWLANA_VAL_IQ, this function configures NI-RFSA to acquire I/Q samples and performs the following actions:  
        /// Sets the NIRFSA_ATTR_ACQUISITION_TYPE attribute to NIRFSA_VAL_IQ.
        /// Sets the NIRFSA_ATTR_IQ_RATE attribute to the value of the NIWLANA_RECOMMENDED_IQ_SAMPLING_RATE attribute value.
        /// Sets the NIRFSA_ATTR_NUMBER_OF_SAMPLES_IS_FINITE attribute to VI_TRUE.
        /// Retrieves the coerced NIRFSA_ATTR_IQ_RATE attribute, multiplies this value by the value of the NIWLANA_RECOMMENDED_ACQUISITION_LENGTH attribute, and sets the NIRFSA_ATTR_NUMBER_OF_SAMPLES attribute to the result.
        /// Sets the NIRFSA_ATTR_NUMBER_OF_RECORDS_IS_FINITE attribute to VI_TRUE.
        /// Sets the NIRFSA_ATTR_NUMBER_OF_RECORDS attribute to the value of the NIWLANA_RECOMMENDED_NUMBER_OF_RECORDS attribute value.
        /// Retrieves the coerced NIRFSA_ATTR_IQ_RATE attribute, multiplies this value by the value of the NIWLANA_RECOMMENDED_IQ_PRE_TRIGGER_DELAY attribute, and sets the NIRFSA_ATTR_REF_TRIGGER_PRETRIGGER_SAMPLES attribute to the result.
        /// If you set the NIRFSA_ATTR_REF_TRIGGER_TYPE attribute to NIRFSA_VAL_IQ_POWER_EDGE, this function sets the NIRFSA_ATTR_REF_TRIGGER_MINIMUM_QUIET_TIME attribute to the value of the NIWLANA_RECOMMENDED_MINIMUM_QUIET_TIME attribute. If you set the NIRFSA_ATTR_REF_TRIGGER_TYPE attribute to any other value, this function sets the NIRFSA_ATTR_REF_TRIGGER_MINIMUM_QUIET_TIME attribute to 0.
        /// If the NIWLANA_RECOMMENDED_ACQUISITION_TYPE attribute returns NIWLANA_VAL_SPECTRUM and you set the NIWLANA_GATED_SPECTRUM_ENABLED attribute to NIWLANA_VAL_TRUE, this function configures NI-RFSA to acquire I/Q samples and performs the following actions: 
        /// Sets the NIRFSA_ATTR_ACQUISITION_TYPE attribute to NIRFSA_VAL_IQ.
        /// If you set the NIRFSA_ATTR_REF_TRIGGER_TYPE attribute to NIRFSA_VAL_IQ_POWER_EDGE, this function sets the NIRFSA_ATTR_REF_TRIGGER_MINIMUM_QUIET_TIME attribute to the value of the NIWLANA_RECOMMENDED_MINIMUM_QUIET_TIME attribute. If you set the NIRFSA_ATTR_REF_TRIGGER_TYPE attribute to any other value, this function sets the NIRFSA_ATTR_REF_TRIGGER_MINIMUM_QUIET_TIME attribute to 0.
        /// Sets the NIRFSA_ATTR_NUMBER_OF_RECORDS_IS_FINITE attribute to VI_TRUE.
        /// Sets the NIRFSA_ATTR_NUMBER_OF_RECORDS attribute to the value of the NIWLANA_RECOMMENDED_NUMBER_OF_RECORDS attribute.
        /// If the NIWLANA_RECOMMENDED_ACQUISITION_TYPE attribute returns NIWLANA_VAL_SPECTRUM and you set the NIWLANA_GATED_SPECTRUM_ENABLED attribute to NIWLANA_VAL_FALSE, this function configures NI-RFSA to compute a power spectrum and performs the following actions: 
        /// Sets the NIRFSA_ATTR_ACQUISITION_TYPE attribute to NIRFSA_VAL_SPECTRUM.
        /// Sets the NIRFSA_ATTR_SPECTRUM_SPAN attribute to the value of the NIWLANA_RECOMMENDED_SPECTRUM_SPAN attribute.
        /// Sets the NIRFSA_ATTR_POWER_SPECTRUM_UNITS attribute to NIRFSA_VAL_VOLTS_SQUARED.
        /// Sets the NIRFSA_ATTR_RESOLUTION_BANDWIDTH_TYPE attribute to NIRFSA_VAL_RBW_3DB, NIRFSA_VAL_RBW_6DB, NIRFSA_VAL_RBW_BIN_WIDTH, or NIRFSA_VAL_RBW_ENBW depending on the NIWLANA_RECOMMENDED_SPECTRUM_RBW_DEFINITION attribute.
        /// Sets the NIRFSA_ATTR_RESOLUTION_BANDWIDTH attribute to the value of the NIWLANA_RECOMMENDED_SPECTRUM_RBW attribute.
        /// Sets the NIRFSA_ATTR_SPECTRUM_AVERAGING_MODE attribute to NIRFSA_VAL_RMS_AVERAGING.
        /// Sets the NIRFSA_ATTR_SPECTRUM_NUMBER_OF_AVERAGES attribute to the value of the NIWLANA_RECOMMENDED_NUMBER_OF_RECORDS attribute.
        /// Sets the NIRFSA_ATTR_FFT_WINDOW_TYPE attribute to NIRFSA_VAL_UNIFORM, NIRFSA_VAL_HANNING, NIRFSA_VAL_BLACKMAN_HARRIS, NIRFSA_VAL_EXACT_BLACKMAN, NIRFSA_VAL_BLACKMAN, NIRFSA_VAL_FLAT_TOP, NIRFSA_VAL_4_TERM_BLACKMAN_HARRIS, NIRFSA_VAL_7_TERM_BLACKMAN_HARRIS, or NIRFSA_VAL_LOW_SIDE_LOBE depending upon the NIWLANA_RECOMMENDED_SPECTRUM_FFT_WINDOW_TYPE attribute.
        /// Retrieves the spectrumInfo cluster using the niRFSA_GetSpectralInfoForSMT function and sets the NIWLANA_FFT_WINDOW_SIZE attribute and the niRFSA_GetSpectralInfoForSMT function and sets the NIWLANA_FFT_SIZE attribute, respectively, to the window size and FFT size elements of the spectralInfo cluster.
        /// 
        /// </summary>
        ///<param name = "session">
        ///</param>
        ///<param name = "rFSASession">
        /// Identifies the instrument session. This parameter is obtained from the niRFSA_init function or niRFSA_InitWithOptions function. 
        /// 
        ///</param>
        ///<param name = "hardwareChannelString">
        /// Specifies the RFSA device channel. Set this parameter to "" (empty string) or NULL.
        /// 
        ///</param>
        ///<param name = "samplesPerRecord">
        /// Returns the number of samples per record configured for the NI-RFSA session.
        /// 
        ///</param>
        ///<returns>
        /// 
        ///niWLANA_RFSAConfigureHardware
        /// int32 __stdcall niWLANA_RFSAConfigureHardware(
        ///     niWLANAnalysisSession session,
        ///     ViSession rfsaSession,
        ///     char hwChannelString[],
        ///     int64 *samplesPerRecord);
        /// Purpose
        /// Configures NI RF vector signal analyzers for either I/Q acquisition or spectral acquisition depending on the type of WLAN measurement you specify.
        /// If the NIWLANA_RECOMMENDED_ACQUISITION_TYPE attribute returns NIWLANA_VAL_IQ, this function configures NI-RFSA to acquire I/Q samples and performs the following actions:  
        /// Sets the NIRFSA_ATTR_ACQUISITION_TYPE attribute to NIRFSA_VAL_IQ.
        /// Sets the NIRFSA_ATTR_IQ_RATE attribute to the value of the NIWLANA_RECOMMENDED_IQ_SAMPLING_RATE attribute value.
        /// Sets the NIRFSA_ATTR_NUMBER_OF_SAMPLES_IS_FINITE attribute to VI_TRUE.
        /// Retrieves the coerced NIRFSA_ATTR_IQ_RATE attribute, multiplies this value by the value of the NIWLANA_RECOMMENDED_ACQUISITION_LENGTH attribute, and sets the NIRFSA_ATTR_NUMBER_OF_SAMPLES attribute to the result.
        /// Sets the NIRFSA_ATTR_NUMBER_OF_RECORDS_IS_FINITE attribute to VI_TRUE.
        /// Sets the NIRFSA_ATTR_NUMBER_OF_RECORDS attribute to the value of the NIWLANA_RECOMMENDED_NUMBER_OF_RECORDS attribute value.
        /// Retrieves the coerced NIRFSA_ATTR_IQ_RATE attribute, multiplies this value by the value of the NIWLANA_RECOMMENDED_IQ_PRE_TRIGGER_DELAY attribute, and sets the NIRFSA_ATTR_REF_TRIGGER_PRETRIGGER_SAMPLES attribute to the result.
        /// If you set the NIRFSA_ATTR_REF_TRIGGER_TYPE attribute to NIRFSA_VAL_IQ_POWER_EDGE, this function sets the NIRFSA_ATTR_REF_TRIGGER_MINIMUM_QUIET_TIME attribute to the value of the NIWLANA_RECOMMENDED_MINIMUM_QUIET_TIME attribute. If you set the NIRFSA_ATTR_REF_TRIGGER_TYPE attribute to any other value, this function sets the NIRFSA_ATTR_REF_TRIGGER_MINIMUM_QUIET_TIME attribute to 0.
        /// If the NIWLANA_RECOMMENDED_ACQUISITION_TYPE attribute returns NIWLANA_VAL_SPECTRUM and you set the NIWLANA_GATED_SPECTRUM_ENABLED attribute to NIWLANA_VAL_TRUE, this function configures NI-RFSA to acquire I/Q samples and performs the following actions: 
        /// Sets the NIRFSA_ATTR_ACQUISITION_TYPE attribute to NIRFSA_VAL_IQ.
        /// If you set the NIRFSA_ATTR_REF_TRIGGER_TYPE attribute to NIRFSA_VAL_IQ_POWER_EDGE, this function sets the NIRFSA_ATTR_REF_TRIGGER_MINIMUM_QUIET_TIME attribute to the value of the NIWLANA_RECOMMENDED_MINIMUM_QUIET_TIME attribute. If you set the NIRFSA_ATTR_REF_TRIGGER_TYPE attribute to any other value, this function sets the NIRFSA_ATTR_REF_TRIGGER_MINIMUM_QUIET_TIME attribute to 0.
        /// Sets the NIRFSA_ATTR_NUMBER_OF_RECORDS_IS_FINITE attribute to VI_TRUE.
        /// Sets the NIRFSA_ATTR_NUMBER_OF_RECORDS attribute to the value of the NIWLANA_RECOMMENDED_NUMBER_OF_RECORDS attribute.
        /// If the NIWLANA_RECOMMENDED_ACQUISITION_TYPE attribute returns NIWLANA_VAL_SPECTRUM and you set the NIWLANA_GATED_SPECTRUM_ENABLED attribute to NIWLANA_VAL_FALSE, this function configures NI-RFSA to compute a power spectrum and performs the following actions: 
        /// Sets the NIRFSA_ATTR_ACQUISITION_TYPE attribute to NIRFSA_VAL_SPECTRUM.
        /// Sets the NIRFSA_ATTR_SPECTRUM_SPAN attribute to the value of the NIWLANA_RECOMMENDED_SPECTRUM_SPAN attribute.
        /// Sets the NIRFSA_ATTR_POWER_SPECTRUM_UNITS attribute to NIRFSA_VAL_VOLTS_SQUARED.
        /// Sets the NIRFSA_ATTR_RESOLUTION_BANDWIDTH_TYPE attribute to NIRFSA_VAL_RBW_3DB, NIRFSA_VAL_RBW_6DB, NIRFSA_VAL_RBW_BIN_WIDTH, or NIRFSA_VAL_RBW_ENBW depending on the NIWLANA_RECOMMENDED_SPECTRUM_RBW_DEFINITION attribute.
        /// Sets the NIRFSA_ATTR_RESOLUTION_BANDWIDTH attribute to the value of the NIWLANA_RECOMMENDED_SPECTRUM_RBW attribute.
        /// Sets the NIRFSA_ATTR_SPECTRUM_AVERAGING_MODE attribute to NIRFSA_VAL_RMS_AVERAGING.
        /// Sets the NIRFSA_ATTR_SPECTRUM_NUMBER_OF_AVERAGES attribute to the value of the NIWLANA_RECOMMENDED_NUMBER_OF_RECORDS attribute.
        /// Sets the NIRFSA_ATTR_FFT_WINDOW_TYPE attribute to NIRFSA_VAL_UNIFORM, NIRFSA_VAL_HANNING, NIRFSA_VAL_BLACKMAN_HARRIS, NIRFSA_VAL_EXACT_BLACKMAN, NIRFSA_VAL_BLACKMAN, NIRFSA_VAL_FLAT_TOP, NIRFSA_VAL_4_TERM_BLACKMAN_HARRIS, NIRFSA_VAL_7_TERM_BLACKMAN_HARRIS, or NIRFSA_VAL_LOW_SIDE_LOBE depending upon the NIWLANA_RECOMMENDED_SPECTRUM_FFT_WINDOW_TYPE attribute.
        /// Retrieves the spectrumInfo cluster using the niRFSA_GetSpectralInfoForSMT function and sets the NIWLANA_FFT_WINDOW_SIZE attribute and the niRFSA_GetSpectralInfoForSMT function and sets the NIWLANA_FFT_SIZE attribute, respectively, to the window size and FFT size elements of the spectralInfo cluster.
        /// 
        ///</returns>
        public int RFSAConfigureHardware(HandleRef rFSASession, string hardwareChannelString, out long samplesPerRecord)
        {
            int pInvokeResult = PInvoke.niWLANA_RFSAConfigureHardware(Handle, rFSASession, hardwareChannelString, out samplesPerRecord);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int GetSpectralMaskMargin(string channelString, out double value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetSpectralMaskMargin(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        #region Version 4.0

        public int GetSpectralMaskMarginFrequencyVector(string channelString, double[] dataArray, int dataArraySize, out int actualNumDataArrayElements)
        {
            int pInvokeResult = PInvoke.niWLANA_GetSpectralMaskMarginFrequencyVector(Handle, channelString, dataArray, dataArraySize, out actualNumDataArrayElements);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int GetSpectralMaskMarginPowerSpectralDensityVector(string channelString, double[] dataArray, int dataArraySize, out int actualNumDataArrayElements)
        {
            int pInvokeResult = PInvoke.niWLANA_GetSpectralMaskMarginPowerSpectralDensityVector(Handle, channelString, dataArray, dataArraySize, out actualNumDataArrayElements);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int GetSpectralMaskFrequencyOffsetsUsed(string channelString, double[] dataArray, int dataArraySize, out int actualNumDataArrayElements)
        {
            int pInvokeResult = PInvoke.niWLANA_GetSpectralMaskFrequencyOffsetsUsed(Handle, channelString, dataArray, dataArraySize, out actualNumDataArrayElements);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int GetSpectralMaskPowerOffsetsUsed(string channelString, double[] dataArray, int dataArraySize, out int actualNumDataArrayElements)
        {
            int pInvokeResult = PInvoke.niWLANA_GetSpectralMaskPowerOffsetsUsed(Handle, channelString, dataArray, dataArraySize, out actualNumDataArrayElements);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }


        public int GetResultSpectralMaskViolation(string channelString, out double value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetResultSpectralMaskViolation(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }


        /********************------------------Version 4.0----------------------------********************/
        public int GetSpectralMaskMarginVector(string channelString, double [] dataArray, int dataArraySize, out int actualNumDataArrayElements)
        {
             int pInvokeResult = PInvoke.niWLANA_GetSpectralMaskMarginVector(Handle, channelString, dataArray, dataArraySize, out actualNumDataArrayElements);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }
       
        public int GetCurrentIterationOFDMDemodPreambleFrequencyErrorTrace(string channelString, double [] time, double [] preambleFrequencyError, int dataArraySize, out int actualArraySize)
        {
            int pInvokeResult = PInvoke.niWLANA_GetCurrentIterationOFDMDemodPreambleFrequencyErrorTrace(Handle, channelString, time, preambleFrequencyError, dataArraySize, out actualArraySize);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int GetCurrentIterationOFDMDemodCommonPilotErrorTrace(string channelString, 	int [] index, double [] CPEMagnitude, double [] CPEPhase, int dataArraySize, out int actualArraySize)
        {
            int pInvokeResult = PInvoke.niWLANA_GetCurrentIterationOFDMDemodCommonPilotErrorTrace(Handle, channelString, index, CPEMagnitude, CPEPhase, dataArraySize, out actualArraySize);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int GetCurrentIterationOFDMDemodPhaseNoisePSDTrace(string channelString, out double f0, out double df, double [] phaseNoisePSD, int dataArraySize, out int actualArraySize)
        {
            int pInvokeResult = PInvoke.niWLANA_GetCurrentIterationOFDMDemodPhaseNoisePSDTrace(Handle, channelString, out f0, out df, phaseNoisePSD, dataArraySize, out actualArraySize);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int Get80211AcAmpduEnabled(string channelString, out int value)
        {
            return GetInt(niWLANAProperties.Property80211acAmpduEnabled, channelString, out value);
        }

        public int Set80211AcAmpduEnabled(string channelString, int value)
        {
            return SetInt(niWLANAProperties.Property80211acAmpduEnabled, channelString, value);
        }



        /*- OFDM Demod:Impairments Estimation Enabled -------------------------*/

         /********************************************/

        public int GetOFDMDemodCarrierFrequencyOffsetEstimationEnabled(string channelString, out int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetOFDMDemodCarrierFrequencyOffsetEstimationEnabled(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int SetOFDMDemodCarrierFrequencyOffsetEstimationEnabled(string channelString, int value)
        {
            int pInvokeResult = PInvoke.niWLANA_SetOFDMDemodCarrierFrequencyOffsetEstimationEnabled(Handle, channelString, value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

         /********************************************/

        public int GetOFDMDemodSampleClockOffsetEstimationEnabled(string channelString, out int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetOFDMDemodSampleClockOffsetEstimationEnabled(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int SetOFDMDemodSampleClockOffsetEstimationEnabled(string channelString, int value)
        {
            int pInvokeResult = PInvoke.niWLANA_SetOFDMDemodSampleClockOffsetEstimationEnabled(Handle, channelString, value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

         /********************************************/

        public int GetOFDMDemodIQGainImbalanceEstimationEnabled(string channelString, out int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetOFDMDemodIQGainImbalanceEstimationEnabled(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int SetOFDMDemodIQGainImbalanceEstimationEnabled(string channelString, int value)
        {
            int pInvokeResult = PInvoke.niWLANA_SetOFDMDemodIQGainImbalanceEstimationEnabled(Handle, channelString, value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

         /********************************************/

        public int GetOFDMDemodQuadratureSkewEstimationEnabled(string channelString, out int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetOFDMDemodQuadratureSkewEstimationEnabled(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int SetOFDMDemodQuadratureSkewEstimationEnabled(string channelString, int value)
        {
            int pInvokeResult = PInvoke.niWLANA_SetOFDMDemodQuadratureSkewEstimationEnabled(Handle, channelString, value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

         /********************************************/

        public int GetOFDMDemodTimingSkewEstimationEnabled(string channelString, out int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetOFDMDemodTimingSkewEstimationEnabled(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int SetOFDMDemodTimingSkewEstimationEnabled(string channelString, int value)
        {
            int pInvokeResult = PInvoke.niWLANA_SetOFDMDemodTimingSkewEstimationEnabled(Handle, channelString, value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

 /********************************************/

        public int GetOFDMDemodCarrierFrequencyLeakageEstimationEnabled(string channelString, out int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetOFDMDemodCarrierFrequencyLeakageEstimationEnabled(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int SetOFDMDemodCarrierFrequencyLeakageEstimationEnabled(string channelString, int value)
        {
            int pInvokeResult = PInvoke.niWLANA_SetOFDMDemodCarrierFrequencyLeakageEstimationEnabled(Handle, channelString, value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        /********************************************/

        public int GetOFDMDemodCommonPilotErrorEstimationEnabled(string channelString, out int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetOFDMDemodCommonPilotErrorEstimationEnabled(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int SetOFDMDemodCommonPilotErrorEstimationEnabled(string channelString, int value)
        {
            int pInvokeResult = PInvoke.niWLANA_SetOFDMDemodCommonPilotErrorEstimationEnabled(Handle, channelString, value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

/* - OFDM Demod:Impairments Compensation Enabled------------------------------------------- */	

        /********************************************/

        public int GetOFDMDemodIQGainImbalanceCompensationEnabled(string channelString, out int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetOFDMDemodIQGainImbalanceCompensationEnabled(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int SetOFDMDemodIQGainImbalanceCompensationEnabled(string channelString, int value)
        {
            int pInvokeResult = PInvoke.niWLANA_SetOFDMDemodIQGainImbalanceCompensationEnabled(Handle, channelString, value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

         /********************************************/

        public int GetOFDMDemodQuadratureSkewCompensationEnabled(string channelString, out int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetOFDMDemodQuadratureSkewCompensationEnabled(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int SetOFDMDemodQuadratureSkewCompensationEnabled(string channelString, int value)
        {
            int pInvokeResult = PInvoke.niWLANA_SetOFDMDemodQuadratureSkewCompensationEnabled(Handle, channelString, value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

         /********************************************/

        public int GetOFDMDemodTimingSkewCompensationEnabled(string channelString, out int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetOFDMDemodTimingSkewCompensationEnabled(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int SetOFDMDemodTimingSkewCompensationEnabled(string channelString, int value)
        {
            int pInvokeResult = PInvoke.niWLANA_SetOFDMDemodTimingSkewCompensationEnabled(Handle, channelString, value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }			

        /********************************************/

        public int GetOfdmDemodCombinedSignalDemodulationEnabled(string channelString, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodCombinedSignalDemodulationEnabled, out value);
        }

        public int SetOfdmDemodCombinedSignalDemodulationEnabled(string channelString, int value)
        {
            return SetInt(niWLANAProperties.OfdmDemodCombinedSignalDemodulationEnabled, value);
        }

        /********************************************/

        public int GetOFDMDemodIQMismatchSignalModel(string channelString, out int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetOFDMDemodIQMismatchSignalModel(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int SetOFDMDemodIQMismatchSignalModel(string channelString, int value)
        {
            int pInvokeResult = PInvoke.niWLANA_SetOFDMDemodIQMismatchSignalModel(Handle, channelString, value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }			

        /********************************************/

        public int GetOfdmDemodBurstStartDetectionEnabled(string channelString, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodBurstStartDetectionEnabled, out value);
        }

        public int SetOfdmDemodBurstStartDetectionEnabled(string channelString, int value)
        {
            return SetInt(niWLANAProperties.OfdmDemodBurstStartDetectionEnabled, value);
        }

        /********************************************/

        public int GetOFDMDemodPreambleFrequencyErrorTraceEnabled(string channelString, out int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetOFDMDemodPreambleFrequencyErrorTraceEnabled(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int SetOFDMDemodPreambleFrequencyErrorTraceEnabled(string channelString, int value)
        {
            int pInvokeResult = PInvoke.niWLANA_SetOFDMDemodPreambleFrequencyErrorTraceEnabled(Handle, channelString, value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }		

        /********************************************/

        public int GetOFDMDemodCommonPilotErrorTraceEnabled(string channelString, out int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetOFDMDemodCommonPilotErrorTraceEnabled(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int SetOFDMDemodCommonPilotErrorTraceEnabled(string channelString, int value)
        {
            int pInvokeResult = PInvoke.niWLANA_SetOFDMDemodCommonPilotErrorTraceEnabled(Handle, channelString, value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }	

        /********************************************/

        public int GetOFDMDemodPhaseNoisePSDTraceEnabled(string channelString, out int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetOFDMDemodPhaseNoisePSDTraceEnabled(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int SetOFDMDemodPhaseNoisePSDTraceEnabled(string channelString, int value)
        {
            int pInvokeResult = PInvoke.niWLANA_SetOFDMDemodPhaseNoisePSDTraceEnabled(Handle, channelString, value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }


        /********************************************/

        public int GetGatedSpectrumAveragingType(string channelString, out int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetGatedSpectrumAveragingType(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int SetGatedSpectrumAveragingType(string channelString, int value)
        {
            int pInvokeResult = PInvoke.niWLANA_SetGatedSpectrumAveragingType(Handle, channelString, value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }


        /********************************************/

        public int GetNumberOfSegments(string channelString, out int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetNumberOfSegments(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int SetNumberOfSegments(string channelString, int value)
        {
            int pInvokeResult = PInvoke.niWLANA_SetNumberOfSegments(Handle, channelString, value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        /********************************************/
        public int GetOFDMLSIGPayloadLength(string channelString, out int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetOFDMLSIGPayloadLength(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int SetOFDMLSIGPayloadLength(string channelString, int value)
        {
            int pInvokeResult = PInvoke.niWLANA_SetOFDMLSIGPayloadLength(Handle, channelString, value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }


        /********************************************/
        public int GetSTBCAllStreamsEnabled(string channelString, out int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetSTBCAllStreamsEnabled(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int SetSTBCAllStreamsEnabled(string channelString, int value)
        {
            int pInvokeResult = PInvoke.niWLANA_SetSTBCAllStreamsEnabled(Handle, channelString, value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        /********************************************/
        public int GetNumberOfSpaceTimeStreams(string channelString, out int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetNumberOfSpaceTimeStreams(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int SetNumberOfSpaceTimeStreams(string channelString, int value)
        {
            int pInvokeResult = PInvoke.niWLANA_SetNumberOfSpaceTimeStreams(Handle, channelString, value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }
        /********************************************/
        public int GetShortGuardIntervalB1Bit(string channelString, out int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetShortGuardIntervalB1Bit(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int SetShortGuardIntervalB1Bit(string channelString, int value)
        {
            int pInvokeResult = PInvoke.niWLANA_SetShortGuardIntervalB1Bit(Handle, channelString, value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }
        /********************************************/
        public int GetAggregationBit(string channelString, out int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetAggregationBit(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int SetAggregationBit(string channelString, int value)
        {
            int pInvokeResult = PInvoke.niWLANA_SetAggregationBit(Handle, channelString, value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }
        /********************************************/
        public int GetSwapIAndQEnabled(string channelString, out int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetSwapIAndQEnabled(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int SetSwapIAndQEnabled(string channelString, int value)
        {
            int pInvokeResult = PInvoke.niWLANA_SetSwapIAndQEnabled(Handle, channelString, value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }	
        /********************************************/

        public int GetSampleClockRateFactor(string channelString, out double value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetSampleClockRateFactor(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }

        public int SetSampleClockRateFactor(string channelString, double value)
        {
            int pInvokeResult = PInvoke.niWLANA_SetSampleClockRateFactor(Handle, channelString, value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }	
        /********************************************/
	
        public int GetOFDMDemodLSIGPayloadLength(string channelString, out  int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetOFDMDemodLSIGPayloadLength(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }
        /********************************************/

        public int GetOFDMDemodSTBCAllStreamsEnabled(string channelString, out  int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetOFDMDemodSTBCAllStreamsEnabled(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }
        /********************************************/


        public int GetOfdmDemodNumberOfSpaceTimeStreams(string channelString, out  int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodNumberOfSpaceTimeStreams, channelString, out value);
        }
        /********************************************/

        public int GetOFDMDemodVHTSIGACRCPassed(string channelString, out  int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetOFDMDemodVHTSIGACRCPassed(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }
        /********************************************/

        public int GetOFDMDemodVHTSIGBCRCPassed(string channelString, out  int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetOFDMDemodVHTSIGBCRCPassed(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }
        /********************************************/

        public int GetOFDMDemodTimingSkew(string channelString, out double value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetOFDMDemodTimingSkew(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }
        /********************************************/
 
        public int GetOfdmDemodRmsPhaseError(string channelString, out double value)
        {
            return GetDouble(niWLANAProperties.OfdmDemodRmsPhaseError, channelString, out value);
        }
        /********************************************/

        public int GetOFDMDemodCommonPilotErrorRMS(string channelString, out double value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetOFDMDemodCommonPilotErrorRMS(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }
        /********************************************/

        public int GetOFDMDemodSpectralFlatnessMarginSubcarrierIndex(string channelString, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodSpectralFlatnessMarginSubcarrierIndex, channelString, out value);
        }
        /********************************************/

        public int GetOFDMDemodNumberOfOFDMSymbols(string channelString, out int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetOFDMDemodNumberOfOFDMSymbols(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }
        /********************************************/

        public int GetOFDMDemodAggregation(string channelString, out  int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetOFDMDemodAggregation(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }
        /********************************************/

        public int GetOFDMDemodShortGuardIntervalB1Bit(string channelString, out  int value)
        {
            int pInvokeResult = PInvoke.niWLANA_GetOFDMDemodShortGuardIntervalB1Bit(Handle, channelString, out value);
            TestForError(pInvokeResult);
            return pInvokeResult;
        }
        /********************************************/

        #endregion



        /// <summary>

        ///Specifies the type of forward error correction (FEC) coding used if you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM,     as defined in section 20.3.11.3 of the IEEE Standard 802.11n-2009. If you set the NIWLANA_OFDM_DEMOD_HEADER_DETECTION_ENABLED attribute to NIWLANA_VAL_FALSE,     the toolkit uses the value of this attribute to perform decoding on bits obtained after OFDM demodulation.
        ///     Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/j/n/p license     and set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        ///     The default value is NIWLANA_VAL_FEC_CODING_TYPE_BCC.
        ///    Get Function: niWLANA_GetFECCodingType
        ///    Set Function: niWLANA_SetFECCodingType
        /// 
        /// </summary>
        public int SetFecCodingType(string channel, int value)
        {
            return SetInt(niWLANAProperties.FecCodingType, channel, value);
        }
        /// <summary>

        ///Specifies the type of forward error correction (FEC) coding used if you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM,     as defined in section 20.3.11.3 of the IEEE Standard 802.11n-2009. If you set the NIWLANA_OFDM_DEMOD_HEADER_DETECTION_ENABLED attribute to NIWLANA_VAL_FALSE,     the toolkit uses the value of this attribute to perform decoding on bits obtained after OFDM demodulation.
        ///     Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/j/n/p license     and set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        ///     The default value is NIWLANA_VAL_FEC_CODING_TYPE_BCC.
        ///    Get Function: niWLANA_GetFECCodingType
        ///    Set Function: niWLANA_SetFECCodingType
        /// 
        /// </summary>
        public int GetFecCodingType(string channel, out int value)
        {
            return GetInt(niWLANAProperties.FecCodingType, channel, out value);
        }
        /// <summary>

        ///Specifies the difference between the number of space-time streams and the number of spatial streams,     as defined in section 20.3.9.4.3 of the IEEE Standard 802.11n-2009.
        ///     The number of spatial streams is derived from the NIWLANA_MCS_INDEX attribute. Different space-time coding schemes are defined     in section 20.3.11.8.1 of the IEEE Standard 802.11n-2009. The toolkit uses this value to calculate the NIWLANA_RECOMMENDED_ACQUISITION_LENGTH attribute if you do not specify     the NIWLANA_ACQUISITION_LENGTH attribute. If you set the NIWLANA_OFDM_DEMOD_HEADER_DETECTION_ENABLED attribute to NIWLANA_VAL_FALSE, the toolkit uses the value     of the NIWLANA_STBC_INDEX attribute as the difference between the number of space-time streams and the number of spatial streams for performing OFDM     demodulation measurements.
        ///     Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/j/n/p license, and you must set the NIWLANA_STANDARD attribute     to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        ///     The default value is 0. Valid values are 0 to 2, inclusive.
        ///    Get Function: niWLANA_GetSTBCIndex
        ///    Set Function: niWLANA_SetSTBCIndex
        /// 
        /// </summary>
        public int SetStbcIndex(string channel, int value)
        {
            return SetInt(niWLANAProperties.StbcIndex, channel, value);
        }
        /// <summary>

        ///Specifies the difference between the number of space-time streams and the number of spatial streams,     as defined in section 20.3.9.4.3 of the IEEE Standard 802.11n-2009.
        ///     The number of spatial streams is derived from the NIWLANA_MCS_INDEX attribute. Different space-time coding schemes are defined     in section 20.3.11.8.1 of the IEEE Standard 802.11n-2009. The toolkit uses this value to calculate the NIWLANA_RECOMMENDED_ACQUISITION_LENGTH attribute if you do not specify     the NIWLANA_ACQUISITION_LENGTH attribute. If you set the NIWLANA_OFDM_DEMOD_HEADER_DETECTION_ENABLED attribute to NIWLANA_VAL_FALSE, the toolkit uses the value     of the NIWLANA_STBC_INDEX attribute as the difference between the number of space-time streams and the number of spatial streams for performing OFDM     demodulation measurements.
        ///     Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/j/n/p license, and you must set the NIWLANA_STANDARD attribute     to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        ///     The default value is 0. Valid values are 0 to 2, inclusive.
        ///    Get Function: niWLANA_GetSTBCIndex
        ///    Set Function: niWLANA_SetSTBCIndex
        /// 
        /// </summary>
        public int GetStbcIndex(string channel, out int value)
        {
            return GetInt(niWLANAProperties.StbcIndex, channel, out value);
        }
        /// <summary>

        ///Specifies whether to enable all traces of OFDM demodulation.
        ///     The default value is NIWLANA_VAL_FALSE.
        ///    Get Function: niWLANA_GetOFDMDemodAllTracesEnabled
        ///    Set Function: niWLANA_SetOFDMDemodAllTracesEnabled
        /// 
        /// </summary>
        public int SetOfdmDemodAllTracesEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.OfdmDemodAllTracesEnabled, channel, value);
        }
        /// <summary>

        ///Specifies whether to enable all traces of OFDM demodulation.
        ///     The default value is NIWLANA_VAL_FALSE.
        ///    Get Function: niWLANA_GetOFDMDemodAllTracesEnabled
        ///    Set Function: niWLANA_SetOFDMDemodAllTracesEnabled
        /// 
        /// </summary>
        public int GetOfdmDemodAllTracesEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodAllTracesEnabled, channel, out value);
        }
        /// <summary>

        ///Specifies whether to enable the channel frequency response trace for signals with an OFDM payload.
        ///     The default value is NIWLANA_VAL_FALSE.
        ///    Get Function: niWLANA_GetOFDMDemodChannelFrequencyResponseTraceEnabled
        ///    Set Function: niWLANA_SetOFDMDemodChannelFrequencyResponseTraceEnabled
        /// 
        /// </summary>
        public int SetOfdmDemodChannelFrequencyResponseTraceEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.OfdmDemodChannelFrequencyResponseTraceEnabled, channel, value);
        }
        /// <summary>

        ///Specifies whether to enable the channel frequency response trace for signals with an OFDM payload.
        ///     The default value is NIWLANA_VAL_FALSE.
        ///    Get Function: niWLANA_GetOFDMDemodChannelFrequencyResponseTraceEnabled
        ///    Set Function: niWLANA_SetOFDMDemodChannelFrequencyResponseTraceEnabled
        /// 
        /// </summary>
        public int GetOfdmDemodChannelFrequencyResponseTraceEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodChannelFrequencyResponseTraceEnabled, channel, out value);
        }
        /// <summary>

        ///Specifies whether to enable the spectral flatness trace for signals with an OFDM payload.
        ///    Get Function: niWLANA_GetOFDMDemodSpectralFlatnessTraceEnabled
        ///    Set Function: niWLANA_SetOFDMDemodSpectralFlatnessTraceEnabled
        /// 
        /// </summary>
        public int SetOfdmDemodSpectralFlatnessTraceEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.OfdmDemodSpectralFlatnessTraceEnabled, channel, value);
        }
        /// <summary>

        ///Specifies whether to enable the spectral flatness trace for signals with an OFDM payload.
        ///    Get Function: niWLANA_GetOFDMDemodSpectralFlatnessTraceEnabled
        ///    Set Function: niWLANA_SetOFDMDemodSpectralFlatnessTraceEnabled
        /// 
        /// </summary>
        public int GetOfdmDemodSpectralFlatnessTraceEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodSpectralFlatnessTraceEnabled, channel, out value);
        }
        /// <summary>

        ///Specifies whether to enable the decoded bits trace for signals with an OFDM payload.
        ///     The default value is NIWLANA_VAL_FALSE.
        ///    Get Function: niWLANA_GetOFDMDemodDecodedBitsTraceEnabled
        ///    Set Function: niWLANA_SetOFDMDemodDecodedBitsTraceEnabled
        /// 
        /// </summary>
        public int SetOfdmDemodDecodedBitsTraceEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.OfdmDemodDecodedBitsTraceEnabled, channel, value);
        }
        /// <summary>

        ///Specifies whether to enable the decoded bits trace for signals with an OFDM payload.
        ///     The default value is NIWLANA_VAL_FALSE.
        ///    Get Function: niWLANA_GetOFDMDemodDecodedBitsTraceEnabled
        ///    Set Function: niWLANA_SetOFDMDemodDecodedBitsTraceEnabled
        /// 
        /// </summary>
        public int GetOfdmDemodDecodedBitsTraceEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodDecodedBitsTraceEnabled, channel, out value);
        }
        /// <summary>

        ///Specifies the duration, in seconds, by which symbol timing adjusts relative to the start of the useful portion of an OFDM payload.
        ///    Note: National Instruments recommends that you set this attribute to the following values to get good EVM numbers.    If you set the NIWLANA_CHANNEL_BANDWIDTH attribute to 5 MHz, set the NIWLANA_OFDM_DEMOD_SYMBOL_TIMING_ADJUSTMENT attribute    to -800 ns. If you set the NIWLANA_CHANNEL_BANDWIDTH attribute to 10 MHz, set the NIWLANA_OFDM_DEMOD_SYMBOL_TIMING_ADJUSTMENT attribute    to -400 ns. This helps avoid ISI effects for narrow bandwidths.
        ///    The default value is &#8211;200 ns.
        ///    Get Function: niWLANA_GetOFDMDemodSymbolTimingAdjustment
        ///    Set Function: niWLANA_SetOFDMDemodSymbolTimingAdjustment
        /// 
        /// </summary>
        public int SetOfdmDemodSymbolTimingAdjustment(string channel, double value)
        {
            return SetDouble(niWLANAProperties.OfdmDemodSymbolTimingAdjustment, channel, value);
        }
        /// <summary>

        ///Specifies the duration, in seconds, by which symbol timing adjusts relative to the start of the useful portion of an OFDM payload.
        ///    Note: National Instruments recommends that you set this attribute to the following values to get good EVM numbers.    If you set the NIWLANA_CHANNEL_BANDWIDTH attribute to 5 MHz, set the NIWLANA_OFDM_DEMOD_SYMBOL_TIMING_ADJUSTMENT attribute    to -800 ns. If you set the NIWLANA_CHANNEL_BANDWIDTH attribute to 10 MHz, set the NIWLANA_OFDM_DEMOD_SYMBOL_TIMING_ADJUSTMENT attribute    to -400 ns. This helps avoid ISI effects for narrow bandwidths.
        ///    The default value is &#8211;200 ns.
        ///    Get Function: niWLANA_GetOFDMDemodSymbolTimingAdjustment
        ///    Set Function: niWLANA_SetOFDMDemodSymbolTimingAdjustment
        /// 
        /// </summary>
        public int GetOfdmDemodSymbolTimingAdjustment(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.OfdmDemodSymbolTimingAdjustment, channel, out value);
        }
        /// <summary>

        ///Specifies whether to estimate carrier frequency offset using only preamble or both preamble and data.
        ///    Get Function: niWLANA_GetOFDMDemodCFOEstimationMethod
        ///    Set Function: niWLANA_SetOFDMDemodCFOEstimationMethod
        /// 
        /// </summary>
        public int SetOfdmDemodCfoEstimationMethod(string channel, int value)
        {
            return SetInt(niWLANAProperties.OfdmDemodCfoEstimationMethod, channel, value);
        }
        /// <summary>

        ///Specifies whether to estimate carrier frequency offset using only preamble or both preamble and data.
        /// The default value is NIWLANA_VAL_CFO_ESTIMATION_METHOD_PREAMBLE_AND_DATA."
        /// Note: This attribute is new in toolkit version 3.0.0. In previous toolkit versions, the toolkit internally used NIWLANA_VAL_CFO_ESTIMATION_METHOD_PREAMBLE_ONLY for carrier frequency offset estimation when you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211AGJP_OFDM or NIWLANA_VAL_STANDARD_80211G_DSSS_OFDM. 
        /// If you want to preserve the results you obtained with previous tookit versions, you must set the NIWLANA_OFDM_DEMOD_CFO_ESTIMATION_METHOD attribute to NIWLANA_VAL_CFO_ESTIMATION_METHOD_PREAMBLE_ONLY.
        ///    Get Function: niWLANA_GetOFDMDemodCFOEstimationMethod
        ///    Set Function: niWLANA_SetOFDMDemodCFOEstimationMethod
        /// 
        /// </summary>
        public int GetOfdmDemodCfoEstimationMethod(string channel, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodCfoEstimationMethod, channel, out value);
        }
        /// <summary>

        ///Specifies whether to enable the decoded bits trace for the payload portion of the IEEE 802.11b or 802.11g DSSS signals.
        ///     Note: The toolkit does not support decoding of received bits for DSSS signals with a data rate of 33 Mbps, and the toolkit cannot return the decoded bits trace.
        ///     The default value is NIWLANA_VAL_FALSE.
        ///    Get Function: niWLANA_GetDSSSDemodDecodedBitsTraceEnabled
        ///    Set Function: niWLANA_SetDSSSDemodDecodedBitsTraceEnabled
        /// 
        /// </summary>
        public int SetDsssDemodDecodedBitsTraceEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.DsssDemodDecodedBitsTraceEnabled, channel, value);
        }
        /// <summary>

        ///Specifies whether to enable the decoded bits trace for the payload portion of the IEEE 802.11b or 802.11g DSSS signals.
        ///     Note: The toolkit does not support decoding of received bits for DSSS signals with a data rate of 33 Mbps, and the toolkit cannot return the decoded bits trace.
        ///     The default value is NIWLANA_VAL_FALSE.
        ///    Get Function: niWLANA_GetDSSSDemodDecodedBitsTraceEnabled
        ///    Set Function: niWLANA_SetDSSSDemodDecodedBitsTraceEnabled
        /// 
        /// </summary>
        public int GetDsssDemodDecodedBitsTraceEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.DsssDemodDecodedBitsTraceEnabled, channel, out value);
        }

        /// <summary>

        ///Returns, as a percentage, the peak value of the chip error vector magnitude (EVM) for the first 1,000 chips in the    payload computed according to section 18.4.7.8 of IEEE Standard 802.11-2007.
        ///     Note: If you set the NIWLANA_DSSS_DEMOD_MAXIMUM_CHIPS_USED attribute to a value less than 1,000, NaN is returned. If you set the NIWLANA_DSSS_DEMOD_MAXIMUM_CHIPS_USED     attribute to &#8211;1 and the actual number of chips present in the signal is less than 1,000, NaN is returned.
        ///     Note: For DSSS demodulation, if equalization is not enabled and the reference pulse-shaping filter     type and filter coefficients do not match the filter    configuration of the DUT or generator, you might notice EVM degradation.     Ensure the pulse-shaping type and pulse-shaping coefficient settings match    the input signal settings.
        ///     Note: If the pulse-shaping filter coefficient is less than 0.2, DSSS EVM might show degradation.
        ///     The toolkit broadly follows section 18.4.7.8 of IEEE Standard 802.11b-1999 and section 18.4.7.8 of IEEE Standard 802.11-2007 to compute the    EVM. The standard calls for EVM computation only on the differential quadrature phase-shift keying (DQPSK) signal.    However, the toolkit computes EVM for all compulsory and optional data rates and modulation schemes defined    for IEEE Standard 802.11b-1999, as well as the extended rate physical layer-packet binary convolutional coding (ERP-PBCC) modes defined in IEEE Standard 802.11g.    Refer to the EVM Differentiation for DSSS Signals help topic for an explanation of    the difference between RMS EVM, peak EVM, 802.11b-1999 peak EVM, and 802.11-2007 peak EVM.
        ///    
        /// 
        /// </summary>
        public int GetResultDsssDemod80211bPeakEvm2007(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultDsssDemod80211bPeakEvm2007, channel, out value);
        }

        /// <summary>

        ///Specifies the type of forward error correction (FEC) coding detected from the high-throughput SIGNAL field (HT-SIG)     as defined in section 20.3.9.4.3 of the IEEE Standard 802.11n-2009.
        ///     Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/j/n/p license, and you must set the NIWLANA_STANDARD attribute     to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        ///    
        /// 
        /// </summary>
        public int GetResultOfdmDemodFecCodingType(string channel, out int value)
        {
            return GetInt(niWLANAProperties.ResultOfdmDemodFecCodingType, channel, out value);
        }

        /// <summary>

        ///Returns the Not Sounding bit detected from the high-throughput SIGNAL (HT-SIG) field as defined in section 20.3.9.4.3 of the IEEE Standard 802.11n-2009.
        ///     Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/j/n/p license and set the NIWLANA_STANDARD attribute     to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        ///    
        /// 
        /// </summary>
        public int GetResultOfdmDemodNotSoundingBit(string channel, out int value)
        {
            return GetInt(niWLANAProperties.ResultOfdmDemodNotSoundingBit, channel, out value);
        }

        /// <summary>

        ///Returns the value of space-time block coding (STBC) field detected from the high-throughput SIGNAL (HT-SIG) field as defined in     section 20.3.9.4.3 of the IEEE Standard 802.11n-2009.
        ///     Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/j/n/p license and set the NIWLANA_STANDARD attribute     to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        ///    
        /// 
        /// </summary>
        public int GetResultOfdmDemodStbcIndex(string channel, out int value)
        {
            return GetInt(niWLANAProperties.ResultOfdmDemodStbcIndex, channel, out value);
        }



        /// <summary>

        ///Returns whether the parity check has passed for the SIGNAL field of the OFDM waveforms conforming to the IEEE standard 802.11-2007     or the non-HT SIGNAL (L-SIG) field of the MIMO OFDM waveforms conforming to the IEEE Standard 802.11n-2009.
        ///     Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/j/n/p license and set    the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211AGJP_OFDM or NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        ///   
        /// 
        /// </summary>
        public int GetResultOfdmDemodHeaderParityPassed(string channel, out int value)
        {
            return GetInt(niWLANAProperties.ResultOfdmDemodHeaderParityPassed, channel, out value);
        }

        /// <summary>

        ///Returns whether the cyclic redundancy check (CRC) has passed for the DSSS-OFDM physical layer convergence procedure (PLCP)     header field, as defined in section 19.3.2 of the IEEE Standard 802.11-2007.
        ///     Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/j/n/p license and set    the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211G_DSSS_OFDM.
        ///  
        /// 
        /// </summary>
        public int GetResultOfdmDemodDsssofdmHeaderCrcPassed(string channel, out int value)
        {
            return GetInt(niWLANAProperties.ResultOfdmDemodDsssofdmHeaderCrcPassed, channel, out value);
        }

        /// <summary>

        ///Returns the data rate, in Mbps, of the NIWLANA_VAL_STANDARD_80211AGJP_OFDM signal after incorporating the channel bandwidth.
        ///     A lower bandwidth, for example 10 MHz, uses a lower clocking scheme and reduces the data rate.
        ///     This attribute is valid only if you set the NIWLAN_STANDARD attribute to NIWLANA_VAL_STANDARD_80211AGJP_OFDM.
        ///   
        /// 
        /// </summary>
        public int GetResultOfdmDemodEffectiveDataRate(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultOfdmDemodEffectiveDataRate, channel, out value);
        }

        /// <summary>

        ///Specifies whether the frame check sequence (FCS) of the received decoded MAC protocol data unit (MPDU) has passed.     The toolkit calculates the checksum over the decoded bits excluding the last 32 bits. The toolkit then compares this value with the checksum value     in the received payload, which is represented by the last 32 bits of the MPDU.
        ///     Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/j/n/p license and set    the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211AGJP_OFDM or NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        ///     Note: The toolkit returns an error if you query this attribute after performing demodulation measurements on DSSS signals with a data rate of 33 Mbps.
        ///   
        /// 
        /// </summary>
        public int GetResultOfdmDemodMacFrameCheckSequencePassed(string channel, out int value)
        {
            return GetInt(niWLANAProperties.ResultOfdmDemodMacFrameCheckSequencePassed, channel, out value);
        }

        /// <summary>

        ///Indicates whether the frame check sequence (FCS) of received decoded MAC protocol data unit (MPDU) has passed.     The toolkit calculates the checksum over the decoded bits, excluding the last 32 bits. The toolkit then compares this value with the checksum value     in the received payload, which is represented by the last 32 bits of the MPDU.
        ///     Note: Decoding of received bits is not supported for DSSS signals with a data rate of 33 Mbps, and the toolkit cannot compute MAC FCS.     The toolkit will return an error if you query this attribute after performing demodulation measurements on DSSS signals with a data rate of 33 Mbps.
        ///   
        /// 
        /// </summary>
        public int GetResultDsssDemodMacFrameCheckSequencePassed(string channel, out int value)
        {
            return GetInt(niWLANAProperties.ResultDsssDemodMacFrameCheckSequencePassed, channel, out value);
        }



        /// <summary>
        /// Specifies the physical layer convergence procedure (PLCP) frame format is used when the NIWLANA_OFDM_DEMOD_80211N_PLCP_FRAME_DETECTION_ENABLED     attribute is set to NIWLANA_VAL_FALSE or for acquisition length calculations because the PLCP frame format determines sequence of preambles,     header, and payload in a frame. If you set the NIWLANA_DSSS_DEMOD_HEADER_DETECTION_ENABLED attribute to NIWLANA_VAL_FALSE, the toolkit uses the value     of the NIWLANA_80211N_PLCP_FRAME_FORMAT attribute as the PLCP frame format for performing orthogonal frequency division multiplexing     (OFDM) demodulation measurements.
        ///     Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/n     license and set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        ///     The default value is NIWLANA_VAL_80211N_PLCP_FRAME_FORMAT_MIXED.    
        /// 
        /// </summary>
        public int Set_80211nPlcpFrameFormat(string channel, int value)
        {
            return SetInt(niWLANAProperties._80211nPlcpFrameFormat, channel, value);
        }

        /// <summary>
        /// Specifies the physical layer convergence procedure (PLCP) frame format is used when the NIWLANA_OFDM_DEMOD_80211N_PLCP_FRAME_DETECTION_ENABLED     attribute is set to NIWLANA_VAL_FALSE or for acquisition length calculations because the PLCP frame format determines sequence of preambles,     header, and payload in a frame. If you set the NIWLANA_DSSS_DEMOD_HEADER_DETECTION_ENABLED attribute to NIWLANA_VAL_FALSE, the toolkit uses the value     of the NIWLANA_80211N_PLCP_FRAME_FORMAT attribute as the PLCP frame format for performing orthogonal frequency division multiplexing     (OFDM) demodulation measurements.
        ///     Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/n     license and set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        ///     The default value is NIWLANA_VAL_80211N_PLCP_FRAME_FORMAT_MIXED.    
        /// 
        /// </summary>
        public int Get_80211nPlcpFrameFormat(string channel, out int value)
        {
            return GetInt(niWLANAProperties._80211nPlcpFrameFormat, channel, out value);
        }

        /// <summary>
        /// Specifies the duration, in seconds, for which the RF signal analyzer acquires the signal after being triggered.    If you do not specify the acquisition length but configure any of the following attributes,     the toolkit calculates the acquisition length based on the attributes.
        ///     IEEE Standard 802.11n-2009
        ///     &#8226;NIWLANA_OFDM_PAYLOAD_LENGTH
        ///     &#8226;NIWLANA_GUARD_INTERVAL
        ///     &#8226;NIWLANA_80211N_PLCP_FRAME_FORMAT
        ///     &#8226;NIWLANA_NUMBER_OF_EXTENSION_SPATIAL_STREAMS
        ///     &#8226;NIWLANA_MCS_INDEX
        ///     IEEE Standard 802.11a-1999, IEEE Standard 802.11b-1999, and IEEE Standard 802.11g-2003
        ///     &#8226;NIWLANA_OFDM_PAYLOAD_LENGTH/NIWLANA_DSSS_PAYLOAD_LENGTH
        ///     &#8226;NIWLANA_OFDM_DATA_RATE/NIWLANA_DSSS_DATA_RATE 
        ///     The toolkit calculates the acquisition length based on the specified parameters. For the properties that you do not specify, toolkit uses default values.
        ///     Note: If you only specify one of the above attributes,     the recommended acquisition length may not be optimal because the toolkit uses default values for the other attributes.
        ///     If the NIWLANA_GATED_SPECTRUM_MODE attribute is set to NIWLANA_VAL_GATED_SPECTRUM_MODE_ACQUISITION_LENGTH or     NIWLANA_VAL_GATED_SPECTRUM_MODE_RBW_AND_ACQUISITION_LENGTH, you must configure the acquisition length     parameter before calling the niWLANA_RFSAReadGatedPowerSpectrum attribute. The acquisitionLength parameter    sets the gate length for the gated spectrum acquisition.
        ///     Note: If you do not set the acquisitionLength parameter, you can set this value     using the niWLANA_RFSAAutoRange function for demodulation and power measurements.
        ///     The default value is 1m.        
        /// 
        /// </summary>
        public int GetAcquisitionLength(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.AcquisitionLength, channel, out value);
        }

        /// <summary>
        /// Specifies the maximum duration to acquire while performing autorange operation using the niWLANA_RFSAAutoRange function.
        ///    The default value is 30 ms.    
        /// 
        /// </summary>
        public int SetAutorangeMaxAcquisitionLength(string channel, double value)
        {
            return SetDouble(niWLANAProperties.AutorangeMaxAcquisitionLength, channel, value);
        }

        /// <summary>
        /// Specifies the maximum duration to acquire while performing autorange operation using the niWLANA_RFSAAutoRange function.
        ///    The default value is 30 ms.    
        /// 
        /// </summary>
        public int GetAutorangeMaxAcquisitionLength(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.AutorangeMaxAcquisitionLength, channel, out value);
        }

        /// <summary>
        /// Specifies the maximum expected idle interval between the bursts to acquire while performing autorange operation using the niWLANA_RFSAAutoRange function.
        ///    The default value is 100 µs.    
        /// 
        /// </summary>
        public int SetAutorangeMaxIdleTime(string channel, double value)
        {
            return SetDouble(niWLANAProperties.AutorangeMaxIdleTime, channel, value);
        }

        /// <summary>
        /// Specifies the maximum expected idle interval between the bursts to acquire while performing autorange operation using the niWLANA_RFSAAutoRange function.
        ///    The default value is 100 µs.    
        /// 
        /// </summary>
        public int GetAutorangeMaxIdleTime(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.AutorangeMaxIdleTime, channel, out value);
        }

        /// <summary>
        /// Specifies the carrier frequency, in hertz (Hz), at which the RF signal analyzer acquires a    WLAN signal. Set this value equal to the carrier frequency of the transmitting device under test (DUT).
        ///     The default value is 2.412G.        
        /// 
        /// </summary>s
        [Obsolete]
        public int GetCarrierFrequency(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.CarrierFrequency, channel, out value);
        }

        /// <summary>
        /// Specifies the bandwidth, in hertz (Hz), of the high throughput (HT) mixed format or Greenfield     format signal to be analyzed. The toolkit uses this value in the niWLANA_RFSAConfigure function to determine     the appropriate sampling rate and symbol structure for demodulation purposes.
        ///     Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/n     license and set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        ///     The default value is 20M. Valid values are 20M and 40M.   
        /// 
        /// </summary>
        public int GetChannelBandwidth(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ChannelBandwidth, channel, out value);
        }

        /// <summary>
        /// Indicates the WLAN Analysis Toolkit version in use.   
        /// 
        /// </summary>
        public int SetCompatibilityVersion(string channel, int value)
        {
            return SetInt(niWLANAProperties.CompatibilityVersion, channel, value);
        }

        /// <summary>
        /// Indicates the WLAN Analysis Toolkit version in use.   
        /// 
        /// </summary>
        public int GetCompatibilityVersion(string channel, out int value)
        {
            return GetInt(niWLANAProperties.CompatibilityVersion, channel, out value);
        }

        /// <summary>
        /// Specifies the payload data rate, in Mbps, of the expected incoming signal. If you set the    NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211BG_DSSS and you have not configured an acquisition length,    the toolkit uses the NIWLANA_DSSS_DATA_RATE attribute to calculate the desired acquisition length. If you set the    NIWLANA_DSSS_HEADER_DETECTION_ENABLED attribute to NIWLANA_VAL_FALSE, the toolkit uses the value of the NIWLANA_DSSS_DATA_RATE     attribute as the data rate for    performing direct sequence spread spectrum (DSSS) demodulation measurements.
        ///     The default value is NIWLANA_VAL_DSSS_DATA_RATE_1.  
        /// 
        /// </summary>
        public int SetDsssDataRate(string channel, int value)
        {
            return SetInt(niWLANAProperties.DsssDataRate, channel, value);
        }

        /// <summary>
        /// Specifies the payload data rate, in Mbps, of the expected incoming signal. If you set the    NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211BG_DSSS and you have not configured an acquisition length,    the toolkit uses the NIWLANA_DSSS_DATA_RATE attribute to calculate the desired acquisition length. If you set the    NIWLANA_DSSS_HEADER_DETECTION_ENABLED attribute to NIWLANA_VAL_FALSE, the toolkit uses the value of the NIWLANA_DSSS_DATA_RATE     attribute as the data rate for    performing direct sequence spread spectrum (DSSS) demodulation measurements.
        ///     The default value is NIWLANA_VAL_DSSS_DATA_RATE_1.  
        /// 
        /// </summary>
        public int GetDsssDataRate(string channel, out int value)
        {
            return GetInt(niWLANAProperties.DsssDataRate, channel, out value);
        }

        /// <summary>
        /// Specifies whether to enable the constellation trace for the payload portion of 802.11a or 802.11g orthogonal frequency division multiplexing (OFDM)    or 802.11g direct sequence spread spectrum-OFDM (DSSS-OFDM) signals.
        ///     The default value is NIWLANA_VAL_FALSE.        
        /// 
        /// </summary>
        public int SetDsssDemodConstellationTraceEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.DsssDemodConstellationTraceEnabled, channel, value);
        }

        /// <summary>
        /// Specifies whether to enable the constellation trace for the payload portion of 802.11a or 802.11g orthogonal frequency division multiplexing (OFDM)    or 802.11g direct sequence spread spectrum-OFDM (DSSS-OFDM) signals.
        ///     The default value is NIWLANA_VAL_FALSE.        
        /// 
        /// </summary>
        public int GetDsssDemodConstellationTraceEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.DsssDemodConstellationTraceEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies whether to enable demodulation-based measurements for 802.11b and 802.11g direct sequence spread spectrum (DSSS) signals.
        ///     The default value is NIWLANA_VAL_FALSE.          
        /// 
        /// </summary>
        public int GetDsssDemodEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.DsssDemodEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies whether to enable equalization. The standard does not allow equalization before computing    error vector magnitude (EVM).
        ///  Note: If you set the NIWLANA_DSSS_DEMOD_EQUALIZATION_ENABLED attribute to NIWLANA_VAL_TRUE, the actual number of chips the toolkit uses to calculate EVM varies.
        ///  For example, the toolkit uses 773 chips for EVM calculation, if you set the NIWLANA_DSSS_DEMOD_MAXIMUM_CHIPS_USED attribute to 1,000.
        ///  If you increase the value of the NIWLANA_DSSS_DEMOD_MAXIMUM_CHIPS_USED attribute, 
        /// the toolkit uses up to a maximum of 991 more chips."    
        /// The default value is NIWLANA_VAL_FALSE.        
        /// 
        /// </summary>
        public int SetDsssDemodEqualizationEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.DsssDemodEqualizationEnabled, channel, value);
        }

        /// <summary>
        /// Specifies whether to enable equalization. The standard does not allow equalization before computing    error vector magnitude (EVM).
        ///     The default value is NIWLANA_VAL_FALSE.        
        /// 
        /// </summary>
        public int GetDsssDemodEqualizationEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.DsssDemodEqualizationEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies whether to enable EVM per symbol trace for 802.11b and 802.11g direct sequence spread spectrum (DSSS) signals.
        ///     The default value is NIWLANA_VAL_FALSE. 
        /// 
        /// </summary>
        [Obsolete]
        public int SetDsssDemodEvmPerSymbolTraceEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.DsssDemodEvmPerSymbolTraceEnabled, channel, value);
        }

        /// <summary>
        /// Specifies whether to enable EVM per symbol trace for 802.11b and 802.11g direct sequence spread spectrum (DSSS) signals.
        ///     The default value is NIWLANA_VAL_FALSE. 
        /// 
        /// </summary>
        [Obsolete]
        public int GetDsssDemodEvmPerSymbolTraceEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.DsssDemodEvmPerSymbolTraceEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies whether to enable computation of time-gated power.
        ///     The default value is NIWLANA_VAL_FALSE.        
        /// 
        /// </summary>
        public int SetDsssDemodGatedPowerEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.DsssDemodGatedPowerEnabled, channel, value);
        }

        /// <summary>
        /// Specifies whether to enable computation of time-gated power.
        ///     The default value is NIWLANA_VAL_FALSE.        
        /// 
        /// </summary>
        public int GetDsssDemodGatedPowerEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.DsssDemodGatedPowerEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies the start time for computing gated power in seconds. The value 0 indicates the start time    is the start of the payload.
        ///     A negative value indicates a position in the preamble or header.    Refer to the Gated Power topic for more information.
        ///     The default value is 0.        
        /// 
        /// </summary>
        public int SetDsssDemodGatedPowerStartTime(string channel, double value)
        {
            return SetDouble(niWLANAProperties.DsssDemodGatedPowerStartTime, channel, value);
        }

        /// <summary>
        /// Specifies the start time for computing gated power in seconds. The value 0 indicates the start time    is the start of the payload.
        ///     A negative value indicates a position in the preamble or header.    Refer to the Gated Power topic for more information.
        ///     The default value is 0.        
        /// 
        /// </summary>
        public int GetDsssDemodGatedPowerStartTime(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.DsssDemodGatedPowerStartTime, channel, out value);
        }

        /// <summary>
        /// Specifies the stop time for computing gated power in seconds. The value 0 indicates the stop time    is the start of the payload.
        ///     A negative value indicates a position in the preamble or header.    Refer to the Gated Power topic for more information.
        ///     The default value is 64 microseconds.        
        /// 
        /// </summary>
        public int SetDsssDemodGatedPowerStopTime(string channel, double value)
        {
            return SetDouble(niWLANAProperties.DsssDemodGatedPowerStopTime, channel, value);
        }

        /// <summary>
        /// Specifies the stop time for computing gated power in seconds. The value 0 indicates the stop time    is the start of the payload.
        ///     A negative value indicates a position in the preamble or header.    Refer to the Gated Power topic for more information.
        ///     The default value is 64 microseconds.        
        /// 
        /// </summary>
        public int GetDsssDemodGatedPowerStopTime(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.DsssDemodGatedPowerStopTime, channel, out value);
        }

        /// <summary>
        /// Specifies whether to enable automatic detection of the physical layer (PHY) header.
        ///     The default value is NIWLANA_VAL_FALSE.        
        /// 
        /// </summary>
        public int SetDsssDemodHeaderDetectionEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.DsssDemodHeaderDetectionEnabled, channel, value);
        }

        /// <summary>
        /// Specifies whether to enable automatic detection of the physical layer (PHY) header.
        ///     The default value is NIWLANA_VAL_FALSE.        
        /// 
        /// </summary>
        public int GetDsssDemodHeaderDetectionEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.DsssDemodHeaderDetectionEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies whether to do low pass filtering after acquiring the signal in baseband to remove out of band noise.
        ///     The default value is NIWLANA_VAL_TRUE.  
        /// 
        /// </summary>
        public int SetDsssDemodLowpassFilteringEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.DsssDemodLowpassFilteringEnabled, channel, value);
        }

        /// <summary>
        /// Specifies whether to do low pass filtering after acquiring the signal in baseband to remove out of band noise.
        ///     The default value is NIWLANA_VAL_TRUE.  
        /// 
        /// </summary>
        public int GetDsssDemodLowpassFilteringEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.DsssDemodLowpassFilteringEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies the maximum number of chips used to compute error vector magnitude (EVM).   If you specify (p) as the number of available chips in the payload and |p-q| is greater than or equal to 300, the toolkit ignores (p-q) chips from the end of the payload and computes EVM on (q) chips.
        /// If |p-q| is less than 300, the toolkit ignores the last 300 chips and computes EVM on |p-300| chips.
        /// Note: If you set this attribute to -1, the toolkit measures EVM on |p-300| chips.\n\n"   
        /// The default value is 1,000. Decreasing this value to significantly increase measurement speed.        
        /// 
        /// </summary>
        public int SetDsssDemodMaximumChipsUsed(string channel, int value)
        {
            return SetInt(niWLANAProperties.DsssDemodMaximumChipsUsed, channel, value);
        }

        /// <summary>
        ///  Specifies the maximum number of chips used to compute error vector magnitude (EVM).   If you specify (p) as the number of available chips in the payload and |p-q| is greater than or equal to 300, the toolkit ignores (p-q) chips from the end of the payload and computes EVM on (q) chips.
        /// If |p-q| is less than 300, the toolkit ignores the last 300 chips and computes EVM on |p-300| chips.
        /// Note: If you set this attribute to -1, the toolkit measures EVM on |p-300| chips.\n\n"   
        /// The default value is 1,000. Decreasing this value to significantly increase measurement speed.        
        ///
        /// </summary>
        public int GetDsssDemodMaximumChipsUsed(string channel, out int value)
        {
            return GetInt(niWLANAProperties.DsssDemodMaximumChipsUsed, channel, out value);
        }

        /// <summary>
        /// Specifies the number of payload chips to remove before starting the error vector magnitude (EVM)    computation. If the number of available payload chips (n) is greater than the value you specify    (m), the toolkit ignores (n-m) chips from the end.
        ///     The default value is 0.        
        /// 
        /// </summary>
        public int SetDsssDemodMeasurementStartLocation(string channel, int value)
        {
            return SetInt(niWLANAProperties.DsssDemodMeasurementStartLocation, channel, value);
        }

        /// <summary>
        /// Specifies the number of payload chips to remove before starting the error vector magnitude (EVM)    computation. If the number of available payload chips (n) is greater than the value you specify    (m), the toolkit ignores (n-m) chips from the end.
        ///     The default value is 0.        
        /// 
        /// </summary>
        public int GetDsssDemodMeasurementStartLocation(string channel, out int value)
        {
            return GetInt(niWLANAProperties.DsssDemodMeasurementStartLocation, channel, out value);
        }

        /// <summary>
        /// Specifies the number of iterations over which the toolkit averages direct sequence spread spectrum (DSSS) demodulation-based measurements.
        ///     If you increase the number of averages, the toolkit provides smoother values but takes a longer time to compute the values.
        ///       The default value is 1. Valid values are 1 to 1,000 (inclusive).        
        /// 
        /// </summary>
        public int GetDsssDemodNumberOfAverages(string channel, out int value)
        {
            return GetInt(niWLANAProperties.DsssDemodNumberOfAverages, channel, out value);
        }

        /// <summary>
        /// Specifies which type of phase tracking to enable. Phase tracking is useful for tracking the phase    variations caused by residual frequency offset and phase noise.
        ///     The default value is NIWLAN_VAL_DSSS_PHASE_TRACKING_STANDARD.   
        /// 
        /// </summary>
        public int SetDsssDemodPhaseTracking(string channel, int value)
        {
            return SetInt(niWLANAProperties.DsssDemodPhaseTracking, channel, value);
        }

        /// <summary>
        /// Specifies which type of phase tracking to enable. Phase tracking is useful for tracking the phase    variations caused by residual frequency offset and phase noise.
        ///     The default value is NIWLAN_VAL_DSSS_PHASE_TRACKING_STANDARD.   
        /// 
        /// </summary>
        public int GetDsssDemodPhaseTracking(string channel, out int value)
        {
            return GetInt(niWLANAProperties.DsssDemodPhaseTracking, channel, out value);
        }

        /// <summary>
        /// Specifies the value of alpha if the NIWLANA_DSSS_DEMOD_REFERENCE_FILTER_TYPE attribute    is set to NIWLANA_VAL_RAISED_COSINE or NIWLANA_VAL_ROOT_RAISED_COSINE.    If the NIWLANA_DSSS_DEMOD_REFERENCE_FILTER_TYPE attribute    is set to NIWLANA_VAL_GAUSSIAN in the non-equalization mode,    the NIWLANA_DSSS_DEMOD_REFERENCE_PULSE_SHAPING_FILTER_COEFFICIENT attribute specifies the value of B_T.    The toolkit ignores the NIWLANA_DSSS_DEMOD_REFERENCE_PULSE_SHAPING_FILTER_COEFFICIENT attribute in other cases.
        ///     The default value is 0.5.        
        /// 
        /// </summary>
        public int SetDsssDemodReferencePulseShapingFilterCoefficient(string channel, double value)
        {
            return SetDouble(niWLANAProperties.DsssDemodReferencePulseShapingFilterCoefficient, channel, value);
        }

        /// <summary>
        /// Specifies the value of alpha if the NIWLANA_DSSS_DEMOD_REFERENCE_FILTER_TYPE attribute    is set to NIWLANA_VAL_RAISED_COSINE or NIWLANA_VAL_ROOT_RAISED_COSINE.    If the NIWLANA_DSSS_DEMOD_REFERENCE_FILTER_TYPE attribute    is set to NIWLANA_VAL_GAUSSIAN in the non-equalization mode,    the NIWLANA_DSSS_DEMOD_REFERENCE_PULSE_SHAPING_FILTER_COEFFICIENT attribute specifies the value of B_T.    The toolkit ignores the NIWLANA_DSSS_DEMOD_REFERENCE_PULSE_SHAPING_FILTER_COEFFICIENT attribute in other cases.
        ///     The default value is 0.5.        
        /// 
        /// </summary>
        public int GetDsssDemodReferencePulseShapingFilterCoefficient(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.DsssDemodReferencePulseShapingFilterCoefficient, channel, out value);
        }

        /// <summary>
        /// Specifies the pulse-shaping filter for generation of matched-filter coefficients. In most cases,    you must set this attribute to the option that is used for the unit under test (UUT).    The toolkit ignores this attribute if you set the NIWLANA_DSSS_DEMOD_EQUALIZATION_ENABLED attribute to NIWLANA_VAL_TRUE.        
        /// 
        /// </summary>
        public int SetDsssDemodReferencePulseShapingFilterType(string channel, int value)
        {
            return SetInt(niWLANAProperties.DsssDemodReferencePulseShapingFilterType, channel, value);
        }

        /// <summary>
        /// Specifies the pulse-shaping filter for generation of matched-filter coefficients. In most cases,    you must set this attribute to the option that is used for the unit under test (UUT).    The toolkit ignores this attribute if you set the NIWLANA_DSSS_DEMOD_EQUALIZATION_ENABLED attribute to NIWLANA_VAL_TRUE.        
        /// 
        /// </summary>
        public int GetDsssDemodReferencePulseShapingFilterType(string channel, out int value)
        {
            return GetInt(niWLANAProperties.DsssDemodReferencePulseShapingFilterType, channel, out value);
        }

        /// <summary>
        /// Specifies the payload length, in bytes, of the expected incoming signal. If you set the    NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211BG_DSSS and you have not configured an acquisition length,    the toolkit uses the NIWLANA_DSSS_PAYLOAD_LENGTH attribute to calculate the desired acquisition length. If you set the    NIWLANA_DSSS_HEADER_DETECTION_ENABLED attribute to NIWLANA_VAL_FALSE, the toolkit uses the value of the NIWLANA_DSSS_PAYLOAD_LENGTH     attribute as the payload length for performing direct sequence spread spectrum (DSSS) demodulation measurements.
        ///     The default value is 1,024.        
        /// 
        /// </summary>
        public int SetDsssPayloadLength(string channel, int value)
        {
            return SetInt(niWLANAProperties.DsssPayloadLength, channel, value);
        }

        /// <summary>
        /// Specifies the payload length, in bytes, of the expected incoming signal. If you set the    NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211BG_DSSS and you have not configured an acquisition length,    the toolkit uses the NIWLANA_DSSS_PAYLOAD_LENGTH attribute to calculate the desired acquisition length. If you set the    NIWLANA_DSSS_HEADER_DETECTION_ENABLED attribute to NIWLANA_VAL_FALSE, the toolkit uses the value of the NIWLANA_DSSS_PAYLOAD_LENGTH     attribute as the payload length for performing direct sequence spread spectrum (DSSS) demodulation measurements.
        ///     The default value is 1,024.        
        /// 
        /// </summary>
        public int GetDsssPayloadLength(string channel, out int value)
        {
            return GetInt(niWLANAProperties.DsssPayloadLength, channel, out value);
        }

        /// <summary>
        /// Specifies the percentage of the maximum windowed burst power at which the power ramp down    in the burst is assumed to start.
        ///     The default value is 90.         
        /// 
        /// </summary>
        public int SetDsssPowerRampDownHighThreshold(string channel, double value)
        {
            return SetDouble(niWLANAProperties.DsssPowerRampDownHighThreshold, channel, value);
        }

        /// <summary>
        /// Specifies the percentage of the maximum windowed burst power at which the power ramp down    in the burst is assumed to start.
        ///     The default value is 90.         
        /// 
        /// </summary>
        public int GetDsssPowerRampDownHighThreshold(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.DsssPowerRampDownHighThreshold, channel, out value);
        }

        /// <summary>
        /// Specifies the percentage of the maximum windowed burst power at which the power ramp down    in the burst is assumed to end.
        ///     The default value is 10.        
        /// 
        /// </summary>
        public int SetDsssPowerRampDownLowThreshold(string channel, double value)
        {
            return SetDouble(niWLANAProperties.DsssPowerRampDownLowThreshold, channel, value);
        }

        /// <summary>
        /// Specifies the percentage of the maximum windowed burst power at which the power ramp down    in the burst is assumed to end.
        ///     The default value is 10.        
        /// 
        /// </summary>
        public int GetDsssPowerRampDownLowThreshold(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.DsssPowerRampDownLowThreshold, channel, out value);
        }

        /// <summary>
        /// Specifies whether to enable measurement of the power ramp-up or ramp-down time for    802.11b and 802.11g DSSS signals.
        ///     You must set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211BG_DSSS to use     the NIWLANA_DSSS_POWER_RAMP_MEASUREMENT_ENABLED attribute. If the NIWLANA_STANDARD attribute is set to any other value, the toolkit returns an error.
        ///     The default value is NIWLANA_VAL_FALSE.        
        /// 
        /// </summary>
        public int GetDsssPowerRampMeasurementEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.DsssPowerRampMeasurementEnabled, channel, out value);
        }



        /// <summary>
        /// Specifies the number of iterations over which the toolkit averages direct sequence spread spectrum (DSSS) power ramp measurements.
        ///     If you increase the number of averages, the toolkit provides smoother values but takes a longer time to compute the values.
        ///     The default value is 1. Valid values are 1 to 1,000 (inclusive).        
        /// 
        /// </summary>
        public int GetDsssPowerRampMeasurementNumberOfAverages(string channel, out int value)
        {
            return GetInt(niWLANAProperties.DsssPowerRampMeasurementNumberOfAverages, channel, out value);
        }

        /// <summary>
        /// Specifies the percentage of the maximum windowed burst power at which the power ramp up    in the burst is assumed to end.
        ///     The default value is 90.        
        /// 
        /// </summary>
        public int SetDsssPowerRampUpHighThreshold(string channel, double value)
        {
            return SetDouble(niWLANAProperties.DsssPowerRampUpHighThreshold, channel, value);
        }

        /// <summary>
        /// Specifies the percentage of the maximum windowed burst power at which the power ramp up    in the burst is assumed to end.
        ///     The default value is 90.        
        /// 
        /// </summary>
        public int GetDsssPowerRampUpHighThreshold(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.DsssPowerRampUpHighThreshold, channel, out value);
        }

        /// <summary>
        /// Specifies the percentage of the maximum windowed burst power at which the power ramp up    in the burst is assumed to start.
        ///     The default value is 10.        
        /// 
        /// </summary>
        public int SetDsssPowerRampUpLowThreshold(string channel, double value)
        {
            return SetDouble(niWLANAProperties.DsssPowerRampUpLowThreshold, channel, value);
        }

        /// <summary>
        /// Specifies the percentage of the maximum windowed burst power at which the power ramp up    in the burst is assumed to start.
        ///     The default value is 10.        
        /// 
        /// </summary>
        public int GetDsssPowerRampUpLowThreshold(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.DsssPowerRampUpLowThreshold, channel, out value);
        }

        /// <summary>
        /// Specifies the fast Fourier transform (FFT) length, in samples, used to compute the acquired I/Q.     If you use the niWLANA_ConfigureRFSA attribute,    the toolkit ignores this value. If you configure the NI PXI-5661 or NI PXIe-5663    independently or use other hardware, you must manually specify the correct FFT length.
        ///     Note: Signals conforming to IEEE Standard 802.11n-2009 do not support NI PXI-5661.
        ///      The default value is 222.          
        /// 
        /// </summary>
        public int SetFftSize(string channel, int value)
        {
            return SetInt(niWLANAProperties.FftSize, channel, value);
        }

        /// <summary>
        /// Specifies the fast Fourier transform (FFT) length, in samples, used to compute the acquired I/Q.     If you use the niWLANA_ConfigureRFSA attribute,    the toolkit ignores this value. If you configure the NI PXI-5661 or NI PXIe-5663    independently or use other hardware, you must manually specify the correct FFT length.
        ///     Note: Signals conforming to IEEE Standard 802.11n-2009 do not support NI PXI-5661.
        ///      The default value is 222.          
        /// 
        /// </summary>
        public int GetFftSize(string channel, out int value)
        {
            return GetInt(niWLANAProperties.FftSize, channel, out value);
        }

        /// <summary>
        /// Specifies the size of the window, in samples, used in the fast Fourier transform (FFT).    If you use the niWLANA_ConfigureRFSA attribute,    the toolkit ignores this value. If you configure the NI PXI-5661 or NI PXIe-5663    independently or use other hardware, you must manually specify the correct window size.
        ///     Note: For signals conforming to IEEE Standard 802.11n-2009, the toolkit does not support NI PXI-5661.
        ///     If you set the standard parameter of the niWLANA_SetStandard function to     NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use an channelx active channel string to configure this attribute.
        ///     The default value is 222.        
        /// 
        /// </summary>
        public int SetFftWindowSize(string channel, int value)
        {
            return SetInt(niWLANAProperties.FftWindowSize, channel, value);
        }

        /// <summary>
        /// Specifies the size of the window, in samples, used in the fast Fourier transform (FFT).    If you use the niWLANA_ConfigureRFSA attribute,    the toolkit ignores this value. If you configure the NI PXI-5661 or NI PXIe-5663    independently or use other hardware, you must manually specify the correct window size.
        ///     Note: For signals conforming to IEEE Standard 802.11n-2009, the toolkit does not support NI PXI-5661.
        ///     If you set the standard parameter of the niWLANA_SetStandard function to     NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use an channelx active channel string to configure this attribute.
        ///     The default value is 222.        
        /// 
        /// </summary>
        public int GetFftWindowSize(string channel, out int value)
        {
            return GetInt(niWLANAProperties.FftWindowSize, channel, out value);
        }

        /// <summary>
        /// Specifies the time-domain window type to use for smoothing the spectrum.
        ///     The default value is NIWLANA_VAL_WINDOW_7_TERM_BLACKMAN_HARRIS.        
        /// 
        /// </summary>
        public int SetFftWindowType(string channel, int value)
        {
            return SetInt(niWLANAProperties.FftWindowType, channel, value);
        }

        /// <summary>
        /// Specifies the time-domain window type to use for smoothing the spectrum.
        ///     The default value is NIWLANA_VAL_WINDOW_7_TERM_BLACKMAN_HARRIS.        
        /// 
        /// </summary>
        public int GetFftWindowType(string channel, out int value)
        {
            return GetInt(niWLANAProperties.FftWindowType, channel, out value);
        }

        /// <summary>
        /// Specifies whether to enable gated spectrum acquisition for spectral measurements. 
        /// 
        /// </summary>
        public int SetGatedSpectrumEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.GatedSpectrumEnabled, channel, value);
        }

        /// <summary>
        /// Specifies whether to enable gated spectrum acquisition for spectral measurements. 
        /// 
        /// </summary>
        public int GetGatedSpectrumEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.GatedSpectrumEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies the mode for computing gated spectrum.
        ///     The default value is NIWLANA_VAL_GATED_SPECTRUM_MODE_RBW.  
        /// 
        /// </summary>
        public int SetGatedSpectrumMode(string channel, int value)
        {
            return SetInt(niWLANAProperties.GatedSpectrumMode, channel, value);
        }

        /// <summary>
        /// Specifies the mode for computing gated spectrum.
        ///     The default value is NIWLANA_VAL_GATED_SPECTRUM_MODE_RBW.  
        /// 
        /// </summary>
        public int GetGatedSpectrumMode(string channel, out int value)
        {
            return GetInt(niWLANAProperties.GatedSpectrumMode, channel, out value);
        }

        /// <summary>
        /// Specifies the guard interval, in seconds, which is used in manual header detection and acquisition length calculation.     If you set the NIWLANA_OFDM_DEMOD_HEADER_DETECTION_ENABLED attribute to NIWLANA_VAL_FALSE, the toolkit uses the value of the NIWLANA_GUARD_INTERVAL attribute as the guard     interval for performing orthogonal frequency division multiplexing (OFDM) demodulation measurements.
        ///      Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/n     license and set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        ///     The default value is 800n. Valid values are 800n and 400n. 
        /// 
        /// </summary>
        public int SetGuardInterval(string channel, double value)
        {
            return SetDouble(niWLANAProperties.GuardInterval, channel, value);
        }

        /// <summary>
        /// Specifies the guard interval, in seconds, which is used in manual header detection and acquisition length calculation.     If you set the NIWLANA_OFDM_DEMOD_HEADER_DETECTION_ENABLED attribute to NIWLANA_VAL_FALSE, the toolkit uses the value of the NIWLANA_GUARD_INTERVAL attribute as the guard     interval for performing orthogonal frequency division multiplexing (OFDM) demodulation measurements.
        ///      Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/n     license and set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        ///     The default value is 800n. Valid values are 800n and 400n. 
        /// 
        /// </summary>
        public int GetGuardInterval(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.GuardInterval, channel, out value);
        }

        /// <summary>
        /// Specifies whether to use an I/Q power-edge reference trigger. If you use another mechanism to    determine when to acquire the data, such as a digital signal exported by a device under test (DUT) at the start of    the packet, set this attribute to NIWLANA_VAL_FALSE and configure the appropriate triggering in NI-RFSA.    If you enable any spectral measurements, NI-RFSA performs spectral acquisition    and ignores this attribute.
        ///     The default value is NIWLANA_VAL_TRUE.        
        /// 
        /// </summary>
        [Obsolete]
        public int SetIqPowerEdgeReferenceTriggerEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.IqPowerEdgeReferenceTriggerEnabled, channel, value);
        }

        /// <summary>
        /// Specifies whether to use an I/Q power-edge reference trigger. If you use another mechanism to    determine when to acquire the data, such as a digital signal exported by a device under test (DUT) at the start of    the packet, set this attribute to NIWLANA_VAL_FALSE and configure the appropriate triggering in NI-RFSA.    If you enable any spectral measurements, NI-RFSA performs spectral acquisition    and ignores this attribute.
        ///     The default value is NIWLANA_VAL_TRUE.        
        /// 
        /// </summary>
        [Obsolete]
        public int GetIqPowerEdgeReferenceTriggerEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.IqPowerEdgeReferenceTriggerEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies the expected average power level, in dBm, of the incoming signal at the input of the RF    signal analyzer excluding any interframe spacing. Set this attribute equal to the     sum of the average transmission power of the waveform and the maximum expected     peak-to-average power ratio (PAPR) of the incoming signal.
        ///     If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must     use an active channel string to configure this attribute.
        ///     Note: If you do not know what power level to set, use the niWLANA_RFSAAutoRange function.
        ///     Tip: Measurements might not be accurate if the incoming burst has an average     power of less than -30 dBm. Consider using a preamplifier to better use the dynamic range of the signal analyzer.
        ///     The default value is 0.          
        /// 
        /// </summary>
        [Obsolete]
        public int SetMaxInputPower(string channel, double value)
        {
            return SetDouble(niWLANAProperties.MaxInputPower, channel, value);
        }

        /// <summary>
        /// Specifies the expected average power level, in dBm, of the incoming signal at the input of the RF    signal analyzer excluding any interframe spacing. Set this attribute equal to the     sum of the average transmission power of the waveform and the maximum expected     peak-to-average power ratio (PAPR) of the incoming signal.
        ///     If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must     use an active channel string to configure this attribute.
        ///     Note: If you do not know what power level to set, use the niWLANA_RFSAAutoRange function.
        ///     Tip: Measurements might not be accurate if the incoming burst has an average     power of less than -30 dBm. Consider using a preamplifier to better use the dynamic range of the signal analyzer.
        ///     The default value is 0.          
        /// 
        /// </summary>
        [Obsolete]
        public int GetMaxInputPower(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.MaxInputPower, channel, out value);
        }

        /// <summary>
        /// Specifies whether to enable measurement of the maximum spectral density of the acquired    power spectrum in W/MHz.
        ///     The default value is NIWLANA_VAL_FALSE.        
        /// 
        /// </summary>
        public int SetMaxSpectralDensityEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.MaxSpectralDensityEnabled, channel, value);
        }

        /// <summary>
        /// Specifies whether to enable measurement of the maximum spectral density of the acquired    power spectrum in W/MHz.
        ///     The default value is NIWLANA_VAL_FALSE.        
        /// 
        /// </summary>
        public int GetMaxSpectralDensityEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.MaxSpectralDensityEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies the number of iterations over which the toolkit averages the maximum spectral    density measurements.
        ///     If you increase the number of averages, the toolkit provides smoother values but takes a longer time to compute the values.
        ///       The default value is 10. Valid values are 1 to 1,000 (inclusive).        
        /// 
        /// </summary>
        public int SetMaxSpectralDensityNumberOfAverages(string channel, int value)
        {
            return SetInt(niWLANAProperties.MaxSpectralDensityNumberOfAverages, channel, value);
        }

        /// <summary>
        /// Specifies the number of iterations over which the toolkit averages the maximum spectral    density measurements.
        ///     If you increase the number of averages, the toolkit provides smoother values but takes a longer time to compute the values.
        ///       The default value is 10. Valid values are 1 to 1,000 (inclusive).        
        /// 
        /// </summary>
        public int GetMaxSpectralDensityNumberOfAverages(string channel, out int value)
        {
            return GetInt(niWLANAProperties.MaxSpectralDensityNumberOfAverages, channel, out value);
        }

        /// <summary>
        /// Specifies the modulation and coding scheme (MCS) index. Each index represents the modulation and coding     used on the payload, according to section 20.6 of IEEE Standard 802.11n-2009. The value of the MCS index is     used in manual header detection and acquisition length calculation. If you set the NIWLANA_DSSS_DEMOD_HEADER_DETECTION_ENABLED    attribute to NIWLANA_VAL_FALSE, the toolkit uses the value of the NIWLANA_MCS_INDEX attribute as the MCS index for performing orthogonal     frequency division multiplexing (OFDM) demodulation measurements.
        ///     Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/n     license and set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        ///     The default value is 0. Valid values are 0 to 31, inclusive.    
        /// 
        /// </summary>
        public int SetMcsIndex(string channel, int value)
        {
            return SetInt(niWLANAProperties.McsIndex, channel, value);
        }

        /// <summary>
        /// Specifies the modulation and coding scheme (MCS) index. Each index represents the modulation and coding     used on the payload, according to section 20.6 of IEEE Standard 802.11n-2009. The value of the MCS index is     used in manual header detection and acquisition length calculation. If you set the NIWLANA_DSSS_DEMOD_HEADER_DETECTION_ENABLED    attribute to NIWLANA_VAL_FALSE, the toolkit uses the value of the NIWLANA_MCS_INDEX attribute as the MCS index for performing orthogonal     frequency division multiplexing (OFDM) demodulation measurements.
        ///     Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/n     license and set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        ///     The default value is 0. Valid values are 0 to 31, inclusive.    
        /// 
        /// </summary>
        public int GetMcsIndex(string channel, out int value)
        {
            return GetInt(niWLANAProperties.McsIndex, channel, out value);
        }

        /// <summary>
        /// Specifies the number of extension spatial streams that is used in manual header detection and acquisition     length calculation. If you set the NIWLANA_DSSS_DEMOD_HEADER_DETECTION_ENABLED attribute to NIWLANA_VAL_FALSE, the toolkit uses the value     of the NIWLANA_NUMBER_OF_EXTENSION_SPATIAL_STREAMS attribute as the number of spatial streams for performing     orthogonal frequency division multiplexing (OFDM) demodulation measurements.
        ///      Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/n     license and set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        ///     The default value is 0. Valid values are 0 to 3, inclusive.  
        /// 
        /// </summary>
        public int SetNumberOfExtensionSpatialStreams(string channel, int value)
        {
            return SetInt(niWLANAProperties.NumberOfExtensionSpatialStreams, channel, value);
        }

        /// <summary>
        /// Specifies the number of extension spatial streams that is used in manual header detection and acquisition     length calculation. If you set the NIWLANA_DSSS_DEMOD_HEADER_DETECTION_ENABLED attribute to NIWLANA_VAL_FALSE, the toolkit uses the value     of the NIWLANA_NUMBER_OF_EXTENSION_SPATIAL_STREAMS attribute as the number of spatial streams for performing     orthogonal frequency division multiplexing (OFDM) demodulation measurements.
        ///      Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/n     license and set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        ///     The default value is 0. Valid values are 0 to 3, inclusive.  
        /// 
        /// </summary>
        public int GetNumberOfExtensionSpatialStreams(string channel, out int value)
        {
            return GetInt(niWLANAProperties.NumberOfExtensionSpatialStreams, channel, out value);
        }

        /// <summary>
        /// For I/Q measurements, this attribute returns the maximum of the    NIWLANA_SPECTRAL_MASK_NUMBER_OF_AVERAGES attribute for all enabled measurements.    For spectral measurements, this attribute returns 1, which indicates the number of    times the measurement loop must run for all enabled measurements.
        /// 
        /// </summary>
        [Obsolete]
        public int GetNumberOfIterations(string channel, out int value)
        {
            return GetInt(niWLANAProperties.NumberOfIterations, channel, out value);
        }

        /// <summary>
        /// Specifies the number of channels (waveforms) to process during analysis measurements.    The number of receive channels must be less than or equal to the number of channels acquired.    You can process fewer number of waveforms than acquired waveforms using the numberOfReceiveChannels parameter.
        ///     Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/n     license and set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        ///      Tip: Set the numberOfReceiveChannels parameter equal to number of transmit antennas of the device under test (DUT).
        ///     The default value is 1. Valid value	s are 1 to 4, inclusive.  
        /// 
        /// </summary>
        public int GetNumberOfReceiveChannels(string channel, out int value)
        {
            return GetInt(niWLANAProperties.NumberOfReceiveChannels, channel, out value);
        }

        /// <summary>
        /// Specifies whether to enable occupied bandwidth (OBW) measurement.
        ///     Set this attribute to NIWLANA_VAL_TRUE to enable the following measurements:    occupied bandwidth (OBW), OBW high frequency,    and OBW low frequency.
        ///     The default value is NIWLANA_VAL_FALSE.        
        /// 
        /// </summary>
        public int SetObwEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.ObwEnabled, channel, value);
        }

        /// <summary>
        /// Specifies whether to enable occupied bandwidth (OBW) measurement.
        ///     Set this attribute to NIWLANA_VAL_TRUE to enable the following measurements:    occupied bandwidth (OBW), OBW high frequency,    and OBW low frequency.
        ///     The default value is NIWLANA_VAL_FALSE.        
        /// 
        /// </summary>
        public int GetObwEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.ObwEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies the number of iterations over which the toolkit averages occupied bandwidth    (OBW) measurements.
        ///     If you increase the number of averages, the toolkit provides smoother values but takes a longer time to compute the values.
        ///     The default value is 10. Valid values are 1 to 1,000 (inclusive).        
        /// 
        /// </summary>
        public int SetObwNumberOfAverages(string channel, int value)
        {
            return SetInt(niWLANAProperties.ObwNumberOfAverages, channel, value);
        }

        /// <summary>
        /// Specifies the number of iterations over which the toolkit averages occupied bandwidth    (OBW) measurements.
        ///     If you increase the number of averages, the toolkit provides smoother values but takes a longer time to compute the values.
        ///     The default value is 10. Valid values are 1 to 1,000 (inclusive).        
        /// 
        /// </summary>
        public int GetObwNumberOfAverages(string channel, out int value)
        {
            return GetInt(niWLANAProperties.ObwNumberOfAverages, channel, out value);
        }

        /// <summary>
        /// Specifies the payload data rate, in Mbps, of the expected incoming signal, as defined in section    17.3.2.2 of IEEE Standard 802.11a-1999. If you set the    NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211AG_OFDM or    NIWLANA_VAL_STANDARD_80211G_DSSS_OFDM and you have not configured    an acquisition length, the toolkit uses NIWLANA_OFDM_DATA_RATE attribute to calculate the desired acquisition length.    If you set the NIWLANA_OFDM_HEADER_DETECTION_ENABLED attribute to NIWLANA_VAL_FALSE, the toolkit uses the value of NIWLANA_OFDM_DATA_RATE     attribute as the data rate for performing OFDM demodulation measurements.
        ///     Note: The toolkit ignores this attribute if you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.    The default value is NIWLANA_VAL_OFDM_DATA_RATE_6.    
        /// 
        /// </summary>
        public int SetOfdmDataRate(string channel, int value)
        {
            return SetInt(niWLANAProperties.OfdmDataRate, channel, value);
        }

        /// <summary>
        /// Specifies the payload data rate, in Mbps, of the expected incoming signal, as defined in section    17.3.2.2 of IEEE Standard 802.11a-1999. If you set the    NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211AG_OFDM or    NIWLANA_VAL_STANDARD_80211G_DSSS_OFDM and you have not configured    an acquisition length, the toolkit uses NIWLANA_OFDM_DATA_RATE attribute to calculate the desired acquisition length.    If you set the NIWLANA_OFDM_HEADER_DETECTION_ENABLED attribute to NIWLANA_VAL_FALSE, the toolkit uses the value of NIWLANA_OFDM_DATA_RATE     attribute as the data rate for performing OFDM demodulation measurements.
        ///     Note: The toolkit ignores this attribute if you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.    The default value is NIWLANA_VAL_OFDM_DATA_RATE_6.    
        /// 
        /// </summary>
        public int GetOfdmDataRate(string channel, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDataRate, channel, out value);
        }

        /// <summary>
        /// Specifies whether to detect the frame format of the 802.11n burst automatically.     If this attribute is disabled, you must set the frame format using the NIWLANA_80211N_PLCP_FRAME_FORMAT attribute.
        ///     Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/n     license and set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        ///     The default value is NIWLANA_VAL_TRUE. 
        /// 
        /// </summary>
        public int SetOfdmDemod80211nPlcpFrameDetectionEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.OfdmDemod80211nPlcpFrameDetectionEnabled, channel, value);
        }

        /// <summary>
        /// Specifies whether to detect the frame format of the 802.11n burst automatically.     If this attribute is disabled, you must set the frame format using the NIWLANA_80211N_PLCP_FRAME_FORMAT attribute.
        ///     Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/n     license and set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        ///     The default value is NIWLANA_VAL_TRUE. 
        /// 
        /// </summary>
        public int GetOfdmDemod80211nPlcpFrameDetectionEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemod80211nPlcpFrameDetectionEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies whether to enable pilot-based mean amplitude tracking. Amplitude tracking is useful    if the mean amplitude of the orthogonal frequency division multiplexing (OFDM) symbol varies over time. However, enabling tracking might    degrade error vector magnitude (EVM) values by approximately 1 to 2 dB because of attempts to    track random amplitude distortions caused by noise and residual intercarrier interference from    symbol to symbol.
        ///     The default value is NIWLANA_VAL_FALSE.        
        /// 
        /// </summary>
        public int SetOfdmDemodAmplitudeTrackingEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.OfdmDemodAmplitudeTrackingEnabled, channel, value);
        }

        /// <summary>
        /// Specifies whether to enable pilot-based mean amplitude tracking. Amplitude tracking is useful    if the mean amplitude of the orthogonal frequency division multiplexing (OFDM) symbol varies over time. However, enabling tracking might    degrade error vector magnitude (EVM) values by approximately 1 to 2 dB because of attempts to    track random amplitude distortions caused by noise and residual intercarrier interference from    symbol to symbol.
        ///     The default value is NIWLANA_VAL_FALSE.        
        /// 
        /// </summary>
        public int GetOfdmDemodAmplitudeTrackingEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodAmplitudeTrackingEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies how the toolkit estimates the channel frequency response.    When a user disables channel tracking, toolkit estimates the channel     response only over the long training sequence and uses this as channel estimate over the entire packet.     When a user enables channel tracking, toolkit estimates the channel response over preamble and data     and uses this as the channel estimate for the entire packet.
        ///     Note: In either case, toolkit does not track the instanteneous channel.
        ///     The default value is NIWLANA_VAL_FALSE.          
        /// 
        /// </summary>
        public int SetOfdmDemodChannelTrackingEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.OfdmDemodChannelTrackingEnabled, channel, value);
        }

        /// <summary>
        /// Specifies how the toolkit estimates the channel frequency response.    When a user disables channel tracking, toolkit estimates the channel     response only over the long training sequence and uses this as channel estimate over the entire packet.     When a user enables channel tracking, toolkit estimates the channel response over preamble and data     and uses this as the channel estimate for the entire packet.
        ///     Note: In either case, toolkit does not track the instanteneous channel.
        ///     The default value is NIWLANA_VAL_FALSE.          
        /// 
        /// </summary>
        public int GetOfdmDemodChannelTrackingEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodChannelTrackingEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies whether to enable constellation trace for the payload portion of 802.11a,    802.11g orthogonal frequency division multiplexing (OFDM), or 802.11g direct sequence spread spectrum-OFDM (DSSS-OFDM), or or 802.11n signals.
        ///     If you set the standard parameter of the niWLANA_SetStandard function to     NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use the channelx active channel string to configure this attribute.
        ///     The default value is NIWLANA_VAL_FALSE.        
        /// 
        /// </summary>
        public int SetOfdmDemodConstellationTraceEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.OfdmDemodConstellationTraceEnabled, channel, value);
        }

        /// <summary>
        /// Specifies whether to enable constellation trace for the payload portion of 802.11a,    802.11g orthogonal frequency division multiplexing (OFDM), or 802.11g direct sequence spread spectrum-OFDM (DSSS-OFDM), or or 802.11n signals.
        ///     If you set the standard parameter of the niWLANA_SetStandard function to     NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use the channelx active channel string to configure this attribute.
        ///     The default value is NIWLANA_VAL_FALSE.        
        /// 
        /// </summary>
        public int GetOfdmDemodConstellationTraceEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodConstellationTraceEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies whether to enable demodulation-based measurements for 802.11a-1999, 802.11g-2003 orthogonal     frequency division multiplexing (OFDM), 802.11g-2003 direct sequence spread spectrum-orthogonal     frequency division multiplexing (DSSS-OFDM), or 802.11n-2009 signals.
        ///     The default value is NIWLANA_VAL_FALSE.          
        /// 
        /// </summary>
        public int SetOfdmDemodEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.OfdmDemodEnabled, channel, value);
        }

        /// <summary>
        /// Specifies whether to enable demodulation-based measurements for 802.11a-1999, 802.11g-2003 orthogonal     frequency division multiplexing (OFDM), 802.11g-2003 direct sequence spread spectrum-orthogonal     frequency division multiplexing (DSSS-OFDM), or 802.11n-2009 signals.
        ///     The default value is NIWLANA_VAL_FALSE.          
        /// 
        /// </summary>
        public int GetOfdmDemodEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies whether to enable EVM per subcarrier trace for 802.11a, 802.11g orthogonal frequency division multiplexing (OFDM),    802.11g direct sequence spread spectrum-OFDM (DSSS-OFDM), or 802.11n signals.
        ///      The default value is NIWLANA_VAL_FALSE.    
        /// 
        /// </summary>
        public int SetOfdmDemodEvmPerSubcarrierTraceEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.OfdmDemodEvmPerSubcarrierTraceEnabled, channel, value);
        }

        /// <summary>
        /// Specifies whether to enable EVM per subcarrier trace for 802.11a, 802.11g orthogonal frequency division multiplexing (OFDM),    802.11g direct sequence spread spectrum-OFDM (DSSS-OFDM), or 802.11n signals.
        ///      The default value is NIWLANA_VAL_FALSE.    
        /// 
        /// </summary>
        public int GetOfdmDemodEvmPerSubcarrierTraceEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodEvmPerSubcarrierTraceEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies whether to enable EVM per symbol per subcarrier trace for 802.11a, 802.11g orthogonal frequency division multiplexing (OFDM),    802.11g direct sequence spread spectrum-OFDM (DSSS-OFDM), or 802.11n signals.
        ///     The default value is NIWLANA_VAL_FALSE.  
        /// 
        /// </summary>
        public int SetOfdmDemodEvmPerSymbolPerSubcarrierTraceEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.OfdmDemodEvmPerSymbolPerSubcarrierTraceEnabled, channel, value);
        }

        /// <summary>
        /// Specifies whether to enable EVM per symbol per subcarrier trace for 802.11a, 802.11g orthogonal frequency division multiplexing (OFDM),    802.11g direct sequence spread spectrum-OFDM (DSSS-OFDM), or 802.11n signals.
        ///     The default value is NIWLANA_VAL_FALSE.  
        /// 
        /// </summary>
        public int GetOfdmDemodEvmPerSymbolPerSubcarrierTraceEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodEvmPerSymbolPerSubcarrierTraceEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies whether to enable EVM per symbol trace for 802.11a, 802.11g orthogonal frequency division multiplexing (OFDM),    802.11g direct sequence spread spectrum-OFDM (DSSS-OFDM), or 802.11n signals.
        ///      The default value is NIWLANA_VAL_FALSE.   
        /// 
        /// </summary>
        public int SetOfdmDemodEvmPerSymbolTraceEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.OfdmDemodEvmPerSymbolTraceEnabled, channel, value);
        }

        /// <summary>
        /// Specifies whether to enable EVM per symbol trace for 802.11a, 802.11g orthogonal frequency division multiplexing (OFDM),    802.11g direct sequence spread spectrum-OFDM (DSSS-OFDM), or 802.11n signals.
        ///      The default value is NIWLANA_VAL_FALSE.   
        /// 
        /// </summary>
        public int GetOfdmDemodEvmPerSymbolTraceEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodEvmPerSymbolTraceEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies whether to enable time-gated power measurements.
        ///     If you set the standard parameter of the niWLANA_SetStandard function to     NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use an channelx active channel string to configure this attribute.
        /// 
        /// </summary>
        public int SetOfdmDemodGatedPowerEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.OfdmDemodGatedPowerEnabled, channel, value);
        }

        /// <summary>
        /// Specifies whether to enable time-gated power measurements.
        ///     If you set the standard parameter of the niWLANA_SetStandard function to     NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use an channelx active channel string to configure this attribute.
        /// 
        /// </summary>
        public int GetOfdmDemodGatedPowerEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodGatedPowerEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies the start time for computing gated power, in seconds. The value of 0    indicates the start time is the start of the payload.
        ///     A negative value indicates a position in the preamble or header.    Refer to the Gated Power topic for more information.
        ///     The default value is 0.        
        /// 
        /// </summary>
        public int SetOfdmDemodGatedPowerStartTime(string channel, double value)
        {
            return SetDouble(niWLANAProperties.OfdmDemodGatedPowerStartTime, channel, value);
        }

        /// <summary>
        /// Specifies the start time for computing gated power, in seconds. The value of 0    indicates the start time is the start of the payload.
        ///     A negative value indicates a position in the preamble or header.    Refer to the Gated Power topic for more information.
        ///     The default value is 0.        
        /// 
        /// </summary>
        public int GetOfdmDemodGatedPowerStartTime(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.OfdmDemodGatedPowerStartTime, channel, out value);
        }

        /// <summary>
        /// Specifies the end time for computing gated power, in seconds. The value of 0    indicates the stop time is the start of the payload.
        ///     A negative value indicates a position in the preamble or header.    Refer to the Gated Power topic for more information.
        ///     The default value is 64 microseconds.        
        /// 
        /// </summary>
        public int SetOfdmDemodGatedPowerStopTime(string channel, double value)
        {
            return SetDouble(niWLANAProperties.OfdmDemodGatedPowerStopTime, channel, value);
        }

        /// <summary>
        /// Specifies the end time for computing gated power, in seconds. The value of 0    indicates the stop time is the start of the payload.
        ///     A negative value indicates a position in the preamble or header.    Refer to the Gated Power topic for more information.
        ///     The default value is 64 microseconds.        
        /// 
        /// </summary>
        public int GetOfdmDemodGatedPowerStopTime(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.OfdmDemodGatedPowerStopTime, channel, out value);
        }

        /// <summary>
        /// Specifies whether to enable automatic detection of the physical layer (PHY) header.
        ///    The default value is NIWLANA_VAL_TRUE.        
        /// 
        /// </summary>
        public int SetOfdmDemodHeaderDetectionEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.OfdmDemodHeaderDetectionEnabled, channel, value);
        }

        /// <summary>
        /// Specifies whether to enable automatic detection of the physical layer (PHY) header.
        ///    The default value is NIWLANA_VAL_TRUE.        
        /// 
        /// </summary>
        public int GetOfdmDemodHeaderDetectionEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodHeaderDetectionEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies whether to do low pass filtering after acquiring the signal in baseband to remove out of band noise.     The default value is NIWLANA_VAL_TRUE. 
        /// 
        /// </summary>
        public int SetOfdmDemodLowpassFilteringEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.OfdmDemodLowpassFilteringEnabled, channel, value);
        }

        /// <summary>
        /// Specifies whether to do low pass filtering after acquiring the signal in baseband to remove out of band noise.     The default value is NIWLANA_VAL_TRUE. 
        /// 
        /// </summary>
        public int GetOfdmDemodLowpassFilteringEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodLowpassFilteringEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies the maximum number of symbols to be used for error vector magnitude (EVM)    computation. You must set this attribute to the correct value to optimize measurement speed.    If the number of available payload symbols (n) is higher than the value    you specify (m), the toolkit ignores (n-m) symbols from the end of the packet.
        ///     The default value is 16.          
        /// 
        /// </summary>
        public int SetOfdmDemodMaximumSymbolsUsed(string channel, int value)
        {
            return SetInt(niWLANAProperties.OfdmDemodMaximumSymbolsUsed, channel, value);
        }

		public int GetOfdmDemodAutoComputeAcquisitionLengthEnabled(string channel, out int value)
        {
            return TestForError(PInvoke.niWLANA_GetOFDMDemodAutoComputeAcquisitionLengthEnabled(Handle, channel, out value));
        }

        public int SetOfdmDemodAutoComputeAcquisitionLengthEnabled(string channel, int value)
        {
            return TestForError(PInvoke.niWLANA_SetOFDMDemodAutoComputeAcquisitionLengthEnabled(Handle, channel, value));
        }

        public int GetDSSSDemodAutoComputeAcquisitionLengthEnabled(string channel, out int value)
        {
            return TestForError(PInvoke.niWLANA_GetDSSSDemodAutoComputeAcquisitionLengthEnabled(Handle, channel, out value));
        }

        public int SetDSSSDemodAutoComputeAcquisitionLengthEnabled(string channel, int value)
        {
            return TestForError(PInvoke.niWLANA_SetDSSSDemodAutoComputeAcquisitionLengthEnabled(Handle, channel, value));
        }
		
        /// <summary>
        /// Specifies the maximum number of symbols to be used for error vector magnitude (EVM)    computation. You must set this attribute to the correct value to optimize measurement speed.    If the number of available payload symbols (n) is higher than the value    you specify (m), the toolkit ignores (n-m) symbols from the end of the packet.
        ///     The default value is 16.          
        /// 
        /// </summary>
        public int GetOfdmDemodMaximumSymbolsUsed(string channel, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodMaximumSymbolsUsed, channel, out value);
        }

        /// <summary>
        /// Specifies the number of orthogonal frequency division multiplexing (OFDM) symbols in the payload to remove before starting error vector    magnitude (EVM) computation.
        ///     The default value is 0.        
        /// 
        /// </summary>
        public int SetOfdmDemodMeasurementStartLocation(string channel, int value)
        {
            return SetInt(niWLANAProperties.OfdmDemodMeasurementStartLocation, channel, value);
        }

        /// <summary>
        /// Specifies the number of orthogonal frequency division multiplexing (OFDM) symbols in the payload to remove before starting error vector    magnitude (EVM) computation.
        ///     The default value is 0.        
        /// 
        /// </summary>
        public int GetOfdmDemodMeasurementStartLocation(string channel, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodMeasurementStartLocation, channel, out value);
        }
        /// <summary>
        /// Specifies the number of iterations over which the toolkit averages orthogonal frequency division multiplexing (OFDM)    demodulation-based measurements.
        ///     If you increase the number of averages, the toolkit provides smoother values but takes a longer time to compute the values.
        ///     The default value is 1. Valid values are 1 to 1,000 (inclusive).        
        /// 
        /// </summary>
        public int SetOfdmDemodNumberOfAverages(string channel, int value)
        {
            return SetInt(niWLANAProperties.OfdmDemodNumberOfAverages, channel, value);
        }
        /// <summary>
        /// Specifies the number of iterations over which the toolkit averages orthogonal frequency division multiplexing (OFDM)    demodulation-based measurements.
        ///     If you increase the number of averages, the toolkit provides smoother values but takes a longer time to compute the values.
        ///     The default value is 1. Valid values are 1 to 1,000 (inclusive).        
        /// 
        /// </summary>
        public int GetOfdmDemodNumberOfAverages(string channel, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodNumberOfAverages, channel, out value);
        }

        /// <summary>
        /// Specifies which type of phase tracking to enable. Phase tracking is useful for tracking the phase    variation over the modulation symbol caused by residual frequency offset and phase noise.
        ///    If you select NIWLAN_VAL_OFDM_PHASE_TRACKING_STANDARD, the toolkit performs pilot-based common phase    error correction over the orthogonal frequency division multiplexing (OFDM) symbol, as mandated in section 17.3.9.7 of IEEE Standard 802.11a-1999.    and section 20.3.21.7.4 of IEEE Standard 802.11n-2009.    If you select NIWLAN_VAL_OFDM_PHASE_TRACKING_INSTANTANEOUS, the toolkit also compensates for the    phase distortion in each modulation symbol and computes only the magnitude error vector magnitude (EVM), which is the error    due to variation in magnitude of the complex modulation symbol over different subcarriers and the    length of the packet. Such compensation is not defined in the IEEE standard. However, the     compensation is useful for determining the modulation distortion in the amplitude and the    contribution of phase errors.
        ///     The default value is NIWLAN_VAL_OFDM_PHASE_TRACKING_STANDARD.  
        /// 
        /// </summary>
        public int SetOfdmDemodPhaseTracking(string channel, int value)
        {
            return SetInt(niWLANAProperties.OfdmDemodPhaseTracking, channel, value);
        }

        /// <summary>
        /// Specifies which type of phase tracking to enable. Phase tracking is useful for tracking the phase    variation over the modulation symbol caused by residual frequency offset and phase noise.
        ///    If you select NIWLAN_VAL_OFDM_PHASE_TRACKING_STANDARD, the toolkit performs pilot-based common phase    error correction over the orthogonal frequency division multiplexing (OFDM) symbol, as mandated in section 17.3.9.7 of IEEE Standard 802.11a-1999.    and section 20.3.21.7.4 of IEEE Standard 802.11n-2009.    If you select NIWLAN_VAL_OFDM_PHASE_TRACKING_INSTANTANEOUS, the toolkit also compensates for the    phase distortion in each modulation symbol and computes only the magnitude error vector magnitude (EVM), which is the error    due to variation in magnitude of the complex modulation symbol over different subcarriers and the    length of the packet. Such compensation is not defined in the IEEE standard. However, the     compensation is useful for determining the modulation distortion in the amplitude and the    contribution of phase errors.
        ///     The default value is NIWLAN_VAL_OFDM_PHASE_TRACKING_STANDARD.  
        /// 
        /// </summary>
        public int GetOfdmDemodPhaseTracking(string channel, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodPhaseTracking, channel, out value);
        }

        /// <summary>
        /// Specifies whether to enable compensation for sample clock frequency offset.
        ///    Although IEEE Standard 802.11a-1999 requires the clock and the carrier signal to be derived    from the same source and locked, this attribute does not make this assumption. Although the    estimation of the clock offset occurs over a maximum 100 orthogonal frequency division multiplexing (OFDM) symbols in the frequency domain,    the toolkit compensates in the time domain to ensure that clock cycle slips can be adjusted.
        ///     The default value is NIWLANA_VAL_TRUE.        
        /// 
        /// </summary>
        public int SetOfdmDemodTimeTrackingEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.OfdmDemodTimeTrackingEnabled, channel, value);
        }

        /// <summary>
        /// Specifies whether to enable compensation for sample clock frequency offset.
        ///    Although IEEE Standard 802.11a-1999 requires the clock and the carrier signal to be derived    from the same source and locked, this attribute does not make this assumption. Although the    estimation of the clock offset occurs over a maximum 100 orthogonal frequency division multiplexing (OFDM) symbols in the frequency domain,    the toolkit compensates in the time domain to ensure that clock cycle slips can be adjusted.
        ///     The default value is NIWLANA_VAL_TRUE.        
        /// 
        /// </summary>
        public int GetOfdmDemodTimeTrackingEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodTimeTrackingEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies the payload length, in bytes, of the expected incoming signal. If you set the    NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211AG_OFDM or    NIWLANA_VAL_STANDARD_80211G_DSSS_OFDM and you have not configured an    acquisition length, the toolkit uses NIWLANA_OFDM_PAYLOAD_LENGTH attribute to calculate the desired acquisition length.    If you set the NIWLANA_OFDM_HEADER_DETECTION_ENABLED attribute to NIWLANA_VAL_FALSE,    the toolkit uses the value of NIWLANA_OFDM_PAYLOAD_LENGTH attribute    as the payload length    for performing OFDM demodulation measurements.
        ///     For IEEE Standard 802.11n-2009 signals, this attribute specifies the payload length per stream.
        ///     If the NIWLANA_STANDARD attribute is set to NIWLANA_VAL_STANDARD_80211AG_OFDM or NIWLANA_VAL_STANDARD_80211G_DSSS_OFDM, the default value is 1,024.     If the NIWLANA_STANDARD attribute is set to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, the default value is 4,096.     
        /// 
        /// </summary>
        public int SetOfdmPayloadLength(string channel, int value)
        {
            return SetInt(niWLANAProperties.OfdmPayloadLength, channel, value);
        }

        /// <summary>
        /// Specifies the payload length, in bytes, of the expected incoming signal. If you set the    NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211AG_OFDM or    NIWLANA_VAL_STANDARD_80211G_DSSS_OFDM and you have not configured an    acquisition length, the toolkit uses NIWLANA_OFDM_PAYLOAD_LENGTH attribute to calculate the desired acquisition length.    If you set the NIWLANA_OFDM_HEADER_DETECTION_ENABLED attribute to NIWLANA_VAL_FALSE,    the toolkit uses the value of NIWLANA_OFDM_PAYLOAD_LENGTH attribute    as the payload length    for performing OFDM demodulation measurements.
        ///     For IEEE Standard 802.11n-2009 signals, this attribute specifies the payload length per stream.
        ///     If the NIWLANA_STANDARD attribute is set to NIWLANA_VAL_STANDARD_80211AG_OFDM or NIWLANA_VAL_STANDARD_80211G_DSSS_OFDM, the default value is 1,024.     If the NIWLANA_STANDARD attribute is set to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, the default value is 4,096.     
        /// 
        /// </summary>
        public int GetOfdmPayloadLength(string channel, out int value)
        {
            return GetInt(niWLANAProperties.OfdmPayloadLength, channel, out value);
        }

        /// <summary>
        /// Specifies the resolution bandwidth (RBW), in hertz (Hz), for spectral acquisition.
        ///    The default value is 100 KHz. The IEEE Standard 802.11a-1999 specifies a RBW of 100 KHz    and a video bandwidth (VBW) of 30 KHz for spectral mask measurement.    To emulate this effect, if you set the NIWLANA_STANDARD attribute to    NIWLANA_VAL_STANDARD_80211AG_OFDM, NIWLANA_VAL_STANDARD_80211G_DSSS_OFDM, or NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM     the toolkit multiplies the number of spectral averages by three.        
        /// 
        /// </summary>
        public int SetRbw(string channel, double value)
        {
            return SetDouble(niWLANAProperties.Rbw, channel, value);
        }

        /// <summary>
        /// Specifies the resolution bandwidth (RBW), in hertz (Hz), for spectral acquisition.
        ///    The default value is 100 KHz. The IEEE Standard 802.11a-1999 specifies a RBW of 100 KHz    and a video bandwidth (VBW) of 30 KHz for spectral mask measurement.    To emulate this effect, if you set the NIWLANA_STANDARD attribute to    NIWLANA_VAL_STANDARD_80211AG_OFDM, NIWLANA_VAL_STANDARD_80211G_DSSS_OFDM, or NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM     the toolkit multiplies the number of spectral averages by three.        
        /// 
        /// </summary>
        public int GetRbw(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.Rbw, channel, out value);
        }

        /// <summary>
        /// Specifies the definition of the resolution bandwidth (RBW) parameter.    
        /// 
        /// </summary>
        public int SetRbwDefinition(string channel, int value)
        {
            return SetInt(niWLANAProperties.RbwDefinition, channel, value);
        }

        /// <summary>
        /// Specifies the definition of the resolution bandwidth (RBW) parameter.    
        /// 
        /// </summary>
        public int GetRbwDefinition(string channel, out int value)
        {
            return GetInt(niWLANAProperties.RbwDefinition, channel, out value);
        }

        /// <summary>
        /// Returns the acquisition length measured by the niWLANA_RFSAAutoRange function.     The niWLANA_RFSAAutoRange function measures the length of the burst and returns the recommended acquisition length to be used     while performing successful measurements. Use this value to set the NIWLANA_ACQUISITION_LENGTH attribute on the niwlan analysis session.    
        /// 
        /// </summary>
        public int GetResultAutorangeAcquisitionLength(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultAutorangeAcquisitionLength, channel, out value);
        }

        /// <summary>
        /// Returns the peak power of the burst as calculated by the niWLANA_RFSAAutoRange function.     Use this value to set the NIWLANA_MAX_INPUT_POWER attribute.    
        /// 
        /// </summary>
        public int GetResultAutorangeMaxInputPower(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultAutorangeMaxInputPower, channel, out value);
        }

        /// <summary>
        /// Returns, as a percentage, the peak value of the chip error vector magnitude (EVM) for the first 1,000 chips in the    payload computed according to section 18.4.7.8 of IEEE Standard 802.11b-1999.
        ///     Note: For DSSS demodulation, if equalization is not enabled and the reference pulse-shaping filter     type and filter coefficients do not match the filter    configuration of the DUT/generator, you might notice EVM degradation.     Ensure the pulse-shaping type and pulse-shaping coefficient settings match    the input signal settings.
        ///     Note: If the pulse-shaping filter coefficient is less than 0.2, DSSS EVM might show degradation.
        ///     The toolkit broadly follows section 18.4.7.8 of IEEE Standard 802.11b-1999 to compute the    root mean square (RMS) value. The standard calls for EVM computation only on the differential quadrature phase shift keying (DQPSK) signal.    However, the toolkit computes EVM for all compulsory and optional data rates and modulation schemes defined    for IEEE Standard 802.11b-1999, as well as the extended rate physical layer-packet binary convolutional coding (ERP-PBCC) modes defined in IEEE Standard 802.11g.    Refer to the EVM Differentiation topic for an explanation of    the difference between RMS EVM, peak EVM, and 802.11b-1999 peak EVM, respectively.    Note: For DSSS demodulation, if equalization is not enabled and the reference pulse-shaping filter type and filter coefficients     do not match the filter configuration of the DUT/generator, you might notice EVM degradation. Ensure the pulse-shaping     type and pulse-shaping coefficient settings match the input signal settings.
        /// 
        /// </summary>
        public int GetResultDsssDemod80211bPeakEvm(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultDsssDemod80211bPeakEvm, channel, out value);
        }

        /// <summary>
        /// Returns the average gated power, in dBm, defined by the measurement interval between the     NIWLANA_DSSS_GATED_POWER_START_TIME and the NIWLANA_DSSS_GATED_POWER_STOP_TIME attributes.        
        /// 
        /// </summary>
        public int GetResultDsssDemodAverageGatedPower(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultDsssDemodAverageGatedPower, channel, out value);
        }

        /// <summary>
        /// Returns the estimated carrier frequency offset, in hertz (Hz), of the transmitting DUT.    For example, if you set the NIWLANA_CARRIER_FREQUENCY attribute to 2.412 GHz and the toolkit    measures the carrier frequency of the DUT to be 2.413 GHz, the carrier frequency offset    is 1 MHz. This measurement follows section 17.3.9.4 of IEEE Standard 802.11a-1999 and section 20.3.21.4 of IEEE Standard 802.11n-2009.        
        /// 
        /// </summary>
        public int GetResultDsssDemodCarrierFrequencyOffset(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultDsssDemodCarrierFrequencyOffset, channel, out value);
        }

        /// <summary>
        /// Returns the ratio, in dB, of the DC offset to the mean amplitude of the burst.
        ///    The toolkit computes the ratio as the normalized DC offset and not according to section 18.4.7.7    of IEEE Standard 802.11b-1999, where the definition is valid only for unmodulated    continuous wave (CW) signals.        
        /// 
        /// </summary>
        public int GetResultDsssDemodCarrierSuppression(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultDsssDemodCarrierSuppression, channel, out value);
        }

        /// <summary>
        /// Returns the data rate, in Mbps, used to transmit the SERVICE field and the physical layer convergence procedure protocol data unit (PPDU). If you set the    NIWLANA_DSSS_DEMOD_HEADER_DETECTION_ENABLED attribute to NIWLANA_VAL_TRUE, the data rate is extracted from the demodulated    SIGNAL field of the frame.    The SIGNAL field is defined in section 17.3.4 of IEEE Standard 802.11a-1999.
        ///     If you set the NIWLANA_DSSS_DEMOD_HEADER_DETECTION_ENABLED attribute to NIWLANA_VAL_FALSE,    the value you specify in the NIWLANA_DSSS_DATA_RATE attribute specifies the data rate.        
        /// 
        /// </summary>
        public int GetResultDsssDemodDataRate(string channel, out int value)
        {
            return GetInt(niWLANAProperties.ResultDsssDemodDataRate, channel, out value);
        }

        /// <summary>
        /// Returns whether the header checksum successfully passed,    as defined in section 18.2.3.6 of IEEE Standard 802.11b-1999.        
        /// 
        /// </summary>
        public int GetResultDsssDemodHeaderChecksumPassed(string channel, out int value)
        {
            return GetInt(niWLANAProperties.ResultDsssDemodHeaderChecksumPassed, channel, out value);
        }

        /// <summary>
        /// Specifies the ratio, in dB, of the mean amplitude of the in-phase (I) signal to the mean    amplitude of the quadrature-phase (Q) signal.
        ///     Note: If the magnitude of the carrier or the Sample clock frequency offset is greater than 25 ppm,     I/Q gain imbalance magnitude is greater than 3 dB, and quadrature skew magnitude is greater than 15 degrees,     the estimates of these impairments might not be accurate.  
        /// 
        /// </summary>
        public int GetResultDsssDemodIqGainImbalance(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultDsssDemodIqGainImbalance, channel, out value);
        }

        /// <summary>
        /// Returns the length of the payload, including the medium access control (MAC) header, in bytes.    If you set the NIWLANA_DSSS_DEMOD_HEADER_DETECTION_ENABLED attribute to NIWLANA_VAL_TRUE, the toolkit extracts the length    from the demodulated SIGNAL field of the frame. The SIGNAL field is defined in    section 17.3.4 of IEEE Standard 802.11a-1999.
        ///     If you set the NIWLANA_DSSS_DEMOD_HEADER_DETECTION_ENABLED attribute to NIWLANA_VAL_FALSE,    the value you specify in the NIWLANA_DSSS_PAYLOAD_LENGTH attribute specifies the payload length.    The toolkit returns a value of -1 if it encounters various data lengths during the averaging process.        
        /// 
        /// </summary>
        public int GetResultDsssDemodPayloadLength(string channel, out int value)
        {
            return GetInt(niWLANAProperties.ResultDsssDemodPayloadLength, channel, out value);
        }

        /// <summary>
        /// Returns, as a percentage, the peak value of the chip error vector magnitude (EVM) for the first 1,000 chips     in the payload computed according to section 18.4.7.8 of IEEE Standard 802.11b-1999.
        ///     Note: For DSSS demodulation, if equalization is not enabled and the reference pulse-shaping    filter type and filter coefficients do not match the filter    configuration of the DUT/generator, you might notice EVM degradation. Ensure the pulse-shaping    type and pulse-shaping coefficient settings match the input signal settings.
        ///     Note: If the pulse-shaping filter coefficient is less than 0.2, DSSS EVM might show degradation.
        ///    The toolkit broadly follows section 18.4.7.8 of IEEE Standard 802.11b-1999    to compute the root mean square (RMS) value. The standard calls for EVM computation    only on the differential quadrature phase shift keying (DQPSK) signal. However, the toolkit computes EVM for all compulsory and optional data rates    and modulation schemes defined for IEEE Standard 802.11b-1999, as well as the extended rate physical layer-packet binary convolutional coding (ERP-PBCC) modes    defined in IEEE Standard 802.11g-2003.  Refer to the EVM Differentiation topic for an explanation of    the difference between RMS EVM, peak EVM, and 802.11b peak EVM, respectively.        
        /// 
        /// </summary>
        public int GetResultDsssDemodPeakEvm(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultDsssDemodPeakEvm, channel, out value);
        }

        /// <summary>
        /// Returns whether the detected preamble type is long and/or short.        
        /// 
        /// </summary>
        public int GetResultDsssDemodPreambleType(string channel, out int value)
        {
            return GetInt(niWLANAProperties.ResultDsssDemodPreambleType, channel, out value);
        }

        /// <summary>
        /// Returns the deviation in angles from 90 degrees between the in-phase (I)    and quadrature-phase (Q) signals.
        ///    Refer to the Quadrature Skew topic for more information.
        ///     Note: If the magnitude of the carrier or the Sample clock frequency offset is greater than 25 ppm,     I/Q gain imbalance magnitude is greater than 3 dB, and quadrature skew magnitude is greater than 15 degrees,     the estimates of these impairments might not be accurate.  
        /// 
        /// </summary>
        public int GetResultDsssDemodQuadratureSkew(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultDsssDemodQuadratureSkew, channel, out value);
        }

        /// <summary>
        /// Returns the root mean square (RMS) value of the chip error vector magnitude (EVM) as a percentage of the mean amplitude of the signal envelope    for 802.11b direct sequence spread spectrum (DSSS) signals.
        ///     Note: For DSSS demodulation, if equalization is not enabled and the reference pulse-shaping    filter type and filter coefficients do not match the filter    configuration of the DUT/generator, you might notice EVM degradation. Ensure the pulse-shaping    type and pulse-shaping coefficient settings match the input signal settings.
        ///     Note: If the pulse-shaping filter coefficient is less than 0.2, DSSS EVM might show degradation.
        ///    The toolkit broadly follows section 18.4.7.8 of IEEE Standard 802.11b-1999    to compute the root mean square (RMS) value. The standard calls for EVM computation    only on the differential quadrature phase shift keying (DQPSK) signal. However, the toolkit computes EVM for all compulsory and optional data rates    and modulation schemes defined for IEEE Standard 802.11b-1999, as well as the extended rate physical layer-packet binary convolutional coding (ERP-PBCC) modes    defined in IEEE Standard 802.11g-2003.  Refer to the EVM Differentiation topic for an explanation of    the difference between RMS EVM, peak EVM, and 802.11b peak EVM, respectively.      
        /// 
        /// </summary>
        public int GetResultDsssDemodRmsEvm(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultDsssDemodRmsEvm, channel, out value);
        }

        /// <summary>
        /// Returns the estimated sample clock offset, in parts per million (ppm).    The estimated offset is the difference between the sample clocks    at the D/A converter of the transmitting DUT and the digitizer. If the clock offset is more than 25 ppm,    the estimated value may not be accurate. Use the estimated offset to verify if the DUT corresponds     to section 18.4.7.5 of IEEE Standard 802.11b-1999 and section 20.3.21.6 of IEEE Standard 802.11n-2009.
        ///     Note: If the magnitude of the carrier or the Sample clock frequency offset is greater than 25 ppm,     I/Q gain imbalance magnitude is greater than 3 dB, and quadrature skew magnitude is greater than 15 degrees,     the estimates of these impairments might not be accurate.    
        /// 
        /// </summary>
        public int GetResultDsssDemodSampleClockOffset(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultDsssDemodSampleClockOffset, channel, out value);
        }

        /// <summary>
        /// Returns whether the toolkit successfully detected the start frame delimiter (SFD),    as defined in sections 18.2.3.2 and 18.2.3.9 of IEEE Standard 802.11b-1999.        
        /// 
        /// </summary>
        public int GetResultDsssDemodSfdFound(string channel, out int value)
        {
            return GetInt(niWLANAProperties.ResultDsssDemodSfdFound, channel, out value);
        }

        /// <summary>
        /// Returns the measured power ramp-down time, in seconds.
        ///      If you set the NIWLANA_DSSS_POWER_RAMP_DOWN_LOW_THRESHOLD    and NIWLANA_DSSS_POWER_RAMP_DOWN_HIGH_THRESHOLD attributes    to 10 percent and 90 percent, respectively, this measurement is performed    according to section 18.4.7.6 of IEEE Standard 802.11b-1999. However, the toolkit    does not define the ramp time for unmodulated step input. To remove the variations     in the instantaneous power caused by modulation on the baseband signal, the toolkit    filters the power vector using the maximum value in a moving window.
        ///     Tip: The DSSS power ramp might not be found if the ramp-up or ramp-down time     of the incoming burst is greater than 5 microseconds, in which case the toolkit returns an error.  
        /// 
        /// </summary>
        public int GetResultDsssPowerRampDownTime(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultDsssPowerRampDownTime, channel, out value);
        }

        /// <summary>
        /// Returns the measured power ramp-up time, in seconds.
        ///    If you set the NIWLANA_DSSS_POWER_RAMP_UP_LOW_THRESHOLD    and NIWLANA_DSSS_POWER_RAMP_UP_HIGH_THRESHOLD attributes    to 10 percent and 90 percent, respectively, this measurement is performed according to    section 18.4.7.6 of IEEE Standard 802.11b-1999. However, the toolkit does not define    the ramp time for unmodulated step input. To remove the variations in the instantaneous    power caused by modulation on the baseband signal, the toolkit filters the power vector      using the maximum value in a moving window.
        ///     Tip: The DSSS power ramp might not be found if the ramp-up or ramp-down time of the     incoming burst is greater than 5 microseconds, in which case the toolkit returns an error.  
        /// 
        /// </summary>
        public int GetResultDsssPowerRampUpTime(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultDsssPowerRampUpTime, channel, out value);
        }

        /// <summary>
        /// Returns the maximum total power over any 1 MHz portion of the spectrum in W/MHz.
        ///     You can use this attribute to verify that the DUT conforms to section 17.3.9.1 of    IEEE Standard 802.11a-1999, section 18.4.7.1 of IEEE Standard 802.11b-1999, or IEEE Standard 802.11n-2009    which specifies the maximum output power in mW/Hz in certain cases.
        ///     If you set the standard parameter of the niWLANA_SetStandard function to     NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use an channelx active channel string to configure this attribute.
        /// 
        /// </summary>
        public int GetResultMaxSpectralDensity(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultMaxSpectralDensity, channel, out value);
        }

        /// <summary>
        /// Returns the occupied bandwidth (OBW) of the signal measured in hertz (Hz). This value    is the frequency range containing 99 percent of the power of the entire signal spectrum.
        ///     If you set the standard parameter of the niWLANA_SetStandard function to     NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use an channelx active channel string to configure this attribute.
        /// 
        /// </summary>
        public int GetResultObw(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultObw, channel, out value);
        }

        /// <summary>
        /// Returns the highest frequency component, in hertz (Hz), of the frequency range containing 99    percent of the entire signal spectrum.        
        /// 
        /// </summary>
        public int GetResultObwHighFrequency(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultObwHighFrequency, channel, out value);
        }

        /// <summary>
        /// Returns the lowest frequency component, in hertz (Hz), of the frequency range containing 99    percent of the entire signal spectrum.        
        /// 
        /// </summary>
        public int GetResultObwLowFrequency(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultObwLowFrequency, channel, out value);
        }

        /// <summary>
        /// Returns the 802.11n physical layer convergence procedure (PLCP) frame format.
        ///     Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/n     license and set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        /// 
        /// </summary>
        public int GetResultOfdmDemod80211nPlcpFrameFormat(string channel, out string value)
        {
            int actualValue;
            int returnValue = GetInt(niWLANAProperties.ResultOfdmDemod80211nPlcpFrameFormat, channel, out actualValue);
            switch (actualValue)
            {
                case niWLANAConstants.Results80211nPlcpFrameFormatMixed:
                    value = "Mixed Mode";
                    break;
                case niWLANAConstants.Results80211nPlcpFrameFormatGreenfield:
                    value = "Greenfield";
                    break;
                case niWLANAConstants.Results80211nPlcpFrameFormatVarious:
                    value = "Various";
                    break;
                case niWLANAConstants.Results80211nPlcpFrameFormatUnknown:
                    value = "Unknown";
                    break;
                default:
                    value = "Unknown";
                    break;
            }
            return returnValue;
        }

        /// <summary>
        /// Returns the average gated power, in dBm. The gated power is the average power of the interval    defined by the interval between the    NIWLANA_OFDM_GATED_POWER_START_TIME and the NIWLANA_OFDM_GATED_POWER_STOP_TIME    attributes.
        ///     If you set the standard parameter of the niWLANA_SetStandard function to     NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use the channelx active channel string to configure this attribute.     
        /// 
        /// </summary>
        public int GetResultOfdmDemodAverageGatedPower(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultOfdmDemodAverageGatedPower, channel, out value);
        }

        /// <summary>
        /// Returns the carrier frequency leakage, in dB, as a ratio the DC subcarrier energy to the sum    of the data and pilot subcarrier energies, measured as defined in section 17.3.9.6.1 of    IEEE Standard 802.11a-1999 and section 20.3.21.7.2 of IEEE Standard 802.11n-2009. You also can see the frequency leakage as the measure of normalized DC offset.
        ///     If you set the standard parameter of the niWLANA_SetStandard function to     NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use the channelx active channel string to configure this attribute.  
        /// 
        /// </summary>
        public int GetResultOfdmDemodCarrierFrequencyLeakage(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultOfdmDemodCarrierFrequencyLeakage, channel, out value);
        }

        /// <summary>
        /// Returns the estimated carrier frequency offset, in hertz (Hz), of the transmitting device under test (DUT).    For example, if you set the NIWLANA_CARRIER_FREQUENCY attribute to 2.412 GHz and the toolkit measures    the carrier frequency of the DUT to be 2.413 GHz, the carrier frequency offset is 1 MHz. This    measurement follows section 17.3.9.4 of IEEE Standard 802.11a-1999 and section 20.3.21.4 of IEEE Standard 802.11n-2009.        
        /// 
        /// </summary>
        public int GetResultOfdmDemodCarrierFrequencyOffset(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultOfdmDemodCarrierFrequencyOffset, channel, out value);
        }

        /// <summary>
        /// Returns the contribution of the power from other streams in channelx when the active channel     string used is channelx. To get the individual contributions of each stream, use the format channelx/streamy for the active channel.
        ///     Use the following active channel string formats to configure this attribute.
        ///     &#8226;channelx.
        ///     &#8226;channelx/streamy, where x is not equal to y.
        ///     Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/n     license and set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        ///     The default value is 0. 
        /// 
        /// </summary>
        public int GetResultOfdmDemodCrossPower(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultOfdmDemodCrossPower, channel, out value);
        }

        /// <summary>
        /// Returns the data rate, in Mbps, used to transmit the SERVICE field and the    physical layer convergence procedure protocol data unit (PPDU).    If you set the NIWLANA_OFDM_HEADER_DETECTION_ENABLED attribute to NIWLANA_VAL_TRUE, the data rate is extracted    from the demodulated SIGNAL field of the frame.    The SIGNAL field is defined in section 17.3.4 of IEEE Standard 802.11a-1999.
        ///     If you set the    NIWLANA_OFDM_HEADER_DETECTION_ENABLED attribute to NIWLANA_VAL_FALSE, the value you specify    in the NIWLANA_OFDM_DATA_RATE attribute specifies the data rate.
        ///     Note: This attribute is not applicable for signals conforming to IEEE Standard 802.11n-2009.     To get the data rate for these signals, use the NIWLANA_RESULT_OFDM_DEMOD_MCS_INDEX attribute.    
        /// 
        /// </summary>
        public int GetResultOfdmDemodDataRate(string channel, out int value)
        {
            return GetInt(niWLANAProperties.ResultOfdmDemodDataRate, channel, out value);
        }

        /// <summary>
        /// Returns the detected guard interval, in seconds.    Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/n     license and set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        /// 
        /// </summary>
        public int GetResultOfdmDemodGuardInterval(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultOfdmDemodGuardInterval, channel, out value);
        }

        /// <summary>
        /// Returns the ratio, in dB, of the mean amplitude of the in-phase (I) signal to the mean amplitude of    the quadrature-phase (Q) signal.
        ///     If you set the standard parameter of the niWLANA_SetStandard function to     NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use the channelx active channel string to configure this attribute.
        ///     Note: If the magnitude of the carrier or the Sample clock frequency offset is greater than 25 ppm,     I/Q gain imbalance magnitude is greater than 3 dB, and quadrature skew magnitude is greater than 15 degrees,     the estimates of these impairments might not be accurate.
        ///     Tip: If the cable connecting the DUT and the RF signal analyzer     has significant low-pass characteristics or in-band ripple, measuring OFDM I/Q gain imbalance and quadrature skew might be adversely affected. 
        /// 
        /// </summary>
        public int GetResultOfdmDemodIqGainImbalance(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultOfdmDemodIqGainImbalance, channel, out value);
        }

        /// <summary>
        /// Returns the detected MCS index, which determines the modulation and coding scheme used, according to section 20.6 of IEEE Standard 802.11n-2009.
        ///     Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/n     license and set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        /// 
        /// </summary>
        public int GetResultOfdmDemodMcsIndex(string channel, out int value)
        {
            return GetInt(niWLANAProperties.ResultOfdmDemodMcsIndex, channel, out value);
        }

        /// <summary>
        ///  Returns the number of spatial streams according to the modulation and coding scheme (MCS) index detected from the header or provided by the user.
        ///     Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/n license and set    the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.   
        /// 
        /// </summary>
        public int GetResultOfdmDemodNumberOfExtensionSpatialStreams(string channel, out int value)
        {
            return GetInt(niWLANAProperties.ResultOfdmDemodNumberOfExtensionSpatialStreams, channel, out value);
        }

        /// <summary>
        /// Returns the length of the payload, including the medium access control (MAC) header, in bytes. If you set the    NIWLANA_OFDM_HEADER_DETECTION_ENABLED attribute to NIWLANA_VAL_TRUE, the payload length is extracted from the    demodulated SIGNAL field of the frame.    The SIGNAL field is defined in section 17.3.4 of IEEE Standard 802.11a-1999.
        ///     If you set the    NIWLANA_OFDM_HEADER_DETECTION_ENABLED attribute to NIWLANA_VAL_FALSE,    the value you specify in the NIWLANA_OFDM_PAYLOAD_LENGTH attribute specifies the payload length.    The toolkit returns a value of -1 if it encounters various data lengths during the averaging process.        
        /// 
        /// </summary>
        public int GetResultOfdmDemodPayloadLength(string channel, out int value)
        {
            return GetInt(niWLANAProperties.ResultOfdmDemodPayloadLength, channel, out value);
        }

        /// <summary>
        /// Returns the deviation in angle from 90 degrees between the in-phase (I) and quadrature-phase (Q)    signals.
        ///     Refer to the Quadrature Skew topic for more information.
        ///     If you set the standard parameter of the niWLANA_SetStandard function to     NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use the channelx active channel string to configure this attribute.
        ///     Note: If the magnitude of the carrier or the Sample clock frequency offset is greater than 25 ppm,     I/Q gain imbalance magnitude is greater than 3 dB, and quadrature skew magnitude is greater than 15 degrees,     the estimates of these impairments might not be accurate.
        ///      Tip: If the cable connecting the DUT and the RF signal analyzer     has significant low-pass characteristics or in-band ripple, measuring OFDM I/Q gain imbalance and quadrature skew might be adversely affected.   
        /// 
        /// </summary>
        public int GetResultOfdmDemodQuadratureSkew(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultOfdmDemodQuadratureSkew, channel, out value);
        }

        /// <summary>
        /// Returns the root mean square (RMS) error vector magnitude (EVM) value,     in dB, as defined in section 17.3.9.6.3 of IEEE Standard 802.11a-1999.    The method of computation is discussed in section 17.3.9.7 of IEEE     Standard 802.11a-1999 and section 20.3.21.7.4 of IEEE Standard 802.11n-2009.
        ///     If you set the standard parameter of the niWLANA_SetStandard function to     NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use an active channel string to configure this attribute.
        ///     &#8226;streamx for stream EVM.
        ///     &#8226;channelx for channel EVM.
        ///     Note: For DSSS demodulation, if equalization is not enabled and the reference pulse-shaping filter type    and filter coefficients do not match the filter configuration of the DUT/generator,     you might notice EVM degradation. Ensure the pulse-shaping type and pulse-shaping     coefficient settings match the input signal settings. 
        /// 
        /// </summary>
        public int GetResultOfdmDemodRmsEvm(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultOfdmDemodRmsEvm, channel, out value);
        }

        /// <summary>
        /// Returns the estimated sample clock offset, in parts per million (ppm). The estimated offset    is the difference between the sample clocks at the D/A converter of the transmitting device under test (DUT) and the digitizer.    If the clock offset is more than 25 ppm, the estimated value might be inaccurate. The clock offset      measurement follows section 17.3.9.5 of IEEE Standard 802.11a-1999, and section 20.3.21.6 of IEEE Standard 802.11n-2009.
        ///     Note: If the magnitude of the carrier or the Sample clock frequency offset is greater than 25 ppm,     I/Q gain imbalance magnitude is greater than 3 dB, and quadrature skew magnitude is greater than     15 degrees, the estimates of these impairments might not be accurate. 
        /// 
        /// </summary>
        public int GetResultOfdmDemodSampleClockOffset(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultOfdmDemodSampleClockOffset, channel, out value);
        }

        /// <summary>
        /// Returns the minimum value, in dB, of the difference between the magnitude of the channel frequency response and the spectral flatness mask,    as defined in section 17.3.9.6.2 of IEEE Standard 802.11a-1999 and section 20.3.21.2 of IEEE Standard 802.11n-2009.
        ///     If you set the standard parameter of the niWLANA_SetStandard function to     NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use the channelx active channel string to configure this attribute.
        ///     Refer to Spectral Flatness Margin for a graphical representation of the spectral flatness margin. 
        /// 
        /// </summary>
        public int GetResultOfdmDemodSpectralFlatnessMargin(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultOfdmDemodSpectralFlatnessMargin, channel, out value);
        }

        /// <summary>
        /// Returns the worst margin by which the spectral mask test fails. If the spectral mask    test passes, the value is 0 because the highest power spectral density (PSD) has    become the 0 dB reference for the spectral mask, as defined in section 17.3.9.2 of    IEEE Standard 802.11a-1999, section 18.4.7.3 of IEEE Standard 802.11b-1999, and IEEE Standard 802.11n-2009.    If the test fails, the value is    negative and indicates the minimum difference between the acquired power spectrum    and the spectral mask.
        ///     If you set the standard parameter of the niWLANA_SetStandard function to     NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use an channelx active channel string to configure this attribute.
        ///     Note: The spectral mask margin test may fail if the FFT window type is set to uniform. 
        /// 
        /// </summary>
        public int GetResultSpectralMaskMargin(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultSpectralMaskMargin, channel, out value);
        }

        /// <summary>
        /// Returns the worst margin for each linear segment of the spectral emission mask, as defined in section     17.3.9.2 of IEEE Standard 802.11a-1999, section 18.4.7.3 of IEEE Standard 802.11b-1999, and section     20.3.21.1 of IEEE Standard 802.11n-2009. The result indicates the minimum difference between the acquired PSD spectrum and the spectral mask.     
        /// 
        /// </summary>
        public int GetResultSpectralMaskMarginVector(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultSpectralMaskMarginVector, channel, out value);
        }

        /// <summary>
        /// Returns the reference level, in dBm/Hz, used for spectral mask margin measurements.    This value is the maxima of the central 18 MHz portion of the power spectral density (PSD)    spectrum.
        ///     The 0 dBr level of the spectral mask corresponds to this power value, and the toolkit    constructs the standard-specific spectral mask accordingly.        
        /// 
        /// </summary>
        public int GetResultSpectralMaskReferenceLevel(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultSpectralMaskReferenceLevel, channel, out value);
        }

        /// <summary>
        /// Returns the average power of the acquired burst, in dBm.
        ///    You can use this measurement to verify that the device under test (DUT) conforms to section 17.3.9.1 of    IEEE Standard 802.11a-1999 or section 18.4.7.1 of IEEE Standard 802.11b-1999,    both of which specify maximum output power, in mW, in certain cases.
        ///     Use the following equation to convert from dBm to mW: mW = 10^(dBm/10)
        ///     If you set the standard parameter of the niWLANA_SetStandard function to     NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use the channelx active channel string to configure this attribute.   
        /// 
        /// </summary>
        public int GetResultTxpowerMeasurementsAveragePower(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultTxpowerMeasurementsAveragePower, channel, out value);
        }

        /// <summary>
        /// Returns the peak power of the acquired burst, in dBm.
        ///     If you set the standard parameter of the niWLANA_SetStandard function to     NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use an channelx active channel string to configure this attribute.
        /// 
        /// </summary>
        public int GetResultTxpowerMeasurementsPeakPower(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultTxpowerMeasurementsPeakPower, channel, out value);
        }

        /// <summary>

        ///Returns the root-mean-square (RMS) error vector magnitude (EVM) value, in dB, of data subcarriers.
        ///     Acceptable RMS EVM limits are defined in section 17.3.9.6.3 of IEEE Standard 802.11a-1999 and section 20.3.21.7.4 of IEEE Standard 802.11n-2009.     The method of computation is discussed in section 17.3.9.7 of IEEE Standard 802.11a-1999 and section 20.3.21.7.4 of IEEE Standard 802.11n-2009.
        ///     If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use the following  active channel string formats to query this attribute:
        ///    streamx for stream EVM
        ///    channelx for channel EVM
        ///    Get Function: niWLANA_GetOFDMDemodDataRMSEVM
        /// 
        /// </summary>
        public int GetResultOfdmDemodDataRmsEvm(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultOfdmDemodDataRmsEvm, channel, out value);
        }

        /// <summary>

        ///Returns the root-mean-square (RMS) error vector magnitude (EVM) value, in dB, of pilot subcarriers.
        ///     Acceptable RMS EVM limits are defined in section 17.3.9.6.3 of IEEE Standard 802.11a-1999 and section 20.3.21.7.4 of IEEE Standard 802.11n-2009.     The method of computation is discussed in section 17.3.9.7 of IEEE Standard 802.11a-1999 and section 20.3.21.7.4 of IEEE Standard 802.11n-2009.
        ///     If you set the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use the following  active channel string formats to configure this attribute:
        ///    streamx for stream EVM
        ///    channelx for channel EVM
        ///    Get Function: niWLANA_GetOFDMDemodPilotRMSEVM
        /// 
        /// </summary>
        public int GetResultOfdmDemodPilotRmsEvm(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.ResultOfdmDemodPilotRmsEvm, channel, out value);
        }

        /// <summary>
        /// Specifies the span, in hertz (Hz), for spectral acquisition.
        /// 	    If you set the standard parameter of the niWLANA_SetStandard function to     NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use an channelx active channel string to configure this attribute.
        ///     For all signals with a channel bandwidth of 20 MHz, the recommended span is 66 MHz.    For 802.11n signals with a channel bandwidth of 40 MHz, the recommended span is 120 MHz.         
        /// 
        /// </summary>
        public int SetSpan(string channel, double value)
        {
            return SetDouble(niWLANAProperties.Span, channel, value);
        }

        /// <summary>
        /// Specifies the span, in hertz (Hz), for spectral acquisition.
        /// 	    If you set the standard parameter of the niWLANA_SetStandard function to     NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use an channelx active channel string to configure this attribute.
        ///     For all signals with a channel bandwidth of 20 MHz, the recommended span is 66 MHz.    For 802.11n signals with a channel bandwidth of 40 MHz, the recommended span is 120 MHz.         
        /// 
        /// </summary>
        public int GetSpan(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.Span, channel, out value);
        }

        /// <summary>
        /// Specifies whether to enable spectral mask-related measurements.
        ///     Refer to section 17.9.3.2 of IEEE Standard 802.11a-1999,     section 18.4.7.3 of IEEE Standard 802.11b-1999, section 19.5.4 of IEEE Standard 802.11g-2003,    and section 20.3.21.1 IEEE Standard 802.11n-2009 for details.
        ///      The default value is NIWLANA_VAL_FALSE.    
        /// 
        /// </summary>
        public int GetSpectralMaskEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.SpectralMaskEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies the array of frequency offsets used to define the spectral mask.     The toolkit does not assume mask symmetry. Therefore, both positive and negative offsets from the center     frequency must be defined in ascending order. Refer to section 20.3.21.1 of IEEE Standard 802.11n-2009 for    the unlicensed band spectral mask definitions for the 20 MHz and 40 MHz bands.
        ///     The default is an array of mask specification corresponding to a nominal bandwidth of 20 MHz.     
        /// 
        /// </summary>
        public int SetSpectralMaskFrequencyOffsets(string channel, double [] data , int dataSize)
        {
            return SetVectorDouble(channel, niWLANAProperties.SpectralMaskFrequencyOffsets, data, dataSize);
        }

        /// <summary>
        /// Specifies the array of frequency offsets used to define the spectral mask.     The toolkit does not assume mask symmetry. Therefore, both positive and negative offsets from the center     frequency must be defined in ascending order. Refer to section 20.3.21.1 of IEEE Standard 802.11n-2009 for    the unlicensed band spectral mask definitions for the 20 MHz and 40 MHz bands.
        ///     The default is an array of mask specification corresponding to a nominal bandwidth of 20 MHz.     
        /// 
        /// </summary>
        public int GetSpectralMaskFrequencyOffsets(string channel, double[] data, int dataSize, out int actualNumDataArrayElements)
        {
            return GetVectorDouble(channel, niWLANAProperties.SpectralMaskFrequencyOffsets, data, dataSize, out actualNumDataArrayElements);
        }

        /// <summary>
        /// Specifies the number of iterations over which the toolkit averages spectral    mask measurements.
        ///     If you increase the number of averages, the toolkit provides smoother values but takes a longer time to compute the values.
        ///     The default value is 10. Valid values are 1 to 1,000 (inclusive).          
        /// 
        /// </summary>
        public int GetSpectralMaskNumberOfAverages(string channel, out int value)
        {
            return GetInt(niWLANAProperties.SpectralMaskNumberOfAverages, channel, out value);
        }

        /// <summary>
        /// Specifies the array of power offsets used to define the spectral mask.     You must specify the offsets, in dB, from the mask reference power level for each of the frequency offset points.    The order of the power offsets in this array correspond to the order of the frequency offsets that you specify in     the NIWLANA_SPECTRAL_MASK_FREQUENCY_OFFSETS attribute. Refer to section 20.3.21.1 of IEEE Standard 802.11n-2009 for the     unlicensed band spectral mask definitions for the 20 MHz and 40 MHz bands.
        ///     The default is an array of mask specification corresponding to a nominal bandwidth of 20 MHz.  
        /// 
        /// </summary>
        public int SetSpectralMaskPowerOffsets(string channel, double [] data, int dataSize)
        {
            return SetVectorDouble(channel, niWLANAProperties.SpectralMaskPowerOffsets, data, dataSize);
        }

        /// <summary>
        /// Specifies the array of power offsets used to define the spectral mask.     You must specify the offsets, in dB, from the mask reference power level for each of the frequency offset points.    The order of the power offsets in this array correspond to the order of the frequency offsets that you specify in     the NIWLANA_SPECTRAL_MASK_FREQUENCY_OFFSETS attribute. Refer to section 20.3.21.1 of IEEE Standard 802.11n-2009 for the     unlicensed band spectral mask definitions for the 20 MHz and 40 MHz bands.
        ///     The default is an array of mask specification corresponding to a nominal bandwidth of 20 MHz.  
        /// 
        /// </summary>
        public int GetSpectralMaskPowerOffsets(string channel, double[] data, int dataSize, out int actualNumDataArrayElements)
        {
            return GetVectorDouble(channel, niWLANAProperties.SpectralMaskPowerOffsets, data, dataSize, out actualNumDataArrayElements);
        }

        /// <summary>
        /// Specifies the user-defined reference level, in dBm/Hz, that the toolkit uses for spectral mask measurements.     This attribute applies only if you set the NIWLANA_SPECTRAL_MASK_REFERENCE_LEVEL_TYPE attribute to NIWLANA_VAL_SPECTRAL_MASK_REFERENCE_LEVEL_TYPE_USER_DEFINED.
        ///     The default value is 0. 
        /// 
        /// </summary>
        public int SetSpectralMaskReferenceLevel(string channel, double value)
        {
            return SetDouble(niWLANAProperties.SpectralMaskReferenceLevel, channel, value);
        }

        /// <summary>
        /// Specifies the user-defined reference level, in dBm/Hz, that the toolkit uses for spectral mask measurements.     This attribute applies only if you set the NIWLANA_SPECTRAL_MASK_REFERENCE_LEVEL_TYPE attribute to NIWLANA_VAL_SPECTRAL_MASK_REFERENCE_LEVEL_TYPE_USER_DEFINED.
        ///     The default value is 0. 
        /// 
        /// </summary>
        public int GetSpectralMaskReferenceLevel(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.SpectralMaskReferenceLevel, channel, out value);
        }

        /// <summary>
        /// Specifies the type of reference to use for spectral mask measurements.
        ///     The default value is NIWLANA_VAL_SPECTRAL_MASK_REFERENCE_LEVEL_TYPE_PEAK_SIGNAL_POWER.   
        /// 
        /// </summary>
        public int SetSpectralMaskReferenceLevelType(string channel, int value)
        {
            return SetInt(niWLANAProperties.SpectralMaskReferenceLevelType, channel, value);
        }

        /// <summary>
        /// Specifies the type of reference to use for spectral mask measurements.
        ///     The default value is NIWLANA_VAL_SPECTRAL_MASK_REFERENCE_LEVEL_TYPE_PEAK_SIGNAL_POWER.   
        /// 
        /// </summary>
        public int GetSpectralMaskReferenceLevelType(string channel, out int value)
        {
            return GetInt(niWLANAProperties.SpectralMaskReferenceLevelType, channel, out value);
        }

        /// <summary>
        /// Specifies whether to enable the spectral mask trace.
        ///     If you set the standard parameter of the niWLANA_SetStandard function to     NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use an channelx active channel string to configure this attribute.
        ///    The default value is NIWLANA_VAL_FALSE.          
        /// 
        /// </summary>
        public int SetSpectralMaskTraceEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.SpectralMaskTraceEnabled, channel, value);
        }

        /// <summary>
        /// Specifies whether to enable the spectral mask trace.
        ///     If you set the standard parameter of the niWLANA_SetStandard function to     NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM, you must use an channelx active channel string to configure this attribute.
        ///    The default value is NIWLANA_VAL_FALSE.          
        /// 
        /// </summary>
        public int GetSpectralMaskTraceEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.SpectralMaskTraceEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies whether the spectral mask is user-defined or standard.
        ///     The default value is NIWLANA_VAL_SPECTRAL_MASK_TYPE_STANDARD. 
        /// 
        /// </summary>
        public int SetSpectralMaskType(string channel, int value)
        {
            return SetInt(niWLANAProperties.SpectralMaskType, channel, value);
        }

        /// <summary>
        /// Specifies whether the spectral mask is user-defined or standard.
        ///     The default value is NIWLANA_VAL_SPECTRAL_MASK_TYPE_STANDARD. 
        /// 
        /// </summary>
        public int GetSpectralMaskType(string channel, out int value)
        {
            return GetInt(niWLANAProperties.SpectralMaskType, channel, out value);
        }

        /// <summary>
        /// Specifies whether to enable all spectral measurement attributes. This attribute overrides    the individual enable settings for various spectral measurements. If you enable this    attribute while any of the time domain power/demodulation measurements are also enabled,    the toolkit returns an error.
        ///     The default value is NIWLANA_VAL_FALSE.        
        /// 
        /// </summary>
        public int SetSpectralMeasurementsAllEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.SpectralMeasurementsAllEnabled, channel, value);
        }

        /// <summary>
        /// Specifies whether to enable all spectral measurement attributes. This attribute overrides    the individual enable settings for various spectral measurements. If you enable this    attribute while any of the time domain power/demodulation measurements are also enabled,    the toolkit returns an error.
        ///     The default value is NIWLANA_VAL_FALSE.        
        /// 
        /// </summary>
        public int GetSpectralMeasurementsAllEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.SpectralMeasurementsAllEnabled, channel, out value);
        }


        /// <summary>
        /// Specifies the standard for measurements.
        ///     Note: If you do not select a standard, the toolkit returns an error.
        ///     The default value is NIWLANA_VAL_STANDARD_80211AG_OFDM.  
        /// 
        /// </summary>
        public int GetStandard(string channel, out int value)
        {
            return GetInt(niWLANAProperties.Standard, channel, out value);
        }

        /// <summary>
        /// Specifies whether to enable measurement of peak power and average power in the acquired burst, in dBm.    The toolkit automatically detects the start and end of a valid burst corresponding to a WLAN    packet.
        ///     If the toolkit cannot automatically determine the start of the burst, the toolkit returns an error.    If the toolkit cannot determine the end of the burst, the toolkit uses the whole acquired waveform.
        ///     The toolkit detects the start of the burst by determining the position at which the total power    of a non-overlapping moving window increases at least 12 dB between two consecutive windows,    as well as two windows separated by one window. The toolkit detects the end of the burst by determining    the position at which the total power of a moving window decreases at least 12 dB between    two consecutive windows, as well as two windows separated by one window.
        ///     The default value is NIWLANA_VAL_FALSE.        
        /// 
        /// </summary>
        public int GetTxpowerMeasurementsEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.TxpowerMeasurementsEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies the number of iterations over which the toolkit averages burst power measurements.
        ///     If you increase the number of averages, the toolkit provides smoother values but takes a longer time to compute the values.
        ///     The default value is 1. Valid values are 1 to 1,000 (inclusive).        
        /// 
        /// </summary>
        public int GetTxpowerMeasurementsNumberOfAverages(string channel, out int value)
        {
            return GetInt(niWLANAProperties.TxpowerMeasurementsNumberOfAverages, channel, out value);
        }

        /// <summary>
        /// Specifies whether to enable the power versus time (PvT) trace.
        ///     The default value is NIWLANA_VAL_FALSE.          
        /// 
        /// </summary>
        public int SetTxpowerMeasurementsPvtTraceEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.TxpowerMeasurementsPvtTraceEnabled, channel, value);
        }
        /// <summary>

        ///Specifies the trigger delay in seconds. The toolkit computes the NIWLANA_IQ_PRE-TRIGGER_DELAY and NIWLANA_POST-TRIGGER_DELAY attributes based on the value of this attribute.
        ///     If the signal to be measured does not generate immediately when the trigger occurs but generates after a delay, set this attribute to the value of the delay.     The trigger delay value must be positive in this case. Set this attribute to a negative value if your application requires a pretrigger delay in addition     to the recommended delay from the NIWLANA_RECOMMENDED_IQ_PRE_TRIGGER_DELAY attribute.
        ///     The default value is 0.
        ///    Get Function: niWLANA_GetTriggerDelay
        ///    Set Function: niWLANA_SetTriggerDelay
        /// 
        /// </summary>
        public int SetTriggerDelay(string channel, double value)
        {
            return SetDouble(niWLANAProperties.TriggerDelay, channel, value);
        }

        /// <summary>

        ///Specifies the trigger delay in seconds. The toolkit computes the NIWLANA_IQ_PRE-TRIGGER_DELAY and NIWLANA_POST-TRIGGER_DELAY attributes based on the value of this attribute.
        ///     If the signal to be measured does not generate immediately when the trigger occurs but generates after a delay, set this attribute to the value of the delay.     The trigger delay value must be positive in this case. Set this attribute to a negative value if your application requires a pretrigger delay in addition     to the recommended delay from the NIWLANA_RECOMMENDED_IQ_PRE_TRIGGER_DELAY attribute.
        ///     The default value is 0.
        ///    Get Function: niWLANA_GetTriggerDelay
        ///    Set Function: niWLANA_SetTriggerDelay
        /// 
        /// </summary>
        public int GetTriggerDelay(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.TriggerDelay, channel, out value);
        }
        /// <summary>

        ///Returns the toolkit recommended acquisition type for the current measurement configuration.     If any of the spectral measurements attributes are enabled, the toolkit sets this attribute to NIWLANA_VAL_SPECTRUM.     Otherwise, the toolkit sets this attribute to NIWLANA_VAL_IQ.
        ///    Get Function: niWLANA_GetRecommendedAcquisitionType
        /// 
        /// </summary>
        public int GetRecommendedAcquisitionType(string channel, out int value)
        {
            return GetInt(niWLANAProperties.RecommendedAcquisitionType, channel, out value);
        }
        /// <summary>

        ///Returns the toolkit recommended acquisition length, in seconds. If you do not use the niWLANA_RFSAConfigureHardware function,     multiply this attribute by the NIWLANA_IQ_SAMPLING_RATE attribute and pass the result to the NIRFSA_ATTR_NUMBER_OF_SAMPLES attribute.
        ///     The NI WLAN Analysis Toolkit determines this attribute using the following equation:
        ///     Recommended Acquisition Length = Acquisition Length + Recommended Pre-Trigger Delay
        ///    Get Function: niWLANA_GetRecommendedAcquisitionLength
        /// 
        /// </summary>
        public int GetRecommendedAcquisitionLength(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.RecommendedAcquisitionLength, channel, out value);
        }
        /// <summary>

        ///Returns the toolkit recommended minimum quiet time, in seconds, during which the signal level must be below the trigger value for triggering to occur.     If you do not use the niWLANA_RFSAConfigureHardware function, pass this attribute to the NIRFSA_ATTR_REF_TRIGGER_MINIMUM_QUIET_TIME attribute.
        ///     This attribute returns a value of 5 microseconds.
        ///    Get Function: niWLANA_GetRecommendedMinimumQuietTime
        /// 
        /// </summary>
        public int GetRecommendedMinimumQuietTime(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.RecommendedMinimumQuietTime, channel, out value);
        }
        /// <summary>

        ///Returns the toolkit recommended number of records to acquire from the NI RF vector signal analyzer.     If you do not use the niWLANA_RFSAConfigureHardware function, pass this attribute to the NIRFSA_ATTR_NUMBER_OF_RECORDS attribute.
        ///     The NIWLANA_RECOMMENDED_NUMBER_OF_RECORDS attribute returns the maximum of the following Number of Averages attributes if the corresponding measurements are enabled.
        ///     Spectral measurements: 
        ///     NIWLANA_SPECTRAL_MASK_NUMBER_OF_AVERAGES
        ///     NIWLANA_OBW_NUMBER_OF_AVERAGES
        ///     NIWLANA_MAX_SPECTRAL_DENSITY_NUMBER_OF_AVERAGES
        ///     I/Q measurements on DSSS signals: 
        ///     NIWLANA_TXPOWER_MEASUREMENTS_NUMBER_OF_AVERAGES
        ///     NIWLANA_DSSS_POWER_RAMP_MEASUREMENT_NUMBER_OF_AVERAGES
        ///     NIWLANA_DSSS_DEMOD_NUMBER_OF_AVERAGES
        ///     I/Q measurements on OFDM signals: 
        ///     NIWLANA_TXPOWER_MEASUREMENTS_NUMBER_OF_AVERAGES
        ///     NIWLANA_OFDM_DEMOD_NUMBER_OF_AVERAGES
        ///    Get Function: niWLANA_GetRecommendedNumberOfRecords
        /// 
        /// </summary>
        public int GetRecommendedNumberOfRecords(string channel, out int value)
        {
            return GetInt(niWLANAProperties.RecommendedNumberOfRecords, channel, out value);
        }
        /// <summary>

        ///Returns the toolkit recommended minimum sampling rate, in samples per second, for the NI RF vector signal analyzer. If you do not use the     niWLANA_RFSAConfigureHardware function, pass this attribute to the niRFSA_ConfigureIQRate function.
        ///     This property is derived from the current signal configuration as shown in the table found in the Recommended IQ Sampling Rate help topic.     To view the table, refer to the NI LabWindows/CVI WLAN Analysis Toolkit Reference Help, and navigate to the Recommended I/Q Sampling Rate help topic.
        ///    Get Function: niWLANA_GetRecommendedIQSamplingRate
        /// 
        /// </summary>
        public int GetRecommendedIqSamplingRate(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.RecommendedIqSamplingRate, channel, out value);
        }
        /// <summary>

        ///Returns the toolkit recommended pretrigger delay, in seconds. If you do not use the niWLANA_RFSAConfigureHardware function,     multiply this attribute by the NIRFSA_ATTR_IQ_RATE attribute, then pass the result to the NIRFSA_ATTR_REF_TRIGGER_PRETRIGGER_SAMPLES attribute.
        ///     The toolkit uses this property to acquire data prior to the trigger to account for the delays in the measurement process.     The minimum pre-trigger delay the toolkit recommends is 5 us.     If your application requires an additional pretrigger delay, configure the NIWLANA_TRIGGER_DELAY attribute.
        ///    Get Function: niWLANA_GetRecommendedIQPreTriggerDelay
        /// 
        /// </summary>
        public int GetRecommendedIqPreTriggerDelay(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.RecommendedIqPreTriggerDelay, channel, out value);
        }
        /// <summary>

        ///Returns the toolkit recommended post-trigger delay, in seconds. If the niWLANA_RFSAMeasure or niWLANA_RFSAMIMOMeasure functions are not being used     and you configure a positive value to the NIWLANA_TRIGGER_DELAY attribute, then pass this attribute to the t0 parameter of the niWLANA_AnalyzeIQComplexF64     or niWLANA_AnalyzeMIMOIQComplexF64 functions.
        ///     The toolkit derives the value for this attribute from the NIWLANA_TRIGGER_DELAY attribute. Configure the NIWLANA_TRIGGER_DELAY attribute if the signal to be measured is generated     after a delay and is not generated immediately when the trigger occurs.
        ///    Get Function: niWLANA_GetRecommendedIQPostTriggerDelay
        /// 
        /// </summary>
        public int GetRecommendedIqPostTriggerDelay(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.RecommendedIqPostTriggerDelay, channel, out value);
        }
        /// <summary>

        ///Returns the toolkit recommended span, in hertz (Hz), for spectral acquisition.
        ///    Get Function: niWLANA_GetRecommendedSpectrumSpan
        /// 
        /// </summary>
        public int GetRecommendedSpectrumSpan(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.RecommendedSpectrumSpan, channel, out value);
        }
        /// <summary>

        ///Returns the toolkit recommended resolution bandwidth (RBW) type for spectral acquisition.     The value of this attribute is the same as the NIWLANA_RBW_DEFINITION attribute. If you do not use the niWLANA_RFSAConfigureHardware function,     derive the NIRFSA_ATTR_RESOLUTION_BANDWIDTH_TYPE attribute from the NIWLANA_RECOMMENDED_SPECTRUM_RBW_DEFINITION attribute.
        ///    Get Function: niWLANA_GetRecommendedSpectrumRBWDefinition
        /// 
        /// </summary>
        public int GetRecommendedSpectrumRbwDefinition(string channel, out int value)
        {
            return GetInt(niWLANAProperties.RecommendedSpectrumRbwDefinition, channel, out value);
        }
        /// <summary>

        ///Returns the toolkit recommended resolution bandwidth (RBW), in hertz (Hz), for spectral acquisition. The value of this property is the same as the     NIWLANA_RBW attribute. If you do not use the niWLANA_RFSAConfigureHardware function, pass the NIWLANA_RECOMMENDED_SPECTRUM_RBW attribute     to the NIRFSA_ATTR_RESOLUTION_BANDWIDTH attribute.
        ///    Get Function: niWLANA_GetRecommendedSpectrumRBW
        /// 
        /// </summary>
        public int GetRecommendedSpectrumRbw(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.RecommendedSpectrumRbw, channel, out value);
        }
        /// <summary>

        ///Returns the toolkit recommended video bandwidth (VBW), in hertz (Hz), for spectral acquisition.
        ///    Get Function: niWLANA_GetRecommendedSpectrumVBW
        /// 
        /// </summary>
        public int GetRecommendedSpectrumVbw(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.RecommendedSpectrumVbw, channel, out value);
        }
        /// <summary>

        ///Returns the toolkit recommended time-domain FFT window type. The value of this attribute is the same as the NIWLANA_FFT_WINDOW_TYPE attribute.     If you do not use the niWLANA_RFSAConfigureHardware function, pass this attribute to the NIRFSA_ATTR_FFT_WINDOW_TYPE attribute.
        ///     Note: For this attribute's valid value descriptions, refer to the FFT Window Type Values help topic in the NI LabWindows/CVI WLAN Analysis Toolkit Reference Help.
        ///    Get Function: niWLANA_GetRecommendedSpectrumFFTWindowType
        /// 
        /// </summary>
        public int GetRecommendedSpectrumFftWindowType(string channel, out int value)
        {
            return GetInt(niWLANAProperties.RecommendedSpectrumFftWindowType, channel, out value);
        }
        /// <summary>

        ///Returns the channel bandwidth detected using the niWLANA_RFSAAutoDetectionOfStandard function.
        ///    Get Function: niWLANA_GetDetectedChannelBandwidth
        /// 
        /// </summary>
        public int GetDetectedChannelBandwidth(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.DetectedChannelBandwidth, channel, out value);
        }

        /// <summary>

        ///Returns the standard, which includes the type of physical layer, detected using the niWLANA_RFSAAutoDetectionOfStandard function.
        ///    Get Function: niWLANA_GetDetectedStandard
        /// 
        /// </summary>
        public int GetDetectedStandard(string channel, out int value)
        {
            return GetInt(niWLANAProperties.DetectedStandard, channel, out value);
        }
        /// <summary>

        ///Specifies whether the toolkit automatically calculates the span required for the current configuration of spectral measurements.     If you want to specify the NIWLANA_SPAN attribute, set the NIWLANA_AUTO_SPAN_ENABLED attribute to NIWLANA_VAL_FALSE.
        ///    Get Function: niWLANA_GetAutoSpanEnabled
        ///    Set Function: niWLANA_SetAutoSpanEnabled
        /// 
        /// </summary>
        public int SetAutoSpanEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.AutoSpanEnabled, channel, value);
        }
        /// <summary>

        ///Specifies whether the toolkit automatically calculates the span required for the current configuration of spectral measurements.     If you want to specify the NIWLANA_SPAN attribute, set the NIWLANA_AUTO_SPAN_ENABLED attribute to NIWLANA_VAL_FALSE.
        ///    Get Function: niWLANA_GetAutoSpanEnabled
        ///    Set Function: niWLANA_SetAutoSpanEnabled
        /// 
        /// </summary>
        public int GetAutoSpanEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.AutoSpanEnabled, channel, out value);
        }
        /// <summary>

        ///Specifies whether to enable the pilot error vector magnitude (EVM) per symbol trace for signals with an OFDM payload.
        ///     The default value is NIWLANA_VAL_FALSE.
        ///    Get Function: niWLANA_GetOFDMDemodPilotEVMPerSymbolTraceEnabled
        ///    Set Function: niWLANA_GetOFDMDemodPilotEVMPerSymbolTraceEnabled
        /// 
        /// </summary>
        public int SetOfdmDemodPilotEvmPerSymbolTraceEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.OfdmDemodPilotEvmPerSymbolTraceEnabled, channel, value);
        }
        /// <summary>

        ///Specifies whether to enable the pilot error vector magnitude (EVM) per symbol trace for signals with an OFDM payload.
        ///     The default value is NIWLANA_VAL_FALSE.
        ///    Get Function: niWLANA_GetOFDMDemodPilotEVMPerSymbolTraceEnabled
        ///    Set Function: niWLANA_GetOFDMDemodPilotEVMPerSymbolTraceEnabled
        /// 
        /// </summary>
        public int GetOfdmDemodPilotEvmPerSymbolTraceEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodPilotEvmPerSymbolTraceEnabled, channel, out value);
        }
        /// <summary>

        ///Specifies whether to enable the data error vector magnitude (EVM) per symbol trace for signals with an OFDM payload.
        ///     The default value is NIWLANA_VAL_FALSE.
        ///    Get Function: niWLANA_GetOFDMDemodDataEVMPerSymbolTraceEnabled
        ///    Set Function: niWLANA_SetOFDMDemodDataEVMPerSymbolTraceEnabled
        /// 
        /// </summary>
        public int SetOfdmDemodDataEvmPerSymbolTraceEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.OfdmDemodDataEvmPerSymbolTraceEnabled, channel, value);
        }
        /// <summary>

        ///Specifies whether to enable the data error vector magnitude (EVM) per symbol trace for signals with an OFDM payload.
        ///     The default value is NIWLANA_VAL_FALSE.
        ///    Get Function: niWLANA_GetOFDMDemodDataEVMPerSymbolTraceEnabled
        ///    Set Function: niWLANA_SetOFDMDemodDataEVMPerSymbolTraceEnabled
        /// 
        /// </summary>
        public int GetOfdmDemodDataEvmPerSymbolTraceEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodDataEvmPerSymbolTraceEnabled, channel, out value);
        }
        /// <summary>

        ///Specifies whether to check for the validity of the signal frame check sequence (FCS). The toolkit calculates FCS over the decoded bits     excluding the last 32 bits. The toolkit then compares that value with the FCS value in the received payload, which is represented by the last 32 bits of the payload.
        ///     The default value is NIWLANA_VAL_FALSE.
        ///    Get Function: niWLANA_GetOFDMDemodMACFrameCheckSequenceCheckEnabled
        ///    Set Function: niWLANA_SetOFDMDemodMACFrameCheckSequenceCheckEnabled
        /// 
        /// </summary>
        public int SetOfdmDemodMacFrameCheckSequenceCheckEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.OfdmDemodMacFrameCheckSequenceCheckEnabled, channel, value);
        }
        /// <summary>

        ///Specifies whether to check for the validity of the signal frame check sequence (FCS). The toolkit calculates FCS over the decoded bits     excluding the last 32 bits. The toolkit then compares that value with the FCS value in the received payload, which is represented by the last 32 bits of the payload.
        ///     The default value is NIWLANA_VAL_FALSE.
        ///    Get Function: niWLANA_GetOFDMDemodMACFrameCheckSequenceCheckEnabled
        ///    Set Function: niWLANA_SetOFDMDemodMACFrameCheckSequenceCheckEnabled
        /// 
        /// </summary>
        public int GetOfdmDemodMacFrameCheckSequenceCheckEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.OfdmDemodMacFrameCheckSequenceCheckEnabled, channel, out value);
        }
        /// <summary>

        ///Specifies whether to enable all traces of DSSS demodulation.
        ///     The default value is NIWLANA_VAL_FALSE.
        ///    Get Function: niWLANA_GetDSSSDemodAllTracesEnabled
        ///    Set Function: niWLANA_SetDSSSDemodAllTracesEnabled
        /// 
        /// </summary>
        public int SetDsssDemodAllTracesEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.DsssDemodAllTracesEnabled, channel, value);
        }
        /// <summary>

        ///Specifies whether to enable all traces of DSSS demodulation.
        ///     The default value is NIWLANA_VAL_FALSE.
        ///    Get Function: niWLANA_GetDSSSDemodAllTracesEnabled
        ///    Set Function: niWLANA_SetDSSSDemodAllTracesEnabled
        /// 
        /// </summary>
        public int GetDsssDemodAllTracesEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.DsssDemodAllTracesEnabled, channel, out value);
        }
        /// <summary>

        ///Specifies whether to enable error vector magnitude (EVM) per chip trace for 802.11b and 802.11g DSSS signals.
        ///     The default value is NIWLANA_VAL_FALSE.
        ///    Get Function: niWLANA_GetDSSSDemodEVMPerChipTraceEnabled
        ///    Set Function: niWLANA_SetDSSSDemodEVMPerChipTraceEnabled
        /// 
        /// </summary>
        public int SetDsssDemodEvmPerChipTraceEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.DsssDemodEvmPerChipTraceEnabled, channel, value);
        }
        /// <summary>

        ///Specifies whether to enable error vector magnitude (EVM) per chip trace for 802.11b and 802.11g DSSS signals.
        ///     The default value is NIWLANA_VAL_FALSE.
        ///    Get Function: niWLANA_GetDSSSDemodEVMPerChipTraceEnabled
        ///    Set Function: niWLANA_SetDSSSDemodEVMPerChipTraceEnabled
        /// 
        /// </summary>
        public int GetDsssDemodEvmPerChipTraceEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.DsssDemodEvmPerChipTraceEnabled, channel, out value);
        }
        /// <summary>

        ///Specifies whether to check for the validity of the signal frame check sequence (FCS). The toolkit calculates the checksum     over the decoded bits, excluding the last 32 bits. The toolkit then compares this value with the checksum value in the received payload,     which is represented by the last 32 bits of the payload.
        ///     Note: Decoding of received bits is not supported for DSSS signals with a data rate of 33 Mbps, and the toolkit cannot compute MAC FCS.
        ///     The default value is NIWLANA_VAL_FALSE.
        ///    Get Function: niWLANA_GetDSSSDemodMACFrameCheckSequenceCheckEnabled
        ///    Set Function: niWLANA_SetDSSSDemodMACFrameCheckSequenceCheckEnabled
        /// 
        /// </summary>
        public int SetDsssDemodMacFrameCheckSequenceCheckEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.DsssDemodMacFrameCheckSequenceCheckEnabled, channel, value);
        }
        /// <summary>

        ///Specifies whether to check for the validity of the signal frame check sequence (FCS). The toolkit calculates the checksum     over the decoded bits, excluding the last 32 bits. The toolkit then compares this value with the checksum value in the received payload,     which is represented by the last 32 bits of the payload.
        ///     Note: Decoding of received bits is not supported for DSSS signals with a data rate of 33 Mbps, and the toolkit cannot compute MAC FCS.
        ///     The default value is NIWLANA_VAL_FALSE.
        ///    Get Function: niWLANA_GetDSSSDemodMACFrameCheckSequenceCheckEnabled
        ///    Set Function: niWLANA_SetDSSSDemodMACFrameCheckSequenceCheckEnabled
        /// 
        /// </summary>
        public int GetDsssDemodMacFrameCheckSequenceCheckEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.DsssDemodMacFrameCheckSequenceCheckEnabled, channel, out value);
        }

        /// <summary>
        /// Specifies whether to enable the power versus time (PvT) trace.
        ///     The default value is NIWLANA_VAL_FALSE.          
        /// 
        /// </summary>
        public int GetTxpowerMeasurementsPvtTraceEnabled(string channel, out int value)
        {
            return GetInt(niWLANAProperties.TxpowerMeasurementsPvtTraceEnabled, channel, out value);
        }
        //added on 26-12-2011
        /// <summary>
        /// Specifies whether to enable measurement of peak power and average power in the acquired burst, in dBm.    The toolkit automatically detects the start and end of a valid burst corresponding to a WLAN    packet.
        ///     If the toolkit cannot automatically determine the start of the burst, the toolkit returns an error.    If the toolkit cannot determine the end of the burst, the toolkit uses the whole acquired waveform.
        ///     The toolkit detects the start of the burst by determining the position at which the total power    of a non-overlapping moving window increases at least 12 dB between two consecutive windows,    as well as two windows separated by one window. The toolkit detects the end of the burst by determining    the position at which the total power of a moving window decreases at least 12 dB between    two consecutive windows, as well as two windows separated by one window.
        ///     The default value is NIWLANA_VAL_FALSE.        
        /// 
        /// </summary>
        public int SetTxpowerMeasurementsEnabled(string channel, int value)
        {
            return SetInt(niWLANAProperties.TxpowerMeasurementsEnabled, channel, value);
        }

        /// <summary>

        ///Specifies the number of iterations over which the toolkit averages burst power measurements.
        ///     If you increase the number of averages, the toolkit provides smoother values but takes longer to compute the values.
        ///     The default value is 1. Valid values are 1 to 1,000, inclusive.
        ///    Get Function: niWLANA_GetTxPowerMeasurementNumberOfAverages
        ///    Set Function: niWLANA_SetTxPowerMeasurementNumberOfAverages
        /// 
        /// </summary>
        public int SetTxpowerMeasurementsNumberOfAverages(string channel, int value)
        {
            return SetInt(niWLANAProperties.TxpowerMeasurementsNumberOfAverages, channel, value);
        }

        /// <summary>

        ///Specifies the instantaneous bandwidth, in hertz (Hz), of the NI RF vector signal analyzer.
        ///     The default value is 50 MHz.
        ///    Get Function: niWLANA_GetDeviceInstantaneousBandwidth
        ///    Set Function: niWLANA_SetDeviceInstantaneousBandwidth
        /// 
        /// </summary>
        public int SetDeviceInstantaneousBandwidth(string channel, double value)
        {
            return SetDouble(niWLANAProperties.DeviceInstantaneousBandwidth, channel, value);
        }
        /// <summary>

        ///Specifies the instantaneous bandwidth, in hertz (Hz), of the NI RF vector signal analyzer.
        ///     The default value is 50 MHz.
        ///    Get Function: niWLANA_GetDeviceInstantaneousBandwidth
        ///    Set Function: niWLANA_SetDeviceInstantaneousBandwidth
        /// 
        /// </summary>
        public int GetDeviceInstantaneousBandwidth(string channel, out double value)
        {
            return GetDouble(niWLANAProperties.DeviceInstantaneousBandwidth, channel, out value);
        }
        /// <summary>

        ///Returns whether the cyclic redundancy check (CRC) has passed for the high-throughput SIGNAL (HT-SIG) field as defined in section 20.3.9.4.3     of the IEEE Standard 802.11n-2009.
        ///     Note: To use this attribute, you must have the NI WLAN Analysis Toolkit for IEEE 802.11a/b/g/j/n/p license and set    the NIWLANA_STANDARD attribute to NIWLANA_VAL_STANDARD_80211N_MIMO_OFDM.
        ///    Get Function: niWLANA_GetOFDMDemodHTSIGCRCPassed
        /// 
        /// </summary>
        public int GetResultOfdmDemodHtSigCrcPassed(string channel, out int value)
        {
            return GetInt(niWLANAProperties.ResultOfdmDemodHtSigCrcPassed, channel, out value);
        }

        private int SetInt(niWLANAProperties propertyId, string repeatedCapabilityOrChannel, int value)
        {
            return TestForError(PInvoke.niWLANA_SetScalarAttributeI32(Handle, repeatedCapabilityOrChannel, propertyId, value));
        }

        private int SetInt(niWLANAProperties propertyId, int value)
        {
            return this.SetInt(propertyId, "", value);
        }

        private int GetInt(niWLANAProperties propertyId, string repeatedCapabilityOrChannel, out int value)
        {
            return TestForError(PInvoke.niWLANA_GetScalarAttributeI32(Handle, repeatedCapabilityOrChannel, propertyId, out value));
        }

        private int GetInt(niWLANAProperties propertyId, out int value)
        {
            return this.GetInt(propertyId, "", out value);
        }

        private int SetDouble(niWLANAProperties propertyId, string repeatedCapabilityOrChannel, double value)
        {
            return TestForError(PInvoke.niWLANA_SetScalarAttributeF64(Handle, repeatedCapabilityOrChannel, propertyId, value));
        }

        private int SetDouble(niWLANAProperties propertyId, double value)
        {
            return this.SetDouble(propertyId, "", value);
        }

        private int GetDouble(niWLANAProperties propertyId, string repeatedCapabilityOrChannel, out double value)
        {
            return TestForError(PInvoke.niWLANA_GetScalarAttributeF64(Handle, repeatedCapabilityOrChannel, propertyId, out value));

        }

        private int GetDouble(niWLANAProperties propertyId, out double value)
        {
            return this.GetDouble(propertyId, "", out value);
        }

        private int SetVectorDouble(string channelString, niWLANAProperties propertyId, double [] data, int dataSize)
        {
            return TestForError(PInvoke.niWLANA_SetVectorAttributeF64(Handle, channelString, propertyId, data, dataSize));
        }

        private int GetVectorDouble(string channelString, niWLANAProperties propertyId, double[] data, int dataSize, out int actualNumDataArrayElements)
        {
            return TestForError(PInvoke.niWLANA_GetVectorAttributeF64(Handle, channelString, propertyId, data, dataSize, out actualNumDataArrayElements));
        }



        /// <summary>
        /// 
        /// Closes the niGSM generation session and releases resources associated with that session. Call this function once for each unique named session that you have created.
        /// 
        /// </summary>
        /// <returns>
        /// Returns the status code of this operation. The status code either indicates success or describes an error or warning condition. Examine the status code from each call to an niGSM generation function to determine if an error has occurred.
        /// To obtain a text description of the status code and additional information about the error condition, call the GetErrorString function.
        /// The general meaning of the status code is as follows:
        /// 
        /// Value           Meaning
        /// ----------------------------------------
        /// 0               Success 
        /// Positive Values Warnings 
        /// Negative Values Exception will be thrown
        /// </returns>
        public void Close()
        {
            if (!_isNamedSession)
                Dispose();
            else
            {
                if (!Handle.Handle.Equals(IntPtr.Zero))
                    PInvoke.niWLANA_CloseSession(Handle);
            }
        }

        #region IDisposable Members


        ///<summary>
        /// Closes the niWLAN Analysis unnamed session and releases resources associated with that unnamed session.
        ///
        ///</summary>


        public void Dispose()
        {
            if (!_isNamedSession)
            {
                this.Dispose(true);
                System.GC.SuppressFinalize(this);
            }
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose managed resources.
            }
            // Dispose() does not close a named session. Users must call Close() to close a named session.
            if (!_isNamedSession)
            {
                // Dispose unmanaged resources
                // Handle.Handle is IntPtr.Zero when the session is inactive/closed.
                if (!Handle.Handle.Equals(IntPtr.Zero))
                {
                    PInvoke.niWLANA_CloseSession(Handle);
                }
            }
        }

        #endregion

        private class PInvoke
        {
            const string nativeDllName = "niWLANAnalysis_net.dll";

            [DllImport(nativeDllName, EntryPoint = "niWLANA_AnalyzeIQComplexF64", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_AnalyzeIQComplexF64(System.Runtime.InteropServices.HandleRef session, double t0, double dt, niComplexNumber[] data, int numberofSamples, int reset, out int averagingDone);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_AnalyzeMIMOIQComplexF64", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_AnalyzeMIMOIQComplexF64(System.Runtime.InteropServices.HandleRef session, double[] t0, double[] dt, niComplexNumber[] waveforms, int numberofChannels, int numberofSamplesInEachWfm, int reset, out int averagingDone);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_AnalyzeMIMOPowerSpectrum", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_AnalyzeMIMOPowerSpectrum(System.Runtime.InteropServices.HandleRef session, double[] f0, double[] df, double[] powerSpectra, int numberofChannels, int numofSamplesInEachSpectrum);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_AnalyzePowerSpectrum", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_AnalyzePowerSpectrum(System.Runtime.InteropServices.HandleRef session, double f0, double df, double[] powerSpectrumData, int dataArraySize);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_CloseSession", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_CloseSession(System.Runtime.InteropServices.HandleRef session);

            [Obsolete]
            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetCurrentIterationConstellation", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetCurrentIterationConstellation(System.Runtime.InteropServices.HandleRef session, string channelString, [In, Out] double[] iData, [In, Out] double[] qData, int dataArraySize, out int actualNumDataArrayElements);

            [Obsolete]
            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetCurrentIterationEVMPerSymbol", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetCurrentIterationEVMPerSymbol(System.Runtime.InteropServices.HandleRef session, string channelString, [In, Out] double[] eVMperSymbol, int eVMperSymbolArraySize, out int actualArraySize);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetCurrentIterationOFDMDemodNumberOfSpatialStreams", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetCurrentIterationOFDMDemodNumberOfSpatialStreams(System.Runtime.InteropServices.HandleRef session, string channelString, out int numStreams);

            [Obsolete]
            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetCurrentIterationOFDMEVMPerSubcarrier", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetCurrentIterationOFDMEVMPerSubcarrier(System.Runtime.InteropServices.HandleRef session, string channelString, [In, Out] double[] eVMperSubcarrier, int eVMperSubcarrierArraySize, out int actualArraySize);

            [Obsolete]
            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetCurrentIterationOFDMEVMPerSymbolPerSubcarrier", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetCurrentIterationOFDMEVMPerSymbolPerSubcarrier(System.Runtime.InteropServices.HandleRef session, string channelString, out double eVMTrace, int numRows, int numColumns, out int actualNumRows, out int actualNumColumns);

            [Obsolete]
            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetCurrentIterationPvT", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetCurrentIterationPvT(System.Runtime.InteropServices.HandleRef session, string channelString, out double t0, out double dt, [In, Out] double[] data, int dataArraySize, out int actualNumDataArrayElements);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetErrorString", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetErrorString(System.Runtime.InteropServices.HandleRef session, int errorCode, StringBuilder errorMessage, int errorMessageLength);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetScalarAttributeF64", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetScalarAttributeF64(System.Runtime.InteropServices.HandleRef session, string channelString, niWLANAProperties attributeID, out double attributeValue);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetScalarAttributeI32", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetScalarAttributeI32(System.Runtime.InteropServices.HandleRef session, string channelString, niWLANAProperties attributeID, out int attributeValue);

            [Obsolete]
            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetSpectralMask", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetSpectralMask(System.Runtime.InteropServices.HandleRef session, string channelString, out double f0, out double df, [In, Out] double[] spectralMask, [In, Out] double[] spectrum, int dataArraySize, out int actualNumDataArrayElements);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetSpectralMaskMargin", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetSpectralMaskMargin(System.Runtime.InteropServices.HandleRef session, string channelString, out double spectralMaskMargin);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetTxPowerMeasurementAveragePower", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetTxPowerMeasurementAveragePower(System.Runtime.InteropServices.HandleRef session, string channelString, out double averagePower);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetVectorAttributeF64", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetVectorAttributeF64(System.Runtime.InteropServices.HandleRef session, string channelString, niWLANAProperties attributeID, [In, Out] double[] data, int dataArraySize, out int actualNumDataArrayElements);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_OpenSession", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_OpenSession(string sessionName, int compatibilityVersion, out IntPtr session, out int isNewSession);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_ResetSession", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_ResetSession(System.Runtime.InteropServices.HandleRef session);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_RFSAAutoLevel", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_RFSAAutoLevel(System.Runtime.InteropServices.HandleRef rFSASession, string hardwareChannelString, double carrierFrequency, double bandwidth, double measurementInterval, int maxNumberofIterations, out double resultantReferenceLevel);
			
            [DllImport(nativeDllName, EntryPoint = "niWLANSA_RFSAAutoLevel", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_RFSAAutoLevel(System.Runtime.InteropServices.HandleRef rFSASession, string hardwareChannelString, double bandwidth, double measurementInterval, int maxNumberofIterations, out double resultantReferenceLevel);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_RFSAAutoRange", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_RFSAAutoRange(System.Runtime.InteropServices.HandleRef session, string wLANChannelString, System.Runtime.InteropServices.HandleRef rFSASession, string hardwareChannelString);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_RFSAConfigure", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_RFSAConfigure(System.Runtime.InteropServices.HandleRef session, string wLANChannelString, System.Runtime.InteropServices.HandleRef rFSASession, string hardwareChannelString, int resetHardware, out long samplesPerRecord);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_RFSAConfigureHardware", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_RFSAConfigureHardware(HandleRef WLANSession, System.Runtime.InteropServices.HandleRef rFSASession, string hardwareChannelString, out long samplesPerRecord);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_RFSAMeasure", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_RFSAMeasure(System.Runtime.InteropServices.HandleRef session, System.Runtime.InteropServices.HandleRef rFSASession, string hardwareChannelString, double timeout);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_RFSAMIMOMeasure", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_RFSAMIMOMeasure(System.Runtime.InteropServices.HandleRef session, IntPtr[] rFSASessions, string[] hardwareChannelStrings, int numberofChannels, double timeout);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_RFSAReadGatedPowerSpectrum", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_RFSAReadGatedPowerSpectrum(System.Runtime.InteropServices.HandleRef session, string wLANChannelString, System.Runtime.InteropServices.HandleRef rFSASession, string hardwareChannelString, double timeout, out double f0, out double df, [In, Out] double[] powerSpectrum, int powerSpectrumArraySize, out int actualNumPowerSpectrumElement);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_RFSAReadMIMOGatedPowerSpectrum", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_RFSAReadMIMOGatedPowerSpectrum(System.Runtime.InteropServices.HandleRef session, string wLANChannelStrings, IntPtr[] rFSASessions, string hwChannelStrings, double timeout, [In, Out] double[] f0, [In, Out] double[] df, [In, Out] double[] powerSpectra, int numberofChannels, int individualSpectrumSize, out int actualNumSamplesInEachSpec);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SelectMeasurements", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SelectMeasurements(System.Runtime.InteropServices.HandleRef session, uint measurement);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetAcquisitionLength", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetAcquisitionLength(System.Runtime.InteropServices.HandleRef session, string channelString, double acquisitionLength);

            [Obsolete]
            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetCarrierFrequency", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetCarrierFrequency(System.Runtime.InteropServices.HandleRef session, string channelString, double carrierFrequency);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetChannelBandwidth", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetChannelBandwidth(System.Runtime.InteropServices.HandleRef session, string channelString, double channelBandwidth);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetDSSSDemodEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetDSSSDemodEnabled(System.Runtime.InteropServices.HandleRef session, string channelString, int enabled);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetDSSSDemodNumberOfAverages", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetDSSSDemodNumberOfAverages(System.Runtime.InteropServices.HandleRef session, string channelString, int numberofAverages);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetDSSSPowerRampMeasurementEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetDSSSPowerRampMeasurementEnabled(System.Runtime.InteropServices.HandleRef session, string channelString, int enabled);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetDSSSPowerRampMeasurementNumberOfAverages", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetDSSSPowerRampMeasurementNumberOfAverages(System.Runtime.InteropServices.HandleRef session, string channelString, int numberofAverages);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetNumberOfReceiveChannels", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetNumberOfReceiveChannels(System.Runtime.InteropServices.HandleRef session, string channelString, int numberofReceiveChannels);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetOFDMDemodEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetOFDMDemodEnabled(System.Runtime.InteropServices.HandleRef session, string channelString, int enabled);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetOFDMDemodNumberOfAverages", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetOFDMDemodNumberOfAverages(System.Runtime.InteropServices.HandleRef session, string channelString, int numberofAverages);

            [Obsolete]
            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetPowerLevel", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetPowerLevel(System.Runtime.InteropServices.HandleRef session, string channelString, double maxInputPower);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetScalarAttributeF64", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetScalarAttributeF64(System.Runtime.InteropServices.HandleRef session, string channelString, niWLANAProperties attributeID, double attributeValue);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetScalarAttributeI32", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetScalarAttributeI32(System.Runtime.InteropServices.HandleRef session, string channelString, niWLANAProperties attributeID, int attributeValue);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetSpectralMaskEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetSpectralMaskEnabled(System.Runtime.InteropServices.HandleRef session, string channelString, int enabled);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetSpectralMaskNumberOfAverages", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetSpectralMaskNumberOfAverages(System.Runtime.InteropServices.HandleRef session, string channelString, int numberofAverages);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetStandard", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetStandard(System.Runtime.InteropServices.HandleRef session, string channelString, int standard);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetTxPowerMeasurementEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetTxPowerMeasurementEnabled(System.Runtime.InteropServices.HandleRef session, string channelString, int enabled);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetTxPowerMeasurementNumberOfAverages", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetTxPowerMeasurementNumberOfAverages(System.Runtime.InteropServices.HandleRef session, string channelString, int numberofAverages);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetVectorAttributeF64", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetVectorAttributeF64(System.Runtime.InteropServices.HandleRef session, string channelString, niWLANAProperties attributeID, double[] data, int dataArraySize);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SelectMeasurementsWithTraces", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SelectMeasurementsWithTraces(System.Runtime.InteropServices.HandleRef session, int measurement, int enableTraces);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetCurrentIterationOFDMDemodNumberOfSpaceTimeStreams", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetCurrentIterationOFDMDemodNumberOfSpaceTimeStreams(System.Runtime.InteropServices.HandleRef session, string channelString, out int numStreams);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetCurrentIterationConstellationTrace", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetCurrentIterationConstellationTrace(System.Runtime.InteropServices.HandleRef session, string channelString, [In, Out] double[] iData, [In, Out] double[] qData, int dataArraySize, out int actualArraySize);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetCurrentIterationPvTTrace", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetCurrentIterationPvTTrace(System.Runtime.InteropServices.HandleRef session, string channelString, out double t0, out double dt, [In, Out] double[] pvT, int dataArraySize, out int actualArraySize);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetCurrentIterationEVMPerSymbolTrace", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetCurrentIterationEVMPerSymbolTrace(System.Runtime.InteropServices.HandleRef session, string channelString, [In, Out] int[] index, [In, Out] double[] eVMperSymbol, int dataArraySize, out int actualArraySize);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetCurrentIterationDecodedBitsTrace", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetCurrentIterationDecodedBitsTrace(System.Runtime.InteropServices.HandleRef session, [In, Out] int[] decodedbits, int dataArraySize, out int actualArraySize);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetSpectralMaskTrace", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetSpectralMaskTrace(System.Runtime.InteropServices.HandleRef session, string channelString, out double f0, out double df, [In, Out] double[] spectralMask, [In, Out] double[] spectrum, int dataArraySize, out int actualArraySize);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetCurrentIterationOFDMDemodEVMPerSymbolPerSubcarrierTrace", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetCurrentIterationOFDMDemodEVMPerSymbolPerSubcarrierTrace(System.Runtime.InteropServices.HandleRef session, string channelString, [In, Out] int[] index, out double eVMTrace, int numRows, int numColumns, out int actualNumRows, out int actualNumColumns);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetCurrentIterationOFDMDemodEVMPerSubcarrierTrace", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetCurrentIterationOFDMDemodEVMPerSubcarrierTrace(System.Runtime.InteropServices.HandleRef session, string channelString, [In, Out] int[] index, [In, Out] double[] eVMperSubcarrier, int dataArraySize, out int actualArraySize);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetCurrentIterationOFDMDemodDataEVMPerSymbolTrace", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetCurrentIterationOFDMDemodDataEVMPerSymbolTrace(System.Runtime.InteropServices.HandleRef session, string channelString, [In, Out] int[] index, [In, Out] double[] dataEVMperSymbol, int dataArraySize, out int actualArraySize);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetCurrentIterationOFDMDemodPilotEVMPerSymbolTrace", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetCurrentIterationOFDMDemodPilotEVMPerSymbolTrace(System.Runtime.InteropServices.HandleRef session, string channelString, [In, Out] int[] index, [In, Out] double[] pilotEVMperSymbol, int dataArraySize, out int actualArraySize);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetCurrentIterationOFDMDemodChannelFrequencyResponseTrace", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetCurrentIterationOFDMDemodChannelFrequencyResponseTrace(System.Runtime.InteropServices.HandleRef session, string channelString, [In, Out] int[] index, [In, Out] double[] magnitude, [In, Out] double[] phase, int dataArraySize, out int actualArraySize);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetCurrentIterationOFDMDemodSpectralFlatnessTrace", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetCurrentIterationOFDMDemodSpectralFlatnessTrace(System.Runtime.InteropServices.HandleRef session, string channelString, [In, Out] int[] index, [In, Out] double[] upperMask, [In, Out] double[] spectralFlatness, [In, Out] double[] lowerMask, int dataArraySize, out int actualArraySize);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SaveConfigurationToFile", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SaveConfigurationToFile(System.Runtime.InteropServices.HandleRef session, string filePath, int operation);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_LoadConfigurationFromFile", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_LoadConfigurationFromFile(System.Runtime.InteropServices.HandleRef session, string filePath, int reset);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_ReadWaveformFromFile", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_ReadWaveformFromFile(string filePath, string waveformName, int offset, int count, out double t0, out double dt, out niComplexNumber waveform, int waveformSize, out int actualSize, out int eOF);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_ChannelNumberToCarrierFrequency", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_ChannelNumberToCarrierFrequency(int frequencyBand, double channelBandwidth, int channelNumber, int secondaryFactor, double channelStartingFactor, out double carrierFrequency);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetDSSSDemodCarrierFrequencyOffset", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetDSSSDemodCarrierFrequencyOffset(System.Runtime.InteropServices.HandleRef session, string channelString, out double carrierFrequencyOffset);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetOFDMDemodAutoComputeAcquisitionLengthEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetOFDMDemodAutoComputeAcquisitionLengthEnabled(System.Runtime.InteropServices.HandleRef session, string channelString, int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetOFDMDemodAutoComputeAcquisitionLengthEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetOFDMDemodAutoComputeAcquisitionLengthEnabled(System.Runtime.InteropServices.HandleRef session, string channelString, out int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetDSSSDemodAutoComputeAcquisitionLengthEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetDSSSDemodAutoComputeAcquisitionLengthEnabled(System.Runtime.InteropServices.HandleRef session, string channelString, int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetDSSSDemodAutoComputeAcquisitionLengthEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetDSSSDemodAutoComputeAcquisitionLengthEnabled(System.Runtime.InteropServices.HandleRef session, string channelString, out int value);
            #region Version 4.0

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetSpectralMaskMarginFrequencyVector", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetSpectralMaskMarginFrequencyVector(HandleRef session, string channelString, [In, Out] double[] dataArray, int dataArraySize, out int actualArraySize);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetSpectralMaskMarginPowerSpectralDensityVector", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetSpectralMaskMarginPowerSpectralDensityVector(HandleRef session, string channelString, [In, Out] double[] dataArray, int dataArraySize, out int actualArraySize);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetSpectralMaskFrequencyOffsetsUsed", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetSpectralMaskFrequencyOffsetsUsed(HandleRef session, string channelString, [In, Out] double[] dataArray, int dataArraySize, out int actualArraySize);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetSpectralMaskPowerOffsetsUsed", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetSpectralMaskPowerOffsetsUsed(HandleRef session, string channelString, [In, Out] double[] dataArray, int dataArraySize, out int actualArraySize);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetResultSpectralMaskViolation", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetResultSpectralMaskViolation(HandleRef session, string channelString, out  double val);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetSpectralMaskMarginVector", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetSpectralMaskMarginVector(HandleRef session, string channelString, [In, Out] double[] dataArray, int dataArraySize, out int actualArraySize);
            
            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetCurrentIterationOFDMDemodPreambleFrequencyErrorTrace", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetCurrentIterationOFDMDemodPreambleFrequencyErrorTrace(HandleRef session, string channelString, [In, Out] double[] time, [In, Out]double[] preambleFrequencyError, int dataArraySize, out int actualArraySize);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetCurrentIterationOFDMDemodCommonPilotErrorTrace", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetCurrentIterationOFDMDemodCommonPilotErrorTrace(HandleRef session, string channelString, [In, Out]int[] index, [In, Out]double[] CPEMagnitude, [In, Out]double[] CPEPhase, int dataArraySize, out int actualArraySize);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetCurrentIterationOFDMDemodPhaseNoisePSDTrace", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetCurrentIterationOFDMDemodPhaseNoisePSDTrace(HandleRef session, string channelString, out double f0, out double df, [In, Out] double[] phaseNoisePSD, int dataArraySize, out int actualArraySize);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetOFDMDemodCarrierFrequencyOffsetEstimationEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetOFDMDemodCarrierFrequencyOffsetEstimationEnabled(HandleRef session, string channelString, out int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetOFDMDemodCarrierFrequencyOffsetEstimationEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetOFDMDemodCarrierFrequencyOffsetEstimationEnabled(HandleRef session, string channelString, int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetOFDMDemodSampleClockOffsetEstimationEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetOFDMDemodSampleClockOffsetEstimationEnabled(HandleRef session, string channelString, out int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetOFDMDemodSampleClockOffsetEstimationEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetOFDMDemodSampleClockOffsetEstimationEnabled(HandleRef session, string channelString, int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetOFDMDemodIQGainImbalanceEstimationEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetOFDMDemodIQGainImbalanceEstimationEnabled(HandleRef session, string channelString, out int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetOFDMDemodIQGainImbalanceEstimationEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetOFDMDemodIQGainImbalanceEstimationEnabled(HandleRef session, string channelString, int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetOFDMDemodQuadratureSkewEstimationEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetOFDMDemodQuadratureSkewEstimationEnabled(HandleRef session, string channelString, out int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetOFDMDemodQuadratureSkewEstimationEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetOFDMDemodQuadratureSkewEstimationEnabled(HandleRef session, string channelString, int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetOFDMDemodTimingSkewEstimationEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetOFDMDemodTimingSkewEstimationEnabled(HandleRef session, string channelString, out int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetOFDMDemodTimingSkewEstimationEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetOFDMDemodTimingSkewEstimationEnabled(HandleRef session, string channelString, int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetOFDMDemodCarrierFrequencyLeakageEstimationEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetOFDMDemodCarrierFrequencyLeakageEstimationEnabled(HandleRef session, string channelString, out int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetOFDMDemodCarrierFrequencyLeakageEstimationEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetOFDMDemodCarrierFrequencyLeakageEstimationEnabled(HandleRef session, string channelString, int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetOFDMDemodCommonPilotErrorEstimationEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetOFDMDemodCommonPilotErrorEstimationEnabled(HandleRef session, string channelString, out int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetOFDMDemodCommonPilotErrorEstimationEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetOFDMDemodCommonPilotErrorEstimationEnabled(HandleRef session, string channelString, int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetOFDMDemodIQGainImbalanceCompensationEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetOFDMDemodIQGainImbalanceCompensationEnabled(HandleRef session, string channelString, out int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetOFDMDemodIQGainImbalanceCompensationEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetOFDMDemodIQGainImbalanceCompensationEnabled(HandleRef session, string channelString, int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetOFDMDemodQuadratureSkewCompensationEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetOFDMDemodQuadratureSkewCompensationEnabled(HandleRef session, string channelString, out int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetOFDMDemodQuadratureSkewCompensationEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetOFDMDemodQuadratureSkewCompensationEnabled(HandleRef session, string channelString, int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetOFDMDemodTimingSkewCompensationEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetOFDMDemodTimingSkewCompensationEnabled(HandleRef session, string channelString, out int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetOFDMDemodTimingSkewCompensationEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetOFDMDemodTimingSkewCompensationEnabled(HandleRef session, string channelString, int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetOFDMDemodIQMismatchSignalModel", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetOFDMDemodIQMismatchSignalModel(HandleRef session, string channelString, out int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetOFDMDemodIQMismatchSignalModel", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetOFDMDemodIQMismatchSignalModel(HandleRef session, string channelString, int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetOFDMDemodPreambleFrequencyErrorTraceEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetOFDMDemodPreambleFrequencyErrorTraceEnabled(HandleRef session, string channelString, out int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetOFDMDemodPreambleFrequencyErrorTraceEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetOFDMDemodPreambleFrequencyErrorTraceEnabled(HandleRef session, string channelString, int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetOFDMDemodCommonPilotErrorTraceEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetOFDMDemodCommonPilotErrorTraceEnabled(HandleRef session, string channelString, out int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetOFDMDemodCommonPilotErrorTraceEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetOFDMDemodCommonPilotErrorTraceEnabled(HandleRef session, string channelString, int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetOFDMDemodPhaseNoisePSDTraceEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetOFDMDemodPhaseNoisePSDTraceEnabled(HandleRef session, string channelString, out int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetOFDMDemodPhaseNoisePSDTraceEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetOFDMDemodPhaseNoisePSDTraceEnabled(HandleRef session, string channelString, int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetGatedSpectrumAveragingType", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetGatedSpectrumAveragingType(HandleRef session, string channelString, out int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetGatedSpectrumAveragingType", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetGatedSpectrumAveragingType(HandleRef session, string channelString, int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetNumberOfSegments", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetNumberOfSegments(HandleRef session, string channelString, out int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetNumberOfSegments", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetNumberOfSegments(HandleRef session, string channelString, int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetOFDMLSIGPayloadLength", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetOFDMLSIGPayloadLength(HandleRef session, string channelString, out int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetOFDMLSIGPayloadLength", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetOFDMLSIGPayloadLength(HandleRef session, string channelString, int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetSTBCAllStreamsEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetSTBCAllStreamsEnabled(HandleRef session, string channelString, out int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetSTBCAllStreamsEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetSTBCAllStreamsEnabled(HandleRef session, string channelString, int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetNumberOfSpaceTimeStreams", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetNumberOfSpaceTimeStreams(HandleRef session, string channelString, out int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetNumberOfSpaceTimeStreams", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetNumberOfSpaceTimeStreams(HandleRef session, string channelString, int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetShortGuardIntervalB1Bit", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetShortGuardIntervalB1Bit(HandleRef session, string channelString, out int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetShortGuardIntervalB1Bit", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetShortGuardIntervalB1Bit(HandleRef session, string channelString, int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetAggregationBit", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetAggregationBit(HandleRef session, string channelString, out int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetAggregationBit", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetAggregationBit(HandleRef session, string channelString, int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetSwapIAndQEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetSwapIAndQEnabled(HandleRef session, string channelString, out int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetSwapIAndQEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetSwapIAndQEnabled(HandleRef session, string channelString, int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetSampleClockRateFactor", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetSampleClockRateFactor(HandleRef session, string channelString, out double value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_SetSampleClockRateFactor", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_SetSampleClockRateFactor(HandleRef session, string channelString, double value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetOFDMDemodLSIGPayloadLength", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetOFDMDemodLSIGPayloadLength(HandleRef session, string channelString, out  int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetOFDMDemodSTBCAllStreamsEnabled", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetOFDMDemodSTBCAllStreamsEnabled(HandleRef session, string channelString, out  int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetOFDMDemodVHTSIGACRCPassed", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetOFDMDemodVHTSIGACRCPassed(HandleRef session, string channelString, out  int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetOFDMDemodVHTSIGBCRCPassed", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetOFDMDemodVHTSIGBCRCPassed(HandleRef session, string channelString, out  int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetOFDMDemodTimingSkew", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetOFDMDemodTimingSkew(HandleRef session, string channelString, out double value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetOFDMDemodCommonPilotErrorRMS", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetOFDMDemodCommonPilotErrorRMS(HandleRef session, string channelString, out double value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetOFDMDemodSpectralFlatnessMarginSubcarrierIndex", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetOFDMDemodSpectralFlatnessMarginSubcarrierIndex(HandleRef session, string channelString, out int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetOFDMDemodNumberOfOFDMSymbols", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetOFDMDemodNumberOfOFDMSymbols(HandleRef session, string channelString, out int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetOFDMDemodAggregation", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetOFDMDemodAggregation(HandleRef session, string channelString, out  int value);

            [DllImport(nativeDllName, EntryPoint = "niWLANA_GetOFDMDemodShortGuardIntervalB1Bit", CallingConvention = CallingConvention.StdCall)]
            public static extern int niWLANA_GetOFDMDemodShortGuardIntervalB1Bit(HandleRef session, string channelString, out  int value);

            #endregion

        }

        public int TestForError(int status)
        {
            if ((status < 0))
            {
                System.Text.StringBuilder msg = new System.Text.StringBuilder();
                status = GetErrorString(status, msg);
                throw new System.Runtime.InteropServices.ExternalException(msg.ToString(), status);
            }
            return status;
        }

        private int TestForError(int status, HandleRef rfsaHandle)
        {
            if ((status < 0))
            {
                System.Text.StringBuilder msg = new System.Text.StringBuilder();
                GetErrorString(status, msg);
                //get rfsa detailed error message
                if (String.IsNullOrEmpty(msg.ToString()))
                    niRFSA.GetError(rfsaHandle, status, msg);
                //get rfsa general error message
                if (String.IsNullOrEmpty(msg.ToString()))
                    niRFSA.ErrorMessage(rfsaHandle, status, msg);
                throw new System.Runtime.InteropServices.ExternalException(msg.ToString(), status);
            }
            return status;
        }

        private int TestForError(int status, HandleRef[] rfsaHandles)
        {
            if ((status < 0))
            {
                System.Text.StringBuilder msg = new System.Text.StringBuilder();
                for (int i = 0; String.IsNullOrEmpty(msg.ToString()) && i < rfsaHandles.Length; i++)
                {
                    if (rfsaHandles[i].Handle != IntPtr.Zero)
                    {
                        //get rfsa detailed error message
                        niRFSA.GetError(rfsaHandles[i], status, msg);
                        //get rfsa general error message
                        if (String.IsNullOrEmpty(msg.ToString()))
                            niRFSA.ErrorMessage(rfsaHandles[i], status, msg);
                    }
                }
                if (String.IsNullOrEmpty(msg.ToString()))
                    GetErrorString(status, msg);
                throw new System.Runtime.InteropServices.ExternalException(msg.ToString(), status);
            }
            return status;
        }
    }

    public class niWLANAConstants
    {
        public const int CompatibilityVersion010000 = 10000;

        public const int CompatibilityVersion020000 = 20000;

        public const int CompatibilityVersion030000 = 30000;

        [Obsolete]
        public const int Standard80211agOfdm = 0;

        public const int Standard80211bgDsss = 1;

        public const int Standard80211gDsssOfdm = 2;

        public const int Standard80211nMimoOfdm = 3;

        public const int AcquisitionTypeIq = 0;

        public const int AcquisitionTypeSpectrum = 1;

        public const int True = 1;

        public const int False = 0;

        public const int OfdmDataRate6 = 0;

        public const int OfdmDataRate9 = 1;

        public const int OfdmDataRate12 = 2;

        public const int OfdmDataRate18 = 3;

        public const int OfdmDataRate24 = 4;

        public const int OfdmDataRate36 = 5;

        public const int OfdmDataRate48 = 6;

        public const int OfdmDataRate54 = 7;

        public const int FecCodingTypeBcc = 0;

        public const int FecCodingTypeLdpc = 1;

        public const int ResultsFecCodingTypeVarious = -1;

        public const int DsssDataRate1 = 0;

        public const int DsssDataRate2 = 1;

        public const int DsssDataRate5p5Cck = 2;

        public const int DsssDataRate5p5Pbcc = 3;

        public const int DsssDataRate11Cck = 4;

        public const int DsssDataRate11Pbcc = 5;

        public const int DsssDataRate22 = 6;

        public const int DsssDataRate33 = 7;

        public const int RbwDefinition3db = 0;

        public const int RbwDefinition6db = 1;

        public const int RbwDefinitionEnbw = 2;

        public const int RbwDefinitionBinWidth = 3;

        public const int WindowUniform = 0;

        public const int WindowHanning = 1;

        public const int WindowHamming = 2;

        public const int WindowBlackmanHarris = 3;

        public const int WindowExactBlackman = 4;

        public const int WindowBlackman = 5;

        public const int WindowFlatTop = 6;

        public const int Window4TermBlackmanHarris = 7;

        public const int Window7TermBlackmanHarris = 8;

        public const int WindowLowSideLobe = 9;

        public const int StandardUnknown = -2;

        public const int GatedSpectrumModeRbw = 0;

        public const int GatedSpectrumModeAcquisitionLength = 1;

        public const int GatedSpectrumModeRbwAndAcquisitionLength = 2;

        public const int SpectralMaskTypeStandard = 0;

        public const int SpectralMaskTypeUserDefined = 1;

        public const int SpectralMaskReferenceLevelTypePeakSignalPower = 0;

        public const int SpectralMaskReferenceLevelTypeUserDefined = 1;

        public const int OfdmPhaseTrackingStandard = 0;

        public const int OfdmPhaseTrackingInstantaneous = 1;

        public const int Results80211nPlcpFrameFormatMixed = 0;

        public const int Results80211nPlcpFrameFormatGreenfield = 1;

        public const int Results80211nPlcpFrameFormatVarious = -1;

        public const int Results80211nPlcpFrameFormatUnknown = -2;

        public const int ResultsOfdmDataRateVarious = 8;

        public const int FilterRectangular = 0;

        public const int FilterRaisedCosine = 1;

        public const int FilterRootRaisedCosine = 2;

        public const int FilterGaussian = 3;

        public const int DsssPhaseTrackingStandard = 0;

        public const int DsssPhaseTrackingInstantaneous = 1;

        public const int ResultsDsssDataRateVarious = 8;

        public const int PreambleTypeLongPreamble = 1;

        public const int PreambleTypeShortPreamble = 0;

        public const int PreambleTypeVarious = 2;

        public const int DsssDemodSfdFoundFalse = 0;

        public const int DsssDemodSfdFoundTrue = 1;

        public const int DsssDemodSfdFoundVarious = 2;

        public const int DsssDemodHeaderChecksumPassedFalse = 0;

        public const int DsssDemodHeaderChecksumPassedTrue = 1;

        public const int DsssDemodHeaderChecksumPassedVarious = 2;

        public const int OfdmDemodMeasurement = 0x1;

        public const int OfdmDemodWithGatedPowerMeasurement = 0x2;

        public const int DsssDemodMeasurement = 0x4;

        public const int DsssDemodWithGatedPowerMeasurement = 0x8;

        public const int DsssPowerRampMeasurement = 0x10;

        public const int TxpPowerMeasurement = 0x20;

        public const int SpectralMaskMeasurement = 0x40;

        public const int ObwMeasurement = 0x80;

        public const int MaxSpectralDensityMeasurement = 0x100;

        public const int CfoEstimationMethodPreambleOnly = 0;

        public const int CfoEstimationMethodPreambleAndData = 1;

        public const int Various = -1;

        public const int NotApplicable = -3;

        public const int Standard80211agjpOfdm = 0;

        #region version 4.0

        public const int Standard80211acMimoOfdm = 4;

        /*- Values for Gated Spectrum Averaging Type -*/
        public const int GatedSpectrumAveragingTypeRms = 0;

        public const int GatedSpectrumAveragingTypeLog = 1;

        public const int SpectralMaskTypeStandardAt5Ghz = 2;

        public const int OfdmPhaseTrackingNone = 2;
        public const int OfdmPhaseTrackingStandardWithCubicSplineFit = 3;

        /*- Values for OFDM IQ Mismatch Correction Signal Model  */
        public const int OfdmIqMismatchCorrectionSignalModelTx = 0;
        public const int OfdmIqMismatchCorrectionSignalModelRx = 1;

        #endregion
    }

    public enum niWLANAProperties
    {

        /// <summary>
        /// double
        /// </summary>
        [Obsolete]
        CarrierFrequency = 0,

        /// <summary>
        /// double
        /// </summary>
        AcquisitionLength = 2,

        /// <summary>
        /// int
        /// </summary>
        [Obsolete]
        IqPowerEdgeReferenceTriggerEnabled = 3,

        /// <summary>
        /// double
        /// </summary>
        Span = 5,

        /// <summary>
        /// double
        /// </summary>
        Rbw = 6,

        /// <summary>
        /// int
        /// </summary>
        Standard = 7,

        /// <summary>
        /// int
        /// </summary>
        SpectralMeasurementsAllEnabled = 8,

        /// <summary>
        /// int
        /// </summary>
        FftWindowType = 9,

        /// <summary>
        /// int
        /// </summary>
        SpectralMaskEnabled = 10,

        /// <summary>
        /// int
        /// </summary>
        SpectralMaskNumberOfAverages = 11,

        /// <summary>
        /// int
        /// </summary>
        ObwEnabled = 16,

        /// <summary>
        /// int
        /// </summary>
        ObwNumberOfAverages = 18,

        /// <summary>
        /// int
        /// </summary>
        MaxSpectralDensityEnabled = 19,

        /// <summary>
        /// int
        /// </summary>
        MaxSpectralDensityNumberOfAverages = 20,

        /// <summary>
        /// double
        /// </summary>
        ResultMaxSpectralDensity = 21,

        /// <summary>
        /// double
        /// </summary>
        ResultSpectralMaskMargin = 22,

        /// <summary>
        /// double
        /// </summary>
        ResultObw = 25,

        /// <summary>
        /// int
        /// </summary>
        DsssPowerRampMeasurementEnabled = 31,

        /// <summary>
        /// int
        /// </summary>
        DsssPowerRampMeasurementNumberOfAverages = 32,

        /// <summary>
        /// int
        /// </summary>
        TxpowerMeasurementsEnabled = 33,

        /// <summary>
        /// int
        /// </summary>
        TxpowerMeasurementsNumberOfAverages = 34,

        /// <summary>
        /// double
        /// </summary>
        ResultTxpowerMeasurementsPeakPower = 35,

        /// <summary>
        /// double
        /// </summary>
        ResultTxpowerMeasurementsAveragePower = 36,

        /// <summary>
        /// double
        /// </summary>
        ResultDsssPowerRampUpTime = 41,

        /// <summary>
        /// double
        /// </summary>
        ResultDsssPowerRampDownTime = 42,
        /// <summary>
        /// int
        /// </summary>
        OfdmDemodEnabled = 43,

        /// <summary>
        /// int
        /// </summary>
        OfdmDemodNumberOfAverages = 44,

        /// <summary>
        /// int
        /// </summary>
        OfdmDemodHeaderDetectionEnabled = 45,

        /// <summary>
        /// double
        /// </summary>
        OfdmDemodGatedPowerStartTime = 46,

        /// <summary>
        /// double
        /// </summary>
        OfdmDemodGatedPowerStopTime = 47,

        /// <summary>
        /// int
        /// </summary>
        OfdmPayloadLength = 48,

        /// <summary>
        /// int
        /// </summary>
        OfdmDataRate = 49,

        /// <summary>
        /// int
        /// </summary>
        OfdmDemodMeasurementStartLocation = 50,

        /// <summary>
        /// int
        /// </summary>
        OfdmDemodAmplitudeTrackingEnabled = 52,

        /// <summary>
        /// int
        /// </summary>
        OfdmDemodPhaseTracking = 53,

        /// <summary>
        /// int
        /// </summary>
        OfdmDemodTimeTrackingEnabled = 54,


        /// <summary>
        /// int
        /// </summary>
        OfdmDemodChannelTrackingEnabled = 55,


        /// <summary>
        /// double
        /// </summary>
        ResultOfdmDemodRmsEvm = 56,

        /// <summary>
        /// double
        /// </summary>
        ResultOfdmDemodCarrierFrequencyOffset = 57,

        /// <summary>
        /// double
        /// </summary>
        ResultOfdmDemodCarrierFrequencyLeakage = 58,

        /// <summary>
        /// double
        /// </summary>
        ResultOfdmDemodSpectralFlatnessMargin = 59,

        /// <summary>
        /// double
        /// </summary>
        ResultOfdmDemodSampleClockOffset = 60,

        /// <summary>
        /// double
        /// </summary>
        ResultOfdmDemodIqGainImbalance = 61,

        /// <summary>
        /// double
        /// </summary>
        ResultOfdmDemodQuadratureSkew = 62,
        /// <summary>
        /// int
        /// </summary>
        ResultOfdmDemodPayloadLength = 64,

        /// <summary>
        /// int
        /// </summary>
        ResultOfdmDemodDataRate = 66,

        /// <summary>
        /// int
        /// </summary>
        DsssDemodEnabled = 67,

        /// <summary>
        /// int
        /// </summary>
        DsssDemodNumberOfAverages = 68,

        /// <summary>
        /// int
        /// </summary>
        DsssDemodEqualizationEnabled = 69,

        /// <summary>
        /// int
        /// </summary>
        DsssDemodReferencePulseShapingFilterType = 70,

        /// <summary>
        /// double
        /// </summary>
        DsssDemodReferencePulseShapingFilterCoefficient = 71,

        /// <summary>
        /// int
        /// </summary>
        DsssDemodHeaderDetectionEnabled = 72,

        /// <summary>
        /// int
        /// </summary>
        DsssPayloadLength = 73,

        /// <summary>
        /// int
        /// </summary>
        DsssDataRate = 74,

        /// <summary>
        /// double
        /// </summary>
        DsssDemodGatedPowerStartTime = 75,

        /// <summary>
        /// double
        /// </summary>
        DsssDemodGatedPowerStopTime = 76,

        /// <summary>
        /// int
        /// </summary>
        DsssDemodMeasurementStartLocation = 77,

        /// <summary>
        /// double
        /// </summary>
        ResultDsssDemodRmsEvm = 79,

        /// <summary>
        /// double
        /// </summary>
        ResultDsssDemodPeakEvm = 80,

        /// <summary>
        /// double
        /// </summary>
        ResultDsssDemodCarrierFrequencyOffset = 81,

        /// <summary>
        /// double
        /// </summary>
        ResultDsssDemodIqGainImbalance = 82,

        /// <summary>
        /// double
        /// </summary>
        ResultDsssDemodQuadratureSkew = 83,

        /// <summary>
        /// double
        /// </summary>
        ResultDsssDemodCarrierSuppression = 84,

        /// <summary>
        /// int
        /// </summary>
        ResultDsssDemodPayloadLength = 85,

        /// <summary>
        /// int
        /// </summary>
        ResultDsssDemodDataRate = 87,

        /// <summary>
        /// int
        /// </summary>
        ResultDsssDemodPreambleType = 88,

        /// <summary>
        /// int
        /// </summary>
        ResultDsssDemodSfdFound = 89,

        /// <summary>
        /// int
        /// </summary>
        ResultDsssDemodHeaderChecksumPassed = 90,

        /// <summary>
        /// int
        /// </summary>
        OfdmDemodGatedPowerEnabled = 112,


        /// <summary>
        /// int
        /// </summary>
        DsssDemodGatedPowerEnabled = 113,

        /// <summary>
        /// int
        /// </summary>
        FftWindowSize = 117,

        /// <summary>
        /// int
        /// </summary>
        FftSize = 118,

        /// <summary>
        /// double
        /// </summary>
        DsssPowerRampUpLowThreshold = 135,

        /// <summary>
        /// double
        /// </summary>
        DsssPowerRampUpHighThreshold = 136,

        /// <summary>
        /// double
        /// </summary>
        DsssPowerRampDownLowThreshold = 137,

        /// <summary>
        /// double
        /// </summary>
        DsssPowerRampDownHighThreshold = 138,

        /// <summary>
        /// double
        /// </summary>
        ResultDsssDemod80211bPeakEvm = 140,


        /// <summary>
        /// int
        /// </summary>
        StbcIndex = 141,

        /// <summary>
        /// int
        /// </summary>
        OfdmDemodMaximumSymbolsUsed = 142,

        /// <summary>
        /// int
        /// </summary>
        DsssDemodMaximumChipsUsed = 143,

        /// <summary>
        /// int
        /// </summary>
        DsssDemodPhaseTracking = 145,

        /// <summary>
        /// int
        /// </summary>
        [Obsolete]
        NumberOfIterations = 154,

        /// <summary>
        /// int
        /// </summary>
        RecommendedNumberOfRecords = 154,

        /// <summary>
        /// int
        /// </summary>
        SpectralMaskReferenceLevelType = 155,

        /// <summary>
        /// double
        /// </summary>
        SpectralMaskReferenceLevel = 156,

        /// <summary>
        /// double
        /// </summary>
        ResultDsssDemodSampleClockOffset = 158,

        /// <summary>
        /// double
        /// </summary>
        ResultObwHighFrequency = 159,

        /// <summary>
        /// int
        /// </summary>
        ResultOfdmDemodStbcIndex = 160,

        /// <summary>
        /// int
        /// </summary>
        FecCodingType = 162,

        /// <summary>
        /// int
        /// </summary>
        ResultOfdmDemodFecCodingType = 163,

        /// <summary>
        /// int
        /// </summary>
        OfdmDemodDecodedBitsTraceEnabled = 164,

        /// <summary>
        /// double
        /// </summary>
        ResultOfdmDemodEffectiveDataRate = 168,

        /// <summary>
        /// int
        /// </summary>
        ResultOfdmDemodNotSoundingBit = 169,

        /// <summary>
        /// double
        /// </summary>
        DeviceInstantaneousBandwidth = 170,

        /// <summary>
        /// double
        /// </summary>
        TriggerDelay = 171,

        /// <summary>
        /// int
        /// </summary>
        RecommendedAcquisitionType = 172,

        /// <summary>
        /// double
        /// </summary>
        RecommendedAcquisitionLength = 173,

        /// <summary>
        /// double
        /// </summary>
        RecommendedMinimumQuietTime = 174,

        /// <summary>
        /// int
        /// </summary>
        DsssDemodMacFrameCheckSequenceCheckEnabled = 175,

        /// <summary>
        /// double
        /// </summary>
        RecommendedIqSamplingRate = 176,

        /// <summary>
        /// double
        /// </summary>
        RecommendedIqPreTriggerDelay = 177,

        /// <summary>
        /// double
        /// </summary>
        RecommendedIqPostTriggerDelay = 178,

        /// <summary>
        /// double
        /// </summary>
        RecommendedSpectrumSpan = 179,

        /// <summary>
        /// int
        /// </summary>
        RecommendedSpectrumRbwDefinition = 180,

        /// <summary>
        /// double
        /// </summary>
        RecommendedSpectrumRbw = 181,

        /// <summary>
        /// double
        /// </summary>
        RecommendedSpectrumVbw = 182,

        /// <summary>
        /// int
        /// </summary>
        RecommendedSpectrumFftWindowType = 183,

        /// <summary>
        /// int
        /// </summary>
        AutoSpanEnabled = 184,

        /// <summary>
        /// int
        /// </summary>
        DsssDemodDecodedBitsTraceEnabled = 185,

        /// <summary>
        /// int
        /// </summary>
        OfdmDemodAllTracesEnabled = 187,

        /// <summary>
        /// int
        /// </summary>
        DsssDemodAllTracesEnabled = 188,

        /// <summary>
        /// int
        /// </summary>
        ResultDsssDemodMacFrameCheckSequencePassed = 189,

        /// <summary>
        /// double
        /// </summary>
        ResultDsssDemod80211bPeakEvm2007 = 205,

        /// <summary>
        /// double
        /// </summary>
        ResultObwLowFrequency = 256,

        /// <summary>
        /// double
        /// </summary>
        ResultOfdmDemodAverageGatedPower = 257,

        /// <summary>
        /// double
        /// </summary>
        ResultDsssDemodAverageGatedPower = 258,

        /// <summary>
        /// double
        /// </summary>
        ResultSpectralMaskReferenceLevel = 262,

        /// <summary>
        /// int
        /// </summary>
        SpectralMaskTraceEnabled = 264,

        /// <summary>
        /// int
        /// </summary>
        TxpowerMeasurementsPvtTraceEnabled = 265,

        /// <summary>
        /// int
        /// </summary>
        OfdmDemodConstellationTraceEnabled = 266,

        /// <summary>
        /// int
        /// </summary>
        DsssDemodConstellationTraceEnabled = 267,

        /// <summary>
        /// double
        /// </summary>
        AutorangeMaxAcquisitionLength = 270,

        /// <summary>
        /// double
        /// </summary>
        AutorangeMaxIdleTime = 274,

        /// <summary>
        /// double
        /// </summary>
        ChannelBandwidth = 276,

        /// <summary>
        /// int
        /// </summary>
        NumberOfReceiveChannels = 275,


        /// <summary>
        /// int
        /// </summary>
        OfdmDemod80211nPlcpFrameDetectionEnabled = 279,

        /// <summary>
        /// int
        /// </summary>
        _80211nPlcpFrameFormat = 280,

        /// <summary>
        /// int
        /// </summary>
        OfdmDemodChannelFrequencyResponseTraceEnabled = 281,

        /// <summary>
        /// double
        /// </summary>
        ResultOfdmDemodCrossPower = 288,

        /// <summary>
        /// int
        /// </summary>
        ResultOfdmDemodMcsIndex = 291,

        /// <summary>
        /// double
        /// </summary>
        ResultOfdmDemodGuardInterval = 293,

        /// <summary>
        /// int
        /// </summary>
        ResultOfdmDemodNumberOfExtensionSpatialStreams = 294,
        /// <summary>
        /// int
        /// </summary>
        GatedSpectrumMode = 295,

        /// <summary>
        /// int
        /// </summary>
        RbwDefinition = 296,

        /// <summary>
        /// int
        /// </summary>
        SpectralMaskType = 297,

        /// <summary>
        /// int
        /// </summary>
        McsIndex = 298,

        /// <summary>
        /// double
        /// </summary>
        GuardInterval = 299,

        /// <summary>
        /// int
        /// </summary>
        NumberOfExtensionSpatialStreams = 300,

        /// <summary>
        /// int
        /// </summary>
        OfdmDemodSpectralFlatnessTraceEnabled = 301,

        /// <summary>
        /// int
        /// </summary>
        ResultOfdmDemod80211nPlcpFrameFormat = 303,

        /// <summary>
        /// int
        /// </summary>
        GatedSpectrumEnabled = 304,

        /// <summary>
        /// float64
        /// </summary>
        SpectralMaskFrequencyOffsets = 305,

        /// <summary>
        /// float64
        /// </summary>
        SpectralMaskPowerOffsets = 306,

        /// <summary>
        /// float64
        /// </summary>
        ResultSpectralMaskMarginVector = 307,

        /// <summary>
        /// int
        /// </summary>
        OfdmDemodEvmPerSymbolTraceEnabled = 308,

        /// <summary>
        /// int
        /// </summary>
        CompatibilityVersion = 309,

        /// <summary>
        /// int
        /// </summary>
        OfdmDemodEvmPerSubcarrierTraceEnabled = 310,

        /// <summary>
        /// int
        /// </summary>
        DsssDemodEvmPerChipTraceEnabled = 311,

        /// <summary>
        /// int
        /// </summary>
        [Obsolete]
        DsssDemodEvmPerSymbolTraceEnabled = 311,

        /// <summary>
        /// int
        /// </summary>
        OfdmDemodLowpassFilteringEnabled = 312,

        /// <summary>
        /// int
        /// </summary>
        DsssDemodLowpassFilteringEnabled = 313,

        /// <summary>
        /// int
        /// </summary>
        OfdmDemodEvmPerSymbolPerSubcarrierTraceEnabled = 314,

        /// <summary>
        /// double
        /// </summary>
        [Obsolete]
        MaxInputPower = 316,

        /// <summary>
        /// double
        /// </summary>
        ResultAutorangeMaxInputPower = 317,

        /// <summary>
        /// double
        /// </summary>
        ResultAutorangeAcquisitionLength = 318,

        /// <summary>
        /// double
        /// </summary>
        OfdmDemodSymbolTimingAdjustment = 321,

        /// <summary>
        /// int
        /// </summary>
        OfdmDemodCfoEstimationMethod = 323,

        /// <summary>
        /// int
        /// </summary>
        OfdmDemodMacFrameCheckSequenceCheckEnabled = 324,

        /// <summary>
        /// int
        /// </summary>
        ResultOfdmDemodHeaderParityPassed = 325,

        /// <summary>
        /// int
        /// </summary>
        ResultOfdmDemodHtSigCrcPassed = 326,

        /// <summary>
        /// int
        /// </summary>
        ResultOfdmDemodMacFrameCheckSequencePassed = 327,

        /// <summary>
        /// double
        /// </summary>
        ResultOfdmDemodPilotRmsEvm = 328,

        /// <summary>
        /// double
        /// </summary>
        ResultOfdmDemodDataRmsEvm = 329,

        /// <summary>
        /// int
        /// </summary>
        OfdmDemodPilotEvmPerSymbolTraceEnabled = 330,


        /// <summary>
        /// int
        /// </summary>
        OfdmDemodDataEvmPerSymbolTraceEnabled = 331,


        /// <summary>
        /// int
        /// </summary>
        ResultOfdmDemodDsssofdmHeaderCrcPassed = 334,

        /// <summary>
        /// int
        /// </summary>
        DetectedStandard = 335,


        /// <summary>
        /// double
        /// </summary>
        DetectedChannelBandwidth = 336,


        #region Version 4.0
        ///<summary>
        /// int 
        ///</summary>
        NumberOfSegments = 209, //0xD1	/*int32*/

        ///<summary>
        /// int 
        ///</summary>
        OfdmLSigPayloadLength = 215, // 0xD7	/*int32*/

        ///<summary>
        /// int 
        ///</summary>
        StbcAllStreamsEnabled = 211, //0xD3	/*int32*/

        ///<summary>
        /// int 
        ///</summary>
        NumberOfSpaceTimeStreams = 210, //					0xD2	/*int32*/

        ///<summary>
        /// int 
        ///</summary>
        ShortGuardIntervalB1Bit = 228, //			0xE4	/*int32*/

        ///<summary>
        /// int 
        ///</summary>
        AggregationBit = 235,	/*int32*/

        ///<summary>
        /// int 
        ///</summary>
        GatedSpectrumAveragingType = 384, //0x180	/*int32*/

        ResultSpectralMaskFrequencyOffsetsUsed = 192, //			0xC0	/*float64, readonly*/

        ResultSpectralMaskPowerOffsetsUsed = 193, //						0xC1	/*float64, readonly*/

        ResultSpectralMaskMarginFrequencyVector = 519, //				0x207	/*float64, readonly*/

        ResultSpectralMaskMarginPowerSpectralDensityVector = 520, //		0x208	/*float64, readonly*/

        ResultSpectralMaskViolation = 521, //									0x209	/*float64, readonly*


        ///<summary>
        /// int 
        ///</summary>
        OfdmDemodPreambleFrequencyErrorTraceEnabled = 213, //			0xD5	/*int32*/

        ///<summary>
        /// int 
        ///</summary>
        OfdmDemodCommonPilotErrorTraceEnabled = 388, //					0x184	/*int32*/

        ///<summary>
        /// int 
        ///</summary>
        OfdmDemodPhaseNoisePsdTraceEnabled = 391, //					0x187	/*int32*/

        ///<summary>
        /// int 
        ///</summary>
        OfdmDemodIqMismatchSignalModel = 233, //						0xE9	/*int32*/

        /*- OFDM Demod:Impairments Estimation Enabled ------------------------------------------------------------*/
        ///<summary>
        /// int 
        ///</summary>
        OfdmDemodCarrierFrequencyOffsetEstimationEnabled = 392, //			0x188	/*int32*/

        ///<summary>
        /// int 
        ///</summary>
        OfdmDemodSampleClockOffsetEstimationEnabled = 393, //		0x189	/*int32*/

        ///<summary>
        /// int 
        ///</summary>
        OfdmDemodIqGainImbalanceEstimationEnabled = 394, //				0x18A	/*int32*/

        ///<summary>
        /// int 
        ///</summary>
        OfdmDemodQuadratureSkewEstimationEnabled = 395, //				0x18B	/*int32*/

        ///<summary>
        /// int 
        ///</summary>
        OfdmDemodTimingSkewEstimationEnabled = 396, //						0x18C	/*int32*/

        ///<summary>
        /// int 
        ///</summary>
        OfdmDemodCarrierFrequencyLeakageEstimationEnabled = 397, //		0x18D	/*int32*/

        ///<summary>
        /// int 
        ///</summary>
        OfdmDemodCommonPilotErrorEstimationEnabled = 398, //				0x18E	/*int32*/

        /*- OFDM Demod:Impairments Compensation Enabled ------------------------------------------------------------*/
        ///<summary>
        /// int 
        ///</summary>

        OfdmDemodIqGainImbalanceCompensationEnabled = 403, //			0x193	/*int32*/
        ///<summary>
        /// int 
        ///</summary>
        OfdmDemodQuadratureSkewCompensationEnabled = 404, //				0x194	/*int32*/

        ///<summary>
        /// int 
        ///</summary>
        OfdmDemodTimingSkewCompensationEnabled = 400, //				0x190	/*int32*/

        /// <summary>
        /// int
        /// </summary>
        Property80211acAmpduEnabled = 399, //0x18F,   /*int32*/

        /// <summary>
        /// int
        /// </summary>
        OfdmDemodCombinedSignalDemodulationEnabled = 408,	//	0x198   /*int32*/

        /// <summary>
        /// int
        /// </summary>
        OfdmDemodBurstStartDetectionEnabled = 389, //				0x185   /*int32*/

        /// <summary>
        /// float ReadOnly
        /// </summary>
        OfdmDemodRmsPhaseError = 224,//						0xE0	/*float64,readonly*/

        /// <summary>
        /// int ReadOnly
        /// </summary>
        OfdmDemodNumberOfSpaceTimeStreams = 217,//				0xD9	/*int32,readonly*/

        /// <summary>
        /// int ReadOnly
        /// </summary>
        OfdmDemodSpectralFlatnessMarginSubcarrierIndex = 381, //	0x17D	/*int32,readonly*/

        /// <summary>
        /// int ReadOnly
        /// </summary>
        OfdmDemodNumberOfOfdmSymbols = 230,     //		0xE6	/*int32,readonly*/

        /// <summary>
        /// int ReadOnly
        /// </summary>
        OfdmDemodAggregation = 234,         //	0xEA	/*int32,readonly*/


        /// <summary>
        /// float ReadOnly
        /// </summary>
        ResultOfdmDemodTimingSkew = 221, //								0xDD	/*float64,readonly*/


        /// <summary>
        /// float ReadOnly
        /// </summary>
        ResultOfdmDemodRmsPhaseError = 224, //								0xE0	/*float64,readonly*/


        /// <summary>
        /// float ReadOnly
        /// </summary>
        ResultOfdmDemodCommonPilotErrorRms = 382, //				0x17E	/*float64,readonly*/

        /// <summary>
        /// int ReadOnly
        /// </summary>
        ResultOfdmDemodLSigPayloadLength = 216, //					0xD8	/*int32,readonly*/

        /// <summary>
        /// int ReadOnly
        /// </summary>
        ResultOfdmDemodStbcAllStreamsEnabled = 218, //				0xDA	/*int32,readonly*/

        /// <summary>
        /// int ReadOnly
        /// </summary>
        ResultOfdmDemodNumberOfSpaceTimeStreams = 217, //			0xD9	/*int32,readonly*/	

        /// <summary>
        /// int ReadOnly
        /// </summary>
        ResultOfdmDemodVhtSigACrcPassed = 219, //        0xDB	/*int32,readonly*/	

        /// <summary>
        /// int ReadOnly
        /// </summary>
        ResultOfdmDemodVhtSigBCrcPassed = 220, //        0xDC	/*int32,readonly*/	

        /// <summary>
        /// int ReadOnly
        /// </summary>
        ResultOfdmDemodShortGuardIntervalB1Bit = 229, // 		0xE5	/*int32,readonly*/

        /*- Advanced -----------------------------------------------------*/
        /// <summary>
        /// int
        /// </summary>
        SwapIAndQEnabled = 214, 	/*int32*/

        /// <summary>
        /// int
        /// </summary>
        SampleClockRateFactor = 383, //								0x17F	/*int32*/

        #endregion

    }
}