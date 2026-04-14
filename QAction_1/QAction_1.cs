using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Win32.SafeHandles;
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
            //protocol.Log($"QA1 fired", LogType.Information, LogLevel.NoLogging);

            //Encoder
            protocol.SetParameter(Parameter.encoderstatus, 1);
			protocol.SetParameter(Parameter.Write.encoderstatus, 1);
			protocol.SetParameter(Parameter.encodercurrentcompressedbitrate, 150);
			protocol.SetParameter(Parameter.encoderautochromaweight, 0);
			protocol.SetParameter(Parameter.Write.encoderautochromaweight, 0);
			protocol.SetParameter(Parameter.encoderchromaweight, 75.0);
			protocol.SetParameter(Parameter.Write.encoderchromaweight, 75.0);
			protocol.SetParameter(Parameter.encoderlosslessmode, 0);
			protocol.SetParameter(Parameter.Write.encoderlosslessmode, 0);

			//Decoder
			protocol.SetParameter(Parameter.decoderstatus, 0);
            protocol.SetParameter(Parameter.Write.decoderstatus, 0);
            protocol.SetParameter(Parameter.decodercurrentcompressedbitrate, -1);
            protocol.SetParameter(Parameter.decoderprogressionorder, -1);
            protocol.SetParameter(Parameter.Write.decoderprogressionorder, -1);
            protocol.SetParameter(Parameter.decodercodeblockwidth, -1);
            protocol.SetParameter(Parameter.decodercodeblockheight, -1);
        }
        catch (Exception ex)
		{
			protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
		}
	}
}
