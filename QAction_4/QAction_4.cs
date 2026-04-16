using System;
using Skyline.DataMiner.Scripting;

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
    private const int BitrateDecimalPlaces = 3;

    public static void Run(SLProtocol protocol)
	{
		try
		{
			double randomBitrate = Math.Round(Rng.NextDouble()*SharedConstants.MaxBitrate, BitrateDecimalPlaces);

            int operationMode = Convert.ToInt32(protocol.GetParameter(Parameter.operationmode));
            if (operationMode == SharedConstants.EncoderMode)
                protocol.SetParameter(Parameter.encodercurrentcompressedbitrate, randomBitrate);
            else
                protocol.SetParameter(Parameter.decodercurrentcompressedbitrate, randomBitrate);
        }
        catch (Exception ex)
		{
			protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
		}
	}
}
