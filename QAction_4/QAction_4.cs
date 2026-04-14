using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Skyline.DataMiner.Scripting;
using Skyline.DataMiner.Utils.Protocol.Extension;
using System.Reflection;

/// <summary>
/// DataMiner QAction Class: Decoder Status Changed.
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
            double writeValue = Convert.ToDouble(protocol.GetParameter(55));

            // Copy write value to read parameter
            protocol.SetParameter(5, writeValue);

            if (writeValue == 0)
            {
                // Disable — set all decoder params to Not Available (-1)
                protocol.SetParameter(7, -1.0);   // DecoderCurrentCompressedBitrate
                protocol.SetParameter(11, -1.0);  // DecoderProgressionOrder
                protocol.SetParameter(12, -1.0);  // DecoderCodeBlockWidth
                protocol.SetParameter(13, -1.0);  // DecoderCodeBlockHeight

            }
            else
            {
                // Enable — restore from copy params
                protocol.SetParameter(7, Convert.ToDouble(protocol.GetParameter(107)));
                protocol.SetParameter(11, Convert.ToDouble(protocol.GetParameter(111)));
                protocol.SetParameter(12, Convert.ToDouble(protocol.GetParameter(112)));
                protocol.SetParameter(13, Convert.ToDouble(protocol.GetParameter(113)));

                // Auto-disable encoder
                protocol.SetParameter(4, 0.0);
                protocol.SetParameter(54, 0.0);
                protocol.SetParameter(6, -1.0);
                protocol.SetParameter(8, -1.0);
                protocol.SetParameter(17, -1.0);
                protocol.SetParameter(10, -1.0);

            }
        }
        catch (Exception ex)
        {
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
        }
    }
}