using System;
using System.IO;
using System.Text;

namespace Breaworlds.Server
{
	public class PlayerConsole
	{
		public static void Message(Player invoker, string message, params object[] arguments)
		{
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(4));
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

		public static void PrivateMessage(Player invoker, string recipient, string message)
		{
			
			try
			{
				recipient = Text.FilterColor(recipient).ToLower();
				if (Rewards.Capitalization.Contains(invoker.Profile.Data.Filename))
				{
					message = Text.Capitalize(message);
				}
				if (invoker.Profile.GetMute() > 0)
				{
					Message(invoker, "~3You are muted. ~0You will be able to talk again in ~1{0}~0.", Text.Time(invoker.Profile.GetMute()));
					return;
				}
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(4));
				message = $"~0[~5Private message from ~1{invoker.Profile.Data.Username}~0] {message}";
				binaryWriter.Write(Encoding.UTF8.GetBytes(message + "\0"));
				binaryWriter.Seek(0, SeekOrigin.Begin);
				binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
				int num = Server.Broadcast(memoryStream.ToArray(), delegate(Player player)
				{
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
					if (Text.FilterColor(player.Profile.Data.Username).ToLower() != recipient)
					{
						return false;
					}
					player.Profile.Reply = invoker.Profile.Data.Username;
					return true;
				});
				binaryWriter.Close();
				if (num == 0)
				{
					Message(invoker, "The specified player is currently offline, so the message was not delivered.");
				}
				else
				{
					Message(invoker, "Private message sent to the specified player.");
				}
			}
			catch (Exception)
			{
				invoker.Close();
			}
		}

		public static void GlobalMessage(Player invoker, string message)
		{
			try
			{
				int num = 100;
				if (Rewards.Permission(invoker.Profile.Data.Filename, Permissions.Mod))
				{
					num = 10;
				}
				if (Rewards.Permission(invoker.Profile.Data.Filename, Permissions.Admin))
				{
					num = 0;
				}
				if (Rewards.Capitalization.Contains(invoker.Profile.Data.Filename))
				{
					message = Text.Capitalize(message);
				}
				if (invoker.Profile.GetMute() > 0)
				{
					Message(invoker, "~3You are muted. ~0You will be able to talk again in ~1{0}~0.", Text.Time(invoker.Profile.GetMute()));
					return;
				}
				if (invoker.Profile.Data.Level < 5)
				{
					Message(invoker, "~3Message not sent. ~0You need to be at least level 5 to send global messages.");
					return;
				}
				if (invoker.LastGlobalMessage + 60000 > DateTime.UtcNow.Ticks / 10000)
				{
					Message(invoker, "~3Message not sent. ~0You'll need to wait a little before sending another global message.");
					return;
				}
				if (!PlayerGems.Has(invoker, num))
				{
					Message(invoker, "~3Message not sent. ~0Not enough gems, global message costs ~1{0} ~0gems.", num);
					return;
				}
				if (!Rewards.Permission(invoker.Profile.Data.Filename, Permissions.Guard))
				{
					invoker.LastGlobalMessage = DateTime.UtcNow.Ticks / 10000;
				}
				PlayerGems.Remove(invoker, num);
				Database.ProfileSave(invoker.Profile.Data);
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(4));
				message = $"~0[Global message from ~1{invoker.Profile.Data.Username}~0] {message}";
				binaryWriter.Write(Encoding.UTF8.GetBytes(message + "\0"));
				binaryWriter.Seek(0, SeekOrigin.Begin);
				binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
				Server.Broadcast(memoryStream.ToArray(), delegate(Player player)
				{
					if (!player.Active)
					{
						return false;
					}
					if (!player.Profile.Active)
					{
						return false;
					}
					return player.Session.Active ? true : false;
				});
				binaryWriter.Close();
				Message(invoker, "Global message sent to everyone in game, used ~1{0} ~0gems.", num);
			}
			catch (Exception)
			{
				invoker.Close();
			}
		}

		public static void StaffMessage(Player invoker, string message)
		{
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(4));
				message = $"~4[Staff chat] [~1{invoker.Profile.Data.Filename}~4]~0 {message}";
				binaryWriter.Write(Encoding.UTF8.GetBytes(message + "\0"));
				binaryWriter.Seek(0, SeekOrigin.Begin);
				binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
				Server.Broadcast(memoryStream.ToArray(), delegate(Player player)
				{
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
					return Rewards.Permission(player.Profile.Data.Filename, Permissions.Guard) ? true : false;
				});
				binaryWriter.Close();
			}
			catch (Exception)
			{
				invoker.Close();
			}
		}

		public static void FriendMessage(Player invoker, string message)
		{
			try
			{
				if (Rewards.Capitalization.Contains(invoker.Profile.Data.Filename))
				{
					message = Text.Capitalize(message);
				}
				if (invoker.Profile.GetMute() > 0)
				{
					Message(invoker, "~3You are muted. ~0You will be able to talk again in ~1{0}~0.", Text.Time(invoker.Profile.GetMute()));
					return;
				}
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(4));
				message = $"~0[Friend message from ~1{invoker.Profile.Data.Username}~0] {message}";
				binaryWriter.Write(Encoding.UTF8.GetBytes(message + "\0"));
				binaryWriter.Seek(0, SeekOrigin.Begin);
				binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
				int num = Server.Broadcast(memoryStream.ToArray(), delegate(Player player)
				{
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
					if (!player.Profile.Data.Friends.Contains(invoker.Profile.Data.Filename))
					{
						return false;
					}
					return (!player.Profile.Data.FriendOffline) ? true : false;
				});
				binaryWriter.Close();
				if (num == 0)
				{
					Message(invoker, "You have no friends that are online, so the message was not sent.");
				}
				else
				{
					Message(invoker, "Friend message sent to all online friends.");
				}
			}
			catch (Exception)
			{
				invoker.Close();
			}
		}

		public static void FriendNotification(Player invoker, string message)
		{
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(4));
				message = $"~0[Friend ~1{invoker.Profile.Data.Filename} ~0{message}]";
				binaryWriter.Write(Encoding.UTF8.GetBytes(message + "\0"));
				binaryWriter.Seek(0, SeekOrigin.Begin);
				binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
				Server.Broadcast(memoryStream.ToArray(), delegate(Player player)
				{
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
					return player.Profile.Data.Friends.Contains(invoker.Profile.Data.Filename) ? true : false;
				});
				binaryWriter.Close();
			}
			catch (Exception)
			{
				invoker.Close();
			}
		}
	}
}
