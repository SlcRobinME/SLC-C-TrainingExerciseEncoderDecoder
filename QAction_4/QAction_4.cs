using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

using Skyline.DataMiner.Scripting;
using Skyline.DataMiner.Utils.Protocol.Extension;

/// <summary>
/// DataMiner QAction Class.
/// </summary>
public static class QAction
{
    /// <summary>
    /// The QAction entry point.
    /// </summary>
    /// <param name="protocol">Link with SLProtocol process.</param>
    /// 
    private static readonly Random Rng = new Random();

    public static void Run(SLProtocol protocol)
	{
		try
		{
			double randomBitrate= Math.Round(Rng.NextDouble()*15.0, 3);

            int encoderStatus = Convert.ToInt32(protocol.GetParameter(100));
            if (encoderStatus == 1) {
                protocol.SetParameter(102,randomBitrate);
            }
            int decoderStatus = Convert.ToInt32(protocol.GetParameter(200));
            if (decoderStatus == 1) {
                protocol.SetParameter(202,randomBitrate);
            }
        }
        catch (Exception ex)
		{
			protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
		}
	}
}
