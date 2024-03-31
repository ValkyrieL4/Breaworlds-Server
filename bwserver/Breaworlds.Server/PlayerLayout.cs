using System;
using System.IO;
using System.Text;

namespace Breaworlds.Server
{
	public class PlayerLayout
	{
		public static void Warning(Player invoker, int time, int icon, params string[] arguments)
		{
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(35));
				binaryWriter.Write(Convert.ToUInt16(time));
				binaryWriter.Write(Convert.ToUInt16(icon));
				binaryWriter.Write(Encoding.UTF8.GetBytes(((arguments.Length != 0) ? arguments[0] : string.Empty) + "\0"));
				binaryWriter.Write(Encoding.UTF8.GetBytes(((arguments.Length > 1) ? arguments[1] : string.Empty) + "\0"));
				binaryWriter.Write(Encoding.UTF8.GetBytes(((arguments.Length > 2) ? arguments[2] : string.Empty) + "\0"));
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

		public static void Notification(Player invoker, int time, int icon, string message, params object[] arguments)
		{
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(17));
				binaryWriter.Write(Convert.ToUInt16(time));
				binaryWriter.Write(Convert.ToUInt16(icon));
				binaryWriter.Write(Encoding.UTF8.GetBytes(string.Format(message, arguments) + "\0"));
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
	}
}
