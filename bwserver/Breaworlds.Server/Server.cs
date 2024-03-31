using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json.Nodes;
using System.Diagnostics;


namespace Breaworlds.Server
{	
	internal class Server
	{
		public static string Version = "3.8.3";

		public static string Message = string.Empty;

		public static Random Random = new Random();

		public static List<Player> Online = new List<Player>();

		public static List<string> Blacklist = new List<string>();

		public static DateTime Date = new DateTime(2019, 5, 14, 0, 0, 0);

		public static Socket Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

		private static void Main(string[] arguments)
		{
			try
			{
				Item.ValidateRarities();
				Item.ValidateRarityEquality();
				if (arguments.Length == 0)
				{
					Listen();
				}
				else if (arguments[0] == "profile")
				{
					Profile profile = new Profile();
					if (Database.ProfileExists(arguments[1]))
					{
						Database.ProfileLoad(ref profile.Data, arguments[1]);
						if (arguments[2] == "set")
						{
							string value = arguments[4];
							switch (arguments[3])
							{
								case "filename": 
									profile.Data.Filename = value;
									break;
								case "mailname": 
									profile.Data.Mailname = value;
									break;
								case "username": 
									profile.Data.Username = value;
									break;
								case "password": 
									profile.Data.Password = value;
									break;
								case "address": 
									profile.Data.Address = value;
									break;
								case "device": 
									profile.Data.Device = value;
									break;
								case "banned": 
									int.TryParse(value, out profile.Data.Data.BanDuration);
									break;
								case "muted": 
									int.TryParse(value, out profile.Data.Data.MuteDuration);
									break;
								case "level": 
									int.TryParse(value, out profile.Data.Data.Level);
									break;
								case "gems": 
									int.TryParse(value, out profile.Data.Data.Gems);
									break;
								case "rocks": 
									int.TryParse(value, out profile.Data.Data.MoonRocks);
									break;
								case "skin": 
									switch (value) {
										case "a":
											int.TryParse(arguments[5], out profile.Data.Data.SkinA);
											break;
										case "r":
											int.TryParse(arguments[5], out profile.Data.Data.SkinR);
											break;
										case "g":
											int.TryParse(arguments[5], out profile.Data.Data.SkinG);
											break;
										case "b":
											int.TryParse(arguments[5], out profile.Data.Data.SkinB);
											break;
									}
									break;
								case "item": 
									profile.SetItem(int.Parse(value), int.Parse(arguments[5]));
									break;
							}
						}
						if (arguments[2] == "add")
						{
							if (arguments[3] == "gems")
							{
								profile.Data.Gems += int.Parse(arguments[4]);
								Database.ProfileSave(profile.Data);
								Database.ProfileFlush(profile.Data.Filename);
							}
							if (arguments[3] == "rocks")
							{
								profile.Data.MoonRocks += int.Parse(arguments[4]);
								Database.ProfileSave(profile.Data);
								Database.ProfileFlush(profile.Data.Filename);
							}
						}
						if (arguments[2] == "remove")
						{
							if (arguments[3] == "gems")
							{
								profile.Data.Gems -= int.Parse(arguments[4]);
								Database.ProfileSave(profile.Data);
								Database.ProfileFlush(profile.Data.Filename);
							}
							if (arguments[3] == "rocks")
							{
								profile.Data.MoonRocks -= int.Parse(arguments[4]);
								Database.ProfileSave(profile.Data);
								Database.ProfileFlush(profile.Data.Filename);
							}
						}
						else if (arguments[2] == "get")
						{
							if (arguments[3] == "filename")
							{
								Console.WriteLine(profile.Data.Filename);
							}
							if (arguments[3] == "mailname")
							{
								Console.WriteLine(profile.Data.Mailname);
							}
							if (arguments[3] == "username")
							{
								Console.WriteLine(profile.Data.Username);
							}
							if (arguments[3] == "password")
							{
								Console.WriteLine(profile.Data.Password);
							}
							if (arguments[3] == "address")
							{
								Console.WriteLine(profile.Data.Address);
							}
							if (arguments[3] == "device")
							{
								Console.WriteLine(profile.Data.Device);
							}
							if (arguments[3] == "banned")
							{
								Console.WriteLine(profile.Data.BanDuration);
							}
							if (arguments[3] == "muted")
							{
								Console.WriteLine(profile.Data.MuteDuration);
							}
							if (arguments[3] == "level")
							{
								Console.WriteLine(profile.Data.Level);
							}
							if (arguments[3] == "gems")
							{
								Console.WriteLine(profile.Data.Gems);
							}
							if (arguments[3] == "2fa")
							{
								Console.WriteLine(profile.Data.StaffVerifiedDevice);
							}
							if (arguments[3] == "rocks")
							{
								Console.WriteLine(profile.Data.MoonRocks);
							}
							if (arguments[3] == "warnings")
							{
								Console.WriteLine(profile.Data.Warnings);
							}
							if (arguments[3] == "purchases")
							{
								foreach (string purchase in profile.Data.Purchases)
								{
									Console.WriteLine(purchase);
								}
							}
							if (arguments[3] == "friends")
							{
								foreach (string friend in profile.Data.Friends)
								{
									Console.WriteLine(friend);
								}
							}
							if (arguments[3] == "worlds")
							{
								foreach (string world in profile.Data.Worlds)
								{
									Console.WriteLine(world);
								}
							}
							if (arguments[3] == "items")
							{
								for (int i = 0; i < profile.Data.ItemIndex.Count; i++)
								{
									int item = profile.Data.ItemIndex[i];
									int num = profile.Data.ItemCount[i];
									Console.WriteLine("x{0} {1}", num, Item.Name(item));
								}
							}
							if (arguments[3] == "skin")
							{
								if (arguments[4] == "a")
								{
									Console.WriteLine(profile.Data.SkinA);
								}
								if (arguments[4] == "r")
								{
									Console.WriteLine(profile.Data.SkinR);
								}
								if (arguments[4] == "g")
								{
									Console.WriteLine(profile.Data.SkinG);
								}
								if (arguments[4] == "b")
								{
									Console.WriteLine(profile.Data.SkinB);
								}
							}
							if (arguments[3] == "item")
							{
								Console.WriteLine(profile.GetItem(int.Parse(arguments[4])));
							}
						}
						else
						{
							Console.WriteLine("Unknown third argument");
						}
						Database.ProfileSave(profile.Data);
						Database.ProfileFlush(profile.Data.Filename);
					}
					else
					{
						Console.WriteLine("Profile not found.");
					}
				}
				else if (arguments[0] == "session")
				{
					if (arguments[1] == "create")
					{
						string text = arguments[2];
						int.TryParse(arguments[3], out var result);
						int.TryParse(arguments[4], out var result2);
						SessionDataHandle sessionDataHandle = new SessionDataHandle(binded: false);
						sessionDataHandle.Filename = text.ToUpper();
						sessionDataHandle.Theme = 0;
						sessionDataHandle.Banned = 0;
						sessionDataHandle.Name = text.ToUpper();
						sessionDataHandle.Owner = "";
						sessionDataHandle.Drop = new List<DroppedItem>();
						sessionDataHandle.Admin = new List<string>();
						sessionDataHandle.Background = new ushort[result * result2];
						sessionDataHandle.Foreground = new ushort[result * result2];
						sessionDataHandle.Special = new object[result * result2];
						sessionDataHandle.Parent = new object[result * result2];
						sessionDataHandle.SizeX = result;
						sessionDataHandle.SizeY = result2;
						sessionDataHandle.CreateDate = DateTime.Now;
						SessionDataHandle sessionDataHandle2 = sessionDataHandle;
						sessionDataHandle2.Foreground[0] = 7;
						sessionDataHandle2.Foreground[sessionDataHandle2.SizeX] = 5;
						Database.SessionSave(sessionDataHandle2);
						Database.SessionFlush(sessionDataHandle2.Filename);
					}
				}
				else if (arguments[0] == "find")
				{
					if (arguments[1] == "device")
					{
						DirectoryInfo directoryInfo = new DirectoryInfo("./profiles/");
						FileInfo[] files = directoryInfo.GetFiles("*.bin");
						FileInfo[] array = files;
						foreach (FileInfo fileInfo in array)
						{
							try
							{
								string text2 = fileInfo.Name.Substring(0, fileInfo.Name.Length - 12);
								Profile profile2 = new Profile();
								Database.ProfileLoad(ref profile2.Data, text2);
								if (profile2.Data.Device == arguments[2])
								{
									Console.WriteLine(profile2.Data.Filename);
								}
								Database.ProfileCache.Remove(text2);
							}
							catch (Exception)
							{
								Console.WriteLine("Corrupted profile {0}", fileInfo.Name);
							}
						}
					}
					else if (arguments[1] == "rockteers")
					{
						int.TryParse(arguments[2], out var result3);
						DirectoryInfo directoryInfo2 = new DirectoryInfo("./profiles/");
						FileInfo[] files2 = directoryInfo2.GetFiles("*.bin");
						FileInfo[] array2 = files2;
						foreach (FileInfo fileInfo2 in array2)
						{
							try
							{
								string text3 = fileInfo2.Name.Substring(0, fileInfo2.Name.Length - 12);
								Profile profile3 = new Profile();
								Database.ProfileLoad(ref profile3.Data, text3);
								if (profile3.Data.MoonRocks >= result3)
								{
									Console.WriteLine("Found {0} in {1}", profile3.Data.MoonRocks, profile3.Data.Filename);
									Console.WriteLine("Setting to 0", profile3.Data.MoonRocks, profile3.Data.Filename);
									int.TryParse(arguments[3], out profile3.Data.Data.MoonRocks);
									Database.ProfileSave(profile3.Data);
									Database.ProfileFlush(profile3.Data.Filename);
								}
								Database.ProfileCache.Remove(text3);
							}
							catch (Exception)
							{
								Console.WriteLine("Corrupted profile {0}", fileInfo2.Name);
							}
						}
					}
					else if (arguments[1] == "rocks")
					{
						int.TryParse(arguments[2], out var result4);
						DirectoryInfo directoryInfo3 = new DirectoryInfo("./profiles/");
						FileInfo[] files3 = directoryInfo3.GetFiles("*.bin");
						FileInfo[] array3 = files3;
						foreach (FileInfo fileInfo3 in array3)
						{
							try
							{
								string text4 = fileInfo3.Name.Substring(0, fileInfo3.Name.Length - 12);
								Profile profile4 = new Profile();
								Database.ProfileLoad(ref profile4.Data, text4);
								if (profile4.Data.MoonRocks >= result4)
								{
									Console.WriteLine("Found {0} in {1}", profile4.Data.MoonRocks, profile4.Data.Filename);
								}
								Database.ProfileCache.Remove(text4);
							}
							catch (Exception)
							{
								Console.WriteLine("Corrupted profile {0}", fileInfo3.Name);
							}
						}
					}
					else if (arguments[1] == "username")
					{
						DirectoryInfo directoryInfo4 = new DirectoryInfo("./profiles/");
						FileInfo[] files4 = directoryInfo4.GetFiles("*.bin");
						FileInfo[] array4 = files4;
						foreach (FileInfo fileInfo4 in array4)
						{
							try
							{
								string text5 = fileInfo4.Name.Substring(0, fileInfo4.Name.Length - 12);
								Profile profile5 = new Profile();
								Database.ProfileLoad(ref profile5.Data, text5);
								if (profile5.Data.Username == arguments[2])
								{
									Console.WriteLine(profile5.Data.Filename);
								}
								Database.ProfileCache.Remove(text5);
							}
							catch (Exception)
							{
								Console.WriteLine("Corrupted profile {0}", fileInfo4.Name);
							}
						}
					}
					else if (arguments[1] == "address")
					{
						DirectoryInfo directoryInfo5 = new DirectoryInfo("./profiles/");
						FileInfo[] files5 = directoryInfo5.GetFiles("*.bin");
						FileInfo[] array5 = files5;
						foreach (FileInfo fileInfo5 in array5)
						{
							try
							{
								string text6 = fileInfo5.Name.Substring(0, fileInfo5.Name.Length - 12);
								Profile profile6 = new Profile();
								Database.ProfileLoad(ref profile6.Data, text6);
								if (profile6.Data.Address == arguments[2])
								{
									Console.WriteLine(profile6.Data.Filename);
								}
								Database.ProfileCache.Remove(text6);
							}
							catch (Exception)
							{
								Console.WriteLine("Corrupted profile {0}", fileInfo5.Name);
							}
						}
					}
					else if (arguments[1] == "email")
					{
						DirectoryInfo directoryInfo6 = new DirectoryInfo("./profiles/");
						FileInfo[] files6 = directoryInfo6.GetFiles("*.bin");
						FileInfo[] array6 = files6;
						foreach (FileInfo fileInfo6 in array6)
						{
							try
							{
								string text7 = fileInfo6.Name.Substring(0, fileInfo6.Name.Length - 12);
								Profile profile7 = new Profile();
								Database.ProfileLoad(ref profile7.Data, text7);
								if (profile7.Data.Mailname.ToUpper() == arguments[2].ToUpper())
								{
									Console.WriteLine(profile7.Data.Filename);
								}
								Database.ProfileCache.Remove(text7);
							}
							catch (Exception)
							{
								Console.WriteLine("Corrupted profile {0}", fileInfo6.Name);
							}
						}
					}
					else if (arguments[1] == "purchase")
					{
						DirectoryInfo directoryInfo7 = new DirectoryInfo("./profiles/");
						FileInfo[] files7 = directoryInfo7.GetFiles("*.bin");
						FileInfo[] array7 = files7;
						foreach (FileInfo fileInfo7 in array7)
						{
							try
							{
								string text8 = fileInfo7.Name.Substring(0, fileInfo7.Name.Length - 12);
								Profile profile8 = new Profile();
								Database.ProfileLoad(ref profile8.Data, text8);
								if (profile8.Data.Purchases.Contains(arguments[2]))
								{
									Console.WriteLine(profile8.Data.Filename);
								}
								Database.ProfileCache.Remove(text8);
							}
							catch (Exception)
							{
								Console.WriteLine("Corrupted profile {0}", fileInfo7.Name);
							}
						}
					}
					else if (arguments[1] == "item")
					{
						int.TryParse(arguments[2], out var result5);
						int.TryParse(arguments[3], out var result6);
						DirectoryInfo directoryInfo8 = new DirectoryInfo("./profiles/");
						FileInfo[] files8 = directoryInfo8.GetFiles("*.bin");
						FileInfo[] array8 = files8;
						foreach (FileInfo fileInfo8 in array8)
						{
							try
							{
								string text9 = fileInfo8.Name.Substring(0, fileInfo8.Name.Length - 12);
								Profile profile9 = new Profile();
								Database.ProfileLoad(ref profile9.Data, text9);
								if (profile9.GetItem(result5) >= result6)
								{
									Console.WriteLine("Found {0} in {1}", profile9.GetItem(result5), profile9.Data.Filename);
								}
								Database.ProfileCache.Remove(text9);
							}
							catch (Exception)
							{
								Console.WriteLine("Corrupted profile {0}", fileInfo8.Name);
							}
						}
					}
					else if (arguments[1] == "itemequal")
					{
						int.TryParse(arguments[2], out var result7);
						int.TryParse(arguments[3], out var result8);
						DirectoryInfo directoryInfo9 = new DirectoryInfo("./profiles/");
						FileInfo[] files9 = directoryInfo9.GetFiles("*.bin");
						FileInfo[] array9 = files9;
						foreach (FileInfo fileInfo9 in array9)
						{
							try
							{
								string text10 = fileInfo9.Name.Substring(0, fileInfo9.Name.Length - 12);
								Profile profile10 = new Profile();
								Database.ProfileLoad(ref profile10.Data, text10);
								if (profile10.GetItem(result7) == result8)
								{
									Console.WriteLine("Found {0} in {1}", profile10.GetItem(result7), fileInfo9.Name);
								}
								Database.ProfileCache.Remove(text10);
							}
							catch (Exception)
							{
								Console.WriteLine("Corrupted profile {0}", fileInfo9.Name);
							}
						}
					}
					else if (arguments[1] == "drop")
					{
						int.TryParse(arguments[2], out var result9);
						int.TryParse(arguments[3], out var result10);
						DirectoryInfo directoryInfo10 = new DirectoryInfo("./sessions/");
						FileInfo[] files10 = directoryInfo10.GetFiles("*.bin");
						FileInfo[] array10 = files10;
						foreach (FileInfo fileInfo10 in array10)
						{
							try
							{
								string text11 = fileInfo10.Name.Substring(0, fileInfo10.Name.Length - 12);
								Session session = new Session();
								Database.SessionLoad(ref session.Data, text11);
								foreach (DroppedItem item2 in session.Data.Drop)
								{
									if (item2.Index == result9 && item2.Count >= result10)
									{
										Console.WriteLine("Found {0} at {1}", item2.Count, session.Data.Name);
									}
								}
								Database.SessionCache.Remove(text11);
							}
							catch (Exception)
							{
								Console.WriteLine("Corrupted session {0}", fileInfo10.Name);
							}
						}
					}
					else if (arguments[1] == "rocks")
					{
						int.TryParse(arguments[2], out var result11);
						DirectoryInfo directoryInfo11 = new DirectoryInfo("./profiles/");
						FileInfo[] files11 = directoryInfo11.GetFiles("*.bin");
						FileInfo[] array11 = files11;
						foreach (FileInfo fileInfo11 in array11)
						{
							try
							{
								string text12 = fileInfo11.Name.Substring(0, fileInfo11.Name.Length - 12);
								Profile profile11 = new Profile();
								Database.ProfileLoad(ref profile11.Data, text12);
								if (profile11.Data.MoonRocks >= result11)
								{
									Console.WriteLine(profile11.Data.Filename);
								}
								Database.ProfileCache.Remove(text12);
							}
							catch (Exception)
							{
								Console.WriteLine("Corrupted profile {0}", fileInfo11.Name);
							}
						}
					}
					else if (arguments[1] == "2fa")
					{
						DirectoryInfo directoryInfo12 = new DirectoryInfo("./profiles/");
						FileInfo[] files12 = directoryInfo12.GetFiles("*.bin");
						FileInfo[] array12 = files12;
						foreach (FileInfo fileInfo12 in array12)
						{
							try
							{
								string text13 = fileInfo12.Name.Substring(0, fileInfo12.Name.Length - 12);
								Profile profile12 = new Profile();
								Database.ProfileLoad(ref profile12.Data, text13);
								if (profile12.Data.StaffVerifiedDevice == arguments[2])
								{
									Console.WriteLine(profile12.Data.Filename);
								}
								Database.ProfileCache.Remove(text13);
							}
							catch (Exception)
							{
								Console.WriteLine("Corrupted profile {0}", fileInfo12.Name);
							}
						}
					}
					else if (arguments[1] == "gems")
					{
						int.TryParse(arguments[2], out var result12);
						DirectoryInfo directoryInfo13 = new DirectoryInfo("./profiles/");
						FileInfo[] files13 = directoryInfo13.GetFiles("*.bin");
						FileInfo[] array13 = files13;
						foreach (FileInfo fileInfo13 in array13)
						{
							try
							{
								string text14 = fileInfo13.Name.Substring(0, fileInfo13.Name.Length - 12);
								Profile profile13 = new Profile();
								Database.ProfileLoad(ref profile13.Data, text14);
								if (profile13.Data.Gems >= result12)
								{
									Console.WriteLine(profile13.Data.Filename);
								}
								Database.ProfileCache.Remove(text14);
							}
							catch (Exception)
							{
								Console.WriteLine("Corrupted profile {0}", fileInfo13.Name);
							}
						}
					}
					else
					{
						Console.WriteLine("Unknown second argument.");
					}
				}
				else if (arguments[0] == "clean")
				{
					if (arguments[1] == "sessions")
					{
						DirectoryInfo directoryInfo14 = new DirectoryInfo("./sessions/");
						FileInfo[] files14 = directoryInfo14.GetFiles("*.bin");
						Session session2 = new Session();
						FileInfo[] array14 = files14;
						foreach (FileInfo fileInfo14 in array14)
						{
							try
							{
								string text15 = fileInfo14.Name.Substring(0, fileInfo14.Name.Length - 12);
								Database.SessionLoad(ref session2.Data, text15);
								if (fileInfo14.LastWriteTime < DateTime.UtcNow.AddDays(-3.0))
								{
									List<ushort> list = new List<ushort>();
									list.AddRange(session2.Data.Foreground);
									if (!list.Contains(319) && !list.Contains(321) && !list.Contains(323) && !list.Contains(73) && !list.Contains(493))
									{
										string path = Database.SessionDirectory + session2.Data.Name.ToUpper() + Database.SessionExtension;
										File.Delete(path);
										Console.WriteLine("Deleted world {0}, owned by {1}, last visited {2}", session2.Data.Name, session2.Data.Owner, fileInfo14.LastWriteTime);
									}
								}
								Database.SessionCache.Remove(text15);
							}
							catch (Exception)
							{
								Console.WriteLine("Corrupted session {0}", fileInfo14.Name);
							}
						}
					}
					else if (arguments[1] == "profiles")
					{
						DirectoryInfo directoryInfo15 = new DirectoryInfo("./profiles/");
						FileInfo[] files15 = directoryInfo15.GetFiles("*.bin");
						Profile profile14 = new Profile();
						FileInfo[] array15 = files15;
						foreach (FileInfo fileInfo15 in array15)
						{
							try
							{
								string text16 = fileInfo15.Name.Substring(0, fileInfo15.Name.Length - 12);
								Database.ProfileLoad(ref profile14.Data, text16);
								if (fileInfo15.LastWriteTime < DateTime.UtcNow.AddDays(-3.0))
								{
									if (profile14.Data.Device == string.Empty)
									{
										File.Delete(fileInfo15.FullName);
										Console.WriteLine("Deleted account {0}, never logged in.", profile14.Data.Filename);
									}
									if (profile14.GetBan() > 0 && profile14.Data.Level <= 3)
									{
										File.Delete(fileInfo15.FullName);
										Console.WriteLine("Deleted account {0}, banned low level.", profile14.Data.Filename);
									}
								}
								Database.ProfileCache.Remove(text16);
							}
							catch (Exception)
							{
								Console.WriteLine("Corrupted profile {0}", fileInfo15.Name);
							}
						}
					}
					else if (arguments[1] == "temporary")
					{
						if (arguments[2] == "profiles")
						{
							DirectoryInfo directoryInfo16 = new DirectoryInfo("./profiles/");
							FileInfo[] files16 = directoryInfo16.GetFiles("*.profile.tmp");
							FileInfo[] array16 = files16;
							foreach (FileInfo fileInfo16 in array16)
							{
								Console.WriteLine("Deleted profile {0}", fileInfo16.Name);
								File.Delete(fileInfo16.FullName);
							}
						}
						else if (arguments[2] == "sessions")
						{
							DirectoryInfo directoryInfo17 = new DirectoryInfo("./sessions/");
							FileInfo[] files17 = directoryInfo17.GetFiles("*.session.tmp");
							FileInfo[] array17 = files17;
							foreach (FileInfo fileInfo17 in array17)
							{
								Console.WriteLine("Deleted session {0}", fileInfo17.Name);
								File.Delete(fileInfo17.FullName);
							}
						}
						else
						{
							Console.WriteLine("Can't clean specified temporary data, name is unkown.");
						}
					}
					else
					{
						Console.WriteLine("Can't clean specified data type, name is unknown.");
					}
				}
				else
				{
					if (!(arguments[0] == "generate"))
					{
						return;
					}
					if (arguments[1] == "item_data.txt")
					{
						Console.WriteLine("Generating file {0}...", arguments[1]);
						StreamWriter streamWriter = new StreamWriter("./item_data.txt");
						foreach (ItemData item3 in Item.List)
						{
							streamWriter.WriteLine("{0,-8} {1,-32} {2,-8} {3,-8}", item3.ID, item3.Name, item3.Type, item3.Rarity);
						}
						streamWriter.Close();
					}
					else if (arguments[1] == "database_statistics.txt")
					{
						Console.WriteLine("Generating file {0}...", arguments[1]);
						StreamWriter streamWriter2 = new StreamWriter("./database_statistics.txt");
						DirectoryInfo directoryInfo18 = new DirectoryInfo(Database.SessionDirectory);
						DirectoryInfo directoryInfo19 = new DirectoryInfo(Database.ProfileDirectory);
						int num14 = 0;
						int num15 = 0;
						long num16 = 0L;
						long num17 = 0L;
						FileInfo[] files18 = directoryInfo18.GetFiles();
						foreach (FileInfo fileInfo18 in files18)
						{
							num16 += fileInfo18.Length;
							num14++;
						}
						FileInfo[] files19 = directoryInfo19.GetFiles();
						foreach (FileInfo fileInfo19 in files19)
						{
							num17 += fileInfo19.Length;
							num15++;
						}
						streamWriter2.WriteLine("Breaworlds database statistics");
						streamWriter2.WriteLine("---------------------------------------------------------------");
						streamWriter2.WriteLine("Total size of sessions: {0:0.00} MB", (double)num16 / 1000000.0);
						streamWriter2.WriteLine("Total size of profiles: {0:0.00} MB", (double)num17 / 1000000.0);
						streamWriter2.WriteLine("Total size: {0:0.00} MB", (double)(num16 + num17) / 1000000.0);
						streamWriter2.WriteLine("---------------------------------------------------------------");
						streamWriter2.WriteLine("Total sessions saved: {0}", num14);
						streamWriter2.WriteLine("Total profiles saved: {0}", num15);
						streamWriter2.WriteLine("Total saved: {0}", num14 + num15);
						streamWriter2.WriteLine("---------------------------------------------------------------");
						streamWriter2.WriteLine("Generated at {0}", DateTime.UtcNow);
						streamWriter2.Close();
					}
					else
					{
						Console.WriteLine("Can't generate specified file, name is unknown.");
					}
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				Thread.Sleep(10000);
				Main(arguments);
			}
		}

		public static void Listen()
		{
			Console.WriteLine("Server has been started. [IPv4]");
			Socket.Bind(new IPEndPoint(IPAddress.Any, 1801));
			Socket.NoDelay = true;
			Socket.Listen(2);
			Database.Initialize();
			Punishment.Initialize();
			Challenge.Initialize();
			Ratings.Initialize();
			while (Socket.IsBound)
			{
				Accept();
			}
		}

		public static void Accept()
		{
			try
			{
				if (Online.Count < 10000)
				{
					Socket socket = Socket.Accept();
					if (Acceptable(socket, 5))
					{
						Connect(new Player(socket));
					}
					else
					{
						socket.Close();
					}
				}
				else
				{
					Thread.Sleep(1000);
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
		}

		public static void Connect(Player player)
		{
			Console.WriteLine("CONNECT!");

			string ip = player.Profile.Address;
			string fwpath = System.IO.Directory.GetCurrentDirectory() + @"\fw_ips.txt";
			string path = System.IO.Directory.GetCurrentDirectory() + @"\legit_ips.txt";

			bool skip = false;

			Console.WriteLine(ip);

			foreach (string line in File.ReadLines(path))
				if (line == ip) {
					Console.WriteLine("NICE LIST!");
					skip = true;
				}

			if (!skip) {
				foreach (string line in File.ReadLines(fwpath))
					if (line == ip) {
						if (player.Socket.Connected) {
							Console.WriteLine("SIKE LIST!");
							player.Socket.Close();
							// Sike! Thats the wrong IP (number)
						}
						return;
					}
				
				using (WebClient wc = new WebClient())
				{

					var json = wc.DownloadString("https://proxycheck.io/v2/" + ip + "?key=8074kv-7b0392-l8y564-u46474");

					JsonNode proxyResult = JsonNode.Parse(json);

					Console.WriteLine((string)proxyResult["status"]);

					if ((string)proxyResult["status"] != "ok")
					{
						Console.WriteLine("SIKE! STATUS NOT OK!");

						AddIp(player, fwpath);
						return;
					}
					else
					{
						Console.WriteLine(proxyResult[ip]);
						if ((string)proxyResult[ip]["proxy"] != "no")
						{
							Console.WriteLine("SIKE! IS PROXY!");
							AddIp(player, fwpath);
							return;
						}
						else if ((string)proxyResult[ip]["type"] != "Residential" && (string)proxyResult[ip]["type"] != "Wireless" && (string)proxyResult[ip]["type"] != "whitelisted by " + ip && (string)proxyResult[ip]["type"] != "Business") {
							Console.WriteLine("SIKE! IS NOT HOME!");
							AddIp(player, fwpath);
							return;
						}
					}
					
				}
				Console.WriteLine("ALLOW!");

				using (StreamWriter sw = File.AppendText(path))
				{
					sw.WriteLine(ip);
				}
			}

			try
			{
				Online.Add(player);
				Terminal.Message("Client {0} connected from {1}, {2} online.", player.Identifier, player.Profile.Address, Online.Count);
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
		}

		private static void AddIp(Player player, string path) {
			string ip = player.Profile.Address;

			if (player.Socket.Connected) {
				Console.WriteLine("SIKE! DIE!");
				player.Socket.Close();
				// Sike! Thats the wrong IP (number)
			}

			string strCmdText = "netsh advfirewall firewall add rule name=BlockProxy dir=in profile=any action=block protocol=TCP localport=1800 remoteip=" + ip;
			Console.WriteLine(strCmdText);
			Console.WriteLine(path);
			using (StreamWriter sw = File.AppendText(path))
        	{
    	        sw.WriteLine(ip);
			}

			Process cmd = new Process();
			cmd.StartInfo.FileName = "cmd.exe";
			cmd.StartInfo.RedirectStandardInput = true;
			cmd.StartInfo.RedirectStandardOutput = true;
			cmd.StartInfo.CreateNoWindow = true;
			cmd.StartInfo.UseShellExecute = false;
			cmd.Start();
			
			cmd.StandardInput.WriteLine(strCmdText);
			cmd.StandardInput.Flush();
			cmd.StandardInput.Close();
			cmd.WaitForExit();
		}

		public static void Disconnect(Player player)
		{
			try
			{
				if (Online.Contains(player))
				{
					Online.Remove(player);
				}
				if (player.Socket != null)
				{
					player.Socket.Close();
				}
				if (player.Valid)
				{
					Terminal.Message("Client {0} disconnected from {1}, {2} online.", player.Identifier, player.Profile.Address, Online.Count);
				}
				else
				{
					Terminal.Message("Invalid client {0} terminated from {1}, {2} online.", player.Identifier, player.Profile.Address, Online.Count);
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
			
		}

		public static bool Acceptable(Socket socket, int limit)
		{
			try
			{
				int num = 0;
				Player[] array = Online.ToArray();
				foreach (Player player in array)
				{
					if (!player.Closed && player.Socket != null && player.Socket.RemoteEndPoint != null)
					{
						IPEndPoint iPEndPoint = (IPEndPoint)socket.RemoteEndPoint;
						IPEndPoint iPEndPoint2 = (IPEndPoint)player.Socket.RemoteEndPoint;
						if (iPEndPoint.Address.Equals(iPEndPoint2.Address))
						{
							num++;
						}
					}
				}
				if (num >= limit)
				{
					return false;
				}
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public static int Broadcast(byte[] data, Func<Player, bool> callback)
		{
			int num = 0;
			Player[] array = Online.ToArray();
			foreach (Player player in array)
			{
				if (callback(player))
				{
					player.Send(data);
					num++;
				}
			}
			return num;
		}

		public static void Restart(int seconds)
		{
			Task.Run(delegate
			{
				int num = seconds;
				while (num >= 0)
				{
					if (num == 0)
					{
						MemoryStream memoryStream = new MemoryStream();
						BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
						binaryWriter.Write(Convert.ToUInt16(0));
						binaryWriter.Write(Convert.ToUInt16(35));
						binaryWriter.Write(Convert.ToUInt16(500));
						binaryWriter.Write(Convert.ToUInt16(3));
						binaryWriter.Write(Encoding.UTF8.GetBytes("~1SERVER IS ~3RESTARTING~1!\0"));
						binaryWriter.Write(Encoding.UTF8.GetBytes("The server is now restarting, we will be back shortly.\0"));
						binaryWriter.Write(Encoding.UTF8.GetBytes("Thank you for your patience.\0"));
						binaryWriter.Seek(0, SeekOrigin.Begin);
						binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
						Player[] array = Online.ToArray();
						foreach (Player player in array)
						{
							if (player.Active && player.Profile.Active && player.Session.Active)
							{
								player.Send(memoryStream.ToArray());
							}
						}
						binaryWriter.Close();
					}
					else
					{
						MemoryStream memoryStream2 = new MemoryStream();
						BinaryWriter binaryWriter2 = new BinaryWriter(memoryStream2);
						binaryWriter2.Write(Convert.ToUInt16(0));
						binaryWriter2.Write(Convert.ToUInt16(4));
						string text = $"~3Restarting the server in ~1{Text.TimeLong(num)}~3 to apply changes.";
						binaryWriter2.Write(Encoding.UTF8.GetBytes(text + "\0"));
						binaryWriter2.Seek(0, SeekOrigin.Begin);
						binaryWriter2.Write(Convert.ToUInt16(memoryStream2.Length));
						Player[] array2 = Online.ToArray();
						foreach (Player player2 in array2)
						{
							if (player2.Active && player2.Profile.Active && player2.Session.Active)
							{
								player2.Send(memoryStream2.ToArray());
							}
						}
						binaryWriter2.Close();
						Console.WriteLine("Restarting the server in {0}.", Text.TimeLong(num));
					}
					if (num > 60)
					{
						num -= 60;
						Thread.Sleep(60000);
					}
					else if (num > 10)
					{
						num -= 10;
						Thread.Sleep(10000);
					}
					else if (num > 5)
					{
						num -= 5;
						Thread.Sleep(5000);
					}
					else if (num >= 0)
					{
						num--;
						Thread.Sleep(1000);
					}
				}
				Player[] array3 = Online.ToArray();
				foreach (Player player3 in array3)
				{
					Database.ProfileSave(player3.Profile.Data);
					player3.Close();
				}
				Console.WriteLine("Server closing.");
				Environment.Exit(0);
			});
		}

		public static async void SendLog(string username, string message)
		{
			try
			{
				await Task.Delay(1);
				using WebClient client = new WebClient();
				NameValueCollection data = new NameValueCollection
				{
					{ "username", username },
					{ "content", message }
				};
				client.UploadValues("https://discord.com/api/webhooks/937060250887016508/NLPq8DAxbpaK44t-9zBXvQ_K9cLEPgvgj2u-RZX-8GOeQII2n-_VHEFR5gbX8zSyPh7X", data);
			}
			catch (Exception ex)
			{
				Exception exception = ex;
				Terminal.Exception(exception);
			}
		}

		public static async void DropLogs(string username, string message)
		{
			try
			{
				await Task.Delay(1);
				using WebClient client = new WebClient();
				NameValueCollection data = new NameValueCollection
				{
					{ "username", username },
					{ "content", message }
				};
				client.UploadValues("https://discord.com/api/webhooks/937060250887016508/NLPq8DAxbpaK44t-9zBXvQ_K9cLEPgvgj2u-RZX-8GOeQII2n-_VHEFR5gbX8zSyPh7X", data);
			}
			catch (Exception ex)
			{
				Exception exception = ex;
				Terminal.Exception(exception);
			}
		}

		public static async void TradeLogs(string username, string message)
		{
			try
			{
				await Task.Delay(1);
				using WebClient client = new WebClient();
				NameValueCollection data = new NameValueCollection
				{
					{ "username", username },
					{ "content", message }
				};
				client.UploadValues("https://discord.com/api/webhooks/937060250887016508/NLPq8DAxbpaK44t-9zBXvQ_K9cLEPgvgj2u-RZX-8GOeQII2n-_VHEFR5gbX8zSyPh7X", data);
			}
			catch (Exception ex)
			{
				Exception exception = ex;
				Terminal.Exception(exception);
			}
		}

		public static async void SendTestLog(string username, string message)
		{
			try
			{
				await Task.Delay(1);
				using WebClient client = new WebClient();
				NameValueCollection data = new NameValueCollection
				{
					{ "username", username },
					{ "content", message }
				};
				client.UploadValues("https://discord.com/api/webhooks/937060250887016508/NLPq8DAxbpaK44t-9zBXvQ_K9cLEPgvgj2u-RZX-8GOeQII2n-_VHEFR5gbX8zSyPh7X", data);
			}
			catch (Exception ex)
			{
				Exception exception = ex;
				Terminal.Exception(exception);
			}
		}

		public static async void SendReason(string username, string message)
		{
			try
			{
				await Task.Delay(1);
				using WebClient client = new WebClient();
				NameValueCollection data = new NameValueCollection
				{
					{ "username", username },
					{ "content", message }
				};
				client.UploadValues("https://discord.com/api/webhooks/937060250887016508/NLPq8DAxbpaK44t-9zBXvQ_K9cLEPgvgj2u-RZX-8GOeQII2n-_VHEFR5gbX8zSyPh7X", data);
			}
			catch (Exception ex)
			{
				Exception exception = ex;
				Terminal.Exception(exception);
			}
		}

		public static async void SendReport(string username, string message)
		{
			try
			{
				await Task.Delay(1);
				using WebClient client = new WebClient();
				NameValueCollection data = new NameValueCollection
				{
					{ "username", username },
					{ "content", message }
				};
				client.UploadValues("https://discord.com/api/webhooks/937060250887016508/NLPq8DAxbpaK44t-9zBXvQ_K9cLEPgvgj2u-RZX-8GOeQII2n-_VHEFR5gbX8zSyPh7X", data);
			}
			catch (Exception ex)
			{
				Exception exception = ex;
				Terminal.Exception(exception);
			}
		}
	}
}
