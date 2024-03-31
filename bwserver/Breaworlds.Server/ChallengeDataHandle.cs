using System.Collections.Generic;

namespace Breaworlds.Server
{
	public class ChallengeDataHandle
	{
		public bool Cached;

		public CacheState State;

		public ChallengeData Data;

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

		public ChallengeTask DailyTask1
		{
			get
			{
				return Data.DailyTask1;
			}
			set
			{
				Data.DailyTask1 = value;
			}
		}

		public ChallengeTask DailyTask2
		{
			get
			{
				return Data.DailyTask2;
			}
			set
			{
				Data.DailyTask2 = value;
			}
		}

		public ChallengeTask DailyTask3
		{
			get
			{
				return Data.DailyTask3;
			}
			set
			{
				Data.DailyTask3 = value;
			}
		}

		public ChallengeTask DailyTask4
		{
			get
			{
				return Data.DailyTask4;
			}
			set
			{
				Data.DailyTask4 = value;
			}
		}

		public ChallengeTask DailyTask5
		{
			get
			{
				return Data.DailyTask5;
			}
			set
			{
				Data.DailyTask5 = value;
			}
		}

		public ChallengeTask DailyTask6
		{
			get
			{
				return Data.DailyTask6;
			}
			set
			{
				Data.DailyTask6 = value;
			}
		}

		public ChallengeTask DailyTask7
		{
			get
			{
				return Data.DailyTask7;
			}
			set
			{
				Data.DailyTask7 = value;
			}
		}

		public List<ChallengeParticipant> Participants
		{
			get
			{
				return Data.Participants;
			}
			set
			{
				Data.Participants = value;
			}
		}

		public ChallengeDataHandle(bool binded)
		{
			if (Database.Debugging)
			{
				if (binded)
				{
					Terminal.Message("New challenge handle initialized.");
				}
				else
				{
					Terminal.Message("New challenge handle initialized outside of the database.");
				}
			}
		}
	}
}
