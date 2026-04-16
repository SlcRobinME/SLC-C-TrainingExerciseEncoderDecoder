using Skyline.DataMiner.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;

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
    public static void Run(SLProtocol protocol)
	{
		try
		{
            int operationMode = Convert.ToInt32(protocol.GetParameter(Parameter.operationmode));
            Dictionary<int, object> parameters;
            if (operationMode == SharedConstants.EncoderMode)
            {
                parameters = new Dictionary<int, object>
            {
                { Parameter.encodercurrentcompressedbitrate, SharedConstants.DefaultBitrate },
                { Parameter.encoderautochromaweight, SharedConstants.AutoChromaWeightDisabled },
                { Parameter.encoderchromaweight, SharedConstants.DefaultChromaWeight },
                { Parameter.encoderlosslessmode, SharedConstants.LosslessModeDisabled },
                { Parameter.decodercurrentcompressedbitrate, SharedConstants.NotAvailable },
                { Parameter.decoderprogressionorder, SharedConstants.NotAvailable },
                { Parameter.decodercodeblockwidth, SharedConstants.NotAvailable },
                { Parameter.decodercodeblockheight, SharedConstants.NotAvailable },
            };
            }
            else 
			{
                parameters = new Dictionary<int, object>
            {
                { Parameter.decodercurrentcompressedbitrate, SharedConstants.DefaultBitrate },
                { Parameter.decoderprogressionorder, SharedConstants.DefaultProgressionOrder },
                { Parameter.decodercodeblockwidth, SharedConstants.DefaultCodeBlockSize },
                { Parameter.decodercodeblockheight, SharedConstants.DefaultCodeBlockSize },
                { Parameter.encodercurrentcompressedbitrate, SharedConstants.NotAvailable },
                { Parameter.encoderautochromaweight, SharedConstants.NotAvailable },
                { Parameter.encoderchromaweight, SharedConstants.NotAvailable },
                { Parameter.encoderlosslessmode, SharedConstants.NotAvailable },
            };
            }

            protocol.SetParameters(parameters.Keys.ToArray(), parameters.Values.ToArray());
        }
        catch (Exception ex)
		{
			protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
		}
	}
}
