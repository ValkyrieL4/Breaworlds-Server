using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Breaworlds.Server
{
	public class Serializer
	{
		public static int ProfileSerializations;

		public static int SessionSerializations;

		public static int ChallengeSerializations;

		public static int ProfileDeserializations;

		public static int SessionDeserializations;

		public static int ChallengeDeserializations;

		public static void SerializeProfile(BinaryWriter writer, ProfileData data)
		{
			try
			{
				writer.Write(Convert.ToInt32(9));
				if (data.Filename == null)
				{
					writer.Write(string.Empty);
				}
				else
				{
					writer.Write(data.Filename);
				}
				if (data.Username == null)
				{
					writer.Write(string.Empty);
				}
				else
				{
					writer.Write(data.Username);
				}
				if (data.Password == null)
				{
					writer.Write(string.Empty);
				}
				else
				{
					writer.Write(data.Password);
				}
				if (data.Mailname == null)
				{
					writer.Write(string.Empty);
				}
				else
				{
					writer.Write(data.Mailname);
				}
				if (data.Device == null)
				{
					writer.Write(string.Empty);
				}
				else
				{
					writer.Write(data.Device);
				}
				if (data.StaffVerifiedDevice == null)
				{
					writer.Write(string.Empty);
				}
				else
				{
					writer.Write(data.StaffVerifiedDevice);
				}
				if (data.Address == null)
				{
					writer.Write(string.Empty);
				}
				else
				{
					writer.Write(data.Address);
				}
				if (data.Session == null)
				{
					writer.Write(string.Empty);
				}
				else
				{
					writer.Write(data.Session);
				}
				writer.Write(data.BanDuration);
				writer.Write(data.Warnings);
				writer.Write(data.FakeLevel);
				writer.Write(data.MuteDuration);
				writer.Write(data.Experience);
				writer.Write(data.Level);
				writer.Write(data.Gems);
				writer.Write(data.MoonRocks);
				writer.Write(data.VideoCooldown);
				writer.Write(data.RewardCooldown);
				writer.Write(data.AllowRespin);
				writer.Write(data.TicketDuration);
				writer.Write(data.HasReviewed);
				writer.Write(data.QuestType);
				writer.Write(data.QuestItem);
				writer.Write(data.QuestLeft);
				writer.Write(data.QuestLevel);
				writer.Write(data.ItemQuest);
				writer.Write(data.ItemQuestType);
				writer.Write(data.ItemQuestStep);
				writer.Write(data.ItemQuestLeft);
				if (data.Worlds == null)
				{
					writer.Write(0);
				}
				else
				{
					while (data.Worlds.Contains(null))
					{
						data.Worlds.Remove(null);
					}
					writer.Write(data.Worlds.Count);
					foreach (string world in data.Worlds)
					{
						writer.Write(world);
					}
				}
				if (data.Friends == null)
				{
					writer.Write(0);
				}
				else
				{
					while (data.Friends.Contains(null))
					{
						data.Friends.Remove(null);
					}
					writer.Write(data.Friends.Count);
					foreach (string friend in data.Friends)
					{
						writer.Write(friend);
					}
				}
				if (data.Purchases == null)
				{
					writer.Write(0);
				}
				else
				{
					while (data.Purchases.Contains(null))
					{
						data.Purchases.Remove(null);
					}
					writer.Write(data.Purchases.Count);
					foreach (string purchase in data.Purchases)
					{
						writer.Write(purchase);
					}
				}
				writer.Write(data.ItemSlots);
				if (data.ItemIndex == null)
				{
					writer.Write(0);
				}
				else
				{
					writer.Write(data.ItemIndex.Count);
					foreach (ushort item in data.ItemIndex)
					{
						writer.Write(Convert.ToUInt16(Math.Max(0, Math.Min(65535, (int)item))));
					}
				}
				if (data.ItemCount == null)
				{
					writer.Write(0);
				}
				else
				{
					writer.Write(data.ItemCount.Count);
					foreach (ushort item2 in data.ItemCount)
					{
						writer.Write(Convert.ToUInt16(Math.Max(0, Math.Min(65535, (int)item2))));
					}
				}
				if (data.ItemEquip == null)
				{
					writer.Write(0);
				}
				else
				{
					writer.Write(data.ItemEquip.Count);
					foreach (ushort item3 in data.ItemEquip)
					{
						writer.Write(Convert.ToUInt16(Math.Max(0, Math.Min(65535, (int)item3))));
					}
				}
				writer.Write(data.Options.Volume);
				writer.Write(data.Options.Sounds);
				writer.Write(data.Options.Shadow);
				writer.Write(data.Options.Smooth);
				writer.Write(data.Options.Full);
				writer.Write(data.FriendOffline);
				writer.Write(data.FriendUnknown);
				writer.Write(Achievements.List.Count);
				for (int i = 0; i < Achievements.List.Count; i++)
				{
					if (data.Achievements.Length > i)
					{
						writer.Write(data.Achievements[i]);
					}
					else
					{
						writer.Write(0);
					}
				}
				writer.Write(Convert.ToByte(Math.Max(0, Math.Min(255, data.SkinA))));
				writer.Write(Convert.ToByte(Math.Max(0, Math.Min(255, data.SkinR))));
				writer.Write(Convert.ToByte(Math.Max(0, Math.Min(255, data.SkinG))));
				writer.Write(Convert.ToByte(Math.Max(0, Math.Min(255, data.SkinB))));
				writer.Write(Convert.ToByte(data.Gender));
				if (false)
				{
					writer.Write(DateTime.UtcNow.Year);
					writer.Write(DateTime.UtcNow.Month);
					writer.Write(DateTime.UtcNow.Day);
					writer.Write(DateTime.UtcNow.Hour);
					writer.Write(DateTime.UtcNow.Minute);
					writer.Write(DateTime.UtcNow.Second);
				}
				else
				{
					writer.Write(data.RegisterDate.Year);
					writer.Write(data.RegisterDate.Month);
					writer.Write(data.RegisterDate.Day);
					writer.Write(data.RegisterDate.Hour);
					writer.Write(data.RegisterDate.Minute);
					writer.Write(data.RegisterDate.Second);
				}
				if (false)
				{
					writer.Write(DateTime.UtcNow.Year);
					writer.Write(DateTime.UtcNow.Month);
					writer.Write(DateTime.UtcNow.Day);
					writer.Write(DateTime.UtcNow.Hour);
					writer.Write(DateTime.UtcNow.Minute);
					writer.Write(DateTime.UtcNow.Second);
				}
				else
				{
					writer.Write(data.LoginDate.Year);
					writer.Write(data.LoginDate.Month);
					writer.Write(data.LoginDate.Day);
					writer.Write(data.LoginDate.Hour);
					writer.Write(data.LoginDate.Minute);
					writer.Write(data.LoginDate.Second);
				}
				writer.Write(data.Online);
				writer.Write(data.Rating);
				ProfileSerializations++;
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
		}

		public static ProfileData DeserializeProfile(BinaryReader reader)
		{
			ProfileData result = default(ProfileData);
			try
			{
				int num = reader.ReadInt32();
				result.Filename = reader.ReadString();
				result.Username = reader.ReadString();
				result.Password = reader.ReadString();
				result.Mailname = reader.ReadString();
				result.Device = reader.ReadString();
				if (num >= 7)
				{
					result.StaffVerifiedDevice = reader.ReadString();
				}
				result.Address = reader.ReadString();
				result.Session = reader.ReadString();
				result.BanDuration = reader.ReadInt32();
				result.MuteDuration = reader.ReadInt32();
				if (num >= 8)
				{
					result.Warnings = reader.ReadInt32();
				}
				if (num >= 9)
				{
					result.FakeLevel = reader.ReadInt32();
				}
				result.Experience = reader.ReadInt32();
				result.Level = reader.ReadInt32();
				result.Gems = Math.Max(0, reader.ReadInt32());
				if (num >= 6)
				{
					result.MoonRocks = Math.Max(0, reader.ReadInt32());
				}
				result.VideoCooldown = reader.ReadInt32();
				result.RewardCooldown = reader.ReadInt32();
				result.AllowRespin = reader.ReadBoolean();
				result.TicketDuration = reader.ReadInt32();
				if (num >= 2)
				{
					result.HasReviewed = reader.ReadBoolean();
				}
				result.QuestType = reader.ReadInt32();
				result.QuestItem = reader.ReadInt32();
				result.QuestLeft = reader.ReadInt32();
				result.QuestLevel = reader.ReadInt32();
				if (num >= 5)
				{
					result.ItemQuest = reader.ReadBoolean();
					result.ItemQuestType = reader.ReadUInt16();
					result.ItemQuestStep = reader.ReadUInt16();
					result.ItemQuestLeft = reader.ReadInt32();
				}
				result.Worlds = new List<string>();
				int num2 = reader.ReadInt32();
				for (int i = 0; i < num2; i++)
				{
					result.Worlds.Add(reader.ReadString());
				}
				result.Friends = new List<string>();
				int num3 = reader.ReadInt32();
				for (int j = 0; j < num3; j++)
				{
					result.Friends.Add(reader.ReadString());
				}
				result.Purchases = new List<string>();
				int num4 = reader.ReadInt32();
				for (int k = 0; k < num4; k++)
				{
					result.Purchases.Add(reader.ReadString());
				}
				result.ItemSlots = reader.ReadInt32();
				result.ItemIndex = new List<ushort>();
				int num5 = reader.ReadInt32();
				for (int l = 0; l < num5; l++)
				{
					result.ItemIndex.Add(reader.ReadUInt16());
				}
				result.ItemCount = new List<ushort>();
				int num6 = reader.ReadInt32();
				for (int m = 0; m < num6; m++)
				{
					result.ItemCount.Add(reader.ReadUInt16());
				}
				result.ItemEquip = new List<ushort>();
				int num7 = reader.ReadInt32();
				for (int n = 0; n < num7; n++)
				{
					result.ItemEquip.Add(reader.ReadUInt16());
				}
				result.Options = new Options
				{
					Volume = reader.ReadInt32(),
					Sounds = reader.ReadBoolean(),
					Shadow = reader.ReadBoolean(),
					Smooth = reader.ReadBoolean(),
					Full = reader.ReadBoolean()
				};
				result.FriendOffline = reader.ReadBoolean();
				result.FriendUnknown = reader.ReadBoolean();
				result.Achievements = new int[Achievements.List.Count];
				Array.Clear(result.Achievements, 0, result.Achievements.Length);
				if (num >= 3)
				{
					int num8 = reader.ReadInt32();
					for (int num9 = 0; num9 < num8; num9++)
					{
						int num10 = reader.ReadInt32();
						if (num9 < result.Achievements.Length)
						{
							result.Achievements[num9] = num10;
						}
					}
				}
				result.SkinA = reader.ReadByte();
				result.SkinR = reader.ReadByte();
				result.SkinG = reader.ReadByte();
				result.SkinB = reader.ReadByte();
				if (num >= 6)
				{
					result.Gender = reader.ReadByte();
				}
				try
				{
					result.RegisterDate = new DateTime(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
				}
				catch (ArgumentOutOfRangeException)
				{
					result.RegisterDate = DateTime.UtcNow;
				}
				try
				{
					result.LoginDate = new DateTime(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
				}
				catch (ArgumentOutOfRangeException)
				{
					result.LoginDate = DateTime.UtcNow;
				}
				result.Online = reader.ReadInt32();
				if (num >= 4)
				{
				}
				ProfileDeserializations++;
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
			return result;
		}

		public static void SerializeSession(BinaryWriter writer, SessionData data)
		{
			try
			{
				writer.Write(Convert.ToUInt32(5));
				if (data.Filename == null)
				{
					writer.Write(string.Empty);
				}
				else
				{
					writer.Write(data.Filename);
				}
				writer.Write(data.SizeX);
				writer.Write(data.SizeY);
				writer.Write(data.Theme);
				writer.Write(data.Banned);
				writer.Write(data.Public);
				if (data.Owner == null)
				{
					writer.Write(string.Empty);
				}
				else
				{
					writer.Write(data.Name);
				}
				if (data.Owner == null)
				{
					writer.Write(string.Empty);
				}
				else
				{
					writer.Write(data.Owner);
				}
				if (data.Drop == null)
				{
					writer.Write(0);
				}
				else
				{
					writer.Write(data.Drop.Count);
					foreach (DroppedItem item in data.Drop)
					{
						writer.Write(Convert.ToUInt16(Math.Max((ushort)0, Math.Min(ushort.MaxValue, item.X))));
						writer.Write(Convert.ToUInt16(Math.Max((ushort)0, Math.Min(ushort.MaxValue, item.Y))));
						writer.Write(Convert.ToUInt16(Math.Max((ushort)0, Math.Min(ushort.MaxValue, item.Index))));
						writer.Write(Convert.ToUInt16(Math.Max((ushort)0, Math.Min(ushort.MaxValue, item.Count))));
					}
				}
				if (data.Bans == null)
				{
					writer.Write(0);
				}
				else
				{
					writer.Write(data.Bans.Count);
					foreach (BanData ban in data.Bans)
					{
						writer.Write(ban.Time);
						writer.Write(ban.Name);
					}
				}
				if (data.Admin == null)
				{
					writer.Write(0);
				}
				else
				{
					writer.Write(data.Admin.Count);
					foreach (string item2 in data.Admin)
					{
						writer.Write(item2);
					}
				}
				if (data.Background == null)
				{
					for (int i = 0; i < data.SizeX * data.SizeY; i++)
					{
						writer.Write(Convert.ToUInt16(0));
					}
				}
				else
				{
					for (int j = 0; j < data.SizeX * data.SizeY; j++)
					{
						writer.Write(Convert.ToUInt16(data.Background[j]));
					}
				}
				if (data.Foreground == null)
				{
					for (int k = 0; k < data.SizeX * data.SizeY; k++)
					{
						writer.Write(Convert.ToUInt16(0));
					}
				}
				else
				{
					for (int l = 0; l < data.SizeX * data.SizeY; l++)
					{
						writer.Write(Convert.ToUInt16(data.Foreground[l]));
					}
				}
				if (data.Special == null)
				{
					for (int m = 0; m < data.SizeX * data.SizeY; m++)
					{
						writer.Write(Convert.ToByte(0));
					}
				}
				else
				{
					for (int n = 0; n < data.SizeX * data.SizeY; n++)
					{
						if (data.Special[n] is LockData lockData)
						{
							writer.Write(Convert.ToByte(1));
							writer.Write(lockData.Public);
							writer.Write(lockData.Ignore);
							if (lockData.Owner == null)
							{
								writer.Write(string.Empty);
							}
							else
							{
								writer.Write(lockData.Owner);
							}
							if (lockData.Admin == null)
							{
								writer.Write(0);
								continue;
							}
							while (lockData.Admin.Contains(null))
							{
								lockData.Admin.Remove(null);
							}
							writer.Write(lockData.Admin.Count);
							foreach (string item3 in lockData.Admin)
							{
								writer.Write(item3);
							}
						}
						else if (data.Special[n] is SeedData seedData)
						{
							writer.Write(Convert.ToByte(2));
							writer.Write(seedData.Time);
							writer.Write(seedData.Spliced);
						}
						else if (data.Special[n] is SignData signData)
						{
							writer.Write(Convert.ToByte(3));
							if (signData.Text == null)
							{
								writer.Write(0);
							}
							else
							{
								writer.Write(signData.Text);
							}
						}
						else if (data.Special[n] is PortalData portalData)
						{
							writer.Write(Convert.ToByte(4));
							writer.Write(portalData.Public);
							if (portalData.World == null)
							{
								writer.Write(string.Empty);
							}
							else
							{
								writer.Write(portalData.World);
							}
							if (portalData.Target == null)
							{
								writer.Write(string.Empty);
							}
							else
							{
								writer.Write(portalData.Target);
							}
						}
						else if (data.Special[n] is DoorData doorData)
						{
							writer.Write(Convert.ToByte(5));
							writer.Write(doorData.Public);
							if (doorData.Password == null)
							{
								writer.Write(string.Empty);
							}
							else
							{
								writer.Write(doorData.Password);
							}
							if (doorData.Name == null)
							{
								writer.Write(string.Empty);
							}
							else
							{
								writer.Write(doorData.Name);
							}
							if (doorData.World == null)
							{
								writer.Write(string.Empty);
							}
							else
							{
								writer.Write(doorData.World);
							}
							if (doorData.Target == null)
							{
								writer.Write(string.Empty);
							}
							else
							{
								writer.Write(doorData.Target);
							}
						}
						else if (data.Special[n] is EntranceData entranceData)
						{
							writer.Write(Convert.ToByte(6));
							writer.Write(entranceData.Closed);
							writer.Write(entranceData.Signal1);
							writer.Write(entranceData.Signal2);
						}
						else if (data.Special[n] is FurnaceData furnaceData)
						{
							writer.Write(Convert.ToByte(7));
							writer.Write(furnaceData.Item);
							writer.Write(furnaceData.Time);
						}
						else if (data.Special[n] is MailBoxData mailBoxData)
						{
							writer.Write(Convert.ToByte(8));
							if (mailBoxData.Text == null)
							{
								writer.Write(0);
								continue;
							}
							writer.Write(mailBoxData.Text.Count);
							foreach (CommentData item4 in mailBoxData.Text)
							{
								if (item4.Name == null)
								{
									writer.Write(string.Empty);
								}
								else
								{
									writer.Write(item4.Name);
								}
								if (item4.Text == null)
								{
									writer.Write(string.Empty);
								}
								else
								{
									writer.Write(item4.Text);
								}
							}
						}
						else if (data.Special[n] is BulletinData bulletinData)
						{
							writer.Write(Convert.ToByte(9));
							writer.Write(bulletinData.Public);
							writer.Write(bulletinData.Author);
							if (bulletinData.Text == null)
							{
								writer.Write(0);
								continue;
							}
							writer.Write(bulletinData.Text.Count);
							foreach (CommentData item5 in bulletinData.Text)
							{
								if (item5.Name == null)
								{
									writer.Write(string.Empty);
								}
								else
								{
									writer.Write(item5.Name);
								}
								if (item5.Text == null)
								{
									writer.Write(string.Empty);
								}
								else
								{
									writer.Write(item5.Text);
								}
							}
						}
						else if (data.Special[n] is VendingData vendingData)
						{
							writer.Write(Convert.ToByte(10));
							writer.Write(vendingData.Sold);
							writer.Write(vendingData.Index);
							writer.Write(vendingData.Count);
							writer.Write(vendingData.Price);
						}
						else if (data.Special[n] is LampData lampData)
						{
							writer.Write(Convert.ToByte(11));
							writer.Write(lampData.On);
							writer.Write(lampData.Signal1);
							writer.Write(lampData.Signal2);
						}
						else if (data.Special[n] is TrafficLightData trafficLightData)
						{
							writer.Write(Convert.ToByte(12));
							writer.Write(trafficLightData.Light);
							writer.Write(trafficLightData.Signal1);
							writer.Write(trafficLightData.Signal2);
							writer.Write(trafficLightData.Signal3);
							writer.Write(trafficLightData.Signal4);
						}
						else if (data.Special[n] is SignalSenderData signalSenderData)
						{
							writer.Write(Convert.ToByte(13));
							writer.Write(signalSenderData.Public);
							writer.Write(signalSenderData.Signal);
						}
						else if (data.Special[n] is MusicBlockData musicBlockData)
						{
							writer.Write(Convert.ToByte(14));
							writer.Write(musicBlockData.Sound);
						}
						else if (data.Special[n] is DisplayBoxData displayBoxData)
						{
							writer.Write(Convert.ToByte(15));
							writer.Write(displayBoxData.Index);
						}
						else if (data.Special[n] is SmokehouseData smokehouseData)
						{
							writer.Write(Convert.ToByte(16));
							writer.Write(smokehouseData.Time);
							writer.Write(smokehouseData.Index);
							writer.Write(smokehouseData.Count);
						}
						else if (data.Special[n] is ProviderData providerData)
						{
							writer.Write(Convert.ToByte(17));
							writer.Write(providerData.Time);
						}
						else if (data.Special[n] is GameJoinData gameJoinData)
						{
							writer.Write(Convert.ToByte(18));
							writer.Write(gameJoinData.Game);
							writer.Write(gameJoinData.Size);
							writer.Write(gameJoinData.Color);
						}
						else if (data.Special[n] is GameSpawnData gameSpawnData)
						{
							writer.Write(Convert.ToByte(19));
							writer.Write(gameSpawnData.Game);
							writer.Write(gameSpawnData.Color);
						}
						else if (data.Special[n] is GameFinishData gameFinishData)
						{
							writer.Write(Convert.ToByte(20));
							writer.Write(gameFinishData.Game);
							writer.Write(gameFinishData.Color);
						}
						else if (data.Special[n] is ChestData chestData)
						{
							writer.Write(Convert.ToByte(21));
							writer.Write(chestData.Open);
						}
						else if (data.Special[n] is HalloweenEnemyData halloweenEnemyData)
						{
							writer.Write(Convert.ToByte(22));
							writer.Write(halloweenEnemyData.Image);
							writer.Write(halloweenEnemyData.Candies);
							if (halloweenEnemyData.Target == null)
							{
								writer.Write(string.Empty);
							}
							else
							{
								writer.Write(halloweenEnemyData.Target);
							}
						}
						else
						{
							writer.Write(Convert.ToByte(0));
						}
					}
				}
				if (data.Parent == null)
				{
					for (int num = 0; num < data.SizeX * data.SizeY; num++)
					{
						writer.Write(Convert.ToByte(0));
					}
				}
				else
				{
					for (int num2 = 0; num2 < data.SizeX * data.SizeY; num2++)
					{
						if (data.Parent[num2] is Parent parent)
						{
							writer.Write(Convert.ToByte(1));
							writer.Write(Convert.ToUInt16(parent.X));
							writer.Write(Convert.ToUInt16(parent.Y));
						}
						else
						{
							writer.Write(Convert.ToByte(0));
						}
					}
				}
				writer.Write(data.AntiPunch);
				writer.Write(data.AntiTalk);
				writer.Write(data.AntiDrop);
				if (false)
				{
					writer.Write(DateTime.UtcNow.Year);
					writer.Write(DateTime.UtcNow.Month);
					writer.Write(DateTime.UtcNow.Day);
					writer.Write(DateTime.UtcNow.Hour);
					writer.Write(DateTime.UtcNow.Minute);
					writer.Write(DateTime.UtcNow.Second);
				}
				else
				{
					writer.Write(data.CreateDate.Year);
					writer.Write(data.CreateDate.Month);
					writer.Write(data.CreateDate.Day);
					writer.Write(data.CreateDate.Hour);
					writer.Write(data.CreateDate.Minute);
					writer.Write(data.CreateDate.Second);
				}
				SessionSerializations++;
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
		}

		public static SessionData DeserializeSession(BinaryReader reader)
		{
			SessionData result = default(SessionData);
			try
			{
				int num = reader.ReadInt32();
				result.Filename = reader.ReadString();
				if (num >= 3)
				{
					result.SizeX = reader.ReadInt32();
					result.SizeY = reader.ReadInt32();
				}
				else
				{
					result.SizeX = reader.ReadByte();
					result.SizeY = reader.ReadByte();
				}
				result.Theme = reader.ReadInt32();
				result.Banned = reader.ReadInt32();
				result.Public = reader.ReadBoolean();
				result.Name = reader.ReadString();
				result.Owner = reader.ReadString();
				result.Drop = new List<DroppedItem>();
				int num2 = reader.ReadInt32();
				for (int i = 0; i < num2; i++)
				{
					result.Drop.Add(new DroppedItem
					{
						X = reader.ReadUInt16(),
						Y = reader.ReadUInt16(),
						Index = reader.ReadUInt16(),
						Count = reader.ReadUInt16()
					});
				}
				if (num < 4)
				{
					int num3 = reader.ReadInt32();
					for (int j = 0; j < num3; j++)
					{
						reader.ReadInt16();
						reader.ReadInt16();
					}
				}
				result.Bans = new List<BanData>();
				if (num >= 4)
				{
					int num4 = reader.ReadInt32();
					for (int k = 0; k < num4; k++)
					{
						result.Bans.Add(new BanData
						{
							Time = reader.ReadInt32(),
							Name = reader.ReadString()
						});
					}
				}
				result.Admin = new List<string>();
				int num5 = reader.ReadInt32();
				for (int l = 0; l < num5; l++)
				{
					result.Admin.Add(reader.ReadString());
				}
				result.Background = new ushort[result.SizeX * result.SizeY];
				for (int m = 0; m < result.SizeX * result.SizeY; m++)
				{
					result.Background[m] = reader.ReadUInt16();
				}
				result.Foreground = new ushort[result.SizeX * result.SizeY];
				for (int n = 0; n < result.SizeX * result.SizeY; n++)
				{
					result.Foreground[n] = reader.ReadUInt16();
				}
				result.Special = new object[result.SizeX * result.SizeY];
				for (int num6 = 0; num6 < result.SizeX * result.SizeY; num6++)
				{
					switch (reader.ReadByte())
					{
					case 1:
					{
						LockData lockData = default(LockData);
						lockData.Public = reader.ReadBoolean();
						lockData.Ignore = reader.ReadBoolean();
						lockData.Owner = reader.ReadString();
						lockData.Admin = new ArrayList();
						LockData lockData2 = lockData;
						num5 = reader.ReadInt32();
						for (int num13 = 0; num13 < num5; num13++)
						{
							lockData2.Admin.Add(reader.ReadString());
						}
						result.Special[num6] = lockData2;
						break;
					}
					case 2:
						result.Special[num6] = new SeedData
						{
							Time = reader.ReadInt32(),
							Spliced = reader.ReadBoolean()
						};
						break;
					case 3:
						result.Special[num6] = new SignData
						{
							Text = reader.ReadString()
						};
						break;
					case 4:
						result.Special[num6] = new PortalData
						{
							Public = reader.ReadBoolean(),
							World = reader.ReadString(),
							Target = reader.ReadString()
						};
						break;
					case 5:
						result.Special[num6] = new DoorData
						{
							Public = reader.ReadBoolean(),
							Password = reader.ReadString(),
							Name = reader.ReadString(),
							World = reader.ReadString(),
							Target = reader.ReadString()
						};
						break;
					case 6:
						result.Special[num6] = new EntranceData
						{
							Closed = reader.ReadBoolean(),
							Signal1 = reader.ReadByte(),
							Signal2 = reader.ReadByte()
						};
						break;
					case 7:
						result.Special[num6] = new FurnaceData
						{
							Item = reader.ReadInt32(),
							Time = reader.ReadInt32()
						};
						break;
					case 8:
					{
						MailBoxData mailBoxData = default(MailBoxData);
						mailBoxData.Text = new List<CommentData>();
						MailBoxData mailBoxData2 = mailBoxData;
						int num11 = reader.ReadInt32();
						for (int num12 = 0; num12 < num11; num12++)
						{
							mailBoxData2.Text.Add(new CommentData
							{
								Name = reader.ReadString(),
								Text = reader.ReadString()
							});
						}
						result.Special[num6] = mailBoxData2;
						break;
					}
					case 9:
					{
						BulletinData bulletinData = default(BulletinData);
						bulletinData.Public = reader.ReadBoolean();
						bulletinData.Author = reader.ReadBoolean();
						bulletinData.Text = new List<CommentData>();
						BulletinData bulletinData2 = bulletinData;
						int num9 = reader.ReadInt32();
						for (int num10 = 0; num10 < num9; num10++)
						{
							bulletinData2.Text.Add(new CommentData
							{
								Name = reader.ReadString(),
								Text = reader.ReadString()
							});
						}
						result.Special[num6] = bulletinData2;
						break;
					}
					case 10:
						result.Special[num6] = new VendingData
						{
							Sold = reader.ReadBoolean(),
							Index = reader.ReadInt32(),
							Count = reader.ReadInt32(),
							Price = reader.ReadInt32()
						};
						break;
					case 11:
						result.Special[num6] = new LampData
						{
							On = reader.ReadBoolean(),
							Signal1 = reader.ReadByte(),
							Signal2 = reader.ReadByte()
						};
						break;
					case 12:
						result.Special[num6] = new TrafficLightData
						{
							Light = reader.ReadInt32(),
							Signal1 = reader.ReadByte(),
							Signal2 = reader.ReadByte(),
							Signal3 = reader.ReadByte(),
							Signal4 = reader.ReadByte()
						};
						break;
					case 13:
						result.Special[num6] = new SignalSenderData
						{
							Public = reader.ReadBoolean(),
							Signal = reader.ReadByte()
						};
						break;
					case 14:
						result.Special[num6] = new MusicBlockData
						{
							Sound = reader.ReadInt32()
						};
						break;
					case 15:
						result.Special[num6] = new DisplayBoxData
						{
							Index = reader.ReadInt32()
						};
						break;
					case 16:
						result.Special[num6] = new SmokehouseData
						{
							Time = reader.ReadInt32(),
							Index = reader.ReadInt32(),
							Count = reader.ReadInt32()
						};
						break;
					case 17:
						result.Special[num6] = new ProviderData
						{
							Time = reader.ReadInt32()
						};
						break;
					case 18:
						result.Special[num6] = new GameJoinData
						{
							Game = reader.ReadInt32(),
							Size = reader.ReadInt32(),
							Color = reader.ReadInt32()
						};
						break;
					case 19:
						result.Special[num6] = new GameSpawnData
						{
							Game = reader.ReadInt32(),
							Color = reader.ReadInt32()
						};
						break;
					case 20:
						result.Special[num6] = new GameFinishData
						{
							Game = reader.ReadInt32(),
							Color = reader.ReadInt32()
						};
						break;
					case 21:
						if (num <= 1)
						{
							reader.ReadString();
							result.Special[num6] = null;
						}
						else
						{
							result.Special[num6] = new ChestData
							{
								Open = reader.ReadBoolean()
							};
						}
						break;
					case 22:
						if (num <= 1)
						{
							reader.ReadString();
							reader.ReadString();
							result.Special[num6] = null;
						}
						else
						{
							result.Special[num6] = new HalloweenEnemyData
							{
								Image = reader.ReadInt32(),
								Candies = reader.ReadInt32(),
								Target = reader.ReadString()
							};
						}
						break;
					case 23:
						if (num <= 1)
						{
							reader.ReadString();
							result.Special[num6] = null;
						}
						break;
					case 24:
						if (num <= 1)
						{
							reader.ReadInt32();
							reader.ReadBoolean();
							result.Special[num6] = null;
						}
						break;
					case 25:
						if (num <= 1)
						{
							reader.ReadBoolean();
							result.Special[num6] = null;
						}
						break;
					case 26:
						if (num <= 1)
						{
							reader.ReadInt32();
							reader.ReadInt32();
							result.Special[num6] = null;
						}
						break;
					case 27:
						if (num <= 1)
						{
							reader.ReadString();
							result.Special[num6] = null;
						}
						break;
					case 28:
						if (num <= 1)
						{
							reader.ReadBoolean();
							int num7 = reader.ReadInt32();
							for (int num8 = 0; num8 < num7; num8++)
							{
								reader.ReadString();
							}
							result.Special[num6] = null;
						}
						break;
					case 29:
						if (num <= 1)
						{
							reader.ReadString();
							result.Special[num6] = null;
						}
						break;
					case 30:
						if (num <= 1)
						{
							reader.ReadString();
							result.Special[num6] = null;
						}
						break;
					default:
						result.Special[num6] = null;
						break;
					}
				}
				result.Parent = new object[result.SizeX * result.SizeY];
				for (int num14 = 0; num14 < result.SizeX * result.SizeY; num14++)
				{
					byte b = reader.ReadByte();
					if (b == 1)
					{
						result.Parent[num14] = new Parent
						{
							X = reader.ReadInt16(),
							Y = reader.ReadInt16()
						};
					}
					else
					{
						result.Parent[num14] = null;
					}
				}
				if (num >= 5)
				{
					result.AntiPunch = reader.ReadBoolean();
					result.AntiTalk = reader.ReadBoolean();
					result.AntiDrop = reader.ReadBoolean();
				}
				try
				{
					result.CreateDate = new DateTime(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
				}
				catch (ArgumentOutOfRangeException)
				{
					result.CreateDate = DateTime.UtcNow;
				}
				SessionDeserializations++;
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
			return result;
		}

		public static void SerializeChallenge(BinaryWriter writer, ChallengeData data)
		{
			try
			{
				writer.Write(Convert.ToInt32(1));
				if (data.Filename == null)
				{
					writer.Write(string.Empty);
				}
				else
				{
					writer.Write(data.Filename);
				}
				writer.Write(data.DailyTask1.Type);
				writer.Write(data.DailyTask1.Item);
				writer.Write(data.DailyTask2.Type);
				writer.Write(data.DailyTask2.Item);
				writer.Write(data.DailyTask3.Type);
				writer.Write(data.DailyTask3.Item);
				writer.Write(data.DailyTask4.Type);
				writer.Write(data.DailyTask4.Item);
				writer.Write(data.DailyTask5.Type);
				writer.Write(data.DailyTask5.Item);
				writer.Write(data.DailyTask6.Type);
				writer.Write(data.DailyTask6.Item);
				writer.Write(data.DailyTask7.Type);
				writer.Write(data.DailyTask7.Item);
				if (data.Participants == null)
				{
					writer.Write(0);
				}
				else
				{
					writer.Write(data.Participants.Count);
					foreach (ChallengeParticipant participant in data.Participants)
					{
						if (participant.Username == null)
						{
							writer.Write(string.Empty);
						}
						else
						{
							writer.Write(participant.Username);
						}
						writer.Write(participant.DailyPoints1);
						writer.Write(participant.DailyPoints2);
						writer.Write(participant.DailyPoints3);
						writer.Write(participant.DailyPoints4);
						writer.Write(participant.DailyPoints5);
						writer.Write(participant.DailyPoints6);
						writer.Write(participant.DailyPoints7);
						writer.Write(participant.Rewarded);
					}
				}
				ChallengeSerializations++;
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
		}

		public static ChallengeData DeserializeChallenge(BinaryReader reader)
		{
			ChallengeData result = default(ChallengeData);
			try
			{
				int num = reader.ReadInt32();
				result.Filename = reader.ReadString();
				result.DailyTask1 = new ChallengeTask
				{
					Type = reader.ReadInt32(),
					Item = reader.ReadInt32()
				};
				result.DailyTask2 = new ChallengeTask
				{
					Type = reader.ReadInt32(),
					Item = reader.ReadInt32()
				};
				result.DailyTask3 = new ChallengeTask
				{
					Type = reader.ReadInt32(),
					Item = reader.ReadInt32()
				};
				result.DailyTask4 = new ChallengeTask
				{
					Type = reader.ReadInt32(),
					Item = reader.ReadInt32()
				};
				result.DailyTask5 = new ChallengeTask
				{
					Type = reader.ReadInt32(),
					Item = reader.ReadInt32()
				};
				result.DailyTask6 = new ChallengeTask
				{
					Type = reader.ReadInt32(),
					Item = reader.ReadInt32()
				};
				result.DailyTask7 = new ChallengeTask
				{
					Type = reader.ReadInt32(),
					Item = reader.ReadInt32()
				};
				int num2 = reader.ReadInt32();
				result.Participants = new List<ChallengeParticipant>();
				for (int i = 0; i < num2; i++)
				{
					result.Participants.Add(new ChallengeParticipant
					{
						Username = reader.ReadString(),
						DailyPoints1 = reader.ReadInt32(),
						DailyPoints2 = reader.ReadInt32(),
						DailyPoints3 = reader.ReadInt32(),
						DailyPoints4 = reader.ReadInt32(),
						DailyPoints5 = reader.ReadInt32(),
						DailyPoints6 = reader.ReadInt32(),
						DailyPoints7 = reader.ReadInt32(),
						Rewarded = reader.ReadBoolean()
					});
				}
				ChallengeDeserializations++;
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
			return result;
		}
	}
}
