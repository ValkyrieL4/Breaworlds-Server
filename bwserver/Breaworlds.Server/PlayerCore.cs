using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Breaworlds.Server
{
	public class PlayerCore
	{
		public static void Reconnect(Player invoker, string host, ushort port)
		{
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Encoding.UTF8.GetBytes(host + "\0"));
				binaryWriter.Write(Convert.ToUInt16(port));
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

		public static void UpdateCharacter(Player invoker)
		{
			try
			{
				Player[] array = Server.Online.ToArray();
				foreach (Player player in array)
				{
					if (player.Active && player.Profile.Active && player.Session.Active && !(player.Session.Data.Name != invoker.Session.Data.Name))
					{
						MemoryStream memoryStream = new MemoryStream();
						BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
						binaryWriter.Write(Convert.ToUInt16(0));
						binaryWriter.Write(Convert.ToUInt16(14));
						if (player == invoker)
						{
							binaryWriter.Write(Convert.ToInt32(0));
						}
						else
						{
							binaryWriter.Write(Convert.ToInt32(invoker.Identifier));
						}
						invoker.Profile.WriteData(binaryWriter, player);
						binaryWriter.Seek(0, SeekOrigin.Begin);
						binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
						player.Send(memoryStream.ToArray());
						binaryWriter.Close();
					}
				}
			}
			catch (Exception)
			{
				invoker.Close();
			}
		}

		public static void UpdatePosition(Player invoker, int x, int y)
		{
			try
			{
				invoker.CurrentX = x;
				invoker.CurrentY = y;
				Player[] array = Server.Online.ToArray();
				foreach (Player player in array)
				{
					if (player.Active && player.Profile.Active && player.Session.Active && !(player.Session.Data.Name != invoker.Session.Data.Name))
					{
						MemoryStream memoryStream = new MemoryStream();
						BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
						binaryWriter.Write(Convert.ToUInt16(0));
						binaryWriter.Write(Convert.ToUInt16(13));
						if (invoker == player)
						{
							binaryWriter.Write(Convert.ToInt32(0));
						}
						else
						{
							binaryWriter.Write(Convert.ToInt32(invoker.Identifier));
						}
						binaryWriter.Write(Convert.ToBoolean(value: false));
						binaryWriter.Write(Convert.ToUInt16(invoker.CurrentX));
						binaryWriter.Write(Convert.ToUInt16(invoker.CurrentY));
						binaryWriter.Seek(0, SeekOrigin.Begin);
						binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
						player.Send(memoryStream.ToArray());
						binaryWriter.Close();
					}
				}
			}
			catch (Exception)
			{
				invoker.Close();
			}
		}

		public static void DestroyCharacter(Player invoker)
		{
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(13));
				binaryWriter.Write(Convert.ToInt32(invoker.Identifier));
				binaryWriter.Write(Convert.ToBoolean(value: true));
				binaryWriter.Seek(0, SeekOrigin.Begin);
				binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
				Server.Broadcast(memoryStream.ToArray(), delegate(Player player)
				{
					if (player == invoker)
					{
						return false;
					}
					if (!player.Active)
					{
						return false;
					}
					if (!player.Profile.Active)
					{
						return false;
					}
					if (!player.Session.Active)
					{
						return false;
					}
					return (!(player.Session.Data.Name != invoker.Session.Data.Name)) ? true : false;
				});
				binaryWriter.Close();
			}
			catch (Exception)
			{
				invoker.Close();
			}
		}

		public static void UpdateAnimation(Player invoker, int animation)
		{
			try
			{
				Player[] array = Server.Online.ToArray();
				foreach (Player player in array)
				{
					if (player.Active && player.Profile.Active && player.Session.Active && !(player.Session.Data.Name != invoker.Session.Data.Name))
					{
						MemoryStream memoryStream = new MemoryStream();
						BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
						binaryWriter.Write(Convert.ToUInt16(0));
						binaryWriter.Write(Convert.ToUInt16(29));
						if (invoker == player)
						{
							binaryWriter.Write(Convert.ToInt32(0));
						}
						else
						{
							binaryWriter.Write(Convert.ToInt32(invoker.Identifier));
						}
						binaryWriter.Write(Convert.ToUInt16(animation));
						binaryWriter.Seek(0, SeekOrigin.Begin);
						binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
						player.Send(memoryStream.ToArray());
						binaryWriter.Close();
					}
				}
			}
			catch (Exception)
			{
				invoker.Close();
			}
		}

		public static void UpdateInventory(Player invoker)
		{
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(6));
				invoker.Profile.WriteItems(binaryWriter);
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

		public static void UpdateOptions(Player invoker)
		{
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(7));
				invoker.Profile.WriteOptions(binaryWriter);
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

		public static void UpdateGems(Player invoker)
		{
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(16));
				binaryWriter.Write(Convert.ToInt32(PlayerGems.Get(invoker)));
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

		public static void UpdateSpecialCurrency(Player invoker)
		{
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(38));
				binaryWriter.Write(Convert.ToInt32(PlayerSpecialCurrency.Get(invoker)));
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

		public static void UpdateDialog(Player invoker, int worker, string name, Action<BinaryWriter> callback)
		{
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(5));
				invoker.Window = Dialog.Create(binaryWriter, worker, name);
				callback(binaryWriter);
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

		public static void CancelTimer(Player invoker)
		{
			try
			{
				invoker.TimerIdentifier = 0;
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(37));
				binaryWriter.Write(Convert.ToBoolean(value: false));
				binaryWriter.Write(Convert.ToByte(0));
				binaryWriter.Write(Convert.ToByte(0));
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

		public static async void UpdateTimer(Player invoker, int seconds, Action action)
		{
			try
			{
				MemoryStream stream = new MemoryStream();
				BinaryWriter writer = new BinaryWriter(stream);
				writer.Write(Convert.ToUInt16(0));
				writer.Write(Convert.ToUInt16(37));
				writer.Write(Convert.ToBoolean(value: true));
				writer.Write(Convert.ToByte(seconds / 60));
				writer.Write(Convert.ToByte(seconds % 60));
				writer.Seek(0, SeekOrigin.Begin);
				writer.Write(Convert.ToUInt16(stream.Length));
				invoker.Send(stream.ToArray());
				writer.Close();
				int identifier = (invoker.TimerIdentifier = Server.Random.Next(int.MinValue, int.MaxValue));
				await Task.Delay(seconds * 1000);
				if (!invoker.Closed && invoker.TimerIdentifier == identifier)
				{
					action();
				}
			}
			catch (Exception)
			{
				invoker.Close();
			}
		}

		public static async void Schedule(Player invoker, int delay, int identifier, Action<Player, int> action)
		{
			try
			{
				await Task.Delay(delay * 1000);
				if (!invoker.Closed)
				{
					action(invoker, identifier);
				}
			}
			catch (Exception)
			{
				invoker.Close();
			}
		}
	}
}
