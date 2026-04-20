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
            int triggerParam = protocol.GetTriggerParameter();
            double readValueOM_Encoder = (double)protocol.GetParameter(Parameter.encodermode_20);
            double readValueOM_Decoder = (double)protocol.GetParameter(Parameter.decodermode_21);

            if (triggerParam == Parameter.encodermode_20 || triggerParam == Parameter.Write.encodermode_70)
            {
                if (readValueOM_Encoder == (double)ParamState.Enabled)
                {
                    protocol.SetParameter(Parameter.decodermode_21, ParamState.Disabled);
                    protocol.SetParameter(Parameter.Write.decodermode_71, ParamState.Disabled);
                }
            }
            else if (triggerParam == Parameter.decodermode_21 || triggerParam == Parameter.Write.decodermode_71)
            {
                if (readValueOM_Decoder == (double)ParamState.Enabled)
                {
                    protocol.SetParameter(Parameter.encodermode_20, ParamState.Disabled);
                    protocol.SetParameter(Parameter.Write.encodermode_70, ParamState.Disabled);
                }
            }
        }
        catch (Exception ex)
        {
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
        }
    }
}
