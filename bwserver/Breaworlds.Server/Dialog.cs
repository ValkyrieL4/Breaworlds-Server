using System;
using System.IO;
using System.Text;

namespace Breaworlds.Server
{
	public class Dialog
	{
		public static string Create(BinaryWriter writer, int worker, string name)
		{
			writer.Write(Convert.ToUInt16(worker));
			writer.Write(Encoding.UTF8.GetBytes(name + "\0"));
			return name;
		}

		public static void Text(BinaryWriter writer, bool breaker, string text, int size)
		{
			text = text.Replace("\n", string.Empty);
			text = text.Replace("\r\n", string.Empty);
			writer.Write(Convert.ToUInt16(1));
			writer.Write(Convert.ToBoolean(breaker));
			writer.Write(Encoding.UTF8.GetBytes(text + "\0"));
			writer.Write(Convert.ToUInt16(size));
		}

		public static void ItemSlot(BinaryWriter writer, bool breaker, int index, int count, int w, int h)
		{
			writer.Write(Convert.ToUInt16(2));
			writer.Write(Convert.ToBoolean(breaker));
			writer.Write(Convert.ToUInt16(index));
			writer.Write(Convert.ToUInt16(count));
			writer.Write(Convert.ToUInt16(w));
			writer.Write(Convert.ToUInt16(h));
		}

		public static void ItemText(BinaryWriter writer, bool breaker, string text, int size, int item)
		{
			text = text.Replace("\n", string.Empty);
			text = text.Replace("\r\n", string.Empty);
			writer.Write(Convert.ToUInt16(3));
			writer.Write(Convert.ToBoolean(breaker));
			writer.Write(Encoding.UTF8.GetBytes(text + "\0"));
			writer.Write(Convert.ToUInt16(size));
			writer.Write(Convert.ToUInt16(item));
		}

		public static void ItemImage(BinaryWriter writer, bool breaker, int size, int item)
		{
			writer.Write(Convert.ToUInt16(3));
			writer.Write(Convert.ToBoolean(breaker));
			writer.Write(Encoding.UTF8.GetBytes("\0"));
			writer.Write(Convert.ToUInt16(size));
			writer.Write(Convert.ToUInt16(item));
		}

		public static void ItemPicker(BinaryWriter writer, bool breaker, string name, int item, int w, int h)
		{
			writer.Write(Convert.ToUInt16(8));
			writer.Write(Convert.ToBoolean(breaker));
			writer.Write(Encoding.UTF8.GetBytes(name + "\0"));
			writer.Write(Convert.ToUInt16(item));
			writer.Write(Convert.ToUInt16(w));
			writer.Write(Convert.ToUInt16(h));
		}

		public static void Button(BinaryWriter writer, bool breaker, string name, string text)
		{
			writer.Write(Convert.ToUInt16(4));
			writer.Write(Convert.ToBoolean(breaker));
			writer.Write(Encoding.UTF8.GetBytes(name + "\0"));
			writer.Write(Encoding.UTF8.GetBytes(text + "\0"));
		}

		public static void Textbox(BinaryWriter writer, bool breaker, string name, string text, byte length)
		{
			text = text.Replace("\n", string.Empty);
			text = text.Replace("\r\n", string.Empty);
			writer.Write(Convert.ToUInt16(5));
			writer.Write(Convert.ToBoolean(breaker));
			writer.Write(Encoding.UTF8.GetBytes(name + "\0"));
			writer.Write(Encoding.UTF8.GetBytes(text + "\0"));
			writer.Write(Convert.ToByte(length));
		}

		public static void Checkbox(BinaryWriter writer, bool breaker, bool value, string name, string text, int size)
		{
			writer.Write(Convert.ToUInt16(6));
			writer.Write(Convert.ToBoolean(breaker));
			writer.Write(Convert.ToBoolean(value));
			writer.Write(Encoding.UTF8.GetBytes(name + "\0"));
			writer.Write(Encoding.UTF8.GetBytes(text + "\0"));
			writer.Write(Convert.ToUInt16(size));
		}

		public static void RGB(BinaryWriter writer, bool breaker, string name, int w, int h, int r, int g, int b)
		{
			writer.Write(Convert.ToUInt16(7));
			writer.Write(Convert.ToBoolean(breaker));
			writer.Write(Encoding.UTF8.GetBytes(name + "\0"));
			writer.Write(Convert.ToUInt16(w));
			writer.Write(Convert.ToUInt16(h));
			writer.Write(Convert.ToUInt16(r));
			writer.Write(Convert.ToUInt16(g));
			writer.Write(Convert.ToUInt16(b));
		}

		public static void Space(BinaryWriter writer)
		{
			writer.Write(Convert.ToUInt16(9));
			writer.Write(Convert.ToBoolean(value: true));
		}

		public static void Achievement(BinaryWriter writer, bool breaker, int icon, string name, string text, string info)
		{
			writer.Write(Convert.ToUInt16(10));
			writer.Write(Convert.ToBoolean(breaker));
			writer.Write(Convert.ToUInt16(icon));
			writer.Write(Encoding.UTF8.GetBytes(name + "\0"));
			writer.Write(Encoding.UTF8.GetBytes(text + "\0"));
			writer.Write(Encoding.UTF8.GetBytes(info + "\0"));
		}

		public static void ItemButton(BinaryWriter writer, bool breaker, string name, int item, int size)
		{
			writer.Write(Convert.ToUInt16(11));
			writer.Write(Convert.ToBoolean(breaker));
			writer.Write(Encoding.UTF8.GetBytes(name + "\0"));
			writer.Write(Convert.ToUInt16(item));
			writer.Write(Convert.ToUInt16(size));
			writer.Write(Convert.ToUInt16(size));
		}

		public static void IconButton(BinaryWriter writer, bool breaker, string name, int icon, int size)
		{
			writer.Write(Convert.ToUInt16(12));
			writer.Write(Convert.ToBoolean(breaker));
			writer.Write(Encoding.UTF8.GetBytes(name + "\0"));
			writer.Write(Convert.ToUInt16(icon));
			writer.Write(Convert.ToUInt16(size));
			writer.Write(Convert.ToUInt16(size));
		}
	}
}
