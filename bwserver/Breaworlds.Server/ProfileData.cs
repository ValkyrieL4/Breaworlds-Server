using System;
using System.Collections.Generic;

namespace Breaworlds.Server
{
	public struct ProfileData
	{
		public string Filename;

		public int Experience;

		public int Gems;

		public int MoonRocks;

		public int Level;

		public int MuteDuration;

		public int VideoCooldown;

		public int TicketDuration;

		public int BanDuration;

		public int Warnings;

		public int Bans;

		public int RewardCooldown;

		public bool AllowRespin;

		public bool ClearedMoonRocks;

		public bool HasReviewed;

		public int QuestType;

		public int QuestItem;

		public int QuestLeft;

		public int QuestLevel;

		public bool ItemQuest;

		public ushort ItemQuestType;

		public ushort ItemQuestStep;

		public int ItemQuestLeft;

		public string Device;

		public string StaffVerifiedDevice;

		public string Address;

		public string Session;

		public string Username;

		public int FakeLevel;

		public string Password;

		public string Mailname;

		public List<string> Worlds;

		public List<string> Friends;

		public List<string> Purchases;

		public int ItemSlots;

		public List<ushort> ItemIndex;

		public List<ushort> ItemCount;

		public List<ushort> ItemEquip;

		public Options Options;

		public bool FriendOffline;

		public bool FriendUnknown;

		public int[] Achievements;

		public int SkinA;

		public int SkinR;

		public int SkinG;

		public int SkinB;

		public byte Gender;

		public DateTime RegisterDate;

		public DateTime LoginDate;

		public int Online;

		public int Rating;
	}
}
