using System;
using System.Collections.Generic;

namespace Breaworlds.Server
{
	public struct SessionData
	{
		public string Filename;

		public int SizeX;

		public int SizeY;

		public int Theme;

		public int Banned;

		public bool Public;

		public string Name;

		public string Owner;

		public List<DroppedItem> Drop;

		public List<BanData> Bans;

		public List<string> Admin;

		public ushort[] Background;

		public ushort[] Foreground;

		public object[] Special;

		public object[] Parent;

		public bool AntiPunch;

		public bool AntiTalk;

		public bool AntiDrop;

		public DateTime CreateDate;
	}
}
