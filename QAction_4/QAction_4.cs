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

            int encoderStatus = Convert.ToInt32(protocol.GetParameter(Parameter.encoderstatus));
            if (encoderStatus == 1) {
                protocol.SetParameter(Parameter.encodercurrentcompressedbitrate, randomBitrate);
            }
            int decoderStatus = Convert.ToInt32(protocol.GetParameter(Parameter.decoderstatus));
            if (decoderStatus == 1) {
                protocol.SetParameter(Parameter.decodercurrentcompressedbitrate, randomBitrate);
            }
        }
        catch (Exception ex)
		{
			protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
		}
	}
}
