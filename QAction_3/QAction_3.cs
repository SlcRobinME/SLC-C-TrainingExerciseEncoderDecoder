using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Skyline.DataMiner.Scripting;
using Skyline.DataMiner.Utils.Protocol.Extension;
using System.Reflection;

/// <summary>
/// DataMiner QAction Class: Encoder Status Changed.
/// </summary>
public static class QAction
{
    /// <summary>
    /// The QAction entry point.
    /// </summary>
    /// <param name="protocol">Link with SLProtocol process.</param>
    public static void Run(SLProtocol protocol)
    {
        try
        {
            double writeValue = Convert.ToDouble(protocol.GetParameter(54));

            // Copy write value to read parameter
            protocol.SetParameter(4, writeValue);

            if (writeValue == 0)
            {
                // Disable — set all encoder params to Not Available (-1)
                protocol.SetParameter(6, -1.0);   // EncoderCurrentCompressedBitrate
                protocol.SetParameter(8, -1.0);   // EncoderAutoChromaWeight
                protocol.SetParameter(17, -1.0);  // EncoderChromaWeight
                protocol.SetParameter(10, -1.0);  // EncoderLosslessMode

            }
            else
            {
                // Enable — restore from copy params
                protocol.SetParameter(6, Convert.ToDouble(protocol.GetParameter(106)));
                protocol.SetParameter(8, Convert.ToDouble(protocol.GetParameter(108)));
                protocol.SetParameter(17, Convert.ToDouble(protocol.GetParameter(117)));
                protocol.SetParameter(10, Convert.ToDouble(protocol.GetParameter(110)));

                // Auto-disable decoder
                protocol.SetParameter(5, 0.0);
                protocol.SetParameter(55, 0.0);
                protocol.SetParameter(7, -1.0);
                protocol.SetParameter(11, -1.0);
                protocol.SetParameter(12, -1.0);
                protocol.SetParameter(13, -1.0);
            }
        }
        catch (Exception ex)
        {
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
        }
    }
}