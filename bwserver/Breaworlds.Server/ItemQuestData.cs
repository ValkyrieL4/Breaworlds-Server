namespace Breaworlds.Server
{
	public struct ItemQuestData
	{
		public string Title;

		public bool Startable;

		public int RewardIndex;

		public int RewardCount;

		public ItemQuestStepData[] Steps;
	}
}
