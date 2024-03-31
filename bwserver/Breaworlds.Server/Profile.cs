using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Breaworlds.Server
{
	public class Profile
	{
		public bool Active;

		public ProfileDataHandle Data;

		public int AdsWatched = 0;

		public int PlatformType = 0;

		public int PlatformVersion = 0;

		public int FailedLoginAttempts = 0;

		public string Reply = string.Empty;

		public string Device = string.Empty;

		public string StaffVerifiedDevice = "none";

		public string Address = string.Empty;

		public string Country = string.Empty;

		public string Version = string.Empty;

		public bool Visible = true;

		public bool Noclip = false;

		public bool Frozen = false;

		public bool Mailed = false;

		public int Strength = 0;

		public int Stack = 500;

		public int Effect = 0;

		public int Slot = 1;

		public bool Bannable = true;

		public bool Pullable = true;

		public bool Killable = true;

		public bool HiddenStaff = false;

		public Profile()
		{
			Reset();
		}

		public void Reset()
		{
			if (Data != null && !string.IsNullOrEmpty(Data.Filename))
			{
				Database.ProfileClose(Data.Filename, binded: false);
			}
			Data = new ProfileDataHandle(binded: false)
			{
				Filename = "",
				FakeLevel = 5,
				Experience = 0,
				Gems = 0,
				MoonRocks = 0,
				Level = 1,
				MuteDuration = 0,
				BanDuration = 0,
				Warnings = 0,
				Bans = 0,
				RewardCooldown = 0,
				AllowRespin = false,
				ClearedMoonRocks = false,
				QuestType = 0,
				QuestItem = 0,
				QuestLeft = 0,
				QuestLevel = 0,
				Device = "",
				StaffVerifiedDevice = "none",
				Address = "",
				Session = "TUTORIAL",
				Username = "",
				Password = "",
				Mailname = "",
				Worlds = new List<string>(0),
				Friends = new List<string>(0),
				Purchases = new List<string>(0),
				ItemSlots = 30,
				ItemIndex = new List<ushort>(),
				ItemCount = new List<ushort>(),
				ItemEquip = new List<ushort>(),
				Options = new Options
				{
					Volume = 100,
					Sounds = true,
					Shadow = true,
					Smooth = true,
					Full = false
				},
				FriendOffline = false,
				FriendUnknown = false,
				Achievements = new int[Achievements.List.Count],
				SkinA = 100,
				SkinR = 173,
				SkinG = 138,
				SkinB = 96,
				Gender = 0,
				RegisterDate = default(DateTime),
				LoginDate = default(DateTime)
			};
			AdsWatched = 0;
			Visible = true;
			Noclip = false;
			Frozen = false;
			Active = false;
		}

		public void Generate()
		{
			string text = "QWERTYUIOPASDFGHJKLZXCVBNM0123456789";
			char[] array = new char[6];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = text[Server.Random.Next(text.Length)];
			}
			Data.Password = new string(array);
		}

		public int Login(string password)
		{
			if (Data.Password.ToUpper() != password.ToUpper())
			{
				FailedLoginAttempts++;
				if (FailedLoginAttempts > 10)
				{
					Punishment.Blacklist(Address, 3600);
				}
				Active = false;
				return 1;
			}
			if (GetBan() > 0)
			{
				Active = false;
				return 2;
			}
			Data.LoginDate = DateTime.UtcNow;
			Data.Address = Address;
			Data.Device = Device;
			Active = true;
			return 0;
		}

		public int Register(string username, string mailname)
		{
			if (!Text.IsAllowed(username))
			{
				return 1;
			}
			if (!Text.Length(username, 3, 12))
			{
				return 2;
			}
			if (!Text.IsEmail(mailname))
			{
				return 3;
			}
			if (Text.IsSwear(username))
			{
				return 4;
			}
			Reset();
			Generate();
			Data.Username = username;
			Data.Mailname = mailname;
			SetItem(1, 1);
			SetItem(3, 1);
			Data.RegisterDate = DateTime.UtcNow;
			Data.Filename = username;
			Database.ProfileSave(Data);
			Server.SendLog(Data.Username, "Has registered");
			return 0;
		}

		public int Recover(string mailname)
		{
			if (mailname.ToUpper() != Data.Mailname.ToUpper())
			{
				return 1;
			}
			if (Mailed)
			{
				return 2;
			}
			Mailed = true;
			Mailer.SendRecoveryEmail(Data.Mailname, Data.Filename, Data.Password);
			return 0;
		}

		public int GetPunchStrength()
		{
			if (Strength == 0)
			{
				if (GetPartItem(7) == 225)
				{
					return 3;
				}
				if (GetPartItem(7) == 285)
				{
					return 3;
				}
				if (GetPartItem(7) == 525)
				{
					return 5;
				}
				if (GetPartItem(7) == 583)
				{
					return 5;
				}
				if (GetPartItem(7) == 1003)
				{
					return 15;
				}
				if (GetPartItem(7) == 1139)
				{
					return 3;
				}
				if (GetPartItem(7) == 1141)
				{
					return -10;
				}
				if (GetPartItem(7) == 1164)
				{
					return -3;
				}
				if (GetPartItem(7) == 1189)
				{
					return -2;
				}
				if (GetPartItem(7) == 1199)
				{
					return -1;
				}
				if (GetPartItem(7) == 1225)
				{
					return -3;
				}
				return 1;
			}
			return Strength;
		}

		public int GetBreakSpeed()
		{
			if (GetPartItem(7) == 145)
			{
				return 2;
			}
			if (GetPartItem(7) == 223)
			{
				return 2;
			}
			if (GetPartItem(7) == 225)
			{
				return 2;
			}
			if (GetPartItem(7) == 285)
			{
				return 2;
			}
			if (GetPartItem(7) == 287)
			{
				return 2;
			}
			if (GetPartItem(7) == 525)
			{
				return 2;
			}
			if (GetPartItem(7) == 583)
			{
				return 2;
			}
			if (GetPartItem(7) == 949)
			{
				return 2;
			}
			if (GetPartItem(7) == 951)
			{
				return 2;
			}
			if (GetPartItem(7) == 953)
			{
				return 2;
			}
			if (GetPartItem(7) == 955)
			{
				return 2;
			}
			if (GetPartItem(7) == 957)
			{
				return 2;
			}
			if (GetPartItem(7) == 959)
			{
				return 2;
			}
			if (GetPartItem(7) == 961)
			{
				return 2;
			}
			if (GetPartItem(7) == 963)
			{
				return 2;
			}
			if (GetPartItem(7) == 975)
			{
				return 2;
			}
			if (GetPartItem(7) == 1003)
			{
				return 2;
			}
			if (GetPartItem(7) == 1139)
			{
				return 2;
			}
			if (GetPartItem(7) == 1141)
			{
				return 2;
			}
			return 1;
		}

		public int GetEffect()
		{
			if (Effect != 0)
			{
				return Effect;
			}
			for (int i = 0; i < Data.ItemIndex.Count; i++)
			{
				int item = Data.ItemIndex[i];
				if (Data.ItemEquip[i] != 0 && Item.Effect(item) != 0)
				{
					return Item.Effect(item);
				}
			}
			return 0;
		}

		public bool VisibleTo(Player player)
		{
			if (Visible)
			{
				return true;
			}
			if (player.Profile == this)
			{
				return true;
			}
			if (Rewards.GetPermission(Data.Filename) <= Rewards.GetPermission(player.Profile.Data.Filename))
			{
				return true;
			}
			return false;
		}

		public void Ban(int seconds)
		{
			TimeSpan timeSpan = DateTime.UtcNow.Subtract(Server.Date);
			Data.BanDuration = Convert.ToInt32(timeSpan.TotalSeconds) + seconds;
			Database.ProfileSave(Data);
		}

		public void Mute(int seconds)
		{
			TimeSpan timeSpan = DateTime.UtcNow.Subtract(Server.Date);
			Data.MuteDuration = Convert.ToInt32(timeSpan.TotalSeconds) + seconds;
			Database.ProfileSave(Data);
		}

		public int GetBan()
		{
			TimeSpan timeSpan = DateTime.UtcNow.Subtract(Server.Date);
			return (int)Math.Floor((double)Data.BanDuration - timeSpan.TotalSeconds);
		}

		public int GetMute()
		{
			TimeSpan timeSpan = DateTime.UtcNow.Subtract(Server.Date);
			return (int)Math.Floor((double)Data.MuteDuration - timeSpan.TotalSeconds);
		}

		public int GetTotalOnline()
		{
			return Data.Online + (int)(DateTime.UtcNow - Data.LoginDate).TotalSeconds;
		}

		public int GetOnline()
		{
			return (int)(DateTime.UtcNow - Data.LoginDate).TotalSeconds;
		}

		public void Equip(int index)
		{
			if (Data.ItemIndex.Contains((ushort)index))
			{
				int index2 = Data.ItemIndex.IndexOf((ushort)index);
				while (GetPartItem(Item.Part(index)) != 0)
				{
					Unequip(GetPartItem(Item.Part(index)));
				}
				Data.ItemEquip[index2] = Item.Part(index);
			}
		}

		public void Unequip(int index)
		{
			if (Data.ItemIndex.Contains((ushort)index))
			{
				int index2 = Data.ItemIndex.IndexOf((ushort)index);
				Data.ItemEquip[index2] = 0;
			}
		}

		public ushort GetItemPart(ushort index)
		{
			if (Data.ItemIndex.Contains(index))
			{
				int index2 = Data.ItemIndex.IndexOf(index);
				return Data.ItemEquip[index2];
			}
			return 0;
		}

		public ushort GetPartItem(ushort part)
		{
			if (Data.ItemEquip.Contains(part))
			{
				int index = Data.ItemEquip.IndexOf(part);
				return Convert.ToUInt16(Data.ItemIndex[index]);
			}
			return 0;
		}

		public bool HasItem(int index, int count)
		{
			return GetItem(index) >= count;
		}

		public void SetItem(int index, int count)
		{
			if (Data.ItemIndex.Contains((ushort)index))
			{
				int index2 = Data.ItemIndex.IndexOf((ushort)index);
				if (count < 1)
				{
					Data.ItemIndex.RemoveAt(index2);
					Data.ItemCount.RemoveAt(index2);
					Data.ItemEquip.RemoveAt(index2);
				}
				else
				{
					Data.ItemIndex[index2] = (ushort)index;
					Data.ItemCount[index2] = (ushort)count;
				}
			}
			else if (count > 0)
			{
				Data.ItemIndex.Add((ushort)index);
				Data.ItemCount.Add((ushort)count);
				Data.ItemEquip.Add(0);
			}
			Database.ProfileSave(Data);
		}

		public ushort GetItem(int index)
		{
			if (Data.ItemIndex.Contains((ushort)index))
			{
				int index2 = Data.ItemIndex.IndexOf((ushort)index);
				return Data.ItemCount[index2];
			}
			return 0;
		}

		public bool CanGetItem(int index, int count)
		{
			if (Data.ItemIndex.Contains((ushort)index))
			{
				int index2 = Data.ItemIndex.IndexOf((ushort)index);
				if (Data.ItemCount[index2] + count <= Stack)
				{
					return true;
				}
				return false;
			}
			if (Data.ItemIndex.Count < Data.ItemSlots && count <= Stack)
			{
				return true;
			}
			return false;
		}

		public bool CanGetReward(RewardItem[] rewards)
		{
			for (int i = 0; i < rewards.Length; i++)
			{
				RewardItem rewardItem = rewards[i];
				if (!CanGetItem(rewardItem.Index, rewardItem.Count))
				{
					return false;
				}
			}
			return true;
		}

		public bool CanGetListing(ShopListing listing)
		{
			ShopItem[] items = listing.Items;
			for (int i = 0; i < items.Length; i++)
			{
				ShopItem shopItem = items[i];
				if (!CanGetItem(shopItem.Index, shopItem.Count))
				{
					return false;
				}
			}
			if (Data.ItemSlots - Data.ItemIndex.Count < listing.Amount)
			{
				return false;
			}
			return true;
		}

		public void WriteOptions(BinaryWriter writer)
		{
			writer.Write(Convert.ToUInt16(Data.Options.Volume));
			writer.Write(Convert.ToBoolean(Data.Options.Sounds));
			writer.Write(Convert.ToBoolean(Data.Options.Shadow));
			writer.Write(value: true);
			writer.Write(Convert.ToBoolean(Data.Options.Full));
			writer.Write(Convert.ToBoolean(value: false));
		}

		public void WriteItems(BinaryWriter writer)
		{
			SlotData[] array = new SlotData[Data.ItemIndex.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new SlotData
				{
					Index = Convert.ToUInt16(Data.ItemIndex[i]),
					Count = Convert.ToUInt16(Data.ItemCount[i]),
					Equip = Convert.ToUInt16(Data.ItemEquip[i])
				};
			}
			for (int j = 0; j < array.Length; j++)
			{
				for (int k = 0; k < array.Length; k++)
				{
					if (Item.Order(array[j].Index) < Item.Order(array[k].Index))
					{
						SlotData slotData = array[j];
						array[j] = array[k];
						array[k] = slotData;
					}
				}
			}
			for (int l = 0; l < array.Length; l++)
			{
				Data.ItemIndex[l] = array[l].Index;
				Data.ItemCount[l] = array[l].Count;
				Data.ItemEquip[l] = array[l].Equip;
			}
			for (int m = 0; m < Data.ItemIndex.Count; m++)
			{
				writer.Write(Convert.ToUInt16((int)Data.ItemIndex[m]));
				writer.Write(Convert.ToUInt16((int)Data.ItemCount[m]));
				writer.Write(Convert.ToUInt16((int)Data.ItemEquip[m]));
			}
			for (int n = Data.ItemIndex.Count; n < Data.ItemSlots; n++)
			{
				writer.Write(Convert.ToUInt16(0));
				writer.Write(Convert.ToUInt16(0));
				writer.Write(Convert.ToUInt16(0));
			}
		}

		public void WriteData(BinaryWriter writer, Player player)
		{
			if (Data.SkinA == 0)
			{
				Data.SkinA = 100;
			}
			if (Data.SkinR == 0)
			{
				Data.SkinR = 173;
			}
			if (Data.SkinG == 0)
			{
				Data.SkinG = 138;
			}
			if (Data.SkinB == 0)
			{
				Data.SkinB = 96;
			}
			if (!Visible)
			{
				writer.Write(Convert.ToUInt16(255));
				writer.Write(Convert.ToUInt16(255));
				writer.Write(Convert.ToUInt16(255));
				writer.Write(Convert.ToUInt16(25));
			}
			else if (GetPartItem(9) == 191)
			{
				writer.Write(Convert.ToUInt16(0));
				writer.Write(Convert.ToUInt16(0));
				writer.Write(Convert.ToUInt16(0));
				if (Noclip)
				{
					writer.Write(Convert.ToUInt16(75));
				}
				else
				{
					writer.Write(Convert.ToUInt16(100));
				}
			}
			else if (GetPartItem(9) == 457)
			{
				writer.Write(Convert.ToUInt16(255));
				writer.Write(Convert.ToUInt16(255));
				writer.Write(Convert.ToUInt16(255));
				if (Noclip)
				{
					writer.Write(Convert.ToUInt16(75));
				}
				else
				{
					writer.Write(Convert.ToUInt16(100));
				}
			}
			else if (GetPartItem(9) == 1037)
			{
				writer.Write(Convert.ToUInt16(Data.SkinR));
				writer.Write(Convert.ToUInt16(Data.SkinG));
				writer.Write(Convert.ToUInt16(Data.SkinB));
				writer.Write(Convert.ToUInt16(0));
			}
			else if (GetPartItem(9) == 1135)
			{
				writer.Write(Convert.ToUInt16(255));
				writer.Write(Convert.ToUInt16(255));
				writer.Write(Convert.ToUInt16(255));
				if (Noclip)
				{
					writer.Write(Convert.ToUInt16(75));
				}
				else
				{
					writer.Write(Convert.ToUInt16(100));
				}
			}
			else if (GetPartItem(9) == 1206)
			{
				writer.Write(Convert.ToUInt16(Data.SkinR));
				writer.Write(Convert.ToUInt16(Data.SkinG));
				writer.Write(Convert.ToUInt16(Data.SkinB));
				writer.Write(Convert.ToUInt16(0));
				if (Noclip)
				{
					writer.Write(Convert.ToUInt16(75));
				}
				else
				{
					writer.Write(Convert.ToUInt16(100));
				}
			}
			else if (GetPartItem(9) == 1205)
			{
				writer.Write(Convert.ToUInt16(Data.SkinR));
				writer.Write(Convert.ToUInt16(Data.SkinG));
				writer.Write(Convert.ToUInt16(Data.SkinB));
				writer.Write(Convert.ToUInt16(0));
				if (Noclip)
				{
					writer.Write(Convert.ToUInt16(75));
				}
				else
				{
					writer.Write(Convert.ToUInt16(100));
				}
			}
			else if (Noclip)
			{
				writer.Write(Convert.ToUInt16(Data.SkinR));
				writer.Write(Convert.ToUInt16(Data.SkinG));
				writer.Write(Convert.ToUInt16(Data.SkinB));
				writer.Write(Convert.ToUInt16(75));
			}
			else if (Frozen)
			{
				writer.Write(Convert.ToUInt16(0));
				writer.Write(Convert.ToUInt16(215));
				writer.Write(Convert.ToUInt16(255));
				writer.Write(Convert.ToUInt16(75));
			}
			else
			{
				writer.Write(Convert.ToUInt16(Data.SkinR));
				writer.Write(Convert.ToUInt16(Data.SkinG));
				writer.Write(Convert.ToUInt16(Data.SkinB));
				writer.Write(Convert.ToUInt16(Data.SkinA));
			}
			writer.Write(Convert.ToByte(Data.Gender));
			if (Rewards.Flag.ContainsKey(Data.Filename))
			{
				string country = Rewards.Flag[Data.Filename];
				writer.Write(Convert.ToUInt16(Item.Flag(country)));
			}
			else
			{
				writer.Write(Convert.ToUInt16(Item.Flag(Country)));
			}
			writer.Write(Convert.ToUInt16(Item.Jumps(GetPartItem(1))));
			writer.Write(Encoding.UTF8.GetBytes(Data.Username + "\0"));
			writer.Write(Convert.ToUInt16(GetPartItem(1)));
			writer.Write(Convert.ToUInt16(GetPartItem(2)));
			writer.Write(Convert.ToUInt16(GetPartItem(3)));
			writer.Write(Convert.ToUInt16(GetPartItem(4)));
			writer.Write(Convert.ToUInt16(GetPartItem(5)));
			writer.Write(Convert.ToUInt16(GetPartItem(6)));
			writer.Write(Convert.ToUInt16(GetPartItem(7)));
			writer.Write(Convert.ToUInt16(GetPartItem(8)));
			writer.Write(Convert.ToUInt16(GetPartItem(9)));
			writer.Write(Convert.ToUInt16(GetPartItem(10)));
			writer.Write(Convert.ToUInt16(GetPartItem(11)));
			writer.Write(Convert.ToUInt16(GetPartItem(12)));
			writer.Write(Convert.ToUInt16(GetPartItem(13)));
			writer.Write(Convert.ToUInt16(Item.Effect(GetPartItem(1))));
			writer.Write(Convert.ToUInt16(Item.Effect(GetPartItem(2))));
			writer.Write(Convert.ToUInt16(Item.Effect(GetPartItem(7))));
			writer.Write(Convert.ToBoolean(VisibleTo(player)));
			writer.Write(Convert.ToBoolean(Noclip));
			writer.Write(Convert.ToBoolean(Frozen));
		}

		public static bool Online(string username)
		{
			foreach (Player item in Server.Online)
			{
				if (!item.Active || !item.Profile.Active || item.Profile.Data.Username != username)
				{
					continue;
				}
				return true;
			}
			return false;
		}
	}
}
