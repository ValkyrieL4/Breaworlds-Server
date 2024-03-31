using System;
using System.IO;

namespace Breaworlds.Server
{
	public class PlayerSession
	{
		public static void UpdateWhole(Player invoker)
		{
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(10));
				invoker.Session.WriteTiles(binaryWriter, invoker.Profile.Data.Filename);
				binaryWriter.Seek(0, SeekOrigin.Begin);
				binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
				invoker.Send(memoryStream.ToArray());
				binaryWriter.Close();
			}
			catch (Exception)
			{
				invoker.Close();
			}
		}

		private static void UpdateTile(Player invoker, int x, int y, int layer, int value)
		{
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(11));
				binaryWriter.Write(Convert.ToUInt16(x));
				binaryWriter.Write(Convert.ToUInt16(y));
				binaryWriter.Write(Convert.ToUInt16(layer));
				binaryWriter.Write(Convert.ToUInt16(value));
				binaryWriter.Seek(0, SeekOrigin.Begin);
				binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
				invoker.Send(memoryStream.ToArray());
				binaryWriter.Close();
			}
			catch (Exception)
			{
				invoker.Close();
			}
		}

		public static void UpdateBackground(Player invoker, int x, int y, int value)
		{
			UpdateTile(invoker, x, y, 1, value);
		}

		public static void UpdateBackground(Player invoker, int index, int value)
		{
			UpdateTile(invoker, invoker.Session.GetTileX(index), invoker.Session.GetTileY(index), 1, value);
		}

		public static void UpdateForeground(Player invoker, int x, int y, int value)
		{
			UpdateTile(invoker, x, y, 2, value);
		}

		public static void UpdateForeground(Player invoker, int index, int value)
		{
			UpdateTile(invoker, invoker.Session.GetTileX(index), invoker.Session.GetTileY(index), 2, value);
		}

		public static void UpdateProperty(Player invoker, int x, int y, int value)
		{
			UpdateTile(invoker, x, y, 3, value);
		}

		public static void UpdateProperty(Player invoker, int index, int value)
		{
			UpdateTile(invoker, invoker.Session.GetTileX(index), invoker.Session.GetTileY(index), 3, value);
		}
	}
}
