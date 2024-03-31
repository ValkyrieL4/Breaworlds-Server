namespace Breaworlds.Server
{
	public struct ShopCategory
	{
		public string Name;

		public ushort Image;

		public CurrencyType Currency;

		public ShopListing[] Listings;
	}
}
