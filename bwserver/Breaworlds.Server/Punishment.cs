using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Breaworlds.Server
{
	internal class Punishment
	{
		public static readonly string Filename = "./punishments.bin";

		public static readonly string Temporary = "./punishments.tmp";

		public static List<PunishmentData> Punishments = new List<PunishmentData>();

		public static void Initialize()
		{
			Deserialize();
		}

		private static void Serialize()
		{
			try
			{
				FileStream output = new FileStream(Temporary, FileMode.Create, FileAccess.Write, FileShare.None);
				using (BinaryWriter binaryWriter = new BinaryWriter(output))
				{
					PunishmentData[] array = Punishments.ToArray();
					for (int i = 0; i < array.Length; i++)
					{
						PunishmentData item = array[i];
						if (Left(item.Time) <= 0)
						{
							Punishments.Remove(item);
						}
					}
					binaryWriter.Write(Punishments.Count);
					PunishmentData[] array2 = Punishments.ToArray();
					for (int j = 0; j < array2.Length; j++)
					{
						PunishmentData punishmentData = array2[j];
						binaryWriter.Write(punishmentData.Host);
						binaryWriter.Write(punishmentData.Name);
						binaryWriter.Write(punishmentData.Time);
					}
				}
				File.Copy(Temporary, Filename, overwrite: true);
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
			finally
			{
				if (File.Exists(Temporary))
				{
					File.Delete(Temporary);
				}
			}
		}

		private static void Deserialize()
		{
			try
			{
				Punishments.Clear();
				if (!File.Exists(Filename))
				{
					return;
				}
				FileStream input = new FileStream(Filename, FileMode.Open, FileAccess.Read, FileShare.Read);
				using BinaryReader binaryReader = new BinaryReader(input);
				int num = binaryReader.ReadInt32();
				for (int i = 0; i < num; i++)
				{
					PunishmentData punishmentData = default(PunishmentData);
					punishmentData.Host = binaryReader.ReadString();
					punishmentData.Name = binaryReader.ReadString();
					punishmentData.Time = binaryReader.ReadInt32();
					PunishmentData item = punishmentData;
					if (Left(item.Time) > 0)
					{
						Punishments.Add(item);
					}
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
		}

		public static void Blacklist(string host, int seconds)
		{
			try
			{
				TimeSpan timeSpan = DateTime.UtcNow.Subtract(Server.Date);
				PunishmentData[] array = Punishments.ToArray();
				for (int i = 0; i < array.Length; i++)
				{
					PunishmentData punishmentData = array[i];
					if (punishmentData.Host.ToLower() == host.ToLower())
					{
						return;
					}
				}
				string text = $"Temp-{Server.Random.Next(111111, 999999)}";
				Punishments.Add(new PunishmentData
				{
					Host = host,
					Name = text,
					Time = Convert.ToInt32(timeSpan.TotalSeconds) + seconds
				});
				Server.SendReason(text, $"Has been blacklisted for {Text.TimeLong(seconds)} for attempting to guess someone's token.");
				Serialize();
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
		}

		public static void Ban(Player sender, string name, int seconds, string reason, bool silent, bool warning, bool location)
		{
			try
			{
				if (Database.ProfileExists(name))
				{
					ProfileDataHandle data = new ProfileDataHandle(binded: false);
					Database.ProfileLoad(ref data, name);
					TimeSpan timeSpan = DateTime.UtcNow.Subtract(Server.Date);
					data.BanDuration = Convert.ToInt32(timeSpan.TotalSeconds) + seconds;
					Player player2 = null;
					Player[] array = Server.Online.ToArray();
					foreach (Player player3 in array)
					{
						if (player3.Active && player3.Profile.Active && player3.Profile.Data.Filename.ToLower() == name.ToLower())
						{
							player2 = player3;
							break;
						}
					}
					if (location)
					{
						PunishmentData[] array2 = Punishments.ToArray();
						for (int j = 0; j < array2.Length; j++)
						{
							PunishmentData item = array2[j];
							if (item.Host.ToLower() == data.Address.ToLower() || item.Name.ToLower() == data.Filename.ToLower())
							{
								Punishments.Remove(item);
							}
						}
						Punishments.Add(new PunishmentData
						{
							Host = data.Address,
							Name = data.Filename,
							Time = Convert.ToInt32(timeSpan.TotalSeconds) + Math.Min(seconds, 86400)
						});
						Serialize();
					}
					if (player2 != null && warning)
					{
						MemoryStream memoryStream = new MemoryStream();
						BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
						binaryWriter.Write(Convert.ToUInt16(0));
						binaryWriter.Write(Convert.ToUInt16(35));
						binaryWriter.Write(Convert.ToUInt16(200));
						binaryWriter.Write(Convert.ToUInt16(1));
						binaryWriter.Write(Encoding.UTF8.GetBytes("~1YOU HAVE BEEN ~3BANNED~1!\0"));
						binaryWriter.Write(Encoding.UTF8.GetBytes($"Your account has been banned for ~1{Text.Time(seconds)} ~0for breaking the game rules." + "\0"));
						binaryWriter.Write(Encoding.UTF8.GetBytes("Contact ~5support@breaworldsgame.com ~0if you have any questions.\0"));
						binaryWriter.Seek(0, SeekOrigin.Begin);
						binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
						player2.Send(memoryStream.ToArray());
						binaryWriter.Close();
					}
					if (!silent)
					{
						MemoryStream memoryStream2 = new MemoryStream();
						BinaryWriter binaryWriter2 = new BinaryWriter(memoryStream2);
						binaryWriter2.Write(Convert.ToUInt16(0));
						binaryWriter2.Write(Convert.ToUInt16(4));
						string text = $"~0[~1{data.Filename}~0 has been ~3banned ~0for breaking the game rules]";
						binaryWriter2.Write(Encoding.UTF8.GetBytes(text + "\0"));
						binaryWriter2.Seek(0, SeekOrigin.Begin);
						binaryWriter2.Write(Convert.ToUInt16(memoryStream2.Length));
						Server.Broadcast(memoryStream2.ToArray(), delegate(Player player)
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
						binaryWriter2.Close();
					}
					else
					{
						MemoryStream memoryStream3 = new MemoryStream();
						BinaryWriter binaryWriter3 = new BinaryWriter(memoryStream3);
						binaryWriter3.Write(Convert.ToUInt16(0));
						binaryWriter3.Write(Convert.ToUInt16(4));
						string text2 = $"~0[~1{data.Filename}~0 has been ~3silently-banned ~0for breaking the game rules]";
						binaryWriter3.Write(Encoding.UTF8.GetBytes(text2 + "\0"));
						binaryWriter3.Seek(0, SeekOrigin.Begin);
						binaryWriter3.Write(Convert.ToUInt16(memoryStream3.Length));
						Server.Broadcast(memoryStream3.ToArray(), delegate(Player player)
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
						binaryWriter3.Close();
					}
					if (!silent)
					{
						Server.SendReason(sender.Profile.Data.Filename, string.Format("{0} {1} for {2}, reason: {3}", location ? "Hard-Banned" : "Soft-Banned", data.Filename, Text.TimeLong(seconds), reason));
					}
					else
					{
						Server.SendReason(sender.Profile.Data.Filename, string.Format("{0} {1} for {2}, reason: {3}", location ? "Silently Hard-Banned" : "Silently Soft-Banned", data.Filename, Text.TimeLong(seconds), reason));
					}
					Database.ProfileSave(data);
					Database.ProfileClose(name, binded: false);
					player2?.Close();
				}
				else
				{
					MemoryStream memoryStream4 = new MemoryStream();
					BinaryWriter binaryWriter4 = new BinaryWriter(memoryStream4);
					binaryWriter4.Write(Convert.ToUInt16(0));
					binaryWriter4.Write(Convert.ToUInt16(4));
					string text3 = "Specified account does not exist.";
					binaryWriter4.Write(Encoding.UTF8.GetBytes(text3 + "\0"));
					binaryWriter4.Seek(0, SeekOrigin.Begin);
					binaryWriter4.Write(Convert.ToUInt16(memoryStream4.Length));
					sender.Send(memoryStream4.ToArray());
					binaryWriter4.Close();
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
		}

		public static void Mute(Player sender, string name, int seconds, string reason, bool silent, bool warning)
		{
			try
			{
				if (Database.ProfileExists(name))
				{
					ProfileDataHandle data = new ProfileDataHandle(binded: false);
					Database.ProfileLoad(ref data, name);
					TimeSpan timeSpan = DateTime.UtcNow.Subtract(Server.Date);
					data.MuteDuration = Convert.ToInt32(timeSpan.TotalSeconds) + seconds;
					Player player2 = null;
					Player[] array = Server.Online.ToArray();
					foreach (Player player3 in array)
					{
						if (player3.Active && player3.Profile.Active && player3.Profile.Data.Filename.ToLower() == name.ToLower())
						{
							player2 = player3;
							break;
						}
					}
					if (player2 != null && warning)
					{
						MemoryStream memoryStream = new MemoryStream();
						BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
						binaryWriter.Write(Convert.ToUInt16(0));
						binaryWriter.Write(Convert.ToUInt16(35));
						binaryWriter.Write(Convert.ToUInt16(200));
						binaryWriter.Write(Convert.ToUInt16(1));
						binaryWriter.Write(Encoding.UTF8.GetBytes("~1YOU HAVE BEEN ~3MUTED~1!\0"));
						binaryWriter.Write(Encoding.UTF8.GetBytes($"Your account has been muted for ~1{Text.Time(seconds)} ~0for breaking the game rules." + "\0"));
						binaryWriter.Write(Encoding.UTF8.GetBytes("Contact ~5support@breaworldsgame.com ~0if you have any questions.\0"));
						binaryWriter.Seek(0, SeekOrigin.Begin);
						binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
						player2.Send(memoryStream.ToArray());
						binaryWriter.Close();
					}
					if (!silent)
					{
						MemoryStream memoryStream2 = new MemoryStream();
						BinaryWriter binaryWriter2 = new BinaryWriter(memoryStream2);
						binaryWriter2.Write(Convert.ToUInt16(0));
						binaryWriter2.Write(Convert.ToUInt16(4));
						string text = $"~0[~1{data.Filename}~0 has been ~3muted ~0for breaking the game rules]";
						binaryWriter2.Write(Encoding.UTF8.GetBytes(text + "\0"));
						binaryWriter2.Seek(0, SeekOrigin.Begin);
						binaryWriter2.Write(Convert.ToUInt16(memoryStream2.Length));
						Server.Broadcast(memoryStream2.ToArray(), delegate(Player player)
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
						binaryWriter2.Close();
					}
					Server.SendReason(sender.Profile.Data.Filename, string.Format("{0} {1} for {2}, reason: {3}", "Soft-Muted", data.Filename, Text.TimeLong(seconds), reason));
					Database.ProfileSave(data);
					Database.ProfileClose(name, binded: false);
				}
				else
				{
					MemoryStream memoryStream3 = new MemoryStream();
					BinaryWriter binaryWriter3 = new BinaryWriter(memoryStream3);
					binaryWriter3.Write(Convert.ToUInt16(0));
					binaryWriter3.Write(Convert.ToUInt16(4));
					string text2 = "Specified account does not exist.";
					binaryWriter3.Write(Encoding.UTF8.GetBytes(text2 + "\0"));
					binaryWriter3.Seek(0, SeekOrigin.Begin);
					binaryWriter3.Write(Convert.ToUInt16(memoryStream3.Length));
					sender.Send(memoryStream3.ToArray());
					binaryWriter3.Close();
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
		}

		public static bool HostBanned(string host, out PunishmentData data)
		{
			PunishmentData[] array = Punishments.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				PunishmentData punishmentData = array[i];
				if (punishmentData.Host.ToLower() == host.ToLower() && Left(punishmentData.Time) > 0)
				{
					data = punishmentData;
					return true;
				}
			}
			data = default(PunishmentData);
			return false;
		}

		public static int Left(int time)
		{
			return (int)Math.Floor((double)time - DateTime.UtcNow.Subtract(Server.Date).TotalSeconds);
		}
	}
}
