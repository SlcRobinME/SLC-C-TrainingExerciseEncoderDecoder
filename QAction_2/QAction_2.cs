using System;
using Skyline.DataMiner.Scripting;

/// <summary>
/// DataMiner QAction Class: After Startup.
/// </summary>
public static class QAction
{
    public enum ParamState
    {
        NA = -1,
        Disabled = 0,
        Enabled = 1,
    }

    private static System.Timers.Timer _bitrateTimer;

    /// <summary>
    /// The QAction entry point.
    /// </summary>
    /// <param name="protocol">Link with SLProtocol process.</param>
    public static void Run(SLProtocol protocol)
    {
        try
        {
            protocol.SetParameters(
            new int[]
            {
            // Status: Disabled (0) by default
                Parameter.encoderstatus_4,
                Parameter.decoderstatus_5,

                // Encoder parameters — default values
                Parameter.encodercurrentcompressedbitrate_6,
                Parameter.encoderautochromaweight_8,
                Parameter.encoderchromaweight_17,
                Parameter.encoderlosslessmode_10,

                // Decoder parameters — default values
                Parameter.decodercurrentcompressedbitrate_7,
                Parameter.decoderprogressionorder_11,
                Parameter.decodercodeblockwidth_12,
                Parameter.decodercodeblockheight_13,
            },
            new object[]
            {
                ParamState.Disabled,
                ParamState.Disabled,

                ParamState.Disabled,
                ParamState.Disabled,
                ParamState.Disabled,
                ParamState.Disabled,

                ParamState.Disabled,
                ParamState.Disabled,
                ParamState.Disabled,
                ParamState.Disabled,
            });
        }
        catch (Exception ex)
        {
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
        }
    }
}