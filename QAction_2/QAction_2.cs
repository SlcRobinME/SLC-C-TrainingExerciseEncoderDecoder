using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Skyline.DataMiner.Scripting;

/// <summary>
/// DataMiner QAction Class: After Startup.
/// </summary>
public static class QAction
{
    private static System.Timers.Timer _bitrateTimer;

    /// <summary>
    /// The QAction entry point.
    /// </summary>
    /// <param name="protocol">Link with SLProtocol process.</param>
    public static void Run(SLProtocol protocol)
    {
        try
        {
            SetDefaults(protocol);

            _bitrateTimer = new System.Timers.Timer(1000);
            _bitrateTimer.Elapsed += (sender, e) => SimulateBitrate(protocol);
            _bitrateTimer.AutoReset = true;
            _bitrateTimer.Start();
        }
        catch (Exception ex)
        {
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
        }
    }

    private static void SetDefaults(SLProtocol protocol)
    {
        // Status: Disabled (0) by default
        protocol.SetParameter(4, 0);
        protocol.SetParameter(5, 0);

        // Encoder parameters — default values
        protocol.SetParameter(6, 0.0);    // EncoderCurrentCompressedBitrate
        protocol.SetParameter(8, 0.0);    // EncoderAutoChromaWeight: Disabled
        protocol.SetParameter(17, 0.0);   // EncoderChromaWeight
        protocol.SetParameter(10, 0.0);   // EncoderLosslessMode: Disabled

        // Decoder parameters — default values
        protocol.SetParameter(7, 00.0);   // DecoderCurrentCompressedBitrate
        protocol.SetParameter(11, 0.0);   // DecoderProgressionOrder: LCRP
        protocol.SetParameter(12, 0.0);   // DecoderCodeBlockWidth
        protocol.SetParameter(13, 0.0);   // DecoderCodeBlockHeight
    }

    private static void SimulateBitrate(SLProtocol protocol)
    {
        var rng = new Random();

        // Only update if Encoder is Enabled
        double encoderStatus = Convert.ToDouble(protocol.GetParameter(4));
        if (encoderStatus == 1)
        {
            double newEncoderBitrate = Math.Round(rng.NextDouble() * 47, 3);
            protocol.SetParameter(6, newEncoderBitrate);
            protocol.SetParameter(106, newEncoderBitrate); // keep copy in sync
        }

        // Only update if Decoder is Enabled
        double decoderStatus = Convert.ToDouble(protocol.GetParameter(5));
        if (decoderStatus == 1)
        {
            double newDecoderBitrate = Math.Round(rng.NextDouble() * 47, 3);
            protocol.SetParameter(7, newDecoderBitrate);
            protocol.SetParameter(107, newDecoderBitrate); // keep copy in sync
        }
    }
}
