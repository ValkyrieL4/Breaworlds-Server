using System;

namespace Breaworlds.Server
{
	internal class Randomizer
	{
		private static int RandomX;

		private static int RandomY;

		private static int RandomZ;

		private static int Random;

		public static int Get(int min, int max)
		{
			if (min > max)
			{
				return 0;
			}
			int num = max - min;
			int num2 = (int)(GetRandomDouble() * (double)num);
			return num2 + min;
		}

		private static double GetRandomDouble()
		{
			int num = DateTime.Now.Millisecond * Random;
			num %= 100000;
			for (int i = RandomX - 10; i < RandomX + 10; i++)
			{
				num += Random;
				UpdateRandoms();
			}
			for (int j = RandomY - 10; j < RandomY + 10; j++)
			{
				num -= Random;
				UpdateRandoms();
			}
			for (int k = RandomZ - 10; k < RandomZ + 10; k++)
			{
				if (num > 10000)
				{
					num += Random;
					UpdateRandoms();
				}
				else
				{
					num -= Random;
					UpdateRandoms();
				}
			}
			num = Math.Abs(num);
			num %= 100000;
			UpdateRandoms();
			return (double)num / 100000.0;
		}

		private static void UpdateRandoms()
		{
			RandomX += RandomY;
			RandomY += RandomZ;
			RandomZ += RandomX;
			Random++;
		}
	}
}
