using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Breaworlds.Server
{
	public class Session
	{
		public bool Active;

		public SessionDataHandle Data;

		public string Door;

		public int Created;

		public int Teleport;

		public int LastSeedX;

		public int LastSeedY;

		public bool Checkpoint;

		public int CheckpointX;

		public int CheckpointY;

		public bool ForceOwner = false;

		public bool ForceAdmin = false;

		public int LockCapacity;

		public Session()
		{
			Active = false;
			Data = DefaultData(binded: true);
			Checkpoint = false;
			CheckpointX = 0;
			CheckpointY = 0;
		}

		public static SessionDataHandle DefaultData(bool binded)
		{
			SessionDataHandle sessionDataHandle = new SessionDataHandle(binded);
			sessionDataHandle.Filename = "";
			sessionDataHandle.Theme = 0;
			sessionDataHandle.Banned = 0;
			sessionDataHandle.Name = "";
			sessionDataHandle.Owner = "";
			sessionDataHandle.Drop = new List<DroppedItem>();
			sessionDataHandle.Bans = new List<BanData>();
			sessionDataHandle.Admin = new List<string>();
			sessionDataHandle.Background = new ushort[5000];
			sessionDataHandle.Foreground = new ushort[5000];
			sessionDataHandle.Special = new object[5000];
			sessionDataHandle.Parent = new object[5000];
			sessionDataHandle.SizeX = 100;
			sessionDataHandle.SizeY = 50;
			sessionDataHandle.AntiPunch = false;
			sessionDataHandle.AntiTalk = false;
			sessionDataHandle.AntiDrop = false;
			sessionDataHandle.CreateDate = DateTime.UtcNow;
			return sessionDataHandle;
		}

		public T GetSpecialData<T>(int index)
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Special[index] is T result)
			{
				return result;
			}
			return (T)Activator.CreateInstance(typeof(T));
		}

		public LockData GetLockData(int index)
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Special[index] is LockData result)
			{
				return result;
			}
			return default(LockData);
		}

		public SeedData GetSeedData(int index)
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Special[index] is SeedData result)
			{
				return result;
			}
			return default(SeedData);
		}

		public SignData GetSignData(int index)
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Special[index] is SignData result)
			{
				return result;
			}
			return default(SignData);
		}

		public PortalData GetPortalData(int index)
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Special[index] is PortalData result)
			{
				return result;
			}
			return default(PortalData);
		}

		public DoorData GetDoorData(int index)
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Special[index] is DoorData result)
			{
				return result;
			}
			return default(DoorData);
		}

		public EntranceData GetEntranceData(int index)
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Special[index] is EntranceData result)
			{
				return result;
			}
			return default(EntranceData);
		}

		public FurnaceData GetFurnaceData(int index)
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Special[index] is FurnaceData result)
			{
				return result;
			}
			return default(FurnaceData);
		}

		public MailBoxData GetMailBoxData(int index)
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Special[index] is MailBoxData result)
			{
				return result;
			}
			return default(MailBoxData);
		}

		public BulletinData GetBulletinData(int index)
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Special[index] is BulletinData result)
			{
				return result;
			}
			return default(BulletinData);
		}

		public VendingData GetVendingData(int index)
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Special[index] is VendingData result)
			{
				return result;
			}
			return default(VendingData);
		}

		public LampData GetLampData(int index)
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Special[index] is LampData result)
			{
				return result;
			}
			return default(LampData);
		}

		public TrafficLightData GetTrafficLightData(int index)
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Special[index] is TrafficLightData result)
			{
				return result;
			}
			return default(TrafficLightData);
		}

		public SignalSenderData GetSignalSenderData(int index)
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Special[index] is SignalSenderData result)
			{
				return result;
			}
			return default(SignalSenderData);
		}

		public MusicBlockData GetMusicBlockData(int index)
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Special[index] is MusicBlockData result)
			{
				return result;
			}
			return default(MusicBlockData);
		}

		public DisplayBoxData GetDisplayBoxData(int index)
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Special[index] is DisplayBoxData result)
			{
				return result;
			}
			return default(DisplayBoxData);
		}

		public SmokehouseData GetSmokehouseData(int index)
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Special[index] is SmokehouseData result)
			{
				return result;
			}
			return default(SmokehouseData);
		}

		public ProviderData GetProviderData(int index)
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Special[index] is ProviderData result)
			{
				return result;
			}
			return default(ProviderData);
		}

		public GameJoinData GetGameJoinData(int index)
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Special[index] is GameJoinData result)
			{
				return result;
			}
			return default(GameJoinData);
		}

		public GameSpawnData GetGameSpawnData(int index)
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Special[index] is GameSpawnData result)
			{
				return result;
			}
			return default(GameSpawnData);
		}

		public GameFinishData GetGameFinishData(int index)
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Special[index] is GameFinishData result)
			{
				return result;
			}
			return default(GameFinishData);
		}

		public ChestData GetChestData(int index)
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Special[index] is ChestData result)
			{
				return result;
			}
			return default(ChestData);
		}

		public HalloweenEnemyData GetHalloweenEnemyData(int index)
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Special[index] is HalloweenEnemyData result)
			{
				return result;
			}
			return default(HalloweenEnemyData);
		}

		public void Generate(SessionDataHandle data, int template)
		{
			if (template == 1)
			{
				data.SizeX = 100;
				data.SizeY = 50;
				data.Theme = 4;
				int num = 20;
				int num2 = Server.Random.Next(100);
				int num3 = 0;
				for (int i = 0; i < 100; i++)
				{
					int num4 = Server.Random.Next(3);
					if (num4 == 0 && num3 == 2)
					{
						num3 = 0;
					}
					else
					{
						num++;
						num3 = 1;
					}
					if (num4 == 1 && num3 == 1)
					{
						num3 = 0;
					}
					else
					{
						num--;
						num3 = 2;
					}
					if (num > 25)
					{
						num = 25;
					}
					if (num < 10)
					{
						num = 15;
					}
					if (num > 23)
					{
						for (int j = 23; j < num; j++)
						{
							data.Foreground[i + j * 100] = 181;
						}
					}
					for (int k = num; k < 45; k++)
					{
						data.Foreground[i + k * 100] = 153;
						data.Background[i + k * 100] = 15;
						if (k >= 40 && Server.Random.Next(5) == 0)
						{
							data.Foreground[i + k * 100] = 11;
						}
						if (k >= 25 && Server.Random.Next(500) == 0)
						{
							data.Foreground[i + k * 100] = 899;
							if (Event.Type == EventType.Summer)
							{
								RewardItem reward = Event.GetReward2();
								data.Drop.Add(new DroppedItem
								{
									X = Convert.ToUInt16(i * 32 + 6),
									Y = Convert.ToUInt16(k * 32 + 6),
									Index = reward.Index,
									Count = reward.Count
								});
							}
						}
					}
					if (num2 == i)
					{
						data.Foreground[i + (num - 1) * 100] = 7;
						data.Foreground[i + num * 100] = 5;
					}
				}
				for (int l = 4500; l < 5000; l++)
				{
					data.Foreground[l] = 5;
				}
			}
			else
			{
				data.SizeX = 100;
				data.SizeY = 50;
				for (int m = 0; m < 2000; m++)
				{
					data.Foreground[m] = 0;
					data.Background[m] = 0;
				}
				for (int n = 2000; n < 4500; n++)
				{
					data.Foreground[n] = 9;
					data.Background[n] = 15;
					if (n >= 2100 && Server.Random.Next(10) == 0)
					{
						data.Foreground[n] = 13;
					}
					if (n >= 3500 && Server.Random.Next(10) == 0)
					{
						data.Foreground[n] = 727;
					}
					if (n >= 4000 && Server.Random.Next(5) == 0)
					{
						data.Foreground[n] = 11;
					}
				}
				for (int num5 = 4500; num5 < 5000; num5++)
				{
					data.Foreground[num5] = 5;
				}
				int num6 = Server.Random.Next(100);
				data.Foreground[1900 + num6] = 7;
				data.Foreground[2000 + num6] = 5;
			}
			data.CreateDate = DateTime.UtcNow;
		}

		public bool Template(string name, int template)
		{
			if (!Database.SessionExists(name) && TryWarp(name, null, unban: false) == 0)
			{
				SessionDataHandle sessionDataHandle = DefaultData(binded: false);
				Generate(sessionDataHandle, template);
				sessionDataHandle.Name = name.ToUpper();
				sessionDataHandle.Filename = sessionDataHandle.Name;
				Database.SessionSave(sessionDataHandle);
				Database.SessionFlush(name);
				Created++;
				return true;
			}
			return false;
		}

		public void Blast(int index)
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (index == 611)
			{
				for (int i = 0; i < Data.SizeX * Data.SizeY; i++)
				{
					if (Item.Default(Data.Background[i]))
					{
						Data.Background[i] = 0;
					}
					if (Item.Default(Data.Foreground[i]))
					{
						Data.Foreground[i] = 0;
					}
				}
			}
			Database.SessionSave(Data);
		}

		public bool CanWorldBan()
		{
			Database.SessionLoad(ref Data, Data.Name);
			TimeSpan timeSpan = DateTime.UtcNow.Subtract(Server.Date);
			BanData[] array = Data.Bans.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				BanData item = array[i];
				if ((double)item.Time < timeSpan.TotalSeconds)
				{
					Data.Bans.Remove(item);
				}
			}
			Database.SessionSave(Data);
			if (Data.Bans.Count > 32)
			{
				return false;
			}
			return true;
		}

		public bool CanDrop()
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Drop.Count > 512)
			{
				return false;
			}
			return true;
		}

		public int TryWarp(string name, Profile profile, bool unban)
		{
			if (!Text.IsAllowed(name))
			{
				return 1;
			}
			if (Text.IsSwear(name))
			{
				return 2;
			}
			if (!Text.Length(name, 1, 32))
			{
				return 3;
			}
			if (name.ToUpper() == "CON")
			{
				return 4;
			}
			if (name.ToUpper() == "PRN")
			{
				return 4;
			}
			if (name.ToUpper() == "AUX")
			{
				return 4;
			}
			if (name.ToUpper() == "NUL")
			{
				return 4;
			}
			if (name.ToUpper() == "COM1")
			{
				return 4;
			}
			if (name.ToUpper() == "COM2")
			{
				return 4;
			}
			if (name.ToUpper() == "COM3")
			{
				return 4;
			}
			if (name.ToUpper() == "COM4")
			{
				return 4;
			}
			if (name.ToUpper() == "COM5")
			{
				return 4;
			}
			if (name.ToUpper() == "COM6")
			{
				return 4;
			}
			if (name.ToUpper() == "COM7")
			{
				return 4;
			}
			if (name.ToUpper() == "COM8")
			{
				return 4;
			}
			if (name.ToUpper() == "COM9")
			{
				return 4;
			}
			if (name.ToUpper() == "COM0")
			{
				return 4;
			}
			if (name.ToUpper() == "LPT1")
			{
				return 4;
			}
			if (name.ToUpper() == "LPT2")
			{
				return 4;
			}
			if (name.ToUpper() == "LPT3")
			{
				return 4;
			}
			if (name.ToUpper() == "LPT4")
			{
				return 4;
			}
			if (name.ToUpper() == "LPT5")
			{
				return 4;
			}
			if (name.ToUpper() == "LPT6")
			{
				return 4;
			}
			if (name.ToUpper() == "LPT7")
			{
				return 4;
			}
			if (name.ToUpper() == "LPT8")
			{
				return 4;
			}
			if (name.ToUpper() == "LPT9")
			{
				return 4;
			}
			if (name.ToUpper() == "LPT0")
			{
				return 4;
			}
			if (profile != null)
			{
				if (!unban && !Rewards.Permission(profile.Data.Filename, Permissions.Guard) && GetBan(name) > 0)
				{
					return 5;
				}
				if (!unban && !Rewards.Permission(profile.Data.Filename, Permissions.Mod) && GetWorldBan(name, profile) > 0)
				{
					return 6;
				}
			}
			if (Players(name) >= 50)
			{
				return 7;
			}
			if (!Database.SessionExists(name) && Created >= 5)
			{
				return 8;
			}
			return 0;
		}

		public void Warp(string name)
		{
			if (!Database.SessionExists(name))
			{
				Active = false;
				Data = DefaultData(binded: true);
				Checkpoint = false;
				CheckpointX = 0;
				CheckpointY = 0;
				Generate(Data, 0);
				Data.Name = name.ToUpper();
				Data.Filename = Data.Name;
				Database.SessionSave(Data);
				Created++;
			}
			else
			{
				Database.SessionLoad(ref Data, name);
			}
			Active = true;
		}

		public void Leave()
		{
			int num = 0;
			Player[] array = Server.Online.ToArray();
			foreach (Player player in array)
			{
				if (player.Active && player.Profile.Active && player.Session.Active && player.Session.Data.Name == Data.Name)
				{
					num++;
				}
			}
			if (num <= 1)
			{
				Database.SessionClose(Data.Filename, binded: true);
			}
			Active = false;
			Data = DefaultData(binded: true);
			Checkpoint = false;
			CheckpointX = 0;
			CheckpointY = 0;
		}

		public int GetTileID(int tile)
		{
			for (int i = 0; i < Data.Foreground.Length; i++)
			{
				if (Data.Foreground[i] == tile)
				{
					return i;
				}
			}
			return -1;
		}

		public int GetDoor(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return GetSpawn();
			}
			for (int i = 0; i < Data.Foreground.Length; i++)
			{
				if ((Data.Foreground[i] == 71 || Data.Foreground[i] == 203 || Data.Foreground[i] == 511) && GetDoorData(i).Name == name)
				{
					return i;
				}
			}
			return GetSpawn();
		}

		public int GetSpawn()
		{
			Database.SessionLoad(ref Data, Data.Name);
			for (int i = 0; i < Data.Foreground.Length; i++)
			{
				if (Data.Foreground[i] == 7)
				{
					return i;
				}
			}
			return 0;
		}

		public int GetTileX(int index)
		{
			return index % Data.SizeX;
		}

		public int GetTileY(int index)
		{
			return index / Data.SizeX;
		}

		public int GetGameSpawn(int game)
		{
			for (int i = 0; i < Data.Foreground.Length; i++)
			{
				if (Data.Foreground[i] == 599 && GetGameSpawnData(i).Game == game)
				{
					return i;
				}
			}
			return GetSpawn();
		}

		public bool ValidateBuilding(int x1, int y1, int x2, int y2, Profile profile)
		{
			if (x2 < 0 || x2 > Data.SizeX)
			{
				return false;
			}
			if (y2 < 0 || y2 > Data.SizeY)
			{
				return false;
			}
			int x3 = (x1 + 8) / 32;
			int y3 = (y1 + 16) / 32;
			int centerX = (x1 + 8) / 32;
			int centerY = (y1 + 16) / 32;
			bool[] matrix = new bool[Data.Foreground.Length];
			ValidateBuildingFill(ref matrix, x3, y3, 0, centerX, centerY, profile);
			int num = x2 + y2 * Data.SizeX;
			if (matrix[num])
			{
				return true;
			}
			return false;
		}

		public void ValidateBuildingFill(ref bool[] matrix, int x, int y, int side, int centerX, int centerY, Profile profile)
		{
			bool flag = x < centerX - 5 || x > centerX + 5;
			bool flag2 = y < centerY - 5 || y > centerY + 5;
			if (!(flag || flag2) && x >= 0 && x < Data.SizeX && y >= 0 && y < Data.SizeY)
			{
				int num = x + y * Data.SizeX;
				if (!matrix[num] && !Item.Solid(Data.Foreground[num]) && (Item.Collision(Data.Foreground[num]) != side || side == 0) && ((Data.Foreground[num] != 195 && Data.Foreground[num] != 205) || !GetEntranceData(num).Closed || TileAccess(profile.Data.Filename, num)))
				{
					matrix[num] = true;
					ValidateBuildingFill(ref matrix, x, y + 1, 1, centerX, centerY, profile);
					ValidateBuildingFill(ref matrix, x + 1, y, 2, centerX, centerY, profile);
					ValidateBuildingFill(ref matrix, x - 1, y, 3, centerX, centerY, profile);
					ValidateBuildingFill(ref matrix, x, y - 1, 4, centerX, centerY, profile);
				}
			}
		}

		public bool ValidateMovement(int x1, int y1, int x2, int y2, Profile profile)
		{
			if (Math.Abs(x1 - x2) > 64)
			{
				return false;
			}
			if (Math.Abs(y1 - y2) > 64)
			{
				return false;
			}
			if (x2 < 0 || x2 > Data.SizeX * 32 - 16)
			{
				return false;
			}
			if (y2 < 0 || y2 > Data.SizeY * 32 - 32)
			{
				return false;
			}
			if (profile.Noclip)
			{
				return true;
			}
			int centerX = (x1 + 8) / 32;
			int centerY = (y1 + 16) / 32;
			bool[] matrix = new bool[Data.Foreground.Length];
			for (int i = 1; i <= 4; i++)
			{
				int num = 0;
				int num2 = 0;
				if (i == 1)
				{
					num = x1 + 1;
					num2 = y1 + 1;
				}
				if (i == 2)
				{
					num = x1 + 16 - 1;
					num2 = y1 + 1;
				}
				if (i == 3)
				{
					num = x1 + 1;
					num2 = y1 + 32 - 1;
				}
				if (i == 4)
				{
					num = x1 + 16 - 1;
					num2 = y1 + 32 - 1;
				}
				int x3 = num / 32;
				int y3 = num2 / 32;
				ValidateMovementFill(ref matrix, x3, y3, 0, centerX, centerY, profile);
			}
			for (int j = 1; j <= 4; j++)
			{
				int num3 = 0;
				int num4 = 0;
				if (j == 1)
				{
					num3 = x2 + 1;
				}
				if (j == 2)
				{
					num3 = x2 + 16 - 1;
				}
				if (j == 3)
				{
					num3 = x2 + 1;
				}
				if (j == 4)
				{
					num3 = x2 + 16 - 1;
				}
				if (j == 1)
				{
					num4 = y2 + 1;
				}
				if (j == 2)
				{
					num4 = y2 + 1;
				}
				if (j == 3)
				{
					num4 = y2 + 32 - 1;
				}
				if (j == 4)
				{
					num4 = y2 + 32 - 1;
				}
				if (j == 1)
				{
					num3 = x2 + 1;
					num4 = y2 + 1;
				}
				if (j == 2)
				{
					num3 = x2 + 16 - 1;
					num4 = y2 + 1;
				}
				if (j == 3)
				{
					num3 = x2 + 1;
					num4 = y2 + 32 - 1;
				}
				if (j == 4)
				{
					num3 = x2 + 16 - 1;
					num4 = y2 + 32 - 1;
				}
				int num5 = num3 / 32;
				int num6 = num4 / 32;
				int num7 = num6 * Data.SizeX + num5;
				if (!matrix[num7])
				{
					return false;
				}
			}
			return true;
		}

		public void ValidateMovementFill(ref bool[] matrix, int x, int y, int side, int centerX, int centerY, Profile profile)
		{
			bool flag = x < centerX - 2 || x > centerX + 2;
			bool flag2 = y < centerY - 2 || y > centerY + 2;
			if (!(flag || flag2) && x >= 0 && x < Data.SizeX && y >= 0 && y < Data.SizeY)
			{
				int num = x + y * Data.SizeX;
				if (!matrix[num] && !Item.Solid(Data.Foreground[num]) && (Item.Collision(Data.Foreground[num]) != side || side == 0) && ((Data.Foreground[num] != 195 && Data.Foreground[num] != 205) || !GetEntranceData(num).Closed || TileAccess(profile.Data.Filename, num)))
				{
					matrix[num] = true;
					ValidateMovementFill(ref matrix, x, y + 1, 1, centerX, centerY, profile);
					ValidateMovementFill(ref matrix, x + 1, y, 2, centerX, centerY, profile);
					ValidateMovementFill(ref matrix, x - 1, y, 3, centerX, centerY, profile);
					ValidateMovementFill(ref matrix, x, y - 1, 4, centerX, centerY, profile);
				}
			}
		}

		public bool WorldOwner(string username)
		{
			if (ForceOwner)
			{
				return true;
			}
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Owner == username)
			{
				return true;
			}
			return false;
		}

		public bool WorldAdmin(string username)
		{
			if (ForceAdmin)
			{
				return true;
			}
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Owner == username)
			{
				return true;
			}
			if (Data.Admin.Contains(username))
			{
				return true;
			}
			return false;
		}

		public bool WorldLocked()
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Owner != string.Empty)
			{
				return true;
			}
			return false;
		}

		public bool TileOwner(string username, int tile)
		{
			if (ForceOwner)
			{
				return true;
			}
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Parent[tile] is Parent parent)
			{
				int num = parent.X + parent.Y * Data.SizeX;
				if (Data.Foreground[num] == 319 || Data.Foreground[num] == 321 || Data.Foreground[num] == 323)
				{
					LockData lockData = GetLockData(num);
					if (lockData.Owner == null)
					{
						lockData.Owner = "";
					}
					if (lockData.Admin == null)
					{
						lockData.Admin = new ArrayList();
					}
					if (lockData.Owner == username)
					{
						return true;
					}
					return false;
				}
				return false;
			}
			if (Data.Owner == string.Empty)
			{
				return true;
			}
			if (Data.Owner == username)
			{
				return true;
			}
			return false;
		}

		public bool TileAdmin(string username, int tile)
		{
			if (ForceAdmin)
			{
				return true;
			}
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Parent[tile] is Parent parent)
			{
				int num = parent.X + parent.Y * Data.SizeX;
				if (Data.Foreground[num] == 319 || Data.Foreground[num] == 321 || Data.Foreground[num] == 323)
				{
					LockData lockData = GetLockData(num);
					if (lockData.Owner == null)
					{
						lockData.Owner = "";
					}
					if (lockData.Admin == null)
					{
						lockData.Admin = new ArrayList();
					}
					if (lockData.Owner == username)
					{
						return true;
					}
					if (lockData.Admin.Contains(username))
					{
						return true;
					}
					return false;
				}
				return false;
			}
			if (Data.Owner == string.Empty)
			{
				return true;
			}
			if (Data.Owner == username)
			{
				return true;
			}
			if (Data.Admin.Contains(username))
			{
				return true;
			}
			return false;
		}

		public bool TileAccess(string username, int tile)
		{
			if (ForceAdmin)
			{
				return true;
			}
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Parent[tile] is Parent parent)
			{
				int num = parent.X + parent.Y * Data.SizeX;
				if (Data.Foreground[num] == 319 || Data.Foreground[num] == 321 || Data.Foreground[num] == 323)
				{
					LockData lockData = GetLockData(num);
					if (lockData.Owner == null)
					{
						lockData.Owner = "";
					}
					if (lockData.Admin == null)
					{
						lockData.Admin = new ArrayList();
					}
					if (lockData.Public)
					{
						return true;
					}
					if (lockData.Owner == username)
					{
						return true;
					}
					if (lockData.Admin.Contains(username))
					{
						return true;
					}
					return false;
				}
				return false;
			}
			if (Data.Public)
			{
				return true;
			}
			if (Data.Owner == string.Empty)
			{
				return true;
			}
			if (Data.Owner == username)
			{
				return true;
			}
			if (Data.Admin.Contains(username))
			{
				return true;
			}
			return false;
		}

		public bool TileLocked(int tile)
		{
			Database.SessionLoad(ref Data, Data.Name);
			if (Data.Parent[tile] is Parent)
			{
				return true;
			}
			if (Data.Owner != string.Empty)
			{
				return true;
			}
			return false;
		}

		public void LockNearest(Parent parent, int x, int y)
		{
			if (x >= 0 && x < Data.SizeX && y >= 0 && y < Data.SizeY && LockCapacity > 0)
			{
				int num = x + y * Data.SizeX;
				if (Data.Parent[num] == null && !parent.Equals(Data.Parent[num]) && (Data.Background[num] != 0 || Data.Foreground[num] != 0) && Item.Lockable(Data.Foreground[num]))
				{
					Data.Parent[num] = parent;
					LockCapacity--;
					LockNearest(parent, x - 1, y);
					LockNearest(parent, x, y - 1);
					LockNearest(parent, x + 1, y);
					LockNearest(parent, x, y + 1);
				}
			}
		}

		public void LockRectangle(Parent parent, int x, int y, int size)
		{
			if (x >= 0 && x < Data.SizeX && y >= 0 && y < Data.SizeY && !((double)x < (double)parent.X - Math.Sqrt(size) / 2.0) && !((double)x > (double)parent.X + Math.Sqrt(size) / 2.0) && !((double)y < (double)parent.Y - Math.Sqrt(size) / 2.0) && !((double)y > (double)parent.Y + Math.Sqrt(size) / 2.0) && LockCapacity > 0)
			{
				int num = x + y * Data.SizeX;
				if (Data.Parent[num] == null && !parent.Equals(Data.Parent[num]) && Item.Lockable(Data.Foreground[num]))
				{
					Data.Parent[num] = parent;
					LockCapacity--;
					LockRectangle(parent, x - 1, y, size);
					LockRectangle(parent, x, y - 1, size);
					LockRectangle(parent, x + 1, y, size);
					LockRectangle(parent, x, y + 1, size);
				}
			}
		}

		public void UnlockArea(Parent parent)
		{
			for (int i = 0; i < Data.Parent.Length; i++)
			{
				if (Data.Parent[i] != null && ((Parent)Data.Parent[i]).X == parent.X && ((Parent)Data.Parent[i]).Y == parent.Y)
				{
					Data.Parent[i] = null;
				}
			}
		}

		public bool OtherLocks(string username)
		{
			Database.SessionLoad(ref Data, Data.Name);
			for (int i = 0; i < Data.Special.Length; i++)
			{
				if (Data.Special[i] is LockData lockData && true && lockData.Owner != username)
				{
					return true;
				}
			}
			return false;
		}

		public int MusicSheetHeight(int tile)
		{
			if (Data.Foreground[tile] == 481)
			{
				int i;
				for (i = 1; Data.Foreground[tile + i * 100] == 481; i++)
				{
				}
				return i;
			}
			return 0;
		}

		public bool HasAntiPunch()
		{
			if (WorldLocked())
			{
				return Data.AntiPunch;
			}
			return false;
		}

		public bool HasAntiTalk(Player player)
		{
			if (Rewards.Permission(player.Profile.Data.Filename, Permissions.Mod))
			{
				return false;
			}
			if (WorldLocked())
			{
				if (WorldAdmin(player.Profile.Data.Filename))
				{
					return false;
				}
				return Data.AntiTalk;
			}
			return false;
		}

		public bool HasAntiDrop(Player player)
		{
			if (Rewards.Permission(player.Profile.Data.Filename, Permissions.Mod))
			{
				return false;
			}
			if (WorldLocked())
			{
				if (WorldAdmin(player.Profile.Data.Filename))
				{
					return false;
				}
				return Data.AntiDrop;
			}
			return false;
		}

		public void Drop(BinaryWriter writer, int index, int count, int x, int y)
		{
			Database.SessionLoad(ref Data, Data.Name);
			DroppedItem droppedItem = default(DroppedItem);
			droppedItem.X = (ushort)x;
			droppedItem.Y = (ushort)y;
			droppedItem.Index = (ushort)index;
			droppedItem.Count = (ushort)count;
			DroppedItem item = droppedItem;
			writer.Write(Convert.ToBoolean(0));
			writer.Write(Convert.ToUInt16(index));
			writer.Write(Convert.ToUInt16(count));
			writer.Write(Convert.ToUInt16(x));
			writer.Write(Convert.ToUInt16(y));
			Data.Drop.Add(item);
			Database.SessionSave(Data);
		}

		public bool Collect(BinaryWriter writer, int index, int count, int x, int y)
		{
			Database.SessionLoad(ref Data, Data.Name);
			DroppedItem droppedItem = default(DroppedItem);
			droppedItem.X = (ushort)x;
			droppedItem.Y = (ushort)y;
			droppedItem.Index = (ushort)index;
			droppedItem.Count = (ushort)count;
			DroppedItem item = droppedItem;
			if (Data.Drop.Contains(item))
			{
				writer.Write(Convert.ToBoolean(1));
				writer.Write(Convert.ToUInt16(index));
				writer.Write(Convert.ToUInt16(count));
				writer.Write(Convert.ToUInt16(x));
				writer.Write(Convert.ToUInt16(y));
				Data.Drop.Remove(item);
				Database.SessionSave(Data);
				return true;
			}
			return false;
		}

		public void WriteTiles(BinaryWriter writer, string username)
		{
			Database.SessionLoad(ref Data, Data.Name);
			writer.Write(Convert.ToUInt16(Data.SizeX));
			writer.Write(Convert.ToUInt16(Data.SizeY));
			for (int i = 0; i < Data.SizeX; i++)
			{
				for (int j = 0; j < Data.SizeY; j++)
				{
					int num = i + j * Data.SizeX;
					writer.Write(Convert.ToUInt16(Data.Background[num]));
					writer.Write(Convert.ToUInt16(Data.Foreground[num]));
					if (Data.Foreground[num] == 195 || Data.Foreground[num] == 205)
					{
						if (TileAccess(username, num))
						{
							writer.Write(Convert.ToUInt16(0));
						}
						else
						{
							writer.Write(Convert.ToUInt16(GetEntranceData(num).Closed));
						}
					}
					else if (Data.Foreground[num] == 419)
					{
						writer.Write(Convert.ToUInt16(GetTrafficLightData(num).Light));
					}
					else if (Data.Foreground[num] == 253 || Data.Foreground[num] == 443 || Data.Foreground[num] == 1087 || Data.Foreground[num] == 1089 || Data.Foreground[num] == 1091 || Data.Foreground[num] == 1093)
					{
						writer.Write(Convert.ToUInt16(GetLampData(num).On));
					}
					else if (Data.Foreground[num] == 485 || Data.Foreground[num] == 487 || Data.Foreground[num] == 489)
					{
						writer.Write(Convert.ToUInt16(GetMusicBlockData(num).Sound));
					}
					else if (Data.Foreground[num] == 535)
					{
						writer.Write(Convert.ToUInt16(GetDisplayBoxData(num).Index));
					}
					else if (Data.Foreground[num] == 597)
					{
						writer.Write(Convert.ToUInt16(GetGameJoinData(num).Color));
					}
					else if (Data.Foreground[num] == 599)
					{
						writer.Write(Convert.ToUInt16(GetGameSpawnData(num).Color));
					}
					else if (Data.Foreground[num] == 601)
					{
						writer.Write(Convert.ToUInt16(GetGameFinishData(num).Color));
					}
					else if (Data.Foreground[num] == 899)
					{
						writer.Write(Convert.ToUInt16(GetChestData(num).Open));
					}
					else if (Data.Foreground[num] == 1143)
					{
						writer.Write(Convert.ToUInt16(GetHalloweenEnemyData(num).Image));
					}
					else
					{
						writer.Write(Convert.ToUInt16(0));
					}
					if (Data.Parent[num] == null)
					{
						writer.Write(Convert.ToUInt16(0));
						continue;
					}
					Parent parent = (Parent)Data.Parent[num];
					int num2 = parent.X + parent.Y * Data.SizeX;
					if (Data.Foreground[num2] == 319 || Data.Foreground[num2] == 321 || Data.Foreground[num2] == 323)
					{
						LockData lockData = GetLockData(num2);
						if (lockData.Owner == null)
						{
							lockData.Owner = "";
						}
						if (lockData.Admin == null)
						{
							lockData.Admin = new ArrayList();
						}
						if (lockData.Owner == username || lockData.Admin.Contains(username))
						{
							writer.Write(Convert.ToUInt16(Data.SizeX * Data.SizeY * 2 + num2));
							continue;
						}
						if (lockData.Public)
						{
							writer.Write(Convert.ToUInt16(Data.SizeX * Data.SizeY + num2));
							continue;
						}
						_ = Data.SizeX;
						_ = Data.SizeY;
						writer.Write(Convert.ToUInt16(0 + num2));
					}
					else
					{
						writer.Write(Convert.ToUInt16(0));
					}
				}
			}
		}

		public void WriteMusic(BinaryWriter writer)
		{
			List<ushort> list = new List<ushort>(Data.Foreground);
			if (list.Contains(481) && list.Contains(483))
			{
				writer.Write(Convert.ToBoolean(value: true));
				writer.Write(Convert.ToUInt16(MusicSheetHeight(list.IndexOf(481))));
				writer.Write(Convert.ToUInt16(GetTileX(list.IndexOf(481))));
				writer.Write(Convert.ToUInt16(GetTileY(list.IndexOf(481))));
				writer.Write(Convert.ToUInt16(GetTileX(list.IndexOf(483))));
				writer.Write(Convert.ToUInt16(GetTileY(list.IndexOf(483))));
			}
			else
			{
				writer.Write(Convert.ToBoolean(value: false));
			}
		}

		public static int GetBan(string name)
		{
			if (Database.SessionExists(name.ToUpper()))
			{
				SessionDataHandle data = new SessionDataHandle(binded: false);
				Database.SessionLoad(ref data, name.ToUpper());
				Database.SessionClose(name.ToUpper(), binded: false);
				return data.Banned;
			}
			return 0;
		}

		public static int GetWorldBan(string name, Profile profile)
		{
			if (Database.SessionExists(name.ToUpper()))
			{
				SessionDataHandle data = new SessionDataHandle(binded: false);
				Database.SessionLoad(ref data, name.ToUpper());
				Database.SessionClose(name.ToUpper(), binded: false);
				TimeSpan timeSpan = DateTime.UtcNow.Subtract(Server.Date);
				BanData[] array = data.Bans.ToArray();
				for (int i = 0; i < array.Length; i++)
				{
					BanData item = array[i];
					if ((double)item.Time > timeSpan.TotalSeconds)
					{
						if (item.Name == profile.Data.Filename)
						{
							return (int)Math.Floor((double)item.Time - timeSpan.TotalSeconds);
						}
					}
					else
					{
						data.Bans.Remove(item);
					}
				}
				return 0;
			}
			return 0;
		}

		public static int Players(string name)
		{
			int num = 0;
			Player[] array = Server.Online.ToArray();
			foreach (Player player in array)
			{
				if (player.Active && player.Profile.Active && player.Session.Active && player.Profile.Visible && !(player.Session.Data.Name != name))
				{
					num++;
				}
			}
			return num;
		}

		public static string Random(Player player)
		{
			string[] array = Database.SessionCache.Keys.ToArray();
			for (int i = 0; i < Math.Min(8, array.Length); i++)
			{
				string text = array[Server.Random.Next(array.Length)];
				if ((!player.Session.Active || !(player.Session.Data.Name == text)) && player.Session.TryWarp(text, player.Profile, unban: false) == 0)
				{
					return text;
				}
			}
			return "BREAWORLDS";
		}
	}
}
