namespace Breaworlds.Server
{
	internal class ItemQuest
	{
		public static readonly bool Active = true;

		public static readonly ItemQuestData[] Quests = new ItemQuestData[7]
		{
			new ItemQuestData
			{
				Title = "Nightmare Wings Quest",
				Startable = true,
				RewardIndex = 543,
				RewardCount = 1,
				Steps = new ItemQuestStepData[20]
				{
					new ItemQuestStepData
					{
						Event = PlayerEvent.SpendGems,
						Index = 0,
						Count = 100000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 149,
						Count = 5
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Experience,
						Index = 0,
						Count = 100000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 451,
						Count = 1
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Fish,
						Index = 571,
						Count = 10
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 187,
						Count = 10
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Build,
						Index = 0,
						Count = 10000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 525,
						Count = 1
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.WinGame,
						Index = 0,
						Count = 100
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Harvest,
						Index = 0,
						Count = 5000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 909,
						Count = 1
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Fish,
						Index = 0,
						Count = 100
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Splice,
						Index = 0,
						Count = 5000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 11,
						Count = 500
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Break,
						Index = 197,
						Count = 1000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Harvest,
						Index = 728,
						Count = 500
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.EarnGems,
						Index = 0,
						Count = 100000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 901,
						Count = 1
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Plant,
						Index = 0,
						Count = 5000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 371,
						Count = 1
					}
				}
			},
			new ItemQuestData
			{
				Title = "Flaming Cape Quest",
				Startable = true,
				RewardIndex = 1017,
				RewardCount = 1,
				Steps = new ItemQuestStepData[20]
				{
					new ItemQuestStepData
					{
						Event = PlayerEvent.Break,
						Index = 1031,
						Count = 500
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.SpendGems,
						Index = 0,
						Count = 100000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Experience,
						Index = 0,
						Count = 100000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Splice,
						Index = 1032,
						Count = 500
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Fish,
						Index = 901,
						Count = 1
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 11,
						Count = 500
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Fish,
						Index = 563,
						Count = 100
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 0,
						Count = 100000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 669,
						Count = 1
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Harvest,
						Index = 12,
						Count = 5000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Fish,
						Index = 567,
						Count = 100
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Break,
						Index = 11,
						Count = 1000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 139,
						Count = 3
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Fish,
						Index = 569,
						Count = 50
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.EarnGems,
						Index = 0,
						Count = 100000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.WinGame,
						Index = 0,
						Count = 100
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 949,
						Count = 3
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Plant,
						Index = 0,
						Count = 5000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Fish,
						Index = 571,
						Count = 5
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 741,
						Count = 1
					}
				}
			},
			new ItemQuestData
			{
				Title = "Invisible Skin Quest",
				Startable = true,
				RewardIndex = 1037,
				RewardCount = 1,
				Steps = new ItemQuestStepData[20]
				{
					new ItemQuestStepData
					{
						Event = PlayerEvent.Break,
						Index = 21,
						Count = 5000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.SpendGems,
						Index = 0,
						Count = 100000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Experience,
						Index = 0,
						Count = 100000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Break,
						Index = 243,
						Count = 500
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Fish,
						Index = 0,
						Count = 100
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 309,
						Count = 500
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Fish,
						Index = 563,
						Count = 100
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 0,
						Count = 100000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 513,
						Count = 3
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Harvest,
						Index = 22,
						Count = 5000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Fish,
						Index = 567,
						Count = 100
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Break,
						Index = 217,
						Count = 1000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 535,
						Count = 10
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Fish,
						Index = 569,
						Count = 50
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.EarnGems,
						Index = 0,
						Count = 100000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.PlayGame,
						Index = 0,
						Count = 500
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 953,
						Count = 5
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Plant,
						Index = 0,
						Count = 5000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Fish,
						Index = 571,
						Count = 5
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 191,
						Count = 1
					}
				}
			},
			new ItemQuestData
			{
				Title = "Red Neon Cape Quest",
				Startable = true,
				RewardIndex = 1021,
				RewardCount = 1,
				Steps = new ItemQuestStepData[10]
				{
					new ItemQuestStepData
					{
						Event = PlayerEvent.Break,
						Index = 925,
						Count = 1000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Experience,
						Index = 0,
						Count = 10000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 949,
						Count = 1
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Harvest,
						Index = 28,
						Count = 100
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.EarnGems,
						Index = 0,
						Count = 50000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Plant,
						Index = 28,
						Count = 100
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 333,
						Count = 1
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Fish,
						Index = 0,
						Count = 100
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 139,
						Count = 3
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 1019,
						Count = 1
					}
				}
			},
			new ItemQuestData
			{
				Title = "Green Neon Cape Quest",
				Startable = true,
				RewardIndex = 1023,
				RewardCount = 1,
				Steps = new ItemQuestStepData[10]
				{
					new ItemQuestStepData
					{
						Event = PlayerEvent.Break,
						Index = 927,
						Count = 1000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Experience,
						Index = 0,
						Count = 10000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 951,
						Count = 1
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Harvest,
						Index = 38,
						Count = 100
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.EarnGems,
						Index = 0,
						Count = 50000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Plant,
						Index = 38,
						Count = 100
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 335,
						Count = 1
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Fish,
						Index = 0,
						Count = 100
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 139,
						Count = 3
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 1019,
						Count = 1
					}
				}
			},
			new ItemQuestData
			{
				Title = "Blue Neon Cape Quest",
				Startable = true,
				RewardIndex = 1025,
				RewardCount = 1,
				Steps = new ItemQuestStepData[10]
				{
					new ItemQuestStepData
					{
						Event = PlayerEvent.Break,
						Index = 929,
						Count = 1000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Experience,
						Index = 0,
						Count = 10000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 953,
						Count = 1
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Harvest,
						Index = 40,
						Count = 100
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.EarnGems,
						Index = 0,
						Count = 50000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Plant,
						Index = 40,
						Count = 100
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 337,
						Count = 1
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Fish,
						Index = 0,
						Count = 100
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 139,
						Count = 3
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 1019,
						Count = 1
					}
				}
			},
			new ItemQuestData
			{
				Title = "Yellow Neon Cape Quest",
				Startable = true,
				RewardIndex = 1027,
				RewardCount = 1,
				Steps = new ItemQuestStepData[10]
				{
					new ItemQuestStepData
					{
						Event = PlayerEvent.Break,
						Index = 931,
						Count = 1000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Experience,
						Index = 0,
						Count = 10000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 955,
						Count = 1
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Harvest,
						Index = 34,
						Count = 100
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.EarnGems,
						Index = 0,
						Count = 50000
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Plant,
						Index = 34,
						Count = 100
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 339,
						Count = 1
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Fish,
						Index = 0,
						Count = 100
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 139,
						Count = 3
					},
					new ItemQuestStepData
					{
						Event = PlayerEvent.Deliver,
						Index = 1019,
						Count = 1
					}
				}
			}
		};

		public static string StepTitle(ItemQuestStepData step)
		{
			if (step.Event == PlayerEvent.Deliver)
			{
				if (step.Index == 0)
				{
					return $"deliver ~1{step.Count} ~0gems";
				}
				return $"deliver ~1x{step.Count} {Item.Name(step.Index)}~0";
			}
			if (step.Event == PlayerEvent.Build)
			{
				if (step.Index == 0)
				{
					return $"build ~1{step.Count} ~0of any block";
				}
				return $"build ~1x{step.Count} {Item.Name(step.Index)}~0";
			}
			if (step.Event == PlayerEvent.Break)
			{
				if (step.Index == 0)
				{
					return $"break ~1{step.Count} ~0of any block";
				}
				return $"break ~1x{step.Count} {Item.Name(step.Index)}~0";
			}
			if (step.Event == PlayerEvent.Plant)
			{
				if (step.Index == 0)
				{
					return $"plant ~1{step.Count} ~0of any seed";
				}
				return $"plant ~1x{step.Count} {Item.Name(step.Index)}~0";
			}
			if (step.Event == PlayerEvent.Splice)
			{
				if (step.Index == 0)
				{
					return $"splice ~1{step.Count} ~0of any seed";
				}
				return $"splice ~1x{step.Count} {Item.Name(step.Index)}~0";
			}
			if (step.Event == PlayerEvent.Harvest)
			{
				if (step.Index == 0)
				{
					return $"harvest ~1{step.Count} ~0of any seed";
				}
				return $"harvest ~1x{step.Count} {Item.Name(step.Index)}~0";
			}
			if (step.Event == PlayerEvent.Experience)
			{
				return $"gain ~1{step.Count} ~0experience points";
			}
			if (step.Event == PlayerEvent.SpendGems)
			{
				return $"spend ~1{step.Count} ~0gems";
			}
			if (step.Event == PlayerEvent.EarnGems)
			{
				return $"earn ~1{step.Count} ~0gems";
			}
			if (step.Event == PlayerEvent.Fish)
			{
				if (step.Index == 0)
				{
					return $"catch ~1{step.Count} ~0of any fish";
				}
				return $"catch ~1x{step.Count} ~1{Item.Name(step.Index)}~0";
			}
			if (step.Event == PlayerEvent.PlayGame)
			{
				return $"play ~1{step.Count} ~0games";
			}
			if (step.Event == PlayerEvent.WinGame)
			{
				return $"play and win ~1{step.Count} ~0games";
			}
			if (step.Event == PlayerEvent.GemMachine)
			{
				return $"harvest ~1{step.Count} ~0gem machines";
			}
			if (step.Event == PlayerEvent.BaitBox)
			{
				if (step.Index == 0)
				{
					return $"collect ~1x{step.Count} {Item.Name(step.Index)} ~0from bait boxes";
				}
				return $"harvest ~1{step.Count} ~0bait boxes";
			}
			return string.Empty;
		}
	}
}
