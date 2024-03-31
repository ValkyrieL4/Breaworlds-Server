using System;
using System.Collections.Generic;

namespace Breaworlds.Server
{
	public class ProfileDataHandle
	{
		public bool Cached;

		public CacheState State;

		public ProfileData Data;

		public string Filename
		{
			get
			{
				return Data.Filename;
			}
			set
			{
				Data.Filename = value;
			}
		}

		public int Experience
		{
			get
			{
				return Data.Experience;
			}
			set
			{
				Data.Experience = value;
			}
		}

		public int Gems
		{
			get
			{
				return Data.Gems;
			}
			set
			{
				Data.Gems = value;
			}
		}

		public int MoonRocks
		{
			get
			{
				return Data.MoonRocks;
			}
			set
			{
				Data.MoonRocks = value;
			}
		}

		public int Warnings
		{
			get
			{
				return Data.Warnings;
			}
			set
			{
				Data.Warnings = value;
			}
		}

		public int Bans
		{
			get
			{
				return Data.Bans;
			}
			set
			{
				Data.Bans = value;
			}
		}

		public int Level
		{
			get
			{
				return Data.Level;
			}
			set
			{
				Data.Level = value;
			}
		}

		public int MuteDuration
		{
			get
			{
				return Data.MuteDuration;
			}
			set
			{
				Data.MuteDuration = value;
			}
		}

		public int VideoCooldown
		{
			get
			{
				return Data.VideoCooldown;
			}
			set
			{
				Data.VideoCooldown = value;
			}
		}

		public int TicketDuration
		{
			get
			{
				return Data.TicketDuration;
			}
			set
			{
				Data.TicketDuration = value;
			}
		}

		public int BanDuration
		{
			get
			{
				return Data.BanDuration;
			}
			set
			{
				Data.BanDuration = value;
			}
		}

		public int RewardCooldown
		{
			get
			{
				return Data.RewardCooldown;
			}
			set
			{
				Data.RewardCooldown = value;
			}
		}

		public bool AllowRespin
		{
			get
			{
				return Data.AllowRespin;
			}
			set
			{
				Data.AllowRespin = value;
			}
		}

		public bool ClearedMoonRocks
		{
			get
			{
				return Data.ClearedMoonRocks;
			}
			set
			{
				Data.ClearedMoonRocks = value;
			}
		}

		public bool HasReviewed
		{
			get
			{
				return Data.HasReviewed;
			}
			set
			{
				Data.HasReviewed = value;
			}
		}

		public int QuestType
		{
			get
			{
				return Data.QuestType;
			}
			set
			{
				Data.QuestType = value;
			}
		}

		public int QuestItem
		{
			get
			{
				return Data.QuestItem;
			}
			set
			{
				Data.QuestItem = value;
			}
		}

		public int QuestLeft
		{
			get
			{
				return Data.QuestLeft;
			}
			set
			{
				Data.QuestLeft = value;
			}
		}

		public int QuestLevel
		{
			get
			{
				return Data.QuestLevel;
			}
			set
			{
				Data.QuestLevel = value;
			}
		}

		public bool ItemQuest
		{
			get
			{
				return Data.ItemQuest;
			}
			set
			{
				Data.ItemQuest = value;
			}
		}

		public ushort ItemQuestType
		{
			get
			{
				return Data.ItemQuestType;
			}
			set
			{
				Data.ItemQuestType = value;
			}
		}

		public ushort ItemQuestStep
		{
			get
			{
				return Data.ItemQuestStep;
			}
			set
			{
				Data.ItemQuestStep = value;
			}
		}

		public int ItemQuestLeft
		{
			get
			{
				return Data.ItemQuestLeft;
			}
			set
			{
				Data.ItemQuestLeft = value;
			}
		}

		public string Device
		{
			get
			{
				return Data.Device;
			}
			set
			{
				Data.Device = value;
			}
		}

		public string StaffVerifiedDevice
		{
			get
			{
				return Data.StaffVerifiedDevice;
			}
			set
			{
				Data.StaffVerifiedDevice = value;
			}
		}

		public string Address
		{
			get
			{
				return Data.Address;
			}
			set
			{
				Data.Address = value;
			}
		}

		public string Session
		{
			get
			{
				return Data.Session;
			}
			set
			{
				Data.Session = value;
			}
		}

		public string Username
		{
			get
			{
				return Data.Username;
			}
			set
			{
				Data.Username = value;
			}
		}

		public int FakeLevel
		{
			get
			{
				return Data.FakeLevel;
			}
			set
			{
				Data.FakeLevel = value;
			}
		}

		public string Password
		{
			get
			{
				return Data.Password;
			}
			set
			{
				Data.Password = value;
			}
		}

		public string Mailname
		{
			get
			{
				return Data.Mailname;
			}
			set
			{
				Data.Mailname = value;
			}
		}

		public List<string> Worlds
		{
			get
			{
				return Data.Worlds;
			}
			set
			{
				Data.Worlds = value;
			}
		}

		public List<string> Friends
		{
			get
			{
				return Data.Friends;
			}
			set
			{
				Data.Friends = value;
			}
		}

		public List<string> Purchases
		{
			get
			{
				return Data.Purchases;
			}
			set
			{
				Data.Purchases = value;
			}
		}

		public int ItemSlots
		{
			get
			{
				return Data.ItemSlots;
			}
			set
			{
				Data.ItemSlots = value;
			}
		}

		public List<ushort> ItemIndex
		{
			get
			{
				return Data.ItemIndex;
			}
			set
			{
				Data.ItemIndex = value;
			}
		}

		public List<ushort> ItemCount
		{
			get
			{
				return Data.ItemCount;
			}
			set
			{
				Data.ItemCount = value;
			}
		}

		public List<ushort> ItemEquip
		{
			get
			{
				return Data.ItemEquip;
			}
			set
			{
				Data.ItemEquip = value;
			}
		}

		public Options Options
		{
			get
			{
				return Data.Options;
			}
			set
			{
				Data.Options = value;
			}
		}

		public bool FriendOffline
		{
			get
			{
				return Data.FriendOffline;
			}
			set
			{
				Data.FriendOffline = value;
			}
		}

		public bool FriendUnknown
		{
			get
			{
				return Data.FriendUnknown;
			}
			set
			{
				Data.FriendUnknown = value;
			}
		}

		public int[] Achievements
		{
			get
			{
				return Data.Achievements;
			}
			set
			{
				Data.Achievements = value;
			}
		}

		public int SkinA
		{
			get
			{
				return Data.SkinA;
			}
			set
			{
				Data.SkinA = value;
			}
		}

		public int SkinR
		{
			get
			{
				return Data.SkinR;
			}
			set
			{
				Data.SkinR = value;
			}
		}

		public int SkinG
		{
			get
			{
				return Data.SkinG;
			}
			set
			{
				Data.SkinG = value;
			}
		}

		public int SkinB
		{
			get
			{
				return Data.SkinB;
			}
			set
			{
				Data.SkinB = value;
			}
		}

		public byte Gender
		{
			get
			{
				return Data.Gender;
			}
			set
			{
				Data.Gender = value;
			}
		}

		public DateTime RegisterDate
		{
			get
			{
				return Data.RegisterDate;
			}
			set
			{
				Data.RegisterDate = value;
			}
		}

		public DateTime LoginDate
		{
			get
			{
				return Data.LoginDate;
			}
			set
			{
				Data.LoginDate = value;
			}
		}

		public int Online
		{
			get
			{
				return Data.Online;
			}
			set
			{
				Data.Online = value;
			}
		}

		public ProfileDataHandle(bool binded)
		{
			if (Database.Debugging)
			{
				if (binded)
				{
					Terminal.Message("New profile handle initialized.");
				}
				else
				{
					Terminal.Message("New profile handle initialized outside of the database.");
				}
			}
		}
	}
}
