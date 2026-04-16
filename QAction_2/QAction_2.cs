using Skyline.DataMiner.Scripting;
using System;

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

            if (operationMode == SharedConstants.EncoderMode)
            {
                protocol.SetParameters(
                    new int[] {
                    Parameter.encodercurrentcompressedbitrate,
                    Parameter.encoderautochromaweight,
                    Parameter.encoderchromaweight,
                    Parameter.encoderlosslessmode,
                    Parameter.decodercurrentcompressedbitrate,
                    Parameter.decoderprogressionorder,
                    Parameter.decodercodeblockwidth,
                    Parameter.decodercodeblockheight
                    },
                    new object[] {
                    SharedConstants.DefaultBitrate,
                    SharedConstants.AutoChromaWeightDisabled,
                    SharedConstants.DefaultChromaWeight,
                    SharedConstants.LosslessModeDisabled,
                    SharedConstants.NotAvailable,
                    SharedConstants.NotAvailable,
                    SharedConstants.NotAvailable,
                    SharedConstants.NotAvailable
                    });
            }
            else 
			{
                protocol.SetParameters(
                    new int[] {
                    Parameter.decodercurrentcompressedbitrate,
                    Parameter.decoderprogressionorder,
                    Parameter.decodercodeblockwidth,
                    Parameter.decodercodeblockheight,
                    Parameter.encodercurrentcompressedbitrate,
                    Parameter.encoderautochromaweight,
                    Parameter.encoderchromaweight,
                    Parameter.encoderlosslessmode
                    },
                    new object[] {
                    SharedConstants.DefaultBitrate,
                    SharedConstants.DefaultProgressionOrder,
                    SharedConstants.DefaultCodeBlockSize,
                    SharedConstants.DefaultCodeBlockSize,
                    SharedConstants.NotAvailable,
                    SharedConstants.NotAvailable,
                    SharedConstants.NotAvailable,
                    SharedConstants.NotAvailable
                    });
            }
		}
		catch (Exception ex)
		{
			protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
		}
	}
}
