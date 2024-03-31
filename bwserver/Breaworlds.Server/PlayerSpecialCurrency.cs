using System;

namespace Breaworlds.Server
{
	public class PlayerSpecialCurrency
	{
		public static void Add(Player invoker, int currency)
		{
			try
			{
				invoker.Profile.Data.MoonRocks += currency;
				PlayerCore.UpdateSpecialCurrency(invoker);
			}
			catch (Exception)
			{
				invoker.Close();
			}
		}

		public static void Remove(Player invoker, int currency)
		{
			try
			{
				invoker.Profile.Data.MoonRocks -= currency;
				PlayerCore.UpdateSpecialCurrency(invoker);
			}
			catch (Exception)
			{
				invoker.Close();
			}
		}

		public static bool Has(Player invoker, int currency)
		{
			return invoker.Profile.Data.MoonRocks >= currency;
		}

		public static void Set(Player invoker, int currency)
		{
			try
			{
				invoker.Profile.Data.MoonRocks += currency;
				PlayerCore.UpdateSpecialCurrency(invoker);
			}
			catch (Exception)
			{
				invoker.Close();
			}
		}

		public static int Get(Player invoker)
		{
			return invoker.Profile.Data.MoonRocks;
		}
	}
}
