using System;
using System.Collections.Generic;
using Skyline.DataMiner.Scripting;
using Skyline.DataMiner.Utils.Protocol.Extension;

/// <summary>
/// DataMiner QAction Class: After Startup.
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
            var defaults = new Dictionary<int, object>
            {
                // Status
                { Parameter.encoderstatus_4,                    ParamState.Disabled },
                { Parameter.decoderstatus_5,                    ParamState.Disabled },

                // Encoder
                { Parameter.encodercurrentcompressedbitrate_6,  ParamState.NA },
                { Parameter.encoderautochromaweight_8,          ParamState.Disabled },
                { Parameter.encoderchromaweight_17,             ParamState.Disabled },
                { Parameter.encoderlosslessmode_10,             ParamState.Disabled },

                // Decoder
                { Parameter.decodercurrentcompressedbitrate_7,  ParamState.NA },
                { Parameter.decoderprogressionorder_11,         ParamState.Disabled },
                { Parameter.decodercodeblockwidth_12,           ParamState.NA },
                { Parameter.decodercodeblockheight_13,          ParamState.NA },
            };

            protocol.SetParameters(defaults);
        }
        catch (Exception ex)
        {
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
        }
    }
}