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
            int decoderStatus = Convert.ToInt32(protocol.GetParameter(Parameter.decoderstatus));

			if (decoderStatus == 1) {
                protocol.SetParameter(Parameter.decodercurrentcompressedbitrate, 10);
                protocol.SetParameter(Parameter.decoderprogressionorder, 0);
                protocol.SetParameter(Parameter.Write.decoderprogressionorder, 0);
                protocol.SetParameter(Parameter.decodercodeblockwidth, 64);
                protocol.SetParameter(Parameter.decodercodeblockheight, 64);

                int encoderStatus = Convert.ToInt32(protocol.GetParameter(Parameter.Write.encoderstatus));
                if (encoderStatus != 0)
                {
                    protocol.SetParameter(Parameter.encoderstatus, 0);
                    protocol.SetParameter(Parameter.Write.encoderstatus, 0);
                }
            }
            else
            {
                protocol.SetParameter(Parameter.decodercurrentcompressedbitrate, -1);
                protocol.SetParameter(Parameter.decoderprogressionorder, -1);
                protocol.SetParameter(Parameter.Write.decoderprogressionorder, -1);
                protocol.SetParameter(Parameter.decodercodeblockwidth, -1);
                protocol.SetParameter(Parameter.decodercodeblockheight, -1);

                int encoderStatus = Convert.ToInt32(protocol.GetParameter(Parameter.encoderstatus));
                if (encoderStatus != 1)
                {
                    protocol.SetParameter(Parameter.encoderstatus, 1);
                    protocol.SetParameter(Parameter.Write.encoderstatus, 1);
                }
            }
        }
        catch (Exception ex)
		{
			protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
		}
	}
}
