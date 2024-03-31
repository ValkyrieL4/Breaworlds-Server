using System;

namespace Breaworlds.Server
{
	public struct ShopListing
	{
		public ushort Image;

		public string Text1;

		public string Text2;

		public string Text3;

		public int Price;

		public int Amount;

		public DateTime AvailableFrom;

		public DateTime AvailableTo;

		public ShopItem[] Items;
	}
}
