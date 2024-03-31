using System;
using System.Collections.Generic;

namespace Breaworlds.Server
{
	public class Challenge
	{
		public static int Price = 100;

		public static bool Active = true;

		public static ChallengeDataHandle Data;

		public static void Initialize()
		{
			if (Database.ChallengeExists(CurrentName()))
			{
				Database.ChallengeLoad(ref Data, CurrentName());
				return;
			}
			Generate();
			Database.ChallengeSave(Data);
		}

		public static DateTime GetWeekBeginning(DateTime date)
		{
			return date.DayOfWeek switch
			{
				DayOfWeek.Monday => date, 
				DayOfWeek.Tuesday => date.AddDays(-1.0), 
				DayOfWeek.Wednesday => date.AddDays(-2.0), 
				DayOfWeek.Thursday => date.AddDays(-3.0), 
				DayOfWeek.Friday => date.AddDays(-4.0), 
				DayOfWeek.Saturday => date.AddDays(-5.0), 
				DayOfWeek.Sunday => date.AddDays(-6.0), 
				_ => date, 
			};
		}

		public static string CurrentName()
		{
			DateTime dateTime = ((DateTime.UtcNow.DayOfWeek == DayOfWeek.Monday) ? DateTime.UtcNow : ((DateTime.UtcNow.DayOfWeek == DayOfWeek.Tuesday) ? DateTime.UtcNow.AddDays(-1.0) : ((DateTime.UtcNow.DayOfWeek == DayOfWeek.Wednesday) ? DateTime.UtcNow.AddDays(-2.0) : ((DateTime.UtcNow.DayOfWeek == DayOfWeek.Thursday) ? DateTime.UtcNow.AddDays(-3.0) : ((DateTime.UtcNow.DayOfWeek == DayOfWeek.Friday) ? DateTime.UtcNow.AddDays(-4.0) : ((DateTime.UtcNow.DayOfWeek == DayOfWeek.Saturday) ? DateTime.UtcNow.AddDays(-5.0) : ((DateTime.UtcNow.DayOfWeek != 0) ? DateTime.UtcNow : DateTime.UtcNow.AddDays(-6.0))))))));
			return $"{dateTime.Year}_{dateTime.Month}_{dateTime.Day}";
		}

		public static string CurrentTitle()
		{
			if (CurrentDay() == 1)
			{
				return $"[{CurrentDay()}] ~6Today is the registration day for the upcoming weekly challenge.";
			}
			if (CurrentDay() == 7)
			{
				return $"[{CurrentDay()}] ~6Today is the final day of weekly challenge, final results are available.";
			}
			return $"[{CurrentDay()}] ~6Today is the {CurrentTaskName()} day of the current weekly challenge.";
		}

		public static int CurrentDay()
		{
			return DateTime.UtcNow.DayOfWeek switch
			{
				DayOfWeek.Monday => 1, 
				DayOfWeek.Tuesday => 2, 
				DayOfWeek.Wednesday => 3, 
				DayOfWeek.Thursday => 4, 
				DayOfWeek.Friday => 5, 
				DayOfWeek.Saturday => 6, 
				DayOfWeek.Sunday => 7, 
				_ => 0, 
			};
		}

		public static void Generate()
		{
			Data = new ChallengeDataHandle(binded: false)
			{
				Participants = new List<ChallengeParticipant>(),
				Filename = CurrentName()
			};
			Data.DailyTask1 = new ChallengeTask
			{
				Type = 0,
				Item = 0
			};
			Data.DailyTask2 = RandomTask();
			Data.DailyTask3 = RandomTask();
			Data.DailyTask4 = RandomTask();
			Data.DailyTask5 = RandomTask();
			Data.DailyTask6 = RandomTask();
			Data.DailyTask7 = new ChallengeTask
			{
				Type = 0,
				Item = 0
			};
			Server.SendLog("Breaworlds.Challenge", $"Started new challenge {CurrentName()}");
		}

		public static bool Update()
		{
			if (Data.Filename != CurrentName())
			{
				Database.ChallengeSave(Data);
				Database.ChallengeClose(Data.Filename);
				Generate();
				Database.ChallengeSave(Data);
				return true;
			}
			return false;
		}

		public static void Register(string username)
		{
			if (CurrentDay() == 1)
			{
				Data.Participants.Add(new ChallengeParticipant
				{
					Username = username
				});
				Server.SendLog("Breaworlds.Challenge", $"Registered {username} for challenge");
				Database.ChallengeSave(Data);
			}
		}

		public static ChallengeParticipant FindParticipant(string username)
		{
			foreach (ChallengeParticipant participant in Data.Participants)
			{
				if (participant.Username == username)
				{
					return participant;
				}
			}
			return default(ChallengeParticipant);
		}

		public static ChallengeParticipant[] WeeklyLeaderboard()
		{
			Update();
			ChallengeParticipant[] array = Data.Participants.ToArray();
			Array.Sort(array, (ChallengeParticipant x, ChallengeParticipant y) => ParticipantPoints(y).CompareTo(ParticipantPoints(x)));
			return array;
		}

		public static void SetTask(int day, ChallengeTask task)
		{
			switch (day)
			{
			case 1:
				Data.DailyTask1 = task;
				break;
			case 2:
				Data.DailyTask1 = task;
				break;
			case 3:
				Data.DailyTask1 = task;
				break;
			case 4:
				Data.DailyTask1 = task;
				break;
			case 5:
				Data.DailyTask1 = task;
				break;
			case 6:
				Data.DailyTask1 = task;
				break;
			case 7:
				Data.DailyTask1 = task;
				break;
			}
			Database.ChallengeSave(Data);
		}

		public static int ParticipantPlace(string username)
		{
			ChallengeParticipant[] array = WeeklyLeaderboard();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Username == username)
				{
					return i + 1;
				}
			}
			return 0;
		}

		public static ChallengeTask RandomTask()
		{
			int num = Server.Random.Next(8) + 1;
			int[] array = new int[1];
			if (num == 1)
			{
				array = new int[11]
				{
					45, 47, 49, 51, 53, 55, 57, 59, 61, 63,
					65
				};
			}
			if (num == 2)
			{
				array = new int[11]
				{
					45, 47, 49, 51, 53, 55, 57, 59, 61, 63,
					65
				};
			}
			if (num == 3)
			{
				array = new int[23]
				{
					23, 25, 27, 29, 31, 33, 35, 37, 39, 41,
					43, 67, 69, 71, 181, 183, 185, 195, 197, 199,
					201, 203, 205
				};
			}
			if (num == 4)
			{
				array = new int[23]
				{
					23, 25, 27, 29, 31, 33, 35, 37, 39, 41,
					43, 67, 69, 71, 181, 183, 185, 195, 197, 199,
					201, 203, 205
				};
			}
			if (num == 5)
			{
				array = new int[32]
				{
					10, 12, 14, 16, 18, 20, 22, 24, 26, 28,
					30, 32, 34, 36, 38, 40, 42, 44, 46, 48,
					50, 52, 54, 56, 58, 60, 62, 64, 66, 68,
					70, 72
				};
			}
			if (num == 6)
			{
				array = new int[32]
				{
					10, 12, 14, 16, 18, 20, 22, 24, 26, 28,
					30, 32, 34, 36, 38, 40, 42, 44, 46, 48,
					50, 52, 54, 56, 58, 60, 62, 64, 66, 68,
					70, 72
				};
			}
			if (num == 7)
			{
				array = new int[31]
				{
					18, 20, 22, 24, 26, 28, 30, 32, 34, 36,
					38, 40, 42, 44, 46, 48, 50, 52, 54, 56,
					58, 60, 62, 64, 66, 68, 70, 72, 164, 184,
					186
				};
			}
			if (num == 8)
			{
				array = new int[6] { 561, 563, 565, 567, 569, 571 };
			}
			int item = array[Server.Random.Next(array.Length)];
			ChallengeTask result = default(ChallengeTask);
			result.Type = num;
			result.Item = item;
			return result;
		}

		public static bool IsParticipant(string username)
		{
			if (Update())
			{
				return false;
			}
			foreach (ChallengeParticipant participant in Data.Participants)
			{
				if (participant.Username == username)
				{
					return true;
				}
			}
			return false;
		}

		public static void ParticipantGain(string username)
		{
			if (Update())
			{
				return;
			}
			for (int i = 0; i < Data.Participants.Count; i++)
			{
				ChallengeParticipant value = Data.Participants[i];
				if (value.Username == username)
				{
					switch (CurrentDay())
					{
					case 1:
						value.DailyPoints1++;
						break;
					case 2:
						value.DailyPoints2++;
						break;
					case 3:
						value.DailyPoints3++;
						break;
					case 4:
						value.DailyPoints4++;
						break;
					case 5:
						value.DailyPoints5++;
						break;
					case 6:
						value.DailyPoints6++;
						break;
					case 7:
						value.DailyPoints7++;
						break;
					}
					Data.Participants[i] = value;
					Database.ChallengeSave(Data);
				}
			}
		}

		public static void ParticipantReward(string username)
		{
			if (CurrentDay() != 7 || Update())
			{
				return;
			}
			for (int i = 0; i < Data.Participants.Count; i++)
			{
				if (Data.Participants[i].Username == username)
				{
					ChallengeParticipant value = Data.Participants[i];
					value.Rewarded = true;
					Data.Participants[i] = value;
					Server.SendLog("Breaworlds.Challenge", $"Rewarded {username}");
				}
			}
			Database.ChallengeSave(Data);
		}

		public static int ParticipantPoints(ChallengeParticipant participant)
		{
			return participant.DailyPoints1 + participant.DailyPoints2 + participant.DailyPoints3 + participant.DailyPoints4 + participant.DailyPoints5 + participant.DailyPoints6 + participant.DailyPoints7;
		}

		public static int ParticipantPoints(ChallengeParticipant participant, int day)
		{
			return day switch
			{
				1 => participant.DailyPoints1, 
				2 => participant.DailyPoints2, 
				3 => participant.DailyPoints3, 
				4 => participant.DailyPoints4, 
				5 => participant.DailyPoints5, 
				6 => participant.DailyPoints6, 
				7 => participant.DailyPoints7, 
				_ => 0, 
			};
		}

		public static ChallengeTask CurrentTask()
		{
			Update();
			return CurrentDay() switch
			{
				1 => Data.DailyTask1, 
				2 => Data.DailyTask2, 
				3 => Data.DailyTask3, 
				4 => Data.DailyTask4, 
				5 => Data.DailyTask5, 
				6 => Data.DailyTask6, 
				7 => Data.DailyTask7, 
				_ => default(ChallengeTask), 
			};
		}

		public static string CurrentTaskName()
		{
			Update();
			return CurrentDay() switch
			{
				1 => TaskName(Data.DailyTask1), 
				2 => TaskName(Data.DailyTask2), 
				3 => TaskName(Data.DailyTask3), 
				4 => TaskName(Data.DailyTask4), 
				5 => TaskName(Data.DailyTask5), 
				6 => TaskName(Data.DailyTask6), 
				7 => TaskName(Data.DailyTask7), 
				_ => string.Empty, 
			};
		}

		public static string TaskName(ChallengeTask task)
		{
			string arg = Item.Name(task.Item);
			if (task.Item == 0)
			{
				arg = "Any item";
			}
			if (task.Type == 1)
			{
				return $"{arg} breaking";
			}
			if (task.Type == 2)
			{
				return $"{arg} building";
			}
			if (task.Type == 3)
			{
				return $"{arg} breaking";
			}
			if (task.Type == 4)
			{
				return $"{arg} building";
			}
			if (task.Type == 5)
			{
				return $"{arg} harvesting";
			}
			if (task.Type == 6)
			{
				return $"{arg} planting";
			}
			if (task.Type == 7)
			{
				return $"{arg} splicing";
			}
			if (task.Type == 7)
			{
				return $"{arg} fishing";
			}
			return $"{Item.Name(task.Item)} custom";
		}

		public static void BreakBackground(string username, int item)
		{
			ChallengeTask challengeTask = CurrentTask();
			if (challengeTask.Type == 1 && (challengeTask.Item == item || challengeTask.Item == 0))
			{
				ParticipantGain(username);
			}
		}

		public static void BuildBackground(string username, int item)
		{
			ChallengeTask challengeTask = CurrentTask();
			if (challengeTask.Type == 2 && (challengeTask.Item == item || challengeTask.Item == 0))
			{
				ParticipantGain(username);
			}
		}

		public static void BreakForeground(string username, int item)
		{
			ChallengeTask challengeTask = CurrentTask();
			if (challengeTask.Type == 3 && (challengeTask.Item == item || challengeTask.Item == 0))
			{
				ParticipantGain(username);
			}
		}

		public static void BuildForeground(string username, int item)
		{
			ChallengeTask challengeTask = CurrentTask();
			if (challengeTask.Type == 4 && (challengeTask.Item == item || challengeTask.Item == 0))
			{
				ParticipantGain(username);
			}
		}

		public static void BreakSeed(string username, int item)
		{
			ChallengeTask challengeTask = CurrentTask();
			if (challengeTask.Type == 5 && (challengeTask.Item == item || challengeTask.Item == 0))
			{
				ParticipantGain(username);
			}
		}

		public static void BuildSeed(string username, int item)
		{
			ChallengeTask challengeTask = CurrentTask();
			if (challengeTask.Type == 6 && (challengeTask.Item == item || challengeTask.Item == 0))
			{
				ParticipantGain(username);
			}
		}

		public static void SpliceSeed(string username, int item)
		{
			ChallengeTask challengeTask = CurrentTask();
			if (challengeTask.Type == 7 && (challengeTask.Item == item || challengeTask.Item == 0))
			{
				ParticipantGain(username);
			}
		}

		public static void CatchFish(string username, int item)
		{
			ChallengeTask challengeTask = CurrentTask();
			if (challengeTask.Type == 8 && (challengeTask.Item == item || challengeTask.Item == 0))
			{
				ParticipantGain(username);
			}
		}
	}
}
