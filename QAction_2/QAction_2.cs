using Skyline.DataMiner.Scripting;
using Skyline.DataMiner.Utils.Protocol.Extension;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

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
			int encoderStatus = Convert.ToInt32(protocol.GetParameter(Parameter.encoderstatus));
            protocol.Log($"QA2 fired | encoderStatus = {encoderStatus}", LogType.Information, LogLevel.NoLogging);

            if (encoderStatus == 1)
			{
				//Encoder enabled
				protocol.SetParameter(Parameter.encodercurrentcompressedbitrate, 150);
				protocol.SetParameter(Parameter.encoderautochromaweight, 0);
				protocol.SetParameter(Parameter.Write.encoderautochromaweight, 0);
				protocol.SetParameter(Parameter.encoderchromaweight, 75.0);
				protocol.SetParameter(Parameter.Write.encoderchromaweight, 75.0);
				protocol.SetParameter(Parameter.encoderlosslessmode, 0);
				protocol.SetParameter(Parameter.Write.encoderlosslessmode, 0);

				//Disable decoder
				int decoderStatus = Convert.ToInt32(protocol.GetParameter(Parameter.decoderstatus));
				if (decoderStatus != 0)
				{
					protocol.SetParameter(Parameter.decoderstatus, 0);
					protocol.SetParameter(Parameter.Write.decoderstatus, 0);
				}
			}
			else 
			{
                //Encoder disabled
                protocol.SetParameter(Parameter.encodercurrentcompressedbitrate, -1);
                protocol.SetParameter(Parameter.encoderautochromaweight, -1);
                protocol.SetParameter(Parameter.Write.encoderautochromaweight, -1);
                protocol.SetParameter(Parameter.encoderchromaweight, -1);
                protocol.SetParameter(Parameter.Write.encoderchromaweight, -1);
                protocol.SetParameter(Parameter.encoderlosslessmode, -1);
                protocol.SetParameter(Parameter.Write.encoderlosslessmode, -1);

				int decoderStatus = Convert.ToInt32(protocol.GetParameter(Parameter.decoderstatus));
				if (decoderStatus != 1) {
                    protocol.SetParameter(Parameter.decoderstatus, 1);
                    protocol.SetParameter(Parameter.Write.decoderstatus, 1);
                }
            }
		}
		catch (Exception ex)
		{
			protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
		}
	}
}
