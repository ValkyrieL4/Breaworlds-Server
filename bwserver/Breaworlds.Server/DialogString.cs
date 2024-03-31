using System;
using System.IO;

namespace Breaworlds.Server
{
	public class DialogString
	{
		public static void Parse(BinaryWriter writer, string data)
		{
			try
			{
				Dialog.Create(writer, 0, ".DialogTest");
				string[] array = data.Split('\n');
				string[] array2 = array;
				foreach (string text in array2)
				{
					if (!text.StartsWith("#") && text.Length > 0)
					{
						string[] array3 = text.Split('|');
						if (array3[0] == "Text" && array3.Length > 3)
						{
							Dialog.Text(writer, bool.Parse(array3[1]), array3[2], int.Parse(array3[3]));
						}
						else if (array3[0] == "ItemSlot" && array3.Length > 5)
						{
							Dialog.ItemSlot(writer, bool.Parse(array3[1]), int.Parse(array3[2]), int.Parse(array3[3]), int.Parse(array3[4]), int.Parse(array3[5]));
						}
						else if (array3[0] == "ItemText" && array3.Length > 4)
						{
							Dialog.ItemText(writer, bool.Parse(array3[1]), array3[2], int.Parse(array3[3]), int.Parse(array3[4]));
						}
						else if (array3[0] == "ItemPicker" && array3.Length > 5)
						{
							Dialog.ItemPicker(writer, bool.Parse(array3[1]), array3[2], int.Parse(array3[3]), int.Parse(array3[4]), int.Parse(array3[5]));
						}
						else if (array3[0] == "Button" && array3.Length > 3)
						{
							Dialog.Button(writer, bool.Parse(array3[1]), array3[2], array3[3]);
						}
						else if (array3[0] == "Textbox" && array3.Length > 4)
						{
							Dialog.Textbox(writer, bool.Parse(array3[1]), array3[2], array3[3], byte.Parse(array3[4]));
						}
						else if (array3[0] == "Checkbox" && array3.Length > 5)
						{
							Dialog.Checkbox(writer, bool.Parse(array3[1]), bool.Parse(array3[2]), array3[3], array3[4], int.Parse(array3[5]));
						}
						else if (array3[0] == "RGB" && array3.Length > 7)
						{
							Dialog.RGB(writer, bool.Parse(array3[1]), array3[2], int.Parse(array3[3]), int.Parse(array3[4]), int.Parse(array3[5]), int.Parse(array3[6]), int.Parse(array3[7]));
						}
						else if (array3[0] == "Space" && array3.Length != 0)
						{
							Dialog.Space(writer);
						}
						else if (array3[0] == "Achievement" && array3.Length > 5)
						{
							Dialog.Achievement(writer, bool.Parse(array3[1]), int.Parse(array3[2]), array3[3], array3[4], array3[5]);
						}
						else if (array3[0] == "ItemButton" && array3.Length > 4)
						{
							Dialog.ItemButton(writer, bool.Parse(array3[1]), array3[2], int.Parse(array3[3]), int.Parse(array3[4]));
						}
						else if (array3[0] == "IconButton" && array3.Length > 4)
						{
							Dialog.IconButton(writer, bool.Parse(array3[1]), array3[2], int.Parse(array3[3]), int.Parse(array3[4]));
						}
					}
				}
			}
			catch (Exception)
			{
				Dialog.Text(writer, breaker: false, "~3Parse error, couldn't parse dialog string further.", 50);
			}
		}
	}
}
