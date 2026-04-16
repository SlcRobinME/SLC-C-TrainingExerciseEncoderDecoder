using System;
using System.Collections.Generic;
using System.Linq;
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
    public static void Run(SLProtocol protocol)
	{
		try
		{
            var initialValues = new Dictionary<int, object>
            {
                { Parameter.operationmode, SharedConstants.EncoderMode },
                { Parameter.encodercurrentcompressedbitrate, SharedConstants.DefaultBitrate },
                { Parameter.encoderautochromaweight, SharedConstants.AutoChromaWeightDisabled },
                { Parameter.encoderchromaweight, SharedConstants.DefaultChromaWeight },
                { Parameter.encoderlosslessmode, SharedConstants.LosslessModeDisabled },
                { Parameter.decodercurrentcompressedbitrate, SharedConstants.NotAvailable },
                { Parameter.decoderprogressionorder, SharedConstants.NotAvailable },
                { Parameter.decodercodeblockwidth, SharedConstants.NotAvailable },
                { Parameter.decodercodeblockheight, SharedConstants.NotAvailable }
            };
            protocol.SetParameters(initialValues.Keys.ToArray(),initialValues.Values.ToArray());
        }
        catch (Exception ex)
		{
			protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
		}
	}
}
