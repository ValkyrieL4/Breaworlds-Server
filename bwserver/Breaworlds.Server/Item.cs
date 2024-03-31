using System;
using System.Collections.Generic;

enum Ability {
	Default = 0,
	Growtime = 1,
	Wireable = 2,
	Blocked = 3,
	Jumpboost = 4,
	Collision = 5,
	Bait = 6, 
	Rod = 7, 
	Fish = 8
}

namespace Breaworlds.Server
{
	public class Item
	{
		public static List<ItemData> List = new List<ItemData>
		{
			new ItemData
			{
				ID = 0,
				Name = "Null",
				Info = "",
				Type = 0,
				Part = 0,
				Rarity = 0,
				Hardness = 0,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 1,
				Name = "Fist",
				Info = "",
				Type = 0,
				Part = 0,
				Rarity = 1,
				Hardness = 0,
				Farmability = 0,
				Tradeable = false,
				Trashable = false,
				Droppable = false,
				Lockable = false,
				Vendable = false,
				Solid = true
			},
			new ItemData
			{
				ID = 2,
				Name = "Fist Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 3,
				Name = "Wrench",
				Info = "",
				Type = 0,
				Part = 0,
				Rarity = 1,
				Hardness = 0,
				Farmability = 0,
				Tradeable = false,
				Trashable = false,
				Droppable = false,
				Lockable = false,
				Vendable = false,
				Solid = true
			},
			new ItemData
			{
				ID = 4,
				Name = "Wrench Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 5,
				Name = "Bedrock Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = false,
				Vendable = false,
				Solid = true
			},
			new ItemData
			{
				ID = 6,
				Name = "Bedrock Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 7,
				Name = "World Entrance",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = false,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 8,
				Name = "World Entrance Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 9,
				Name = "Dirt Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 3,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 10,
				Name = "Dirt Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 11,
				Name = "Lava Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 3,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 12,
				Name = "Lava Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 13,
				Name = "Stone Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 6,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 14,
				Name = "Stone Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 15,
				Name = "Cave Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 1,
				Hardness = 3,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 16,
				Name = "Cave Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 17,
				Name = "Wooden Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 2,
				Hardness = 4,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 18,
				Name = "Wooden Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 2,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 19,
				Name = "Stone Brick",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 2,
				Hardness = 4,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 20,
				Name = "Stone Brick Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 2,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 21,
				Name = "Glass Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 2,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 22,
				Name = "Glass Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 2,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 23,
				Name = "Black Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 3,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 24,
				Name = "Black Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 3,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 25,
				Name = "White Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 4,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 26,
				Name = "White Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 4,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 27,
				Name = "Red Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 3,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 28,
				Name = "Red Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 3,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 29,
				Name = "Brown Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 3,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 30,
				Name = "Brown Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 3,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 31,
				Name = "Grey Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 3,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 32,
				Name = "Grey Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 3,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 33,
				Name = "Yellow Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 7,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 34,
				Name = "Yellow Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 7,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 35,
				Name = "Orange Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 10,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 36,
				Name = "Orange Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 10,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 37,
				Name = "Green Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 10,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 38,
				Name = "Green Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 10,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 39,
				Name = "Blue Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 17,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 40,
				Name = "Blue Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 17,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 41,
				Name = "Purple Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 20,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 42,
				Name = "Purple Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 20,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 43,
				Name = "Pink Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 21,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 44,
				Name = "Pink Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 21,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 45,
				Name = "Black Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 4,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 46,
				Name = "Black Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 4,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 47,
				Name = "White Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 5,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 48,
				Name = "White Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 5,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 49,
				Name = "Red Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 4,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 50,
				Name = "Red Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 4,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 51,
				Name = "Brown Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 4,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 52,
				Name = "Brown Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 4,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 53,
				Name = "Grey Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 4,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 54,
				Name = "Grey Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 4,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 55,
				Name = "Yellow Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 8,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 56,
				Name = "Yellow Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 8,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 57,
				Name = "Orange Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 11,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 58,
				Name = "Orange Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 11,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 59,
				Name = "Green Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 11,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 60,
				Name = "Green Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 11,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 61,
				Name = "Blue Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 18,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 62,
				Name = "Blue Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 18,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 63,
				Name = "Purple Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 21,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 64,
				Name = "Purple Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 21,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 65,
				Name = "Pink Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 22,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 66,
				Name = "Pink Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 22,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 67,
				Name = "Wooden Platform",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 6,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 68,
				Name = "Wooden Platform Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 6,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 69,
				Name = "Wooden Sign",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 5,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 70,
				Name = "Wooden Sign Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 5,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 71,
				Name = "Wooden Door",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 12,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 72,
				Name = "Wooden Door Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 12,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 73,
				Name = "World Lock",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 20,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = false,
				Vendable = false,
				Solid = true
			},
			new ItemData
			{
				ID = 74,
				Name = "World Lock Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 75,
				Name = "Black Shirt",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 76,
				Name = "Black Shirt Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 77,
				Name = "Black Pants",
				Info = "",
				Type = 4,
				Part = 6,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 78,
				Name = "Black Pants Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 79,
				Name = "Black Shoes",
				Info = "",
				Type = 4,
				Part = 8,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 80,
				Name = "Black Shoes Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 81,
				Name = "Blue Shirt",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 82,
				Name = "Blue Shirt Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 83,
				Name = "Blue Pants",
				Info = "",
				Type = 4,
				Part = 6,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 84,
				Name = "Blue Pants Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 85,
				Name = "Blue Shoes",
				Info = "",
				Type = 4,
				Part = 8,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 86,
				Name = "Blue Shoes Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 87,
				Name = "Green Shirt",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 88,
				Name = "Green Shirt Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 89,
				Name = "Green Pants",
				Info = "",
				Type = 4,
				Part = 6,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 90,
				Name = "Green Pants Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 91,
				Name = "Green Shoes",
				Info = "",
				Type = 4,
				Part = 8,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 92,
				Name = "Green Shoes Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 93,
				Name = "Red Shirt",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 94,
				Name = "Red Shirt Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 95,
				Name = "Red Pants",
				Info = "",
				Type = 4,
				Part = 6,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 96,
				Name = "Red Pants Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 97,
				Name = "Red Shoes",
				Info = "",
				Type = 4,
				Part = 8,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 98,
				Name = "Red Shoes Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 99,
				Name = "White Shirt",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 100,
				Name = "White Shirt Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 101,
				Name = "White Pants",
				Info = "",
				Type = 4,
				Part = 6,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 102,
				Name = "White Pants Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 103,
				Name = "White Shoes",
				Info = "",
				Type = 4,
				Part = 8,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 104,
				Name = "White Shoes Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 105,
				Name = "Yellow Shirt",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 106,
				Name = "Yellow Shirt Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 107,
				Name = "Yellow Pants",
				Info = "",
				Type = 4,
				Part = 6,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 108,
				Name = "Yellow Pants Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 109,
				Name = "Yellow Shoes",
				Info = "",
				Type = 4,
				Part = 8,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 110,
				Name = "Yellow Shoes Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 111,
				Name = "Dark Sweater",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 112,
				Name = "Dark Sweater Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 113,
				Name = "Dark Spiky Hair",
				Info = "",
				Type = 4,
				Part = 2,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 114,
				Name = "Dark Spiky Hair Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 115,
				Name = "Messy Brown Hair",
				Info = "",
				Type = 4,
				Part = 2,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 116,
				Name = "Messy Brown Hair Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 117,
				Name = "Short Blonde Hair",
				Info = "",
				Type = 4,
				Part = 2,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 118,
				Name = "Short Blonde Hair Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 119,
				Name = "Long Brown Hair",
				Info = "",
				Type = 4,
				Part = 2,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 120,
				Name = "Long Brown Hair Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 121,
				Name = "Fedora",
				Info = "",
				Type = 4,
				Part = 3,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 122,
				Name = "Fedora Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 123,
				Name = "Golden Crown",
				Info = "",
				Type = 4,
				Part = 3,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 124,
				Name = "Golden Crown Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 125,
				Name = "Pineapple Hat",
				Info = "",
				Type = 4,
				Part = 3,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 126,
				Name = "Pineapple Hat Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 127,
				Name = "Black Cap",
				Info = "",
				Type = 4,
				Part = 3,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 128,
				Name = "Black Cap Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 129,
				Name = "Blue Cap",
				Info = "",
				Type = 4,
				Part = 3,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 130,
				Name = "Blue Cap Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 131,
				Name = "Green Cap",
				Info = "",
				Type = 4,
				Part = 3,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 132,
				Name = "Green Cap Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 133,
				Name = "Red Cap",
				Info = "",
				Type = 4,
				Part = 3,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 134,
				Name = "Red Cap Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 135,
				Name = "White Cap",
				Info = "",
				Type = 4,
				Part = 3,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 136,
				Name = "White Cap Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 137,
				Name = "Yellow Cap",
				Info = "",
				Type = 4,
				Part = 3,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 138,
				Name = "Yellow Cap Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 139,
				Name = "Blank Cape",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 140,
				Name = "Blank Cape Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 141,
				Name = "Golden Cape",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 142,
				Name = "Golden Cape Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 143,
				Name = "Diamond Cape",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 144,
				Name = "Diamond Cape Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 145,
				Name = "Wooden Pickaxe",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 3,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 146,
				Name = "Wooden Pickaxe Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 3,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 147,
				Name = "Forest Theme",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 148,
				Name = "Forest Theme Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 149,
				Name = "Night Theme",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 150,
				Name = "Night Theme Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 151,
				Name = "Darkness Theme",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 152,
				Name = "Darkness Theme Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 153,
				Name = "Sand Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 4,
				Farmability = 2,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 154,
				Name = "Sand Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 2,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 155,
				Name = "Furnace",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 156,
				Name = "Furnace Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 157,
				Name = "Gem Machine",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 158,
				Name = "Gem Machine Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 159,
				Name = "Low Density Polyethylene",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 160,
				Name = "Low Density Polyethylene Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 161,
				Name = "High Density Polyethylene",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 162,
				Name = "High Density Polyethylene Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 163,
				Name = "Plastic Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 4,
				Farmability = 2,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 164,
				Name = "Plastic Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 2,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 165,
				Name = "Golden Block Fragment",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 166,
				Name = "Golden Block Fragment Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 167,
				Name = "Diamond Block Fragment",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 168,
				Name = "Diamond Block Fragment Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 169,
				Name = "Emerald Block Fragment",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 170,
				Name = "Emerald Block Fragment Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 171,
				Name = "Ruby Block Fragment",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 172,
				Name = "Ruby Block Fragment Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 173,
				Name = "Golden Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 28,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 174,
				Name = "Golden Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 28,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 175,
				Name = "Diamond Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 28,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 176,
				Name = "Diamond Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 28,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 177,
				Name = "Emerald Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 28,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 178,
				Name = "Emerald Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 28,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 179,
				Name = "Ruby Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 28,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 180,
				Name = "Ruby Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 28,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 181,
				Name = "Water",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 182,
				Name = "Water Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 183,
				Name = "Quicksand",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 2,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 184,
				Name = "Quicksand Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 2,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 185,
				Name = "Mud Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 2,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 186,
				Name = "Mud Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 2,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 187,
				Name = "Evil Wings",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 188,
				Name = "Evil Wings Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 189,
				Name = "Glowy Wings",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 190,
				Name = "Glowy Wings Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 191,
				Name = "Rainbow Skin",
				Info = "",
				Type = 4,
				Part = 9,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 192,
				Name = "Rainbow Skin Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 193,
				Name = "Rusty Wings",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 194,
				Name = "Rusty Wings Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 195,
				Name = "Wooden Entrance",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 14,
				Hardness = 3,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 196,
				Name = "Wooden Entrance Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 14,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 197,
				Name = "Iron Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 4,
				Hardness = 3,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 198,
				Name = "Iron Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 4,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 199,
				Name = "Iron Platform",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 10,
				Hardness = 3,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 200,
				Name = "Iron Platform Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 10,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 201,
				Name = "Iron Sign",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 9,
				Hardness = 3,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 202,
				Name = "Iron Sign Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 9,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 203,
				Name = "Iron Door",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 16,
				Hardness = 3,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 204,
				Name = "Iron Door Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 16,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 205,
				Name = "Iron Entrance",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 18,
				Hardness = 3,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 206,
				Name = "Iron Entrance Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 18,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 207,
				Name = "Red Portal",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 19,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 208,
				Name = "Red Portal Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 19,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 209,
				Name = "Green Portal",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 26,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 210,
				Name = "Green Portal Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 26,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 211,
				Name = "Blue Portal",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 33,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 212,
				Name = "Blue Portal Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 33,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 213,
				Name = "Yellow Portal",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 23,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 214,
				Name = "Yellow Portal Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 23,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 215,
				Name = "Wooden Window",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 4,
				Hardness = 3,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 216,
				Name = "Wooden Window Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 4,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 217,
				Name = "Plastic Window",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 5,
				Hardness = 3,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 218,
				Name = "Plastic Window Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 5,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 219,
				Name = "Iron Window",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 9,
				Hardness = 3,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 220,
				Name = "Iron Window Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 9,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 221,
				Name = "Reward Box 1",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 3,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 222,
				Name = "Reward Box 1 Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 223,
				Name = "Katana",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 224,
				Name = "Katana Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 225,
				Name = "Stone Hammer",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 226,
				Name = "Stone Hammer Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 227,
				Name = "Desert Theme",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 228,
				Name = "Desert Theme Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 229,
				Name = "Pet Water Drop",
				Info = "",
				Type = 4,
				Part = 10,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 230,
				Name = "Pet Water Drop Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 231,
				Name = "Pet Fire",
				Info = "",
				Type = 4,
				Part = 10,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 232,
				Name = "Pet Fire Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 233,
				Name = "Pet Mini Earth",
				Info = "",
				Type = 4,
				Part = 10,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 234,
				Name = "Pet Mini Earth Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 235,
				Name = "Pet Robot",
				Info = "",
				Type = 4,
				Part = 10,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 236,
				Name = "Pet Robot Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 237,
				Name = "Reward Box 2",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 3,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 238,
				Name = "Reward Box 2 Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 239,
				Name = "Plastic Wings",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 240,
				Name = "Plastic Wings Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 241,
				Name = "Golden Plastic Wings",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 242,
				Name = "Golden Plastic Wings Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 243,
				Name = "Rainbow Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 244,
				Name = "Rainbow Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 245,
				Name = "Wooden Table",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 16,
				Hardness = 4,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 246,
				Name = "Wooden Table Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 16,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 247,
				Name = "Wooden Chair",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 7,
				Hardness = 4,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 248,
				Name = "Wooden Chair Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 7,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 249,
				Name = "Wooden Ladder",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 8,
				Hardness = 4,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 250,
				Name = "Wooden Ladder Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 8,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 251,
				Name = "Iron Ladder",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 12,
				Hardness = 4,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 252,
				Name = "Iron Ladder Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 12,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 253,
				Name = "Ceiling Lamp",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 6,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 254,
				Name = "Ceiling Lamp Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 6,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 255,
				Name = "Aquarium",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 3,
				Hardness = 4,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 256,
				Name = "Aquarium Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 3,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 257,
				Name = "Non-Player Character",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 4,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = true
			},
			new ItemData
			{
				ID = 258,
				Name = "Non-Player Character Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 259,
				Name = "Pet Ice Cream",
				Info = "",
				Type = 4,
				Part = 10,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 260,
				Name = "Pet Ice Cream Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 261,
				Name = "Red Parrot Wings",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 262,
				Name = "Red Parrot Wings Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 263,
				Name = "Spike Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 12,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 264,
				Name = "Spike Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 12,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 265,
				Name = "Red Jelly Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 4,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 266,
				Name = "Red Jelly Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 4,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 267,
				Name = "Green Jelly Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 11,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 268,
				Name = "Green Jelly Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 11,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 269,
				Name = "Blue Jelly Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 18,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 270,
				Name = "Blue Jelly Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 18,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 271,
				Name = "Yellow Jelly Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 8,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 272,
				Name = "Yellow Jelly Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 8,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 273,
				Name = "World Key",
				Info = "",
				Type = 0,
				Part = 0,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = true
			},
			new ItemData
			{
				ID = 274,
				Name = "World Key Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 275,
				Name = "Red Car",
				Info = "",
				Type = 4,
				Part = 11,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 276,
				Name = "Red Car Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 277,
				Name = "Green Car",
				Info = "",
				Type = 4,
				Part = 11,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 278,
				Name = "Green Car Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 279,
				Name = "Blue Car",
				Info = "",
				Type = 4,
				Part = 11,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 280,
				Name = "Blue Car Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 281,
				Name = "Yellow Car",
				Info = "",
				Type = 4,
				Part = 11,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 282,
				Name = "Yellow Car Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 283,
				Name = "Quest Token",
				Info = "",
				Type = 0,
				Part = 0,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = true
			},
			new ItemData
			{
				ID = 284,
				Name = "Quest Token Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 285,
				Name = "Death Axe",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 286,
				Name = "Death Axe Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 287,
				Name = "Golden Pickaxe",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 288,
				Name = "Golden Pickaxe Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 289,
				Name = "Watering Can",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 5,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 290,
				Name = "Watering Can Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 5,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 291,
				Name = "Red Laser Sword",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 29,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 292,
				Name = "Red Laser Sword Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 29,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 293,
				Name = "Green Laser Sword",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 29,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 294,
				Name = "Green Laser Sword Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 29,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 295,
				Name = "Blue Laser Sword",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 29,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 296,
				Name = "Blue Laser Sword Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 29,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 297,
				Name = "Yellow Laser Sword",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 29,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 298,
				Name = "Yellow Laser Sword Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 29,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 299,
				Name = "Red Glass Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 5,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 300,
				Name = "Red Glass Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 5,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 301,
				Name = "Green Glass Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 12,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 302,
				Name = "Green Glass Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 12,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 303,
				Name = "Blue Glass Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 19,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 304,
				Name = "Blue Glass Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 19,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 305,
				Name = "Yellow Glass Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 9,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 306,
				Name = "Yellow Glass Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 9,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 307,
				Name = "Mail Box",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 25,
				Hardness = 5,
				Farmability = 2,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 308,
				Name = "Mail Box Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 25,
				Hardness = 10,
				Farmability = 2,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 309,
				Name = "Donation Box",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 23,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 310,
				Name = "Donation Box Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 23,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 311,
				Name = "Bulletin Board",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 27,
				Hardness = 5,
				Farmability = 2,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 312,
				Name = "Bulletin Board Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 27,
				Hardness = 10,
				Farmability = 2,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 313,
				Name = "Vending Machine",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 314,
				Name = "Vending Machine Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 315,
				Name = "Marble Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 8,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 316,
				Name = "Marble Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 8,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 317,
				Name = "Granite Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 7,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 318,
				Name = "Granite Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 7,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 319,
				Name = "Small Lock",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 20,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 320,
				Name = "Small Lock Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 321,
				Name = "Medium Lock",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 20,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 322,
				Name = "Medium Lock Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 323,
				Name = "Big Lock",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 20,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 324,
				Name = "Big Lock Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 325,
				Name = "Wooden Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 3,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 326,
				Name = "Wooden Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 3,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 327,
				Name = "Straw Hat",
				Info = "",
				Type = 4,
				Part = 3,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 328,
				Name = "Straw Hat Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 329,
				Name = "Octopus Hat",
				Info = "",
				Type = 4,
				Part = 3,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 330,
				Name = "Octopus Hat Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 331,
				Name = "Octopus Wings",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 332,
				Name = "Octopus Wings Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 333,
				Name = "Red Visor",
				Info = "",
				Type = 4,
				Part = 4,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 334,
				Name = "Red Visor Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 335,
				Name = "Green Visor",
				Info = "",
				Type = 4,
				Part = 4,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 336,
				Name = "Green Visor Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 337,
				Name = "Blue Visor",
				Info = "",
				Type = 4,
				Part = 4,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 338,
				Name = "Blue Visor Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 339,
				Name = "Yellow Visor",
				Info = "",
				Type = 4,
				Part = 4,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 340,
				Name = "Yellow Visor Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 341,
				Name = "Black Metal Panel",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 7,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 342,
				Name = "Black Metal Panel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 7,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 343,
				Name = "White Metal Panel",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 8,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 344,
				Name = "White Metal Panel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 8,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 345,
				Name = "Red Metal Panel",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 7,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 346,
				Name = "Red Metal Panel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 7,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 347,
				Name = "Brown Metal Panel",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 7,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 348,
				Name = "Brown Metal Panel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 7,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 349,
				Name = "Grey Metal Panel",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 7,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 350,
				Name = "Grey Metal Panel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 7,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 351,
				Name = "Yellow Metal Panel",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 11,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 352,
				Name = "Yellow Metal Panel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 11,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 353,
				Name = "Orange Metal Panel",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 14,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 354,
				Name = "Orange Metal Panel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 14,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 355,
				Name = "Green Metal Panel",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 14,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 356,
				Name = "Green Metal Panel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 14,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 357,
				Name = "Blue Metal Panel",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 21,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 358,
				Name = "Blue Metal Panel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 21,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 359,
				Name = "Purple Metal Panel",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 24,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 360,
				Name = "Purple Metal Panel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 24,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 361,
				Name = "Pink Metal Panel",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 25,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 362,
				Name = "Pink Metal Panel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 25,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 363,
				Name = "Brown Ponytail",
				Info = "",
				Type = 4,
				Part = 2,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 364,
				Name = "Brown Ponytail Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 365,
				Name = "Blonde Ponytail",
				Info = "",
				Type = 4,
				Part = 2,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 366,
				Name = "Blonde Ponytail Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 367,
				Name = "Frost Cape",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 368,
				Name = "Frost Cape Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 369,
				Name = "Vampire Cape",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 370,
				Name = "Vampire Cape Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 371,
				Name = "Golden Rusty Wings",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 372,
				Name = "Golden Rusty Wings Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 373,
				Name = "Emerald Cape",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 374,
				Name = "Emerald Cape Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 375,
				Name = "Ruby Cape",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 376,
				Name = "Ruby Cape Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 377,
				Name = "Grass",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 2,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 378,
				Name = "Grass Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 2,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 379,
				Name = "Bush",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 4,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 380,
				Name = "Bush Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 4,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 381,
				Name = "Mushroom",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 3,
				Hardness = 4,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 382,
				Name = "Mushroom Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 3,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 383,
				Name = "Fairy Wings",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 384,
				Name = "Fairy Wings Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 385,
				Name = "Raven Wings",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 386,
				Name = "Raven Wings Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 387,
				Name = "Pet Cloud",
				Info = "",
				Type = 4,
				Part = 10,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 388,
				Name = "Pet Cloud Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 389,
				Name = "Trampoline",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 9,
				Hardness = 4,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 390,
				Name = "Trampoline Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 9,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 391,
				Name = "Diamond Plastic Wings",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 392,
				Name = "Diamond Plastic Wings Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 393,
				Name = "Emerald Plastic Wings",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 394,
				Name = "Emerald Plastic Wings Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 395,
				Name = "Ruby Plastic Wings",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 396,
				Name = "Ruby Plastic Wings Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 397,
				Name = "Black Short Shirt",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 398,
				Name = "Black Short Shirt Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 399,
				Name = "Blue Short Shirt",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 400,
				Name = "Blue Short Shirt Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 401,
				Name = "Green Short Shirt",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 402,
				Name = "Green Short Shirt Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 403,
				Name = "Red Short Shirt",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 404,
				Name = "Red Short Shirt Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 405,
				Name = "White Short Shirt",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 406,
				Name = "White Short Shirt Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 407,
				Name = "Yellow Short Shirt",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 408,
				Name = "Yellow Short Shirt Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 409,
				Name = "Red Wool Scarf",
				Info = "",
				Type = 4,
				Part = 12,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 410,
				Name = "Red Wool Scarf Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 411,
				Name = "Green Wool Scarf",
				Info = "",
				Type = 4,
				Part = 12,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 412,
				Name = "Green Wool Scarf Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 413,
				Name = "Blue Wool Scarf",
				Info = "",
				Type = 4,
				Part = 12,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 414,
				Name = "Blue Wool Scarf Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 415,
				Name = "Yellow Wool Scarf",
				Info = "",
				Type = 4,
				Part = 12,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 416,
				Name = "Yellow Wool Scarf Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 417,
				Name = "Pillar",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 13,
				Hardness = 4,
				Farmability = 2,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 418,
				Name = "Pillar Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 13,
				Hardness = 10,
				Farmability = 2,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 419,
				Name = "Traffic Light",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 15,
				Hardness = 4,
				Farmability = 2,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 420,
				Name = "Traffic Light Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 15,
				Hardness = 10,
				Farmability = 2,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 421,
				Name = "Police Hat",
				Info = "",
				Type = 4,
				Part = 3,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 422,
				Name = "Police Hat Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 423,
				Name = "Navy Hat",
				Info = "",
				Type = 4,
				Part = 3,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 424,
				Name = "Navy Hat Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 425,
				Name = "Chef Hat",
				Info = "",
				Type = 4,
				Part = 3,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 426,
				Name = "Chef Hat Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 427,
				Name = "Dice Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 9,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 428,
				Name = "Dice Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 9,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 429,
				Name = "Checkpoint",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 42,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 430,
				Name = "Checkpoint Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 42,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 431,
				Name = "Event Ticket",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 432,
				Name = "Event Ticket Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 433,
				Name = "Reward Box 3",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 3,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 434,
				Name = "Reward Box 3 Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 435,
				Name = "Pumpkin Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 436,
				Name = "Pumpkin Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 437,
				Name = "Gravestone",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 438,
				Name = "Gravestone Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 439,
				Name = "Electric Pole",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 440,
				Name = "Electric Pole Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 441,
				Name = "Signal Button",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 442,
				Name = "Signal Button Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 443,
				Name = "Gothic Lamp",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 444,
				Name = "Gothic Lamp Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 445,
				Name = "Gothic Fence",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 446,
				Name = "Gothic Fence Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 447,
				Name = "Bat Wings",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 448,
				Name = "Bat Wings Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 449,
				Name = "Pet Bat",
				Info = "",
				Type = 4,
				Part = 10,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 450,
				Name = "Pet Bat Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 451,
				Name = "Pumpkin Mask",
				Info = "",
				Type = 4,
				Part = 3,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 452,
				Name = "Pumpkin Mask Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 453,
				Name = "Witch Hat",
				Info = "",
				Type = 4,
				Part = 3,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 454,
				Name = "Witch Hat Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 455,
				Name = "Dracula Cape",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 456,
				Name = "Dracula Cape Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 457,
				Name = "Dracula Skin",
				Info = "",
				Type = 4,
				Part = 9,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 458,
				Name = "Dracula Skin Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 459,
				Name = "Skeleton",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 460,
				Name = "Skeleton Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 461,
				Name = "Skull Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 462,
				Name = "Skull Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 463,
				Name = "Winter Theme",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 464,
				Name = "Winter Theme Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 465,
				Name = "Dark Blue Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 30,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 466,
				Name = "Dark Blue Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 30,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 467,
				Name = "Dark Brown Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 16,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 468,
				Name = "Dark Brown Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 16,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 469,
				Name = "Dark Green Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 23,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 470,
				Name = "Dark Green Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 23,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 471,
				Name = "Dark Grey Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 16,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 472,
				Name = "Dark Grey Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 16,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 473,
				Name = "Dark Orange Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 23,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 474,
				Name = "Dark Orange Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 23,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 475,
				Name = "Dark Purple Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 33,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 476,
				Name = "Dark Purple Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 33,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 477,
				Name = "Dark Red Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 16,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 478,
				Name = "Dark Red Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 16,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 479,
				Name = "Dark Yellow Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 20,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 480,
				Name = "Dark Yellow Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 20,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 481,
				Name = "Music Begin",
				Info = "This is used to mark where music begins.",
				Type = 2,
				Part = 0,
				Rarity = 16,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 482,
				Name = "Music Begin Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 16,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 483,
				Name = "Music End",
				Info = "This is used to mark where music ends.",
				Type = 2,
				Part = 0,
				Rarity = 16,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 484,
				Name = "Music End Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 16,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 485,
				Name = "Music - Piano",
				Info = "This is used to play piano sounds.",
				Type = 2,
				Part = 0,
				Rarity = 16,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 486,
				Name = "Music - Piano Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 16,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 487,
				Name = "Music - Bass",
				Info = "This is used to play bass sounds.",
				Type = 2,
				Part = 0,
				Rarity = 16,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 488,
				Name = "Music - Bass Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 16,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 489,
				Name = "Music - Drums",
				Info = "This is used to play drum sounds.",
				Type = 2,
				Part = 0,
				Rarity = 16,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 490,
				Name = "Music - Drums Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 16,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 491,
				Name = "Music - Hard Bass",
				Info = "This is used to play drum sounds.",
				Type = 2,
				Part = 0,
				Rarity = 16,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 492,
				Name = "Music - Hard Bass Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 16,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 493,
				Name = "Titanium Lock",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 20,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = false,
				Vendable = false,
				Solid = true
			},
			new ItemData
			{
				ID = 494,
				Name = "Titanium Lock Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 495,
				Name = "Anti Punch",
				Info = "Disables punching others completely in the world.",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 496,
				Name = "Anti Punch Seed",
				Info = "",
				Type = 0,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 497,
				Name = "Anti Talk",
				Info = "Disables talking for world visitors.",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 498,
				Name = "Anti Talk Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 499,
				Name = "Anti Drop",
				Info = "Disables dropping for world visitors.",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 500,
				Name = "Anti Drop Seed",
				Info = "",
				Type = 0,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 501,
				Name = "Snow Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 3,
				Farmability = 2,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 502,
				Name = "Snow Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 2,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 503,
				Name = "Ice Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 2,
				Hardness = 7,
				Farmability = 2,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 504,
				Name = "Ice Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 2,
				Hardness = 10,
				Farmability = 2,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 505,
				Name = "Candy Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 6,
				Hardness = 7,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 506,
				Name = "Candy Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 6,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 507,
				Name = "Candy Platform",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 16,
				Hardness = 3,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 508,
				Name = "Candy Platform Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 16,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 509,
				Name = "Candy Sign",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 15,
				Hardness = 3,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 510,
				Name = "Candy Sign Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 15,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 511,
				Name = "Candy Door",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 22,
				Hardness = 3,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 512,
				Name = "Candy Door Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 22,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 513,
				Name = "Frost Hair",
				Info = "",
				Type = 4,
				Part = 2,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 514,
				Name = "Frost Hair Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 515,
				Name = "Santa's Coat",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 516,
				Name = "Santa's Coat Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 517,
				Name = "Santa's Pants",
				Info = "",
				Type = 4,
				Part = 6,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 518,
				Name = "Santa's Pants Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 519,
				Name = "Santa's Boots",
				Info = "",
				Type = 4,
				Part = 8,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 520,
				Name = "Santa's Boots Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 521,
				Name = "Santa's Hat",
				Info = "",
				Type = 4,
				Part = 3,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 522,
				Name = "Santa's Hat Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 523,
				Name = "Santa's Beard",
				Info = "",
				Type = 4,
				Part = 13,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 524,
				Name = "Santa's Beard Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 525,
				Name = "Nightmare Scythe",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 526,
				Name = "Nightmare Scythe Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 527,
				Name = "Pet Reindeer",
				Info = "",
				Type = 4,
				Part = 10,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 528,
				Name = "Pet Reindeer Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 529,
				Name = "Pet Snowball",
				Info = "",
				Type = 4,
				Part = 10,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 530,
				Name = "Pet Snowball Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 531,
				Name = "White Wool Scarf",
				Info = "",
				Type = 4,
				Part = 12,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 532,
				Name = "White Wool Scarf Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 533,
				Name = "Black Wool Scarf",
				Info = "",
				Type = 4,
				Part = 12,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 534,
				Name = "Black Wool Scarf Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 535,
				Name = "Display Box",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 7,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 536,
				Name = "Display Box Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 537,
				Name = "Red Firework",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 0,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 538,
				Name = "Red Firework Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 539,
				Name = "Green Firework",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 0,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 540,
				Name = "Green Firework Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 541,
				Name = "Blue Firework",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 0,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 542,
				Name = "Blue Firework Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 543,
				Name = "Nightmare Wings",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 4,
				Farmability = 1,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = true
			},
			new ItemData
			{
				ID = 544,
				Name = "Nightmare Wings Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 545,
				Name = "Wooden Fishing Rod",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 546,
				Name = "Wooden Fishing Rod Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 547,
				Name = "Fishing Bait - Worm",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 0,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 548,
				Name = "Fishing Bait - Worm Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 549,
				Name = "Fishing Bait - Bread",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 0,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 550,
				Name = "Fishing Bait - Bread Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 551,
				Name = "Fishing Bait - Cereal",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 0,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 552,
				Name = "Fishing Bait - Cereal Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 553,
				Name = "Fishing Bait - Shrimp",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 0,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 554,
				Name = "Fishing Bait - Shrimp Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 555,
				Name = "Fishing Bait - Cricket",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 0,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 556,
				Name = "Fishing Bait - Cricket Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 557,
				Name = "Smokehouse",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 7,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 558,
				Name = "Smokehouse Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 559,
				Name = "Bait Box",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 7,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 560,
				Name = "Bait Box Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 561,
				Name = "Salmon",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 0,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 562,
				Name = "Salmon Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 563,
				Name = "Carp",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 0,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 564,
				Name = "Carp Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 565,
				Name = "Goldfish",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 0,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 566,
				Name = "Goldfish Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 567,
				Name = "Trout",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 0,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 568,
				Name = "Trout Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 569,
				Name = "Catfish",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 0,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 570,
				Name = "Catfish Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 571,
				Name = "Shark",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 0,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 572,
				Name = "Shark Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 573,
				Name = "Golden Fishing Rod",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 574,
				Name = "Golden Fishing Rod Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 575,
				Name = "Striped Red Shirt",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 576,
				Name = "Striped Red Shirt Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 577,
				Name = "Striped Green Shirt",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 578,
				Name = "Striped Green Shirt Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 579,
				Name = "Striped Blue Shirt",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 580,
				Name = "Striped Blue Shirt Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 581,
				Name = "Striped Yellow Shirt",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 582,
				Name = "Striped Yellow Shirt Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 583,
				Name = "Golden Nightmare Scythe",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 584,
				Name = "Golden Nightmare Scythe Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 585,
				Name = "Saint Valentine's Cape",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 586,
				Name = "Saint Valentine's Cape Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 587,
				Name = "Saint Valentine's Crown",
				Info = "",
				Type = 4,
				Part = 3,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 588,
				Name = "Saint Valentine's Crown Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 589,
				Name = "Saint Valentine's Staff",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 590,
				Name = "Saint Valentine's Staff Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 591,
				Name = "Pet Heart",
				Info = "",
				Type = 4,
				Part = 10,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 592,
				Name = "Pet Heart Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 593,
				Name = "Heart Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 2,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 594,
				Name = "Heart Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 2,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 595,
				Name = "Golden Heart Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 100,
				Hardness = 10,
				Farmability = 2,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 596,
				Name = "Golden Heart Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 100,
				Hardness = 10,
				Farmability = 2,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 597,
				Name = "Game Block: Join",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 999,
				Hardness = 10,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 598,
				Name = "Game Block: Join Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 999,
				Hardness = 10,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 599,
				Name = "Game Block: Spawn",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 999,
				Hardness = 10,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 600,
				Name = "Game Block: Spawn Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 999,
				Hardness = 10,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 601,
				Name = "Game Block: Finish",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 999,
				Hardness = 10,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 602,
				Name = "Game Block: Finish Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 999,
				Hardness = 10,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 603,
				Name = "Solid Maze Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 16,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 604,
				Name = "Solid Maze Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 16,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 605,
				Name = "Non-Solid Maze Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 19,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 606,
				Name = "Non-Solid Maze Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 19,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 607,
				Name = "Iron Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 7,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 608,
				Name = "Iron Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 7,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 609,
				Name = "Golden Watering Can",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 500,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 610,
				Name = "Golden Watering Can Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 500,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 611,
				Name = "Atomic Bomb",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 500,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 612,
				Name = "Atomic Bomb Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 500,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 613,
				Name = "Black Plastic Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 4,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 614,
				Name = "Black Plastic Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 4,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 615,
				Name = "White Plastic Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 5,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 616,
				Name = "White Plastic Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 5,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 617,
				Name = "Red Plastic Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 4,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 618,
				Name = "Red Plastic Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 4,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 619,
				Name = "Brown Plastic Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 4,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 620,
				Name = "Brown Plastic Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 4,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 621,
				Name = "Grey Plastic Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 4,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 622,
				Name = "Grey Plastic Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 4,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 623,
				Name = "Yellow Plastic Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 8,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 624,
				Name = "Yellow Plastic Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 8,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 625,
				Name = "Orange Plastic Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 11,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 626,
				Name = "Orange Plastic Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 11,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 627,
				Name = "Green Plastic Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 11,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 628,
				Name = "Green Plastic Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 11,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 629,
				Name = "Blue Plastic Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 18,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 630,
				Name = "Blue Plastic Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 18,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 631,
				Name = "Purple Plastic Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 21,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 632,
				Name = "Purple Plastic Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 21,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 633,
				Name = "Pink Plastic Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 22,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 634,
				Name = "Pink Plastic Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 22,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 635,
				Name = "Black Brick",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 5,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 636,
				Name = "Black Brick Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 5,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 637,
				Name = "White Brick",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 6,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 638,
				Name = "White Brick Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 6,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 639,
				Name = "Red Brick",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 5,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 640,
				Name = "Red Brick Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 5,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 641,
				Name = "Brown Brick",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 5,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 642,
				Name = "Brown Brick Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 5,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 643,
				Name = "Grey Brick",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 5,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 644,
				Name = "Grey Brick Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 5,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 645,
				Name = "Yellow Brick",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 9,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 646,
				Name = "Yellow Brick Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 9,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 647,
				Name = "Orange Brick",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 12,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 648,
				Name = "Orange Brick Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 12,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 649,
				Name = "Green Brick",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 12,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 650,
				Name = "Green Brick Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 12,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 651,
				Name = "Blue Brick",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 19,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 652,
				Name = "Blue Brick Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 19,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 653,
				Name = "Purple Brick",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 22,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 654,
				Name = "Purple Brick Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 22,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 655,
				Name = "Pink Brick",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 23,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 656,
				Name = "Pink Brick Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 23,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 657,
				Name = "Leprechaun's Hat",
				Info = "",
				Type = 4,
				Part = 3,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 658,
				Name = "Leprechaun's Hat Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 659,
				Name = "Leprechaun's Coat",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 660,
				Name = "Leprechaun's Coat Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 661,
				Name = "Leprechaun's Pants",
				Info = "",
				Type = 4,
				Part = 6,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 662,
				Name = "Leprechaun's Pants Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 663,
				Name = "Leprechaun's Shoes",
				Info = "",
				Type = 4,
				Part = 8,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 664,
				Name = "Leprechaun's Shoes Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 665,
				Name = "Leprechaun's Beard",
				Info = "",
				Type = 4,
				Part = 13,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 666,
				Name = "Leprechaun's Beard Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 667,
				Name = "Clover Wings",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 668,
				Name = "Clover Wings Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 669,
				Name = "Flaming Hair",
				Info = "",
				Type = 4,
				Part = 2,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 670,
				Name = "Flaming Hair Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 671,
				Name = "Top Hat",
				Info = "",
				Type = 4,
				Part = 3,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 672,
				Name = "Top Hat Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 673,
				Name = "Apple Mask",
				Info = "",
				Type = 4,
				Part = 13,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 674,
				Name = "Apple Mask Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 675,
				Name = "Crate Box",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 6,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 676,
				Name = "Crate Box Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 6,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 677,
				Name = "Climbing Vine",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 12,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 678,
				Name = "Climbing Vine Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 12,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 679,
				Name = "Acid",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 4,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 680,
				Name = "Acid Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 681,
				Name = "Golden Spike Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 40,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 682,
				Name = "Golden Spike Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 40,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 683,
				Name = "Diamond Spike Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 40,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 684,
				Name = "Diamond Spike Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 40,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 685,
				Name = "Emerald Spike Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 40,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 686,
				Name = "Emerald Spike Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 40,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 687,
				Name = "Ruby Spike Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 40,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 688,
				Name = "Ruby Spike Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 40,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 689,
				Name = "Acid Lava Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 3,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = true
			},
			new ItemData
			{
				ID = 690,
				Name = "Acid Lava Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 691,
				Name = "Acid Hair",
				Info = "",
				Type = 4,
				Part = 2,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 692,
				Name = "Acid Hair Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 693,
				Name = "Pastel Red Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 5,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 694,
				Name = "Pastel Red Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 5,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 695,
				Name = "Pastel Green Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 12,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 696,
				Name = "Pastel Green Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 12,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 697,
				Name = "Pastel Blue Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 19,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 698,
				Name = "Pastel Blue Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 19,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 699,
				Name = "Pastel Yellow Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 9,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 700,
				Name = "Pastel Yellow Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 9,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 701,
				Name = "Red Pastel Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 4,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 702,
				Name = "Red Pastel Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 4,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 703,
				Name = "Green Pastel Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 11,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 704,
				Name = "Green Pastel Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 11,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 705,
				Name = "Blue Pastel Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 18,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 706,
				Name = "Blue Pastel Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 18,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 707,
				Name = "Yellow Pastel Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 8,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 708,
				Name = "Yellow Pastel Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 8,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 709,
				Name = "Easter Egg",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 0,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 710,
				Name = "Easter Egg Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 711,
				Name = "Golden Easter Egg",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 100,
				Hardness = 32,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 712,
				Name = "Golden Easter Egg Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 100,
				Hardness = 10,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 713,
				Name = "Bunny Mask",
				Info = "",
				Type = 4,
				Part = 13,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 714,
				Name = "Bunny Mask Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 715,
				Name = "Bunny Shirt",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 716,
				Name = "Bunny Shirt Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 717,
				Name = "Bunny Pants",
				Info = "",
				Type = 4,
				Part = 6,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 718,
				Name = "Bunny Pants Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 719,
				Name = "Bunny Shoes",
				Info = "",
				Type = 4,
				Part = 8,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 720,
				Name = "Bunny Shoes Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 721,
				Name = "Egg Head",
				Info = "",
				Type = 4,
				Part = 3,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 722,
				Name = "Egg Head Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 723,
				Name = "Face Mask",
				Info = "",
				Type = 4,
				Part = 13,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 724,
				Name = "Face Mask Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 725,
				Name = "Chocolate Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 726,
				Name = "Chocolate Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 727,
				Name = "Obsidian Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 728,
				Name = "Obsidian Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 729,
				Name = "Golden Bunny Mask",
				Info = "",
				Type = 4,
				Part = 13,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 730,
				Name = "Golden Bunny Mask Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 731,
				Name = "Golden Bunny Shirt",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 732,
				Name = "Golden Bunny Shirt Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 733,
				Name = "Golden Bunny Pants",
				Info = "",
				Type = 4,
				Part = 6,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 734,
				Name = "Golden Bunny Pants Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 735,
				Name = "Golden Bunny Shoes",
				Info = "",
				Type = 4,
				Part = 8,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 736,
				Name = "Golden Bunny Shoes Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 737,
				Name = "Golden Egg Head",
				Info = "",
				Type = 4,
				Part = 3,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 738,
				Name = "Golden Egg Head Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 739,
				Name = "Iron Bolt",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 740,
				Name = "Iron Bolt Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 741,
				Name = "Flaming Wings",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 742,
				Name = "Flaming Wings Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 743,
				Name = "Dark Blue Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 31,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 744,
				Name = "Dark Blue Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 31,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 745,
				Name = "Dark Brown Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 17,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 746,
				Name = "Dark Brown Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 17,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 747,
				Name = "Dark Green Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 24,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 748,
				Name = "Dark Green Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 24,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 749,
				Name = "Dark Grey Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 17,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 750,
				Name = "Dark Grey Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 17,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 751,
				Name = "Dark Orange Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 24,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 752,
				Name = "Dark Orange Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 24,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 753,
				Name = "Dark Purple Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 34,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 754,
				Name = "Dark Purple Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 34,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 755,
				Name = "Dark Red Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 17,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 756,
				Name = "Dark Red Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 17,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 757,
				Name = "Dark Yellow Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 21,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 758,
				Name = "Dark Yellow Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 21,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 759,
				Name = "Dark Blue Plastic Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 31,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 760,
				Name = "Dark Blue Plastic Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 31,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 761,
				Name = "Dark Brown Plastic Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 17,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 762,
				Name = "Dark Brown Plastic Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 17,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 763,
				Name = "Dark Green Plastic Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 24,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 764,
				Name = "Dark Green Plastic Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 24,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 765,
				Name = "Dark Grey Plastic Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 17,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 766,
				Name = "Dark Grey Plastic Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 17,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 767,
				Name = "Dark Orange Plastic Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 24,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 768,
				Name = "Dark Orange Plastic Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 24,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 769,
				Name = "Dark Purple Plastic Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 34,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 770,
				Name = "Dark Purple Plastic Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 34,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 771,
				Name = "Dark Red Plastic Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 17,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 772,
				Name = "Dark Red Plastic Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 17,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 773,
				Name = "Dark Yellow Plastic Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 21,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 774,
				Name = "Dark Yellow Plastic Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 21,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 775,
				Name = "Dark Blue Metal Panel",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 34,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 776,
				Name = "Dark Blue Metal Panel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 34,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 777,
				Name = "Dark Brown Metal Panel",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 20,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 778,
				Name = "Dark Brown Metal Panel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 20,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 779,
				Name = "Dark Green Metal Panel",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 27,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 780,
				Name = "Dark Green Metal Panel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 27,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 781,
				Name = "Dark Grey Metal Panel",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 20,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 782,
				Name = "Dark Grey Metal Panel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 20,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 783,
				Name = "Dark Orange Metal Panel",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 27,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 784,
				Name = "Dark Orange Metal Panel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 27,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 785,
				Name = "Dark Purple Metal Panel",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 37,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 786,
				Name = "Dark Purple Metal Panel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 37,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 787,
				Name = "Dark Red Metal Panel",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 20,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 788,
				Name = "Dark Red Metal Panel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 20,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 789,
				Name = "Dark Yellow Metal Panel",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 24,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 790,
				Name = "Dark Yellow Metal Panel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 24,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 791,
				Name = "Pastel Red Plastic Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 5,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 792,
				Name = "Pastel Red Plastic Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 5,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 793,
				Name = "Pastel Green Plastic Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 12,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 794,
				Name = "Pastel Green Plastic Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 12,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 795,
				Name = "Pastel Blue Plastic Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 19,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 796,
				Name = "Pastel Blue Plastic Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 19,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 797,
				Name = "Pastel Yellow Plastic Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 9,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 798,
				Name = "Pastel Yellow Plastic Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 9,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 799,
				Name = "Pastel Red Metal Panel",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 8,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 800,
				Name = "Pastel Red Metal Panel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 8,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 801,
				Name = "Pastel Green Metal Panel",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 15,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 802,
				Name = "Pastel Green Metal Panel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 15,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 803,
				Name = "Pastel Blue Metal Panel",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 22,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 804,
				Name = "Pastel Blue Metal Panel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 22,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 805,
				Name = "Pastel Yellow Metal Panel",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 12,
				Hardness = 5,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 806,
				Name = "Pastel Yellow Metal Panel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 12,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 807,
				Name = "Number Block Zero",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 808,
				Name = "Number Block Zero Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 809,
				Name = "Number Block One",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 810,
				Name = "Number Block One Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 811,
				Name = "Number Block Two",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 812,
				Name = "Number Block Two Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 813,
				Name = "Number Block Three",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 814,
				Name = "Number Block Three Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 815,
				Name = "Number Block Four",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 816,
				Name = "Number Block Four Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 817,
				Name = "Number Block Five",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 818,
				Name = "Number Block Five Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 819,
				Name = "Number Block Six",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 820,
				Name = "Number Block Six Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 821,
				Name = "Number Block Seven",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 822,
				Name = "Number Block Seven Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 823,
				Name = "Number Block Eight",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 824,
				Name = "Number Block Eight Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 825,
				Name = "Number Block Nine",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 826,
				Name = "Number Block Nine Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 827,
				Name = "Math Symbol Plus",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 828,
				Name = "Math Symbol Plus Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 829,
				Name = "Math Symbol Minus",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 830,
				Name = "Math Symbol Minus Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 831,
				Name = "Math Symbol Multiply",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 832,
				Name = "Math Symbol Multiply Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 833,
				Name = "Math Symbol Divide",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 834,
				Name = "Math Symbol Divide Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 835,
				Name = "Math Symbol Equal",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 836,
				Name = "Math Symbol Equal Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 837,
				Name = "Arrow Block Left",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 838,
				Name = "Arrow Block Left Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 839,
				Name = "Arrow Block Right",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 840,
				Name = "Arrow Block Right Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 841,
				Name = "Arrow Block Up",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 842,
				Name = "Arrow Block Up Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 843,
				Name = "Arrow Block Down",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 844,
				Name = "Arrow Block Down Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 845,
				Name = "Tangram Block: Rectangle Left",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 5,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 846,
				Name = "Tangram Block: Rectangle Left Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 5,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 847,
				Name = "Tangram Block: Rectangle Right",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 5,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 848,
				Name = "Tangram Block: Rectangle Right Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 5,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 849,
				Name = "Tangram Block: Rectangle Top",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 5,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 850,
				Name = "Tangram Block: Rectangle Top Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 5,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 851,
				Name = "Tangram Block: Rectangle Bottom",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 5,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 852,
				Name = "Tangram Block: Rectangle Bottom Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 5,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 853,
				Name = "Tangram Block: Triangle Top Left",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 6,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 854,
				Name = "Tangram Block: Triangle Top Left Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 6,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 855,
				Name = "Tangram Block: Triangle Top Right",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 6,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 856,
				Name = "Tangram Block: Triangle Top Right Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 6,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 857,
				Name = "Tangram Block: Triangle Bottom Left",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 6,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 858,
				Name = "Tangram Block: Triangle Bottom Left Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 6,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 859,
				Name = "Tangram Block: Triangle Bottom Right",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 6,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 860,
				Name = "Tangram Block: Triangle Bottom Right Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 6,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 861,
				Name = "Party Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 862,
				Name = "Party Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 863,
				Name = "Party Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 864,
				Name = "Party Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 865,
				Name = "Confetti Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 866,
				Name = "Confetti Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 867,
				Name = "Confetti Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 868,
				Name = "Confetti Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 869,
				Name = "Number Candle Zero",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 2,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 870,
				Name = "Number Candle Zero Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 2,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 871,
				Name = "Number Candle One",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 2,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 872,
				Name = "Number Candle One Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 2,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 873,
				Name = "Number Candle Two",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 2,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 874,
				Name = "Number Candle Two Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 2,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 875,
				Name = "Number Candle Three",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 2,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 876,
				Name = "Number Candle Three Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 2,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 877,
				Name = "Number Candle Four",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 2,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 878,
				Name = "Number Candle Four Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 2,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 879,
				Name = "Number Candle Five",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 2,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 880,
				Name = "Number Candle Five Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 2,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 881,
				Name = "Number Candle Six",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 2,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 882,
				Name = "Number Candle Six Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 2,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 883,
				Name = "Number Candle Seven",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 2,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 884,
				Name = "Number Candle Seven Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 2,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 885,
				Name = "Number Candle Eight",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 2,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 886,
				Name = "Number Candle Eight Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 2,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 887,
				Name = "Number Candle Nine",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 2,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 888,
				Name = "Number Candle Nine Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 2,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 889,
				Name = "Red Firework Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 890,
				Name = "Red Firework Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 891,
				Name = "Green Firework Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 892,
				Name = "Green Firework Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 893,
				Name = "Blue Firework Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 894,
				Name = "Blue Firework Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 895,
				Name = "Anniversary Cake",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 2,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 896,
				Name = "Anniversary Cake Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 2,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 897,
				Name = "Disco Ball",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 2,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 898,
				Name = "Disco Ball Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 2,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 899,
				Name = "Treasure Chest",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 32,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 900,
				Name = "Treasure Chest Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 32,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 901,
				Name = "Pet Dragon",
				Info = "",
				Type = 4,
				Part = 10,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 902,
				Name = "Pet Dragon Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 903,
				Name = "White Ball",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 0,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 904,
				Name = "White Ball Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 905,
				Name = "Nightmare Cape",
				Info = "Made by Hobitos.",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 906,
				Name = "Nightmare Cape Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 907,
				Name = "Pet Neon Dragon",
				Info = "",
				Type = 4,
				Part = 10,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 908,
				Name = "Pet Neon Dragon Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 909,
				Name = "Neon Wings",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 910,
				Name = "Neon Wings Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 911,
				Name = "Straight Long Brown Hair",
				Info = "",
				Type = 4,
				Part = 2,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 912,
				Name = "Straight Long Brown Hair Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 913,
				Name = "Red Goggles",
				Info = "",
				Type = 4,
				Part = 4,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 914,
				Name = "Red Goggles Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 915,
				Name = "Green Goggles",
				Info = "",
				Type = 4,
				Part = 4,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 916,
				Name = "Green Goggles Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 917,
				Name = "Blue Goggles",
				Info = "",
				Type = 4,
				Part = 4,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 918,
				Name = "Blue Goggles Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 919,
				Name = "Yellow Goggles",
				Info = "",
				Type = 4,
				Part = 4,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 920,
				Name = "Yellow Goggles Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 921,
				Name = "Pet Shark",
				Info = "",
				Type = 4,
				Part = 10,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 922,
				Name = "Pet Shark Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 923,
				Name = "Pet Octopus",
				Info = "",
				Type = 4,
				Part = 10,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 924,
				Name = "Pet Octopus Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 925,
				Name = "Red Neon Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 23,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 926,
				Name = "Red Neon Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 23,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 927,
				Name = "Green Neon Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 30,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 928,
				Name = "Green Neon Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 30,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 929,
				Name = "Blue Neon Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 37,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 930,
				Name = "Blue Neon Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 37,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 931,
				Name = "Yellow Neon Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 27,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 932,
				Name = "Yellow Neon Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 27,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 933,
				Name = "Red Neon Platform",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 33,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 934,
				Name = "Red Neon Platform Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 33,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 935,
				Name = "Green Neon Platform",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 40,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 936,
				Name = "Green Neon Platform Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 40,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 937,
				Name = "Blue Neon Platform",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 47,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 938,
				Name = "Blue Neon Platform Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 47,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 939,
				Name = "Yellow Neon Platform",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 37,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 940,
				Name = "Yellow Neon Platform Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 37,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 941,
				Name = "Red Neon Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 30,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 942,
				Name = "Red Neon Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 30,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 943,
				Name = "Green Neon Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 37,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 944,
				Name = "Green Neon Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 37,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 945,
				Name = "Blue Neon Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 44,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 946,
				Name = "Blue Neon Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 44,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 947,
				Name = "Yellow Neon Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 34,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 948,
				Name = "Yellow Neon Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 34,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 949,
				Name = "Red Neon Sword",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 950,
				Name = "Red Neon Sword Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 951,
				Name = "Green Neon Sword",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 952,
				Name = "Green Neon Sword Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 953,
				Name = "Blue Neon Sword",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 954,
				Name = "Blue Neon Sword Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 955,
				Name = "Yellow Neon Sword",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 956,
				Name = "Yellow Neon Sword Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 957,
				Name = "Red Neon Katana",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 958,
				Name = "Red Neon Katana Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 959,
				Name = "Green Neon Katana",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 960,
				Name = "Green Neon Katana Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 961,
				Name = "Blue Neon Katana",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 962,
				Name = "Blue Neon Katana Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 963,
				Name = "Yellow Neon Katana",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 964,
				Name = "Yellow Neon Katana Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 965,
				Name = "Beach Ticket",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 966,
				Name = "Beach Ticket Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 967,
				Name = "Pirate Hat",
				Info = "",
				Type = 4,
				Part = 3,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 968,
				Name = "Pirate Hat Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 969,
				Name = "Pirate Shirt",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 970,
				Name = "Pirate Shirt Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 971,
				Name = "Pirate Pants",
				Info = "",
				Type = 4,
				Part = 6,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 972,
				Name = "Pirate Pants Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 973,
				Name = "Pirate Boots",
				Info = "",
				Type = 4,
				Part = 8,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 974,
				Name = "Pirate Boots Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 975,
				Name = "Pirate Sword",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 976,
				Name = "Pirate Sword Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 977,
				Name = "Pet Megalodon",
				Info = "",
				Type = 4,
				Part = 10,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 978,
				Name = "Pet Megalodon Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 979,
				Name = "Pet Parrot",
				Info = "",
				Type = 4,
				Part = 10,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 980,
				Name = "Pet Parrot Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 981,
				Name = "Leaf Wings",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 982,
				Name = "Leaf Wings Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 983,
				Name = "Red Slippers",
				Info = "",
				Type = 4,
				Part = 8,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 984,
				Name = "Red Slippers Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 985,
				Name = "Green Slippers",
				Info = "",
				Type = 4,
				Part = 8,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 986,
				Name = "Green Slippers Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 987,
				Name = "Blue Slippers",
				Info = "",
				Type = 4,
				Part = 8,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 988,
				Name = "Blue Slippers Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 989,
				Name = "Yellow Slippers",
				Info = "",
				Type = 4,
				Part = 8,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 990,
				Name = "Yellow Slippers Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 991,
				Name = "Red Snorkel",
				Info = "",
				Type = 4,
				Part = 4,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 992,
				Name = "Red Snorkel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 993,
				Name = "Green Snorkel",
				Info = "",
				Type = 4,
				Part = 4,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 994,
				Name = "Green Snorkel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 995,
				Name = "Blue Snorkel",
				Info = "",
				Type = 4,
				Part = 4,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 996,
				Name = "Blue Snorkel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 997,
				Name = "Yellow Snorkel",
				Info = "",
				Type = 4,
				Part = 4,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 998,
				Name = "Yellow Snorkel Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 999,
				Name = "Neon Butterfly Wings",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1000,
				Name = "Neon Butterfly Wings Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1001,
				Name = "Sunglasses",
				Info = "",
				Type = 4,
				Part = 4,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1002,
				Name = "Sunglasses Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1003,
				Name = "Golden Trident",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1004,
				Name = "Golden Trident Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1005,
				Name = "Swimming Pool Block",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1006,
				Name = "Swimming Pool Block Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1007,
				Name = "Swimming Pool Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1008,
				Name = "Swimming Pool Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1009,
				Name = "Sand Castle",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1010,
				Name = "Sand Castle Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1011,
				Name = "Beach Chair",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1012,
				Name = "Beach Chair Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1013,
				Name = "Beach Umbrella",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1014,
				Name = "Beach Umbrella Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1015,
				Name = "Beach Lifebuoy",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1016,
				Name = "Beach Lifebuoy Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1017,
				Name = "Flaming Cape",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = true
			},
			new ItemData
			{
				ID = 1018,
				Name = "Flaming Cape Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1019,
				Name = "Neon Cape",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1020,
				Name = "Neon Cape Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1021,
				Name = "Red Neon Cape",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = true
			},
			new ItemData
			{
				ID = 1022,
				Name = "Red Neon Cape Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1023,
				Name = "Green Neon Cape",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = true
			},
			new ItemData
			{
				ID = 1024,
				Name = "Green Neon Cape Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1025,
				Name = "Blue Neon Cape",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = true
			},
			new ItemData
			{
				ID = 1026,
				Name = "Blue Neon Cape Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1027,
				Name = "Yellow Neon Cape",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = true
			},
			new ItemData
			{
				ID = 1028,
				Name = "Yellow Neon Cape Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1029,
				Name = "Black Medium Hair",
				Info = "",
				Type = 4,
				Part = 2,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1030,
				Name = "Black Medium Hair Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1031,
				Name = "Wooden Chandelier",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 17,
				Hardness = 4,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1032,
				Name = "Wooden Chandelier Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 17,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1033,
				Name = "Wooden Bookcase",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 18,
				Hardness = 4,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1034,
				Name = "Wooden Bookcase Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 18,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1035,
				Name = "Wooden Bed",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 9,
				Hardness = 4,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1036,
				Name = "Wooden Bed Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 9,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1037,
				Name = "Invisible Skin",
				Info = "",
				Type = 4,
				Part = 9,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = false,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 1038,
				Name = "Invisible Skin Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1039,
				Name = "Stone Brick Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 9,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1040,
				Name = "Stone Brick Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 9,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1041,
				Name = "Black Brick Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 14,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1042,
				Name = "Black Brick Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 14,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1043,
				Name = "White Brick Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 15,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1044,
				Name = "White Brick Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 15,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1045,
				Name = "Red Brick Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 14,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1046,
				Name = "Red Brick Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 14,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1047,
				Name = "Brown Brick Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 14,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1048,
				Name = "Brown Brick Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 14,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1049,
				Name = "Grey Brick Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 14,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1050,
				Name = "Grey Brick Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 14,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1051,
				Name = "Yellow Brick Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 18,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1052,
				Name = "Yellow Brick Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 18,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1053,
				Name = "Orange Brick Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 21,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1054,
				Name = "Orange Brick Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 21,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1055,
				Name = "Green Brick Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 21,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1056,
				Name = "Green Brick Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 21,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1057,
				Name = "Blue Brick Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 28,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1058,
				Name = "Blue Brick Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 28,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1059,
				Name = "Purple Brick Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 31,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1060,
				Name = "Purple Brick Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 31,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1061,
				Name = "Pink Brick Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 32,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1062,
				Name = "Pink Brick Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 32,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1063,
				Name = "Space Theme",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1064,
				Name = "Space Theme Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1065,
				Name = "Small Tree",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 4,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1066,
				Name = "Small Tree Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1067,
				Name = "Big Tree",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 4,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1068,
				Name = "Big Tree Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1069,
				Name = "Tulips",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 7,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1070,
				Name = "Tulips Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 7,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1071,
				Name = "Roses",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 7,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1072,
				Name = "Roses Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 7,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1073,
				Name = "Begonias",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 7,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1074,
				Name = "Begonias Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 7,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1075,
				Name = "Sunflower",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 6,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1076,
				Name = "Sunflower Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 6,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1077,
				Name = "Small Rock",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 8,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1078,
				Name = "Small Rock Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 8,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1079,
				Name = "Medium Rock",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 8,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1080,
				Name = "Medium Rock Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 8,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1081,
				Name = "Big Rock",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 8,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1082,
				Name = "Big Rock Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 8,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1083,
				Name = "Barrier Rope",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 8,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1084,
				Name = "Barrier Rope Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1085,
				Name = "Velvet Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 1,
				Hardness = 5,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1086,
				Name = "Velvet Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1087,
				Name = "Red Ceiling Lamp",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 11,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1088,
				Name = "Red Ceiling Lamp Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 11,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1089,
				Name = "Green Ceiling Lamp",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 18,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1090,
				Name = "Green Ceiling Lamp Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 18,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1091,
				Name = "Blue Ceiling Lamp",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 25,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1092,
				Name = "Blue Ceiling Lamp Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 25,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1093,
				Name = "Yellow Ceiling Lamp",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 15,
				Hardness = 4,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1094,
				Name = "Yellow Ceiling Lamp Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 15,
				Hardness = 10,
				Farmability = 3,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1095,
				Name = "Reward Sign 1",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 3,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 1096,
				Name = "Reward Sign 1 Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 1097,
				Name = "Reward Sign 2",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 3,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 1098,
				Name = "Reward Sign 2 Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 1099,
				Name = "Reward Welcome Sign",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 3,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 1100,
				Name = "Reward Welcome Sign Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 1101,
				Name = "Glass Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 3,
				Hardness = 3,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1102,
				Name = "Glass Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 3,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1103,
				Name = "Red Glass Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 6,
				Hardness = 3,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1104,
				Name = "Red Glass Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 6,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1105,
				Name = "Green Glass Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 13,
				Hardness = 3,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1106,
				Name = "Green Glass Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 13,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1107,
				Name = "Blue Glass Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 20,
				Hardness = 3,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1108,
				Name = "Blue Glass Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 20,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1109,
				Name = "Yellow Glass Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 10,
				Hardness = 3,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1110,
				Name = "Yellow Glass Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 10,
				Hardness = 10,
				Farmability = 4,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1111,
				Name = "Black Tuxedo Top",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1112,
				Name = "Black Tuxedo Top Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1113,
				Name = "Black Tuxedo Pants",
				Info = "",
				Type = 4,
				Part = 6,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1114,
				Name = "Black Tuxedo Pants Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1115,
				Name = "Blue Tuxedo Top",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1116,
				Name = "Blue Tuxedo Top Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1117,
				Name = "Blue Tuxedo Pants",
				Info = "",
				Type = 4,
				Part = 6,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1118,
				Name = "Blue Tuxedo Pants Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1119,
				Name = "Tuxedo Shoes",
				Info = "",
				Type = 4,
				Part = 8,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1120,
				Name = "Tuxedo Shoes Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1121,
				Name = "Jump Sign",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 3,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 1122,
				Name = "Jump Sign Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 1123,
				Name = "Go Left Sign",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 3,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 1124,
				Name = "Go Left Sign Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 1125,
				Name = "Go Right Sign",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 3,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 1126,
				Name = "Go Right Sign Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 1127,
				Name = "Spooky Theme",
				Info = "",
				Type = 5,
				Part = 0,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1128,
				Name = "Spooky Theme Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1129,
				Name = "Pet Flaming Bat",
				Info = "",
				Type = 4,
				Part = 10,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1130,
				Name = "Pet Flaming Bat Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1131,
				Name = "Trap Platform",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 3,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1132,
				Name = "Trap Platform Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1133,
				Name = "Bear Trap",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 3,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1134,
				Name = "Bear Trap Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1135,
				Name = "Skeleton Character",
				Info = "",
				Type = 4,
				Part = 9,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1136,
				Name = "Skeleton Character Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1137,
				Name = "Pet Skeleton Bat",
				Info = "",
				Type = 4,
				Part = 10,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1138,
				Name = "Pet Skeleton Bat Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1139,
				Name = "Wooden Scythe",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1140,
				Name = "Wooden Scythe Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1141,
				Name = "Soul Scythe",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1142,
				Name = "Soul Scythe Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1143,
				Name = "Halloween Enemy",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 3,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 1144,
				Name = "Halloween Enemy Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 1145,
				Name = "Halloween Candy",
				Info = "",
				Type = 0,
				Part = 0,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1146,
				Name = "Halloween Candy Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1147,
				Name = "Skeleton Wings",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1148,
				Name = "Skeleton Wings Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1149,
				Name = "Small Bone",
				Info = "",
				Type = 0,
				Part = 0,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1150,
				Name = "Small Bone Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1151,
				Name = "Medium Bone",
				Info = "",
				Type = 0,
				Part = 0,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1152,
				Name = "Medium Bone Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1153,
				Name = "Big Bone",
				Info = "",
				Type = 0,
				Part = 0,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1154,
				Name = "Big Bone Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1155,
				Name = "Golden Knight's Helmet",
				Info = "",
				Type = 4,
				Part = 13,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1156,
				Name = "Golden Knight's Helmet Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1157,
				Name = "Golden Knight's Chestplate",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1158,
				Name = "Golden Knight's Chestplate Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1159,
				Name = "Golden Knight's Pants",
				Info = "",
				Type = 4,
				Part = 6,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1160,
				Name = "Golden Knight's Pants Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1161,
				Name = "Golden Knight's Boots",
				Info = "",
				Type = 4,
				Part = 8,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1162,
				Name = "Golden Knight's Boots Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1163,
				Name = "Golden Knight's Wand",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1164,
				Name = "Golden Knight's Wand Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1165,
				Name = "Executioner's Face Cover",
				Info = "",
				Type = 4,
				Part = 13,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1166,
				Name = "Executioner's Face Cover Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1167,
				Name = "Executioner's Chestplate",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1168,
				Name = "Executioner's Chestplate Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1169,
				Name = "Executioner's Pants",
				Info = "",
				Type = 4,
				Part = 6,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1170,
				Name = "Executioner's Pants Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1171,
				Name = "Executioner's Boots",
				Info = "",
				Type = 4,
				Part = 8,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1172,
				Name = "Executioner's Boots Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1173,
				Name = "Executioner's Axe",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1174,
				Name = "Executioner's Axe Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1175,
				Name = "Medieval Cape",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1176,
				Name = "Medieval Cape Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1177,
				Name = "Torch",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 3,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1178,
				Name = "Torch Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1179,
				Name = "Lead Rope",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 3,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1180,
				Name = "Lead Rope Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1181,
				Name = "Silver Knight's Helmet",
				Info = "",
				Type = 4,
				Part = 13,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1182,
				Name = "Silver Knight's Helmet Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1183,
				Name = "Silver Knight's Chestplate",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1184,
				Name = "Silver Knight's Chestplate Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1185,
				Name = "Silver Knight's Pants",
				Info = "",
				Type = 4,
				Part = 6,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1186,
				Name = "Silver Knight's Pants Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1187,
				Name = "Silver Knight's Boots",
				Info = "",
				Type = 4,
				Part = 8,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1188,
				Name = "Silver Knight's Boots Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1189,
				Name = "Silver Knight's Wand",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1190,
				Name = "Silver Knight's Wand Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1191,
				Name = "Bronze Knight's Helmet",
				Info = "",
				Type = 4,
				Part = 13,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1192,
				Name = "Bronze Knight's Helmet Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1193,
				Name = "Bronze Knight's Chestplate",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1194,
				Name = "Bronze Knight's Chestplate Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1195,
				Name = "Bronze Knight's Pants",
				Info = "",
				Type = 4,
				Part = 6,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1196,
				Name = "Bronze Knight's Pants Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1197,
				Name = "Bronze Knight's Boots",
				Info = "",
				Type = 4,
				Part = 8,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1198,
				Name = "Bronze Knight's Boots Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1199,
				Name = "Bronze Knight's Wand",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1200,
				Name = "Bronze Knight's Wand Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1201,
				Name = "Soul Cape",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1202,
				Name = "Soul Cape Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1203,
				Name = "Death's Cape",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1204,
				Name = "Death's Cape Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1205,
				Name = "Medieval Torch",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1206,
				Name = "Medieval Torch Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1207,
				Name = "Medieval Chandelier",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1208,
				Name = "Medieval Chandelier Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1209,
				Name = "Castle Tower",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1210,
				Name = "Castle Tower Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1211,
				Name = "Castle Background",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1212,
				Name = "Castle Background Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1213,
				Name = "Castle Window",
				Info = "",
				Type = 1,
				Part = 0,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1214,
				Name = "Castle Window Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1215,
				Name = "Castle Wall",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1216,
				Name = "Castle Wall Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1217,
				Name = "Jetpack",
				Info = "",
				Type = 4,
				Part = 1,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1218,
				Name = "Jetpack Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1219,
				Name = "Space Suit",
				Info = "",
				Type = 4,
				Part = 5,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1220,
				Name = "Space Suit Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1221,
				Name = "Space Helmet",
				Info = "",
				Type = 4,
				Part = 13,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1222,
				Name = "Space Helmet Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1223,
				Name = "Space Pants & Boots",
				Info = "",
				Type = 4,
				Part = 6,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1224,
				Name = "Space Pants & Boots Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1225,
				Name = "Blue Lightsaber",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1226,
				Name = "Blue Lightsaber Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1227,
				Name = "Red Double Lightsaber",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1228,
				Name = "Red Lightsaber Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1229,
				Name = "Yellow Lightsaber",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1230,
				Name = "Yellow Lightsaber Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1231,
				Name = "Green Lightsaber",
				Info = "",
				Type = 4,
				Part = 7,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1232,
				Name = "Yellow Lightsaber Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = false
			},
			new ItemData
			{
				ID = 1233,
				Name = "Non-Player Character (Space)",
				Info = "",
				Type = 2,
				Part = 0,
				Rarity = 1,
				Hardness = 4,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = true
			},
			new ItemData
			{
				ID = 1234,
				Name = "Non-Player Character Seed (Space)",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 1235,
				Name = "Long Blonde Ponytail Hair",
				Info = "",
				Type = 4,
				Part = 2,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1236,
				Name = "Long Blonde Ponytail Hair Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 1237,
				Name = "Brown Sidepiece Hair",
				Info = "",
				Type = 4,
				Part = 2,
				Rarity = 1,
				Hardness = 5,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1238,
				Name = "Brown Sidepiece Hair Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 1239,
				Name = "Pet Alien",
				Info = "",
				Type = 4,
				Part = 10,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1240,
				Name = "Pet Alien Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			},
			new ItemData
			{
				ID = 1241,
				Name = "Pet Mini Astronaut",
				Info = "",
				Type = 4,
				Part = 10,
				Rarity = 1,
				Hardness = 3,
				Farmability = 1,
				Tradeable = true,
				Trashable = true,
				Droppable = true,
				Lockable = true,
				Vendable = true,
				Solid = true
			},
			new ItemData
			{
				ID = 1242,
				Name = "Pet Mini Astronaut Seed",
				Info = "",
				Type = 3,
				Part = 0,
				Rarity = 1,
				Hardness = 10,
				Farmability = 0,
				Tradeable = false,
				Trashable = true,
				Droppable = false,
				Lockable = true,
				Vendable = false,
				Solid = false
			}
		};

		public static void ValidateRarities()
		{
			for (int i = 0; i < List.Count; i++)
			{
				for (int j = 0; j < List.Count; j++)
				{
					if (i > j && Combine((ushort)i, (ushort)j) != 0)
					{
						int num = Rarity(i);
						int num2 = Rarity(j);
						int num3 = Rarity(Combine((ushort)i, (ushort)j));
						if (num + num2 != num3)
						{
							Terminal.Message("Wrong rarity for {0}, real is {1}.", Combine((ushort)i, (ushort)j), num + num2);
						}
					}
				}
			}
		}

		public static void ValidateRarityEquality()
		{
			for (int i = 1; i < List.Count; i += 2)
			{
				if (Rarity(i) != Rarity(ItemToSeed(i)))
				{
					Terminal.Message("Inequality in rarities for {0}.", i);
				}
			}
		}

		public static ItemData Data(int item)
		{
			if (item < List.Count)
			{
				return List[item];
			}
			return List[0];
		}

		public static string Name(int item)
		{
			return Data(item).Name;
		}

		public static string Info(int item)
		{
			return Data(item).Info;
		}

		public static ushort Type(int item)
		{
			return Data(item).Type;
		}

		public static ushort Part(int item)
		{
			return Data(item).Part;
		}

		public static ushort Rarity(int item)
		{
			return Data(item).Rarity;
		}

		public static ushort Hardness(int item)
		{
			return Data(item).Hardness;
		}

		public static ushort Farmability(int item)
		{
			return Data(item).Farmability;
		}

		public static bool Tradeable(int item)
		{
			return Data(item).Tradeable;
		}

		public static bool Trashable(int item)
		{
			return Data(item).Trashable;
		}

		public static bool Droppable(int item)
		{
			return Data(item).Droppable;
		}

		public static bool Lockable(int item)
		{
			return Data(item).Lockable;
		}

		public static bool Vendable(int item)
		{
			return Data(item).Vendable;
		}

		public static bool Solid(int item)
		{
			return Data(item).Solid;
		}

		public static ExchangeData Exchange(int item)
		{
			switch (item)
			{
			case 73:
			{
				ExchangeData result = default(ExchangeData);
				result.Index = 493;
				result.Count = 1;
				result.Amount = 100;
				return result;
			}
			case 493:
			{
				ExchangeData result = default(ExchangeData);
				result.Index = 73;
				result.Count = 100;
				result.Amount = 1;
				return result;
			}
			case 593:
			{
				ExchangeData result = default(ExchangeData);
				result.Index = 595;
				result.Count = 1;
				result.Amount = 100;
				return result;
			}
			case 595:
			{
				ExchangeData result = default(ExchangeData);
				result.Index = 593;
				result.Count = 100;
				result.Amount = 1;
				return result;
			}
			case 289:
			{
				ExchangeData result = default(ExchangeData);
				result.Index = 609;
				result.Count = 1;
				result.Amount = 100;
				return result;
			}
			case 609:
			{
				ExchangeData result = default(ExchangeData);
				result.Index = 289;
				result.Count = 100;
				result.Amount = 1;
				return result;
			}
			case 709:
			{
				ExchangeData result = default(ExchangeData);
				result.Index = 711;
				result.Count = 1;
				result.Amount = 100;
				return result;
			}
			case 711:
			{
				ExchangeData result = default(ExchangeData);
				result.Index = 709;
				result.Count = 100;
				result.Amount = 1;
				return result;
			}
			case 1149:
			{
				ExchangeData result = default(ExchangeData);
				result.Index = 1137;
				result.Count = 1;
				result.Amount = 500;
				return result;
			}
			case 1151:
			{
				ExchangeData result = default(ExchangeData);
				result.Index = 1147;
				result.Count = 1;
				result.Amount = 500;
				return result;
			}
			case 1153:
			{
				ExchangeData result = default(ExchangeData);
				result.Index = 1135;
				result.Count = 1;
				result.Amount = 500;
				return result;
			}
			default:
				return default(ExchangeData);
			}
		}

		public static int ItemGems(int item)
		{
			if (Farmability(item) != 0)
			{
				return (int)Math.Round(Math.Sqrt((int)Rarity(item)) / 2.0 * 3.0);
			}
			return 0;
		}

		public static int SeedGems(int item)
		{
			return (int)Math.Round(Math.Sqrt((int)Rarity(item)) / 2.0 * 4.0);
		}

		public static int SeedCurrency(int item)
		{
			return (int)Math.Round(Math.Sqrt((int)Rarity(item) / 3) * 1.0);
		}

		public static int BreakExtra(int item, Player player)
		{
			if (item != 595 || Event.Type == EventType.Valentine)
			{
			}
			if (item == 711 && Event.Type == EventType.Easter)
			{
				return Event.GetReward2().Index;
			}
			if (item == 895 && Event.Type == EventType.Anniversary)
			{
				return Event.GetReward2().Index;
			}
			if (item != 711 && Event.Type == EventType.Easter && Server.Random.Next(10) == 0)
			{
				return 709;
			}
			if (item != 895 && Event.Type == EventType.Anniversary && Server.Random.Next(25) == 0)
			{
				return 895;
			}
			if (Type(item) == 2 && Server.Random.Next(10) == 0)
			{
				int[] array = new int[4] { 159, 160, 161, 162 };
				return array[Server.Random.Next(array.Length)];
			}
			if (Type(item) == 3 && Server.Random.Next(10) == 0)
			{
				int[] array2 = new int[4] { 165, 167, 169, 171 };
				return array2[Server.Random.Next(array2.Length)];
			}
			return 0;
		}

		public static int Growtime(int item)
		{
			TimeSpan timeSpan = DateTime.UtcNow.Subtract(Server.Date);
			int num = (int)Math.Ceiling((double)(Rarity(item) * Rarity(item) * Rarity(item) + Rarity(item) * 30));

			return (int)timeSpan.TotalSeconds + item switch
			{
				157 => 3600,
				163 => 600,
				239 => 7200,
				241 => 28800,
				391 => 28800,
				393 => 28800,
				395 => 28800,
				371 => 259200,
				559 => 28800,
				561 => 144000,
				563 => 72000,
				565 => 144000,
				567 => 108000,
				569 => 108000,
				571 => 180000,
				_ => num,
			};
		}

		public static int SeedToItem(int item)
		{
			return item - 1;
		}

		public static int ItemToSeed(int item)
		{
			return item + 1;
		}

		public static int GetHarvest(int time)
		{
			return (int)Math.Floor((double)time - DateTime.UtcNow.Subtract(Server.Date).TotalSeconds);
		}

		public static bool Harvestable(int time)
		{
			if ((double)time > DateTime.UtcNow.Subtract(Server.Date).TotalSeconds)
			{
				return false;
			}
			return true;
		}

		public static bool Wireable(int item)
		{
			return item switch
			{
				195 => true, 
				205 => true, 
				253 => true, 
				419 => true, 
				439 => true, 
				441 => true, 
				443 => true, 
				_ => false, 
			};
		}

		public static bool Blocked(int item)
		{
			return item switch
			{
				71 => true, 
				73 => true, 
				203 => true, 
				207 => true, 
				209 => true, 
				211 => true, 
				213 => true, 
				319 => true, 
				321 => true, 
				323 => true, 
				493 => true, 
				511 => true, 
				_ => false, 
			};
		}

		public static int Order(int item)
		{
			switch (item)
			{
			case 1:
				return 1;
			case 3:
				return 2;
			default:
				if (Type(item) == 2)
				{
					return List.Count + item;
				}
				if (Type(item) == 1)
				{
					return List.Count * 2 + item;
				}
				if (Type(item) == 3)
				{
					return List.Count * 3 + item;
				}
				if (Type(item) == 5)
				{
					return List.Count * 4 + item;
				}
				if (Type(item) == 4)
				{
					return List.Count * 5 + item;
				}
				return List.Count * 6 + item;
			}
		}

		public static int Jumps(int item)
		{
			return item switch
			{
				139 => 1, 
				141 => 2, 
				143 => 2, 
				187 => 1, 
				189 => 2, 
				193 => 3, 
				239 => 2, 
				241 => 3, 
				261 => 2, 
				331 => 2, 
				367 => 2, 
				369 => 2, 
				371 => 3, 
				373 => 2, 
				375 => 2, 
				383 => 2, 
				385 => 1, 
				391 => 3, 
				393 => 3, 
				395 => 3, 
				447 => 2, 
				455 => 2, 
				543 => 3, 
				585 => 2, 
				667 => 2, 
				741 => 2, 
				905 => 2, 
				909 => 2, 
				981 => 2, 
				999 => 3, 
				1017 => 2, 
				1019 => 2, 
				1021 => 2, 
				1023 => 2, 
				1025 => 2, 
				1027 => 2, 
				1147 => 2, 
				1175 => 2, 
				1201 => 2, 
				1203 => 2, 
				1217 => 3, 
				_ => 0, 
			};
		}

		public static PunchEffect PunchEffect(int item)
		{
			switch (item)
			{
			case 225:
			{
				PunchEffect result = default(PunchEffect);
				result.ID = 3;
				result.Data1 = 1;
				return result;
			}
			case 285:
			{
				PunchEffect result = default(PunchEffect);
				result.ID = 3;
				result.Data1 = 2;
				return result;
			}
			case 589:
			{
				PunchEffect result = default(PunchEffect);
				result.ID = 3;
				result.Data1 = 3;
				return result;
			}
			case 739:
			{
				PunchEffect result = default(PunchEffect);
				result.ID = 3;
				result.Data1 = 4;
				return result;
			}
			case 525:
			{
				PunchEffect result = default(PunchEffect);
				result.ID = 2;
				result.Data1 = 2;
				result.X = 0;
				result.Y = 4;
				return result;
			}
			case 583:
			{
				PunchEffect result = default(PunchEffect);
				result.ID = 2;
				result.Data1 = 1;
				result.X = 0;
				result.Y = 4;
				return result;
			}
			case 1003:
			{
				PunchEffect result = default(PunchEffect);
				result.ID = 2;
				result.Data1 = 3;
				result.X = 0;
				result.Y = 4;
				return result;
			}
			case 1141:
			{
				PunchEffect result = default(PunchEffect);
				result.ID = 4;
				result.Data1 = 1;
				result.X = 0;
				result.Y = 4;
				return result;
			}
			default:
				return default(PunchEffect);
			}
		}

		public static int Effect(int item)
		{
			return item switch
			{
				513 => 3, 
				367 => 4, 
				241 => 5, 
				287 => 5, 
				371 => 5, 
				141 => 5, 
				143 => 6, 
				373 => 7, 
				375 => 8, 
				543 => 9, 
				585 => 10, 
				667 => 11, 
				669 => 12, 
				691 => 13, 
				741 => 15, 
				905 => 16, 
				1017 => 17, 
				1141 => 18, 
				1201 => 18, 
				1203 => 19, 
				1217 => 20, 
				_ => 0, 
			};
		}

		public static int Collision(int item)
		{
			return item switch
			{
				67 => 1, 
				199 => 1, 
				249 => 1, 
				251 => 1, 
				389 => 1, 
				507 => 1, 
				677 => 1, 
				933 => 1, 
				935 => 1, 
				937 => 1, 
				939 => 1, 
				_ => 0, 
			};
		}

		public static int Bait(int item)
		{
			return item switch
			{
				547 => 1, 
				549 => 2, 
				551 => 2, 
				553 => 3, 
				555 => 3, 
				_ => 0, 
			};
		}

		public static bool Rod(int item)
		{
			return item switch
			{
				545 => true, 
				573 => true, 
				_ => false, 
			};
		}

		public static bool Smokable(int item)
		{
			return item switch
			{
				561 => true, 
				563 => true, 
				565 => true, 
				567 => true, 
				569 => true, 
				571 => true, 
				_ => false, 
			};
		}

		public static int Fish(int item)
		{
			return item switch
			{
				561 => 150, 
				563 => 75, 
				565 => 150, 
				567 => 100, 
				569 => 100, 
				571 => 500, 
				_ => 0, 
			};
		}

		public static bool Default(int item)
		{
			return item switch
			{
				9 => true, 
				11 => true, 
				13 => true, 
				15 => true, 
				181 => true, 
				153 => true, 
				501 => true, 
				727 => true, 
				_ => false, 
			};
		}

		public static ushort Combine(ushort one, ushort two)
		{
			ushort num = Math.Min(one, two);
			ushort num2 = Math.Max(one, two);
			if (num == 10 && num2 == 12)
			{
				return 18;
			}
			if (num == 10 && num2 == 14)
			{
				return 20;
			}
			if (num == 12 && num2 == 14)
			{
				return 22;
			}
			if (num == 16 && num2 == 20)
			{
				return 24;
			}
			if (num == 20 && num2 == 22)
			{
				return 26;
			}
			if (num == 12 && num2 == 20)
			{
				return 28;
			}
			if (num == 10 && num2 == 20)
			{
				return 30;
			}
			if (num == 14 && num2 == 20)
			{
				return 32;
			}
			if (num == 26 && num2 == 28)
			{
				return 34;
			}
			if (num == 24 && num2 == 34)
			{
				return 36;
			}
			if (num == 28 && num2 == 34)
			{
				return 38;
			}
			if (num == 34 && num2 == 38)
			{
				return 40;
			}
			if (num == 24 && num2 == 40)
			{
				return 42;
			}
			if (num == 26 && num2 == 40)
			{
				return 44;
			}
			if (num == 16 && num2 == 24)
			{
				return 46;
			}
			if (num == 16 && num2 == 26)
			{
				return 48;
			}
			if (num == 16 && num2 == 28)
			{
				return 50;
			}
			if (num == 16 && num2 == 30)
			{
				return 52;
			}
			if (num == 16 && num2 == 32)
			{
				return 54;
			}
			if (num == 16 && num2 == 34)
			{
				return 56;
			}
			if (num == 16 && num2 == 36)
			{
				return 58;
			}
			if (num == 16 && num2 == 38)
			{
				return 60;
			}
			if (num == 16 && num2 == 40)
			{
				return 62;
			}
			if (num == 16 && num2 == 42)
			{
				return 64;
			}
			if (num == 16 && num2 == 44)
			{
				return 66;
			}
			if (num == 18 && num2 == 52)
			{
				return 68;
			}
			if (num == 18 && num2 == 32)
			{
				return 70;
			}
			if (num == 18 && num2 == 36)
			{
				return 72;
			}
			if (num == 14 && num2 == 18)
			{
				return 146;
			}
			if (num == 154 && num2 == 182)
			{
				return 184;
			}
			if (num == 10 && num2 == 182)
			{
				return 186;
			}
			if (num == 18 && num2 == 72)
			{
				return 196;
			}
			if (num == 14 && num2 == 32)
			{
				return 198;
			}
			if (num == 68 && num2 == 198)
			{
				return 200;
			}
			if (num == 70 && num2 == 198)
			{
				return 202;
			}
			if (num == 72 && num2 == 198)
			{
				return 204;
			}
			if (num == 196 && num2 == 198)
			{
				return 206;
			}
			if (num == 28 && num2 == 204)
			{
				return 208;
			}
			if (num == 38 && num2 == 204)
			{
				return 210;
			}
			if (num == 40 && num2 == 204)
			{
				return 212;
			}
			if (num == 34 && num2 == 204)
			{
				return 214;
			}
			if (num == 18 && num2 == 22)
			{
				return 216;
			}
			if (num == 164 && num2 == 216)
			{
				return 218;
			}
			if (num == 198 && num2 == 218)
			{
				return 220;
			}
			if (num == 18 && num2 == 196)
			{
				return 246;
			}
			if (num == 18 && num2 == 70)
			{
				return 248;
			}
			if (num == 18 && num2 == 68)
			{
				return 250;
			}
			if (num == 198 && num2 == 250)
			{
				return 252;
			}
			if (num == 22 && num2 == 198)
			{
				return 254;
			}
			if (num == 22 && num2 == 182)
			{
				return 256;
			}
			if (num == 198 && num2 == 344)
			{
				return 264;
			}
			if (num == 28 && num2 == 182)
			{
				return 266;
			}
			if (num == 38 && num2 == 182)
			{
				return 268;
			}
			if (num == 40 && num2 == 182)
			{
				return 270;
			}
			if (num == 34 && num2 == 182)
			{
				return 272;
			}
			if (num == 182 && num2 == 198)
			{
				return 290;
			}
			if (num == 22 && num2 == 28)
			{
				return 300;
			}
			if (num == 22 && num2 == 38)
			{
				return 302;
			}
			if (num == 22 && num2 == 40)
			{
				return 304;
			}
			if (num == 22 && num2 == 34)
			{
				return 306;
			}
			if (num == 174 && num2 == 244)
			{
				return 298;
			}
			if (num == 176 && num2 == 244)
			{
				return 296;
			}
			if (num == 178 && num2 == 244)
			{
				return 294;
			}
			if (num == 180 && num2 == 244)
			{
				return 292;
			}
			if (num == 202 && num2 == 246)
			{
				return 308;
			}
			if (num == 198 && num2 == 304)
			{
				return 310;
			}
			if (num == 18 && num2 == 308)
			{
				return 312;
			}
			if (num == 20 && num2 == 638)
			{
				return 316;
			}
			if (num == 20 && num2 == 636)
			{
				return 318;
			}
			if (num == 16 && num2 == 18)
			{
				return 326;
			}
			if (num == 24 && num2 == 198)
			{
				return 342;
			}
			if (num == 26 && num2 == 198)
			{
				return 344;
			}
			if (num == 28 && num2 == 198)
			{
				return 346;
			}
			if (num == 30 && num2 == 198)
			{
				return 348;
			}
			if (num == 32 && num2 == 198)
			{
				return 350;
			}
			if (num == 34 && num2 == 198)
			{
				return 352;
			}
			if (num == 36 && num2 == 198)
			{
				return 354;
			}
			if (num == 38 && num2 == 198)
			{
				return 356;
			}
			if (num == 40 && num2 == 198)
			{
				return 358;
			}
			if (num == 42 && num2 == 198)
			{
				return 360;
			}
			if (num == 44 && num2 == 198)
			{
				return 362;
			}
			if (num == 10 && num2 == 16)
			{
				return 378;
			}
			if (num == 18 && num2 == 378)
			{
				return 380;
			}
			if (num == 16 && num2 == 378)
			{
				return 382;
			}
			if (num == 68 && num2 == 382)
			{
				return 390;
			}
			if (num == 14 && num2 == 264)
			{
				return 418;
			}
			if (num == 202 && num2 == 254)
			{
				return 420;
			}
			if (num == 614 && num2 == 616)
			{
				return 428;
			}
			if (num == 204 && num2 == 210)
			{
				return 430;
			}
			if (num == 40 && num2 == 418)
			{
				return 466;
			}
			if (num == 30 && num2 == 418)
			{
				return 468;
			}
			if (num == 38 && num2 == 418)
			{
				return 470;
			}
			if (num == 32 && num2 == 418)
			{
				return 472;
			}
			if (num == 36 && num2 == 418)
			{
				return 474;
			}
			if (num == 42 && num2 == 418)
			{
				return 476;
			}
			if (num == 28 && num2 == 418)
			{
				return 478;
			}
			if (num == 34 && num2 == 418)
			{
				return 480;
			}
			if (num == 182 && num2 == 502)
			{
				return 504;
			}
			if (num == 266 && num2 == 504)
			{
				return 506;
			}
			if (num == 200 && num2 == 506)
			{
				return 508;
			}
			if (num == 202 && num2 == 506)
			{
				return 510;
			}
			if (num == 204 && num2 == 506)
			{
				return 512;
			}
			if (num == 198 && num2 == 326)
			{
				return 608;
			}
			if (num == 198 && num2 == 264)
			{
				return 604;
			}
			if (num == 264 && num2 == 608)
			{
				return 606;
			}
			if (num == 24 && num2 == 164)
			{
				return 614;
			}
			if (num == 26 && num2 == 164)
			{
				return 616;
			}
			if (num == 28 && num2 == 164)
			{
				return 618;
			}
			if (num == 30 && num2 == 164)
			{
				return 620;
			}
			if (num == 32 && num2 == 164)
			{
				return 622;
			}
			if (num == 34 && num2 == 164)
			{
				return 624;
			}
			if (num == 36 && num2 == 164)
			{
				return 626;
			}
			if (num == 38 && num2 == 164)
			{
				return 628;
			}
			if (num == 40 && num2 == 164)
			{
				return 630;
			}
			if (num == 42 && num2 == 164)
			{
				return 632;
			}
			if (num == 44 && num2 == 164)
			{
				return 634;
			}
			if (num == 20 && num2 == 24)
			{
				return 636;
			}
			if (num == 20 && num2 == 26)
			{
				return 638;
			}
			if (num == 20 && num2 == 28)
			{
				return 640;
			}
			if (num == 20 && num2 == 30)
			{
				return 642;
			}
			if (num == 20 && num2 == 32)
			{
				return 644;
			}
			if (num == 20 && num2 == 34)
			{
				return 646;
			}
			if (num == 20 && num2 == 36)
			{
				return 648;
			}
			if (num == 20 && num2 == 38)
			{
				return 650;
			}
			if (num == 20 && num2 == 40)
			{
				return 652;
			}
			if (num == 20 && num2 == 42)
			{
				return 654;
			}
			if (num == 20 && num2 == 44)
			{
				return 656;
			}
			if (num == 174 && num2 == 264)
			{
				return 682;
			}
			if (num == 176 && num2 == 264)
			{
				return 684;
			}
			if (num == 178 && num2 == 264)
			{
				return 686;
			}
			if (num == 180 && num2 == 264)
			{
				return 688;
			}
			if (num == 250 && num2 == 380)
			{
				return 678;
			}
			if (num == 10 && num2 == 70)
			{
				return 676;
			}
			if (num == 28 && num2 == 728)
			{
				return 702;
			}
			if (num == 38 && num2 == 728)
			{
				return 704;
			}
			if (num == 40 && num2 == 728)
			{
				return 706;
			}
			if (num == 34 && num2 == 728)
			{
				return 708;
			}
			if (num == 16 && num2 == 702)
			{
				return 694;
			}
			if (num == 16 && num2 == 704)
			{
				return 696;
			}
			if (num == 16 && num2 == 706)
			{
				return 698;
			}
			if (num == 16 && num2 == 708)
			{
				return 700;
			}
			if (num == 16 && num2 == 466)
			{
				return 744;
			}
			if (num == 16 && num2 == 468)
			{
				return 746;
			}
			if (num == 16 && num2 == 470)
			{
				return 748;
			}
			if (num == 16 && num2 == 472)
			{
				return 750;
			}
			if (num == 16 && num2 == 474)
			{
				return 752;
			}
			if (num == 16 && num2 == 476)
			{
				return 754;
			}
			if (num == 16 && num2 == 478)
			{
				return 756;
			}
			if (num == 16 && num2 == 480)
			{
				return 758;
			}
			if (num == 164 && num2 == 466)
			{
				return 760;
			}
			if (num == 164 && num2 == 468)
			{
				return 762;
			}
			if (num == 164 && num2 == 470)
			{
				return 764;
			}
			if (num == 164 && num2 == 472)
			{
				return 766;
			}
			if (num == 164 && num2 == 474)
			{
				return 768;
			}
			if (num == 164 && num2 == 476)
			{
				return 770;
			}
			if (num == 164 && num2 == 478)
			{
				return 772;
			}
			if (num == 164 && num2 == 480)
			{
				return 774;
			}
			if (num == 198 && num2 == 466)
			{
				return 776;
			}
			if (num == 198 && num2 == 468)
			{
				return 778;
			}
			if (num == 198 && num2 == 470)
			{
				return 780;
			}
			if (num == 198 && num2 == 472)
			{
				return 782;
			}
			if (num == 198 && num2 == 474)
			{
				return 784;
			}
			if (num == 198 && num2 == 476)
			{
				return 786;
			}
			if (num == 198 && num2 == 478)
			{
				return 788;
			}
			if (num == 198 && num2 == 480)
			{
				return 790;
			}
			if (num == 164 && num2 == 702)
			{
				return 792;
			}
			if (num == 164 && num2 == 704)
			{
				return 794;
			}
			if (num == 164 && num2 == 706)
			{
				return 796;
			}
			if (num == 164 && num2 == 708)
			{
				return 798;
			}
			if (num == 198 && num2 == 702)
			{
				return 800;
			}
			if (num == 198 && num2 == 704)
			{
				return 802;
			}
			if (num == 198 && num2 == 706)
			{
				return 804;
			}
			if (num == 198 && num2 == 708)
			{
				return 806;
			}
			if (num == 614 && num2 == 838)
			{
				return 846;
			}
			if (num == 614 && num2 == 840)
			{
				return 848;
			}
			if (num == 614 && num2 == 842)
			{
				return 850;
			}
			if (num == 614 && num2 == 844)
			{
				return 852;
			}
			if (num == 616 && num2 == 838)
			{
				return 854;
			}
			if (num == 616 && num2 == 840)
			{
				return 856;
			}
			if (num == 616 && num2 == 842)
			{
				return 858;
			}
			if (num == 616 && num2 == 844)
			{
				return 860;
			}
			if (num == 808 && num2 == 896)
			{
				return 870;
			}
			if (num == 810 && num2 == 896)
			{
				return 872;
			}
			if (num == 812 && num2 == 896)
			{
				return 874;
			}
			if (num == 814 && num2 == 896)
			{
				return 876;
			}
			if (num == 816 && num2 == 896)
			{
				return 878;
			}
			if (num == 818 && num2 == 896)
			{
				return 880;
			}
			if (num == 820 && num2 == 896)
			{
				return 882;
			}
			if (num == 822 && num2 == 896)
			{
				return 884;
			}
			if (num == 824 && num2 == 896)
			{
				return 886;
			}
			if (num == 826 && num2 == 896)
			{
				return 888;
			}
			if (num == 198 && num2 == 208)
			{
				return 926;
			}
			if (num == 198 && num2 == 210)
			{
				return 928;
			}
			if (num == 198 && num2 == 212)
			{
				return 930;
			}
			if (num == 198 && num2 == 214)
			{
				return 932;
			}
			if (num == 200 && num2 == 926)
			{
				return 934;
			}
			if (num == 200 && num2 == 928)
			{
				return 936;
			}
			if (num == 200 && num2 == 930)
			{
				return 938;
			}
			if (num == 200 && num2 == 932)
			{
				return 940;
			}
			if (num == 608 && num2 == 926)
			{
				return 942;
			}
			if (num == 608 && num2 == 928)
			{
				return 944;
			}
			if (num == 608 && num2 == 930)
			{
				return 946;
			}
			if (num == 608 && num2 == 932)
			{
				return 948;
			}
			if (num == 196 && num2 == 206)
			{
				return 900;
			}
			if (num == 18 && num2 == 420)
			{
				return 1032;
			}
			if (num == 18 && num2 == 246)
			{
				return 1034;
			}
			if (num == 18 && num2 == 248)
			{
				return 1036;
			}
			if (num == 20 && num2 == 608)
			{
				return 1040;
			}
			if (num == 636 && num2 == 1040)
			{
				return 1042;
			}
			if (num == 638 && num2 == 1040)
			{
				return 1044;
			}
			if (num == 640 && num2 == 1040)
			{
				return 1046;
			}
			if (num == 642 && num2 == 1040)
			{
				return 1048;
			}
			if (num == 644 && num2 == 1040)
			{
				return 1050;
			}
			if (num == 646 && num2 == 1040)
			{
				return 1052;
			}
			if (num == 648 && num2 == 1040)
			{
				return 1054;
			}
			if (num == 650 && num2 == 1040)
			{
				return 1056;
			}
			if (num == 652 && num2 == 1040)
			{
				return 1058;
			}
			if (num == 654 && num2 == 1040)
			{
				return 1060;
			}
			if (num == 656 && num2 == 1040)
			{
				return 1062;
			}
			if (num == 254 && num2 == 300)
			{
				return 1088;
			}
			if (num == 254 && num2 == 302)
			{
				return 1090;
			}
			if (num == 254 && num2 == 304)
			{
				return 1092;
			}
			if (num == 254 && num2 == 306)
			{
				return 1094;
			}
			if (num == 10 && num2 == 1076)
			{
				return 1070;
			}
			if (num == 12 && num2 == 1076)
			{
				return 1072;
			}
			if (num == 14 && num2 == 1076)
			{
				return 1074;
			}
			if (num == 378 && num2 == 380)
			{
				return 1076;
			}
			if (num == 16 && num2 == 22)
			{
				return 1102;
			}
			if (num == 16 && num2 == 300)
			{
				return 1104;
			}
			if (num == 16 && num2 == 302)
			{
				return 1106;
			}
			if (num == 16 && num2 == 304)
			{
				return 1108;
			}
			if (num == 16 && num2 == 306)
			{
				return 1110;
			}
			if (num == 14 && num2 == 1070)
			{
				return 1078;
			}
			if (num == 14 && num2 == 1072)
			{
				return 1080;
			}
			if (num == 14 && num2 == 1074)
			{
				return 1082;
			}
			return 0;
		}

		public static int Flag(string country)
		{
			return country.ToLower() switch
			{
				"ad" => 0, 
				"ae" => 1, 
				"af" => 2, 
				"ag" => 3, 
				"ai" => 4, 
				"al" => 5, 
				"am" => 6, 
				"an" => 7, 
				"ao" => 8, 
				"ar" => 9, 
				"as" => 10, 
				"at" => 11, 
				"au" => 12, 
				"aw" => 13, 
				"ax" => 14, 
				"az" => 15, 
				"ba" => 16, 
				"bb" => 17, 
				"bd" => 18, 
				"be" => 19, 
				"bf" => 20, 
				"bg" => 21, 
				"bh" => 22, 
				"bi" => 23, 
				"bj" => 24, 
				"bm" => 25, 
				"bn" => 26, 
				"bo" => 27, 
				"br" => 28, 
				"bs" => 29, 
				"bt" => 30, 
				"bv" => 31, 
				"bw" => 32, 
				"by" => 33, 
				"bz" => 34, 
				"ca" => 35, 
				"es.catalonia" => 36, 
				"cc" => 37, 
				"cd" => 38, 
				"cf" => 39, 
				"cg" => 40, 
				"ch" => 41, 
				"ci" => 42, 
				"ck" => 43, 
				"cl" => 44, 
				"cm" => 45, 
				"cn" => 46, 
				"co" => 47, 
				"cr" => 48, 
				"cs" => 49, 
				"cu" => 50, 
				"cv" => 51, 
				"cx" => 52, 
				"cy" => 53, 
				"cz" => 54, 
				"de" => 55, 
				"dj" => 56, 
				"dk" => 57, 
				"dm" => 58, 
				"do" => 59, 
				"dz" => 60, 
				"ec" => 61, 
				"ee" => 62, 
				"eg" => 63, 
				"eh" => 64, 
				"gb.england" => 65, 
				"er" => 66, 
				"es" => 67, 
				"et" => 68, 
				"breaworlds.verified" => 69, 
				"breaworlds.youtuber" => 70, 
				"fi" => 71, 
				"fj" => 72, 
				"fk" => 73, 
				"fm" => 74, 
				"fo" => 75, 
				"fr" => 76, 
				"ga" => 77, 
				"gb" => 78, 
				"en" => 78, 
				"gd" => 79, 
				"ge" => 80, 
				"gf" => 81, 
				"gh" => 82, 
				"gi" => 83, 
				"gl" => 84, 
				"gm" => 85, 
				"gn" => 86, 
				"gp" => 87, 
				"gq" => 88, 
				"gr" => 89, 
				"gs" => 90, 
				"gt" => 91, 
				"gu" => 92, 
				"gw" => 93, 
				"gy" => 94, 
				"hk" => 95, 
				"hm" => 96, 
				"hn" => 97, 
				"hr" => 98, 
				"ht" => 99, 
				"hu" => 100, 
				"id" => 101, 
				"ie" => 102, 
				"il" => 103, 
				"in" => 104, 
				"io" => 105, 
				"iq" => 106, 
				"ir" => 107, 
				"is" => 108, 
				"it" => 109, 
				"jm" => 110, 
				"jo" => 111, 
				"jp" => 112, 
				"ke" => 113, 
				"kg" => 114, 
				"kh" => 115, 
				"ki" => 116, 
				"km" => 117, 
				"kn" => 118, 
				"kp" => 119, 
				"kr" => 120, 
				"kw" => 121, 
				"ky" => 122, 
				"kz" => 123, 
				"la" => 124, 
				"lb" => 125, 
				"lc" => 126, 
				"li" => 127, 
				"lk" => 128, 
				"lr" => 129, 
				"ls" => 130, 
				"lt" => 131, 
				"lu" => 132, 
				"lv" => 133, 
				"ly" => 134, 
				"ma" => 135, 
				"mc" => 136, 
				"md" => 137, 
				"me" => 138, 
				"mg" => 139, 
				"mh" => 140, 
				"mk" => 141, 
				"ml" => 142, 
				"mm" => 143, 
				"mn" => 144, 
				"mo" => 145, 
				"mp" => 146, 
				"mq" => 147, 
				"mr" => 148, 
				"ms" => 149, 
				"mt" => 150, 
				"mu" => 151, 
				"mv" => 152, 
				"mw" => 153, 
				"mx" => 154, 
				"my" => 155, 
				"mz" => 156, 
				"na" => 157, 
				"nc" => 158, 
				"ne" => 159, 
				"nf" => 160, 
				"ng" => 161, 
				"ni" => 162, 
				"nl" => 163, 
				"no" => 164, 
				"np" => 165, 
				"nr" => 166, 
				"nu" => 167, 
				"nz" => 168, 
				"om" => 169, 
				"pa" => 170, 
				"pe" => 171, 
				"pf" => 172, 
				"pg" => 173, 
				"ph" => 174, 
				"pk" => 175, 
				"pl" => 176, 
				"pm" => 177, 
				"pn" => 178, 
				"pr" => 179, 
				"ps" => 180, 
				"pt" => 181, 
				"pw" => 182, 
				"py" => 183, 
				"qa" => 184, 
				"re" => 185, 
				"ro" => 186, 
				"rs" => 187, 
				"ru" => 188, 
				"rw" => 189, 
				"sa" => 190, 
				"sb" => 191, 
				"sc" => 192, 
				"gb.scotland" => 193, 
				"sd" => 194, 
				"se" => 195, 
				"sg" => 196, 
				"sh" => 197, 
				"si" => 198, 
				"sj" => 199, 
				"sk" => 200, 
				"sl" => 201, 
				"sm" => 202, 
				"sn" => 203, 
				"so" => 204, 
				"sr" => 205, 
				"st" => 206, 
				"sv" => 207, 
				"sy" => 208, 
				"sz" => 209, 
				"tc" => 210, 
				"td" => 211, 
				"tf" => 212, 
				"tg" => 213, 
				"th" => 214, 
				"tj" => 215, 
				"tk" => 216, 
				"tl" => 217, 
				"tm" => 218, 
				"tn" => 219, 
				"to" => 220, 
				"tr" => 221, 
				"tt" => 222, 
				"tv" => 223, 
				"tw" => 224, 
				"tz" => 225, 
				"ua" => 226, 
				"ug" => 227, 
				"um" => 228, 
				"us" => 229, 
				"uy" => 230, 
				"uz" => 231, 
				"va" => 232, 
				"vc" => 233, 
				"ve" => 234, 
				"vg" => 235, 
				"vi" => 236, 
				"vn" => 237, 
				"vu" => 238, 
				"gb.wales" => 239, 
				"wf" => 240, 
				"ws" => 241, 
				"ye" => 242, 
				"yt" => 243, 
				"za" => 244, 
				"zm" => 245, 
				"zw" => 246, 
				"nomo" => 247, 
				_ => 229, 
			};
		}
	}
}
