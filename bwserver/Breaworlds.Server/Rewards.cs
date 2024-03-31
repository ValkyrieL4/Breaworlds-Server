using System.Collections.Generic;

namespace Breaworlds.Server
{
	public class Rewards
	{
		public static Dictionary<string, Permissions> Permissions = new Dictionary<string, Permissions>
		{
			{
				"Zyliss",
				Breaworlds.Server.Permissions.Dev
			},
			{
				"Ragnar",
				Breaworlds.Server.Permissions.Dev
			},
            {
				"marko",
				Breaworlds.Server.Permissions.Admin
            }
		};

		public static List<string> Capitalization = new List<string> { "SDFG2" };

		public static List<string> Testing = new List<string> { "Ragnar", "Zyliss" };

		public static Dictionary<string, string> Flag = new Dictionary<string, string>
		{
			{ "Ragnar", "breaworlds.verified" },
			{ "Zyliss", "breaworlds.verified" }
		};

		public static List<RewardItem> Daily = new List<RewardItem>
		{
			new RewardItem
			{
				Index = 73,
				Count = 1,
				Priority = 10
			},
			new RewardItem
			{
				Index = 1199,
				Count = 1,
				Priority = 50
			},
			new RewardItem
			{
				Index = 1185,
				Count = 1,
				Priority = 30
			},
			new RewardItem
			{
				Index = 1165,
				Count = 1,
				Priority = 20
			},
			new RewardItem
			{
				Index = 1181,
				Count = 5,
				Priority = 40
			},
			new RewardItem
			{
				Index = 1155,
				Count = 1,
				Priority = 20
			},
			new RewardItem
			{
				Index = 1207,
				Count = 1,
				Priority = 20
			}
		};

		public static List<RewardItem> Fishing1 = new List<RewardItem>
		{
			new RewardItem
			{
				Index = 563,
				Count = 1,
				Priority = 1
			},
			new RewardItem
			{
				Index = 567,
				Count = 1,
				Priority = 3
			},
			new RewardItem
			{
				Index = 569,
				Count = 1,
				Priority = 3
			}
		};

		public static List<RewardItem> Fishing2 = new List<RewardItem>
		{
			new RewardItem
			{
				Index = 563,
				Count = 1,
				Priority = 1
			},
			new RewardItem
			{
				Index = 567,
				Count = 1,
				Priority = 3
			},
			new RewardItem
			{
				Index = 569,
				Count = 1,
				Priority = 3
			},
			new RewardItem
			{
				Index = 561,
				Count = 1,
				Priority = 5
			},
			new RewardItem
			{
				Index = 565,
				Count = 1,
				Priority = 5
			}
		};

		public static List<RewardItem> Fishing3 = new List<RewardItem>
		{
			new RewardItem
			{
				Index = 563,
				Count = 1,
				Priority = 1
			},
			new RewardItem
			{
				Index = 567,
				Count = 1,
				Priority = 1
			},
			new RewardItem
			{
				Index = 569,
				Count = 1,
				Priority = 1
			},
			new RewardItem
			{
				Index = 561,
				Count = 1,
				Priority = 2
			},
			new RewardItem
			{
				Index = 565,
				Count = 1,
				Priority = 2
			},
			new RewardItem
			{
				Index = 571,
				Count = 1,
				Priority = 5
			},
			new RewardItem
			{
				Index = 329,
				Count = 1,
				Priority = 100
			},
			new RewardItem
			{
				Index = 331,
				Count = 1,
				Priority = 200
			},
			new RewardItem
			{
				Index = 901,
				Count = 1,
				Priority = 200
			},
			new RewardItem
			{
				Index = 921,
				Count = 1,
				Priority = 100
			},
			new RewardItem
			{
				Index = 923,
				Count = 1,
				Priority = 100
			}
		};

		public static List<RewardItem> Bait = new List<RewardItem>
		{
			new RewardItem
			{
				Index = 547,
				Count = 1,
				Priority = 1
			},
			new RewardItem
			{
				Index = 549,
				Count = 1,
				Priority = 3
			},
			new RewardItem
			{
				Index = 551,
				Count = 1,
				Priority = 5
			},
			new RewardItem
			{
				Index = 553,
				Count = 1,
				Priority = 10
			},
			new RewardItem
			{
				Index = 555,
				Count = 1,
				Priority = 10
			}
		};

		public static List<RewardSkin> Skins = new List<RewardSkin>
		{
			new RewardSkin
			{
				Level = 1,
				A = 100,
				R = 254,
				G = 227,
				B = 198
			},
			new RewardSkin
			{
				Level = 1,
				A = 100,
				R = 253,
				G = 231,
				B = 173
			},
			new RewardSkin
			{
				Level = 1,
				A = 100,
				R = 248,
				G = 217,
				B = 152
			},
			new RewardSkin
			{
				Level = 1,
				A = 100,
				R = 249,
				G = 212,
				B = 160
			},
			new RewardSkin
			{
				Level = 1,
				A = 100,
				R = 236,
				G = 192,
				B = 145
			},
			new RewardSkin
			{
				Level = 1,
				A = 100,
				R = 242,
				G = 194,
				B = 128
			},
			new RewardSkin
			{
				Level = 1,
				A = 100,
				R = 212,
				G = 158,
				B = 122
			},
			new RewardSkin
			{
				Level = 1,
				A = 100,
				R = 187,
				G = 101,
				B = 54
			},
			new RewardSkin
			{
				Level = 1,
				A = 100,
				R = 207,
				G = 150,
				B = 95
			},
			new RewardSkin
			{
				Level = 1,
				A = 100,
				R = 173,
				G = 138,
				B = 96
			},
			new RewardSkin
			{
				Level = 1,
				A = 100,
				R = 147,
				G = 95,
				B = 55
			},
			new RewardSkin
			{
				Level = 1,
				A = 100,
				R = 115,
				G = 63,
				B = 23
			},
			new RewardSkin
			{
				Level = 1,
				A = 100,
				R = 178,
				G = 102,
				B = 68
			},
			new RewardSkin
			{
				Level = 1,
				A = 100,
				R = 127,
				G = 68,
				B = 34
			},
			new RewardSkin
			{
				Level = 1,
				A = 100,
				R = 95,
				G = 51,
				B = 16
			},
			new RewardSkin
			{
				Level = 1,
				A = 100,
				R = 41,
				G = 23,
				B = 9
			},
			new RewardSkin
			{
				Level = 10,
				A = 100,
				R = 83,
				G = 139,
				B = 219
			},
			new RewardSkin
			{
				Level = 20,
				A = 100,
				R = 92,
				G = 196,
				B = 60
			},
			new RewardSkin
			{
				Level = 30,
				A = 100,
				R = 214,
				G = 74,
				B = 41
			},
			new RewardSkin
			{
				Level = 40,
				A = 100,
				R = byte.MaxValue,
				G = 221,
				B = 55
			},
			new RewardSkin
			{
				Level = 50,
				A = 100,
				R = byte.MaxValue,
				G = 136,
				B = 43
			},
			new RewardSkin
			{
				Level = 60,
				A = 100,
				R = 106,
				G = 38,
				B = 165
			},
			new RewardSkin
			{
				Level = 70,
				A = 100,
				R = 128,
				G = 128,
				B = 128
			},
			new RewardSkin
			{
				Level = 80,
				A = 100,
				R = 201,
				G = 62,
				B = 205
			}
		};

		public static RewardItem Random(List<RewardItem> list)
		{
			RewardItem result;
			do
			{
				result = list[Server.Random.Next(list.Count)];
			}
			while (Server.Random.Next(result.Priority) != 0);
			return result;
		}

		public static bool Permission(string name, Permissions permissions)
		{
			if (Permissions.ContainsKey(name))
			{
				return Permissions[name] >= permissions;
			}
			if (permissions <= Breaworlds.Server.Permissions.None)
			{
				return true;
			}
			return false;
		}

		public static Permissions GetPermission(string name)
		{
			if (Permissions.ContainsKey(name))
			{
				return Permissions[name];
			}
			return Breaworlds.Server.Permissions.None;
		}

		public static RewardSkin[] UnlockedSkins(int level)
		{
			List<RewardSkin> list = new List<RewardSkin>();
			foreach (RewardSkin skin in Skins)
			{
				if (skin.Level <= level)
				{
					list.Add(skin);
				}
			}
			return list.ToArray();
		}
	}
}
