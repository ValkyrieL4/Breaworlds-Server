using System;
using System.Collections.Generic;

namespace Breaworlds.Server
{
	internal class Event
	{
		public static readonly DateTime AvailableFrom = new DateTime(2020, 11, 16, 4, 0, 0);

		public static readonly DateTime AvailableTo = new DateTime(2020, 12, 1, 0, 0, 0);

		public static readonly int TimeToComplete = 5;

		public static readonly EventType Type = EventType.Space;

		public static readonly string World = "";

		public static readonly string Message = "~3SPACE EVENT~0 is here, break blocks with ~mhigh rarity~0 to find moon rocks.";

		public static readonly List<RewardItem> Rewards1 = new List<RewardItem>();

		public static readonly List<RewardItem> Rewards2 = new List<RewardItem>();

		public static bool Active
		{
			get
			{
				if (DateTime.UtcNow > AvailableFrom && DateTime.UtcNow < AvailableTo)
				{
					return true;
				}
				return false;
			}
		}

		public static bool TicketActive(int ticket)
		{
			if ((double)ticket > DateTime.UtcNow.Subtract(Server.Date).TotalSeconds)
			{
				return true;
			}
			return false;
		}

		public static int TicketDuration()
		{
			return (int)DateTime.UtcNow.Subtract(Server.Date).TotalSeconds + TimeToComplete * 60;
		}

		public static RewardItem GetReward1()
		{
			RewardItem result = Rewards1[Server.Random.Next(Rewards1.Count)];
			while (Server.Random.Next(result.Priority) != 0)
			{
				result = Rewards1[Server.Random.Next(Rewards1.Count)];
			}
			return result;
		}

		public static RewardItem GetReward2()
		{
			RewardItem result = Rewards2[Server.Random.Next(Rewards2.Count)];
			while (Server.Random.Next(result.Priority) != 0)
			{
				result = Rewards2[Server.Random.Next(Rewards2.Count)];
			}
			return result;
		}

		public static bool ActiveLater()
		{
			if (AvailableFrom != DateTime.MinValue && AvailableFrom > DateTime.UtcNow)
			{
				return true;
			}
			return false;
		}

		public static int ActiveAfter()
		{
			if (ActiveLater())
			{
				return (int)AvailableFrom.Subtract(DateTime.UtcNow).TotalSeconds;
			}
			return 0;
		}

		public static bool InactiveLater()
		{
			if (AvailableTo != DateTime.MinValue && AvailableTo > DateTime.UtcNow)
			{
				return true;
			}
			return false;
		}

		public static int InactiveAfter()
		{
			if (InactiveLater())
			{
				return (int)AvailableTo.Subtract(DateTime.UtcNow).TotalSeconds;
			}
			return 0;
		}
	}
}
