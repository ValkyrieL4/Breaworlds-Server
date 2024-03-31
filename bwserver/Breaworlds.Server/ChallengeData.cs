using System.Collections.Generic;

namespace Breaworlds.Server
{
	public struct ChallengeData
	{
		public string Filename;

		public ChallengeTask DailyTask1;

		public ChallengeTask DailyTask2;

		public ChallengeTask DailyTask3;

		public ChallengeTask DailyTask4;

		public ChallengeTask DailyTask5;

		public ChallengeTask DailyTask6;

		public ChallengeTask DailyTask7;

		public List<ChallengeParticipant> Participants;
	}
}
