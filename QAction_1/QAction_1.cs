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
			//Encoder
			protocol.SetParameter(100, 1);
			protocol.SetParameter(101, 1);
			protocol.SetParameter(102, 150);
			protocol.SetParameter(103, 0);
			protocol.SetParameter(104, 0);
			protocol.SetParameter(105, 75.0);
			protocol.SetParameter(106, 75.0);
			protocol.SetParameter(107, 0);
			protocol.SetParameter(108, 0);
			//Decoder
			protocol.SetParameter(200, 0);
            protocol.SetParameter(201, 0);
            protocol.SetParameter(202, -1);
            protocol.SetParameter(203, -1);
            protocol.SetParameter(204, -1);
            protocol.SetParameter(205, -1);
            protocol.SetParameter(206, -1);
        }
        catch (Exception ex)
		{
			protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
		}
	}
}
