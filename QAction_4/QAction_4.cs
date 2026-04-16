using System;
using Skyline.DataMiner.Scripting;

/// <summary>
/// DataMiner QAction Class: Decoder Status Changed.
/// </summary>
public static class QAction
{
    public enum ParamState
    {
        NA = -1,
        Disabled = 0,
        Enabled = 1,
    }

    /// <summary>
    /// The QAction entry point.
    /// </summary>
    /// <param name="protocol">Link with SLProtocol process.</param>
    public static void Run(SLProtocol protocol)
    {
        try
        {
            double writeValue = Convert.ToDouble(protocol.GetParameter(Parameter.Write.decoderstatus_55));

            // Copy write value to read parameter
            protocol.SetParameter(Parameter.decoderstatus_5, writeValue);

            if (writeValue == 0)
            {
                // Disable — set all decoder params to Not Available (-1)
                protocol.SetParameters(
                    new int[]
                    {
                        Parameter.decodercurrentcompressedbitrate_7,
                        Parameter.decoderprogressionorder_11,
                        Parameter.decodercodeblockwidth_12,
                        Parameter.decodercodeblockheight_13,
                    },
                    new object[] { ParamState.NA, ParamState.NA, ParamState.NA, ParamState.NA });
            }
            else
            {
                protocol.SetParameters(
                    new int[]
                    {
                        // Restore decoder (ONE CALL)
                        Parameter.decodercurrentcompressedbitrate_7,
                        Parameter.decoderprogressionorder_11,
                        Parameter.decodercodeblockwidth_12,
                        Parameter.decodercodeblockheight_13,

                        // Disable encoder (ONE CALL)
                        Parameter.Write.encoderstatus_54,
                        Parameter.encoderstatus_4,
                        Parameter.encodercurrentcompressedbitrate_6,
                        Parameter.encoderautochromaweight_8,
                        Parameter.encoderchromaweight_17,
                        Parameter.encoderlosslessmode_10,
                    },
                    new object[]
                    {
                        protocol.GetParameter(Parameter.copyofdecodercurrentcompressedbitrate),
                        protocol.GetParameter(Parameter.copyofdecoderprogressionorder),
                        protocol.GetParameter(Parameter.copyofdecodercodeblockwidth),
                        protocol.GetParameter(Parameter.copyofdecodercodeblockheight),

                        ParamState.Disabled,
                        ParamState.Disabled,
                        ParamState.NA,
                        ParamState.NA,
                        ParamState.NA,
                        ParamState.NA,
                    });
            }
        }
        catch (Exception ex)
        {
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
        }
    }
}