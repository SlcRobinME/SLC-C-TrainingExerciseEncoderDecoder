using System;
using Skyline.DataMiner.Scripting;

/// <summary>
/// DataMiner QAction Class.
/// </summary>
public static class QAction
{
    public enum ParamState
    {
        NA = -1,
        Disabled = 0,
        Enabled = 1,
    }

    /// <summary>
    /// The QAction entry point.
    /// </summary>
    /// <param name="protocol">Link with SLProtocol process.</param>
    public static void Run(SLProtocol protocol)
	{
        var rng = new Random();

        object[] statuses = (object[])protocol.GetParameters(new uint[]
        {
            Parameter.encoderstatus_4,
            Parameter.decoderstatus_5,
        });

        // Only update if Encoder is Enabled
        if (Convert.ToUInt16(statuses[0]) == (ushort)ParamState.Enabled)
        {
            double newEncoderBitrate = Math.Round(rng.NextDouble() * 47, 3);
            protocol.SetParameters(
                new int[] { Parameter.encodercurrentcompressedbitrate_6, Parameter.copyofencodercurrentcompressedbitrate_106 },
                new object[] { newEncoderBitrate, newEncoderBitrate });
        }

        // Only update if Decoder is Enabled
        if (Convert.ToUInt16(statuses[1]) == (ushort)ParamState.Enabled)
        {
            double newDecoderBitrate = Math.Round(rng.NextDouble() * 47, 3);
            protocol.SetParameters(
                new int[] { Parameter.decodercurrentcompressedbitrate_7, Parameter.copyofdecodercurrentcompressedbitrate_107 },
                new object[] { newDecoderBitrate, newDecoderBitrate });
        }
    }
}
