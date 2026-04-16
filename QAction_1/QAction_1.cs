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
    public static void Run(SLProtocol protocol)
	{
		try
		{
            protocol.SetParameters(
                new int[] {
                Parameter.operationmode,
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
                SharedConstants.EncoderMode,
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
        catch (Exception ex)
		{
			protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
		}
	}
}
