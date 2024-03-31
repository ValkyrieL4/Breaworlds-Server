using System;
using System.IO;

namespace Breaworlds.Server
{
	public class PlayerQuests
	{
		public static void Event(Player invoker, PlayerEvent type, params int[] arguments)
		{
			try
			{
				if (!invoker.Profile.Data.ItemQuest || invoker.Profile.Data.ItemQuestLeft <= 0)
				{
					return;
				}
				ItemQuestData itemQuestData = ItemQuest.Quests[invoker.Profile.Data.ItemQuestType];
				ItemQuestStepData itemQuestStepData = itemQuestData.Steps[invoker.Profile.Data.ItemQuestStep];
				if (type == PlayerEvent.Deliver && type == itemQuestStepData.Event)
				{
					if (arguments[0] == itemQuestStepData.Index || itemQuestStepData.Index == 0)
					{
						invoker.Profile.Data.ItemQuestLeft -= arguments[1];
					}
				}
				else if (type == PlayerEvent.Build && type == itemQuestStepData.Event)
				{
					if (arguments[0] == itemQuestStepData.Index || itemQuestStepData.Index == 0)
					{
						invoker.Profile.Data.ItemQuestLeft--;
					}
				}
				else if (type == PlayerEvent.Break && type == itemQuestStepData.Event)
				{
					if (arguments[0] == itemQuestStepData.Index || itemQuestStepData.Index == 0)
					{
						invoker.Profile.Data.ItemQuestLeft--;
					}
				}
				else if (type == PlayerEvent.Plant && type == itemQuestStepData.Event)
				{
					if (arguments[0] == itemQuestStepData.Index || itemQuestStepData.Index == 0)
					{
						invoker.Profile.Data.ItemQuestLeft--;
					}
				}
				else if (type == PlayerEvent.Splice && type == itemQuestStepData.Event)
				{
					if (arguments[0] == itemQuestStepData.Index || itemQuestStepData.Index == 0)
					{
						invoker.Profile.Data.ItemQuestLeft--;
					}
				}
				else if (type == PlayerEvent.Harvest && type == itemQuestStepData.Event)
				{
					if (arguments[0] == itemQuestStepData.Index || itemQuestStepData.Index == 0)
					{
						invoker.Profile.Data.ItemQuestLeft--;
					}
				}
				else if (type == PlayerEvent.Experience && type == itemQuestStepData.Event)
				{
					invoker.Profile.Data.ItemQuestLeft -= arguments[0];
				}
				else if (type == PlayerEvent.SpendGems && type == itemQuestStepData.Event)
				{
					invoker.Profile.Data.ItemQuestLeft -= arguments[0];
				}
				else if (type == PlayerEvent.EarnGems && type == itemQuestStepData.Event)
				{
					invoker.Profile.Data.ItemQuestLeft -= arguments[0];
				}
				else if (type == PlayerEvent.Fish && type == itemQuestStepData.Event)
				{
					if (arguments[0] == itemQuestStepData.Index || itemQuestStepData.Index == 0)
					{
						invoker.Profile.Data.ItemQuestLeft--;
					}
				}
				else if (type == PlayerEvent.PlayGame && type == itemQuestStepData.Event)
				{
					invoker.Profile.Data.ItemQuestLeft--;
				}
				else if (type == PlayerEvent.WinGame && type == itemQuestStepData.Event)
				{
					invoker.Profile.Data.ItemQuestLeft--;
				}
				else if (type == PlayerEvent.GemMachine && type == itemQuestStepData.Event)
				{
					invoker.Profile.Data.ItemQuestLeft--;
				}
				else if (type == PlayerEvent.BaitBox && type == itemQuestStepData.Event && (arguments[0] == itemQuestStepData.Index || itemQuestStepData.Index == 0))
				{
					invoker.Profile.Data.ItemQuestLeft--;
				}
				if (invoker.Profile.Data.ItemQuestLeft < 0)
				{
					invoker.Profile.Data.ItemQuestLeft = 0;
				}
				if (invoker.Profile.Data.ItemQuestLeft == 0)
				{
					PlayerLayout.Warning(invoker, 200, 2, "~1QUEST STEP ~4COMPLETE~1!", "Congratulations, you've completed your quest step.");
				}
			}
			catch (Exception)
			{
				invoker.Close();
			}
		}

		public static void ShowItemQuest(Player invoker)
		{
			try
			{
				if (invoker.Profile.Data.ItemQuest)
				{
					PlayerCore.UpdateDialog(invoker, 0, "Quest.Item.Step", delegate(BinaryWriter dialog)
					{
						Dialog.ItemText(dialog, breaker: true, "~1Item quest step", 75, 3);
						ItemQuestData itemQuestData = ItemQuest.Quests[invoker.Profile.Data.ItemQuestType];
						ItemQuestStepData step = itemQuestData.Steps[invoker.Profile.Data.ItemQuestStep];
						Dialog.Text(dialog, breaker: true, $"You are on the ~1{itemQuestData.Title}~0.", 50);
						Dialog.Text(dialog, breaker: true, $"You have to {ItemQuest.StepTitle(step)}. ({step.Count - invoker.Profile.Data.ItemQuestLeft}/{step.Count})", 50);
						if (invoker.Profile.Data.ItemQuestLeft <= 0)
						{
							Dialog.Button(dialog, breaker: true, "Continue", "Continue");
						}
						else
						{
							if (step.Event == PlayerEvent.Deliver)
							{
								Dialog.Button(dialog, breaker: true, "Deliver", "Deliver items");
							}
							Dialog.Button(dialog, breaker: true, "Cancel", "Cancel quest");
						}
						Dialog.Button(dialog, breaker: true, "Close", "Close");
					});
					return;
				}
				PlayerCore.UpdateDialog(invoker, 0, "Quest.Item", delegate(BinaryWriter dialog)
				{
					Dialog.ItemText(dialog, breaker: true, "~1Start item quest", 75, 3);
					Dialog.Text(dialog, breaker: true, "Welcome, pick the item you'd like to get and", 50);
					Dialog.Text(dialog, breaker: true, "let's start completing steps of your quest.", 50);
					int num = 0;
					for (int i = 0; i < ItemQuest.Quests.Length; i++)
					{
						if (ItemQuest.Quests[i].Startable)
						{
							if (num == 4 || i == ItemQuest.Quests.Length - 1)
							{
								Dialog.ItemButton(dialog, breaker: true, i.ToString(), ItemQuest.Quests[i].RewardIndex, 96);
								num = 0;
							}
							else
							{
								Dialog.ItemButton(dialog, breaker: false, i.ToString(), ItemQuest.Quests[i].RewardIndex, 96);
								num++;
							}
						}
					}
					Dialog.Button(dialog, breaker: true, "Cancel", "Cancel");
				});
			}
			catch (Exception)
			{
				invoker.Close();
			}
		}
	}
}
