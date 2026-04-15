
using Skyline.DataMiner.Scripting;
using System;

public static class QAction
{
    public static void Run(SLProtocol protocol)
    {
        try
        {
            double current = Convert.ToDouble(protocol.GetParameter(73));
            double newVal = (current == 1) ? 0.0 : 1.0;
            protocol.SetParameter(73, newVal);
        }
        catch (Exception ex)
        {
            protocol.Log($"QA{protocol.QActionID}|Run|{ex}", LogType.Error, LogLevel.NoLogging);
        }
    }
}
