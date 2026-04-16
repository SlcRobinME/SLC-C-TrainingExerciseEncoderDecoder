using System;
using Skyline.DataMiner.Scripting;

/// <summary>
/// DataMiner QAction Class: Encoder Status Changed.
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
            double writeValue = Convert.ToDouble(protocol.GetParameter(Parameter.Write.encoderstatus_54));

            // Copy write value to read parameter
            protocol.SetParameter(Parameter.encoderstatus_4, writeValue);

            if (writeValue == 0)
            {
                // Disable — set all encoder params to Not Available (-1)
                protocol.SetParameters(
                    new int[]
                    {
                        Parameter.encodercurrentcompressedbitrate_6,
                        Parameter.encoderautochromaweight_8,
                        Parameter.encoderchromaweight_17,
                        Parameter.encoderlosslessmode_10,
                    },
                    new object[] { ParamState.NA, ParamState.NA, ParamState.NA, ParamState.NA });
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

                protocol.SetParameters(
                    new int[]
                    {
                        // Restore encoder params
                        Parameter.encodercurrentcompressedbitrate_6,
                        Parameter.encoderautochromaweight_8,
                        Parameter.encoderchromaweight_17,
                        Parameter.encoderlosslessmode_10,

                        // Auto-disable decoder
                        Parameter.Write.decoderstatus_55,
                        Parameter.decoderstatus_5,
                        Parameter.decodercurrentcompressedbitrate_7,
                        Parameter.decoderprogressionorder_11,
                        Parameter.decodercodeblockwidth_12,
                        Parameter.decodercodeblockheight_13,
                    },
                    new object[]
                    {
                        copyValues[0],
                        copyValues[1],
                        copyValues[2],
                        copyValues[3],

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