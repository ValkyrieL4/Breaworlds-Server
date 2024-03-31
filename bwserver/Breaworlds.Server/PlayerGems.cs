using System;

namespace Breaworlds.Server
{
	public class PlayerGems
	{
		public static void Add(Player invoker, int gems)
		{
			try
			{
				invoker.Profile.Data.Gems += gems;
				PlayerCore.UpdateGems(invoker);
				PlayerQuests.Event(invoker, PlayerEvent.EarnGems, gems);
			}
			catch (Exception)
			{
				invoker.Close();
			}
		}

		public static void Remove(Player invoker, int gems)
		{
			try
			{
				invoker.Profile.Data.Gems -= gems;
				PlayerCore.UpdateGems(invoker);
				PlayerQuests.Event(invoker, PlayerEvent.SpendGems, gems);
			}
			catch (Exception)
			{
				invoker.Close();
			}
		}

		public static bool Has(Player invoker, int gems)
		{
			return invoker.Profile.Data.Gems >= gems;
		}

		public static void Set(Player invoker, int gems)
		{
			try
			{
				invoker.Profile.Data.Gems += gems;
				PlayerCore.UpdateGems(invoker);
			}
			catch (Exception)
			{
				invoker.Close();
			}
		}

		public static int Get(Player invoker)
		{
			return invoker.Profile.Data.Gems;
		}
	}
}
