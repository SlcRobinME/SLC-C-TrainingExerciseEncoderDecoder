using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Skyline.DataMiner.Net.Messages.Diagnostics.Requests;
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
	public static void Run(SLProtocol protocol)
	{
		try
		{
			int encoderStatus = Convert.ToInt32(protocol.GetParameter(100));
			if (encoderStatus == 1)
			{
				//Encoder enabled
				protocol.SetParameter(101, 1);
				protocol.SetParameter(102, 150);
				protocol.SetParameter(103, 0);
				protocol.SetParameter(104, 0);
				protocol.SetParameter(105, 75.0);
				protocol.SetParameter(106, 75.0);
				protocol.SetParameter(107, 0);
				protocol.SetParameter(108, 0);

				//Disable decoder
				int decoderStatus = Convert.ToInt32(protocol.GetParameter(200));
				if (decoderStatus != 0)
				{
					protocol.SetParameter(200, 0);
					protocol.SetParameter(201, 0);
				}
			}
			else 
			{
                //Encoder disabled
                protocol.SetParameter(101, 0);
                protocol.SetParameter(102, -1);
                protocol.SetParameter(103, -1);
                protocol.SetParameter(104, -1);
                protocol.SetParameter(105, -1);
                protocol.SetParameter(106, -1);
                protocol.SetParameter(107, -1);
                protocol.SetParameter(108, -1);

				int decoderStatus = Convert.ToInt32(protocol.GetParameter(200));
				if (decoderStatus != 1) {
                    protocol.SetParameter(200, 1);
                    protocol.SetParameter(201, 1);
                }
            }
		}
		catch (Exception ex)
		{
			protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
		}
	}
}
