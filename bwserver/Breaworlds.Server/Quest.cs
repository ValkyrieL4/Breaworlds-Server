using System;

namespace Breaworlds.Server
{
	internal class Quest
	{
		public static readonly bool Active = true;

		public static readonly string World = "BREAWORLDS";

		public static readonly int Start = 1;

		public static readonly int Count = 4;

		public static void RandomQuest(int level, out int type, out int item, out int left)
		{
			type = Server.Random.Next(8) + 1;
			int[] array = new int[1];
			if (type == 1)
			{
				array = new int[12]
				{
					0, 45, 47, 49, 51, 53, 55, 57, 59, 61,
					63, 65
				};
			}
			if (type == 2)
			{
				array = new int[12]
				{
					0, 45, 47, 49, 51, 53, 55, 57, 59, 61,
					63, 65
				};
			}
			if (type == 3)
			{
				array = new int[24]
				{
					0, 23, 25, 27, 29, 31, 33, 35, 37, 39,
					41, 43, 67, 69, 71, 181, 183, 185, 195, 197,
					199, 201, 203, 205
				};
			}
			if (type == 4)
			{
				array = new int[24]
				{
					0, 23, 25, 27, 29, 31, 33, 35, 37, 39,
					41, 43, 67, 69, 71, 181, 183, 185, 195, 197,
					199, 201, 203, 205
				};
			}
			if (type == 5)
			{
				array = new int[33]
				{
					0, 10, 12, 14, 16, 18, 20, 22, 24, 26,
					28, 30, 32, 34, 36, 38, 40, 42, 44, 46,
					48, 50, 52, 54, 56, 58, 60, 62, 64, 66,
					68, 70, 72
				};
			}
			if (type == 6)
			{
				array = new int[33]
				{
					0, 10, 12, 14, 16, 18, 20, 22, 24, 26,
					28, 30, 32, 34, 36, 38, 40, 42, 44, 46,
					48, 50, 52, 54, 56, 58, 60, 62, 64, 66,
					68, 70, 72
				};
			}
			if (type == 7)
			{
				array = new int[32]
				{
					0, 18, 20, 22, 24, 26, 28, 30, 32, 34,
					36, 38, 40, 42, 44, 46, 48, 50, 52, 54,
					56, 58, 60, 62, 64, 66, 68, 70, 72, 164,
					184, 186
				};
			}
			if (type == 8)
			{
				array = new int[6] { 561, 563, 565, 567, 569, 571 };
			}
			item = array[Server.Random.Next(array.Length)];
			if (type == 1 || type == 2 || type == 3 || type == 4)
			{
				if (item == 0)
				{
					left = Math.Min(Server.Random.Next(level) + 1, 200);
				}
				else
				{
					left = Math.Min(Server.Random.Next(level) + 1, 50);
				}
			}
			else if (type == 6 || type == 7)
			{
				if (item == 0)
				{
					left = Math.Min(Server.Random.Next(level) + 1, 100);
				}
				else
				{
					left = Math.Min(Server.Random.Next(level) + 1, 50);
				}
			}
			else if (type == 8)
			{
				if (item == 0)
				{
					left = Math.Min(Server.Random.Next(level) + 1, 50);
				}
				else
				{
					left = Math.Min(Server.Random.Next(level) + 1, 10);
				}
			}
			else
			{
				left = 0;
			}
		}

		public static int BreakBackground(int type, int item, int tile)
		{
			if (type == 1 && (item == tile || item == 0))
			{
				return 1;
			}
			return 0;
		}

		public static int BuildBackground(int type, int item, int tile)
		{
			if (type == 2 && (item == tile || item == 0))
			{
				return 1;
			}
			return 0;
		}

		public static int BreakForeground(int type, int item, int tile)
		{
			if (type == 3 && (item == tile || item == 0))
			{
				return 1;
			}
			return 0;
		}

		public static int BuildForeground(int type, int item, int tile)
		{
			if (type == 4 && (item == tile || item == 0))
			{
				return 1;
			}
			return 0;
		}

		public static int BreakSeed(int type, int item, int tile)
		{
			if (type == 5 && (item == tile || item == 0))
			{
				return 1;
			}
			return 0;
		}

		public static int BuildSeed(int type, int item, int tile)
		{
			if (type == 6 && (item == tile || item == 0))
			{
				return 1;
			}
			return 0;
		}

		public static int SpliceSeed(int type, int item, int tile)
		{
			if (type == 7 && (item == tile || item == 0))
			{
				return 1;
			}
			return 0;
		}

		public static int CatchFish(int type, int item, int fish)
		{
			if (type == 8 && (item == fish || item == 0))
			{
				return 1;
			}
			return 0;
		}
	}
}
