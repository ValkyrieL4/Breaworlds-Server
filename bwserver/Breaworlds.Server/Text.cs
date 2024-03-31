using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Breaworlds.Server
{
	public class Text
	{
		private static EmailAddressAttribute EmailAttribute = new EmailAddressAttribute();

		private static readonly string[] SwearWords = new string[22]
		{
			"bitch", "penis", "porn", "shit", "tits", "pussy", "nigger", "cock", "dick", "bastard",
			"sex", "cunt", "fuck", "nigga", "asshole", "kontol", "memek", "anjing", "goblok", "bokep",
			"yarrak", "fag"
		};

		private static readonly string[] ColorCodes = new string[16]
		{
			"~r", "~o", "~0", "~1", "~2", "~3", "~4", "~5", "~6", "~7",
			"~8", "~9", "~v", "~m", "~a", "~d"
		};

		public static bool IsEmail(string text)
		{
			return EmailAttribute.IsValid(text);
		}

		public static bool IsSwear(string text)
		{
			text = FilterColor(text.ToLower());
			string[] swearWords = SwearWords;
			foreach (string value in swearWords)
			{
				if (text.Contains(value))
				{
					return true;
				}
			}
			return false;
		}

		public static bool IsAllowed(string text)
		{
			foreach (char c in text)
			{
				if (c != 'Q' && c != 'q' && c != 'W' && c != 'w' && c != 'E' && c != 'e' && c != 'R' && c != 'r' && c != 'T' && c != 't' && c != 'Y' && c != 'y' && c != 'U' && c != 'u' && c != 'I' && c != 'i' && c != 'O' && c != 'o' && c != 'P' && c != 'p' && c != 'A' && c != 'a' && c != 'S' && c != 's' && c != 'D' && c != 'd' && c != 'F' && c != 'f' && c != 'G' && c != 'g' && c != 'H' && c != 'h' && c != 'J' && c != 'j' && c != 'K' && c != 'k' && c != 'L' && c != 'l' && c != 'Z' && c != 'z' && c != 'X' && c != 'x' && c != 'C' && c != 'c' && c != 'V' && c != 'v' && c != 'B' && c != 'b' && c != 'N' && c != 'n' && c != 'M' && c != 'm' && c != '0' && c != '1' && c != '2' && c != '3' && c != '4' && c != '5' && c != '6' && c != '7' && c != '8' && c != '9')
				{
					return false;
				}
			}
			if (text.ToUpper() == "CON")
			{
				return false;
			}
			if (text.ToUpper() == "PRN")
			{
				return false;
			}
			if (text.ToUpper() == "AUX")
			{
				return false;
			}
			if (text.ToUpper() == "NUL")
			{
				return false;
			}
			if (text.ToUpper() == "COM1")
			{
				return false;
			}
			if (text.ToUpper() == "COM2")
			{
				return false;
			}
			if (text.ToUpper() == "COM3")
			{
				return false;
			}
			if (text.ToUpper() == "COM4")
			{
				return false;
			}
			if (text.ToUpper() == "COM5")
			{
				return false;
			}
			if (text.ToUpper() == "COM6")
			{
				return false;
			}
			if (text.ToUpper() == "COM7")
			{
				return false;
			}
			if (text.ToUpper() == "COM8")
			{
				return false;
			}
			if (text.ToUpper() == "COM9")
			{
				return false;
			}
			if (text.ToUpper() == "COM0")
			{
				return false;
			}
			if (text.ToUpper() == "LPT1")
			{
				return false;
			}
			if (text.ToUpper() == "LPT2")
			{
				return false;
			}
			if (text.ToUpper() == "LPT3")
			{
				return false;
			}
			if (text.ToUpper() == "LPT4")
			{
				return false;
			}
			if (text.ToUpper() == "LPT5")
			{
				return false;
			}
			if (text.ToUpper() == "LPT6")
			{
				return false;
			}
			if (text.ToUpper() == "LPT7")
			{
				return false;
			}
			if (text.ToUpper() == "LPT8")
			{
				return false;
			}
			if (text.ToUpper() == "LPT9")
			{
				return false;
			}
			if (text.ToUpper() == "LPT0")
			{
				return false;
			}
			return true;
		}

		public static string FilterSwear(string text)
		{
			string[] swearWords = SwearWords;
			foreach (string text2 in swearWords)
			{
				string replacement = new string('*', text2.Length);
				text = Regex.Replace(text, text2, replacement, RegexOptions.IgnoreCase);
			}
			return text;
		}

		public static string FilterColor(string text)
		{
			string[] colorCodes = ColorCodes;
			foreach (string pattern in colorCodes)
			{
				text = Regex.Replace(text, pattern, "", RegexOptions.IgnoreCase);
			}
			return text;
		}

		public static string Time(int time)
		{
			List<string> list = new List<string>();
			TimeSpan timeSpan = TimeSpan.FromSeconds(time);
			float num = timeSpan.Days;
			float num2 = timeSpan.Hours;
			float num3 = timeSpan.Minutes;
			float num4 = timeSpan.Seconds;
			if (num > 0f)
			{
				list.Add($"{num}d");
			}
			if (num2 > 0f)
			{
				list.Add($"{num2}h");
			}
			if (num3 > 0f)
			{
				list.Add($"{num3}m");
			}
			if (num4 > 0f)
			{
				list.Add($"{num4}s");
			}
			return string.Join(" ", list.ToArray());
		}

		public static string TimeLong(int time)
		{
			List<string> list = new List<string>();
			TimeSpan timeSpan = TimeSpan.FromSeconds(time);
			float num = timeSpan.Days;
			float num2 = timeSpan.Hours;
			float num3 = timeSpan.Minutes;
			float num4 = timeSpan.Seconds;
			if (num > 0f)
			{
				list.Add(string.Format("{0} {1}", num, (num % 10f == 1f) ? "day" : "days"));
			}
			if (num2 > 0f)
			{
				list.Add(string.Format("{0} {1}", num2, (num2 % 10f == 1f) ? "hour" : "hours"));
			}
			if (num3 > 0f)
			{
				list.Add(string.Format("{0} {1}", num3, (num3 % 10f == 1f) ? "minute" : "minutes"));
			}
			if (num4 > 0f)
			{
				list.Add(string.Format("{0} {1}", num4, (num4 % 10f == 1f) ? "second" : "seconds"));
			}
			return string.Join(" ", list.ToArray());
		}

		public static bool Length(string text, int min, int max)
		{
			if (text.Length < min)
			{
				return false;
			}
			if (text.Length > max)
			{
				return false;
			}
			return true;
		}

		public static string Ordinal(int number)
		{
			return (number % 100) switch
			{
				11 => number + "th", 
				12 => number + "th", 
				13 => number + "th", 
				_ => (number % 10) switch
				{
					1 => number + "st", 
					2 => number + "nd", 
					3 => number + "rd", 
					_ => number + "th", 
				}, 
			};
		}

		public static string Delimit(int number)
		{
			return number.ToString("N0");
		}

		public static string WordWrap(string text, int width)
		{
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			List<string> list3 = new List<string>();
			string[] array = text.Split(' ');
			foreach (string text2 in array)
			{
				if (text2.Length > width)
				{
					for (int j = 0; j < text2.Length; j += width)
					{
						list.Add(text2.Substring(j, Math.Min(width, text2.Length - j)));
					}
				}
				else
				{
					list.Add(text2);
				}
			}
			foreach (string item in list)
			{
				if (string.Join(" ", list3.ToArray()).Length + item.Length > width)
				{
					list2.Add(string.Join(" ", list3.ToArray()));
					list3.Clear();
				}
				list3.Add(item);
			}
			if (list3.Count != 0)
			{
				list2.Add(string.Join(" ", list3.ToArray()));
			}
			return string.Join(Environment.NewLine, list2.ToArray());
		}

		public static string Capitalize(string input)
		{
			try
			{
				string text = input.ToLower();
				char[] array = new char[3] { '.', '!', '?' };
				char[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					char c = array2[i];
					string[] array3 = text.Split(c);
					for (int j = 0; j < array3.Length; j++)
					{
						string[] array4 = array3[j].Split(' ');
						for (int k = 0; k < array4.Length; k++)
						{
							if (array4[k] == "cant")
							{
								array4[k] = "can't";
							}
							if (array4[k] == "dont")
							{
								array4[k] = "don't";
							}
							if (array4[k] == "doesnt")
							{
								array4[k] = "doesn't";
							}
							if (array4[k] == "wont")
							{
								array4[k] = "won't";
							}
							if (array4[k] == "wasnt")
							{
								array4[k] = "wasn't";
							}
							if (array4[k] == "werent")
							{
								array4[k] = "weren't";
							}
							if (array4[k] == "im")
							{
								array4[k] = "I'm";
							}
							if (array4[k] == "its")
							{
								array4[k] = "it's";
							}
							if (array4[k] == "isnt")
							{
								array4[k] = "isn't";
							}
							if (array4[k] == "arent")
							{
								array4[k] = "aren't";
							}
							if (array4[k] == "thats")
							{
								array4[k] = "that's";
							}
							if (array4[k] == "theres")
							{
								array4[k] = "there's";
							}
							if (array4[k] == "couldnt")
							{
								array4[k] = "couldn't";
							}
							if (array4[k] == "rly")
							{
								array4[k] = "really";
							}
							if (array4[k] == "idc")
							{
								array4[k] = "I don't care";
							}
							if (array4[k] == "idk")
							{
								array4[k] = "I don't know";
							}
							if (array4[k] == "btw")
							{
								array4[k] = "by the way";
							}
							if (array4[k] == "sup")
							{
								array4[k] = "what's up";
							}
							if (array4[k] == "ik")
							{
								array4[k] = "I know";
							}
							if (array4[k] == "thx")
							{
								array4[k] = "thanks";
							}
							if (array4[k] == "np")
							{
								array4[k] = "no problem";
							}
							if (array4[k] == "ty")
							{
								array4[k] = "thank you";
							}
							if (array4[k] == "nty")
							{
								array4[k] = "no thanks";
							}
							if (array4[k] == "k")
							{
								array4[k] = "okay";
							}
							if (array4[k] == "kk")
							{
								array4[k] = "okay";
							}
							if (array4[k] == "u")
							{
								array4[k] = "you";
							}
							if (array4[k] == "ye")
							{
								array4[k] = "yes";
							}
							if (array4[k] == "i")
							{
								array4[k] = "I";
							}
						}
						string text2 = string.Join(" ", array4);
						if (text2.Length > 0)
						{
							array3[j] = text2[0].ToString().ToUpper() + text2.Substring(1);
						}
					}
					text = string.Join(c + " ", array3);
				}
				text = text.Trim();
				bool flag = false;
				if (text.Length > 0)
				{
					char[] array5 = array;
					foreach (char c2 in array5)
					{
						if (text[text.Length - 1] == c2)
						{
							flag = true;
						}
					}
				}
				else
				{
					flag = true;
				}
				if (!flag)
				{
					return text + ".";
				}
				return text;
			}
			catch (Exception)
			{
				return input;
			}
		}
	}
}
