using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Breaworlds.Server
{
	internal class Tournament
	{
		public static readonly bool Active = true;

		public static readonly string Filename = "./tournament.bin";

		public static readonly string Temporary = "./tournament.tmp";

		public static TournamentData Data = new TournamentData
		{
			YY = DateTime.UtcNow.Year,
			MM = DateTime.UtcNow.Month,
			DD = DateTime.UtcNow.Day,
			Winner1 = string.Empty,
			Winner2 = string.Empty,
			Winner3 = string.Empty,
			Type = TournamentType.None,
			Points = new Dictionary<string, int>()
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
					binaryWriter.Write(string.IsNullOrEmpty(Data.Winner1) ? string.Empty : Data.Winner1);
					binaryWriter.Write(string.IsNullOrEmpty(Data.Winner2) ? string.Empty : Data.Winner2);
					binaryWriter.Write(string.IsNullOrEmpty(Data.Winner3) ? string.Empty : Data.Winner3);
					binaryWriter.Write(Convert.ToInt32(Data.Type));
					binaryWriter.Write(Data.Points.Count);
					foreach (string key in Data.Points.Keys)
					{
						binaryWriter.Write(string.IsNullOrEmpty(key) ? string.Empty : key);
						if (Data.Points.TryGetValue(key, out var value))
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
				TournamentData data = default(TournamentData);
				data.Winner1 = string.Empty;
				data.Winner2 = string.Empty;
				data.Winner3 = string.Empty;
				data.Points = new Dictionary<string, int>();
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
				Data.Winner1 = binaryReader.ReadString();
				Data.Winner2 = binaryReader.ReadString();
				Data.Winner3 = binaryReader.ReadString();
				Data.Type = (TournamentType)binaryReader.ReadInt32();
				int num = binaryReader.ReadInt32();
				for (int i = 0; i < num; i++)
				{
					Data.Points.Add(binaryReader.ReadString(), binaryReader.ReadInt32());
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
		}

		public static void Gain(Player player, TournamentType type)
		{
			try
			{
				if (Data.Type == type)
				{
					Data.Points.TryGetValue(player.Profile.Data.Filename, out var value);
					Data.Points[player.Profile.Data.Filename] = value + 1;
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
		}

		public static async void Loop(int timeout)
		{
			try
			{
				if (Data.YY == DateTime.UtcNow.Year && Data.MM == DateTime.UtcNow.Month && Data.DD == DateTime.UtcNow.Day)
				{
					Leaderboard = Data.Points.Keys.ToArray();
					for (int x = 0; x < Leaderboard.Length; x++)
					{
						for (int y = 0; y < Leaderboard.Length; y++)
						{
							Data.Points.TryGetValue(Leaderboard[x], out var valueX);
							Data.Points.TryGetValue(Leaderboard[y], out var valueY);
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
					string winner1 = string.Empty;
					string winner2 = string.Empty;
					string winner3 = string.Empty;
					if (Leaderboard.Length > 2)
					{
						winner1 = Leaderboard[0];
						winner2 = Leaderboard[1];
						winner3 = Leaderboard[2];
						Player[] array = Server.Online.ToArray();
						foreach (Player player in array)
						{
							if (player.Active && player.Profile.Active && player.Session.Active)
							{
								PlayerConsole.Message(player, "Today's tournament has ended, calculating results.");
								PlayerConsole.Message(player, "Tournament place #1: {0}", winner1);
								PlayerConsole.Message(player, "Tournament place #2: {0}", winner2);
								PlayerConsole.Message(player, "Tournament place #3: {0}", winner3);
							}
						}
					}
					Leaderboard = new string[0];
					TournamentData data = default(TournamentData);
					data.YY = DateTime.UtcNow.Year;
					data.MM = DateTime.UtcNow.Month;
					data.DD = DateTime.UtcNow.Day;
					data.Winner1 = winner1;
					data.Winner2 = winner2;
					data.Winner3 = winner3;
					data.Type = TournamentType.None;
					data.Points = new Dictionary<string, int>();
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
