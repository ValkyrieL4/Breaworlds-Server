namespace Breaworlds.Server
{
	public class Platform
	{
		public static string Name(int id)
		{
			return id switch
			{
				0 => "Windows", 
				1 => "MacOS", 
				2 => "Browser", 
				3 => "iOS", 
				4 => "Android", 
				_ => $"Unknown ({id})", 
			};
		}

		public static bool Equals(int id, PlatformType type)
		{
			if (id == 0 && type == PlatformType.Windows)
			{
				return true;
			}
			if (id == 1 && type == PlatformType.MacOS)
			{
				return true;
			}
			if (id == 2 && type == PlatformType.Browser)
			{
				return true;
			}
			if (id == 3 && type == PlatformType.iOS)
			{
				return true;
			}
			if (id == 4 && type == PlatformType.Android)
			{
				return true;
			}
			return false;
		}

		public static bool Mobile(int id)
		{
			return id switch
			{
				3 => true, 
				4 => true, 
				_ => false, 
			};
		}

		public static bool Desktop(int id)
		{
			return id switch
			{
				0 => true, 
				1 => true, 
				_ => false, 
			};
		}

		public static bool Outdated(int id, int version)
		{
			if (id == 0 && version <= 393216)
			{
				return true;
			}
			if (id == 1 && version <= 167788555)
			{
				return true;
			}
			if (id == 3 && version <= 134234113)
			{
				return true;
			}
			if (id == 4 && version <= 24)
			{
				return true;
			}
			return false;
		}

		public static string Version(int id, int version)
		{
			if (id == 0 && version == 327680)
			{
				return "Windows 2000 (5.0)";
			}
			if (id == 0 && version == 327681)
			{
				return "Windows XP (5.1)";
			}
			if (id == 0 && version == 327682)
			{
				return "Windows XP 64-Bit Edition (5.2)";
			}
			if (id == 0 && version == 327682)
			{
				return "Windows Server 2003 (5.2)";
			}
			if (id == 0 && version == 327682)
			{
				return "Windows Server 2003 R2 (5.2)";
			}
			if (id == 0 && version == 393216)
			{
				return "Windows Vista (6.0)";
			}
			if (id == 0 && version == 393216)
			{
				return "Windows Server 2008 (6.0)";
			}
			if (id == 0 && version == 393217)
			{
				return "Windows Server 2008 R2 (6.1)";
			}
			if (id == 0 && version == 393217)
			{
				return "Windows 7 (6.1)";
			}
			if (id == 0 && version == 393218)
			{
				return "Windows Server 2012 (6.2)";
			}
			if (id == 0 && version == 393218)
			{
				return "Windows 8 (6.2)";
			}
			if (id == 0 && version == 393219)
			{
				return "Windows 8.1 (6.3)";
			}
			if (id == 0 && version == 655360)
			{
				return "Windows 10 (10.0)";
			}
			if (id == 1 && version == 167772164)
			{
				return "Cheetah (10.0.4)";
			}
			if (id == 1 && version == 167776261)
			{
				return "Puma (10.1.5)";
			}
			if (id == 1 && version == 167780360)
			{
				return "Jaguar (10.2.8)";
			}
			if (id == 1 && version == 167784457)
			{
				return "Panther (10.3.9)";
			}
			if (id == 1 && version == 167788555)
			{
				return "Tiger (10.4.11)";
			}
			if (id == 1 && version == 167792648)
			{
				return "Leopard (10.5.8)";
			}
			if (id == 1 && version == 167796744)
			{
				return "Snow Leopard (10.6.8)";
			}
			if (id == 1 && version == 167800837)
			{
				return "Lion (10.7.5)";
			}
			if (id == 1 && version == 167804930)
			{
				return "Mountain Lion (10.8.2)";
			}
			if (id == 1 && version == 167809025)
			{
				return "Mavericks (10.9.1)";
			}
			if (id == 1 && version == 167813120)
			{
				return "Yosemite (10.10)";
			}
			if (id == 1 && version == 167817216)
			{
				return "El Capitan (10.11)";
			}
			if (id == 1 && version == 167821312)
			{
				return "Sierra (10.12)";
			}
			if (id == 1 && version == 167825408)
			{
				return "High Sierra (10.13)";
			}
			if (id == 1 && version == 167829504)
			{
				return "Mojave (10.14)";
			}
			if (id == 3 && version == 50335747)
			{
				return "iOS 3 (3.1.3)";
			}
			if (id == 3 && version == 67117057)
			{
				return "iOS 4 (4.2.1)";
			}
			if (id == 3 && version == 83890177)
			{
				return "iOS 5 (5.1.1)";
			}
			if (id == 3 && version == 100663297)
			{
				return "iOS 6 (6.0.1)";
			}
			if (id == 3 && version == 100663298)
			{
				return "iOS 6 (6.0.2)";
			}
			if (id == 3 && version == 100667392)
			{
				return "iOS 6 (6.1)";
			}
			if (id == 3 && version == 117440516)
			{
				return "iOS 7 (7.0.4)";
			}
			if (id == 3 && version == 117444608)
			{
				return "iOS 7 (7.1)";
			}
			if (id == 3 && version == 134217728)
			{
				return "iOS 8 (8.0)";
			}
			if (id == 3 && version == 134221824)
			{
				return "iOS 8 (8.1)";
			}
			if (id == 3 && version == 134225920)
			{
				return "iOS 8 (8.2)";
			}
			if (id == 3 && version == 134230016)
			{
				return "iOS 8 (8.3)";
			}
			if (id == 3 && version == 134234112)
			{
				return "iOS 8 (8.4)";
			}
			if (id == 3 && version == 134234113)
			{
				return "iOS 8 (8.4.1)";
			}
			if (id == 3 && version == 150994944)
			{
				return "iOS 9 (9.0)";
			}
			if (id == 3 && version == 150994945)
			{
				return "iOS 9 (9.0.1)";
			}
			if (id == 3 && version == 150994946)
			{
				return "iOS 9 (9.0.2)";
			}
			if (id == 3 && version == 150999040)
			{
				return "iOS 9 (9.1)";
			}
			if (id == 3 && version == 151003136)
			{
				return "iOS 9 (9.2)";
			}
			if (id == 3 && version == 151003137)
			{
				return "iOS 9 (9.2.1)";
			}
			if (id == 3 && version == 151007232)
			{
				return "iOS 9 (9.3)";
			}
			if (id == 3 && version == 167772160)
			{
				return "iOS 10 (10.0)";
			}
			if (id == 3 && version == 167776256)
			{
				return "iOS 10 (10.1)";
			}
			if (id == 3 && version == 167780352)
			{
				return "iOS 10 (10.2)";
			}
			if (id == 3 && version == 167784448)
			{
				return "iOS 10 (10.3)";
			}
			if (id == 3 && version == 184549376)
			{
				return "iOS 11 (11.0)";
			}
			if (id == 3 && version == 184553472)
			{
				return "iOS 11 (11.1)";
			}
			if (id == 3 && version == 184557568)
			{
				return "iOS 11 (11.2)";
			}
			if (id == 3 && version == 184561664)
			{
				return "iOS 11 (11.3)";
			}
			if (id == 3 && version == 184565760)
			{
				return "iOS 11 (11.4)";
			}
			if (id == 3 && version == 201326592)
			{
				return "iOS 12 (12.0)";
			}
			if (id == 4 && version == 3)
			{
				return "Cupcake (1.5)";
			}
			if (id == 4 && version == 4)
			{
				return "Donut (1.6)";
			}
			if (id == 4 && version == 5)
			{
				return "Eclair (2.0)";
			}
			if (id == 4 && version == 6)
			{
				return "Eclair (2.0.1)";
			}
			if (id == 4 && version == 7)
			{
				return "Eclair (2.1)";
			}
			if (id == 4 && version == 8)
			{
				return "Froyo (2.2.x)";
			}
			if (id == 4 && version == 9)
			{
				return "Gingerbread (2.3 - 2.3.2)";
			}
			if (id == 4 && version == 10)
			{
				return "Gingerbread (2.3.3 - 2.3.7)";
			}
			if (id == 4 && version == 11)
			{
				return "Honeycomb (3.0)";
			}
			if (id == 4 && version == 12)
			{
				return "Honeycomb (3.1)";
			}
			if (id == 4 && version == 13)
			{
				return "Honeycomb (3.2.x)";
			}
			if (id == 4 && version == 14)
			{
				return "Ice Cream Sandwich (4.0.1 - 4.0.2)";
			}
			if (id == 4 && version == 15)
			{
				return "Ice Cream Sandwich (4.0.3 - 4.0.4)";
			}
			if (id == 4 && version == 16)
			{
				return "Jelly Bean (4.1.x)";
			}
			if (id == 4 && version == 17)
			{
				return "Jelly Bean (4.2.x)";
			}
			if (id == 4 && version == 18)
			{
				return "Jelly Bean (4.3.x)";
			}
			if (id == 4 && version == 19)
			{
				return "KitKat (4.4.x)";
			}
			if (id == 4 && version == 21)
			{
				return "Lollipop (5.0 - 5.1.1)";
			}
			if (id == 4 && version == 22)
			{
				return "Lollipop (5.0 - 5.1.1)";
			}
			if (id == 4 && version == 23)
			{
				return "Marshmallow (6.0 - 6.0.1)";
			}
			if (id == 4 && version == 24)
			{
				return "Nougat (7.0 - 7.11)";
			}
			if (id == 4 && version == 25)
			{
				return "Nougat (7.0 - 7.11)";
			}
			if (id == 4 && version == 26)
			{
				return "Oreo (8.0 - 8.11)";
			}
			if (id == 4 && version == 27)
			{
				return "Oreo (8.0 - 8.11)";
			}
			if (id == 4 && version == 28)
			{
				return "Pie (9.0)";
			}
			return $"Unknown ({version})";
		}
	}
}
