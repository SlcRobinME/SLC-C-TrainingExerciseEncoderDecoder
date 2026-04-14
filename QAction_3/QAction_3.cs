using System;
using System.Collections.Generic;
using System.Globalization;
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
	public static void Run(SLProtocol protocol)
	{
		try
		{
            int decoderStatus = Convert.ToInt32(protocol.GetParameter(200));

			if (decoderStatus == 1) {
                protocol.SetParameter(201, 1);
                protocol.SetParameter(202, 200);
                protocol.SetParameter(203, 0);
                protocol.SetParameter(204, 0);
                protocol.SetParameter(205, 64);
                protocol.SetParameter(206, 64);


                int encoderStatus = Convert.ToInt32(protocol.GetParameter(100));
                if (encoderStatus != 0)
                {
                    protocol.SetParameter(100, 0);
                    protocol.SetParameter(101, 0);
                }
            }
            else
            {
                protocol.SetParameter(201, 0);
                protocol.SetParameter(202, -1);
                protocol.SetParameter(203, -1);
                protocol.SetParameter(204, -1);
                protocol.SetParameter(205, -1);
                protocol.SetParameter(206, -1);

                int encoderStatus = Convert.ToInt32(protocol.GetParameter(100));
                if (encoderStatus != 1)
                {
                    protocol.SetParameter(100, 1);
                    protocol.SetParameter(101, 1);
                }
            }
        }
        catch (Exception ex)
		{
			protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
		}
	}
}
