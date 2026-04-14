using System;
using Skyline.DataMiner.Scripting;

public class QAction
{
    public static void Run(SLProtocol protocol)
    {
        try
        {
            int value = Convert.ToInt32(protocol.GetParameter(70));

            protocol.Log($"[SET MODE] value={value}", LogType.Allways, LogLevel.NoLogging);

            protocol.SetParameter(170, value);

            protocol.Log($"[STATE] 170={value}", LogType.Allways, LogLevel.NoLogging);
        }
        catch (Exception ex)
        {
            protocol.Log("[ERROR] " + ex.Message, LogType.Error, LogLevel.NoLogging);
        }
    }
}
