using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Breaworlds.Server
{
	public class Shop
	{
		public static readonly int DefaultCategory = 0;

		public static List<ShopCategory> Categories = new List<ShopCategory>
		{
			new ShopCategory
			{
				Name = "Gems",
				Image = 0,
				Currency = CurrencyType.Money,
				Listings = new ShopListing[4]
				{
					new ShopListing
					{
						Image = 0,
						Text1 = "~1Small Gem Pack + x5 Moon Rocks (~r+5% GEMS~1)",
						Text2 = "The small gem pack contains 22,000 gems",
						Text3 = "and it costs ~12.99 EUR~0."
					},
					new ShopListing
					{
						Image = 0,
						Text1 = "~1Medium Gem Pack + x10 Moon Rocks (~r+10% GEMS~1)",
						Text2 = "The medium gem pack contains 57,500 gems",
						Text3 = "and it costs ~14.99 EUR~0."
					},
					new ShopListing
					{
						Image = 0,
						Text1 = "~1Big Gem Pack + x15 Moon Rocks (~r+15% GEMS~1)",
						Text2 = "The big gem pack contains 240,000 gems",
						Text3 = "and it costs ~112.99 EUR~0."
					},
					new ShopListing
					{
						Image = 0,
						Text1 = "~1Huge Gem Pack + x30 Moon Rocks (~r+30% GEMS~1)",
						Text2 = "The huge gem pack contains 675,000 gems",
						Text3 = "and it costs ~129.99 EUR~0."
					}
				}
			},
			new ShopCategory
			{
				Name = "Upgrades",
				Image = 3,
				Currency = CurrencyType.Gems,
				Listings = new ShopListing[1]
				{
					new ShopListing
					{
						Image = 3,
						Text1 = "~1Inventory upgrade",
						Text2 = "This upgrade increases your inventory size",
						Text3 = "by 10 slots, click to view the price."
					}
				}
			},
			new ShopCategory
			{
				Name = "Token Items",
				Image = 283,
				Currency = CurrencyType.Tokens,
				Listings = new ShopListing[3]
				{
					new ShopListing
					{
						Image = 285,
						Text1 = "~1" + Item.Name(285),
						Text2 = "This awesome axe gives you the faster",
						Text3 = "breaking effect. Costs 100 tokens.",
						Price = 100,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 285,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 287,
						Text1 = "~1" + Item.Name(287),
						Text2 = "This pickaxe lets you break blocks faster",
						Text3 = "and has sparkling effect. Costs 500 tokens.",
						Price = 500,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 287,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 741,
						Text1 = "~1" + Item.Name(741),
						Text2 = "These wings let you jump twice in the air",
						Text3 = "and have flaming effect. Costs 500 tokens.",
						Price = 500,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 741,
								Count = 1,
								Priority = 1
							}
						}
					}
				}
			},
			new ShopCategory
			{
				Name = "Locks",
				Image = 73,
				Currency = CurrencyType.Gems,
				Listings = new ShopListing[5]
				{
					new ShopListing
					{
						Image = 319,
						Text1 = "~1" + Item.Name(319),
						Text2 = "Locks 3x3 tiles if there are no locks",
						Text3 = "by other players. Costs 100 gems.",
						Price = 100,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 319,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 321,
						Text1 = "~1" + Item.Name(321),
						Text2 = "Locks 7x7 tiles if there are no locks",
						Text3 = "by other players. Costs 500 gems.",
						Price = 500,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 321,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 323,
						Text1 = "~1" + Item.Name(323),
						Text2 = "Locks 15x15 tiles if there are no locks",
						Text3 = "by other players. Costs 1,000 gems.",
						Price = 1000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 323,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 73,
						Text1 = "~1" + Item.Name(73),
						Text2 = "Locks entire world if there are no locks",
						Text3 = "by other players. Costs 2,500 gems.",
						Price = 2500,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 73,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 493,
						Text1 = "~1" + Item.Name(493),
						Text2 = "Locks entire world if there are no locks",
						Text3 = "by other players. Costs 250,000 gems.",
						Price = 250000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 493,
								Count = 1,
								Priority = 1
							}
						}
					}
				}
			},
			new ShopCategory
			{
				Name = "Special Items",
				Image = 313,
				Currency = CurrencyType.Gems,
				Listings = new ShopListing[9]
				{
					new ShopListing
					{
						Image = 7,
						Text1 = "~1" + Item.Name(7),
						Text2 = "This magical item can change location of",
						Text3 = "your world entrance. Costs 5,000 gems.",
						Price = 5000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 7,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 157,
						Text1 = "~1" + Item.Name(157),
						Text2 = "This machine produces gems every few hours,",
						Text3 = "but don't forget to harvest it! Costs 12,500 gems.",
						Price = 12500,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 157,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 313,
						Text1 = "~1" + Item.Name(313),
						Text2 = "This machine can automatically sell your items",
						Text3 = "even if you are offline. Costs 10,000 gems.",
						Price = 10000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 313,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 535,
						Text1 = "~1" + Item.Name(535),
						Text2 = "This box can store single item that you can",
						Text3 = "show off with. Costs 5,000 gems.",
						Price = 5000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 535,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 155,
						Text1 = "~1" + Item.Name(155),
						Text2 = "This item is used to modify or reproduce",
						Text3 = "various items. Costs 500 gems.",
						Price = 500,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 155,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 495,
						Text1 = "~1" + Item.Name(495),
						Text2 = "This item disables punching in your world",
						Text3 = "completely for everyone. Costs 50,000 gems.",
						Price = 50000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 495,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 497,
						Text1 = "~1" + Item.Name(497),
						Text2 = "This item disables talking for everyone who",
						Text3 = "doesn't have access. Costs 100,000 gems.",
						Price = 100000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 497,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 499,
						Text1 = "~1" + Item.Name(499),
						Text2 = "This item disables dropping for everyone who",
						Text3 = "doesn't have access. Costs 100,000 gems.",
						Price = 100000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 499,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 611,
						Text1 = "~1" + Item.Name(611),
						Text2 = "This bomb can be used to clean all the basic blocks",
						Text3 = "instantly in a world you own. Costs 15,000 gems.",
						Price = 15000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 611,
								Count = 1,
								Priority = 1
							}
						}
					}
				}
			},
			new ShopCategory
			{
				Name = "Themes",
				Image = 147,
				Currency = CurrencyType.Gems,
				Listings = new ShopListing[5]
				{
					new ShopListing
					{
						Image = 147,
						Text1 = "~1" + Item.Name(147),
						Text2 = "Changes world theme to day, world must be",
						Text3 = "locked by you. Costs 1,000 gems.",
						Price = 1000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 147,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 149,
						Text1 = "~1" + Item.Name(149),
						Text2 = "Changes world theme to night, world must be",
						Text3 = "locked by you. Costs 5,000 gems.",
						Price = 5000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 149,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 151,
						Text1 = "~1" + Item.Name(151),
						Text2 = "Changes world theme to darkness, world must be",
						Text3 = "locked by you. Costs 10,000 gems.",
						Price = 10000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 151,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 227,
						Text1 = "~1" + Item.Name(227),
						Text2 = "Changes world theme to beach, world must be",
						Text3 = "locked by you. Costs 25,000 gems.",
						Price = 25000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 227,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 1063,
						Text1 = "~1" + Item.Name(1063),
						Text2 = "Changes world theme to space, world must be",
						Text3 = "locked by you. Costs 25,000 gems.",
						Price = 25000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 1063,
								Count = 1,
								Priority = 1
							}
						}
					}
				}
			},
			new ShopCategory
			{
				Name = "Item Packs",
				Image = 23,
				Currency = CurrencyType.Gems,
				Listings = new ShopListing[11]
				{
					new ShopListing
					{
						Image = 23,
						Text1 = "~1Block Pack",
						Text2 = "This pack contains various blocks that will",
						Text3 = "help you become a builder. Costs 500 gems.",
						Price = 500,
						Amount = 3,
						Items = new ShopItem[12]
						{
							new ShopItem
							{
								Index = 23,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 25,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 27,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 29,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 31,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 33,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 35,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 37,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 39,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 41,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 43,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 243,
								Count = 5,
								Priority = 5
							}
						}
					},
					new ShopListing
					{
						Image = 24,
						Text1 = "~1Seed Pack",
						Text2 = "This pack contains various seeds that will",
						Text3 = "help you with splicing. Costs 500 gems.",
						Price = 500,
						Amount = 3,
						Items = new ShopItem[15]
						{
							new ShopItem
							{
								Index = 22,
								Count = 5,
								Priority = 1
							},
							new ShopItem
							{
								Index = 24,
								Count = 5,
								Priority = 1
							},
							new ShopItem
							{
								Index = 26,
								Count = 5,
								Priority = 1
							},
							new ShopItem
							{
								Index = 28,
								Count = 5,
								Priority = 1
							},
							new ShopItem
							{
								Index = 30,
								Count = 5,
								Priority = 1
							},
							new ShopItem
							{
								Index = 32,
								Count = 5,
								Priority = 1
							},
							new ShopItem
							{
								Index = 34,
								Count = 5,
								Priority = 1
							},
							new ShopItem
							{
								Index = 36,
								Count = 5,
								Priority = 1
							},
							new ShopItem
							{
								Index = 38,
								Count = 5,
								Priority = 1
							},
							new ShopItem
							{
								Index = 40,
								Count = 5,
								Priority = 1
							},
							new ShopItem
							{
								Index = 42,
								Count = 5,
								Priority = 1
							},
							new ShopItem
							{
								Index = 44,
								Count = 5,
								Priority = 1
							},
							new ShopItem
							{
								Index = 68,
								Count = 5,
								Priority = 1
							},
							new ShopItem
							{
								Index = 70,
								Count = 5,
								Priority = 1
							},
							new ShopItem
							{
								Index = 72,
								Count = 5,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 117,
						Text1 = "~1Clothes Pack",
						Text2 = "This pack contains various wearable items to",
						Text3 = "customize your character. Costs 1,000 gems.",
						Price = 1000,
						Amount = 3,
						Items = new ShopItem[71]
						{
							new ShopItem
							{
								Index = 75,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 77,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 79,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 81,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 83,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 85,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 87,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 89,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 91,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 93,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 95,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 97,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 99,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 101,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 103,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 105,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 107,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 109,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 111,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 113,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 115,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 117,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 119,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 121,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 125,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 127,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 129,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 131,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 133,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 135,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 137,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 139,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 327,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 363,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 365,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 397,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 399,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 401,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 403,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 405,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 407,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 409,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 411,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 413,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 415,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 531,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 533,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 421,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 423,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 425,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 275,
								Count = 1,
								Priority = 5
							},
							new ShopItem
							{
								Index = 277,
								Count = 1,
								Priority = 5
							},
							new ShopItem
							{
								Index = 279,
								Count = 1,
								Priority = 5
							},
							new ShopItem
							{
								Index = 281,
								Count = 1,
								Priority = 5
							},
							new ShopItem
							{
								Index = 333,
								Count = 1,
								Priority = 5
							},
							new ShopItem
							{
								Index = 335,
								Count = 1,
								Priority = 5
							},
							new ShopItem
							{
								Index = 337,
								Count = 1,
								Priority = 5
							},
							new ShopItem
							{
								Index = 339,
								Count = 1,
								Priority = 5
							},
							new ShopItem
							{
								Index = 575,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 577,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 579,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 581,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 671,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 911,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 913,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 915,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 917,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 919,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 1029,
								Count = 1,
								Priority = 10
							},
							new ShopItem
							{
								Index = 1235,
								Count = 1,
								Priority = 10
							},
							new ShopItem
							{
								Index = 1237,
								Count = 1,
								Priority = 10
							}
						}
					},
					new ShopListing
					{
						Image = 481,
						Text1 = "~1Music Composer Pack",
						Text2 = "This pack contains various music tiles that",
						Text3 = "play lots of different sounds. Costs 250 gems.",
						Price = 250,
						Amount = 3,
						Items = new ShopItem[6]
						{
							new ShopItem
							{
								Index = 482,
								Count = 5,
								Priority = 1
							},
							new ShopItem
							{
								Index = 484,
								Count = 5,
								Priority = 1
							},
							new ShopItem
							{
								Index = 486,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 488,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 490,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 492,
								Count = 10,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 545,
						Text1 = "~1Fisher's Pack",
						Text2 = "This pack contains all the tools you'll ever need",
						Text3 = "to catch any type of fish. Costs 7,500 gems.",
						Price = 7500,
						Amount = 8,
						Items = new ShopItem[9]
						{
							new ShopItem
							{
								Index = 545,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 547,
								Count = 5,
								Priority = 1
							},
							new ShopItem
							{
								Index = 549,
								Count = 5,
								Priority = 1
							},
							new ShopItem
							{
								Index = 551,
								Count = 5,
								Priority = 1
							},
							new ShopItem
							{
								Index = 553,
								Count = 5,
								Priority = 1
							},
							new ShopItem
							{
								Index = 555,
								Count = 5,
								Priority = 1
							},
							new ShopItem
							{
								Index = 557,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 559,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 573,
								Count = 1,
								Priority = 100
							}
						}
					},
					new ShopListing
					{
						Image = 809,
						Text1 = "~1Numeric & Symbol Blocks Pack",
						Text2 = "This pack contains various number and symbol",
						Text3 = "blocks. Costs 500 gems.",
						Price = 500,
						Amount = 3,
						Items = new ShopItem[19]
						{
							new ShopItem
							{
								Index = 807,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 809,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 811,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 813,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 815,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 817,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 819,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 821,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 823,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 825,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 827,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 829,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 831,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 833,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 835,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 837,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 839,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 841,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 843,
								Count = 10,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 537,
						Text1 = "~1Fireworks Pack",
						Text2 = "This pack contains various fireworks which you can",
						Text3 = "combine together for awesome effects. Costs 100 gems.",
						Price = 100,
						Amount = 3,
						Items = new ShopItem[3]
						{
							new ShopItem
							{
								Index = 537,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 539,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 541,
								Count = 10,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 925,
						Text1 = "~1Neon Items Pack",
						Text2 = "This pack contains various neon blocks and weapons",
						Text3 = "that you can use anywhere. Costs 2,500 gems.",
						Price = 2500,
						Amount = 3,
						Items = new ShopItem[20]
						{
							new ShopItem
							{
								Index = 925,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 927,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 929,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 931,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 933,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 935,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 937,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 939,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 941,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 943,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 945,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 947,
								Count = 10,
								Priority = 1
							},
							new ShopItem
							{
								Index = 949,
								Count = 1,
								Priority = 20
							},
							new ShopItem
							{
								Index = 951,
								Count = 1,
								Priority = 20
							},
							new ShopItem
							{
								Index = 953,
								Count = 1,
								Priority = 20
							},
							new ShopItem
							{
								Index = 955,
								Count = 1,
								Priority = 20
							},
							new ShopItem
							{
								Index = 957,
								Count = 1,
								Priority = 40
							},
							new ShopItem
							{
								Index = 959,
								Count = 1,
								Priority = 40
							},
							new ShopItem
							{
								Index = 961,
								Count = 1,
								Priority = 40
							},
							new ShopItem
							{
								Index = 963,
								Count = 1,
								Priority = 40
							}
						}
					},
					new ShopListing
					{
						Image = 599,
						Text1 = "~1Game Blocks Pack (~r-20% OFF~1)",
						Text2 = "These blocks handle mini-game hosting for",
						Text3 = "you. Costs 40,000 gems.",
						Price = 40000,
						Amount = 3,
						Items = new ShopItem[3]
						{
							new ShopItem
							{
								Index = 597,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 599,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 601,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 671,
						AvailableFrom = new DateTime(2020, 11, 5, 0, 0, 0),
						Text1 = "~1Tuxedo Pack",
						Text2 = "This pack contains various tuxedos and",
						Text3 = "other accessories. Costs 500 gems.",
						Price = 500,
						Amount = 1,
						Items = new ShopItem[6]
						{
							new ShopItem
							{
								Index = 671,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 1111,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 1113,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 1115,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 1117,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 1119,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 1155,
						AvailableFrom = new DateTime(2020, 11, 15, 0, 0, 0),
						Text1 = "~1Medieval Pack",
						Text2 = "This pack contains various items from the",
						Text3 = "medieval times. Costs 25,000 gems.",
						Price = 25000,
						Amount = 3,
						Items = new ShopItem[28]
						{
							new ShopItem
							{
								Index = 1175,
								Count = 1,
								Priority = 100
							},
							new ShopItem
							{
								Index = 1155,
								Count = 1,
								Priority = 15
							},
							new ShopItem
							{
								Index = 1157,
								Count = 1,
								Priority = 15
							},
							new ShopItem
							{
								Index = 1159,
								Count = 1,
								Priority = 15
							},
							new ShopItem
							{
								Index = 1161,
								Count = 1,
								Priority = 15
							},
							new ShopItem
							{
								Index = 1163,
								Count = 1,
								Priority = 15
							},
							new ShopItem
							{
								Index = 1165,
								Count = 1,
								Priority = 5
							},
							new ShopItem
							{
								Index = 1167,
								Count = 1,
								Priority = 5
							},
							new ShopItem
							{
								Index = 1169,
								Count = 1,
								Priority = 5
							},
							new ShopItem
							{
								Index = 1171,
								Count = 1,
								Priority = 5
							},
							new ShopItem
							{
								Index = 1173,
								Count = 1,
								Priority = 5
							},
							new ShopItem
							{
								Index = 1177,
								Count = 1,
								Priority = 1
							},
							new ShopItem
							{
								Index = 1181,
								Count = 1,
								Priority = 10
							},
							new ShopItem
							{
								Index = 1183,
								Count = 1,
								Priority = 10
							},
							new ShopItem
							{
								Index = 1185,
								Count = 1,
								Priority = 10
							},
							new ShopItem
							{
								Index = 1187,
								Count = 1,
								Priority = 10
							},
							new ShopItem
							{
								Index = 1189,
								Count = 1,
								Priority = 10
							},
							new ShopItem
							{
								Index = 1191,
								Count = 1,
								Priority = 5
							},
							new ShopItem
							{
								Index = 1193,
								Count = 1,
								Priority = 5
							},
							new ShopItem
							{
								Index = 1195,
								Count = 1,
								Priority = 5
							},
							new ShopItem
							{
								Index = 1197,
								Count = 1,
								Priority = 5
							},
							new ShopItem
							{
								Index = 1199,
								Count = 1,
								Priority = 5
							},
							new ShopItem
							{
								Index = 1205,
								Count = 5,
								Priority = 5
							},
							new ShopItem
							{
								Index = 1207,
								Count = 5,
								Priority = 5
							},
							new ShopItem
							{
								Index = 1209,
								Count = 5,
								Priority = 5
							},
							new ShopItem
							{
								Index = 1211,
								Count = 5,
								Priority = 5
							},
							new ShopItem
							{
								Index = 1213,
								Count = 5,
								Priority = 5
							},
							new ShopItem
							{
								Index = 1215,
								Count = 5,
								Priority = 5
							}
						}
					}
				}
			},
			new ShopCategory
			{
				Name = "Pets",
				Image = 907,
				Currency = CurrencyType.Gems,
				Listings = new ShopListing[5]
				{
					new ShopListing
					{
						Image = 229,
						Text1 = "~1" + Item.Name(229),
						Text2 = "This pet will never leave you alone and follow",
						Text3 = "you anywhere you go. Costs 25,000 gems.",
						Price = 25000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 229,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 231,
						Text1 = "~1" + Item.Name(231),
						Text2 = "This pet will never leave you alone and follow",
						Text3 = "you anywhere you go. Costs 25,000 gems.",
						Price = 25000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 231,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 233,
						Text1 = "~1" + Item.Name(233),
						Text2 = "This pet will never leave you alone and follow",
						Text3 = "you anywhere you go. Costs 35,000 gems.",
						Price = 35000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 233,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 235,
						Text1 = "~1" + Item.Name(235),
						Text2 = "This pet will never leave you alone and follow",
						Text3 = "you anywhere you go. Costs 50,000 gems.",
						Price = 50000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 235,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 907,
						Text1 = "~1" + Item.Name(907),
						Text2 = "This pet will never leave you alone and follow",
						Text3 = "you anywhere you go. Costs 100,000 gems.",
						Price = 100000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 907,
								Count = 1,
								Priority = 1
							}
						}
					}
				}
			},
			new ShopCategory
			{
				Name = "Wearables",
				Image = 123,
				Currency = CurrencyType.Gems,
				Listings = new ShopListing[10]
				{
					new ShopListing
					{
						Image = 187,
						Text1 = "~1" + Item.Name(187),
						Text2 = "These wings let you jump in the air",
						Text3 = "one time. Costs 5,000 gems.",
						Price = 5000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 187,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 189,
						Text1 = "~1" + Item.Name(189),
						Text2 = "These wings let you jump in the air",
						Text3 = "two times. Costs 15,000 gems.",
						Price = 15000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 189,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 1019,
						Text1 = "~1" + Item.Name(1019),
						Text2 = "This cape lets you jump in the air",
						Text3 = "two times. Costs 500,000 gems.",
						Price = 500000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 1019,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 383,
						Text1 = "~1" + Item.Name(383),
						Text2 = "These wings let you jump in the air",
						Text3 = "two times. Costs 25,000 gems.",
						Price = 25000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 383,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 193,
						Text1 = "~1" + Item.Name(193),
						Text2 = "These wings let you jump in the air",
						Text3 = "three times. Costs 100,000 gems.",
						Price = 100000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 193,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 261,
						Text1 = "~1" + Item.Name(261),
						Text2 = "These wings let you jump in the air",
						Text3 = "two times. Costs 25,000 gems.",
						Price = 25000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 261,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 909,
						Text1 = "~1" + Item.Name(909),
						Text2 = "These wings let you jump in the air",
						Text3 = "two times. Costs 200,000 gems.",
						Price = 200000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 909,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 669,
						Text1 = "~1" + Item.Name(669),
						Text2 = "This item will always be burning even",
						Text3 = "if you go in water. Costs 100,000 gems.",
						Price = 100000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 669,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 691,
						Text1 = "~1" + Item.Name(691),
						Text2 = "This item is almost same as burning version",
						Text3 = "of it, just that it's acid. Costs 500,000 gems.",
						Price = 500000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 691,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 999,
						Text1 = "~1" + Item.Name(999),
						Text2 = "These wings let you jump in the air",
						Text3 = "three times. Costs 1,000,000 gems.",
						Price = 1000000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 999,
								Count = 1,
								Priority = 1
							}
						}
					}
				}
			},
			new ShopCategory
			{
				Name = "~0Space Items",
				Image = 1217,
				Currency = CurrencyType.MoonRocks,
				Listings = new ShopListing[7]
				{
					new ShopListing
					{
						Image = 1221,
						AvailableFrom = Event.AvailableFrom,
						AvailableTo = Event.AvailableTo,
						Text1 = "~1" + Item.Name(1221),
						Text2 = "This Space Helmet is designed for true",
						Text3 = " moon rock collectors. Costs 100 Moon Rocks.",
						Price = 100,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 1221,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 1223,
						AvailableFrom = Event.AvailableFrom,
						AvailableTo = Event.AvailableTo,
						Text1 = "~1" + Item.Name(1223),
						Text2 = "These Space Pants & Boots are designed",
						Text3 = " for true moon rock collectors. Costs 200 Moon Rocks.",
						Price = 200,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 1223,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 1239,
						AvailableFrom = Event.AvailableFrom,
						AvailableTo = Event.AvailableTo,
						Text1 = "~1" + Item.Name(1239),
						Text2 = "This pet will never leave you alone and follow",
						Text3 = "you anywhere you go. Costs 500 Moon Rocks.",
						Price = 500,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 1239,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 1219,
						AvailableFrom = Event.AvailableFrom,
						AvailableTo = Event.AvailableTo,
						Text1 = "~1" + Item.Name(1219),
						Text2 = "This Space Suit is designed for true",
						Text3 = " moon rock collectors. Costs 500 Moon Rocks.",
						Price = 500,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 1219,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 1241,
						AvailableFrom = Event.AvailableFrom,
						AvailableTo = Event.AvailableTo,
						Text1 = "~1" + Item.Name(1241),
						Text2 = "This pet will never leave you alone and follow",
						Text3 = "you anywhere you go. Costs 1,000 Moon Rocks.",
						Price = 1000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 1241,
								Count = 1,
								Priority = 1
							}
						}
					},
					new ShopListing
					{
						Image = 1225,
						AvailableFrom = Event.AvailableFrom,
						AvailableTo = Event.AvailableTo,
						Text1 = "~1Lightsaber Pack",
						Text2 = "This pack contains one Lightsaber from the depths of outer space,",
						Text3 = " with a chance at the ~3RARE~0 Red Double Lightsaber. Costs 2,000 Moon Rocks.",
						Price = 2000,
						Amount = 1,
						Items = new ShopItem[4]
						{
							new ShopItem
							{
								Index = 1225,
								Count = 1,
								Priority = 10
							},
							new ShopItem
							{
								Index = 1227,
								Count = 1,
								Priority = 110
							},
							new ShopItem
							{
								Index = 1229,
								Count = 1,
								Priority = 10
							},
							new ShopItem
							{
								Index = 1231,
								Count = 1,
								Priority = 50
							}
						}
					},
					new ShopListing
					{
						Image = 1217,
						AvailableFrom = Event.AvailableFrom,
						AvailableTo = Event.AvailableTo,
						Text1 = "~1" + Item.Name(1217),
						Text2 = "This Jetpack lets you fly in the air, leaving",
						Text3 = " a trail of smoke. Costs 10,000 Moon Rocks.",
						Price = 10000,
						Amount = 1,
						Items = new ShopItem[1]
						{
							new ShopItem
							{
								Index = 1217,
								Count = 1,
								Priority = 1
							}
						}
					}
				}
			}
		};

		public static bool ListingAvailable(ShopListing listing)
		{
			if (listing.AvailableFrom != DateTime.MinValue && listing.AvailableFrom > DateTime.UtcNow)
			{
				return false;
			}
			if (listing.AvailableTo != DateTime.MinValue && listing.AvailableTo < DateTime.UtcNow)
			{
				return false;
			}
			return true;
		}

		public static bool ListingAvailableLater(ShopListing listing)
		{
			if (listing.AvailableFrom != DateTime.MinValue && listing.AvailableFrom > DateTime.UtcNow)
			{
				return true;
			}
			return false;
		}

		public static int ListingAvailableAfter(ShopListing listing)
		{
			if (ListingAvailableLater(listing))
			{
				return (int)listing.AvailableFrom.Subtract(DateTime.UtcNow).TotalSeconds;
			}
			return 0;
		}

		public static bool ListingUnavailableLater(ShopListing listing)
		{
			if (listing.AvailableTo != DateTime.MinValue && listing.AvailableTo > DateTime.UtcNow)
			{
				return true;
			}
			return false;
		}

		public static int ListingUnavailableAfter(ShopListing listing)
		{
			if (ListingUnavailableLater(listing))
			{
				return (int)listing.AvailableTo.Subtract(DateTime.UtcNow).TotalSeconds;
			}
			return 0;
		}

		public static void WriteCategories(BinaryWriter writer, int index)
		{
			writer.Write(Convert.ToUInt16(Categories.Count));
			for (int i = 0; i < Categories.Count; i++)
			{
				ShopCategory shopCategory = Categories[i];
				writer.Write(Convert.ToUInt16(i));
				if (i == index)
				{
					writer.Write(Convert.ToBoolean(value: true));
				}
				else
				{
					writer.Write(Convert.ToBoolean(value: false));
				}
				writer.Write(Convert.ToUInt16(shopCategory.Image));
				writer.Write(Encoding.UTF8.GetBytes(shopCategory.Name + "\0"));
			}
		}

		public static void WriteListings(BinaryWriter writer, int index)
		{
			ShopCategory shopCategory = Categories[index];
			writer.Write(Convert.ToUInt16(shopCategory.Listings.Length));
			for (int i = 0; i < shopCategory.Listings.Length; i++)
			{
				ShopListing listing = shopCategory.Listings[i];
				if (ListingAvailableLater(listing))
				{
					writer.Write(Convert.ToUInt16(i));
					writer.Write(Convert.ToUInt16(listing.Image));
					writer.Write(Encoding.UTF8.GetBytes("~1Coming soon!\0"));
					if (listing.Items.Length == 1)
					{
						writer.Write(Encoding.UTF8.GetBytes($"This item will be available in {Text.Time(ListingAvailableAfter(listing))}" + "\0"));
						writer.Write(Encoding.UTF8.GetBytes("\0"));
					}
					else
					{
						writer.Write(Encoding.UTF8.GetBytes($"This pack will be available in {Text.Time(ListingAvailableAfter(listing))}" + "\0"));
						writer.Write(Encoding.UTF8.GetBytes("\0"));
					}
				}
				else if (ListingUnavailableLater(listing))
				{
					writer.Write(Convert.ToUInt16(i));
					writer.Write(Convert.ToUInt16(listing.Image));
					writer.Write(Encoding.UTF8.GetBytes(listing.Text1 + $" ({Text.Time(ListingUnavailableAfter(listing))} left)" + "\0"));
					writer.Write(Encoding.UTF8.GetBytes(listing.Text2 + "\0"));
					writer.Write(Encoding.UTF8.GetBytes(listing.Text3 + "\0"));
				}
				else if (!ListingAvailable(listing))
				{
					writer.Write(Convert.ToUInt16(i));
					writer.Write(Convert.ToUInt16(listing.Image));
					writer.Write(Encoding.UTF8.GetBytes("~1Item unavailable!\0"));
					if (listing.Items.Length == 1)
					{
						writer.Write(Encoding.UTF8.GetBytes("This item is not available anymore, nobody\0"));
						writer.Write(Encoding.UTF8.GetBytes("knows when it may return...\0"));
					}
					else
					{
						writer.Write(Encoding.UTF8.GetBytes("This pack is not available anymore, nobody\0"));
						writer.Write(Encoding.UTF8.GetBytes("knows when it may return...\0"));
					}
				}
				else
				{
					writer.Write(Convert.ToUInt16(i));
					writer.Write(Convert.ToUInt16(listing.Image));
					writer.Write(Encoding.UTF8.GetBytes(listing.Text1 + "\0"));
					writer.Write(Encoding.UTF8.GetBytes(listing.Text2 + "\0"));
					writer.Write(Encoding.UTF8.GetBytes(listing.Text3 + "\0"));
				}
			}
		}

		public static ShopItem[] Purchase(ShopListing listing)
		{
			if (listing.Amount == listing.Items.Length)
			{
				return listing.Items;
			}
			List<ShopItem> list = new List<ShopItem>();
			while (list.Count < listing.Amount)
			{
				ShopItem item = listing.Items[Server.Random.Next(listing.Items.Length)];
				if (Server.Random.Next(item.Priority) == 0)
				{
					list.Add(item);
				}
			}
			return list.ToArray();
		}
	}
}
