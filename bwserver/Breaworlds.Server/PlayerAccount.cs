using System;
using System.IO;
using System.Text;

namespace Breaworlds.Server
{
	public class PlayerAccount
	{
		public static void ResponseLogin(Player invoker, bool success, string username, string password, string message, params object[] arguments)
		{
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(1));
				binaryWriter.Write(Convert.ToBoolean(success));
				binaryWriter.Write(Encoding.UTF8.GetBytes(string.Format(message, arguments) + "\0"));
				if (success)
				{
					binaryWriter.Write(Encoding.UTF8.GetBytes(username + "\0"));
					binaryWriter.Write(Encoding.UTF8.GetBytes(password + "\0"));
				}
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

		public static void ResponseRegister(Player invoker, bool success, string username, string password, string message, params object[] arguments)
		{
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(2));
				binaryWriter.Write(Convert.ToBoolean(success));
				binaryWriter.Write(Encoding.UTF8.GetBytes(string.Format(message, arguments) + "\0"));
				if (success)
				{
					binaryWriter.Write(Encoding.UTF8.GetBytes(username + "\0"));
					binaryWriter.Write(Encoding.UTF8.GetBytes(password + "\0"));
				}
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

		public static void ResponseRecover(Player invoker, string message, params object[] arguments)
		{
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(3));
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
