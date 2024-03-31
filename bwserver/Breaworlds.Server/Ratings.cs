using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breaworlds.Server
{
	internal class Ratings
	{
		public static readonly bool Active = true;

		public static readonly string Filename = "./ratings.bin";

		public static readonly string Temporary = "./ratings.tmp";

		public static RatingData Data = new RatingData
		{
			Winner = string.Empty,
			Rating = new Dictionary<string, int>(),
			YY = DateTime.UtcNow.Year,
			MM = DateTime.UtcNow.Month,
			DD = DateTime.UtcNow.Day
		};

		public static string[] Leaderboard = new string[0];

		public static void Initialize()
		{
			Deserialize();
			Loop(60000);
		}

		public static void Serialize()
		{
			try
			{
				FileStream output = new FileStream(Temporary, FileMode.Create, FileAccess.Write, FileShare.None);
				using (BinaryWriter binaryWriter = new BinaryWriter(output))
				{
					binaryWriter.Write(Data.YY);
					binaryWriter.Write(Data.MM);
					binaryWriter.Write(Data.DD);
					if (string.IsNullOrEmpty(Data.Winner))
					{
						binaryWriter.Write(string.Empty);
					}
					else
					{
						binaryWriter.Write(Data.Winner);
					}
					binaryWriter.Write(Data.Rating.Count);
					foreach (string key in Data.Rating.Keys)
					{
						if (string.IsNullOrEmpty(key))
						{
							binaryWriter.Write(string.Empty);
						}
						else
						{
							binaryWriter.Write(key);
						}
						if (Data.Rating.TryGetValue(key, out var value))
						{
							binaryWriter.Write(value);
						}
						else
						{
							binaryWriter.Write(0);
						}
					}
				}
				File.Copy(Temporary, Filename, overwrite: true);
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
			finally
			{
				if (File.Exists(Temporary))
				{
					File.Delete(Temporary);
				}
			}
		}

		public static void Deserialize()
		{
			try
			{
				RatingData data = default(RatingData);
				data.Winner = string.Empty;
				data.Rating = new Dictionary<string, int>();
				data.YY = DateTime.UtcNow.Year;
				data.MM = DateTime.UtcNow.Month;
				data.DD = DateTime.UtcNow.Day;
				Data = data;
				if (!File.Exists(Filename))
				{
					return;
				}
				FileStream input = new FileStream(Filename, FileMode.Open, FileAccess.Read, FileShare.Read);
				using BinaryReader binaryReader = new BinaryReader(input);
				Data.YY = binaryReader.ReadInt32();
				Data.MM = binaryReader.ReadInt32();
				Data.DD = binaryReader.ReadInt32();
				Data.Winner = binaryReader.ReadString();
				int num = binaryReader.ReadInt32();
				for (int i = 0; i < num; i++)
				{
					Data.Rating.Add(binaryReader.ReadString(), binaryReader.ReadInt32());
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
		}

		public static int Position(string name)
		{
			for (int i = 0; i < Leaderboard.Length; i++)
			{
				if (Leaderboard[i] == name)
				{
					return i + 1;
				}
			}
			return Leaderboard.Length + 1;
		}

		public static async void Loop(int timeout)
		{
			try
			{
				if (Data.YY == DateTime.UtcNow.Year && Data.MM == DateTime.UtcNow.Month && Data.DD == DateTime.UtcNow.Day)
				{
					Player[] array = Server.Online.ToArray();
					foreach (Player player2 in array)
					{
						if (player2 != null && player2.Active && player2.Profile.Active && player2.Session.Active && !(player2.Session.Data.Filename == "TUTORIAL") && !(player2.Session.Data.Filename == "BREAWORLDS") && !(player2.Session.Data.Filename == Data.Winner) && player2.Profile.Data.Level >= 5)
						{
							Data.Rating.TryGetValue(player2.Session.Data.Filename, out var value);
							Data.Rating[player2.Session.Data.Filename] = value + 1;
						}
					}
					Leaderboard = Data.Rating.Keys.ToArray();
					for (int x = 0; x < Leaderboard.Length; x++)
					{
						for (int y = 0; y < Leaderboard.Length; y++)
						{
							Data.Rating.TryGetValue(Leaderboard[x], out var valueX);
							Data.Rating.TryGetValue(Leaderboard[y], out var valueY);
							if (valueX > valueY)
							{
								string session = Leaderboard[x];
								Leaderboard[x] = Leaderboard[y];
								Leaderboard[y] = session;
							}
						}
					}
					Serialize();
				}
				else
				{
					string winner = string.Empty;
					if (Leaderboard.Length != 0)
					{
						winner = Leaderboard[0];
						MemoryStream stream = new MemoryStream();
						BinaryWriter writer = new BinaryWriter(stream);
						writer.Write(Convert.ToUInt16(0));
						writer.Write(Convert.ToUInt16(4));
						string message = $"~rThe world ~1{winner} ~ris now today's world of the day!";
						writer.Write(Encoding.UTF8.GetBytes(message + "\0"));
						writer.Seek(0, SeekOrigin.Begin);
						writer.Write(Convert.ToUInt16(stream.Length));
						Server.Broadcast(stream.ToArray(), delegate(Player player)
						{
							if (!player.Active)
							{
								return false;
							}
							if (!player.Profile.Active)
							{
								return false;
							}
							return player.Session.Active ? true : false;
						});
						writer.Close();
					}
					Leaderboard = new string[0];
					RatingData data = default(RatingData);
					data.Winner = winner;
					data.Rating = new Dictionary<string, int>();
					data.YY = DateTime.UtcNow.Year;
					data.MM = DateTime.UtcNow.Month;
					data.DD = DateTime.UtcNow.Day;
					Data = data;
					Serialize();
				}
				await Task.Delay(timeout);
			}
			catch (Exception ex)
			{
				Exception exception = ex;
				Terminal.Exception(exception);
			}
			finally
			{
				Loop(timeout);
			}
		}
	}
}
