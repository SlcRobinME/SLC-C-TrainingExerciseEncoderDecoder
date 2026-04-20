using System;
using System.Collections.Generic;
using Skyline.DataMiner.Scripting;
using Skyline.DataMiner.Utils.Protocol.Extension;

/// <summary>
/// DataMiner QAction Class: Decoder Status Changed.
/// </summary>
public static class QAction
{
    /// <summary>
    /// The QAction entry point.
    /// </summary>
    /// <param name="protocol">Link with SLProtocol process.</param>
    public static void Run(SLProtocolExt protocol)
    {
        try
        {
            double readValue = (double)protocol.GetParameter(Parameter.decoderstatus_5);

            if (readValue == (double)ParamState.Disabled)
            {
                var disableDecoder = new Dictionary<int, object>
                {
                    { Parameter.decodercurrentcompressedbitrate_7,  ParamState.NA },
                    { Parameter.decoderprogressionorder_11,         ParamState.NA },
                    { Parameter.decodercodeblockwidth_12,           ParamState.NA },
                    { Parameter.decodercodeblockheight_13,          ParamState.NA },
                };

                protocol.SetParameters(disableDecoder);
            }
            else
            {
                // Batch-get all copy params in one call
                object[] copyValues = (object[])protocol.GetParameters(new uint[]
                {
                    Parameter.copyofdecodercurrentcompressedbitrate_107,
                    Parameter.copyofdecoderprogressionorder_111,
                    Parameter.copyofdecodercodeblockwidth_112,
                    Parameter.copyofdecodercodeblockheight_113,
                });

                var enableDecoder = new Dictionary<int, object>
                {
                    // Restore decoder params from copy
                    { Parameter.decodercurrentcompressedbitrate_7,  copyValues[0] },
                    { Parameter.decoderprogressionorder_11,         copyValues[1] },
                    { Parameter.decodercodeblockwidth_12,           copyValues[2] },
                    { Parameter.decodercodeblockheight_13,          copyValues[3] },

                    // Auto-disable encoder
                    { Parameter.Write.encoderstatus_54,             ParamState.Disabled },
                    { Parameter.encoderstatus_4,                    ParamState.Disabled },
                    { Parameter.encodercurrentcompressedbitrate_6,  ParamState.NA },
                    { Parameter.encoderautochromaweight_8,          ParamState.NA },
                    { Parameter.encoderchromaweight_17,             ParamState.NA },
                    { Parameter.encoderlosslessmode_10,             ParamState.NA },
                };

                protocol.SetParameters(enableDecoder);
            }
        }
        catch (Exception ex)
        {
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
        }
    }
}