using System;
using System.Collections.Generic;

namespace Breaworlds.Server
{
	public class SessionDataHandle
	{
		public bool Cached;

		public CacheState State;

		public SessionData Data;

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

		public int SizeX
		{
			get
			{
				return Data.SizeX;
			}
			set
			{
				Data.SizeX = value;
			}
		}

		public int SizeY
		{
			get
			{
				return Data.SizeY;
			}
			set
			{
				Data.SizeY = value;
			}
		}

		public int Theme
		{
			get
			{
				return Data.Theme;
			}
			set
			{
				Data.Theme = value;
			}
		}

		public int Banned
		{
			get
			{
				return Data.Banned;
			}
			set
			{
				Data.Banned = value;
			}
		}

		public bool Public
		{
			get
			{
				return Data.Public;
			}
			set
			{
				Data.Public = value;
			}
		}

		public string Name
		{
			get
			{
				return Data.Name;
			}
			set
			{
				Data.Name = value;
			}
		}

		public string Owner
		{
			get
			{
				return Data.Owner;
			}
			set
			{
				Data.Owner = value;
			}
		}

		public List<DroppedItem> Drop
		{
			get
			{
				return Data.Drop;
			}
			set
			{
				Data.Drop = value;
			}
		}

		public List<BanData> Bans
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

		public List<string> Admin
		{
			get
			{
				return Data.Admin;
			}
			set
			{
				Data.Admin = value;
			}
		}

		public ushort[] Background
		{
			get
			{
				return Data.Background;
			}
			set
			{
				Data.Background = value;
			}
		}

		public ushort[] Foreground
		{
			get
			{
				return Data.Foreground;
			}
			set
			{
				Data.Foreground = value;
			}
		}

		public object[] Special
		{
			get
			{
				return Data.Special;
			}
			set
			{
				Data.Special = value;
			}
		}

		public object[] Parent
		{
			get
			{
				return Data.Parent;
			}
			set
			{
				Data.Parent = value;
			}
		}

		public bool AntiPunch
		{
			get
			{
				return Data.AntiPunch;
			}
			set
			{
				Data.AntiPunch = value;
			}
		}

		public bool AntiTalk
		{
			get
			{
				return Data.AntiTalk;
			}
			set
			{
				Data.AntiTalk = value;
			}
		}

		public bool AntiDrop
		{
			get
			{
				return Data.AntiDrop;
			}
			set
			{
				Data.AntiDrop = value;
			}
		}

		public DateTime CreateDate
		{
			get
			{
				return Data.CreateDate;
			}
			set
			{
				Data.CreateDate = value;
			}
		}

		public SessionDataHandle(bool binded)
		{
			if (Database.Debugging)
			{
				if (binded)
				{
					Terminal.Message("New session handle initialized.");
				}
				else
				{
					Terminal.Message("New session handle initialized outside of the database.");
				}
			}
		}
	}
}
