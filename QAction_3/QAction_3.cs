using System;
using System.Collections.Generic;
using Skyline.DataMiner.Scripting;
using Skyline.DataMiner.Utils.Protocol.Extension;

/// <summary>
/// DataMiner QAction Class: Encoder Status Changed.
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
            double readValue = (double)protocol.GetParameter(Parameter.encoderstatus_4);

            if (readValue == (double)ParamState.Disabled)
            {
                var disableEncoder = new Dictionary<int, object>
                    {
                        { Parameter.encodercurrentcompressedbitrate_6,  ParamState.NA },
                        { Parameter.encoderautochromaweight_8,          ParamState.NA },
                        { Parameter.encoderchromaweight_17,             ParamState.NA },
                        { Parameter.encoderlosslessmode_10,             ParamState.NA },
                    };

                protocol.SetParameters(
                    new List<int>(disableEncoder.Keys).ToArray(),
                    new List<object>(disableEncoder.Values).ToArray());
            }
            else
            {
                // Batch-get all copy params in one call
                object[] copyValues = (object[])protocol.GetParameters(new uint[]
                {
                    Parameter.copyofencodercurrentcompressedbitrate_106,
                    Parameter.copyofencoderautochromaweight_108,
                    Parameter.copyofencoderchromaweight_117,
                    Parameter.copyofencoderlosslessmode_110,
                });

                var enableEncoder = new Dictionary<int, object>
                {
                    // Restore encoder params from copy
                    { Parameter.encodercurrentcompressedbitrate_6,  copyValues[0] },
                    { Parameter.encoderautochromaweight_8,          copyValues[1] },
                    { Parameter.encoderchromaweight_17,             copyValues[2] },
                    { Parameter.encoderlosslessmode_10,             copyValues[3] },

                    // Auto-disable decoder
                    { Parameter.Write.decoderstatus_55,             ParamState.Disabled },
                    { Parameter.decoderstatus_5,                    ParamState.Disabled },
                    { Parameter.decodercurrentcompressedbitrate_7,  ParamState.NA },
                    { Parameter.decoderprogressionorder_11,         ParamState.NA },
                    { Parameter.decodercodeblockwidth_12,           ParamState.NA },
                    { Parameter.decodercodeblockheight_13,          ParamState.NA },
                };
                protocol.SetParameters(enableEncoder);
            }
        }
        catch (Exception ex)
        {
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
        }
    }
}