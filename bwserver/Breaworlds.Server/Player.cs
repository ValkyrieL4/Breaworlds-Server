using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Breaworlds.Server
{
	public class Player
	{
		public bool Valid;

		public bool Active;

		public bool Closed;

		public bool Loaded;

		public Socket Socket;

		private byte[] Buffer;

		public int Identifier;

		public int Accounts;

		public Profile Profile;

		public Session Session;

		public bool GameWaiting;

		public bool GamePlaying;

		public int GameID;

		public int TradeID;

		public int FriendID;

		public int WrenchID;

		public int ShopPage;

		public string Window;

		public long LastWarped;

		public long LastFished;

		public long LastPunched;

		public long LastMessage;

		public long LastGlobalMessage;

		public int Action;

		public int CurrentX;

		public int CurrentY;

		public int PreviousX;

		public int PreviousY;

		public bool Fishing;

		public bool FishingDone;

		public int FishingIdentifier;

		public int FishingTries;

		public int FishingBait;

		public int FishingX;

		public int FishingY;

		public int TimerIdentifier;

		public bool Trading;

		public bool TradeAccepted;

		public bool TradeCanceled;

		public bool TradeReviewed;

		public List<SlotData> TradingOffer;

		public DateTime ServerPingTime;

		public DateTime ClientPingTime;

		public Player(Socket socket)
		{
			try
			{
				Socket = socket;
				Socket.NoDelay = true;
				Buffer = new byte[2];
				Profile = new Profile();
				Session = new Session();
				Console.WriteLine("P: {0}", ((IPEndPoint)Socket.RemoteEndPoint).Port);
				Profile.Address = ((IPEndPoint)Socket.RemoteEndPoint).Address.ToString();
				PlayerCore.Schedule(this, 10, 0, delegate(Player invoker, int identifier)
				{
					if (!invoker.Valid && !invoker.Closed)
					{
						Console.WriteLine("Caught invalid client {0} at timed validator", invoker.Profile.Address);
						invoker.Close();
					}
				});
				Thread thread = new Thread((ThreadStart)delegate
				{
					Handle();
				});
				Identifier = thread.ManagedThreadId;
				thread.Start();
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				Close();
			}
		}

		private void Handle()
		{
			try
			{
				PunishmentData punishment;
				PunishmentData punishment2;
				PunishmentData punishment3;
				string[] arguments;
				bool delivered;
				string username;
				int warnings;
				ushort worker;
				ShopItem[] items3;
				ShopItem[] items2;
				ShopItem[] items;
				RewardItem reward;
				int gems;
				HalloweenEnemyData data3;
				HalloweenEnemyData data2;
				ShopCategory category;
				ShopListing listing2;
				while (!Closed)
				{
					Buffer = new byte[2];
					if (Socket.Receive(Buffer, 0, Buffer.Length, SocketFlags.None) != Buffer.Length)
					{
						Close();
						break;
					}
					int i = Buffer.Length;
					int num = BitConverter.ToUInt16(Buffer, 0);
					Array.Resize(ref Buffer, num);
					int num2;
					for (; i < num; i += num2)
					{
						num2 = Socket.Receive(Buffer, i, num - i, SocketFlags.None);
						if (num2 == 0)
						{
							Close();
							break;
						}
					}
					MemoryStream input = new MemoryStream(Buffer);
					BinaryReader binaryReader = new BinaryReader(input);
					ushort num3 = binaryReader.ReadUInt16();
					switch (binaryReader.ReadUInt16())
					{
					case 0:
						Profile.PlatformType = binaryReader.ReadInt16();
						Profile.PlatformVersion = binaryReader.ReadInt32();
						Profile.Device = binaryReader.ReadString();
						Profile.Version = binaryReader.ReadString();
						Profile.Country = binaryReader.ReadString();
						binaryReader.Close();
						if (Profile.Version == Server.Version)
						{
							Active = true;
							PlayerConsole.Message(this, "Connected to the server successfully.");
						}
						else
						{
							Active = false;
							PlayerCore.UpdateDialog(this, 0, "Update", delegate(BinaryWriter dialog)
							{
								Dialog.ItemText(dialog, breaker: true, "~1Update required", 75, 3);
								Dialog.Text(dialog, breaker: true, "Please download the latest update before you", 50);
								Dialog.Text(dialog, breaker: true, "can play online. It might take some time for", 50);
								Dialog.Text(dialog, breaker: true, "the update to appear in some stores.", 50);
								Dialog.Button(dialog, breaker: true, "Cancel", "Cancel");
							});
						}
						Valid = true;
						break;
					case 1:
					{
						string text105 = binaryReader.ReadString();
						string password = binaryReader.ReadString();
						ushort accounts2 = binaryReader.ReadUInt16();
						binaryReader.Close();
						if (Profile.Active || !Active)
						{
							break;
						}
						Accounts = accounts2;
						if (Punishment.HostBanned(Profile.Address, out punishment))
						{
							PlayerAccount.ResponseLogin(this, false, null, null, "~3Your location is banned, this ban will expire in ~1{0}~3, it's caused by ~1{1}~3.", Text.Time(Punishment.Left(punishment.Time)), punishment.Name);
							PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
							{
								Dialog.ItemText(dialog, breaker: true, $"~1You are banned for {Text.Time(Punishment.Left(punishment.Time))}", 75, 3);
								Dialog.Text(dialog, breaker: true, "Your device and/or location has been banned because of", 50);
								Dialog.Text(dialog, breaker: true, $"what you did on account named ~1{punishment.Name}~0.", 50);
								Dialog.Space(dialog);
								Dialog.Text(dialog, breaker: true, "If you didn't do anything wrong and don't own the account", 50);
								Dialog.Text(dialog, breaker: true, "that's mentioned above, it might be someone else who has", 50);
								Dialog.Text(dialog, breaker: true, "recently played using your device or network. If you are", 50);
								Dialog.Text(dialog, breaker: true, "using public network, connecting to another one may help", 50);
								Dialog.Text(dialog, breaker: true, "you solve this issue. If you still think there's a problem", 50);
								Dialog.Text(dialog, breaker: true, "contact us at ~1support@breaworldsgame.com~0.", 50);
								Dialog.Button(dialog, breaker: true, "Okay", "Okay");
							});
						}
						else if (Database.ProfileExists(text105))
						{
							Database.ProfileLoad(ref Profile.Data, text105);
							switch (Profile.Login(password))
							{
							case 0:
							{
								bool flag21 = false;
								Player[] array67 = Server.Online.ToArray();
								foreach (Player player62 in array67)
								{
									try
									{
										if (player62 != this && player62.Active && player62.Profile.Active && !(player62.Profile.Data.Filename != Profile.Data.Filename))
										{
											player62.Close();
											flag21 = true;
										}
									}
									catch (Exception exception)
									{
										Terminal.Exception(exception);
									}
								}
								if (flag21)
								{
									PlayerConsole.Message(this, "~3This account was already online, kicked it out.");
								}
								if (Profile.Data.Filename != Profile.Data.Username && !Rewards.Permission(Profile.Data.Filename, Permissions.Mod))
								{
									Profile.Data.Username = Profile.Data.Filename;
								}
								if (Profile.StaffVerifiedDevice != Profile.Device && Rewards.Permission(Profile.Data.Filename, Permissions.Guard) && Profile.StaffVerifiedDevice != "none")
								{
									PlayerAccount.ResponseLogin(this, false, null, null, "Your current device ID is not authorized for this account, contact an administrator.");
									Server.SendTestLog(text105, "Has logged in from an unauthorized device: " + Profile.Device);
									break;
								}
								PlayerAccount.ResponseLogin(this, true, Profile.Data.Filename, Profile.Data.Password, "You've logged in as ~1{0} ~0successfully.", Profile.Data.Username);
								if (Rewards.Permission(Profile.Data.Filename, Permissions.Guard) && Profile.Data.Filename != "Zyliss" && Profile.Data.Address != "127.0.0.1")
								{
									Server.SendTestLog(Profile.Data.Filename, "Has logged in from IP: " + Profile.Data.Address + " and Device: " + Profile.Data.Device);
								}
								PlayerCore.UpdateInventory(this);
								PlayerCore.UpdateGems(this);
								PlayerCore.UpdateSpecialCurrency(this);
								PlayerCore.UpdateOptions(this);
								if (Profile.Data.StaffVerifiedDevice == "none" && Rewards.Permission(Profile.Data.Filename, Permissions.Guard))
								{
									PlayerConsole.Message(this, "~12FA IS ~3DISABLED~1! You are required to verify this device by going to Account Options.");
								}
								PlayerCore.UpdateDialog(this, 0, "WelcomeMessage", delegate(BinaryWriter dialog)
								{
									Dialog.ItemText(dialog, breaker: true, "~1Breaworlds Gazette", 75, 3);
									Dialog.ItemImage(dialog, breaker: true, 200, 5);
									Dialog.Text(dialog, breaker: true, "~1- ~0Female Gender has been added", 40);
									Dialog.Text(dialog, breaker: true, "~0(can be changed via account options)", 40);
									Dialog.Text(dialog, breaker: true, "~1- ~0Character has been re-worked with shading,", 40);
									Dialog.Text(dialog, breaker: true, " ~0and some items have been re-designed", 40);
									Dialog.Text(dialog, breaker: true, "~1- ~0Breaworlds has been sent into outerspace,", 40);
									Dialog.Text(dialog, breaker: true, " ~0Rewards World has had a make-over for the occasion", 40);
									Dialog.Text(dialog, breaker: true, "~1- ~0Space Items have been added to the shop", 40);
									Dialog.Text(dialog, breaker: true, "~1- ~0New currency, Moon rocks will be discoverable", 40);
									Dialog.Text(dialog, breaker: true, " ~0by breaking specific blocks in the game starting 11/16.", 40);
									Dialog.Text(dialog, breaker: true, "", 40);
									if (Rewards.Permission(Profile.Data.Filename, Permissions.Guard))
									{
										Dialog.Text(dialog, breaker: true, "~rStaff Updates", 55);
										Dialog.Text(dialog, breaker: true, "~1- ~0/silentban added", 40);
										Dialog.Text(dialog, breaker: true, "~1- ~0/warn moved to Guardians and above", 40);
									}
									Dialog.Text(dialog, breaker: true, "~1Last updated: 11/13/2020~0", 35);
									Dialog.Text(dialog, breaker: true, "", 40);
									Dialog.Button(dialog, breaker: true, "Thanks, continue to game", "Thanks, continue to game");
								});
								if (!Profile.Data.FriendOffline)
								{
									PlayerConsole.FriendNotification(this, "has logged in");
								}
								if (Event.Active && !string.IsNullOrEmpty(Event.Message))
								{
									if (Event.InactiveLater())
									{
										PlayerConsole.Message(this, "{0} ({1} left)", Event.Message, Text.Time(Event.InactiveAfter()));
									}
									else
									{
										PlayerConsole.Message(this, Event.Message);
									}
								}
								if (!string.IsNullOrEmpty(Server.Message))
								{
									PlayerConsole.Message(this, Server.Message);
								}
								if (Challenge.Active)
								{
									PlayerConsole.Message(this, Challenge.CurrentTitle());
								}
								Warp(Profile.Data.Session, unban: false);
								break;
							}
							case 1:
								PlayerAccount.ResponseLogin(this, false, null, null, "~3Login failed, the login token is incorrect.");
								Profile.Reset();
								break;
							case 2:
								PlayerAccount.ResponseLogin(this, false, null, null, "~3Sorry, this account is banned, you'll be able to play again after ~1{0}~3.", Text.Time(Profile.GetBan()));
								Profile.Reset();
								break;
							}
						}
						else
						{
							PlayerAccount.ResponseLogin(this, false, null, null, "~3The username that you provided isn't yet in use, go ahead and claim it!");
						}
						break;
					}
					case 2:
					{
						string text102 = binaryReader.ReadString();
						string mailname2 = binaryReader.ReadString();
						ushort num168 = binaryReader.ReadUInt16();
						binaryReader.Close();
						if (Profile.Active || !Active)
						{
							break;
						}
						Accounts = num168;
						if (num168 <= -1 && Profile.Address != "127.0.0.1")
						{
							PlayerAccount.ResponseRegister(this, false, null, null, "~3You have reached the limit of registrations, wait a little before registering again.");
							break;
						}
						if (Punishment.HostBanned(Profile.Address, out punishment2))
						{
							PlayerAccount.ResponseLogin(this, false, null, null, "~3Your location is banned, this ban will expire in ~1{0}~3, it's caused by ~1{1}~3.", Text.Time(Punishment.Left(punishment2.Time)), punishment2.Name);
							PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
							{
								Dialog.ItemText(dialog, breaker: true, $"~1You are banned for {Text.Time(Punishment.Left(punishment2.Time))}", 75, 3);
								Dialog.Text(dialog, breaker: true, "Your device and/or location has been banned because of", 50);
								Dialog.Text(dialog, breaker: true, $"what you did on account named ~1{punishment2.Name}~0.", 50);
								Dialog.Space(dialog);
								Dialog.Text(dialog, breaker: true, "If you didn't do anything wrong and don't own the account", 50);
								Dialog.Text(dialog, breaker: true, "that's mentioned above, it might be someone else who has", 50);
								Dialog.Text(dialog, breaker: true, "recently played using your device or network. If you are", 50);
								Dialog.Text(dialog, breaker: true, "using public network, connecting to another one may help", 50);
								Dialog.Text(dialog, breaker: true, "you solve this issue. If you still think there's a problem", 50);
								Dialog.Text(dialog, breaker: true, "contact us at ~1support@breaworldsgame.com~0.", 50);
								Dialog.Button(dialog, breaker: true, "Okay", "Okay");
							});
							break;
						}
						if (Database.ProfileExists(text102))
						{
							PlayerAccount.ResponseRegister(this, false, null, null, "~3The username you requested is already in use, think of another one.");
							break;
						}
						Database.ProfileLoad(ref Profile.Data, text102);
						switch (Profile.Register(text102, mailname2))
						{
						case 0:
							PlayerAccount.ResponseRegister(this, true, Profile.Data.Filename, Profile.Data.Password, "Registration complete, you can now login!");
							PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
							{
								Dialog.ItemText(dialog, breaker: true, "~1Registration complete", 75, 3);
								Dialog.Text(dialog, breaker: true, "Thank you for registering and welcome to Breaworlds,", 50);
								Dialog.Text(dialog, breaker: true, "your login token is ~1" + Profile.Data.Password + "~0, write it down in case you", 50);
								Dialog.Text(dialog, breaker: true, "forget it, you'll use it to login, do not share your login", 50);
								Dialog.Text(dialog, breaker: true, "token with anyone.", 50);
								Dialog.Button(dialog, breaker: false, "Okay", "Okay");
							});
							break;
						case 1:
							PlayerAccount.ResponseRegister(this, false, null, null, "~3Registration failed. Usernames can only contain symbols A-z and 0-9.");
							break;
						case 2:
							PlayerAccount.ResponseRegister(this, false, null, null, "~3Registration failed. Invalid username length, it should be between 3 and 12 characters.");
							break;
						case 3:
							PlayerAccount.ResponseRegister(this, false, null, null, "~3The email address that you provided doesn't seem valid. Please try again.");
							break;
						case 4:
							PlayerAccount.ResponseRegister(this, false, null, null, "~3The username that you requested is not nice, please think of another one.");
							break;
						}
						Database.ProfileClose(Profile.Data.Filename, binded: false);
						Profile.Reset();
						break;
					}
					case 3:
					{
						string name5 = binaryReader.ReadString();
						string mailname = binaryReader.ReadString();
						ushort accounts = binaryReader.ReadUInt16();
						binaryReader.Close();
						if (Profile.Active || !Active)
						{
							break;
						}
						Accounts = accounts;
						if (Punishment.HostBanned(Profile.Address, out punishment3))
						{
							PlayerAccount.ResponseLogin(this, false, null, null, "~3Your location is banned, this ban will expire in ~1{0}~3, it's caused by ~1{1}~3.", Text.Time(Punishment.Left(punishment3.Time)), punishment3.Name);
							PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
							{
								Dialog.ItemText(dialog, breaker: true, $"~1You are banned for {Text.Time(Punishment.Left(punishment3.Time))}", 75, 3);
								Dialog.Text(dialog, breaker: true, "Your device and/or location has been banned because of", 50);
								Dialog.Text(dialog, breaker: true, $"what you did on account named ~1{punishment3.Name}~0.", 50);
								Dialog.Space(dialog);
								Dialog.Text(dialog, breaker: true, "If you didn't do anything wrong and don't own the account", 50);
								Dialog.Text(dialog, breaker: true, "that's mentioned above, it might be someone else who has", 50);
								Dialog.Text(dialog, breaker: true, "recently played using your device or network. If you are", 50);
								Dialog.Text(dialog, breaker: true, "using public network, connecting to another one may help", 50);
								Dialog.Text(dialog, breaker: true, "you solve this issue. If you still think there's a problem", 50);
								Dialog.Text(dialog, breaker: true, "contact us at ~1support@breaworldsgame.com~0.", 50);
								Dialog.Button(dialog, breaker: true, "Okay", "Okay");
							});
						}
						else if (Database.ProfileExists(name5))
						{
							Database.ProfileLoad(ref Profile.Data, name5);
							switch (Profile.Recover(mailname))
							{
							case 0:
								PlayerAccount.ResponseRecover(this, "A recovery email has been sent to your email address.");
								break;
							case 1:
								PlayerAccount.ResponseRecover(this, "~3Email address doesn't match the one that you provided while registering.");
								break;
							case 2:
								PlayerAccount.ResponseRecover(this, "~3Please wait a little before requesting another recovery email.");
								break;
							}
							Database.ProfileClose(Profile.Data.Filename, binded: false);
							Profile.Reset();
						}
						else
						{
							PlayerAccount.ResponseRecover(this, "~3The account that you're trying to recover doesn't exist.");
						}
						break;
					}
					case 4:
					{
						string text87 = binaryReader.ReadString();
						binaryReader.Close();
						if (!Profile.Active || !Session.Active)
						{
							break;
						}
						text87 = text87.Replace("\r\n", string.Empty);
						text87 = text87.Replace("\n", string.Empty);
						text87 = text87.Replace("[", string.Empty);
						text87 = text87.Replace("]", string.Empty);
						string text88 = Text.FilterSwear(text87);
						arguments = text88.Split(' ');
						if (LastMessage + 1000 > DateTime.UtcNow.Ticks / 10000 && Profile.Data.Filename != "Zyliss")
						{
							PlayerConsole.Message(this, "~3Spam detected. ~0Please wait a few seconds before sending another message.");
						}
						else if (arguments[0].StartsWith("/"))
						{
							PlayerConsole.Message(this, "~5{0}", Text.FilterColor(text88));
							if (arguments[0].ToLower() == "/help")
							{
								PlayerConsole.Message(this, "Valid commands are help, rules, warp, owner, g, p, f, r, pull, kill, fps, time, staff, trade, hello, dance, ban, unban, bans, list, quest.");
							}
							else if (arguments[0].ToLower() == "/rules")
							{
								PlayerCore.UpdateDialog(this, 0, "Rules", delegate(BinaryWriter dialog)
								{
									Dialog.ItemText(dialog, breaker: true, "~1Terms and conditions agreement", 75, 3);
									Dialog.ItemText(dialog, breaker: true, "1. Do not share your login token with anyone, you will get scammed.", 50, 1);
									Dialog.ItemText(dialog, breaker: true, "2. Pay to win games are not allowed and will result in a punishment.", 50, 1);
									Dialog.ItemText(dialog, breaker: true, "3. Using auto clickers, macros, bots, speed hacks can get you banned.", 50, 1);
									Dialog.ItemText(dialog, breaker: true, "4. Trading non game items is illegal and you'll be punished for that.", 50, 1);
									Dialog.ItemText(dialog, breaker: true, "5. Inappropriate behavior is not allowed and will result in a ban.", 50, 1);
									Dialog.ItemText(dialog, breaker: true, "6. If you see someone else breaking the rules, please report them.", 50, 1);
									Dialog.ItemText(dialog, breaker: true, "7. Protect your items properly, scammed items can't be recovered.", 50, 1);
									Dialog.ItemText(dialog, breaker: true, "8. Even if you break rules by accident, you'll still be punished.", 50, 1);
									Dialog.ItemText(dialog, breaker: true, "9. Please contact support@breaworldsgame.com if you need help.", 50, 1);
									Dialog.Button(dialog, breaker: true, "Accept", "I have read and agreed");
								});
							}
							else if (arguments[0].ToLower() == "/owner")
							{
								if (Session.Data.Owner != "")
								{
									if (Session.Data.Admin.Count > 0)
									{
										PlayerConsole.Message(this, "World is owned by ~1{0}~0, admins of this world are ~1{1}~0.", Session.Data.Owner, string.Join("~0, ~1", Session.Data.Admin.ToArray()));
									}
									else
									{
										PlayerConsole.Message(this, "World is owned by ~1{0}~0, there are no admins.", Session.Data.Owner);
									}
								}
								else
								{
									PlayerConsole.Message(this, "This world is not owned by anyone.");
								}
							}
							else if (arguments[0].ToLower() == "/warp")
							{
								if (arguments.Length > 1)
								{
									Warp(arguments[1], unban: false);
								}
								else
								{
									PlayerConsole.Message(this, "Command usage is ~1/warp <World>~0, takes you to another world.");
								}
							}
							else if (arguments[0].ToLower() == "/fps")
							{
								PlayerConsole.Message(this, "You currently have ~1{0} ~0frames per second.", "{fps}");
							}
							else if (arguments[0].ToLower() == "/time")
							{
								PlayerConsole.Message(this, "Breaworlds time is ~1{0}~0.", DateTime.UtcNow.ToString("f"));
							}
							else if (arguments[0].ToLower() == "/staff")
							{
								List<string> list3 = new List<string>();
								Player[] array41 = Server.Online.ToArray();
								foreach (Player player36 in array41)
								{
									if (player36.Active && player36.Profile.Active && player36.Session.Active && Rewards.Permission(player36.Profile.Data.Filename, Permissions.Mod) && player36.Profile.Data.Username == "~r" + player36.Profile.Data.Filename && !player36.Profile.HiddenStaff)
									{
										list3.Add(player36.Profile.Data.Username);
									}
								}
								if (list3.Count == 0)
								{
									PlayerConsole.Message(this, "No staff members are currently online.");
								}
								else
								{
									PlayerConsole.Message(this, "Staff online: ~1{0}~0.", string.Join("~0, ", list3));
								}
							}
							else if (arguments[0].ToLower() == "/news" || arguments[0].ToLower() == "/updates")
							{
								PlayerCore.UpdateDialog(this, 0, "WelcomeMessage", delegate(BinaryWriter dialog)
								{
									Dialog.ItemText(dialog, breaker: true, "~1Breaworlds Gazette", 75, 3);
									Dialog.ItemImage(dialog, breaker: true, 200, 5);
									Dialog.Text(dialog, breaker: true, "~1- ~0Female Gender has been added", 40);
									Dialog.Text(dialog, breaker: true, "~0(can be changed via account options)", 40);
									Dialog.Text(dialog, breaker: true, "~1- ~0Character has been re-worked with shading,", 40);
									Dialog.Text(dialog, breaker: true, " ~0and some items have been re-designed", 40);
									Dialog.Text(dialog, breaker: true, "~1- ~0Breaworlds has been sent into outerspace,", 40);
									Dialog.Text(dialog, breaker: true, " ~0Rewards World has had a make-over for the occasion", 40);
									Dialog.Text(dialog, breaker: true, "~1- ~0Space Items have been added to the shop", 40);
									Dialog.Text(dialog, breaker: true, "~1- ~0New currency, Moon rocks will be discoverable", 40);
									Dialog.Text(dialog, breaker: true, " ~0by breaking specific blocks in the game starting 11/16.", 40);
									Dialog.Text(dialog, breaker: true, "", 40);
									if (Rewards.Permission(Profile.Data.Filename, Permissions.Guard))
									{
										Dialog.Text(dialog, breaker: true, "~rStaff Updates", 55);
										Dialog.Text(dialog, breaker: true, "~1- ~0/silentban added", 40);
										Dialog.Text(dialog, breaker: true, "~1- ~0/warn moved to Guardians and above", 40);
									}
									Dialog.Text(dialog, breaker: true, "~1Last updated: 11/13/2020~0", 35);
									Dialog.Text(dialog, breaker: true, "", 40);
									Dialog.Button(dialog, breaker: true, "Thanks, continue to game", "Thanks, continue to game");
								});
							}
							else if (arguments[0].ToLower() == "/trade")
							{
								if (arguments.Length > 1)
								{
									bool flag12 = false;
									string text89 = arguments[1];
									Player[] array42 = Server.Online.ToArray();
									foreach (Player player37 in array42)
									{
										if (player37 != this && player37.Active && player37.Profile.Active && player37.Session.Active && !(player37.Session.Data.Name != Session.Data.Name) && player37.Profile.VisibleTo(this) && Text.FilterColor(player37.Profile.Data.Username).ToLower().StartsWith(text89.ToLower()))
										{
											TradeStart(player37.Identifier);
											flag12 = true;
											break;
										}
									}
									if (!flag12)
									{
										PlayerConsole.Message(this, "No players found with usernames starting with ~1{0}~0.", text89);
									}
								}
								else
								{
									PlayerConsole.Message(this, "Command usage is ~1/trade <Player>~0, begins trading with specified player.");
								}
							}
							else if (arguments[0].ToLower() == "/list")
							{
								List<string> list4 = new List<string>();
								Player[] array43 = Server.Online.ToArray();
								foreach (Player player38 in array43)
								{
									if (player38.Active && player38.Profile.Active && player38.Session.Active && !(player38.Session.Data.Name != Session.Data.Name) && player38.Profile.VisibleTo(this))
									{
										list4.Add(player38.Profile.Data.Username);
									}
								}
								PlayerConsole.Message(this, "Players in this world: ~1{0}~0.", string.Join("~0, ~1", list4));
							}
							else if (arguments[0].ToLower() == "/quest")
							{
								PlayerQuests.ShowItemQuest(this);
							}
							else if (arguments[0].ToLower() == "/hello")
							{
								input = new MemoryStream();
								BinaryWriter binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(32));
								binaryWriter.Write(Convert.ToInt32(0));
								binaryWriter.Write(Convert.ToByte(1));
								binaryWriter.Write(Convert.ToByte(0));
								binaryWriter.Write(Convert.ToByte(5));
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								Send(input.ToArray());
								binaryWriter.Close();
								input = new MemoryStream();
								binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(32));
								binaryWriter.Write(Convert.ToInt32(Identifier));
								binaryWriter.Write(Convert.ToByte(1));
								binaryWriter.Write(Convert.ToByte(0));
								binaryWriter.Write(Convert.ToByte(5));
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								Server.Broadcast(input.ToArray(), delegate(Player player)
								{
									if (!player.Active)
									{
										return false;
									}
									if (!player.Profile.Active)
									{
										return false;
									}
									if (!player.Session.Active)
									{
										return false;
									}
									return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
								});
								binaryWriter.Close();
							}
							else if (arguments[0].ToLower() == "/dance")
							{
								input = new MemoryStream();
								BinaryWriter binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(32));
								binaryWriter.Write(Convert.ToInt32(0));
								binaryWriter.Write(Convert.ToByte(3));
								binaryWriter.Write(Convert.ToByte(0));
								binaryWriter.Write(Convert.ToByte(100));
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								Send(input.ToArray());
								binaryWriter.Close();
								input = new MemoryStream();
								binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(32));
								binaryWriter.Write(Convert.ToInt32(Identifier));
								binaryWriter.Write(Convert.ToByte(3));
								binaryWriter.Write(Convert.ToByte(0));
								binaryWriter.Write(Convert.ToByte(100));
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								Server.Broadcast(input.ToArray(), delegate(Player player)
								{
									if (!player.Active)
									{
										return false;
									}
									if (!player.Profile.Active)
									{
										return false;
									}
									if (!player.Session.Active)
									{
										return false;
									}
									return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
								});
								binaryWriter.Close();
							}
							else if (arguments[0].ToLower() == "/tmp" && Profile.Data.Filename == "quu98" && Profile.Data.Filename == "Zyliss")
							{
								if (arguments.Length > 2)
								{
									int.TryParse(arguments[2], out var result20);
									if (Session.Template(arguments[1], result20))
									{
										Warp(arguments[1], unban: false);
									}
								}
							}
							else if (arguments[0].ToLower() == "/per" && Rewards.Permission(Profile.Data.Filename, Permissions.Dev))
							{
								if (arguments.Length > 1)
								{
									if (Rewards.Permissions.ContainsKey(arguments[1]))
									{
										Rewards.Permissions.Remove(arguments[1]);
									}
									if (arguments.Length > 2)
									{
										int.TryParse(arguments[2], out var result21);
										if (result21 == 1)
										{
											Rewards.Permissions.Add(arguments[1], Permissions.Guard);
										}
										if (result21 == 2)
										{
											Rewards.Permissions.Add(arguments[1], Permissions.Mod);
										}
										if (result21 == 3)
										{
											Rewards.Permissions.Add(arguments[1], Permissions.Admin);
										}
										if (result21 == 4)
										{
											Rewards.Permissions.Add(arguments[1], Permissions.Dev);
										}
									}
								}
							}
							else if (arguments[0].ToLower() == "/res" && Rewards.Permission(Profile.Data.Filename, Permissions.Dev))
							{
								if (arguments.Length > 1)
								{
									int.TryParse(arguments[1], out var result22);
									if (result22 < 1)
									{
										result22 = 1;
									}
									if (result22 > 60)
									{
										result22 = 60;
									}
									Server.Restart(result22 * 60);
								}
								else
								{
									Server.Restart(300);
								}
								Server.SendLog(Profile.Data.Filename, "Has requested server restart");
							}
							else if (arguments[0].ToLower() == "/gsm" && Rewards.Permission(Profile.Data.Filename, Permissions.Dev))
							{
								input = new MemoryStream();
								BinaryWriter binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(4));
								string text90 = string.Format("~0[~3Announcement~0] {0}", string.Join(" ", arguments, 1, arguments.Length - 1));
								binaryWriter.Write(Encoding.UTF8.GetBytes(text90 + "\0"));
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								int num134 = Server.Broadcast(input.ToArray(), delegate(Player player)
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
								binaryWriter.Close();
								PlayerConsole.Message(this, "Staff message sent to ~1{0} ~0players.", num134);
							}
							else if (arguments[0].ToLower() == "/con" && Rewards.Permission(Profile.Data.Filename, Permissions.Dev))
							{
								PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
								{
									Dialog.ItemText(dialog, breaker: true, "~1Connectivity information", 75, 3);
									Dialog.Text(dialog, breaker: true, "Raw player count: ~1" + Server.Online.Count, 50);
									Dialog.Text(dialog, breaker: true, "Cached profiles count: ~1" + Database.ProfileCache.Count, 50);
									Dialog.Text(dialog, breaker: true, "Cached sessions count: ~1" + Database.SessionCache.Count, 50);
									Dialog.Button(dialog, breaker: true, "Okay", "Okay");
								});
							}
							else if (arguments[0].ToLower() == "/ser" && Rewards.Permission(Profile.Data.Filename, Permissions.Dev))
							{
								PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
								{
									Dialog.ItemText(dialog, breaker: true, "~1Serializer usage", 75, 3);
									Dialog.Text(dialog, breaker: true, "Profile serializations: ~1" + Serializer.ProfileSerializations, 50);
									Dialog.Text(dialog, breaker: true, "Profile deserializations: ~1" + Serializer.ProfileDeserializations, 50);
									Dialog.Space(dialog);
									Dialog.Text(dialog, breaker: true, "Session serializations: ~1" + Serializer.SessionSerializations, 50);
									Dialog.Text(dialog, breaker: true, "Session deserializations: ~1" + Serializer.SessionDeserializations, 50);
									Dialog.Space(dialog);
									Dialog.Text(dialog, breaker: true, "Challenge serializations: ~1" + Serializer.ChallengeSerializations, 50);
									Dialog.Text(dialog, breaker: true, "Challenge deserializations: ~1" + Serializer.ChallengeDeserializations, 50);
									Dialog.Button(dialog, breaker: true, "Okay", "Okay");
								});
							}
							else if (arguments[0].ToLower() == "/link" && Rewards.Permission(Profile.Data.Filename, Permissions.Dev))
							{
								if (arguments.Length > 2)
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(27));
									binaryWriter.Write(Encoding.UTF8.GetBytes(arguments[2] + "\0"));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Server.Broadcast(input.ToArray(), delegate(Player player)
									{
										if (!player.Active)
										{
											return false;
										}
										if (!player.Profile.Active)
										{
											return false;
										}
										return (!(Text.FilterColor(player.Profile.Data.Username).ToLower() != arguments[1].ToLower())) ? true : false;
									});
									binaryWriter.Close();
								}
							}
							else if (arguments[0].ToLower() == "/slots" && Rewards.Permission(Profile.Data.Filename, Permissions.Dev))
							{
								if (arguments.Length > 1)
								{
									int.TryParse(arguments[1], out var result23);
									Profile.Data.ItemSlots = result23;
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(6));
									Profile.WriteItems(binaryWriter);
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
							}
							else if (arguments[0].ToLower() == "/purge1" && Rewards.Permission(Profile.Data.Filename, Permissions.Dev))
							{
								int num135 = 0;
								Player[] array44 = Server.Online.ToArray();
								foreach (Player player39 in array44)
								{
									if (!player39.Active)
									{
										player39.Closed = false;
										player39.Close();
										num135++;
									}
								}
								PlayerConsole.Message(this, "Purge1 done, {0} applied", num135);
							}
							else if (arguments[0].ToLower() == "/purge2" && Rewards.Permission(Profile.Data.Filename, Permissions.Dev))
							{
								int num137 = 0;
								Player[] array45 = Server.Online.ToArray();
								foreach (Player player40 in array45)
								{
									if (!player40.Session.Active)
									{
										player40.Closed = false;
										player40.Close();
										num137++;
									}
								}
								PlayerConsole.Message(this, "Purge2 done, {0} applied", num137);
							}
							else if (arguments[0].ToLower() == "/purge3" && Rewards.Permission(Profile.Data.Filename, Permissions.Dev))
							{
								int num139 = 0;
								Player[] array46 = Server.Online.ToArray();
								foreach (Player player41 in array46)
								{
									if (player41.Closed)
									{
										Server.Disconnect(player41);
										num139++;
									}
								}
								PlayerConsole.Message(this, "Purge3 done, {0} applied", num139);
							}
							else if (arguments[0].ToLower() == "/purge4" && Rewards.Permission(Profile.Data.Filename, Permissions.Dev))
							{
								int num141 = 0;
								Player[] array47 = Server.Online.ToArray();
								foreach (Player player42 in array47)
								{
									if (!player42.Valid)
									{
										player42.Closed = false;
										player42.Close();
										num141++;
									}
								}
								PlayerConsole.Message(this, "Purge4 done, {0} applied", num141);
							}
							else if (arguments[0].ToLower() == "/change" && Rewards.Permission(Profile.Data.Filename, Permissions.Dev))
							{
								if (arguments.Length > 3)
								{
									ushort.TryParse(arguments[1], out var result24);
									ushort.TryParse(arguments[2], out var result25);
									ushort.TryParse(arguments[3], out var result26);
									if (result26 == 1)
									{
										for (int num143 = 0; num143 < Session.Data.Background.Length; num143++)
										{
											if (Session.Data.Background[num143] == result24)
											{
												Session.Data.Background[num143] = result25;
											}
										}
									}
									if (result26 == 2)
									{
										for (int num144 = 0; num144 < Session.Data.Foreground.Length; num144++)
										{
											if (Session.Data.Foreground[num144] == result24)
											{
												Session.Data.Foreground[num144] = result25;
											}
										}
									}
									Player[] array48 = Server.Online.ToArray();
									foreach (Player player43 in array48)
									{
										if (player43.Active && player43.Profile.Active && player43.Session.Active && !(player43.Session.Data.Name != Session.Data.Name))
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(10));
											player43.Session.WriteTiles(binaryWriter, player43.Profile.Data.Filename);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											player43.Send(input.ToArray());
											binaryWriter.Close();
										}
									}
								}
							}
							else if (arguments[0].ToLower() == "/dialog" && Rewards.Permission(Profile.Data.Filename, Permissions.Mod))
							{
								PlayerCore.UpdateDialog(this, 0, "Dialog.Creator", delegate(BinaryWriter dialog)
								{
									Dialog.ItemText(dialog, breaker: true, "~1Dialog creator", 75, 3);
									Dialog.Text(dialog, breaker: true, "Text|<bool:line_break>|<string:text>|<int:size>", 25);
									Dialog.Text(dialog, breaker: true, "ItemSlot|<bool:line_break>|<int:item_index>|<int:item_count>|<int:width>|<int:height>", 25);
									Dialog.Text(dialog, breaker: true, "ItemText|<bool:line_break>|<string:text>|<int:size>|<int:item_index>", 25);
									Dialog.Text(dialog, breaker: true, "ItemPicker|<bool:line_break>|<string:id>|<int:item_index>|<int:width>|<int:height>", 25);
									Dialog.Text(dialog, breaker: true, "Button|<bool:line_break>|<string:id>|<string:text>", 25);
									Dialog.Text(dialog, breaker: true, "Textbox|<bool:line_break>|<string:id>|<string:text>|<int:length>", 25);
									Dialog.Text(dialog, breaker: true, "Checkbox|<bool:line_break>|<bool:value>|<string:id>|<string:text>|<int:size>", 25);
									Dialog.Text(dialog, breaker: true, "RGB|<bool:line_break>|<string:id>|<int:width>|<int:height>|<int:r>|<int:g>|<int:b>", 25);
									Dialog.Text(dialog, breaker: true, "Space", 25);
									Dialog.Text(dialog, breaker: true, "Achievement|<bool:line_break>|<int:icon>|<string:name>|<string:text>|<string:info>", 25);
									Dialog.Text(dialog, breaker: true, "ItemButton|<bool:line_break>|<string:id>|<int:item>|<int:size>", 25);
									Dialog.Text(dialog, breaker: true, "IconButton|<bool:line_break>|<string:id>|<int:icon>|<int:size>", 25);
									for (int num184 = 0; num184 < 20; num184++)
									{
										Dialog.Textbox(dialog, breaker: true, num184.ToString(), "", 128);
									}
									Dialog.Button(dialog, breaker: false, "Run", "Run");
									Dialog.Button(dialog, breaker: false, "Close", "Close");
								});
							}
							else if (arguments[0].ToLower() == "/timer" && Rewards.Permission(Profile.Data.Filename, Permissions.Dev))
							{
								if (arguments.Length > 1)
								{
									int.TryParse(arguments[1], out var result27);
									PlayerCore.UpdateTimer(this, result27, delegate
									{
										PlayerConsole.Message(this, "Time up!");
									});
								}
							}
							else if (arguments[0].ToLower() == "/canceltimer" && Rewards.Permission(Profile.Data.Filename, Permissions.Dev))
							{
								PlayerCore.CancelTimer(this);
							}
							else if (arguments[0].ToLower() == "/strength" && Rewards.Permission(Profile.Data.Filename, Permissions.Admin))
							{
								if (arguments.Length > 1)
								{
									int.TryParse(arguments[1], out Profile.Strength);
								}
							}
							else if (arguments[0].ToLower() == "/level" && Rewards.Permission(Profile.Data.Filename, Permissions.Admin))
							{
								if (arguments.Length <= 1)
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(4));
									text88 = $"Command usage is ~1/level <Level>~0, changes your experience level.";
									binaryWriter.Write(Encoding.UTF8.GetBytes(text88 + "\0"));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								else
								{
									int.TryParse(arguments[1], out var result28);
									if (result28 < 1 || result28 > 100)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										text88 = "You can only set your level in range from 1 to 100.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text88 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else
									{
										Profile.Data.Level = result28;
										Server.SendLog(Profile.Data.Filename, $"Has changed their level to {result28}");
									}
								}
							}
							else if (arguments[0].ToLower() == "/gems" && Rewards.Permission(Profile.Data.Filename, Permissions.Admin))
							{
								if (arguments.Length <= 1)
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(4));
									text88 = $"Command usage is ~1/gems <Count>~0, gives you gems.";
									binaryWriter.Write(Encoding.UTF8.GetBytes(text88 + "\0"));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								else
								{
									int.TryParse(arguments[1], out var result29);
									if (result29 > 100000)
									{
										PlayerConsole.Message(this, "You can only spawn 100,000 gems at a time.");
									}
									else
									{
										PlayerGems.Set(this, result29);
										Server.SendLog(Profile.Data.Filename, $"Has set their gems to {result29}.");
									}
								}
							}
							else if (arguments[0].ToLower() == "/ggems" && Profile.Data.Filename == "Zyliss")
							{
								if (arguments.Length <= 1)
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(4));
									text88 = $"Command usage is ~1/ggems <Count>~0, gives the entire game gems.";
									binaryWriter.Write(Encoding.UTF8.GetBytes(text88 + "\0"));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								else
								{
									int.TryParse(arguments[1], out var result30);
									if (result30 > 100000)
									{
										PlayerConsole.Message(this, "You can only give 100,000 gems at a time.");
									}
									else
									{
										Player[] array49 = Server.Online.ToArray();
										foreach (Player player44 in array49)
										{
											if (player44.Active && player44.Profile.Active && player44.Session.Active)
											{
												PlayerGems.Set(player44, result30);
												PlayerConsole.Message(player44, "~1You've been gifted ~m" + $"{result30:#,###,###.##}" + " gems~1 by the server.");
											}
										}
										Server.SendLog(Profile.Data.Filename, $"Has given everyone in the game {$"{result30:#,###,###.##}"} gems.");
									}
								}
							}
							else if (arguments[0].ToLower() == "/sc" && Rewards.Permission(Profile.Data.Filename, Permissions.Dev))
							{
								if (arguments.Length <= 1)
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(4));
									text88 = $"Command usage is ~1/sc <Count>~0, gives you special currency.";
									binaryWriter.Write(Encoding.UTF8.GetBytes(text88 + "\0"));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								else
								{
									int.TryParse(arguments[1], out var result31);
									PlayerSpecialCurrency.Set(this, result31);
									Server.SendLog(Profile.Data.Filename, $"Has set their special currency to {result31}.");
								}
							}
							else if (arguments[0].ToLower() == "/item")
							{
								if (arguments.Length <= 2)
								{
									PlayerConsole.Message(this, "Command usage is ~1/item <Index> <Count>~0, gives the specified item.");
								}
								else
								{
									ushort.TryParse(arguments[1], out var result32);
									ushort.TryParse(arguments[2], out var result33);
									if (result33 > 100)
									{
										PlayerConsole.Message(this, "You can only spawn max 100 items at a time.");
									}
									else if (Profile.CanGetItem(result32, result33))
									{
										Profile.SetItem(result32, Profile.GetItem(result32) + result33);
										PlayerCore.UpdateInventory(this);
										PlayerConsole.Message(this, "You got x{0} {1}.", result33, Item.Name(result32));
										Server.SendLog(Profile.Data.Filename, $"Added {result33} {Item.Name(result32)} to their account");
									}
									else
									{
										PlayerConsole.Message(this, "Couldn't get the item, not enough inventory space.");
									}
								}
							}
							else if (arguments[0].ToLower() == "/remove" && Rewards.Permission(Profile.Data.Filename, Permissions.Admin))
							{
								if (arguments.Length <= 3)
								{
									PlayerConsole.Message(this, "Command usage is ~1/remove <Username> <ItemId> <Count>~0, removes items from a player.");
								}
								else
								{
									string text91 = arguments[1];
									ushort.TryParse(arguments[2], out var result34);
									ushort.TryParse(arguments[3], out var result35);
									Player[] array50 = Server.Online.ToArray();
									foreach (Player player45 in array50)
									{
										if (player45.Active && player45.Profile.Active && player45.Session.Active && player45.Profile.HasItem(result34, result35) && Text.FilterColor(player45.Profile.Data.Username).ToLower() == text91.ToLower())
										{
											player45.Profile.SetItem(result34, player45.Profile.GetItem(result34) - result35);
											PlayerCore.UpdateInventory(player45);
											PlayerConsole.Message(this, "You removed x{0} {1} from {2}.", result35, Item.Name(result34), player45.Profile.Data.Username);
											Server.SendLog(Profile.Data.Filename, $"Removed {result35} {Item.Name(result34)} from {player45.Profile.Data.Filename}");
										}
									}
								}
							}
							else if (arguments[0].ToLower() == "/skin" && Rewards.Permission(Profile.Data.Filename, Permissions.Admin))
							{
								Profile.Data.SkinR = int.Parse(arguments[1]);
								Profile.Data.SkinG = int.Parse(arguments[2]);
								Profile.Data.SkinB = int.Parse(arguments[3]);
								Profile.Data.SkinA = int.Parse(arguments[4]);
								input = new MemoryStream();
								BinaryWriter binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(14));
								binaryWriter.Write(Convert.ToInt32(0));
								Profile.WriteData(binaryWriter, this);
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								Send(input.ToArray());
								binaryWriter.Close();
								Player[] array51 = Server.Online.ToArray();
								foreach (Player player46 in array51)
								{
									if (player46 != this && player46.Active && player46.Profile.Active && player46.Session.Active && !(player46.Session.Data.Name != Session.Data.Name))
									{
										input = new MemoryStream();
										binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(14));
										binaryWriter.Write(Convert.ToInt32(Identifier));
										Profile.WriteData(binaryWriter, player46);
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										player46.Send(input.ToArray());
										binaryWriter.Close();
									}
								}
							}
							else if (arguments[0].ToLower() == "/flag" && Rewards.Permission(Profile.Data.Filename, Permissions.Admin))
							{
								if (arguments.Length > 1)
								{
									if (Rewards.Flag.ContainsKey(arguments[1]))
									{
										Rewards.Flag.Remove(arguments[1]);
									}
									if (arguments.Length > 2)
									{
										Rewards.Flag.Add(arguments[1], arguments[2]);
										Server.SendLog(Profile.Data.Filename, $"Has set a custom flag for {arguments[1]}");
									}
									else
									{
										Server.SendLog(Profile.Data.Filename, $"Has removed a custom flag for {arguments[1]}");
									}
								}
							}
							else if (arguments[0].ToLower() == "/cap" && Rewards.Permission(Profile.Data.Filename, Permissions.Admin))
							{
								if (arguments.Length > 1)
								{
									if (Rewards.Capitalization.Contains(arguments[1]))
									{
										Rewards.Capitalization.Remove(arguments[1]);
									}
									else
									{
										Rewards.Capitalization.Add(arguments[1]);
									}
								}
							}
							else if (arguments[0].ToLower() == "/find")
							{
								if (arguments.Length > 1)
								{
									string value5 = string.Join(" ", arguments, 1, arguments.Length - 1).ToLower();
									foreach (ItemData item3 in Item.List)
									{
										if (item3.Name.ToLower().Contains(value5))
										{
											PlayerConsole.Message(this, "Found ~1{0}~0 at ~1{1}~0.", item3.Name, item3.ID);
										}
									}
								}
							}
							else if (arguments[0].ToLower() == "/cin")
							{
								Profile.Data.ItemIndex = new List<ushort>();
								Profile.Data.ItemCount = new List<ushort>();
								Profile.Data.ItemEquip = new List<ushort>();
								PlayerConsole.Message(this, "Your inventory has been cleared.");
								Profile.SetItem(1, 1);
								Profile.SetItem(3, 1);
								input = new MemoryStream();
								BinaryWriter binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(6));
								Profile.WriteItems(binaryWriter);
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								Send(input.ToArray());
								binaryWriter.Close();
							}
							else if (arguments[0].ToLower() == "/addtest" && Rewards.Permission(Profile.Data.Filename, Permissions.Admin))
							{
								if (arguments.Length > 1)
								{
									if (Rewards.Testing.Contains(arguments[1]))
									{
										Rewards.Testing.Remove(arguments[1]);
										PlayerConsole.Message(this, "User removed.");
									}
									else
									{
										Rewards.Testing.Add(arguments[1]);
										PlayerConsole.Message(this, "User added.");
									}
								}
							}
							else if (arguments[0].ToLower() == "/rate" && Rewards.Permission(Profile.Data.Filename, Permissions.Admin))
							{
								if (arguments.Length > 1)
								{
									int.TryParse(arguments[1], out var result36);
									Ratings.Data.Rating.TryGetValue(Session.Data.Filename, out var value6);
									Ratings.Data.Rating[Session.Data.Filename] = value6 + result36;
								}
							}
							else if (arguments[0].ToLower() == "/rates" && Rewards.Permission(Profile.Data.Filename, Permissions.Admin))
							{
								input = new MemoryStream();
								BinaryWriter binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(5));
								Window = Dialog.Create(binaryWriter, 0, "test rating");
								Dialog.ItemText(binaryWriter, breaker: true, "~1Top worlds today", 75, 3);
								for (int num149 = 0; num149 < Math.Min(Ratings.Leaderboard.Length, 10); num149++)
								{
									Dialog.Button(binaryWriter, breaker: true, Ratings.Leaderboard[num149], $"#{num149 + 1}. {Ratings.Leaderboard[num149]}, {Ratings.Data.Rating[Ratings.Leaderboard[num149]]}");
								}
								Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								Send(input.ToArray());
								binaryWriter.Close();
							}
							else if (arguments[0].ToLower() == "/findu" && Rewards.Permission(Profile.Data.Filename, Permissions.Guard))
							{
								if (arguments.Length > 1)
								{
									if (!Text.IsAllowed(arguments[1]))
									{
										PlayerConsole.Message(this, "Usernames can only contain symbols A-z and 0-9");
									}
									else if (!Text.Length(arguments[1], 3, 12))
									{
										PlayerConsole.Message(this, "Username is too long or too short, it must be from 3 to 12 characters long!");
									}
									else
									{
										string value7 = arguments[1].ToLower();
										int num150 = 0;
										Player[] array52 = Server.Online.ToArray();
										foreach (Player player47 in array52)
										{
											if (player47.Active && player47.Profile.Active && player47.Session.Active && player47.Profile.Data.Filename.ToLower().StartsWith(value7))
											{
												PlayerConsole.Message(this, "Player ~1{0} ~0(~1{1}~0) has been found in ~1{2}~0.", player47.Profile.Data.Username, player47.Profile.Data.Filename, player47.Session.Data.Name);
												num150++;
											}
										}
										if (num150 == 0)
										{
											PlayerConsole.Message(this, "No players online with usernames starting with ~1{0}~0.", arguments[1]);
										}
									}
								}
								else
								{
									PlayerConsole.Message(this, "Command usage is ~1/findu <Username>~0, looks for online players.");
								}
							}
							else if (arguments[0].ToLower() == "/nick" && Rewards.Permission(Profile.Data.Filename, Permissions.Mod))
							{
								if (arguments.Length > 1)
								{
									if (!Text.IsAllowed(arguments[1]) && Profile.Data.Filename != "Zyliss")
									{
										PlayerConsole.Message(this, "Usernames can only contain symbols A-z and 0-9!");
									}
									else if (!Text.Length(arguments[1], 3, 12) && Profile.Data.Filename != "Zyliss")
									{
										PlayerConsole.Message(this, "Username is too long or too short, it must be from 3 to 12 characters long!");
									}
									else
									{
										string text92 = arguments[1];
										if (text92 == Profile.Data.Filename)
										{
											text92 = $"{Profile.Data.Filename}";
										}
										Profile.Data.Username = text92;
										Database.ProfileSave(Profile.Data);
										PlayerConsole.Message(this, "Your username has been changed to ~1{0}~0.", Profile.Data.Username);
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(14));
										binaryWriter.Write(Convert.ToInt32(0));
										Profile.WriteData(binaryWriter, this);
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
										Player[] array53 = Server.Online.ToArray();
										foreach (Player player48 in array53)
										{
											if (player48 != this && player48.Active && player48.Profile.Active && player48.Session.Active && !(player48.Session.Data.Name != Session.Data.Name))
											{
												input = new MemoryStream();
												binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(14));
												binaryWriter.Write(Convert.ToInt32(Identifier));
												Profile.WriteData(binaryWriter, player48);
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												player48.Send(input.ToArray());
												binaryWriter.Close();
											}
										}
										if (!Rewards.Permission(Profile.Data.Filename, Permissions.Dev))
										{
											Server.SendLog(Profile.Data.Filename, $"Changed their username to {Profile.Data.Username}");
										}
									}
								}
								else if (arguments.Length <= 1)
								{
									string text93 = Profile.Data.Filename;
									if (text93 == Profile.Data.Filename)
									{
										text93 = $"~r{Profile.Data.Filename}";
									}
									Profile.Data.Username = text93;
									Database.ProfileSave(Profile.Data);
									PlayerConsole.Message(this, "Your username has been changed to ~1{0}~0.", Profile.Data.Username);
									Profile.Data.FakeLevel = Server.Random.Next(20);
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(14));
									binaryWriter.Write(Convert.ToInt32(0));
									Profile.WriteData(binaryWriter, this);
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
									Player[] array54 = Server.Online.ToArray();
									foreach (Player player49 in array54)
									{
										if (player49 != this && player49.Active && player49.Profile.Active && player49.Session.Active && !(player49.Session.Data.Name != Session.Data.Name))
										{
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(14));
											binaryWriter.Write(Convert.ToInt32(Identifier));
											Profile.WriteData(binaryWriter, player49);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											player49.Send(input.ToArray());
											binaryWriter.Close();
										}
									}
									if (!Rewards.Permission(Profile.Data.Filename, Permissions.Dev))
									{
										Server.SendLog(Profile.Data.Filename, $"Changed their username to {Profile.Data.Username}");
									}
								}
							}
								else if ((arguments[0].ToLower() == "/noc" && (Session.WorldOwner(Profile.Data.Username) || Session.WorldAdmin(Profile.Data.Username))) || (arguments[0].ToLower() == "/noc" && Rewards.Permission(Profile.Data.Filename, Permissions.Mod)))
								{
								Profile.Noclip = !Profile.Noclip;
								input = new MemoryStream();
								BinaryWriter binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(14));
								binaryWriter.Write(Convert.ToInt32(0));
								Profile.WriteData(binaryWriter, this);
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								Send(input.ToArray());
								binaryWriter.Close();
								Player[] array55 = Server.Online.ToArray();
								foreach (Player player50 in array55)
								{
									if (player50 != this && player50.Active && player50.Profile.Active && player50.Session.Active && !(player50.Session.Data.Name != Session.Data.Name))
									{
										input = new MemoryStream();
										binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(14));
										binaryWriter.Write(Convert.ToInt32(Identifier));
										Profile.WriteData(binaryWriter, player50);
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										player50.Send(input.ToArray());
										binaryWriter.Close();
									}
								}
								if (Profile.Noclip)
								{
									PlayerConsole.Message(this, "~4Noclip enabled. ~0You can now walk trough blocks, but please don't stand in them.");
								}
								else
								{
									PlayerConsole.Message(this, "~3Noclip disabled. ~0You can't walk trough blocks anymore, just like others.");
								}
							}
							else if (arguments[0].ToLower() == "/summon" && Rewards.Permission(Profile.Data.Filename, Permissions.Mod))
							{
								if (arguments.Length > 1)
								{
									if (!Text.IsAllowed(arguments[1]))
									{
										PlayerConsole.Message(this, "Usernames can only contain symbols A-z and 0-9!");
									}
									else if (!Text.Length(arguments[1], 3, 12))
									{
										PlayerConsole.Message(this, "Username is too long or too short, it must be from 3 to 12 characters long!");
									}
									else
									{
										string text94 = arguments[1].ToLower();
										int num155 = 0;
										Player[] array56 = Server.Online.ToArray();
										foreach (Player player51 in array56)
										{
											if (player51.Active && player51.Profile.Active && player51.Session.Active && Text.FilterColor(player51.Profile.Data.Username).ToLower() == text94.ToLower())
											{
												player51.Session.Teleport = Identifier;
												player51.Warp(Session.Data.Name, unban: true);
												PlayerConsole.Message(player51, "You've been summoned by ~1{0}~0!", Profile.Data.Username);
												PlayerConsole.Message(this, "Summoning ~1{0}~0.", player51.Profile.Data.Username);
												num155++;
											}
										}
										if (num155 == 0)
										{
											PlayerConsole.Message(this, "No players online with usernames starting with ~1{0}~0.", arguments[1]);
										}
									}
								}
								else
								{
									PlayerConsole.Message(this, "Command usage is ~1 / summon < Username > ~0, summons someone to your location.");
								}
							}
							else if (arguments[0].ToLower() == "/warpto" && Rewards.Permission(Profile.Data.Filename, Permissions.Mod))
							{
								if (arguments.Length > 1)
								{
									if (!Text.IsAllowed(arguments[1]))
									{
										PlayerConsole.Message(this, "Usernames can only contain symbols A-z and 0-9!");
									}
									else if (!Text.Length(arguments[1], 3, 12))
									{
										PlayerConsole.Message(this, "Username is too long or too short, it must be from 3 to 12 characters long!");
									}
									else
									{
										string text95 = arguments[1].ToLower();
										int num157 = 0;
										Player[] array57 = Server.Online.ToArray();
										foreach (Player player52 in array57)
										{
											if (player52.Active && player52.Profile.Active && player52.Session.Active && Text.FilterColor(player52.Profile.Data.Username).ToLower() == text95.ToLower())
											{
												Session.Teleport = player52.Identifier;
												Warp(player52.Session.Data.Name, unban: false);
												PlayerConsole.Message(this, "Warping to ~1{0}~0.", player52.Profile.Data.Username);
												num157++;
											}
										}
										if (num157 == 0)
										{
											PlayerConsole.Message(this, "No players online with usernames starting with ~1{0}~0.", arguments[1]);
										}
									}
								}
								else
								{
									PlayerConsole.Message(this, "Command usage is ~1 /warpto <Username> ~0, teleports you to player's location.");
								}
							}
							else if (arguments[0].ToLower() == "/warn" && Rewards.Permission(Profile.Data.Filename, Permissions.Guard))
							{
								if (arguments.Length > 2)
								{
									text88 = string.Join(" ", arguments, 2, arguments.Length - 2);
									delivered = false;
									username = arguments[1];
									warnings = 0;
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(35));
									binaryWriter.Write(Convert.ToUInt16(200));
									binaryWriter.Write(Convert.ToUInt16(1));
									binaryWriter.Write(Encoding.UTF8.GetBytes("~1YOU'VE BEEN ~3WARNED~1!\0"));
									binaryWriter.Write(Encoding.UTF8.GetBytes(text88 + "\0"));
									binaryWriter.Write(Encoding.UTF8.GetBytes("\0"));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Server.Broadcast(input.ToArray(), delegate(Player player)
									{
										if (!player.Active)
										{
											return false;
										}
										if (!player.Profile.Active)
										{
											return false;
										}
										if (Text.FilterColor(player.Profile.Data.Username).ToLower() != username.ToLower())
										{
											return false;
										}
										username = player.Profile.Data.Username;
										delivered = true;
										return true;
									});
									binaryWriter.Close();
									if (delivered)
									{
										PlayerConsole.Message(this, "Warning has been given to ~1{0}~0.", username);
									}
									else
									{
										PlayerConsole.Message(this, "Warning not sent, ~1{0} ~0is not online right now.", username);
									}
									if (delivered)
									{
										Server.SendReason(Profile.Data.Filename, $"Warned {username}");
										Server.Broadcast(input.ToArray(), delegate(Player player)
										{
											if (Text.FilterColor(player.Profile.Data.Username).ToLower() != username.ToLower())
											{
												return false;
											}
											if (player.Profile.Active)
											{
												warnings = player.Profile.Data.Warnings;
											}
											player.Profile.Data.Warnings = warnings + 1;
											return true;
										});
									}
								}
								else
								{
									PlayerConsole.Message(this, "Command usage is ~1/warn <Player> <Message>~0, sends a warning to a specified player.");
								}
							}
							else if (arguments[0].ToLower() == "/silentban" && Rewards.Permission(Profile.Data.Filename, Permissions.Mod))
							{
								if (arguments.Length > 3)
								{
									string name = arguments[1];
									int.TryParse(arguments[2], out var result37);
									string reason3 = string.Join(" ", arguments, 3, arguments.Length - 3);
									Punishment.Ban(this, name, result37 * 60, reason3, silent: true, warning: true, location: false);
								}
								else
								{
									PlayerConsole.Message(this, "Command usage is ~1/silentban <Player> <Minutes> <Reason>~0, bans specified account silently.");
								}
							}
							else if (arguments[0].ToLower() == "/sban" && Rewards.Permission(Profile.Data.Filename, Permissions.Mod))
							{
								if (arguments.Length > 3)
								{
									string name2 = arguments[1];
									int.TryParse(arguments[2], out var result38);
									string reason4 = string.Join(" ", arguments, 3, arguments.Length - 3);
									Punishment.Ban(this, name2, result38 * 60, reason4, silent: false, warning: true, location: false);
								}
								else
								{
									PlayerConsole.Message(this, "Command usage is ~1/sban <Player> <Minutes> <Reason>~0, bans specified account.");
								}
							}
							else if (arguments[0].ToLower() == "/hban" && Rewards.Permission(Profile.Data.Filename, Permissions.Mod))
							{
								if (arguments.Length > 3)
								{
									string name3 = arguments[1];
									int.TryParse(arguments[2], out var result39);
									string reason5 = string.Join(" ", arguments, 3, arguments.Length - 3);
									Punishment.Ban(this, name3, result39 * 60, reason5, silent: false, warning: true, location: true);
								}
								else
								{
									PlayerConsole.Message(this, "Command usage is ~1/ipban <Player> <Minutes> <Reason>~0, bans specified account and it's origin.");
								}
							}
							else if (arguments[0].ToLower() == "/smute" && Rewards.Permission(Profile.Data.Filename, Permissions.Guard))
							{
								if (arguments.Length > 3)
								{
									string name4 = arguments[1];
									int.TryParse(arguments[2], out var result40);
									string reason6 = string.Join(" ", arguments, 3, arguments.Length - 3);
									Punishment.Mute(this, name4, result40 * 60, reason6, silent: false, warning: true);
								}
								else
								{
									PlayerConsole.Message(this, "Command usage is ~1/smute <Player> <Minutes> <Reason>~0, mutes specified account.");
								}
							}
							else if (arguments[0].ToLower() == "/freeze" && Rewards.Permission(Profile.Data.Filename, Permissions.Mod))
							{
								bool flag13 = false;
								string text96 = string.Empty;
								if (arguments.Length > 1)
								{
									text96 = arguments[1];
								}
								Player[] array58 = Server.Online.ToArray();
								foreach (Player player53 in array58)
								{
									if (player53 != this && player53.Active && player53.Profile.Active && player53.Session.Active && !(player53.Session.Data.Name != Session.Data.Name) && player53.Profile.VisibleTo(this) && Text.FilterColor(player53.Profile.Data.Username).ToLower().StartsWith(text96.ToLower()))
									{
										PlayerControl.Freeze(this, player53);
										flag13 = true;
									}
								}
								if (!flag13)
								{
									if (text96.Length > 0)
									{
										PlayerConsole.Message(this, "No players found with usernames starting with ~1{0}~0.", text96);
									}
									else
									{
										PlayerConsole.Message(this, "There are no other players in this world.");
									}
								}
							}
							else if (arguments[0].ToLower() == "/inv" && Rewards.Permission(Profile.Data.Filename, Permissions.Guard))
							{
								Profile.Visible = !Profile.Visible;
								input = new MemoryStream();
								BinaryWriter binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(14));
								binaryWriter.Write(Convert.ToInt32(0));
								Profile.WriteData(binaryWriter, this);
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								Send(input.ToArray());
								binaryWriter.Close();
								Player[] array59 = Server.Online.ToArray();
								foreach (Player player54 in array59)
								{
									if (player54 != this && player54.Active && player54.Profile.Active && player54.Session.Active && !(player54.Session.Data.Name != Session.Data.Name))
									{
										input = new MemoryStream();
										binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(14));
										binaryWriter.Write(Convert.ToInt32(Identifier));
										Profile.WriteData(binaryWriter, player54);
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										player54.Send(input.ToArray());
										binaryWriter.Close();
									}
								}
								if (Profile.Visible)
								{
									PlayerConsole.Message(this, "~3Invisibility disabled. ~0You're visible to everyone now.");
								}
								else
								{
									PlayerConsole.Message(this, "~4Invisibility enabled. ~0Normal players can't see you anymore.");
								}
							}
							else if (arguments[0].ToLower() == "/ban")
							{
								bool flag14 = false;
								string text97 = string.Empty;
								if (arguments.Length > 1)
								{
									text97 = arguments[1];
								}
								Player[] array60 = Server.Online.ToArray();
								foreach (Player player55 in array60)
								{
									if (player55 != this && player55.Active && player55.Profile.Active && player55.Session.Active && !(player55.Session.Data.Name != Session.Data.Name) && player55.Profile.VisibleTo(this) && Text.FilterColor(player55.Profile.Data.Username).ToLower().StartsWith(text97.ToLower()))
									{
										PlayerControl.Ban(this, player55);
										flag14 = true;
									}
								}
								if (!flag14)
								{
									if (text97.Length > 0)
									{
										PlayerConsole.Message(this, "No players found with usernames starting with ~1{0}~0.", text97);
									}
									else
									{
										PlayerConsole.Message(this, "There are no players in this world.");
									}
								}
							}
							else if (arguments[0].ToLower() == "/bans")
							{
								List<string> list5 = new List<string>();
								foreach (BanData ban in Session.Data.Bans)
								{
									list5.Add(ban.Name);
								}
								if (list5.Count == 0)
								{
									PlayerConsole.Message(this, "There are no banned players in this world.");
								}
								else
								{
									PlayerConsole.Message(this, "World banned players: ~1{0}~0.", string.Join("~0, ~1", list5));
								}
							}
							else if (arguments[0].ToLower() == "/unban")
							{
								if (arguments.Length > 1)
								{
									PlayerControl.Unban(this, arguments[1]);
								}
								else
								{
									PlayerControl.Unban(this, string.Empty);
								}
							}
							else if (arguments[0].ToLower() == "/pull")
							{
								bool flag15 = false;
								string text98 = string.Empty;
								if (arguments.Length > 1)
								{
									text98 = arguments[1];
								}
								Player[] array61 = Server.Online.ToArray();
								foreach (Player player56 in array61)
								{
									if (player56 != this && player56.Active && player56.Profile.Active && player56.Session.Active && !(player56.Session.Data.Name != Session.Data.Name) && player56.Profile.VisibleTo(this) && Text.FilterColor(player56.Profile.Data.Username).ToLower().StartsWith(text98.ToLower()))
									{
										PlayerControl.Pull(this, player56);
										flag15 = true;
									}
								}
								if (!flag15)
								{
									if (text98.Length > 0)
									{
										PlayerConsole.Message(this, "No players found with usernames starting with ~1{0}~0.", text98);
									}
									else
									{
										PlayerConsole.Message(this, "There are no players in this world.");
									}
								}
							}
							else if (arguments[0].ToLower() == "/kill")
							{
								bool flag16 = false;
								string text99 = string.Empty;
								if (arguments.Length > 1)
								{
									text99 = arguments[1];
								}
								Player[] array62 = Server.Online.ToArray();
								foreach (Player player57 in array62)
								{
									if (player57 != this && player57.Active && player57.Profile.Active && player57.Session.Active && !(player57.Session.Data.Name != Session.Data.Name) && player57.Profile.VisibleTo(this) && Text.FilterColor(player57.Profile.Data.Username).ToLower().StartsWith(text99.ToLower()))
									{
										PlayerControl.Kill(this, player57);
										flag16 = true;
									}
								}
								if (!flag16)
								{
									if (text99.Length > 0)
									{
										PlayerConsole.Message(this, "No players found with usernames starting with ~1{0}~0.", text99);
									}
									else
									{
										PlayerConsole.Message(this, "There are no players in this world.");
									}
								}
							}
							else if (arguments[0].ToLower() == "/g")
							{
								if (arguments.Length > 1)
								{
									PlayerConsole.GlobalMessage(this, string.Join(" ", arguments, 1, arguments.Length - 1));
								}
								else
								{
									PlayerConsole.Message(this, "Command usage is ~1/g <Message>~0, sends a global message.");
								}
							}
							else if (arguments[0].ToLower() == "/p")
							{
								if (arguments.Length > 2)
								{
									PlayerConsole.PrivateMessage(this, arguments[1], string.Join(" ", arguments, 2, arguments.Length - 2));
								}
								else
								{
									PlayerConsole.Message(this, "Command usage is ~1/p <Player> <Message>~0, sends a private message to the player specified.");
								}
							}
							else if (arguments[0].ToLower() == "/r")
							{
								if (arguments.Length > 1)
								{
									if (string.IsNullOrEmpty(Profile.Reply))
									{
										PlayerConsole.Message(this, "Reply not sent, no recent private messages to reply to.");
									}
									else
									{
										PlayerConsole.PrivateMessage(this, Profile.Reply, string.Join(" ", arguments, 1, arguments.Length - 1));
									}
								}
								else
								{
									PlayerConsole.Message(this, "Command usage is ~1/r <Message>~0, sends a reply to the last private message.");
								}
							}
							else if (arguments[0].ToLower() == "/f")
							{
								if (arguments.Length > 1)
								{
									PlayerConsole.FriendMessage(this, string.Join(" ", arguments, 1, arguments.Length - 1));
								}
								else
								{
									PlayerConsole.Message(this, "Command usage is ~1/f <Message>~0, sends a message to all friends online.");
								}
							}
							else if (arguments[0].ToLower() == "/s" && Rewards.Permission(Profile.Data.Filename, Permissions.Guard))
							{
								if (arguments.Length > 1)
								{
									PlayerConsole.StaffMessage(this, string.Join(" ", arguments, 1, arguments.Length - 1));
								}
								else
								{
									PlayerConsole.Message(this, "Command usage is ~1/m <Message>~0, sends a message to all staff members.");
								}
							}
							else
							{
								PlayerConsole.Message(this, "~3Unknown command. ~0Type ~1/help ~0to see available commands.");
							}
						}
						else if (Profile.GetMute() > 0)
						{
							PlayerConsole.Message(this, "~3You are muted. ~0You will be able to talk again in ~1{0}~0.", Text.Time(Profile.GetMute()));
						}
						else if (Session.HasAntiTalk(this))
						{
							PlayerConsole.Message(this, "~3You are muted. ~0World owner does not allow visitors to talk here.");
						}
						else
						{
							if (Rewards.Capitalization.Contains(Profile.Data.Filename))
							{
								text88 = Text.Capitalize(text88);
							}
							input = new MemoryStream();
							BinaryWriter binaryWriter = new BinaryWriter(input);
							binaryWriter.Write(Convert.ToUInt16(0));
							binaryWriter.Write(Convert.ToUInt16(4));
							string text100 = $"~0[~1{Profile.Data.Username}~0] {text88}";
							binaryWriter.Write(Encoding.UTF8.GetBytes(text100 + "\0"));
							binaryWriter.Seek(0, SeekOrigin.Begin);
							binaryWriter.Write(Convert.ToUInt16(input.Length));
							Server.Broadcast(input.ToArray(), delegate(Player player)
							{
								if (!player.Active)
								{
									return false;
								}
								if (!player.Profile.Active)
								{
									return false;
								}
								if (!player.Session.Active)
								{
									return false;
								}
								return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
							});
							binaryWriter.Close();
							string text101 = Text.WordWrap(text88, 32);
							input = new MemoryStream();
							binaryWriter = new BinaryWriter(input);
							binaryWriter.Write(Convert.ToUInt16(0));
							binaryWriter.Write(Convert.ToUInt16(21));
							binaryWriter.Write(Convert.ToInt32(0));
							binaryWriter.Write(Encoding.UTF8.GetBytes(text101 + "\0"));
							binaryWriter.Write(Convert.ToUInt16(25));
							binaryWriter.Seek(0, SeekOrigin.Begin);
							binaryWriter.Write(Convert.ToUInt16(input.Length));
							Send(input.ToArray());
							binaryWriter.Close();
							input = new MemoryStream();
							binaryWriter = new BinaryWriter(input);
							binaryWriter.Write(Convert.ToUInt16(0));
							binaryWriter.Write(Convert.ToUInt16(21));
							binaryWriter.Write(Convert.ToInt32(Identifier));
							binaryWriter.Write(Encoding.UTF8.GetBytes(text101 + "\0"));
							binaryWriter.Write(Convert.ToUInt16(25));
							binaryWriter.Seek(0, SeekOrigin.Begin);
							binaryWriter.Write(Convert.ToUInt16(input.Length));
							Server.Broadcast(input.ToArray(), delegate(Player player)
							{
								if (player == this)
								{
									return false;
								}
								if (!player.Active)
								{
									return false;
								}
								if (!player.Profile.Active)
								{
									return false;
								}
								if (!player.Session.Active)
								{
									return false;
								}
								return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
							});
							binaryWriter.Close();
						}
						LastMessage = DateTime.UtcNow.Ticks / 10000;
						break;
					}
					case 5:
					{
						worker = binaryReader.ReadUInt16();
						string text35 = binaryReader.ReadString();
						string text36 = binaryReader.ReadString();
						Dictionary<string, string> dictionary = new Dictionary<string, string>();
						Dictionary<string, ushort> dictionary2 = new Dictionary<string, ushort>();
						int num51 = binaryReader.ReadUInt16();
						int num52 = binaryReader.ReadUInt16();
						for (int num53 = 0; num53 < num51; num53++)
						{
							string key = binaryReader.ReadString();
							string value2 = binaryReader.ReadString();
							dictionary.Add(key, value2);
						}
						for (int num54 = 0; num54 < num52; num54++)
						{
							string key2 = binaryReader.ReadString();
							ushort value3 = binaryReader.ReadUInt16();
							dictionary2.Add(key2, value3);
						}
						binaryReader.Close();
						if (Window == text35)
						{
							Window = string.Empty;
							if (Profile.Active && text35 == "Dialog.Creator" && text36 == "Run" && Rewards.Permission(Profile.Data.Filename, Permissions.Guard))
							{
								string text37 = string.Empty;
								for (int num55 = 0; num55 < 20; num55++)
								{
									text37 += $"{dictionary[num55.ToString()]}\n";
								}
								input = new MemoryStream();
								BinaryWriter binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(5));
								DialogString.Parse(binaryWriter, text37);
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								Send(input.ToArray());
								binaryWriter.Close();
							}
							if (Profile.Active && text35 == "Item")
							{
								ushort num56 = Convert.ToUInt16(Profile.Data.ItemIndex[worker]);
								ushort num57 = Convert.ToUInt16(Profile.Data.ItemCount[worker]);
								if (text36 == "Exchange")
								{
									ExchangeData exchangeData = Item.Exchange(num56);
									if (exchangeData.Amount > 0)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, worker, "Item.Exchange");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Exchange " + Item.Name(num56), 75, num56);
										Dialog.Text(binaryWriter, breaker: true, "Would you like to exchange items?", 50);
										Dialog.Text(binaryWriter, breaker: true, "~3You'll give:", 50);
										Dialog.ItemText(binaryWriter, breaker: true, $"~1{exchangeData.Amount}x ~0{Item.Name(num56)}", 50, num56);
										Dialog.Text(binaryWriter, breaker: true, "~4You'll get:", 50);
										Dialog.ItemText(binaryWriter, breaker: true, $"~1{exchangeData.Count}x ~0{Item.Name(exchangeData.Index)}", 50, exchangeData.Index);
										Dialog.Button(binaryWriter, breaker: false, "Exchange", "Exchange");
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
								if (text36 == "Consume")
								{
									if (num56 == 289 || num56 == 609)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, worker, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1" + Item.Name(num56), 75, num56);
										Dialog.Text(binaryWriter, breaker: true, "Tap on a tree while holding this to water it.", 50);
										Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (Item.Fish(num56) != 0)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, worker, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1" + Item.Name(num56), 75, num56);
										Dialog.Text(binaryWriter, breaker: true, "Put this in a smokehouse to earn some gems.", 50);
										Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (Item.Bait(num56) != 0)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, worker, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1" + Item.Name(num56), 75, num56);
										Dialog.Text(binaryWriter, breaker: true, "Tap on water while holding this to fish.", 50);
										Dialog.Text(binaryWriter, breaker: true, "A fishing rod must be equipped as well.", 50);
										Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (num56 == 537 || num56 == 539 || num56 == 541)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, worker, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1" + Item.Name(num56), 75, num56);
										Dialog.Text(binaryWriter, breaker: true, "Tap anywhere around yourself to fire this.", 50);
										Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, worker, "Item.Consume");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Consume " + Item.Name(num56), 75, num56);
										Dialog.Text(binaryWriter, breaker: true, "~3WARNING: ~0This item will be gone", 50);
										Dialog.Text(binaryWriter, breaker: true, "after using it, so do it carefully.", 50);
										Dialog.Checkbox(binaryWriter, breaker: true, value: false, "Confirm", "Confirm this action", 50);
										Dialog.Button(binaryWriter, breaker: false, "Consume", "Consume");
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
								if (text36 == "Wear" && Item.Type(num56) == 4)
								{
									if (Profile.GetItemPart(num56) > 0)
									{
										PlayerConsole.Message(this, "Unequipped ~1{0}~0.", Item.Name(num56));
										Profile.Unequip(num56);
									}
									else
									{
										PlayerConsole.Message(this, "Equipped ~1{0}~0.", Item.Name(num56));
										Profile.Equip(num56);
									}
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(6));
									Profile.WriteItems(binaryWriter);
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
									input = new MemoryStream();
									binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(14));
									binaryWriter.Write(Convert.ToInt32(0));
									Profile.WriteData(binaryWriter, this);
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
									Player[] array8 = Server.Online.ToArray();
									foreach (Player player9 in array8)
									{
										if (player9 != this && player9.Active && player9.Profile.Active && player9.Session.Active && !(player9.Session.Data.Name != Session.Data.Name))
										{
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(14));
											binaryWriter.Write(Convert.ToInt32(Identifier));
											Profile.WriteData(binaryWriter, player9);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											player9.Send(input.ToArray());
											binaryWriter.Close();
										}
									}
								}
								if (text36 == "Drop")
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, worker, "Item.Drop");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Drop " + Item.Name(num56), 75, num56);
									Dialog.Text(binaryWriter, breaker: true, "How many would you like to drop?", 50);
									Dialog.Textbox(binaryWriter, breaker: true, "Count", num57.ToString(), 5);
									Dialog.Button(binaryWriter, breaker: false, "Drop", "Drop");
									Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								if (text36 == "Trash")
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, worker, "Item.Trash");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Trash " + Item.Name(num56), 75, num56);
									Dialog.Text(binaryWriter, breaker: true, "How many would you like to trash?", 50);
									Dialog.Textbox(binaryWriter, breaker: true, "Count", num57.ToString(), 5);
									Dialog.Checkbox(binaryWriter, breaker: true, value: false, "Confirm", "Confirm this action", 50);
									Dialog.Button(binaryWriter, breaker: false, "Trash", "Trash");
									Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								if (text36 == "Info")
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, worker, "Message");
									Dialog.ItemText(binaryWriter, breaker: true, "~1About " + Item.Name(num56), 75, num56);
									Dialog.Text(binaryWriter, breaker: true, Item.Info(num56), 50);
									Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								if (text36 == "Edit" && Rewards.Permission(Profile.Data.Filename, Permissions.Dev))
								{
									ItemData itemData = Item.List[num56];
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, num56, "Item.Edit");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Edit " + Item.Name(num56), 75, num56);
									Dialog.Text(binaryWriter, breaker: true, "What's the item name?", 50);
									Dialog.Textbox(binaryWriter, breaker: true, "Name", itemData.Name, 32);
									Dialog.Text(binaryWriter, breaker: true, "What's the item info?", 50);
									Dialog.Textbox(binaryWriter, breaker: true, "Info", itemData.Info, 32);
									Dialog.Text(binaryWriter, breaker: true, "What's the item type?", 50);
									Dialog.Textbox(binaryWriter, breaker: true, "Type", itemData.Type.ToString(), 8);
									Dialog.Text(binaryWriter, breaker: true, "What's the item part ID?", 50);
									Dialog.Textbox(binaryWriter, breaker: true, "Part", itemData.Part.ToString(), 8);
									Dialog.Text(binaryWriter, breaker: true, "What's the item rarity?", 50);
									Dialog.Textbox(binaryWriter, breaker: true, "Rarity", itemData.Rarity.ToString(), 8);
									Dialog.Text(binaryWriter, breaker: true, "What's the item hardness?", 50);
									Dialog.Textbox(binaryWriter, breaker: true, "Hardness", itemData.Hardness.ToString(), 8);
									Dialog.Text(binaryWriter, breaker: true, "What's the item farmability?", 50);
									Dialog.Textbox(binaryWriter, breaker: true, "Farmability", itemData.Farmability.ToString(), 8);
									Dialog.Checkbox(binaryWriter, breaker: true, itemData.Tradeable, "Tradeable", "Item Tradeable", 50);
									Dialog.Checkbox(binaryWriter, breaker: true, itemData.Trashable, "Trashable", "Item Trashable", 50);
									Dialog.Checkbox(binaryWriter, breaker: true, itemData.Droppable, "Droppable", "Item Droppable", 50);
									Dialog.Checkbox(binaryWriter, breaker: true, itemData.Lockable, "Lockable", "Item Lockable", 50);
									Dialog.Checkbox(binaryWriter, breaker: true, itemData.Vendable, "Vendable", "Item Vendable", 50);
									Dialog.Checkbox(binaryWriter, breaker: true, itemData.Solid, "Solid", "Item Solid", 50);
									Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
									Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
							}
							if (Profile.Active && text35 == "Item.Exchange" && text36 == "Exchange")
							{
								ushort num59 = Convert.ToUInt16(Profile.Data.ItemIndex[worker]);
								ushort num60 = Convert.ToUInt16(Profile.Data.ItemCount[worker]);
								ExchangeData exchangeData2 = Item.Exchange(num59);
								if (exchangeData2.Amount > 0)
								{
									if (Profile.GetItem(num59) < exchangeData2.Amount)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, worker, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Exchange " + Item.Name(num59), 75, num59);
										Dialog.Text(binaryWriter, breaker: true, "~3Not enough items to exchange.", 50);
										Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (!Profile.CanGetItem(exchangeData2.Index, exchangeData2.Count))
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, worker, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Exchange " + Item.Name(num59), 75, num59);
										Dialog.Text(binaryWriter, breaker: true, "~3Not enough inventory space.", 50);
										Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else
									{
										Profile.SetItem(num59, Profile.GetItem(num59) - exchangeData2.Amount);
										Profile.SetItem(exchangeData2.Index, Profile.GetItem(exchangeData2.Index) + exchangeData2.Count);
										PlayerConsole.Message(this, "Exchanged ~1x{0} {1} ~0for ~1x{2} {3}~0.", exchangeData2.Amount, Item.Name(num59), exchangeData2.Count, Item.Name(exchangeData2.Index));
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(6));
										Profile.WriteItems(binaryWriter);
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
							}
							if (Profile.Active && text35 == "Item.Consume" && text36 == "Consume")
							{
								ushort num61 = Convert.ToUInt16(Profile.Data.ItemIndex[worker]);
								ushort num62 = Convert.ToUInt16(Profile.Data.ItemCount[worker]);
								if (dictionary2["Confirm"] == 0)
								{
									PlayerConsole.Message(this, "You must confirm this action.");
								}
								else if (num61 == 147 || num61 == 149 || num61 == 151 || num61 == 227 || num61 == 463 || num61 == 1063 || num61 == 1127)
								{
									if (!Session.WorldOwner(Profile.Data.Filename))
									{
										PlayerConsole.Message(this, "Changing world theme requires owner acccess.");
									}
									else
									{
										Profile.SetItem(num61, Profile.GetItem(num61) - 1);
										PlayerCore.UpdateInventory(this);
										if (num61 == 147)
										{
											Session.Data.Theme = 1;
										}
										if (num61 == 149)
										{
											Session.Data.Theme = 2;
										}
										if (num61 == 151)
										{
											Session.Data.Theme = 3;
										}
										if (num61 == 227)
										{
											Session.Data.Theme = 4;
										}
										if (num61 == 463)
										{
											Session.Data.Theme = 5;
										}
										if (num61 == 1063)
										{
											Session.Data.Theme = 6;
										}
										if (num61 == 1127)
										{
											Session.Data.Theme = 7;
										}
										Database.SessionSave(Session.Data);
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(20));
										binaryWriter.Write(Convert.ToUInt16(Session.Data.Theme));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Server.Broadcast(input.ToArray(), delegate(Player player)
										{
											if (!player.Active)
											{
												return false;
											}
											if (!player.Profile.Active)
											{
												return false;
											}
											if (!player.Session.Active)
											{
												return false;
											}
											return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
										});
										binaryWriter.Close();
									}
								}
								else if (num61 == 165 || num61 == 167 || num61 == 169 || num61 == 171)
								{
									if (!Profile.HasItem(num61, 24))
									{
										PlayerConsole.Message(this, "You need 24 crystal fragments to make this.");
									}
									else if (!Profile.HasItem(163, 4))
									{
										PlayerConsole.Message(this, "You need 4 plastic blocks to make this.");
									}
									else
									{
										int num63 = 0;
										if (num61 == 165)
										{
											num63 = 173;
										}
										if (num61 == 167)
										{
											num63 = 175;
										}
										if (num61 == 169)
										{
											num63 = 177;
										}
										if (num61 == 171)
										{
											num63 = 179;
										}
										if (!Profile.CanGetItem(num63, 1))
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(4));
											string text38 = "You don't have enough inventory space.";
											binaryWriter.Write(Encoding.UTF8.GetBytes(text38 + "\0"));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else
										{
											Profile.SetItem(163, Profile.GetItem(163) - 4);
											Profile.SetItem(num61, Profile.GetItem(num61) - 24);
											Profile.SetItem(num63, Profile.GetItem(num63) + 1);
											PlayerConsole.Message(this, "You crafted ~1{0}~0!", Item.Name(num63));
											PlayerCore.UpdateInventory(this);
										}
									}
								}
								else
								{
									switch (num61)
									{
									case 431:
										if (!Profile.HasItem(431, 1))
										{
											PlayerConsole.Message(this, "The ticket has disappeared from your inventory somehow.");
											break;
										}
										if (!Event.Active)
										{
											PlayerConsole.Message(this, "Event is over, ticket is not valid anymore. Maybe you can use it later in another event?");
											break;
										}
										if (Session.Data.Name == Event.World)
										{
											PlayerConsole.Message(this, "You're already in the event world, no point of wasting another ticket.");
											break;
										}
										Profile.Data.TicketDuration = Event.TicketDuration();
										Profile.SetItem(431, Profile.GetItem(431) - 1);
										PlayerCore.UpdateInventory(this);
										if (string.IsNullOrEmpty(Event.World))
										{
											break;
										}
										PlayerConsole.Message(this, "Ticket has been activated, warping to the event world. You'll have 5 minutes to complete it!");
										Warp(Event.World, unban: true);
										PlayerCore.UpdateTimer(this, Event.TimeToComplete * 60, delegate
										{
											if (Event.Active && Event.World == Session.Data.Name)
											{
												PlayerLayout.Warning(this, 200, 3, "~1YOU ~3FAILED~1!", "You didn't finish in time.", "Warping out.");
												Warp(Session.Random(this), unban: false);
											}
										});
										break;
									case 611:
										if (Session.WorldOwner(Profile.Data.Filename))
										{
											Profile.SetItem(num61, Profile.GetItem(num61) - 1);
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(6));
											Profile.WriteItems(binaryWriter);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
											Session.Blast(num61);
											Player[] array9 = Server.Online.ToArray();
											foreach (Player player10 in array9)
											{
												if (player10.Active && player10.Profile.Active && player10.Session.Active && !(player10.Session.Data.Name != Session.Data.Name))
												{
													input = new MemoryStream();
													binaryWriter = new BinaryWriter(input);
													binaryWriter.Write(Convert.ToUInt16(0));
													binaryWriter.Write(Convert.ToUInt16(10));
													player10.Session.WriteTiles(binaryWriter, player10.Profile.Data.Filename);
													binaryWriter.Seek(0, SeekOrigin.Begin);
													binaryWriter.Write(Convert.ToUInt16(input.Length));
													player10.Send(input.ToArray());
													binaryWriter.Close();
												}
											}
										}
										else
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(4));
											string text39 = "World must be locked by you in order to blast it.";
											binaryWriter.Write(Encoding.UTF8.GetBytes(text39 + "\0"));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										break;
									case 965:
										if (Profile.HasItem(num61, 1))
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, num61, "Item.Template");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Confirm template", 75, num61);
											Dialog.Text(binaryWriter, breaker: true, "What's your desired world name?", 50);
											Dialog.Textbox(binaryWriter, breaker: true, "World", string.Empty, 32);
											Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
											Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										break;
									}
								}
							}
							if (Profile.Active && text35 == "Item.Drop" && text36 == "Drop")
							{
								ushort num65 = Convert.ToUInt16(Profile.Data.ItemIndex[worker]);
								ushort num66 = Convert.ToUInt16(Profile.Data.ItemCount[worker]);
								ushort.TryParse(dictionary["Count"], out var result);
								if (!Session.CanDrop())
								{
									PlayerConsole.Message(this, "There are too many dropped items, pick some up and try again.");
								}
								else if (Session.HasAntiDrop(this))
								{
									PlayerConsole.Message(this, "World owner doesn't allow visitors to drop items.");
								}
								else if (!Item.Droppable(num65))
								{
									PlayerConsole.Message(this, "This item is too special to be dropped somewhere.");
								}
								else if (result < 1 || result > 500)
								{
									PlayerConsole.Message(this, "The item count you specified doesn't seem valid, try again?");
								}
								else if (!Profile.HasItem(num65, result))
								{
									PlayerConsole.Message(this, "You don't have that much of this item.");
								}
								else
								{
									int num67;
									int num68;
									if (PreviousX > CurrentX)
									{
										num67 = CurrentX - 36;
										num68 = CurrentY + 6;
									}
									else
									{
										num67 = CurrentX + 32;
										num68 = CurrentY + 6;
									}
									int num69 = (int)Math.Floor((double)(num67 + 10) / 32.0);
									int num70 = (int)Math.Floor((double)(num68 + 10) / 32.0);
									int num71 = num69 + num70 * Session.Data.SizeX;
									if (num67 < 0 || num67 >= Session.Data.SizeX * 32 - 20 || num68 < 0 || num68 >= Session.Data.SizeY * 32 - 20)
									{
										PlayerConsole.Message(this, "Drop failed, how would you drop outside world?");
									}
									else if (Item.Solid(Session.Data.Foreground[num71]) || Session.Data.Foreground[num71] == 195 || Session.Data.Foreground[num71] == 205)
									{
										PlayerConsole.Message(this, "Drop failed, you can't drop anything inside a block.");
									}
									else
									{
										Profile.SetItem(num65, Profile.GetItem(num65) - result);
										PlayerConsole.Message(this, "Dropped ~1x{0} {1}~0.", result, Item.Name(num65));
										if ((!Item.Default(num65) && Item.Farmability(num65) < 4 && Item.Rarity(num65) > 10) || !Item.Lockable(num65))
										{
											Server.DropLogs("Drop Logs (" + Profile.Data.Filename + ")", string.Format(Profile.Data.Filename + " dropped (x" + result + " of " + Item.Name(num65) + ") in world " + Session.Data.Name));
										}
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(6));
										Profile.WriteItems(binaryWriter);
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
										input = new MemoryStream();
										binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(14));
										binaryWriter.Write(Convert.ToInt32(0));
										Profile.WriteData(binaryWriter, this);
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
										Player[] array10 = Server.Online.ToArray();
										foreach (Player player11 in array10)
										{
											if (player11 != this && player11.Active && player11.Profile.Active && player11.Session.Active && !(player11.Session.Data.Name != Session.Data.Name))
											{
												input = new MemoryStream();
												binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(14));
												binaryWriter.Write(Convert.ToInt32(Identifier));
												Profile.WriteData(binaryWriter, player11);
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												player11.Send(input.ToArray());
												binaryWriter.Close();
											}
										}
										input = new MemoryStream();
										binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(12));
										Session.Drop(binaryWriter, num65, result, num67, num68);
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Server.Broadcast(input.ToArray(), delegate(Player player)
										{
											if (!player.Active)
											{
												return false;
											}
											if (!player.Profile.Active)
											{
												return false;
											}
											if (!player.Session.Active)
											{
												return false;
											}
											return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
										});
										binaryWriter.Close();
									}
								}
							}
							if (Profile.Active && text35 == "Item.Trash" && text36 == "Trash")
							{
								ushort num73 = Convert.ToUInt16(Profile.Data.ItemIndex[worker]);
								ushort num74 = Convert.ToUInt16(Profile.Data.ItemCount[worker]);
								ushort.TryParse(dictionary["Count"], out var result2);
								if (dictionary2["Confirm"] == 0)
								{
									PlayerConsole.Message(this, "You must confirm this action.");
								}
								else if (!Item.Trashable(num73))
								{
									PlayerConsole.Message(this, "This item is too special to be trashed.");
								}
								else if (result2 < 1 || result2 > 500)
								{
									PlayerConsole.Message(this, "The item count you specified doesn't seem valid.");
								}
								else if (!Profile.HasItem(num73, result2))
								{
									PlayerConsole.Message(this, "~3You don't have that much items to trash.");
								}
								else
								{
									Profile.SetItem(num73, Profile.GetItem(num73) - result2);
									PlayerConsole.Message(this, "Trashed ~1x{0} {1}~0.", result2, Item.Name(num73));
									Achievement(AchievementType.TrashItem, increase: true, result2);
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(6));
									Profile.WriteItems(binaryWriter);
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
									input = new MemoryStream();
									binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(14));
									binaryWriter.Write(Convert.ToInt32(0));
									Profile.WriteData(binaryWriter, this);
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
									Player[] array11 = Server.Online.ToArray();
									foreach (Player player12 in array11)
									{
										if (player12 != this && player12.Active && player12.Profile.Active && player12.Session.Active && !(player12.Session.Data.Name != Session.Data.Name))
										{
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(14));
											binaryWriter.Write(Convert.ToInt32(Identifier));
											Profile.WriteData(binaryWriter, player12);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											player12.Send(input.ToArray());
											binaryWriter.Close();
										}
									}
								}
							}
							if (Profile.Active && text35 == "Item.Edit" && text36 == "Accept" && Rewards.Permission(Profile.Data.Filename, Permissions.Dev))
							{
								ItemData itemData2 = default(ItemData);
								itemData2.ID = worker;
								itemData2.Name = dictionary["Name"];
								itemData2.Info = dictionary["Info"];
								ItemData value4 = itemData2;
								ushort.TryParse(dictionary["Type"], out value4.Type);
								ushort.TryParse(dictionary["Part"], out value4.Part);
								ushort.TryParse(dictionary["Rarity"], out value4.Rarity);
								ushort.TryParse(dictionary["Hardness"], out value4.Hardness);
								ushort.TryParse(dictionary["Farmability"], out value4.Farmability);
								value4.Tradeable = Convert.ToBoolean(dictionary2["Tradeable"]);
								value4.Trashable = Convert.ToBoolean(dictionary2["Trashable"]);
								value4.Droppable = Convert.ToBoolean(dictionary2["Droppable"]);
								value4.Lockable = Convert.ToBoolean(dictionary2["Lockable"]);
								value4.Vendable = Convert.ToBoolean(dictionary2["Vendable"]);
								value4.Solid = Convert.ToBoolean(dictionary2["Solid"]);
								Item.List[worker] = value4;
								Server.SendLog(Profile.Data.Filename, $"Has updated information of item {worker}");
							}
							if (Profile.Active && text35 == "Item.Template" && text36 == "Accept" && Profile.HasItem(worker, 1))
							{
								int num76 = 0;
								if (worker == 965)
								{
									num76 = 1;
								}
								if (num76 == 0)
								{
									PlayerConsole.Message(this, "This item can't be used as a template.");
								}
								else if (Session.Template(dictionary["World"], num76))
								{
									Profile.SetItem(worker, Profile.GetItem(worker) - 1);
									PlayerConsole.Message(this, "A template has been sucessfully applied.");
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(6));
									Profile.WriteItems(binaryWriter);
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
									Warp(dictionary["World"], unban: false);
								}
								else
								{
									PlayerConsole.Message(this, "Template couldn't be used, try later or try using another name.");
								}
							}
							if (Profile.Active && text35 == "Popup.PayPal" && text36 == "Accept")
							{
								input = new MemoryStream();
								BinaryWriter binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(27));
								string text40 = "https://breaworldsgame.com/payment";
								binaryWriter.Write(Encoding.UTF8.GetBytes(text40 + "\0"));
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								Send(input.ToArray());
								binaryWriter.Close();
								Profile.Data.HasReviewed = true;
								Database.ProfileSave(Profile.Data);
								Server.SendLog(Profile.Data.Filename, "Has been redirected to PayPal.");
							}
							if (Profile.Active && text35 == "Popup.Review" && text36 == "Now")
							{
								input = new MemoryStream();
								BinaryWriter binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(27));
								string text41 = "https://play.google.com/store/apps/details?id=com.breaworlds.app";
								binaryWriter.Write(Encoding.UTF8.GetBytes(text41 + "\0"));
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								Send(input.ToArray());
								binaryWriter.Close();
								Profile.Data.HasReviewed = true;
								Database.ProfileSave(Profile.Data);
								Server.SendLog(Profile.Data.Filename, "Has accepted review request.");
							}
							if (Profile.Active && text35 == "Challenge")
							{
								if (text36 == "Register")
								{
									if (!Challenge.Active)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, 0, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Weekly challenge", 75, 3);
										Dialog.Text(binaryWriter, breaker: true, "Sorry, registration for weekly challenge is unavailable.", 50);
										Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (!Challenge.IsParticipant(Profile.Data.Filename))
									{
										if (Challenge.CurrentDay() != 1)
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, 0, "Message");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Weekly challenge", 75, 3);
											Dialog.Text(binaryWriter, breaker: true, "You are too late, registration day is over.", 50);
											Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else if (Challenge.Data.Participants.Count >= 500)
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, 0, "Message");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Weekly challenge", 75, 3);
											Dialog.Text(binaryWriter, breaker: true, "There are too much participants already.", 50);
											Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else if (!PlayerGems.Has(this, Challenge.Price))
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, 0, "Message");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Weekly challenge", 75, 3);
											Dialog.Text(binaryWriter, breaker: true, "Not enough gems to register for weekly challenge.", 50);
											Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else
										{
											PlayerGems.Remove(this, Challenge.Price);
											PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
											{
												Dialog.ItemText(dialog, breaker: true, "~1Weekly challenge", 75, 3);
												Dialog.Text(dialog, breaker: true, "You've registered for the upcoming weekly challenge.", 50);
												Dialog.Button(dialog, breaker: false, "Okay", "Okay");
											});
											Challenge.Register(Profile.Data.Filename);
										}
									}
								}
								if (text36 == "Leaderboard")
								{
									ChallengeParticipant[] array12 = Challenge.WeeklyLeaderboard();
									int num77 = Math.Min(array12.Length, 10);
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Challenge");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Leaderboard", 75, 3);
									Dialog.Text(binaryWriter, breaker: true, $"Showing {num77}/{array12.Length} participants", 50);
									for (int num78 = 0; num78 < num77; num78++)
									{
										ChallengeParticipant participant = array12[num78];
										Dialog.Text(binaryWriter, breaker: true, $"#{num78 + 1} ~1{participant.Username} ~0(~1{Challenge.ParticipantPoints(participant)} ~0points)", 50);
									}
									Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								if (text36 == "Reward")
								{
									ChallengeParticipant challengeParticipant = Challenge.FindParticipant(Profile.Data.Filename);
									if (Challenge.Active && Challenge.IsParticipant(Profile.Data.Filename) && !challengeParticipant.Rewarded && Challenge.CurrentDay() == 7)
									{
										int num79 = Challenge.ParticipantPlace(Profile.Data.Filename);
										int num80 = 0;
										int num81 = 0;
										if (num79 == 1)
										{
											num80 = 0;
											num81 = 50000;
										}
										if (num79 == 2)
										{
											num80 = 0;
											num81 = 20000;
										}
										if (num79 == 3)
										{
											num80 = 0;
											num81 = 10000;
										}
										if (num80 > 0 && Profile.CanGetItem(num80, 1))
										{
											Profile.SetItem(num80, Profile.GetItem(num80) + 1);
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(6));
											Profile.WriteItems(binaryWriter);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										if (num81 > 0)
										{
											PlayerGems.Add(this, num81);
										}
										if (num79 >= 1 && num79 <= 3)
										{
											if (num79 == 1)
											{
												Experience(randomize: false, 1000);
											}
											if (num79 == 2)
											{
												Experience(randomize: false, 500);
											}
											if (num79 == 3)
											{
												Experience(randomize: false, 250);
											}
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, 0, "Message");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Weekly challenge", 75, 3);
											if (num80 > 0 && num81 > 0)
											{
												Dialog.Text(binaryWriter, breaker: true, $"You've been rewarded with ~1{num81} ~0gems", 50);
												Dialog.Text(binaryWriter, breaker: true, $"and one ~1{Item.Name(num80)}~0!", 50);
											}
											else if (num80 > 0)
											{
												Dialog.Text(binaryWriter, breaker: true, $"You've been rewarded with ~1{Item.Name(num80)}~0!", 50);
											}
											else if (num81 > 0)
											{
												Dialog.Text(binaryWriter, breaker: true, $"You've been rewarded with ~1{num81} ~0gems.", 50);
											}
											Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
											Challenge.ParticipantReward(Profile.Data.Filename);
										}
										Server.SendLog(Profile.Data.Filename, $"Has taken their {Text.Ordinal(num79)} place weekly challenges reward");
									}
								}
							}
							if (Profile.Active && text35 == "Shop.Purchase" && text36 == "Accept")
							{
								ShopCategory shopCategory = Shop.Categories[ShopPage];
								ShopListing listing = shopCategory.Listings[worker];
								if (shopCategory.Currency == CurrencyType.Gems)
								{
									items3 = Shop.Purchase(listing);
									if (!Shop.ListingAvailable(listing))
									{
										PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
										{
											Dialog.ItemText(dialog, breaker: true, "~1Item unavailable", 75, 3);
											Dialog.Text(dialog, breaker: true, "You're late! This item is not available anymore.", 50);
											Dialog.Button(dialog, breaker: false, "Okay", "Okay");
										});
									}
									else if (!PlayerGems.Has(this, listing.Price))
									{
										PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
										{
											Dialog.ItemText(dialog, breaker: true, "~1Not enough gems", 75, 3);
											Dialog.Text(dialog, breaker: true, "Sorry, you don't have enough", 50);
											Dialog.Text(dialog, breaker: true, "gems to purchase this item.", 50);
											Dialog.Button(dialog, breaker: false, "Okay", "Okay");
										});
									}
									else if (!Profile.CanGetListing(listing))
									{
										PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
										{
											Dialog.ItemText(dialog, breaker: true, "~1Not enough space", 75, 3);
											Dialog.Text(dialog, breaker: true, "Sorry, you don't have enough space in", 50);
											Dialog.Text(dialog, breaker: true, "your inventory to purchase this item.", 50);
											Dialog.Button(dialog, breaker: false, "Okay", "Okay");
										});
									}
									else
									{
										PlayerGems.Remove(this, listing.Price);
										Achievement(AchievementType.SpendGems, increase: true, listing.Price);
										ShopItem[] array13 = items3;
										for (int num82 = 0; num82 < array13.Length; num82++)
										{
											ShopItem shopItem = array13[num82];
											Profile.SetItem(shopItem.Index, Profile.GetItem(shopItem.Index) + shopItem.Count);
											PlayerConsole.Message(this, "You received ~1x{0} {1}~0!", shopItem.Count, Item.Name(shopItem.Index));
										}
										PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
										{
											Dialog.ItemText(dialog, breaker: true, "~1Purchase complete", 75, 3);
											Dialog.Text(dialog, breaker: true, $"You spent ~1{listing.Price} ~0gems.", 50);
											Dialog.Text(dialog, breaker: true, "You received the following items:", 50);
											Dialog.Space(dialog);
											ShopItem[] array70 = items3;
											for (int num187 = 0; num187 < array70.Length; num187++)
											{
												ShopItem shopItem6 = array70[num187];
												Dialog.ItemText(dialog, breaker: true, $"x{shopItem6.Count} {Item.Name(shopItem6.Index)}", 50, shopItem6.Index);
											}
											Dialog.Button(dialog, breaker: false, "Okay", "Okay");
										});
										PlayerCore.UpdateInventory(this);
									}
								}
								else if (shopCategory.Currency == CurrencyType.Tokens)
								{
									items2 = Shop.Purchase(listing);
									if (!Shop.ListingAvailable(listing))
									{
										PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
										{
											Dialog.ItemText(dialog, breaker: true, "~1Item unavailable", 75, 3);
											Dialog.Text(dialog, breaker: true, "You're late! This item is not available anymore.", 50);
											Dialog.Button(dialog, breaker: false, "Okay", "Okay");
										});
									}
									else if (!Profile.HasItem(283, listing.Price))
									{
										PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
										{
											Dialog.ItemText(dialog, breaker: true, "~1Not enough tokens", 75, 3);
											Dialog.Text(dialog, breaker: true, "Sorry, you don't have enough", 50);
											Dialog.Text(dialog, breaker: true, "tokens to purchase this item.", 50);
											Dialog.Button(dialog, breaker: false, "Okay", "Okay");
										});
									}
									else if (!Profile.CanGetListing(listing))
									{
										PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
										{
											Dialog.ItemText(dialog, breaker: true, "~1Not enough space", 75, 3);
											Dialog.Text(dialog, breaker: true, "Sorry, you don't have enough space in", 50);
											Dialog.Text(dialog, breaker: true, "your inventory to purchase this item.", 50);
											Dialog.Button(dialog, breaker: false, "Okay", "Okay");
										});
									}
									else
									{
										Profile.SetItem(283, Profile.GetItem(283) - listing.Price);
										Achievement(AchievementType.SpendTokens, increase: true, listing.Price);
										ShopItem[] array14 = items2;
										for (int num83 = 0; num83 < array14.Length; num83++)
										{
											ShopItem shopItem2 = array14[num83];
											Profile.SetItem(shopItem2.Index, Profile.GetItem(shopItem2.Index) + shopItem2.Count);
											PlayerConsole.Message(this, "You received ~1x{0} {1}~0!", shopItem2.Count, Item.Name(shopItem2.Index));
										}
										PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
										{
											Dialog.ItemText(dialog, breaker: true, "~1Purchase complete", 75, 3);
											Dialog.Text(dialog, breaker: true, $"You spent ~1{listing.Price} ~0tokens.", 50);
											Dialog.Text(dialog, breaker: true, "You have got the following items", 50);
											Dialog.Space(dialog);
											ShopItem[] array69 = items2;
											for (int num186 = 0; num186 < array69.Length; num186++)
											{
												ShopItem shopItem5 = array69[num186];
												Dialog.ItemText(dialog, breaker: true, $"x{shopItem5.Count} {Item.Name(shopItem5.Index)}", 50, shopItem5.Index);
											}
											Dialog.Button(dialog, breaker: false, "Okay", "Okay");
										});
										PlayerCore.UpdateInventory(this);
									}
								}
								else if (shopCategory.Currency == CurrencyType.MoonRocks)
								{
									items = Shop.Purchase(listing);
									if (!Shop.ListingAvailable(listing))
									{
										PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
										{
											Dialog.ItemText(dialog, breaker: true, "~1Item unavailable", 75, 3);
											Dialog.Text(dialog, breaker: true, "You're late! This item is not available anymore.", 50);
											Dialog.Button(dialog, breaker: false, "Okay", "Okay");
										});
									}
									else if (!PlayerSpecialCurrency.Has(this, listing.Price))
									{
										PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
										{
											Dialog.ItemText(dialog, breaker: true, "~1Not enough moon rocks", 75, 3);
											Dialog.Text(dialog, breaker: true, "Sorry, you don't have enough", 50);
											Dialog.Text(dialog, breaker: true, "moon rocks to purchase this item.", 50);
											Dialog.Button(dialog, breaker: false, "Okay", "Okay");
										});
									}
									else if (!Profile.CanGetListing(listing))
									{
										PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
										{
											Dialog.ItemText(dialog, breaker: true, "~1Not enough space", 75, 3);
											Dialog.Text(dialog, breaker: true, "Sorry, you don't have enough space in", 50);
											Dialog.Text(dialog, breaker: true, "your inventory to purchase this item.", 50);
											Dialog.Button(dialog, breaker: false, "Okay", "Okay");
										});
									}
									else
									{
										PlayerSpecialCurrency.Remove(this, listing.Price);
										Achievement(AchievementType.SpendRocks, increase: true, listing.Price);
										ShopItem[] array15 = items;
										for (int num84 = 0; num84 < array15.Length; num84++)
										{
											ShopItem shopItem3 = array15[num84];
											Profile.SetItem(shopItem3.Index, Profile.GetItem(shopItem3.Index) + shopItem3.Count);
											PlayerConsole.Message(this, "You received ~1x{0} {1}~0!", shopItem3.Count, Item.Name(shopItem3.Index));
										}
										PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
										{
											Dialog.ItemText(dialog, breaker: true, "~1Purchase complete", 75, 3);
											Dialog.Text(dialog, breaker: true, $"You spent ~1{listing.Price} ~0moon rocks.", 50);
											Dialog.Text(dialog, breaker: true, "You have got the following items", 50);
											Dialog.Space(dialog);
											ShopItem[] array68 = items;
											for (int num185 = 0; num185 < array68.Length; num185++)
											{
												ShopItem shopItem4 = array68[num185];
												Dialog.ItemText(dialog, breaker: true, $"x{shopItem4.Count} {Item.Name(shopItem4.Index)}", 50, shopItem4.Index);
											}
											Dialog.Button(dialog, breaker: false, "Okay", "Okay");
										});
										PlayerCore.UpdateInventory(this);
									}
								}
							}
							if (Profile.Active && text35 == "Shop.Upgrade.Slots" && text36 == "Accept")
							{
								int gems2 = (Profile.Data.ItemSlots - 20) * 10;
								if (PlayerGems.Has(this, gems2))
								{
									PlayerGems.Remove(this, gems2);
									Profile.Data.ItemSlots += 10;
									Database.ProfileSave(Profile.Data);
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(6));
									Profile.WriteItems(binaryWriter);
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
									input = new MemoryStream();
									binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Message");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Purchase complete", 75, 3);
									Dialog.Text(binaryWriter, breaker: true, "Your inventory size has been increased.", 50);
									Dialog.Text(binaryWriter, breaker: true, $"You currently have ~1{Profile.Data.ItemSlots} ~0item slots.", 50);
									Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								else
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Message");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Inventory upgrade", 75, 3);
									Dialog.Text(binaryWriter, breaker: true, "Not enough gems.", 50);
									Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
							}
							if (Profile.Active && text35 == "Report.Session" && text36 == "Submit")
							{
								string text42 = dictionary["Reason"];
								string message = string.Format("Reported world {0} with reason ``{1}``, {2} priority.", Session.Data.Filename, text42, (dictionary2["Priority"] == 0) ? "low" : "high");
								if (text42.Length > 0)
								{
									Server.SendReport(Profile.Data.Filename, message);
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, worker, "Message");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Report submitted", 75, 3);
									Dialog.Text(binaryWriter, breaker: true, "Your report has been submitted and will be reviewed", 50);
									Dialog.Text(binaryWriter, breaker: true, "by moderators as soon as possible.", 50);
									Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								else
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, worker, "Message");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Reason required", 75, 3);
									Dialog.Text(binaryWriter, breaker: true, "Please fill in the reason field, a reason is required", 50);
									Dialog.Text(binaryWriter, breaker: true, "to be provided for us to investigate your issue.", 50);
									Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
							}
							if (Profile.Active && text35 == "Report.Profile" && text36 == "Submit")
							{
								Player interact = GetInteract(WrenchID);
								if (interact == null)
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(4));
									string text43 = "The player has left.";
									binaryWriter.Write(Encoding.UTF8.GetBytes(text43 + "\0"));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								else
								{
									string text44 = dictionary["Reason"];
									string message2 = string.Format("Reported player {0} with reason ``{1}``, {2} priority.", interact.Profile.Data.Filename, text44, (dictionary2["Priority"] == 0) ? "low" : "high");
									if (text44.Length > 0)
									{
										Server.SendReport(Profile.Data.Filename, message2);
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, worker, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Report submitted", 75, 3);
										Dialog.Text(binaryWriter, breaker: true, "Your report has been submitted and will be reviewed", 50);
										Dialog.Text(binaryWriter, breaker: true, "by moderators as soon as possible.", 50);
										Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, worker, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Reason required", 75, 3);
										Dialog.Text(binaryWriter, breaker: true, "Please fill in the reason field, a reason is required", 50);
										Dialog.Text(binaryWriter, breaker: true, "to be provided for us to investigate your issue.", 50);
										Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
							}
							if (Profile.Active && text35 == "Event.Finish" && text36 == "Okay")
							{
								Warp(Session.Random(this), unban: false);
							}
							if (Profile.Active && text35 == "Event.Halloween.Enemy")
							{
								if (Session.Data.Foreground[worker] != 1143)
								{
									PlayerConsole.Message(this, "The item you tried to use has disappeared.");
								}
								else if (text36 == "Accept")
								{
									HalloweenEnemyData halloweenEnemyData = Session.GetHalloweenEnemyData(worker);
									if (Profile.HasItem(1145, halloweenEnemyData.Candies))
									{
										Profile.SetItem(1145, Profile.GetItem(1145) - halloweenEnemyData.Candies);
										PlayerCore.UpdateInventory(this);
										Enter(worker, null);
									}
									else
									{
										PlayerCore.UpdateDialog(this, 0, "Event.Halloween.Enemy.Liar", delegate(BinaryWriter dialog)
										{
											Dialog.ItemText(dialog, breaker: true, "~1No candy?", 75, 3);
											Dialog.Text(dialog, breaker: true, "You told me you'll give me a candy, but", 50);
											Dialog.Text(dialog, breaker: true, "you didn't have one. I'll kick you out.", 50);
											Dialog.Button(dialog, breaker: false, "Continue", "Continue");
										});
									}
								}
								else if (text36 == "Refuse")
								{
									PlayerLayout.Warning(this, 200, 3, "~1YOU ~3FAILED~1!", "You didn't help someone who could've helped you.", "Warping out.");
									Warp(Session.Random(this), unban: false);
								}
							}
							if (Profile.Active && text35 == "Event.Halloween.Enemy.Liar")
							{
								PlayerLayout.Warning(this, 200, 3, "~1YOU ~3FAILED~1!", "You lied to someone who could've helped you.", "You've been kicked out.");
								Warp(Session.Random(this), unban: false);
							}
							if (Profile.Active && text35 == "Session")
							{
								if (text36 == "Worlds")
								{
									PlayerCore.UpdateDialog(this, 0, "Session.Worlds", delegate(BinaryWriter dialog)
									{
										Dialog.ItemText(dialog, breaker: true, "~1World menu", 75, 3);
										Dialog.Text(dialog, breaker: true, "Where would you like to go?", 50);
										Dialog.Textbox(dialog, breaker: true, "World", "", 32);
										Dialog.Text(dialog, breaker: true, "World name must be 1-32 characters long, only A-Z and 0-9 characters are allowed.", 25);
										Dialog.Button(dialog, breaker: true, "List", "World suggestions & leaderboard");
										Dialog.Button(dialog, breaker: false, "Warp", "Warp");
										Dialog.Button(dialog, breaker: false, "Cancel", "Cancel");
									});
								}
								if (text36 == "Respawn")
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(29));
									binaryWriter.Write(Convert.ToInt32(0));
									binaryWriter.Write(Convert.ToUInt16(2));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
									input = new MemoryStream();
									binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(29));
									binaryWriter.Write(Convert.ToInt32(Identifier));
									binaryWriter.Write(Convert.ToUInt16(2));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Server.Broadcast(input.ToArray(), delegate(Player player)
									{
										if (player == this)
										{
											return false;
										}
										if (!player.Active)
										{
											return false;
										}
										if (!player.Profile.Active)
										{
											return false;
										}
										if (!player.Session.Active)
										{
											return false;
										}
										return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
									});
									binaryWriter.Close();
								}
								if (text36 == "Options")
								{
									PlayerCore.UpdateDialog(this, 0, "Options", delegate(BinaryWriter dialog)
									{
										Dialog.ItemText(dialog, breaker: true, "~1Game options", 75, 3);
										Dialog.Button(dialog, breaker: true, "General", "General options");
										Dialog.Button(dialog, breaker: true, "Account", "Account options");
										Dialog.Button(dialog, breaker: true, "Friend", "Friend options");
										Dialog.Button(dialog, breaker: false, "Cancel", "Cancel");
									});
								}
								if (text36 == "Shop")
								{
									ShopPage = Shop.DefaultCategory;
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(33));
									Shop.WriteCategories(binaryWriter, Shop.DefaultCategory);
									Shop.WriteListings(binaryWriter, Shop.DefaultCategory);
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								if (text36 == "Report")
								{
									PlayerCore.UpdateDialog(this, 0, "Report.Session", delegate(BinaryWriter dialog)
									{
										Dialog.ItemText(dialog, breaker: true, "~1Report world", 75, 3);
										Dialog.Text(dialog, breaker: true, "What's the reason?", 50);
										Dialog.Textbox(dialog, breaker: true, "Reason", "", 64);
										Dialog.Text(dialog, breaker: true, "Reason must be written in English.", 25);
										Dialog.Checkbox(dialog, breaker: true, value: false, "Priority", "High priority report", 50);
										Dialog.Button(dialog, breaker: false, "Submit", "Submit");
										Dialog.Button(dialog, breaker: false, "Cancel", "Cancel");
									});
								}
								if (text36 == "Moderator" && Rewards.Permission(Profile.Data.Filename, Permissions.Mod))
								{
									PlayerCore.UpdateDialog(this, 0, "Session.Moderator", delegate(BinaryWriter dialog)
									{
										Dialog.ItemText(dialog, breaker: true, "~1Moderator tools", 75, 3);
										if (Session.Data.Banned == 0)
										{
											Dialog.Button(dialog, breaker: true, "Ban", "Ban current world");
										}
										else
										{
											Dialog.Button(dialog, breaker: true, "Ban", "Unban current world");
										}
										if (Rewards.Permission(Profile.Data.Filename, Permissions.Admin))
										{
											Dialog.Button(dialog, breaker: true, "Access", "Manage access");
											Dialog.Button(dialog, breaker: true, "Owner", "Change owner");
											Dialog.Button(dialog, breaker: true, "Clone", "Clone world");
										}
										Dialog.Button(dialog, breaker: true, "Control", "Control options");
										Dialog.Button(dialog, breaker: false, "Cancel", "Cancel");
									});
								}
								if (text36 == "Server")
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Session.Server");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Change server", 75, 3);
									Dialog.Button(binaryWriter, breaker: true, "MAIN", "Connect to main server");
									Dialog.Button(binaryWriter, breaker: true, "CREATIVE", "Connect to creative server");
									Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
							}
							if (Profile.Active && text35 == "Session.Worlds")
							{
								if (text36 == "Warp")
								{
									Warp(dictionary["World"], unban: false);
								}
								if (text36 == "Random")
								{
									Warp(Session.Random(this), unban: false);
								}
								if (text36 == "Active")
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Session.Worlds.Suggestions");
									Dialog.ItemText(binaryWriter, breaker: true, "~1World suggestions", 75, 3);
									Dialog.Text(binaryWriter, breaker: true, "Showing random active worlds.", 50);
									if (Profile.Data.Level < 5)
									{
										Dialog.Button(binaryWriter, breaker: true, "TUTORIAL", "~rTutorial world");
									}
									else
									{
										Dialog.Button(binaryWriter, breaker: true, "BREAWORLDS", "~rRewards world");
									}
									List<string> list = new List<string>();
									list.AddRange(Database.SessionCache.Keys.ToArray());
									if (Session.Active)
									{
										list.Remove(Session.Data.Filename);
									}
									list.Remove("BREAWORLDS");
									list.Remove("TUTORIAL");
									for (int num85 = 0; num85 < Math.Min(10, list.Count); num85++)
									{
										string text45 = list[Server.Random.Next(list.Count)];
										string text46 = $"{text45} ({Session.Players(text45)})";
										list.Remove(text45);
										Dialog.Button(binaryWriter, breaker: true, text45, text46);
									}
									Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								if (text36 == "List")
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Session.Worlds.List");
									Dialog.ItemText(binaryWriter, breaker: true, "~1World suggestions", 75, 3);
									if (Profile.Data.Level < 5)
									{
										Dialog.Button(binaryWriter, breaker: true, "Main", "Tutorial world");
									}
									else
									{
										Dialog.Button(binaryWriter, breaker: true, "Main", "Rewards world");
									}
									Dialog.Button(binaryWriter, breaker: true, "Winner", "World of the day");
									Dialog.Button(binaryWriter, breaker: true, "Active", "Active worlds");
									Dialog.Button(binaryWriter, breaker: true, "Rating", "Leaderboard");
									Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
							}
							if (Profile.Active && text35 == "Session.Worlds.List")
							{
								if (text36 == "Main")
								{
									if (Profile.Data.Level < 5)
									{
										Warp("TUTORIAL", unban: false);
									}
									else
									{
										Warp("BREAWORLDS", unban: false);
									}
								}
								if (text36 == "Winner")
								{
									Warp(Ratings.Data.Winner, unban: false);
								}
								if (text36 == "Active")
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Session.Worlds.List.Active");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Random active worlds", 75, 3);
									List<string> list2 = new List<string>();
									list2.AddRange(Database.SessionCache.Keys.ToArray());
									if (Session.Active)
									{
										list2.Remove(Session.Data.Filename);
									}
									list2.Remove("BREAWORLDS");
									list2.Remove("TUTORIAL");
									if (list2.Count == 0)
									{
										Dialog.Text(binaryWriter, breaker: true, "No worlds yet, try again in a few minutes.", 50);
									}
									for (int num86 = 0; num86 < Math.Min(10, list2.Count); num86++)
									{
										string text47 = list2[Server.Random.Next(list2.Count)];
										string text48 = $"{text47} ({Session.Players(text47)})";
										list2.Remove(text47);
										Dialog.Button(binaryWriter, breaker: true, text47, text48);
									}
									Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								if (text36 == "Rating")
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Session.Worlds.List.Rating");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Top worlds today", 75, 3);
									if (Ratings.Leaderboard.Length == 0)
									{
										Dialog.Text(binaryWriter, breaker: true, "No worlds yet, try again in a few minutes.", 50);
									}
									for (int num87 = 0; num87 < Math.Min(Ratings.Leaderboard.Length, 10); num87++)
									{
										Dialog.Button(binaryWriter, breaker: true, Ratings.Leaderboard[num87], $"#{num87 + 1}. {Ratings.Leaderboard[num87]}");
									}
									Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
							}
							if (Profile.Active && text35 == "Session.Worlds.List.Active" && text36 != "Cancel")
							{
								Warp(text36, unban: false);
							}
							if (Profile.Active && text35 == "Session.Worlds.List.Rating" && text36 != "Cancel")
							{
								Warp(text36, unban: false);
							}
							if (Profile.Active && text35 == "Session.Moderator")
							{
								if (text36 == "Ban" && Rewards.Permission(Profile.Data.Filename, Permissions.Mod))
								{
									Database.SessionLoad(ref Session.Data, Session.Data.Name);
									if (Session.Data.Banned == 0)
									{
										Server.SendLog(Profile.Data.Filename, $"Banned world {Session.Data.Name}");
										Session.Data.Banned = 1;
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(35));
										binaryWriter.Write(Convert.ToUInt16(200));
										binaryWriter.Write(Convert.ToUInt16(1));
										binaryWriter.Write(Encoding.UTF8.GetBytes("~1WORLD HAS BEEN ~3BANNED~1!\0"));
										binaryWriter.Write(Encoding.UTF8.GetBytes("This world has been banned, you will be teleported to another one.\0"));
										binaryWriter.Write(Encoding.UTF8.GetBytes("\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Player[] array16 = Server.Online.ToArray();
										foreach (Player player13 in array16)
										{
											if (player13.Active && player13.Profile.Active && player13.Session.Active && !(player13.Session.Data.Name != Session.Data.Name) && !Rewards.Permission(player13.Profile.Data.Filename, Permissions.Mod))
											{
												player13.Warp(Session.Random(this), unban: false);
												player13.Send(input.ToArray());
											}
										}
										binaryWriter.Close();
									}
									else
									{
										Server.SendLog(Profile.Data.Filename, $"Unbanned world {Session.Data.Name}");
										Session.Data.Banned = 0;
									}
									Database.SessionSave(Session.Data);
								}
								if (text36 == "Access" && Rewards.Permission(Profile.Data.Filename, Permissions.Admin))
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Session.Moderator.Access");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Manage access", 75, 3);
									Dialog.Checkbox(binaryWriter, breaker: true, Session.ForceOwner, "Owner", "Enable owner access", 50);
									Dialog.Checkbox(binaryWriter, breaker: true, Session.ForceAdmin, "Admin", "Enable admin access", 50);
									Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
									Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								if (text36 == "Owner" && Rewards.Permission(Profile.Data.Filename, Permissions.Admin))
								{
									Database.SessionLoad(ref Session.Data, Session.Data.Name);
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Session.Moderator.Owner");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Change world owner", 75, 3);
									Dialog.Text(binaryWriter, breaker: true, "Who should own this world?", 50);
									Dialog.Textbox(binaryWriter, breaker: true, "Owner", Session.Data.Owner, 16);
									Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
									Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								if (text36 == "Clone" && Rewards.Permission(Profile.Data.Filename, Permissions.Admin))
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Session.Moderator.Clone");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Clone another world", 75, 3);
									Dialog.Text(binaryWriter, breaker: true, "Which world would you like to clone?", 50);
									Dialog.Textbox(binaryWriter, breaker: true, "World", string.Empty, 16);
									Dialog.Checkbox(binaryWriter, breaker: true, value: false, "Owner", "Clone owner", 50);
									Dialog.Checkbox(binaryWriter, breaker: true, value: false, "Admin", "Clone admins", 50);
									Dialog.Checkbox(binaryWriter, breaker: true, value: false, "Background", "Clone background", 50);
									Dialog.Checkbox(binaryWriter, breaker: true, value: false, "Foreground", "Clone foreground", 50);
									Dialog.Checkbox(binaryWriter, breaker: true, value: false, "Special", "Clone special data", 50);
									Dialog.Checkbox(binaryWriter, breaker: true, value: false, "Parent", "Clone parent data", 50);
									Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
									Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								if (text36 == "Control" && Rewards.Permission(Profile.Data.Filename, Permissions.Mod))
								{
									PlayerCore.UpdateDialog(this, 0, "Session.Moderator.Control", delegate(BinaryWriter dialog)
									{
										Dialog.ItemText(dialog, breaker: true, "~1Control options", 75, 3);
										Dialog.Checkbox(dialog, breaker: true, Profile.HiddenStaff, "HiddenStaff", "Hidden in staff list", 50);
										Dialog.Checkbox(dialog, breaker: true, Profile.Bannable, "Bannable", "Can be banned", 50);
										Dialog.Checkbox(dialog, breaker: true, Profile.Pullable, "Pullable", "Can be pulled", 50);
										Dialog.Checkbox(dialog, breaker: true, Profile.Killable, "Killable", "Can be killed", 50);
										Dialog.Button(dialog, breaker: false, "Accept", "Accept");
										Dialog.Button(dialog, breaker: false, "Cancel", "Cancel");
									});
								}
							}
							if (Profile.Active && text35 == "Session.Moderator.Access" && text36 == "Accept" && Rewards.Permission(Profile.Data.Filename, Permissions.Admin))
							{
								Session.ForceOwner = dictionary2["Owner"] == 1;
								Session.ForceAdmin = dictionary2["Admin"] == 1;
							}
							if (Profile.Active && text35 == "Session.Moderator.Owner" && text36 == "Accept" && Rewards.Permission(Profile.Data.Filename, Permissions.Admin))
							{
								Database.SessionLoad(ref Session.Data, Session.Data.Name);
								Session.Data.Owner = dictionary["Owner"];
								Database.SessionSave(Session.Data);
								Server.SendLog(Profile.Data.Filename, $"Changed owner of the world {Session.Data.Filename} to {Session.Data.Owner}");
							}
							if (Profile.Active && text35 == "Session.Moderator.Clone" && text36 == "Accept" && Rewards.Permission(Profile.Data.Filename, Permissions.Admin))
							{
								SessionDataHandle data4 = new SessionDataHandle(binded: false);
								Database.SessionLoad(ref data4, dictionary["World"]);
								Database.SessionLoad(ref Session.Data, Session.Data.Name);
								if (dictionary2["Owner"] == 1)
								{
									Session.Data.Owner = data4.Owner;
								}
								if (dictionary2["Admin"] == 1)
								{
									Session.Data.Admin = data4.Admin;
								}
								if (dictionary2["Background"] == 1)
								{
									Session.Data.Background = (ushort[])data4.Background.Clone();
								}
								if (dictionary2["Foreground"] == 1)
								{
									Session.Data.Foreground = (ushort[])data4.Foreground.Clone();
								}
								if (dictionary2["Special"] == 1)
								{
									Session.Data.Special = (object[])data4.Special.Clone();
								}
								if (dictionary2["Parent"] == 1)
								{
									Session.Data.Parent = (object[])data4.Parent.Clone();
								}
								Database.SessionClose(dictionary["World"], binded: false);
								Database.SessionSave(Session.Data);
								input = new MemoryStream();
								BinaryWriter binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(35));
								binaryWriter.Write(Convert.ToUInt16(200));
								binaryWriter.Write(Convert.ToUInt16(1));
								binaryWriter.Write(Encoding.UTF8.GetBytes("~1WORLD HAS BEEN ~3CLONED~1!\0"));
								binaryWriter.Write(Encoding.UTF8.GetBytes("The world that you are currently in has been cloned.\0"));
								binaryWriter.Write(Encoding.UTF8.GetBytes("\0"));
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								Server.Broadcast(input.ToArray(), delegate(Player player)
								{
									if (!player.Active)
									{
										return false;
									}
									if (!player.Profile.Active)
									{
										return false;
									}
									if (!player.Session.Active)
									{
										return false;
									}
									return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
								});
								binaryWriter.Close();
								Player[] array17 = Server.Online.ToArray();
								foreach (Player player14 in array17)
								{
									if (player14.Active && player14.Profile.Active && player14.Session.Active && !(player14.Session.Data.Name != Session.Data.Name))
									{
										input = new MemoryStream();
										binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(10));
										player14.Session.WriteTiles(binaryWriter, player14.Profile.Data.Filename);
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										player14.Send(input.ToArray());
										binaryWriter.Close();
									}
								}
								Server.SendLog(Profile.Data.Filename, string.Format("Cloned world {0} to {1}", dictionary["World"], Session.Data.Filename));
							}
							if (Profile.Active && text35 == "Session.Moderator.Control" && text36 == "Accept" && Rewards.Permission(Profile.Data.Filename, Permissions.Mod))
							{
								Profile.HiddenStaff = dictionary2["HiddenStaff"] == 1;
								Profile.Bannable = dictionary2["Bannable"] == 1;
								Profile.Pullable = dictionary2["Pullable"] == 1;
								Profile.Killable = dictionary2["Killable"] == 1;
							}
							if (Profile.Active && text35 == "Session.Server")
							{
								if (text36 == "MAIN")
								{
									PlayerCore.Reconnect(this, "148.251.92.249", 1800);
								}
								if (text36 == "CREATIVE")
								{
									PlayerCore.Reconnect(this, "148.251.92.249", 1801);
								}
							}
							if (Profile.Active && text35 == "Options")
							{
								if (text36 == "General")
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Options.General");
									Dialog.ItemText(binaryWriter, breaker: true, "~1General game options", 75, 3);
									Dialog.Text(binaryWriter, breaker: true, "Preferred sound volume?", 50);
									Dialog.Textbox(binaryWriter, breaker: true, "Volume", Profile.Data.Options.Volume.ToString(), 8);
									Dialog.Text(binaryWriter, breaker: true, "Available volume range is 1-100.", 25);
									Dialog.Space(binaryWriter);
									Dialog.Checkbox(binaryWriter, breaker: true, Profile.Data.Options.Sounds, "Sounds", "Enable sounds", 75);
									Dialog.Checkbox(binaryWriter, breaker: true, Profile.Data.Options.Shadow, "Shadow", "Enable shadows", 75);
									Dialog.Checkbox(binaryWriter, breaker: true, Profile.Data.Options.Smooth, "Smooth", "Smooth mode", 75);
									Dialog.Checkbox(binaryWriter, breaker: true, Profile.Data.Options.Full, "Full", "Fullscreen", 75);
									Dialog.Text(binaryWriter, breaker: true, "Once you disable shadows and smooth", 50);
									Dialog.Text(binaryWriter, breaker: true, "mode, you'll get better performance on", 50);
									Dialog.Text(binaryWriter, breaker: true, "older mobile devices. Your options are", 50);
									Dialog.Text(binaryWriter, breaker: true, "synchronized across all of your devices.", 50);
									Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
									Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								if (text36 == "Account")
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Options.Account");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Account options", 75, 3);
									if (Rewards.Permission(Profile.Data.Filename, Permissions.Admin))
									{
										Dialog.Button(binaryWriter, breaker: true, "Password", "Set password");
									}
									if (Profile.Data.StaffVerifiedDevice == "none" && Rewards.Permission(Profile.Data.Filename, Permissions.Guard))
									{
										Dialog.Button(binaryWriter, breaker: true, "Verify", "Change 2FA Device");
									}
									Dialog.Button(binaryWriter, breaker: true, "Token", "Change token");
									Dialog.Button(binaryWriter, breaker: true, "Email", "Change email");
									Dialog.Button(binaryWriter, breaker: true, "Gender", "Change gender");
									Dialog.Button(binaryWriter, breaker: true, "Skin", "Change skin");
									Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								if (text36 == "Friend")
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Options.Friend");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Friend options", 75, 3);
									Dialog.Checkbox(binaryWriter, breaker: true, Profile.Data.FriendUnknown, "Unknown", "Hide location", 75);
									Dialog.Checkbox(binaryWriter, breaker: true, Profile.Data.FriendOffline, "Offline", "Appear offline", 75);
									Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
									Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
							}
							if (Profile.Active && text35 == "Options.Friend" && text36 == "Accept")
							{
								Profile.Data.FriendUnknown = dictionary2["Unknown"] == 1;
								Profile.Data.FriendOffline = dictionary2["Offline"] == 1;
							}
							if (Profile.Active && text35 == "Options.Account")
							{
								if (text36 == "Verify" && Rewards.Permission(Profile.Data.Filename, Permissions.Guard))
								{
									Profile.Data.StaffVerifiedDevice = Profile.Data.Device;
									Server.SendTestLog(Profile.Data.Filename, $"Has just verified their device on their staff account.");
									PlayerLayout.Warning(this, 200, 2, "~12FA ~4ENABLED~1!", "2FA has been enabled for this device.");
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(14));
									binaryWriter.Write(Convert.ToInt32(0));
									Profile.WriteData(binaryWriter, this);
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
									Player[] array18 = Server.Online.ToArray();
									foreach (Player player15 in array18)
									{
										if (player15 != this && player15.Active && player15.Profile.Active && player15.Session.Active && !(player15.Session.Data.Name != Session.Data.Name))
										{
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(14));
											binaryWriter.Write(Convert.ToInt32(Identifier));
											Profile.WriteData(binaryWriter, player15);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											player15.Send(input.ToArray());
											binaryWriter.Close();
										}
									}
								}
								if (text36 == "Password")
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Options.Account.Password");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Set custom password", 75, 3);
									Dialog.Text(binaryWriter, breaker: true, "Setting custom password will increase your account's", 50);
									Dialog.Text(binaryWriter, breaker: true, "security. ~3Do not use same password anywhere else.", 50);
									Dialog.Textbox(binaryWriter, breaker: true, "Password", "", 32);
									Dialog.Checkbox(binaryWriter, breaker: true, value: false, "Confirm", "Confirm this action", 50);
									Dialog.Button(binaryWriter, breaker: false, "Change", "Change");
									Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								if (text36 == "Token")
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Options.Account.Token");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Change login token", 75, 3);
									Dialog.Text(binaryWriter, breaker: true, "Would you like to change your login token?", 50);
									Dialog.Text(binaryWriter, breaker: true, "A new login token will be generated for you", 50);
									Dialog.Text(binaryWriter, breaker: true, "and you will not be able to use the old one.", 50);
									Dialog.Checkbox(binaryWriter, breaker: true, value: false, "Confirm", "Confirm this action", 50);
									Dialog.Button(binaryWriter, breaker: false, "Change", "Change");
									Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								if (text36 == "Email")
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Options.Account.Email");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Change email address", 75, 3);
									Dialog.Text(binaryWriter, breaker: true, "What's your new email address?", 50);
									Dialog.Textbox(binaryWriter, breaker: true, "Email", "", 64);
									Dialog.Text(binaryWriter, breaker: true, "Make sure you enter a valid email address in case you lose your login token.", 25);
									Dialog.Checkbox(binaryWriter, breaker: true, value: false, "Confirm", "Confirm this action", 50);
									Dialog.Button(binaryWriter, breaker: false, "Change", "Change");
									Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								if (text36 == "Gender")
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Options.Account.Gender");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Change your gender", 73, 3);
									Dialog.Text(binaryWriter, breaker: true, "Pick your gender.", 50);
									Dialog.Button(binaryWriter, breaker: false, "Female", "Female");
									Dialog.Button(binaryWriter, breaker: false, "Male", "Male");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								if (text36 == "Skin")
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Options.Account.Skin");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Change skin tone", 73, 3);
									Dialog.Text(binaryWriter, breaker: true, "Which color do you want?", 50);
									RewardSkin[] array19 = Rewards.UnlockedSkins(Profile.Data.Level);
									for (int num91 = 0; num91 < array19.Length; num91++)
									{
										RewardSkin rewardSkin = array19[num91];
										if ((num91 + 1) % 4 == 0 || num91 + 1 == array19.Length)
										{
											Dialog.RGB(binaryWriter, breaker: true, num91.ToString(), 96, 64, rewardSkin.R, rewardSkin.G, rewardSkin.B);
										}
										else
										{
											Dialog.RGB(binaryWriter, breaker: false, num91.ToString(), 96, 64, rewardSkin.R, rewardSkin.G, rewardSkin.B);
										}
									}
									Dialog.Button(binaryWriter, breaker: true, "Cancel", "Cancel");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
							}
							if (Profile.Active && text35 == "Options.General" && text36 == "Accept")
							{
								int.TryParse(dictionary["Volume"], out var result3);
								if (result3 < 1)
								{
									result3 = 1;
								}
								if (result3 > 100)
								{
									result3 = 100;
								}
								Profile.Data.Options = new Options
								{
									Volume = result3,
									Sounds = (dictionary2["Sounds"] == 1),
									Shadow = (dictionary2["Shadow"] == 1),
									Smooth = (dictionary2["Smooth"] == 1),
									Full = (dictionary2["Full"] == 1)
								};
								Database.ProfileSave(Profile.Data);
								input = new MemoryStream();
								BinaryWriter binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(7));
								Profile.WriteOptions(binaryWriter);
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								Send(input.ToArray());
								binaryWriter.Close();
							}
							if (Profile.Active && text35 == "Options.Account.Password" && text36 == "Change" && dictionary2["Confirm"] == 1)
							{
								Profile.Data.Password = dictionary["Password"].ToUpper();
								Database.ProfileSave(Profile.Data);
								input = new MemoryStream();
								BinaryWriter binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(2));
								binaryWriter.Write(Convert.ToBoolean(1));
								string text49 = "Login credentials have been updated.";
								binaryWriter.Write(Encoding.UTF8.GetBytes(text49 + "\0"));
								binaryWriter.Write(Encoding.UTF8.GetBytes(Profile.Data.Filename + "\0"));
								binaryWriter.Write(Encoding.UTF8.GetBytes(Profile.Data.Password + "\0"));
								binaryWriter.Write(Convert.ToUInt16(Accounts));
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								Send(input.ToArray());
								binaryWriter.Close();
								Server.SendLog(Profile.Data.Filename, "Has changed their password");
							}
							if (Profile.Active && text35 == "Options.Account.Token" && text36 == "Change")
							{
								if (dictionary2["Confirm"] == 1)
								{
									Profile.Generate();
									Database.ProfileSave(Profile.Data);
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(2));
									binaryWriter.Write(Convert.ToBoolean(1));
									string text50 = "Login credentials have been updated.";
									binaryWriter.Write(Encoding.UTF8.GetBytes(text50 + "\0"));
									binaryWriter.Write(Encoding.UTF8.GetBytes(Profile.Data.Filename + "\0"));
									binaryWriter.Write(Encoding.UTF8.GetBytes(Profile.Data.Password + "\0"));
									binaryWriter.Write(Convert.ToUInt16(Accounts));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
									input = new MemoryStream();
									binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Message");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Change login token", 75, 3);
									Dialog.Text(binaryWriter, breaker: true, $"Your new login token is ~1{Profile.Data.Password}~0.", 50);
									Dialog.Text(binaryWriter, breaker: true, "Write it down in case you forget it.", 50);
									Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
									Server.SendLog(Profile.Data.Filename, "Has changed their token");
								}
								else
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Message");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Change login token", 75, 3);
									Dialog.Text(binaryWriter, breaker: true, "You must confirm this action.", 50);
									Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
							}
							if (Profile.Active && text35 == "Options.Account.Email" && text36 == "Change")
							{
								if (dictionary2["Confirm"] == 1)
								{
									if (Text.IsEmail(dictionary["Email"]))
									{
										Profile.Data.Mailname = dictionary["Email"];
										Database.ProfileSave(Profile.Data);
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, 0, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Change email address", 75, 3);
										Dialog.Text(binaryWriter, breaker: true, "Your email address has been changed", 50);
										Dialog.Text(binaryWriter, breaker: true, $"to ~1{Profile.Data.Mailname}~0.", 50);
										Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
										Server.SendLog(Profile.Data.Filename, "Has changed their email");
									}
									else
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, 0, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Change email address", 75, 3);
										Dialog.Text(binaryWriter, breaker: true, "Invalid email address entered.", 50);
										Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
								else
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Message");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Change email address", 75, 3);
									Dialog.Text(binaryWriter, breaker: true, "You must confirm this action.", 50);
									Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
							}
							if (Profile.Active && text35 == "Options.Account.Gender")
							{
								if (text36 == "Female")
								{
									Profile.Data.Gender = 1;
									PlayerConsole.Message(this, "Gender has been updated to Female.");
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(14));
									binaryWriter.Write(Convert.ToInt32(0));
									Profile.WriteData(binaryWriter, this);
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
									Player[] array20 = Server.Online.ToArray();
									foreach (Player player16 in array20)
									{
										if (player16 != this && player16.Active && player16.Profile.Active && player16.Session.Active && !(player16.Session.Data.Name != Session.Data.Name))
										{
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(14));
											binaryWriter.Write(Convert.ToInt32(Identifier));
											Profile.WriteData(binaryWriter, player16);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											player16.Send(input.ToArray());
											binaryWriter.Close();
										}
									}
								}
								else if (text36 == "Male")
								{
									Profile.Data.Gender = 0;
									PlayerConsole.Message(this, "Gender has been updated to Male.");
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(14));
									binaryWriter.Write(Convert.ToInt32(0));
									Profile.WriteData(binaryWriter, this);
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
									Player[] array21 = Server.Online.ToArray();
									foreach (Player player17 in array21)
									{
										if (player17 != this && player17.Active && player17.Profile.Active && player17.Session.Active && !(player17.Session.Data.Name != Session.Data.Name))
										{
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(14));
											binaryWriter.Write(Convert.ToInt32(Identifier));
											Profile.WriteData(binaryWriter, player17);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											player17.Send(input.ToArray());
											binaryWriter.Close();
										}
									}
								}
							}
							if (Profile.Active && text35 == "Options.Account.Skin" && text36 != "Cancel")
							{
								int.TryParse(text36, out var result4);
								RewardSkin[] array22 = Rewards.UnlockedSkins(Profile.Data.Level);
								Profile.Data.SkinA = array22[result4].A;
								Profile.Data.SkinR = array22[result4].R;
								Profile.Data.SkinG = array22[result4].G;
								Profile.Data.SkinB = array22[result4].B;
								input = new MemoryStream();
								BinaryWriter binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(14));
								binaryWriter.Write(Convert.ToInt32(0));
								Profile.WriteData(binaryWriter, this);
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								Send(input.ToArray());
								binaryWriter.Close();
								Player[] array23 = Server.Online.ToArray();
								foreach (Player player18 in array23)
								{
									if (player18 != this && player18.Active && player18.Profile.Active && player18.Session.Active && !(player18.Session.Data.Name != Session.Data.Name))
									{
										input = new MemoryStream();
										binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(14));
										binaryWriter.Write(Convert.ToInt32(Identifier));
										Profile.WriteData(binaryWriter, player18);
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										player18.Send(input.ToArray());
										binaryWriter.Close();
									}
								}
							}
							if (Profile.Active && text35 == "FriendList")
							{
								if (text36 == "ShowOffline")
								{
									Friendlist(offline: true);
								}
								else if (text36 == "HideOffline")
								{
									Friendlist(offline: false);
								}
								else
								{
									foreach (string friend in Profile.Data.Friends)
									{
										if (!(text36 == Profile.Data.Friends.IndexOf(friend).ToString()))
										{
											continue;
										}
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, Profile.Data.Friends.IndexOf(friend), "FriendList.Profile");
										Dialog.ItemText(binaryWriter, breaker: true, $"~1{friend}", 75, 3);
										Player player19 = null;
										Player[] array24 = Server.Online.ToArray();
										foreach (Player player20 in array24)
										{
											if (player20.Profile.Data.Filename == friend)
											{
												player19 = player20;
											}
										}
										if (player19 == null)
										{
											Dialog.Text(binaryWriter, breaker: true, "Friend is currently ~3offline~0.", 50);
											Dialog.Space(binaryWriter);
										}
										else if (player19.Profile.Data.FriendOffline)
										{
											Dialog.Text(binaryWriter, breaker: true, "Friend is currently ~3offline~0.", 50);
											Dialog.Space(binaryWriter);
										}
										else if (player19.Profile.Data.FriendUnknown)
										{
											Dialog.Text(binaryWriter, breaker: true, "Friend is ~4online~0, but has made their location hidden.", 50);
											Dialog.Space(binaryWriter);
										}
										else if (!player19.Profile.Visible)
										{
											Dialog.Text(binaryWriter, breaker: true, "Friend is ~4online~0, but their location is not available.", 50);
										}
										else if (player19.Session.Active)
										{
											Dialog.Text(binaryWriter, breaker: true, $"Friend is ~4online~0 and currently playing in", 50);
											Dialog.Text(binaryWriter, breaker: true, $"the world ~1{player19.Session.Data.Name}~0.", 50);
											Dialog.Space(binaryWriter);
											Dialog.Button(binaryWriter, breaker: true, "Warp", $"Warp to {player19.Session.Data.Name}");
										}
										else
										{
											Dialog.Text(binaryWriter, breaker: true, "Friend is ~4online~0, but hasn't entered a world yet.", 50);
											Dialog.Space(binaryWriter);
										}
										Dialog.Button(binaryWriter, breaker: true, "Remove", "Remove as friend");
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
							}
							if (Profile.Active && text35 == "FriendList.Profile")
							{
								if (text36 == "Remove")
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, worker, "FriendList.Profile.Remove");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Remove as friend", 75, 3);
									string arg3 = Profile.Data.Friends[worker].ToString();
									Dialog.Text(binaryWriter, breaker: true, $"Do you really want to remove ~1{arg3} ~0as friend?", 50);
									Dialog.Text(binaryWriter, breaker: true, $"You can always add them back later if you want.", 50);
									Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
									Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								if (text36 == "Warp")
								{
									string text51 = Profile.Data.Friends[worker].ToString();
									Player player21 = null;
									Player[] array25 = Server.Online.ToArray();
									foreach (Player player22 in array25)
									{
										if (player22.Profile.Data.Filename == text51)
										{
											player21 = player22;
										}
									}
									if (player21 == null)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										string text52 = "Couldn't warp to friend, friend is not online anymore.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text52 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (player21.Profile.Data.FriendOffline)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										string text53 = "Couldn't warp to friend, friend is not online anymore.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text53 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (player21.Profile.Data.FriendUnknown)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										string text54 = "Couldn't warp to friend, their location is not public anymore.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text54 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (player21.Session.Active)
									{
										Warp(player21.Session.Data.Name, unban: false);
									}
									else
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										string text55 = "Couldn't warp to friend, friend is not in a world yet.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text55 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
							}
							if (Profile.Active && text35 == "FriendList.Profile.Remove" && text36 == "Accept")
							{
								string text56 = Profile.Data.Friends[worker].ToString();
								if (Database.ProfileExists(text56))
								{
									ProfileDataHandle data5 = new ProfileDataHandle(binded: false);
									Database.ProfileLoad(ref data5, text56);
									data5.Friends.Remove(Profile.Data.Filename);
									Database.ProfileSave(data5);
									if (!Profile.Online(text56))
									{
										Database.ProfileClose(text56, binded: false);
									}
								}
								Profile.Data.Friends.Remove(text56);
							}
							if (Profile.Active && text35 == "Wrench.Self")
							{
								if (text36 == "Worlds")
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Message");
									Dialog.ItemText(binaryWriter, breaker: true, "~1My worlds", 75, 3);
									if (Profile.Data.Worlds.Count == 0)
									{
										Dialog.Text(binaryWriter, breaker: true, "You do not own any worlds.", 50);
									}
									else
									{
										Profile.Data.Worlds.Sort();
										foreach (string world in Profile.Data.Worlds)
										{
											Dialog.ItemText(binaryWriter, breaker: true, world, 50, 273);
										}
									}
									Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								if (text36 == "Challenge")
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									if (Challenge.CurrentDay() == 1)
									{
										Window = Dialog.Create(binaryWriter, 0, "Challenge");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Weekly challenge", 75, 3);
										if (!Challenge.Active)
										{
											Dialog.Text(binaryWriter, breaker: true, "Sorry, weekly challenge is unavailable.", 50);
											Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
										}
										else if (Challenge.IsParticipant(Profile.Data.Filename))
										{
											Dialog.Text(binaryWriter, breaker: true, "Today is the registration day, you've already", 50);
											Dialog.Text(binaryWriter, breaker: true, "registered for the weekly challenge, tomorrow the", 50);
											Dialog.Text(binaryWriter, breaker: true, "weekly challenge begins, you'll have to complete", 50);
											Dialog.Text(binaryWriter, breaker: true, "the following steps.", 50);
											Dialog.Space(binaryWriter);
											Dialog.ItemText(binaryWriter, breaker: true, $"~1{Challenge.TaskName(Challenge.Data.DailyTask2)}", 50, 1);
											Dialog.ItemText(binaryWriter, breaker: true, $"~1{Challenge.TaskName(Challenge.Data.DailyTask3)}", 50, 1);
											Dialog.ItemText(binaryWriter, breaker: true, $"~1{Challenge.TaskName(Challenge.Data.DailyTask4)}", 50, 1);
											Dialog.ItemText(binaryWriter, breaker: true, $"~1{Challenge.TaskName(Challenge.Data.DailyTask5)}", 50, 1);
											Dialog.ItemText(binaryWriter, breaker: true, $"~1{Challenge.TaskName(Challenge.Data.DailyTask6)}", 50, 1);
											Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
										}
										else
										{
											Dialog.Text(binaryWriter, breaker: true, $"Today is the registration day, you can register", 50);
											Dialog.Text(binaryWriter, breaker: true, $"for the upcoming weekly challenge for ~1{Challenge.Price} ~0gems", 50);
											Dialog.Text(binaryWriter, breaker: true, $"you'll get daily steps to complete, top 3 players", 50);
											Dialog.Text(binaryWriter, breaker: true, $"in leaderboard will win the challenge.", 50);
											Dialog.Button(binaryWriter, breaker: true, "Register", $"Register for {Challenge.Price} gems");
											Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										}
									}
									else if (Challenge.CurrentDay() == 7)
									{
										Window = Dialog.Create(binaryWriter, 0, "Challenge");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Weekly challenge", 75, 3);
										if (Challenge.IsParticipant(Profile.Data.Filename))
										{
											Dialog.Text(binaryWriter, breaker: true, "The weekly challenge is finally over, today is the", 50);
											Dialog.Text(binaryWriter, breaker: true, "final day of current weekly challenge.", 50);
											Dialog.Space(binaryWriter);
											ChallengeParticipant participant2 = Challenge.FindParticipant(Profile.Data.Filename);
											int count2 = Challenge.Data.Participants.Count;
											int num97 = Challenge.ParticipantPlace(Profile.Data.Filename);
											int num98 = Challenge.ParticipantPoints(participant2);
											Dialog.Text(binaryWriter, breaker: true, $"You are in the ~1{Text.Ordinal(num97)} ~0place between ~1{count2} ~0participants", 50);
											Dialog.Text(binaryWriter, breaker: true, $"with total ~1{num98} ~0weekly points.", 50);
											if (num97 >= 1 && num97 <= 3)
											{
												if (participant2.Rewarded)
												{
													Dialog.Text(binaryWriter, breaker: true, "You've already claimed your reward.", 50);
												}
												else
												{
													Dialog.Button(binaryWriter, breaker: true, "Reward", "Claim my reward");
												}
											}
											else
											{
												Dialog.Text(binaryWriter, breaker: true, "No rewards are available for you.", 50);
											}
											Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
										}
										else
										{
											Dialog.Text(binaryWriter, breaker: true, "Today is the final day of current weekly", 50);
											Dialog.Text(binaryWriter, breaker: true, "challenge which you haven't participated", 50);
											Dialog.Text(binaryWriter, breaker: true, "in, tomorrow you'll be able to register for", 50);
											Dialog.Text(binaryWriter, breaker: true, "another weekly challenge.", 50);
											Dialog.Button(binaryWriter, breaker: true, "Leaderboard", "View final leaderboard");
											Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
										}
									}
									else
									{
										Window = Dialog.Create(binaryWriter, 0, "Challenge");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Weekly challenge", 75, 3);
										Dialog.Text(binaryWriter, breaker: true, $"Today is the ~1{Challenge.CurrentTaskName()} ~0day.", 50);
										if (Challenge.IsParticipant(Profile.Data.Filename))
										{
											ChallengeParticipant participant3 = Challenge.FindParticipant(Profile.Data.Filename);
											int num99 = Challenge.ParticipantPoints(participant3);
											int num100 = Challenge.ParticipantPoints(participant3, Challenge.CurrentDay());
											int number = Challenge.ParticipantPlace(Profile.Data.Filename);
											Dialog.Text(binaryWriter, breaker: true, $"You've collected ~1{num100} ~0points today and ~1{num99}", 50);
											Dialog.Text(binaryWriter, breaker: true, $"points this week. You are in ~1{Text.Ordinal(number)} ~0place.", 50);
										}
										else
										{
											Dialog.Text(binaryWriter, breaker: true, "You haven't registered at this week,", 50);
											Dialog.Text(binaryWriter, breaker: true, "come back next week.", 50);
										}
										Dialog.Button(binaryWriter, breaker: true, "Leaderboard", "View leaderboard");
										Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
									}
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								if (text36 == "Achievements")
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Message");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Achievements", 75, 3);
									for (int num101 = 0; num101 < Achievements.List.Count; num101++)
									{
										int num102 = Profile.Data.Achievements[num101];
										Achievement achievement = Achievements.List[num101];
										if (achievement.Amount <= num102)
										{
											string info = "~4Complete";
											Dialog.Achievement(binaryWriter, breaker: true, 2, achievement.Name, achievement.Text, info);
										}
										else
										{
											int num103 = (int)((double)num102 / (double)achievement.Amount * 100.0);
											string info2 = $"{num103}%";
											Dialog.Achievement(binaryWriter, breaker: true, 3, achievement.Name, achievement.Text, info2);
										}
									}
									Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								if (text36 == "ItemQuest")
								{
									PlayerQuests.ShowItemQuest(this);
								}
							}
							if (Profile.Active && text35 == "Wrench.Other")
							{
								if (text36 == "AddFriend")
								{
									Player interact2 = GetInteract(WrenchID);
									if (interact2 == null)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										string text57 = "The player has left.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text57 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (Profile.Data.Friends.Count >= 100)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										string text58 = "Couldn't send friend request, you already have 100 friends.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text58 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (interact2.Profile.Data.Friends.Count >= 100)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										string text59 = $"Couldn't send friend request, ~1{interact2.Profile.Data.Username}~0 already has 100 friends.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text59 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (interact2.FriendID == Identifier)
									{
										if (!Profile.Data.Friends.Contains(interact2.Profile.Data.Filename))
										{
											Profile.Data.Friends.Add(interact2.Profile.Data.Filename);
											Database.ProfileSave(Profile.Data);
										}
										if (!interact2.Profile.Data.Friends.Contains(Profile.Data.Filename))
										{
											interact2.Profile.Data.Friends.Add(Profile.Data.Filename);
											Database.ProfileSave(interact2.Profile.Data);
										}
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										string text60 = $"You are now friends with ~1{Profile.Data.Username}~0.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text60 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										interact2.Send(input.ToArray());
										binaryWriter.Close();
										input = new MemoryStream();
										binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										text60 = $"You are now friends with ~1{interact2.Profile.Data.Username}~0.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text60 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else
									{
										FriendID = SetInteract(interact2);
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										string text61 = $"~1{Profile.Data.Username} ~0has sent you a friend request.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text61 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										interact2.Send(input.ToArray());
										binaryWriter.Close();
										input = new MemoryStream();
										binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										text61 = $"Friend request sent to ~1{interact2.Profile.Data.Username}~0.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text61 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
								if (text36 == "RemoveFriend")
								{
									Player interact3 = GetInteract(WrenchID);
									if (interact3 == null)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										string text62 = "The player has left.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text62 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (Profile.Data.Friends.Contains(interact3.Profile.Data.Filename))
									{
										int num104 = Profile.Data.Friends.IndexOf(interact3.Profile.Data.Filename);
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num104, "FriendList.Profile.Remove");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Remove as friend", 75, 3);
										string arg4 = Profile.Data.Friends[num104].ToString();
										Dialog.Text(binaryWriter, breaker: true, $"Do you really want to remove ~1{arg4} ~0as friend?", 50);
										Dialog.Text(binaryWriter, breaker: true, $"You can always add them back later if you want.", 50);
										Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
								if (text36 == "Trade")
								{
									Player interact4 = GetInteract(WrenchID);
									if (interact4 == null)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										string text63 = "The player has left.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text63 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (WrenchID != 0)
									{
										TradeStart(WrenchID);
									}
								}
								if (text36 == "Report")
								{
									Player interact5 = GetInteract(WrenchID);
									if (interact5 == null)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										string text64 = "The player has left.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text64 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, 0, "Report.Profile");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Report " + interact5.Profile.Data.Username, 75, 3);
										Dialog.Text(binaryWriter, breaker: true, "What's the reason?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Reason", "", 64);
										Dialog.Text(binaryWriter, breaker: true, "Reason must be written in English.", 25);
										Dialog.Checkbox(binaryWriter, breaker: true, value: false, "Priority", "High priority report", 50);
										Dialog.Button(binaryWriter, breaker: false, "Submit", "Submit");
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
								if (text36 == "WorldBan")
								{
									Player interact6 = GetInteract(WrenchID);
									if (interact6 != null)
									{
										PlayerControl.Ban(this, interact6);
									}
								}
								if (text36 == "Pull")
								{
									Player interact7 = GetInteract(WrenchID);
									if (interact7 != null)
									{
										PlayerControl.Pull(this, interact7);
									}
								}
								if (text36 == "Kill")
								{
									Player interact8 = GetInteract(WrenchID);
									if (interact8 != null)
									{
										PlayerControl.Kill(this, interact8);
									}
								}
								if (text36 == "Freeze")
								{
									Player interact9 = GetInteract(WrenchID);
									if (interact9 != null)
									{
										PlayerControl.Freeze(this, interact9);
									}
								}
								if (text36 == "Punish" && Rewards.Permission(Profile.Data.Filename, Permissions.Mod))
								{
									Player interact10 = GetInteract(WrenchID);
									if (interact10 == null)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										string text65 = "The player has left.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text65 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, 0, "Wrench.Other.Punish");
										Dialog.ItemText(binaryWriter, breaker: true, $"~1Punish {interact10.Profile.Data.Filename}", 75, 3);
										Dialog.Button(binaryWriter, breaker: true, "Ban", "Ban player");
										Dialog.Button(binaryWriter, breaker: true, "Mute", "Mute player");
										Dialog.Button(binaryWriter, breaker: true, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
								if (text36 == "Lookup" && Rewards.Permission(Profile.Data.Filename, Permissions.Mod))
								{
									Player interact11 = GetInteract(WrenchID);
									if (interact11 == null)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										string text66 = "The player has left.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text66 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, 0, "Wrench.Other.Lookup");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Lookup player", 75, 3);
										Dialog.Text(binaryWriter, breaker: true, "Raw name: ~1" + interact11.Profile.Data.Filename, 50);
										Dialog.Text(binaryWriter, breaker: true, "Username: ~1" + interact11.Profile.Data.Username, 50);
										if (Rewards.Permission(Profile.Data.Filename, Permissions.Dev))
										{
											if (Rewards.Permission(interact11.Profile.Data.Filename, Permissions.Dev))
											{
												Dialog.Text(binaryWriter, breaker: true, "Permissions: ~1Dev", 50);
											}
											else if (Rewards.Permission(interact11.Profile.Data.Filename, Permissions.Admin))
											{
												Dialog.Text(binaryWriter, breaker: true, "Permissions: ~1Admin", 50);
											}
											else if (Rewards.Permission(interact11.Profile.Data.Filename, Permissions.Mod))
											{
												Dialog.Text(binaryWriter, breaker: true, "Permissions: ~1Mod", 50);
											}
											else if (Rewards.Permission(interact11.Profile.Data.Filename, Permissions.Guard))
											{
												Dialog.Text(binaryWriter, breaker: true, "Permissions: ~1Guard", 50);
											}
											else
											{
												Dialog.Text(binaryWriter, breaker: true, "Permissions: ~1None", 50);
											}
											Dialog.Text(binaryWriter, breaker: true, "Identifier: ~1" + interact11.Identifier, 50);
											Dialog.Text(binaryWriter, breaker: true, "Hash: ~1" + interact11.Profile.Device, 50);
										}
										Dialog.Space(binaryWriter);
										Dialog.Button(binaryWriter, breaker: true, "Items", "View item list");
										Dialog.Button(binaryWriter, breaker: true, "Worlds", "View world list");
										Dialog.Space(binaryWriter);
										Dialog.Text(binaryWriter, breaker: true, "OS name: ~1" + Platform.Name(interact11.Profile.PlatformType), 50);
										Dialog.Text(binaryWriter, breaker: true, "OS version: ~1" + Platform.Version(interact11.Profile.PlatformType, interact11.Profile.PlatformVersion), 50);
										Dialog.Text(binaryWriter, breaker: true, "Login date: ~1" + interact11.Profile.Data.LoginDate.ToString(), 50);
										Dialog.Text(binaryWriter, breaker: true, "Register date: ~1" + interact11.Profile.Data.RegisterDate.ToString(), 50);
										Dialog.Text(binaryWriter, breaker: true, "Total online: ~1" + Text.Time(interact11.Profile.GetTotalOnline()), 50);
										Dialog.Text(binaryWriter, breaker: true, "Account warnings: ~1" + interact11.Profile.Data.Warnings, 50);
										Dialog.Text(binaryWriter, breaker: true, "Country: ~1" + interact11.Profile.Country, 50);
										Dialog.Text(binaryWriter, breaker: true, "Version: ~1" + interact11.Profile.Version, 50);
										Dialog.Text(binaryWriter, breaker: true, "Level: ~1" + interact11.Profile.Data.Level, 50);
										Dialog.Text(binaryWriter, breaker: true, "Gems: ~1" + interact11.Profile.Data.Gems, 50);
										if (Event.Type == EventType.Space && DateTime.UtcNow > Event.AvailableFrom && DateTime.UtcNow < Event.AvailableTo)
										{
											Dialog.Text(binaryWriter, breaker: true, "Moon Rocks: ~1" + interact11.Profile.Data.MoonRocks, 50);
										}
										Dialog.Text(binaryWriter, breaker: true, "XP: ~1" + interact11.Profile.Data.Experience, 50);
										Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
							}
							if (Profile.Active && text35 == "Wrench.Other.Punish")
							{
								if (text36 == "Ban" && Rewards.Permission(Profile.Data.Filename, Permissions.Mod))
								{
									Player interact12 = GetInteract(WrenchID);
									if (interact12 == null)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										string text67 = "The player has left.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text67 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, 0, "Wrench.Other.Punish.Ban");
										Dialog.ItemText(binaryWriter, breaker: true, $"~1Ban {interact12.Profile.Data.Filename}", 75, 3);
										Dialog.Text(binaryWriter, breaker: true, "How many days?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Days", string.Empty, 16);
										Dialog.Text(binaryWriter, breaker: true, "How many hours?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Hours", string.Empty, 16);
										Dialog.Text(binaryWriter, breaker: true, "How many minutes?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Minutes", string.Empty, 16);
										Dialog.Text(binaryWriter, breaker: true, "How many seconds?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Seconds", string.Empty, 16);
										Dialog.Text(binaryWriter, breaker: true, "What's the reason?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Reason", string.Empty, 16);
										Dialog.Space(binaryWriter);
										Dialog.Checkbox(binaryWriter, breaker: true, value: false, "Address", "Apply on location too", 50);
										Dialog.Checkbox(binaryWriter, breaker: true, value: false, "Confirm", "I confirm this action", 50);
										Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
								if (text36 == "Mute" && Rewards.Permission(Profile.Data.Filename, Permissions.Mod))
								{
									Player interact13 = GetInteract(WrenchID);
									if (interact13 == null)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										string text68 = "The player has left.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text68 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, 0, "Wrench.Other.Punish.Mute");
										Dialog.ItemText(binaryWriter, breaker: true, $"~1Mute {interact13.Profile.Data.Filename}", 75, 3);
										Dialog.Text(binaryWriter, breaker: true, "How many days?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Days", string.Empty, 16);
										Dialog.Text(binaryWriter, breaker: true, "How many hours?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Hours", string.Empty, 16);
										Dialog.Text(binaryWriter, breaker: true, "How many minutes?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Minutes", string.Empty, 16);
										Dialog.Text(binaryWriter, breaker: true, "How many seconds?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Seconds", string.Empty, 16);
										Dialog.Text(binaryWriter, breaker: true, "What's the reason?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Reason", string.Empty, 16);
										Dialog.Space(binaryWriter);
										Dialog.Checkbox(binaryWriter, breaker: true, value: false, "Confirm", "I confirm this action", 50);
										Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
							}
							if (Profile.Active && text35 == "Wrench.Other.Punish.Ban" && text36 == "Accept" && Rewards.Permission(Profile.Data.Filename, Permissions.Mod))
							{
								Player interact14 = GetInteract(WrenchID);
								if (interact14 == null)
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(4));
									string text69 = "The player has left.";
									binaryWriter.Write(Encoding.UTF8.GetBytes(text69 + "\0"));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								else
								{
									string reason = dictionary["Reason"];
									bool location = dictionary2["Address"] == 1;
									bool flag6 = dictionary2["Confirm"] == 1;
									int.TryParse(dictionary["Days"], out var result5);
									int.TryParse(dictionary["Hours"], out var result6);
									int.TryParse(dictionary["Minutes"], out var result7);
									int.TryParse(dictionary["Seconds"], out var result8);
									result8 += result5 * 86400 + result6 * 3600 + result7 * 60;
									if (!flag6)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										string text70 = "You must confirm before submitting.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text70 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else
									{
										Punishment.Ban(this, interact14.Profile.Data.Filename, result8, reason, silent: false, warning: true, location);
									}
								}
							}
							if (Profile.Active && text35 == "Wrench.Other.Punish.Mute" && text36 == "Accept" && Rewards.Permission(Profile.Data.Filename, Permissions.Mod))
							{
								Player interact15 = GetInteract(WrenchID);
								if (interact15 == null)
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(4));
									string text71 = "The player has left.";
									binaryWriter.Write(Encoding.UTF8.GetBytes(text71 + "\0"));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								else
								{
									string reason2 = dictionary["Reason"];
									bool flag7 = dictionary2["Confirm"] == 1;
									int.TryParse(dictionary["Days"], out var result9);
									int.TryParse(dictionary["Hours"], out var result10);
									int.TryParse(dictionary["Minutes"], out var result11);
									int.TryParse(dictionary["Seconds"], out var result12);
									result12 += result9 * 86400 + result10 * 3600 + result11 * 60;
									if (!flag7)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										string text72 = "You must confirm before submitting.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text72 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else
									{
										Punishment.Mute(this, interact15.Profile.Data.Filename, result12, reason2, silent: false, warning: true);
									}
								}
							}
							if (Profile.Active && text35 == "Wrench.Other.Lookup")
							{
								if (text36 == "Items" && Rewards.Permission(Profile.Data.Filename, Permissions.Mod))
								{
									Player interact16 = GetInteract(WrenchID);
									if (interact16 == null)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										string text73 = "The player has left.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text73 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, 0, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Lookup player", 75, 3);
										Dialog.Text(binaryWriter, breaker: true, $"~1{interact16.Profile.Data.Filename}~0's inventory.", 50);
										for (int num105 = 0; num105 < interact16.Profile.Data.ItemCount.Count; num105++)
										{
											int item = interact16.Profile.Data.ItemIndex[num105];
											Dialog.ItemText(binaryWriter, breaker: true, "x" + (int)interact16.Profile.Data.ItemCount[num105] + " ~1" + Item.Name(item), 50, item);
										}
										Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
								if (text36 == "Worlds" && Rewards.Permission(Profile.Data.Filename, Permissions.Mod))
								{
									Player interact17 = GetInteract(WrenchID);
									if (interact17 == null)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										string text74 = "The player has left.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text74 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, 0, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Lookup player", 75, 3);
										Dialog.Text(binaryWriter, breaker: true, $"~1{interact17.Profile.Data.Filename}~0's owned worlds.", 50);
										if (interact17.Profile.Data.Worlds.Count == 0)
										{
											Dialog.Text(binaryWriter, breaker: true, "Player does not own any worlds.", 50);
										}
										else
										{
											interact17.Profile.Data.Worlds.Sort();
											foreach (string world2 in interact17.Profile.Data.Worlds)
											{
												Dialog.ItemText(binaryWriter, breaker: true, world2, 50, 273);
											}
										}
										Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
							}
							if (Profile.Active && text35 == "Trading.Item" && text36 == "Accept")
							{
								ushort index2 = Convert.ToUInt16(Profile.Data.ItemIndex[worker]);
								ushort num106 = Convert.ToUInt16(Profile.Data.ItemIndex[worker]);
								ushort.TryParse(dictionary["Count"], out var result13);
								TradeItem(index2, result13);
							}
							if (Profile.Active && text35 == "Trading.Review")
							{
								if (text36 == "Accept")
								{
									TradeReview();
								}
								if (text36 == "Cancel")
								{
									TradeCancel();
								}
							}
							if (Profile.Active && text35 == "Door.Password" && text36 == "Accept")
							{
								Enter(worker, dictionary["Password"]);
							}
							if (Profile.Active && text35 == "Wrench.Sign" && text36 == "Update")
							{
								if (Session.Data.Foreground[worker] != 69 && Session.Data.Foreground[worker] != 201 && Session.Data.Foreground[worker] != 509)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else if (Session.TileAccess(Profile.Data.Filename, worker))
								{
									Database.SessionLoad(ref Session.Data, Session.Data.Name);
									Session.Data.Special[worker] = new SignData
									{
										Text = Text.FilterSwear(dictionary["Text"])
									};
									Database.SessionSave(Session.Data);
								}
							}
							if (Profile.Active && text35 == "Wrench.Door" && text36 == "Update")
							{
								if (Session.Data.Foreground[worker] != 71 && Session.Data.Foreground[worker] != 203 && Session.Data.Foreground[worker] != 511)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else if (Session.TileAccess(Profile.Data.Filename, worker))
								{
									Database.SessionLoad(ref Session.Data, Session.Data.Name);
									Session.Data.Special[worker] = new DoorData
									{
										Public = (dictionary2["Public"] == 1),
										Password = dictionary["Password"],
										Target = dictionary["Target"],
										World = dictionary["World"],
										Name = dictionary["Name"]
									};
									Database.SessionSave(Session.Data);
								}
							}
							if (Profile.Active && text35 == "Wrench.Portal" && text36 == "Update")
							{
								if (Session.Data.Foreground[worker] != 207 && Session.Data.Foreground[worker] != 209 && Session.Data.Foreground[worker] != 211 && Session.Data.Foreground[worker] != 213)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else if (Session.TileAccess(Profile.Data.Filename, worker))
								{
									Database.SessionLoad(ref Session.Data, Session.Data.Name);
									Session.Data.Special[worker] = new PortalData
									{
										Public = (dictionary2["Public"] == 1),
										Target = dictionary["Target"],
										World = dictionary["World"]
									};
									Database.SessionSave(Session.Data);
								}
							}
							if (Profile.Active && text35 == "Wrench.Furnace")
							{
								if (Session.Data.Foreground[worker] != 155)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else if (Session.TileAccess(Profile.Data.Filename, worker))
								{
									bool flag8 = false;
									ushort item2 = 0;
									ushort num107 = 0;
									ushort num108 = 0;
									ushort num109 = 0;
									ushort num110 = 0;
									if (text36 == "Polyethylene")
									{
										flag8 = true;
										item2 = 163;
										num107 = 159;
										num108 = 1;
										num109 = 161;
										num110 = 1;
									}
									if (text36 == "PlasticWings")
									{
										flag8 = true;
										item2 = 239;
										num107 = 163;
										num108 = 500;
										num109 = 189;
										num110 = 3;
									}
									if (text36 == "GoldenPlasticWings")
									{
										flag8 = true;
										item2 = 241;
										num107 = 173;
										num108 = 500;
										num109 = 239;
										num110 = 5;
									}
									if (text36 == "DiamondPlasticWings")
									{
										flag8 = true;
										item2 = 391;
										num107 = 175;
										num108 = 500;
										num109 = 239;
										num110 = 5;
									}
									if (text36 == "EmeraldPlasticWings")
									{
										flag8 = true;
										item2 = 393;
										num107 = 177;
										num108 = 500;
										num109 = 239;
										num110 = 5;
									}
									if (text36 == "RubyPlasticWings")
									{
										flag8 = true;
										item2 = 395;
										num107 = 179;
										num108 = 500;
										num109 = 239;
										num110 = 5;
									}
									if (text36 == "GoldenRustyWings")
									{
										flag8 = true;
										item2 = 371;
										num107 = 173;
										num108 = 500;
										num109 = 193;
										num110 = 5;
									}
									if (flag8)
									{
										if (Profile.GetItem(num107) >= num108 && Profile.GetItem(num109) >= num110)
										{
											Profile.SetItem(num107, Profile.GetItem(num107) - num108);
											Profile.SetItem(num109, Profile.GetItem(num109) - num110);
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(6));
											Profile.WriteItems(binaryWriter);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
											Database.SessionLoad(ref Session.Data, Session.Data.Name);
											Session.Data.Special[worker] = new FurnaceData
											{
												Time = Item.Growtime(item2),
												Item = item2
											};
											Database.SessionSave(Session.Data);
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, 0, "Message");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Furnace", 75, 155);
											Dialog.Text(binaryWriter, breaker: true, "Started making ~1" + Item.Name(item2), 50);
											Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, 0, "Message");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Furnace", 75, 155);
											Dialog.Text(binaryWriter, breaker: true, "~3Required ingredients are:", 50);
											Dialog.ItemText(binaryWriter, breaker: true, "~1x" + num108 + " ~0" + Item.Name(num107), 50, num107);
											Dialog.ItemText(binaryWriter, breaker: true, "~1x" + num110 + " ~0" + Item.Name(num109), 50, num109);
											Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
									}
									if (text36 == "Collect" && Session.Data.Special[worker] != null)
									{
										FurnaceData furnaceData2 = Session.GetFurnaceData(worker);
										if (Item.Harvestable(furnaceData2.Time) && furnaceData2.Item != 0)
										{
											if (Profile.CanGetItem((ushort)furnaceData2.Item, 1))
											{
												Profile.SetItem((ushort)furnaceData2.Item, Profile.GetItem((ushort)furnaceData2.Item) + 1);
												Database.ProfileSave(Profile.Data);
												input = new MemoryStream();
												BinaryWriter binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(5));
												Window = Dialog.Create(binaryWriter, 0, "Message");
												Dialog.ItemText(binaryWriter, breaker: true, "~1Furnace", 75, 155);
												Dialog.Text(binaryWriter, breaker: true, "~4You got:", 50);
												Dialog.ItemText(binaryWriter, breaker: true, "~1x1 ~0" + Item.Name(furnaceData2.Item), 50, furnaceData2.Item);
												Dialog.ItemText(binaryWriter, breaker: true, "~1x50 ~0Experience", 50, 1);
												Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Send(input.ToArray());
												binaryWriter.Close();
												Experience(randomize: false, 50);
												input = new MemoryStream();
												binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(6));
												Profile.WriteItems(binaryWriter);
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Send(input.ToArray());
												binaryWriter.Close();
												Database.SessionLoad(ref Session.Data, Session.Data.Name);
												Session.Data.Special[worker] = null;
												Database.SessionSave(Session.Data);
											}
											else
											{
												input = new MemoryStream();
												BinaryWriter binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(5));
												Window = Dialog.Create(binaryWriter, 0, "Message");
												Dialog.ItemText(binaryWriter, breaker: true, "~1Furnace", 75, 155);
												Dialog.Text(binaryWriter, breaker: true, "Not enough inventory space to collect items.", 50);
												Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Send(input.ToArray());
												binaryWriter.Close();
											}
										}
									}
								}
							}
							if (Profile.Active && text35 == "Wrench.Entrance")
							{
								if (Session.Data.Foreground[worker] != 195 && Session.Data.Foreground[worker] != 205)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else if (text36 == "Accept" && Session.TileAccess(Profile.Data.Filename, worker))
								{
									Database.SessionLoad(ref Session.Data, Session.Data.Name);
									Session.Data.Special[worker] = new EntranceData
									{
										Closed = (dictionary2["Closed"] == 1)
									};
									Database.SessionSave(Session.Data);
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(11));
									binaryWriter.Write(Convert.ToUInt16(Session.GetTileX(worker)));
									binaryWriter.Write(Convert.ToUInt16(Session.GetTileY(worker)));
									binaryWriter.Write(Convert.ToUInt16(3));
									binaryWriter.Write(Convert.ToUInt16(dictionary2["Closed"]));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Server.Broadcast(input.ToArray(), delegate(Player player)
									{
										if (!player.Active)
										{
											return false;
										}
										if (!player.Profile.Active)
										{
											return false;
										}
										if (!player.Session.Active)
										{
											return false;
										}
										if (player.Session.Data.Name != Session.Data.Name)
										{
											return false;
										}
										return (!Session.TileAccess(player.Profile.Data.Filename, worker)) ? true : false;
									});
									binaryWriter.Close();
								}
							}
							if (Profile.Active && text35 == "Wrench.TrafficLight")
							{
								if (Session.Data.Foreground[worker] != 419)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else
								{
									switch (text36)
									{
									case "Green":
										if (!Session.TileAccess(Profile.Data.Filename, worker))
										{
											break;
										}
										goto case "Off";
									case "Off":
									case "Red":
									case "Yellow":
									{
										int num111 = 0;
										if (text36 == "Red")
										{
											num111 = 1;
										}
										if (text36 == "Yellow")
										{
											num111 = 2;
										}
										if (text36 == "Green")
										{
											num111 = 3;
										}
										Database.SessionLoad(ref Session.Data, Session.Data.Name);
										Session.Data.Special[worker] = new TrafficLightData
										{
											Light = num111
										};
										Database.SessionSave(Session.Data);
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(11));
										binaryWriter.Write(Convert.ToUInt16(Session.GetTileX(worker)));
										binaryWriter.Write(Convert.ToUInt16(Session.GetTileY(worker)));
										binaryWriter.Write(Convert.ToUInt16(3));
										binaryWriter.Write(Convert.ToUInt16(num111));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Server.Broadcast(input.ToArray(), delegate(Player player)
										{
											if (!player.Active)
											{
												return false;
											}
											if (!player.Profile.Active)
											{
												return false;
											}
											if (!player.Session.Active)
											{
												return false;
											}
											return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
										});
										binaryWriter.Close();
										break;
									}
									}
								}
							}
							if (Profile.Active && text35 == "Wrench.Lamp")
							{
								if (Session.Data.Foreground[worker] != 253 && Session.Data.Foreground[worker] != 443 && Session.Data.Foreground[worker] != 1087 && Session.Data.Foreground[worker] != 1089 && Session.Data.Foreground[worker] != 1091 && Session.Data.Foreground[worker] != 1093)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else if (text36 == "Accept" && Session.TileAccess(Profile.Data.Filename, worker))
								{
									Database.SessionLoad(ref Session.Data, Session.Data.Name);
									Session.Data.Special[worker] = new LampData
									{
										On = (dictionary2["On"] == 1)
									};
									Database.SessionSave(Session.Data);
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(11));
									binaryWriter.Write(Convert.ToUInt16(Session.GetTileX(worker)));
									binaryWriter.Write(Convert.ToUInt16(Session.GetTileY(worker)));
									binaryWriter.Write(Convert.ToUInt16(3));
									binaryWriter.Write(Convert.ToUInt16(dictionary2["On"] == 1));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Server.Broadcast(input.ToArray(), delegate(Player player)
									{
										if (!player.Active)
										{
											return false;
										}
										if (!player.Profile.Active)
										{
											return false;
										}
										if (!player.Session.Active)
										{
											return false;
										}
										return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
									});
									binaryWriter.Close();
								}
							}
							if (Profile.Active && text35 == "Wrench.Halloween.Enemy")
							{
								if (Session.Data.Foreground[worker] != 1143)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else if (text36 == "Accept" && Session.TileAccess(Profile.Data.Filename, worker))
								{
									Database.SessionLoad(ref Session.Data, Session.Data.Name);
									ushort.TryParse(dictionary["Image"], out var result14);
									ushort.TryParse(dictionary["Candies"], out var result15);
									if (result14 > 100)
									{
										result14 = 100;
									}
									if (result15 > 500)
									{
										result15 = 500;
									}
									Session.Data.Special[worker] = new HalloweenEnemyData
									{
										Image = result14,
										Candies = result15,
										Target = dictionary["Target"]
									};
									Database.SessionSave(Session.Data);
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(11));
									binaryWriter.Write(Convert.ToUInt16(Session.GetTileX(worker)));
									binaryWriter.Write(Convert.ToUInt16(Session.GetTileY(worker)));
									binaryWriter.Write(Convert.ToUInt16(3));
									binaryWriter.Write(Convert.ToUInt16(result14));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Server.Broadcast(input.ToArray(), delegate(Player player)
									{
										if (!player.Active)
										{
											return false;
										}
										if (!player.Profile.Active)
										{
											return false;
										}
										if (!player.Session.Active)
										{
											return false;
										}
										return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
									});
									binaryWriter.Close();
								}
							}
							if (Profile.Active && text35 == "Wrench.QuestNpc")
							{
								if (Session.Data.Foreground[worker] != 257 && Session.Data.Foreground[worker] != 1233)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else
								{
									switch (text36)
									{
									case "Start":
										if (Profile.Data.QuestType == 0)
										{
											Quest.RandomQuest(Profile.Data.QuestLevel, out Profile.Data.Data.QuestType, out Profile.Data.Data.QuestItem, out Profile.Data.Data.QuestLeft);
										}
										break;
									case "Finish":
										if (Profile.Data.QuestType != 0 && Profile.Data.QuestLeft <= 0)
										{
											if (Profile.CanGetItem(283, 1))
											{
												input = new MemoryStream();
												BinaryWriter binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(5));
												Window = Dialog.Create(binaryWriter, 0, "Message");
												Dialog.ItemText(binaryWriter, breaker: true, "~1Quest", 75, 3);
												Dialog.Text(binaryWriter, breaker: true, "You have completed your quest and got a", 50);
												Dialog.Text(binaryWriter, breaker: true, "quest token. Keep it going!", 50);
												Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Send(input.ToArray());
												binaryWriter.Close();
												Profile.SetItem(283, Profile.GetItem(283) + 1);
												Experience(randomize: false, 100);
												Profile.Data.QuestType = 0;
												Profile.Data.QuestItem = 0;
												Profile.Data.QuestLeft = 0;
												Profile.Data.QuestLevel++;
												input = new MemoryStream();
												binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(6));
												Profile.WriteItems(binaryWriter);
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Send(input.ToArray());
												binaryWriter.Close();
											}
											else
											{
												input = new MemoryStream();
												BinaryWriter binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(5));
												Window = Dialog.Create(binaryWriter, 0, "Message");
												Dialog.ItemText(binaryWriter, breaker: true, "~1Quest", 75, 3);
												Dialog.Text(binaryWriter, breaker: true, "Not enough inventory space.", 50);
												Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Send(input.ToArray());
												binaryWriter.Close();
											}
										}
										break;
									case "Skip":
										if (Platform.Mobile(Profile.PlatformType))
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, 0, "Message");
											Dialog.Text(binaryWriter, breaker: true, "Loading advertisement...", 75);
											Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(22));
											Advertisements.Write(binaryWriter, "SkipQuest");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, 0, "Message");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Skip quest", 75, 3);
											Dialog.Text(binaryWriter, breaker: true, "Sorry, advertisements are only", 50);
											Dialog.Text(binaryWriter, breaker: true, "available on mobile devices.", 50);
											Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										break;
									}
								}
							}
							if (Profile.Active && text35 == "Wrench.Mailbox")
							{
								if (Session.Data.Foreground[worker] != 307)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else if (text36 == "Post")
								{
									MailBoxData mailBoxData2 = Session.GetMailBoxData(worker);
									if (mailBoxData2.Text == null)
									{
										mailBoxData2.Text = new List<CommentData>();
									}
									bool flag9 = false;
									foreach (CommentData item4 in mailBoxData2.Text)
									{
										if (item4.Name == Profile.Data.Filename)
										{
											flag9 = true;
										}
									}
									if (flag9)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, 0, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Mailbox", 75, 307);
										Dialog.Text(binaryWriter, breaker: true, "You already have left a message in this", 50);
										Dialog.Text(binaryWriter, breaker: true, "mailbox, find another one or try later.", 50);
										Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (mailBoxData2.Text.Count >= 32)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, 0, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Mailbox", 75, 307);
										Dialog.Text(binaryWriter, breaker: true, "This mailbox has too much unread messages,", 50);
										Dialog.Text(binaryWriter, breaker: true, "please find another one or try later.", 50);
										Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else
									{
										mailBoxData2.Text.Add(new CommentData
										{
											Name = Profile.Data.Filename,
											Text = Text.FilterSwear(dictionary["Text"])
										});
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, 0, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Mailbox", 75, 307);
										Dialog.Text(binaryWriter, breaker: true, "Message has been submitted.", 50);
										Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									Session.Data.Special[worker] = mailBoxData2;
									Database.SessionSave(Session.Data);
								}
								else if (text36 == "Clear")
								{
									MailBoxData mailBoxData3 = Session.GetMailBoxData(worker);
									if (mailBoxData3.Text == null)
									{
										mailBoxData3.Text = new List<CommentData>();
									}
									mailBoxData3.Text.Clear();
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Message");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Mailbox", 75, 307);
									Dialog.Text(binaryWriter, breaker: true, "Messages cleared successfully.", 50);
									Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
									Session.Data.Special[worker] = mailBoxData3;
									Database.SessionSave(Session.Data);
								}
							}
							if (Profile.Active && text35 == "Wrench.BulletinBoard")
							{
								if (Session.Data.Foreground[worker] != 311)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else
								{
									switch (text36)
									{
									case "Post":
									{
										BulletinData bulletinData4 = Session.GetBulletinData(worker);
										if (bulletinData4.Text == null)
										{
											bulletinData4.Text = new List<CommentData>();
										}
										if (Session.TileAccess(Profile.Data.Filename, worker) || bulletinData4.Public)
										{
											bulletinData4.Text.Add(new CommentData
											{
												Name = Profile.Data.Filename,
												Text = Text.FilterSwear(dictionary["Text"])
											});
											if (bulletinData4.Text.Count > 20)
											{
												bulletinData4.Text.RemoveAt(0);
											}
											Session.Data.Special[worker] = bulletinData4;
											Database.SessionSave(Session.Data);
										}
										break;
									}
									case "Update":
										if (Session.TileAccess(Profile.Data.Filename, worker))
										{
											BulletinData bulletinData3 = Session.GetBulletinData(worker);
											bulletinData3.Public = dictionary2["Public"] == 1;
											bulletinData3.Author = dictionary2["Author"] == 1;
											Session.Data.Special[worker] = bulletinData3;
											Database.SessionSave(Session.Data);
										}
										break;
									case "Clear":
										if (Session.TileAccess(Profile.Data.Filename, worker))
										{
											BulletinData bulletinData2 = Session.GetBulletinData(worker);
											if (bulletinData2.Text == null)
											{
												bulletinData2.Text = new List<CommentData>();
											}
											bulletinData2.Text.Clear();
											Session.Data.Special[worker] = bulletinData2;
											Database.SessionSave(Session.Data);
										}
										break;
									}
								}
							}
							if (Profile.Active && text35 == "Wrench.WorldLock.Owner")
							{
								if (Session.Data.Foreground[worker] != 73 && Session.Data.Foreground[worker] != 493)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else
								{
									switch (text36)
									{
									case "Access":
									{
										Database.SessionLoad(ref Session.Data, Session.Data.Name);
										if (!Session.WorldOwner(Profile.Data.Filename))
										{
											break;
										}
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, worker, "Wrench.WorldLock.Owner.GiveAccess");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Add admins", 75, Session.Data.Foreground[worker]);
										bool flag10 = true;
										Player[] array29 = Server.Online.ToArray();
										foreach (Player player25 in array29)
										{
											if (player25 != this && player25.Active && player25.Profile.Active && player25.Session.Active && !(player25.Session.Data.Name != Session.Data.Name) && player25.Profile.VisibleTo(this) && !Session.Data.Admin.Contains(player25.Profile.Data.Filename))
											{
												Dialog.Checkbox(binaryWriter, breaker: true, value: false, player25.Profile.Data.Filename, player25.Profile.Data.Username, 75);
												flag10 = false;
											}
										}
										if (flag10)
										{
											Dialog.Text(binaryWriter, breaker: true, "There's no one in this world who you", 50);
											Dialog.Text(binaryWriter, breaker: true, "might want to give access to.", 50);
										}
										if (flag10)
										{
											Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
										}
										else
										{
											Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
											Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										}
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
										break;
									}
									case "GetKey":
										Database.SessionLoad(ref Session.Data, Session.Data.Name);
										if (Session.WorldOwner(Profile.Data.Filename))
										{
											Profile.SetItem(273, 1);
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, worker, "Message");
											Dialog.ItemText(binaryWriter, breaker: true, "~1World Lock key", 75, Session.Data.Foreground[worker]);
											Dialog.Text(binaryWriter, breaker: true, "You've got the world key, you can now use", 50);
											Dialog.Text(binaryWriter, breaker: true, "it to trade your world to other players.", 50);
											Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(6));
											Profile.WriteItems(binaryWriter);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										break;
									case "Accept":
									{
										Database.SessionLoad(ref Session.Data, Session.Data.Name);
										if (!Session.WorldOwner(Profile.Data.Filename))
										{
											break;
										}
										if (Session.Data.Public != (dictionary2["Public"] == 1))
										{
											Session.Data.Public = dictionary2["Public"] == 1;
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(4));
											string text75 = string.Format("~1{0} ~0has set the world to {1}~0.", Profile.Data.Username, Session.Data.Public ? "~4public" : "~3private");
											binaryWriter.Write(Encoding.UTF8.GetBytes(text75 + "\0"));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Server.Broadcast(input.ToArray(), delegate(Player player)
											{
												if (!player.Active)
												{
													return false;
												}
												if (!player.Profile.Active)
												{
													return false;
												}
												if (!player.Session.Active)
												{
													return false;
												}
												return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
											});
											binaryWriter.Close();
											Player[] array26 = Server.Online.ToArray();
											foreach (Player player23 in array26)
											{
												if (player23 != this && player23.Active && player23.Profile.Active && player23.Session.Active && !(player23.Session.Data.Name != Session.Data.Name))
												{
													input = new MemoryStream();
													binaryWriter = new BinaryWriter(input);
													binaryWriter.Write(Convert.ToUInt16(0));
													binaryWriter.Write(Convert.ToUInt16(10));
													player23.Session.WriteTiles(binaryWriter, player23.Profile.Data.Filename);
													binaryWriter.Seek(0, SeekOrigin.Begin);
													binaryWriter.Write(Convert.ToUInt16(input.Length));
													player23.Send(input.ToArray());
													binaryWriter.Close();
												}
											}
										}
										string[] array27 = Session.Data.Admin.ToArray();
										foreach (string text76 in array27)
										{
											if (!dictionary2.ContainsKey(text76) || dictionary2[text76] != 0)
											{
												continue;
											}
											Session.Data.Admin.Remove(text76);
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(4));
											string text77 = "Owner has removed ~1" + text76 + "~0's access to the world lock.";
											binaryWriter.Write(Encoding.UTF8.GetBytes(text77 + "\0"));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Server.Broadcast(input.ToArray(), delegate(Player player)
											{
												if (!player.Active)
												{
													return false;
												}
												if (!player.Profile.Active)
												{
													return false;
												}
												if (!player.Session.Active)
												{
													return false;
												}
												return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
											});
											binaryWriter.Close();
											Player[] array28 = Server.Online.ToArray();
											foreach (Player player24 in array28)
											{
												if (player24 != this && player24.Active && player24.Profile.Active && player24.Session.Active && !(player24.Session.Data.Name != Session.Data.Name) && !(player24.Profile.Data.Filename != text76))
												{
													input = new MemoryStream();
													binaryWriter = new BinaryWriter(input);
													binaryWriter.Write(Convert.ToUInt16(0));
													binaryWriter.Write(Convert.ToUInt16(10));
													player24.Session.WriteTiles(binaryWriter, player24.Profile.Data.Filename);
													binaryWriter.Seek(0, SeekOrigin.Begin);
													binaryWriter.Write(Convert.ToUInt16(input.Length));
													player24.Send(input.ToArray());
													binaryWriter.Close();
												}
											}
										}
										Database.SessionSave(Session.Data);
										break;
									}
									}
								}
							}
							if (Profile.Active && text35 == "Wrench.WorldLock.Owner.GiveAccess")
							{
								if (Session.Data.Foreground[worker] != 73 && Session.Data.Foreground[worker] != 493)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else
								{
									Database.SessionLoad(ref Session.Data, Session.Data.Name);
									if (Session.WorldOwner(Profile.Data.Filename))
									{
										Player[] array30 = Server.Online.ToArray();
										foreach (Player player26 in array30)
										{
											if (player26 == this || !player26.Active || !player26.Profile.Active || !player26.Session.Active || player26.Session.Data.Name != Session.Data.Name || !dictionary2.ContainsKey(player26.Profile.Data.Filename) || dictionary2[player26.Profile.Data.Filename] != 1 || Session.Data.Admin.Contains(player26.Profile.Data.Filename))
											{
												continue;
											}
											Session.Data.Admin.Add(player26.Profile.Data.Filename);
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(4));
											string text78 = $"Owner has given ~1{player26.Profile.Data.Username} ~0access to the world lock.";
											binaryWriter.Write(Encoding.UTF8.GetBytes(text78 + "\0"));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Player[] array31 = Server.Online.ToArray();
											foreach (Player player27 in array31)
											{
												if (player27.Active && player27.Profile.Active && player27.Session.Active && !(player27.Session.Data.Name != Session.Data.Name))
												{
													player27.Send(input.ToArray());
												}
											}
											binaryWriter.Close();
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(10));
											player26.Session.WriteTiles(binaryWriter, player26.Profile.Data.Filename);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											player26.Send(input.ToArray());
											binaryWriter.Close();
										}
										Database.SessionSave(Session.Data);
									}
								}
							}
							if (Profile.Active && text35 == "Wrench.WorldLock.Admin")
							{
								if (Session.Data.Foreground[worker] != 73 && Session.Data.Foreground[worker] != 493)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else if (text36 == "RemoveAccess")
								{
									Database.SessionLoad(ref Session.Data, Session.Data.Name);
									if (Session.Data.Admin.Contains(Profile.Data.Filename))
									{
										string filename = Profile.Data.Filename;
										Session.Data.Admin.Remove(filename);
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										string text79 = "~1" + filename + "~0 has removed their admin access.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text79 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Server.Broadcast(input.ToArray(), delegate(Player player)
										{
											if (!player.Active)
											{
												return false;
											}
											if (!player.Profile.Active)
											{
												return false;
											}
											if (!player.Session.Active)
											{
												return false;
											}
											return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
										});
										binaryWriter.Close();
										input = new MemoryStream();
										binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(10));
										Session.WriteTiles(binaryWriter, Profile.Data.Filename);
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
										Database.SessionSave(Session.Data);
									}
								}
							}
							if (Profile.Active && text35 == "Wrench.Lock.Owner")
							{
								if (Session.Data.Foreground[worker] != 319 && Session.Data.Foreground[worker] != 321 && Session.Data.Foreground[worker] != 323)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else if (text36 == "Access")
								{
									LockData lockData7 = Session.GetLockData(worker);
									if (Session.TileOwner(Profile.Data.Filename, worker))
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, worker, "Wrench.Lock.Owner.GiveAccess");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Add admins", 75, Session.Data.Foreground[worker]);
										bool flag11 = true;
										Player[] array32 = Server.Online.ToArray();
										foreach (Player player28 in array32)
										{
											if (player28 != this && player28.Active && player28.Profile.Active && player28.Session.Active && !(player28.Session.Data.Name != Session.Data.Name) && player28.Profile.VisibleTo(this) && !lockData7.Admin.Contains(player28.Profile.Data.Filename))
											{
												Dialog.Checkbox(binaryWriter, breaker: true, value: false, player28.Profile.Data.Filename, player28.Profile.Data.Username, 75);
												flag11 = false;
											}
										}
										if (flag11)
										{
											Dialog.Text(binaryWriter, breaker: true, "There's no one in this world who you", 50);
											Dialog.Text(binaryWriter, breaker: true, "might want to give access to.", 50);
										}
										if (flag11)
										{
											Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
										}
										else
										{
											Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
											Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										}
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
								else if (text36 == "Accept")
								{
									LockData lockData8 = Session.GetLockData(worker);
									if (Session.TileOwner(Profile.Data.Filename, worker))
									{
										if (lockData8.Public != (dictionary2["Public"] == 1))
										{
											lockData8.Public = dictionary2["Public"] == 1;
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(4));
											string text80 = string.Format("~1{0} ~0has set their lock to {1}~0.", Profile.Data.Username, lockData8.Public ? "~4public" : "~3private");
											binaryWriter.Write(Encoding.UTF8.GetBytes(text80 + "\0"));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Server.Broadcast(input.ToArray(), delegate(Player player)
											{
												if (!player.Active)
												{
													return false;
												}
												if (!player.Profile.Active)
												{
													return false;
												}
												if (!player.Session.Active)
												{
													return false;
												}
												return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
											});
											binaryWriter.Close();
											Player[] array33 = Server.Online.ToArray();
											foreach (Player player29 in array33)
											{
												if (player29 != this && player29.Active && player29.Profile.Active && player29.Session.Active && !(player29.Session.Data.Name != Session.Data.Name))
												{
													input = new MemoryStream();
													binaryWriter = new BinaryWriter(input);
													binaryWriter.Write(Convert.ToUInt16(0));
													binaryWriter.Write(Convert.ToUInt16(10));
													player29.Session.WriteTiles(binaryWriter, player29.Profile.Data.Filename);
													binaryWriter.Seek(0, SeekOrigin.Begin);
													binaryWriter.Write(Convert.ToUInt16(input.Length));
													player29.Send(input.ToArray());
													binaryWriter.Close();
												}
											}
										}
										if (lockData8.Ignore != (dictionary2["Ignore"] == 1))
										{
											lockData8.Ignore = dictionary2["Ignore"] == 1;
											if (lockData8.Ignore)
											{
												Database.SessionLoad(ref Session.Data, Session.Data.Name);
												if (Session.Data.Foreground[worker] == 319)
												{
													Session.LockCapacity = 9;
												}
												else if (Session.Data.Foreground[worker] == 321)
												{
													Session.LockCapacity = 49;
												}
												else if (Session.Data.Foreground[worker] == 323)
												{
													Session.LockCapacity = 225;
												}
												else
												{
													Session.LockCapacity = 0;
												}
												int tileX2 = Session.GetTileX(worker);
												int tileY2 = Session.GetTileY(worker);
												Session.UnlockArea(new Parent
												{
													X = tileX2,
													Y = tileY2
												});
												Session.LockNearest(new Parent
												{
													X = tileX2,
													Y = tileY2
												}, tileX2, tileY2);
												Database.SessionSave(Session.Data);
											}
											else
											{
												Database.SessionLoad(ref Session.Data, Session.Data.Name);
												if (Session.Data.Foreground[worker] == 319)
												{
													Session.LockCapacity = 9;
												}
												else if (Session.Data.Foreground[worker] == 321)
												{
													Session.LockCapacity = 49;
												}
												else if (Session.Data.Foreground[worker] == 323)
												{
													Session.LockCapacity = 225;
												}
												else
												{
													Session.LockCapacity = 0;
												}
												int tileX3 = Session.GetTileX(worker);
												int tileY3 = Session.GetTileY(worker);
												Session.UnlockArea(new Parent
												{
													X = tileX3,
													Y = tileY3
												});
												Session.LockRectangle(new Parent
												{
													X = tileX3,
													Y = tileY3
												}, tileX3, tileY3, Session.LockCapacity);
												Database.SessionSave(Session.Data);
											}
											Player[] array34 = Server.Online.ToArray();
											foreach (Player player30 in array34)
											{
												if (player30.Active && player30.Profile.Active && player30.Session.Active && !(player30.Session.Data.Name != Session.Data.Name))
												{
													input = new MemoryStream();
													BinaryWriter binaryWriter = new BinaryWriter(input);
													binaryWriter.Write(Convert.ToUInt16(0));
													binaryWriter.Write(Convert.ToUInt16(10));
													player30.Session.WriteTiles(binaryWriter, player30.Profile.Data.Filename);
													binaryWriter.Seek(0, SeekOrigin.Begin);
													binaryWriter.Write(Convert.ToUInt16(input.Length));
													player30.Send(input.ToArray());
													binaryWriter.Close();
												}
											}
										}
										object[] array35 = lockData8.Admin.ToArray();
										for (int num121 = 0; num121 < array35.Length; num121++)
										{
											string text81 = (string)array35[num121];
											if (!dictionary2.ContainsKey(text81) || dictionary2[text81] != 0)
											{
												continue;
											}
											lockData8.Admin.Remove(text81);
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(4));
											string text82 = "Owner has removed ~1" + text81 + "~0's access to the lock.";
											binaryWriter.Write(Encoding.UTF8.GetBytes(text82 + "\0"));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Server.Broadcast(input.ToArray(), delegate(Player player)
											{
												if (!player.Active)
												{
													return false;
												}
												if (!player.Profile.Active)
												{
													return false;
												}
												if (!player.Session.Active)
												{
													return false;
												}
												return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
											});
											binaryWriter.Close();
											Player[] array36 = Server.Online.ToArray();
											foreach (Player player31 in array36)
											{
												if (player31 != this && player31.Active && player31.Profile.Active && player31.Session.Active && !(player31.Session.Data.Name != Session.Data.Name) && !(player31.Profile.Data.Filename != text81))
												{
													input = new MemoryStream();
													binaryWriter = new BinaryWriter(input);
													binaryWriter.Write(Convert.ToUInt16(0));
													binaryWriter.Write(Convert.ToUInt16(10));
													player31.Session.WriteTiles(binaryWriter, player31.Profile.Data.Filename);
													binaryWriter.Seek(0, SeekOrigin.Begin);
													binaryWriter.Write(Convert.ToUInt16(input.Length));
													player31.Send(input.ToArray());
													binaryWriter.Close();
												}
											}
										}
										Session.Data.Special[worker] = lockData8;
										Database.SessionSave(Session.Data);
									}
								}
							}
							if (Profile.Active && text35 == "Wrench.Lock.Owner.GiveAccess")
							{
								if (Session.Data.Foreground[worker] != 319 && Session.Data.Foreground[worker] != 321 && Session.Data.Foreground[worker] != 323)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else
								{
									LockData lockData9 = Session.GetLockData(worker);
									if (Session.TileOwner(Profile.Data.Filename, worker))
									{
										Player[] array37 = Server.Online.ToArray();
										foreach (Player player32 in array37)
										{
											if (player32 == this || !player32.Active || !player32.Profile.Active || !player32.Session.Active || player32.Session.Data.Name != Session.Data.Name || !dictionary2.ContainsKey(player32.Profile.Data.Filename) || dictionary2[player32.Profile.Data.Filename] != 1 || lockData9.Admin.Contains(player32.Profile.Data.Filename))
											{
												continue;
											}
											lockData9.Admin.Add(player32.Profile.Data.Filename);
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(4));
											string text83 = $"Owner has given ~1{player32.Profile.Data.Username} ~0access to the lock.";
											binaryWriter.Write(Encoding.UTF8.GetBytes(text83 + "\0"));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Player[] array38 = Server.Online.ToArray();
											foreach (Player player33 in array38)
											{
												if (player33.Active && player33.Profile.Active && player33.Session.Active && !(player33.Session.Data.Name != Session.Data.Name))
												{
													player33.Send(input.ToArray());
												}
											}
											binaryWriter.Close();
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(10));
											player32.Session.WriteTiles(binaryWriter, player32.Profile.Data.Filename);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											player32.Send(input.ToArray());
											binaryWriter.Close();
										}
										Session.Data.Special[worker] = lockData9;
										Database.SessionSave(Session.Data);
									}
								}
							}
							if (Profile.Active && text35 == "Wrench.Lock.Admin")
							{
								if (Session.Data.Foreground[worker] != 319 && Session.Data.Foreground[worker] != 321 && Session.Data.Foreground[worker] != 323)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else if (text36 == "RemoveAccess")
								{
									LockData lockData10 = Session.GetLockData(worker);
									if (Session.TileAccess(Profile.Data.Filename, worker))
									{
										string filename2 = Profile.Data.Filename;
										lockData10.Admin.Remove(filename2);
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										string text84 = "~1" + filename2 + "~0 has removed their admin access.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text84 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Server.Broadcast(input.ToArray(), delegate(Player player)
										{
											if (!player.Active)
											{
												return false;
											}
											if (!player.Profile.Active)
											{
												return false;
											}
											if (!player.Session.Active)
											{
												return false;
											}
											return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
										});
										binaryWriter.Close();
										input = new MemoryStream();
										binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(10));
										Session.WriteTiles(binaryWriter, Profile.Data.Filename);
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
										Session.Data.Special[worker] = lockData10;
										Database.SessionSave(Session.Data);
									}
								}
							}
							if (Profile.Active && text35 == "Wrench.VendingMachine")
							{
								if (Session.Data.Foreground[worker] != 313)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else if (text36 == "Add" && Session.TileAccess(Profile.Data.Filename, worker))
								{
									VendingData vendingData4 = Session.GetVendingData(worker);
									if (vendingData4.Index == 0 && vendingData4.Count == 0 && vendingData4.Price == 0)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, worker, "Wrench.VendingMachine.Add");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Vending Machine", 75, 313);
										Dialog.Text(binaryWriter, breaker: true, "Tap on the box to select item", 50);
										Dialog.ItemPicker(binaryWriter, breaker: true, "Index", 0, 64, 64);
										Dialog.Text(binaryWriter, breaker: true, "How many to sell?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Count", "", 8);
										Dialog.Text(binaryWriter, breaker: true, "What's the price?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Price", "", 8);
										Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
								else if (text36 == "Earn" && Session.TileAccess(Profile.Data.Filename, worker))
								{
									VendingData vendingData5 = Session.GetVendingData(worker);
									if (vendingData5.Sold && vendingData5.Index != 0 && vendingData5.Count != 0 && vendingData5.Price != 0)
									{
										if (Profile.CanGetItem(73, vendingData5.Price))
										{
											Database.SessionLoad(ref Session.Data, Session.Data.Name);
											Session.Data.Special[worker] = null;
											Database.SessionSave(Session.Data);
											Profile.SetItem(73, Profile.GetItem(73) + vendingData5.Price);
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, worker, "Message");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Vending Machine", 75, 313);
											Dialog.Text(binaryWriter, breaker: true, "You earned ~1x" + vendingData5.Price + " " + Item.Name(73), 50);
											Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(6));
											Profile.WriteItems(binaryWriter);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, worker, "Message");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Vending Machine", 75, 313);
											Dialog.Text(binaryWriter, breaker: true, "~3Not enough inventory space.", 50);
											Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
									}
								}
								else if (text36 == "Take" && Session.TileAccess(Profile.Data.Filename, worker))
								{
									VendingData vendingData6 = Session.GetVendingData(worker);
									if (!vendingData6.Sold && vendingData6.Index != 0 && vendingData6.Count != 0 && vendingData6.Price != 0)
									{
										if (Profile.CanGetItem(vendingData6.Index, vendingData6.Count))
										{
											Database.SessionLoad(ref Session.Data, Session.Data.Name);
											Session.Data.Special[worker] = null;
											Database.SessionSave(Session.Data);
											Profile.SetItem(vendingData6.Index, Profile.GetItem(vendingData6.Index) + vendingData6.Count);
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, worker, "Message");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Vending Machine", 75, 313);
											Dialog.Text(binaryWriter, breaker: true, "You took ~1x" + vendingData6.Count + " " + Item.Name(vendingData6.Index), 50);
											Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(6));
											Profile.WriteItems(binaryWriter);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, worker, "Message");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Vending Machine", 75, 313);
											Dialog.Text(binaryWriter, breaker: true, "~3Not enough inventory space.", 50);
											Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
									}
								}
								else if (text36 == "Buy")
								{
									VendingData vendingData7 = Session.GetVendingData(worker);
									if (vendingData7.Index != 0 && vendingData7.Count != 0 && vendingData7.Price != 0)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, worker, "Wrench.VendingMachine.Buy");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Vending Machine", 75, 313);
										Dialog.Text(binaryWriter, breaker: true, "Do you really want to buy this?", 50);
										Dialog.Text(binaryWriter, breaker: true, "~3You'll give:", 50);
										Dialog.ItemText(binaryWriter, breaker: true, $"~1{vendingData7.Price}x ~0{Item.Name(73)}", 50, 73);
										Dialog.Text(binaryWriter, breaker: true, "~4You'll get:", 50);
										Dialog.ItemText(binaryWriter, breaker: true, $"~1{vendingData7.Count}x ~0{Item.Name(vendingData7.Index)}", 50, vendingData7.Index);
										Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
							}
							if (Profile.Active && text35 == "Wrench.VendingMachine.Add")
							{
								if (Session.Data.Foreground[worker] != 313)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else if (text36 == "Accept" && Session.TileAccess(Profile.Data.Filename, worker))
								{
									VendingData vendingData8 = Session.GetVendingData(worker);
									if (vendingData8.Index == 0 && vendingData8.Count == 0 && vendingData8.Price == 0)
									{
										ushort num125 = dictionary2["Index"];
										ushort.TryParse(dictionary["Count"], out var result16);
										ushort.TryParse(dictionary["Price"], out var result17);
										if (!Item.Vendable(num125))
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, worker, "Message");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Vending Machine", 75, 313);
											Dialog.Text(binaryWriter, breaker: true, "Cannot sell this item.", 50);
											Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else if (result16 < 1 || result16 > 500)
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, worker, "Message");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Vending Machine", 75, 313);
											Dialog.Text(binaryWriter, breaker: true, "Invalid item count specified, the range is 1 to 500.", 50);
											Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else if (result17 < 1 || result17 > 500)
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, worker, "Message");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Vending Machine", 75, 313);
											Dialog.Text(binaryWriter, breaker: true, "Invalid item price specified, the range is 1 to 500.", 50);
											Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else if (Profile.GetItem(num125) < result16)
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, worker, "Message");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Vending Machine", 75, 313);
											Dialog.Text(binaryWriter, breaker: true, "You don't own that much items to sell.", 50);
											Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else
										{
											Profile.SetItem(num125, Profile.GetItem(num125) - result16);
											Database.SessionLoad(ref Session.Data, Session.Data.Name);
											Session.Data.Special[worker] = new VendingData
											{
												Sold = false,
												Index = num125,
												Count = result16,
												Price = result17
											};
											Database.SessionSave(Session.Data);
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(6));
											Profile.WriteItems(binaryWriter);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(14));
											binaryWriter.Write(Convert.ToInt32(0));
											Profile.WriteData(binaryWriter, this);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
											Player[] array39 = Server.Online.ToArray();
											foreach (Player player34 in array39)
											{
												if (player34 != this && player34.Active && player34.Profile.Active && player34.Session.Active && !(player34.Session.Data.Name != Session.Data.Name))
												{
													input = new MemoryStream();
													binaryWriter = new BinaryWriter(input);
													binaryWriter.Write(Convert.ToUInt16(0));
													binaryWriter.Write(Convert.ToUInt16(14));
													binaryWriter.Write(Convert.ToInt32(Identifier));
													Profile.WriteData(binaryWriter, player34);
													binaryWriter.Seek(0, SeekOrigin.Begin);
													binaryWriter.Write(Convert.ToUInt16(input.Length));
													player34.Send(input.ToArray());
													binaryWriter.Close();
												}
											}
										}
									}
								}
							}
							if (Profile.Active && text35 == "Wrench.VendingMachine.Buy")
							{
								if (Session.Data.Foreground[worker] != 313)
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(4));
									string text85 = "Somehow the vending machine has disappeared, nothing to edit.";
									binaryWriter.Write(Encoding.UTF8.GetBytes(text85 + "\0"));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								else if (text36 == "Accept")
								{
									VendingData vendingData9 = Session.GetVendingData(worker);
									if (Profile.GetItem(73) < vendingData9.Price)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, worker, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Vending Machine", 75, 313);
										Dialog.Text(binaryWriter, breaker: true, "Sorry, you cannot afford this item.", 50);
										Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (!Profile.CanGetItem(vendingData9.Index, vendingData9.Count))
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, worker, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Vending Machine", 75, 313);
										Dialog.Text(binaryWriter, breaker: true, "Sorry, you don't have enough inventory space.", 50);
										Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else
									{
										vendingData9 = Session.GetVendingData(worker);
										if (!vendingData9.Sold && vendingData9.Index != 0 && vendingData9.Count != 0 && vendingData9.Price != 0)
										{
											Database.SessionLoad(ref Session.Data, Session.Data.Name);
											Session.Data.Special[worker] = new VendingData
											{
												Index = vendingData9.Index,
												Count = vendingData9.Count,
												Price = vendingData9.Price,
												Sold = true
											};
											Database.SessionSave(Session.Data);
											Profile.SetItem(73, Profile.GetItem(73) - vendingData9.Price);
											Profile.SetItem(vendingData9.Index, Profile.GetItem(vendingData9.Index) + vendingData9.Count);
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, worker, "Message");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Vending Machine", 75, 313);
											Dialog.Text(binaryWriter, breaker: true, "You bought ~1x" + vendingData9.Count + " " + Item.Name(vendingData9.Index), 50);
											Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(6));
											Profile.WriteItems(binaryWriter);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
									}
								}
							}
							if (Profile.Active && text35 == "Wrench.RewardBox1")
							{
								if (Session.Data.Foreground[worker] != 221)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else if (text36 == "Claim")
								{
									if (Platform.Mobile(Profile.PlatformType))
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, 0, "Message");
										Dialog.Text(binaryWriter, breaker: true, "Loading advertisement...", 75);
										Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
										input = new MemoryStream();
										binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(22));
										Advertisements.Write(binaryWriter, "RewardBox1");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else
									{
										Reward1();
									}
								}
							}
							if (Profile.Active && text35 == "Wrench.RewardBox1.Respin" && text36 == "Respin" && Profile.Data.AllowRespin)
							{
								if (Platform.Mobile(Profile.PlatformType))
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Message");
									Dialog.Text(binaryWriter, breaker: true, "Loading advertisement...", 75);
									Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
									input = new MemoryStream();
									binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(22));
									Advertisements.Write(binaryWriter, "RewardBox1");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								else
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Message");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Second daily reward", 75, 3);
									Dialog.Text(binaryWriter, breaker: true, "Sorry, advertisements are only", 50);
									Dialog.Text(binaryWriter, breaker: true, "available on mobile devices.", 50);
									Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
							}
							if (Profile.Active && text35 == "Wrench.RewardBox2")
							{
								if (Session.Data.Foreground[worker] != 237)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else if (text36 == "Watch")
								{
									if (Platform.Mobile(Profile.PlatformType))
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, 0, "Message");
										Dialog.Text(binaryWriter, breaker: true, "Loading advertisement...", 75);
										Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
										input = new MemoryStream();
										binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(22));
										Advertisements.Write(binaryWriter, "RewardBox2");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, 0, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Free gems", 75, 3);
										Dialog.Text(binaryWriter, breaker: true, "Sorry, advertisements are only", 50);
										Dialog.Text(binaryWriter, breaker: true, "available on mobile devices.", 50);
										Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
							}
							if (Profile.Active && text35 == "Wrench.RewardBox3")
							{
								if (Session.Data.Foreground[worker] != 433)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else if (text36 == "Claim" && Event.Active && Event.TicketActive(Profile.Data.TicketDuration) && Session.Data.Name == Event.World)
								{
									if (Session.Data.Foreground[worker] != 433)
									{
										PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
										{
											Dialog.ItemText(dialog, breaker: true, "~1Event reward", 75, 3);
											Dialog.Text(dialog, breaker: true, "~3The reward box has disappeared.", 50);
											Dialog.Button(dialog, breaker: false, "Okay", "Okay");
										});
									}
									else if (Profile.CanGetReward(Event.Rewards1.ToArray()))
									{
										reward = Event.GetReward1();
										Profile.SetItem(reward.Index, Profile.GetItem(reward.Index) + reward.Count);
										Profile.Data.TicketDuration = 0;
										PlayerCore.UpdateInventory(this);
										PlayerCore.CancelTimer(this);
										PlayerCore.UpdateDialog(this, 0, "Event.Finish", delegate(BinaryWriter dialog)
										{
											Dialog.ItemText(dialog, breaker: true, "~1Event reward", 75, 3);
											Dialog.Text(dialog, breaker: true, "You have been rewarded with:", 50);
											Dialog.ItemText(dialog, breaker: true, $"x{reward.Count} ~1{Item.Name(reward.Index)}", 50, reward.Index);
											Dialog.Button(dialog, breaker: false, "Okay", "Okay");
										});
									}
									else
									{
										PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
										{
											Dialog.ItemText(dialog, breaker: true, "~1Event reward", 75, 3);
											Dialog.Text(dialog, breaker: true, "~3Not enough inventory space.", 50);
											Dialog.Button(dialog, breaker: false, "Okay", "Okay");
										});
									}
								}
							}
							if (Profile.Active && text35 == "Wrench.MusicBlock")
							{
								if (Session.Data.Foreground[worker] != 485 && Session.Data.Foreground[worker] != 487 && Session.Data.Foreground[worker] != 489 && Session.Data.Foreground[worker] != 491)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else if (text36 == "Accept" && Session.TileAccess(Profile.Data.Filename, worker))
								{
									ushort num127 = 0;
									if (dictionary["Sound"].ToUpper() == "A")
									{
										num127 = 0;
									}
									if (dictionary["Sound"].ToUpper() == "B")
									{
										num127 = 1;
									}
									if (dictionary["Sound"].ToUpper() == "C")
									{
										num127 = 2;
									}
									if (dictionary["Sound"].ToUpper() == "D")
									{
										num127 = 3;
									}
									if (dictionary["Sound"].ToUpper() == "E")
									{
										num127 = 4;
									}
									if (dictionary["Sound"].ToUpper() == "F")
									{
										num127 = 5;
									}
									if (dictionary["Sound"].ToUpper() == "G")
									{
										num127 = 6;
									}
									Database.SessionLoad(ref Session.Data, Session.Data.Name);
									Session.Data.Special[worker] = new MusicBlockData
									{
										Sound = num127
									};
									Database.SessionSave(Session.Data);
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(11));
									binaryWriter.Write(Convert.ToUInt16(Session.GetTileX(worker)));
									binaryWriter.Write(Convert.ToUInt16(Session.GetTileY(worker)));
									binaryWriter.Write(Convert.ToUInt16(3));
									binaryWriter.Write(Convert.ToUInt16(num127));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Server.Broadcast(input.ToArray(), delegate(Player player)
									{
										if (!player.Active)
										{
											return false;
										}
										if (!player.Profile.Active)
										{
											return false;
										}
										if (!player.Session.Active)
										{
											return false;
										}
										return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
									});
									binaryWriter.Close();
								}
							}
							if (Profile.Active && text35 == "Wrench.DisplayBox")
							{
								if (Session.Data.Foreground[worker] != 535)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else if (text36 == "Add")
								{
									ushort num128 = dictionary2["Index"];
									if (Session.GetDisplayBoxData(worker).Index == 0 && Session.TileAccess(Profile.Data.Filename, worker) && Profile.HasItem(num128, 1))
									{
										if (Item.Tradeable(num128) && num128 != 273)
										{
											Profile.SetItem(num128, Profile.GetItem(num128) - 1);
											Database.ProfileSave(Profile.Data);
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(6));
											Profile.WriteItems(binaryWriter);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(14));
											binaryWriter.Write(Convert.ToInt32(0));
											Profile.WriteData(binaryWriter, this);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
											Player[] array40 = Server.Online.ToArray();
											foreach (Player player35 in array40)
											{
												if (player35 != this && player35.Active && player35.Profile.Active && player35.Session.Active && !(player35.Session.Data.Name != Session.Data.Name))
												{
													input = new MemoryStream();
													binaryWriter = new BinaryWriter(input);
													binaryWriter.Write(Convert.ToUInt16(0));
													binaryWriter.Write(Convert.ToUInt16(14));
													binaryWriter.Write(Convert.ToInt32(Identifier));
													Profile.WriteData(binaryWriter, player35);
													binaryWriter.Seek(0, SeekOrigin.Begin);
													binaryWriter.Write(Convert.ToUInt16(input.Length));
													player35.Send(input.ToArray());
													binaryWriter.Close();
												}
											}
											Database.SessionLoad(ref Session.Data, Session.Data.Name);
											Session.Data.Special[worker] = new DisplayBoxData
											{
												Index = num128
											};
											Database.SessionSave(Session.Data);
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(11));
											binaryWriter.Write(Convert.ToUInt16(Session.GetTileX(worker)));
											binaryWriter.Write(Convert.ToUInt16(Session.GetTileY(worker)));
											binaryWriter.Write(Convert.ToUInt16(3));
											binaryWriter.Write(Convert.ToUInt16(num128));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Server.Broadcast(input.ToArray(), delegate(Player player)
											{
												if (!player.Active)
												{
													return false;
												}
												if (!player.Profile.Active)
												{
													return false;
												}
												if (!player.Session.Active)
												{
													return false;
												}
												return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
											});
											binaryWriter.Close();
										}
										else
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, 0, "Message");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Display Box", 75, 3);
											Dialog.Text(binaryWriter, breaker: true, "~3You cannot display this.", 50);
											Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
									}
								}
								else if (text36 == "Take")
								{
									DisplayBoxData displayBoxData3 = Session.GetDisplayBoxData(worker);
									if (displayBoxData3.Index != 0 && Session.TileAccess(Profile.Data.Filename, worker))
									{
										if (Profile.CanGetItem(displayBoxData3.Index, 1))
										{
											Profile.SetItem(displayBoxData3.Index, Profile.GetItem(displayBoxData3.Index) + 1);
											Database.ProfileSave(Profile.Data);
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(6));
											Profile.WriteItems(binaryWriter);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
											Database.SessionLoad(ref Session.Data, Session.Data.Name);
											Session.Data.Special[worker] = new DisplayBoxData
											{
												Index = 0
											};
											Database.SessionSave(Session.Data);
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(11));
											binaryWriter.Write(Convert.ToUInt16(Session.GetTileX(worker)));
											binaryWriter.Write(Convert.ToUInt16(Session.GetTileY(worker)));
											binaryWriter.Write(Convert.ToUInt16(3));
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Server.Broadcast(input.ToArray(), delegate(Player player)
											{
												if (!player.Active)
												{
													return false;
												}
												if (!player.Profile.Active)
												{
													return false;
												}
												if (!player.Session.Active)
												{
													return false;
												}
												return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
											});
											binaryWriter.Close();
										}
										else
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, 0, "Message");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Display Box", 75, 3);
											Dialog.Text(binaryWriter, breaker: true, "~3Not enough inventory space.", 50);
											Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
									}
								}
							}
							if (Profile.Active && text35 == "Wrench.Smokehouse")
							{
								if (Session.Data.Foreground[worker] != 557)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else if (text36 == "Place")
								{
									int num130 = dictionary2["Index"];
									int.TryParse(dictionary["Count"], out var result18);
									SmokehouseData smokehouseData3 = Session.GetSmokehouseData(worker);
									if (smokehouseData3.Index != 0 || smokehouseData3.Count != 0)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, 0, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Smokehouse", 75, 557);
										Dialog.Text(binaryWriter, breaker: true, "Someone has already placed something", 50);
										Dialog.Text(binaryWriter, breaker: true, "inside, next time be faster.", 50);
										Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (result18 <= 0)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, 0, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Smokehouse", 75, 557);
										Dialog.Text(binaryWriter, breaker: true, "Negative amount won't work there.", 50);
										Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (!Item.Smokable(num130))
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, 0, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Smokehouse", 75, 557);
										Dialog.Text(binaryWriter, breaker: true, "This item doesn't seem to be able to be", 50);
										Dialog.Text(binaryWriter, breaker: true, "placed in a smokehouse.", 50);
										Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (Profile.GetItem(num130) < result18)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, 0, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Smokehouse", 75, 557);
										Dialog.Text(binaryWriter, breaker: true, "You can't place more items than you have.", 50);
										Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else
									{
										Profile.SetItem(num130, Profile.GetItem(num130) - result18);
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(6));
										Profile.WriteItems(binaryWriter);
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
										Database.SessionLoad(ref Session.Data, Session.Data.Name);
										Session.Data.Special[worker] = new SmokehouseData
										{
											Time = Item.Growtime(num130),
											Index = num130,
											Count = result18
										};
										Database.SessionSave(Session.Data);
										input = new MemoryStream();
										binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, 0, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Smokehouse", 75, 557);
										Dialog.Text(binaryWriter, breaker: true, $"~1{Item.Name(num130)} ~0has been placed in.", 50);
										Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
								else if (text36 == "Sell")
								{
									SmokehouseData data = Session.GetSmokehouseData(worker);
									if (data.Index == 0 || data.Count == 0)
									{
										PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
										{
											Dialog.ItemText(dialog, breaker: true, "~1Smokehouse", 75, 557);
											Dialog.Text(dialog, breaker: true, "Someone has already collected everything from", 50);
											Dialog.Text(dialog, breaker: true, "this smokehouse, next time be faster.", 50);
											Dialog.Button(dialog, breaker: true, "Okay", "Okay");
										});
									}
									else if (Item.Harvestable(data.Time))
									{
										gems = Item.Fish(data.Index) * data.Count;
										PlayerGems.Add(this, gems);
										Session.Data.Special[worker] = null;
										Database.SessionSave(Session.Data);
										PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
										{
											Dialog.ItemText(dialog, breaker: true, "~1Smokehouse", 75, 557);
											Dialog.Text(dialog, breaker: true, $"You have sold your ~1{Item.Name(data.Index)} ~0for ~1{Text.Delimit(gems)} ~0gems.", 50);
											Dialog.Button(dialog, breaker: true, "Okay", "Okay");
										});
									}
								}
							}
							if (Profile.Active && text35 == "Wrench.GameJoin")
							{
								if (Session.Data.Foreground[worker] != 597)
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(4));
									string text86 = "Somehow the game join has disappeared, find another one.";
									binaryWriter.Write(Encoding.UTF8.GetBytes(text86 + "\0"));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								else if (text36 == "Accept" && Session.TileAccess(Profile.Data.Filename, worker))
								{
									Database.SessionLoad(ref Session.Data, Session.Data.Name);
									GameJoinData gameJoinData2 = default(GameJoinData);
									int.TryParse(dictionary["Game"], out gameJoinData2.Game);
									int.TryParse(dictionary["Size"], out gameJoinData2.Size);
									int.TryParse(dictionary["Color"], out gameJoinData2.Color);
									if (gameJoinData2.Game < 0)
									{
										gameJoinData2.Game = 0;
									}
									if (gameJoinData2.Game > 100)
									{
										gameJoinData2.Game = 100;
									}
									if (gameJoinData2.Size < 0)
									{
										gameJoinData2.Size = 0;
									}
									if (gameJoinData2.Size > 100)
									{
										gameJoinData2.Size = 100;
									}
									if (gameJoinData2.Color < 0)
									{
										gameJoinData2.Color = 0;
									}
									if (gameJoinData2.Color > 100)
									{
										gameJoinData2.Color = 100;
									}
									Session.Data.Special[worker] = gameJoinData2;
									Database.SessionSave(Session.Data);
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(11));
									binaryWriter.Write(Convert.ToUInt16(Session.GetTileX(worker)));
									binaryWriter.Write(Convert.ToUInt16(Session.GetTileY(worker)));
									binaryWriter.Write(Convert.ToUInt16(3));
									binaryWriter.Write(Convert.ToUInt16(gameJoinData2.Color));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Server.Broadcast(input.ToArray(), delegate(Player player)
									{
										if (!player.Active)
										{
											return false;
										}
										if (!player.Profile.Active)
										{
											return false;
										}
										if (!player.Session.Active)
										{
											return false;
										}
										return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
									});
									binaryWriter.Close();
								}
							}
							if (Profile.Active && text35 == "Wrench.GameSpawn")
							{
								if (Session.Data.Foreground[worker] != 599)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else if (text36 == "Accept" && Session.TileAccess(Profile.Data.Filename, worker))
								{
									Database.SessionLoad(ref Session.Data, Session.Data.Name);
									GameSpawnData gameSpawnData2 = default(GameSpawnData);
									int.TryParse(dictionary["Game"], out gameSpawnData2.Game);
									int.TryParse(dictionary["Color"], out gameSpawnData2.Color);
									if (gameSpawnData2.Game < 0)
									{
										gameSpawnData2.Game = 0;
									}
									if (gameSpawnData2.Game > 100)
									{
										gameSpawnData2.Game = 100;
									}
									if (gameSpawnData2.Color < 0)
									{
										gameSpawnData2.Color = 0;
									}
									if (gameSpawnData2.Color > 100)
									{
										gameSpawnData2.Color = 100;
									}
									Session.Data.Special[worker] = gameSpawnData2;
									Database.SessionSave(Session.Data);
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(11));
									binaryWriter.Write(Convert.ToUInt16(Session.GetTileX(worker)));
									binaryWriter.Write(Convert.ToUInt16(Session.GetTileY(worker)));
									binaryWriter.Write(Convert.ToUInt16(3));
									binaryWriter.Write(Convert.ToUInt16(gameSpawnData2.Color));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Server.Broadcast(input.ToArray(), delegate(Player player)
									{
										if (!player.Active)
										{
											return false;
										}
										if (!player.Profile.Active)
										{
											return false;
										}
										if (!player.Session.Active)
										{
											return false;
										}
										return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
									});
									binaryWriter.Close();
								}
							}
							if (Profile.Active && text35 == "Wrench.GameFinish")
							{
								if (Session.Data.Foreground[worker] != 601)
								{
									PlayerConsole.Message(this, "The item you tried to wrench has disappeared.");
								}
								else if (text36 == "Accept" && Session.TileAccess(Profile.Data.Filename, worker))
								{
									Database.SessionLoad(ref Session.Data, Session.Data.Name);
									GameFinishData gameFinishData2 = default(GameFinishData);
									int.TryParse(dictionary["Game"], out gameFinishData2.Game);
									int.TryParse(dictionary["Color"], out gameFinishData2.Color);
									if (gameFinishData2.Game < 0)
									{
										gameFinishData2.Game = 0;
									}
									if (gameFinishData2.Game > 100)
									{
										gameFinishData2.Game = 100;
									}
									if (gameFinishData2.Color < 0)
									{
										gameFinishData2.Color = 0;
									}
									if (gameFinishData2.Color > 100)
									{
										gameFinishData2.Color = 100;
									}
									Session.Data.Special[worker] = gameFinishData2;
									Database.SessionSave(Session.Data);
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(11));
									binaryWriter.Write(Convert.ToUInt16(Session.GetTileX(worker)));
									binaryWriter.Write(Convert.ToUInt16(Session.GetTileY(worker)));
									binaryWriter.Write(Convert.ToUInt16(3));
									binaryWriter.Write(Convert.ToUInt16(gameFinishData2.Color));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Server.Broadcast(input.ToArray(), delegate(Player player)
									{
										if (!player.Active)
										{
											return false;
										}
										if (!player.Profile.Active)
										{
											return false;
										}
										if (!player.Session.Active)
										{
											return false;
										}
										return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
									});
									binaryWriter.Close();
								}
							}
							if (Profile.Active && text35 == "Quest.Item" && ushort.TryParse(text36, out var result19))
							{
								input = new MemoryStream();
								BinaryWriter binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(5));
								Window = Dialog.Create(binaryWriter, result19, "Quest.Item.Start");
								Dialog.ItemText(binaryWriter, breaker: true, "~1Start new item quest", 75, 3);
								Dialog.Text(binaryWriter, breaker: true, "Do you really want to start a new quest for item?", 50);
								Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
								Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								Send(input.ToArray());
								binaryWriter.Close();
							}
							if (Profile.Active && text35 == "Quest.Item.Start" && text36 == "Accept" && !Profile.Data.ItemQuest && ItemQuest.Quests[worker].Startable)
							{
								Profile.Data.ItemQuest = true;
								Profile.Data.ItemQuestType = worker;
								Profile.Data.ItemQuestStep = 0;
								ItemQuestData itemQuestData = ItemQuest.Quests[Profile.Data.ItemQuestType];
								ItemQuestStepData itemQuestStepData = itemQuestData.Steps[Profile.Data.ItemQuestStep];
								Profile.Data.ItemQuestLeft = itemQuestStepData.Count;
								PlayerQuests.ShowItemQuest(this);
							}
							if (Profile.Active && text35 == "Quest.Item.Step")
							{
								if (text36 == "Cancel" && Profile.Data.ItemQuest)
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Quest.Item.Cancel");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Give up on item quest", 75, 3);
									Dialog.Text(binaryWriter, breaker: true, "Do you really want to give up on your item quest?", 50);
									Dialog.Text(binaryWriter, breaker: true, "Your delivered or spent items won't be returned.", 50);
									Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
									Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								if (text36 == "Deliver" && Profile.Data.ItemQuest)
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Quest.Item.Deliver");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Deliver items for quest", 75, 3);
									Dialog.Text(binaryWriter, breaker: true, "Do you really want to deliver the items?", 50);
									Dialog.Text(binaryWriter, breaker: true, "Your delivered items can't be returned.", 50);
									Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
									Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								if (text36 == "Continue" && Profile.Data.ItemQuest && Profile.Data.ItemQuest && Profile.Data.ItemQuestLeft <= 0)
								{
									if (Profile.Data.ItemQuestStep + 1 >= ItemQuest.Quests[Profile.Data.ItemQuestType].Steps.Length)
									{
										int rewardIndex = ItemQuest.Quests[Profile.Data.ItemQuestType].RewardIndex;
										int rewardCount = ItemQuest.Quests[Profile.Data.ItemQuestType].RewardCount;
										if (Profile.CanGetItem(rewardIndex, rewardCount))
										{
											Profile.Data.ItemQuest = false;
											Profile.Data.ItemQuestType = 0;
											Profile.Data.ItemQuestStep = 0;
											Profile.Data.ItemQuestLeft = 0;
											PlayerLayout.Warning(this, 200, 2, "~1QUEST ~4COMPLETE~1!", "Congratulations, you've completed your quest.", "Your reward was delivered.");
											Profile.SetItem(rewardIndex, Profile.GetItem(rewardIndex) + rewardCount);
											PlayerCore.UpdateInventory(this);
											Server.SendLog(Profile.Data.Filename, $"Finished item quest and got x{rewardCount} {Item.Name(rewardIndex)}");
										}
										else
										{
											PlayerConsole.Message(this, "Not enough inventory space for prize.");
										}
									}
									else
									{
										Profile.Data.ItemQuestStep++;
										ItemQuestData itemQuestData2 = ItemQuest.Quests[Profile.Data.ItemQuestType];
										ItemQuestStepData itemQuestStepData2 = itemQuestData2.Steps[Profile.Data.ItemQuestStep];
										Profile.Data.ItemQuestLeft = itemQuestStepData2.Count;
										PlayerQuests.ShowItemQuest(this);
									}
								}
							}
							if (Profile.Active && text35 == "Quest.Item.Cancel")
							{
								if (text36 == "Cancel" && Profile.Data.ItemQuest)
								{
									PlayerQuests.ShowItemQuest(this);
								}
								if (text36 == "Accept" && Profile.Data.ItemQuest)
								{
									Profile.Data.ItemQuest = false;
									Profile.Data.ItemQuestType = 0;
									Profile.Data.ItemQuestStep = 0;
									Profile.Data.ItemQuestLeft = 0;
									PlayerConsole.Message(this, "Sad to see you go, your quest has been cancelled.");
								}
							}
							if (Profile.Active && text35 == "Quest.Item.Deliver")
							{
								if (text36 == "Cancel" && Profile.Data.ItemQuest)
								{
									PlayerQuests.ShowItemQuest(this);
								}
								if (text36 == "Accept" && Profile.Data.ItemQuest)
								{
									ItemQuestData itemQuestData3 = ItemQuest.Quests[Profile.Data.ItemQuestType];
									ItemQuestStepData itemQuestStepData3 = itemQuestData3.Steps[Profile.Data.ItemQuestStep];
									if (itemQuestStepData3.Event == PlayerEvent.Deliver && itemQuestStepData3.Index == 0)
									{
										if (PlayerGems.Has(this, itemQuestStepData3.Count))
										{
											PlayerGems.Remove(this, itemQuestStepData3.Count);
											PlayerQuests.Event(this, PlayerEvent.Deliver, itemQuestStepData3.Index, itemQuestStepData3.Count);
											PlayerQuests.ShowItemQuest(this);
										}
										else
										{
											PlayerConsole.Message(this, "You don't have enough gems to deliver.");
										}
									}
									if (itemQuestStepData3.Event == PlayerEvent.Deliver && itemQuestStepData3.Index != 0)
									{
										if (Profile.HasItem(itemQuestStepData3.Index, itemQuestStepData3.Count))
										{
											Profile.SetItem(itemQuestStepData3.Index, Profile.GetItem(itemQuestStepData3.Index) - itemQuestStepData3.Count);
											PlayerQuests.Event(this, PlayerEvent.Deliver, itemQuestStepData3.Index, itemQuestStepData3.Count);
											PlayerCore.UpdateInventory(this);
											PlayerQuests.ShowItemQuest(this);
										}
										else
										{
											PlayerConsole.Message(this, "You don't have enough items to deliver.");
										}
									}
								}
							}
						}
						else
						{
							Window = string.Empty;
						}
						dictionary.Clear();
						dictionary2.Clear();
						break;
					}
					case 6:
					{
						bool flag17 = binaryReader.ReadBoolean();
						ushort num164 = binaryReader.ReadUInt16();
						binaryReader.Close();
						if (!Profile.Active || Profile.Slot >= Profile.Data.ItemIndex.Count)
						{
							break;
						}
						int num165 = Profile.Data.ItemIndex[num164];
						int num166 = Profile.Data.ItemCount[num164];
						int num167 = Profile.Data.ItemEquip[num164];
						if (flag17)
						{
							BinaryWriter binaryWriter;
							if (Trading)
							{
								Player interact18 = GetInteract(TradeID);
								if (interact18.Trading && interact18.TradeID == Identifier && interact18.TradeAccepted && TradeAccepted)
								{
									PlayerConsole.Message(this, "You're not allowed to change your offer during review.");
									break;
								}
								input = new MemoryStream();
								binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(5));
								Window = Dialog.Create(binaryWriter, num164, "Trading.Item");
								Dialog.ItemText(binaryWriter, breaker: true, $"Add ~1{Item.Name(num165)}", 75, num165);
								Dialog.Text(binaryWriter, breaker: true, "How many would you like to add?", 50);
								Dialog.Textbox(binaryWriter, breaker: true, "Count", "", 8);
								Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
								Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								Send(input.ToArray());
								binaryWriter.Close();
								break;
							}
							input = new MemoryStream();
							binaryWriter = new BinaryWriter(input);
							binaryWriter.Write(Convert.ToUInt16(0));
							binaryWriter.Write(Convert.ToUInt16(5));
							Window = Dialog.Create(binaryWriter, num164, "Item");
							Dialog.ItemText(binaryWriter, breaker: true, $"~1{Item.Name(num165)}", 75, num165);
							Dialog.Text(binaryWriter, breaker: true, "Rarity: ~1" + Item.Rarity(num165), 50);
							if (Item.Exchange(num165).Amount > 0)
							{
								Dialog.Button(binaryWriter, breaker: true, "Exchange", "Exchange");
							}
							if (Rewards.Permission(Profile.Data.Filename, Permissions.Dev))
							{
								Dialog.Button(binaryWriter, breaker: true, "Edit", "Edit");
							}
							if (Item.Type(num165) == 4)
							{
								if (num167 == 0)
								{
									Dialog.Button(binaryWriter, breaker: true, "Wear", "Wear");
								}
								else
								{
									Dialog.Button(binaryWriter, breaker: true, "Wear", "Unwear");
								}
							}
							if (Item.Type(num165) == 5)
							{
								Dialog.Button(binaryWriter, breaker: true, "Consume", "Consume");
							}
							if (Item.Droppable(num165))
							{
								Dialog.Button(binaryWriter, breaker: true, "Drop", "Drop");
							}
							if (Item.Trashable(num165))
							{
								Dialog.Button(binaryWriter, breaker: true, "Trash", "Trash");
							}
							if (Item.Info(num165) != string.Empty)
							{
								Dialog.Button(binaryWriter, breaker: true, "Info", "Info");
							}
							Dialog.Button(binaryWriter, breaker: true, "Cancel", "Cancel");
							binaryWriter.Seek(0, SeekOrigin.Begin);
							binaryWriter.Write(Convert.ToUInt16(input.Length));
							Send(input.ToArray());
							binaryWriter.Close();
						}
						else
						{
							PlayerLayout.Notification(this, 100, num165, Item.Name(num165));
						}
						break;
					}
					case 7:
						binaryReader.Close();
						break;
					case 8:
						binaryReader.Close();
						break;
					case 9:
						binaryReader.Close();
						if (!Profile.Active || !Session.Active)
						{
							break;
						}
						PlayerCore.UpdateDialog(this, 0, "Session", delegate(BinaryWriter dialog)
						{
							Dialog.ItemText(dialog, breaker: true, $"~1World {Session.Data.Name}", 75, 3);
							Dialog.Button(dialog, breaker: true, "Worlds", "World menu");
							Dialog.Button(dialog, breaker: true, "Respawn", "Respawn");
							Dialog.Button(dialog, breaker: true, "Options", "Options");
							Dialog.Button(dialog, breaker: true, "Shop", "Shop");
							Dialog.Button(dialog, breaker: true, "Server", "Switch server");
							if (Rewards.Permission(Profile.Data.Filename, Permissions.Mod))
							{
								Dialog.Button(dialog, breaker: true, "Moderator", "Staff tools");
							}
							else
							{
								Dialog.Button(dialog, breaker: true, "Report", "Report");
							}
							Dialog.Button(dialog, breaker: true, "Cancel", "Cancel");
						});
						break;
					case 10:
					{
						binaryReader.Close();
						if (!Profile.Active || !Session.Active)
						{
							break;
						}
						input = new MemoryStream();
						BinaryWriter binaryWriter = new BinaryWriter(input);
						binaryWriter.Write(Convert.ToUInt16(0));
						binaryWriter.Write(Convert.ToUInt16(10));
						Session.WriteTiles(binaryWriter, Profile.Data.Filename);
						binaryWriter.Seek(0, SeekOrigin.Begin);
						binaryWriter.Write(Convert.ToUInt16(input.Length));
						Send(input.ToArray());
						binaryWriter.Close();
						input = new MemoryStream();
						binaryWriter = new BinaryWriter(input);
						binaryWriter.Write(Convert.ToUInt16(0));
						binaryWriter.Write(Convert.ToUInt16(28));
						Session.WriteMusic(binaryWriter);
						binaryWriter.Seek(0, SeekOrigin.Begin);
						binaryWriter.Write(Convert.ToUInt16(input.Length));
						Send(input.ToArray());
						binaryWriter.Close();
						input = new MemoryStream();
						binaryWriter = new BinaryWriter(input);
						binaryWriter.Write(Convert.ToUInt16(0));
						binaryWriter.Write(Convert.ToUInt16(20));
						binaryWriter.Write(Convert.ToUInt16(Session.Data.Theme));
						binaryWriter.Seek(0, SeekOrigin.Begin);
						binaryWriter.Write(Convert.ToUInt16(input.Length));
						Send(input.ToArray());
						binaryWriter.Close();
						foreach (DroppedItem item5 in Session.Data.Drop)
						{
							input = new MemoryStream();
							binaryWriter = new BinaryWriter(input);
							binaryWriter.Write(Convert.ToUInt16(0));
							binaryWriter.Write(Convert.ToUInt16(12));
							binaryWriter.Write(Convert.ToBoolean(0));
							binaryWriter.Write(Convert.ToUInt16(item5.Index));
							binaryWriter.Write(Convert.ToUInt16(item5.Count));
							binaryWriter.Write(Convert.ToUInt16(item5.X));
							binaryWriter.Write(Convert.ToUInt16(item5.Y));
							binaryWriter.Seek(0, SeekOrigin.Begin);
							binaryWriter.Write(Convert.ToUInt16(input.Length));
							Send(input.ToArray());
							binaryWriter.Close();
						}
						int door = Session.GetDoor(Session.Door);
						Session.Door = string.Empty;
						CurrentX = Session.GetTileX(door) * 32 + 8;
						CurrentY = Session.GetTileY(door) * 32;
						if (Session.Teleport != 0)
						{
							Player[] array63 = Server.Online.ToArray();
							foreach (Player player58 in array63)
							{
								if (player58.Active && player58.Profile.Active && player58.Session.Active && player58.Identifier == Session.Teleport && !(player58.Session.Data.Name != Session.Data.Name))
								{
									CurrentX = player58.CurrentX;
									CurrentY = player58.CurrentY;
								}
							}
							Session.Teleport = 0;
						}
						PreviousX = CurrentX;
						PreviousY = CurrentY;
						input = new MemoryStream();
						binaryWriter = new BinaryWriter(input);
						binaryWriter.Write(Convert.ToUInt16(0));
						binaryWriter.Write(Convert.ToUInt16(13));
						binaryWriter.Write(Convert.ToInt32(0));
						binaryWriter.Write(Convert.ToBoolean(value: false));
						binaryWriter.Write(Convert.ToUInt16(CurrentX));
						binaryWriter.Write(Convert.ToUInt16(CurrentY));
						binaryWriter.Seek(0, SeekOrigin.Begin);
						binaryWriter.Write(Convert.ToUInt16(input.Length));
						Send(input.ToArray());
						binaryWriter.Close();
						input = new MemoryStream();
						binaryWriter = new BinaryWriter(input);
						binaryWriter.Write(Convert.ToUInt16(0));
						binaryWriter.Write(Convert.ToUInt16(13));
						binaryWriter.Write(Convert.ToInt32(Identifier));
						binaryWriter.Write(Convert.ToBoolean(value: false));
						binaryWriter.Write(Convert.ToUInt16(CurrentX));
						binaryWriter.Write(Convert.ToUInt16(CurrentY));
						binaryWriter.Seek(0, SeekOrigin.Begin);
						binaryWriter.Write(Convert.ToUInt16(input.Length));
						Server.Broadcast(input.ToArray(), delegate(Player player)
						{
							if (player == this)
							{
								return false;
							}
							if (!player.Active)
							{
								return false;
							}
							if (!player.Profile.Active)
							{
								return false;
							}
							if (!player.Session.Active)
							{
								return false;
							}
							return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
						});
						binaryWriter.Close();
						input = new MemoryStream();
						binaryWriter = new BinaryWriter(input);
						binaryWriter.Write(Convert.ToUInt16(0));
						binaryWriter.Write(Convert.ToUInt16(14));
						binaryWriter.Write(Convert.ToInt32(0));
						Profile.WriteData(binaryWriter, this);
						binaryWriter.Seek(0, SeekOrigin.Begin);
						binaryWriter.Write(Convert.ToUInt16(input.Length));
						Send(input.ToArray());
						binaryWriter.Close();
						Player[] array64 = Server.Online.ToArray();
						foreach (Player player59 in array64)
						{
							if (player59 != this && player59.Active && player59.Profile.Active && player59.Session.Active && !(player59.Session.Data.Name != Session.Data.Name))
							{
								input = new MemoryStream();
								binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(14));
								binaryWriter.Write(Convert.ToInt32(Identifier));
								Profile.WriteData(binaryWriter, player59);
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								player59.Send(input.ToArray());
								binaryWriter.Close();
							}
						}
						Player[] array65 = Server.Online.ToArray();
						foreach (Player player60 in array65)
						{
							if (player60 != this && player60.Active && player60.Profile.Active && player60.Session.Active && !(player60.Session.Data.Name != Session.Data.Name))
							{
								input = new MemoryStream();
								binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(13));
								binaryWriter.Write(Convert.ToInt32(player60.Identifier));
								binaryWriter.Write(Convert.ToBoolean(value: false));
								binaryWriter.Write(Convert.ToUInt16(player60.CurrentX));
								binaryWriter.Write(Convert.ToUInt16(player60.CurrentY));
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								Send(input.ToArray());
								binaryWriter.Close();
								input = new MemoryStream();
								binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(14));
								binaryWriter.Write(Convert.ToInt32(player60.Identifier));
								player60.Profile.WriteData(binaryWriter, this);
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								Send(input.ToArray());
								binaryWriter.Close();
							}
						}
						Player[] array66 = Server.Online.ToArray();
						foreach (Player player61 in array66)
						{
							if (player61.Active && player61.Profile.Active && player61.Session.Active && !(player61.Session.Data.Name != Session.Data.Name) && Profile.VisibleTo(player61))
							{
								input = new MemoryStream();
								binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(4));
								string text104 = $"~0[~1{Profile.Data.Username} ~0has entered the world]";
								binaryWriter.Write(Encoding.UTF8.GetBytes(text104 + "\0"));
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								player61.Send(input.ToArray());
								binaryWriter.Close();
							}
						}
						Profile.Data.Session = Session.Data.Name;
						Loaded = true;
						break;
					}
					case 11:
					{
						ushort num5 = binaryReader.ReadUInt16();
						ushort num6 = binaryReader.ReadUInt16();
						ushort num7 = binaryReader.ReadUInt16();
						ushort num8 = binaryReader.ReadUInt16();
						binaryReader.Close();
						if (!Profile.Active || !Session.Active)
						{
							break;
						}
						int num9 = (int)num5 / 32;
						int num10 = (int)num6 / 32;
						Database.SessionLoad(ref Session.Data, Session.Data.Filename);
						if (!Loaded || LastPunched + 200 > DateTime.UtcNow.Ticks / 10000 || num9 < 0 || num10 < 0 || num9 >= Session.Data.SizeX || num10 >= Session.Data.SizeY)
						{
							break;
						}
						LastPunched = DateTime.UtcNow.Ticks / 10000;
						PlayerCore.UpdateAnimation(this, 0);
						Player player2 = null;
						Player[] array = Server.Online.ToArray();
						foreach (Player player3 in array)
						{
							if (player3.Active && player3.Profile.Active && player3.Session.Active && !(player3.Session.Data.Name != Session.Data.Name) && player3.Profile.VisibleTo(this))
							{
								if ((player3.CurrentX + 8) / 32 == num9 && (player3.CurrentY + 16) / 32 == num10)
								{
									player2 = player3;
								}
								if (player2 == this)
								{
									break;
								}
							}
						}
						WrenchID = SetInteract(player2);
						if (Fishing)
						{
							if (FishingDone)
							{
								FishingDone = false;
								Fishing = false;
								input = new MemoryStream();
								BinaryWriter binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(31));
								binaryWriter.Write(Convert.ToInt32(0));
								binaryWriter.Write(Convert.ToBoolean(value: false));
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								Send(input.ToArray());
								binaryWriter.Close();
								input = new MemoryStream();
								binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(31));
								binaryWriter.Write(Convert.ToInt32(Identifier));
								binaryWriter.Write(Convert.ToBoolean(0));
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								Server.Broadcast(input.ToArray(), delegate(Player player)
								{
									if (player == this)
									{
										return false;
									}
									if (!player.Active)
									{
										return false;
									}
									if (!player.Profile.Active)
									{
										return false;
									}
									if (!player.Session.Active)
									{
										return false;
									}
									return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
								});
								binaryWriter.Close();
								RewardItem rewardItem = default(RewardItem);
								if (FishingBait == 1)
								{
									rewardItem = Rewards.Fishing1[Server.Random.Next(Rewards.Fishing1.Count)];
								}
								if (FishingBait == 2)
								{
									rewardItem = Rewards.Fishing2[Server.Random.Next(Rewards.Fishing2.Count)];
								}
								if (FishingBait == 3)
								{
									rewardItem = Rewards.Fishing3[Server.Random.Next(Rewards.Fishing3.Count)];
								}
								if (rewardItem.Index != 0 && Server.Random.Next(rewardItem.Priority) == 0)
								{
									if (Profile.CanGetItem(rewardItem.Index, rewardItem.Count))
									{
										Profile.Data.QuestLeft -= Quest.CatchFish(Profile.Data.QuestType, Profile.Data.QuestItem, rewardItem.Index);
										Challenge.CatchFish(Profile.Data.Filename, rewardItem.Index);
										Achievement(AchievementType.CatchFish, increase: true, 1);
										PlayerQuests.Event(this, PlayerEvent.Fish, rewardItem.Index, rewardItem.Count);
										Profile.SetItem(rewardItem.Index, Profile.GetItem(rewardItem.Index) + rewardItem.Count);
										PlayerConsole.Message(this, "You got ~1{0}~0.", Item.Name(rewardItem.Index));
										input = new MemoryStream();
										binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(6));
										Profile.WriteItems(binaryWriter);
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else
									{
										PlayerConsole.Message(this, "You caught a fish, but you have no space for it, so it swims away.");
									}
									Tournament.Gain(this, TournamentType.Fishing);
								}
								else
								{
									PlayerConsole.Message(this, "This time you didn't catch anything, maybe next time you'll succeed?");
								}
								Experience(randomize: true, 100);
							}
							else if (FishingTries > 3)
							{
								PlayerConsole.Message(this, "Too much noise, you'll need another bait. Be patient next time.");
								Fishing = false;
								input = new MemoryStream();
								BinaryWriter binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(31));
								binaryWriter.Write(Convert.ToInt32(0));
								binaryWriter.Write(Convert.ToBoolean(value: false));
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								Send(input.ToArray());
								binaryWriter.Close();
								input = new MemoryStream();
								binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(31));
								binaryWriter.Write(Convert.ToInt32(Identifier));
								binaryWriter.Write(Convert.ToBoolean(0));
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								Server.Broadcast(input.ToArray(), delegate(Player player)
								{
									if (player == this)
									{
										return false;
									}
									if (!player.Active)
									{
										return false;
									}
									if (!player.Profile.Active)
									{
										return false;
									}
									if (!player.Session.Active)
									{
										return false;
									}
									return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
								});
								binaryWriter.Close();
							}
							else
							{
								PlayerConsole.Message(this, "Please be patient, stand still, you just threw the fishing rod. You'll be notified when you catch something.");
								FishingTries++;
							}
						}
						else if (Profile.HasItem(num7, 1))
						{
							if (player2 != null && num7 == 1)
							{
								LastPunched = DateTime.UtcNow.Ticks / 10000;
								int sizeX = Session.Data.SizeX;
								int sizeY = Session.Data.SizeY;
								int num11 = num9 + num10 * sizeX;
								if (Item.PunchEffect(Profile.GetPartItem(7)).ID != 0)
								{
									PunchEffect punchEffect = Item.PunchEffect(Profile.GetPartItem(7));
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(30));
									binaryWriter.Write(Convert.ToUInt16(punchEffect.ID));
									binaryWriter.Write(Convert.ToUInt16(CurrentX + 8 + punchEffect.X));
									binaryWriter.Write(Convert.ToUInt16(CurrentY + 8 + punchEffect.Y));
									binaryWriter.Write(Convert.ToUInt16(num9 * 32 + 16));
									binaryWriter.Write(Convert.ToUInt16(num10 * 32 + 16));
									binaryWriter.Write(Convert.ToUInt16(punchEffect.Data1));
									binaryWriter.Write(Convert.ToUInt16(punchEffect.Data2));
									binaryWriter.Write(Convert.ToUInt16(punchEffect.Data3));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Server.Broadcast(input.ToArray(), delegate(Player player)
									{
										if (player == this)
										{
											return true;
										}
										if (!player.Active)
										{
											return false;
										}
										if (!player.Profile.Active)
										{
											return false;
										}
										if (!player.Session.Active)
										{
											return false;
										}
										if (player.Session.Data.Name != Session.Data.Name)
										{
											return false;
										}
										return Profile.VisibleTo(player) ? true : false;
									});
									binaryWriter.Close();
								}
								if (player2 == this)
								{
									if (Session.Data.Foreground[num11] == 7)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, 0, "Session.Worlds");
										Dialog.ItemText(binaryWriter, breaker: true, "~1World menu", 75, 3);
										Dialog.Text(binaryWriter, breaker: true, "Where would you like to go?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "World", "", 32);
										Dialog.Text(binaryWriter, breaker: true, "World name must be 1-32 characters long, only A-Z and 0-9 characters are allowed.", 25);
										Dialog.Button(binaryWriter, breaker: false, "Warp", "Warp");
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (Session.Data.Foreground[num11] == 71 || Session.Data.Foreground[num11] == 203 || Session.Data.Foreground[num11] == 511)
									{
										Enter(num11, null);
									}
								}
								if (player2 != this)
								{
									int punchStrength = Profile.GetPunchStrength();
									if (!player2.Profile.Noclip && !Session.HasAntiPunch())
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(18));
										binaryWriter.Write(Convert.ToUInt16(CurrentX + 8));
										binaryWriter.Write(Convert.ToUInt16(CurrentY + 16));
										binaryWriter.Write(Convert.ToInt32(punchStrength));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										player2.Send(input.ToArray());
										binaryWriter.Close();
									}
								}
							}
							else if (player2 == null && num7 == 1)
							{
								int sizeX2 = Session.Data.SizeX;
								int sizeY2 = Session.Data.SizeY;
								int num12 = num9 + num10 * sizeX2;
								if (Item.PunchEffect(Profile.GetPartItem(7)).ID != 0)
								{
									PunchEffect punchEffect2 = Item.PunchEffect(Profile.GetPartItem(7));
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(30));
									binaryWriter.Write(Convert.ToUInt16(punchEffect2.ID));
									binaryWriter.Write(Convert.ToUInt16(CurrentX + 8 + punchEffect2.X));
									binaryWriter.Write(Convert.ToUInt16(CurrentY + 8 + punchEffect2.Y));
									binaryWriter.Write(Convert.ToUInt16(num9 * 32 + 16));
									binaryWriter.Write(Convert.ToUInt16(num10 * 32 + 16));
									binaryWriter.Write(Convert.ToUInt16(punchEffect2.Data1));
									binaryWriter.Write(Convert.ToUInt16(punchEffect2.Data2));
									binaryWriter.Write(Convert.ToUInt16(punchEffect2.Data3));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Server.Broadcast(input.ToArray(), delegate(Player player)
									{
										if (player == this)
										{
											return true;
										}
										if (!player.Active)
										{
											return false;
										}
										if (!player.Profile.Active)
										{
											return false;
										}
										if (!player.Session.Active)
										{
											return false;
										}
										if (player.Session.Data.Name != Session.Data.Name)
										{
											return false;
										}
										return Profile.VisibleTo(player) ? true : false;
									});
									binaryWriter.Close();
								}
								if (num8 == 0)
								{
									if (Session.Data.Foreground[num12] == 157)
									{
										ProviderData providerData = Session.GetProviderData(num12);
										if (Session.TileAccess(Profile.Data.Filename, num12) && Item.Harvestable(providerData.Time))
										{
											PlayerGems.Add(this, Server.Random.Next(1, 10));
											Session.Data.Special[num12] = new ProviderData
											{
												Time = Item.Growtime(Session.Data.Foreground[num12])
											};
											Experience(randomize: true, 32);
											PlayerQuests.Event(this, PlayerEvent.GemMachine);
											Database.ProfileSave(Profile.Data);
											Database.SessionSave(Session.Data);
										}
									}
									if (Session.Data.Foreground[num12] == 427)
									{
										int num13 = Server.Random.Next(6);
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(11));
										binaryWriter.Write(Convert.ToUInt16(Session.GetTileX(num12)));
										binaryWriter.Write(Convert.ToUInt16(Session.GetTileY(num12)));
										binaryWriter.Write(Convert.ToUInt16(3));
										binaryWriter.Write(Convert.ToUInt16(num13));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Server.Broadcast(input.ToArray(), delegate(Player player)
										{
											if (!player.Active)
											{
												return false;
											}
											if (!player.Profile.Active)
											{
												return false;
											}
											if (!player.Session.Active)
											{
												return false;
											}
											return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
										});
										binaryWriter.Close();
										input = new MemoryStream();
										binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										string text = $"~0[~1{Profile.Data.Username} ~0has got ~1{num13 + 1} ~0from a dice block]";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Server.Broadcast(input.ToArray(), delegate(Player player)
										{
											if (!player.Active)
											{
												return false;
											}
											if (!player.Profile.Active)
											{
												return false;
											}
											if (!player.Session.Active)
											{
												return false;
											}
											return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
										});
										binaryWriter.Close();
									}
									if (Session.Data.Foreground[num12] == 559)
									{
										ProviderData providerData2 = Session.GetProviderData(num12);
										if (Session.TileAccess(Profile.Data.Filename, num12) && Item.Harvestable(providerData2.Time))
										{
											RewardItem rewardItem2 = Rewards.Random(Rewards.Bait);
											int index = rewardItem2.Index;
											int count = rewardItem2.Count;
											int x = num9 * 32 + 6;
											int y = num10 * 32 + 6;
											if (Session.Data.Drop.Count >= 512)
											{
												PlayerConsole.Message(this, "~3There are too many dropped items in this world, pick some up and try again.");
											}
											else if (index != 0 && count != 0)
											{
												input = new MemoryStream();
												BinaryWriter binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(12));
												Session.Drop(binaryWriter, index, count, x, y);
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Server.Broadcast(input.ToArray(), delegate(Player player)
												{
													if (!player.Active)
													{
														return false;
													}
													if (!player.Profile.Active)
													{
														return false;
													}
													if (!player.Session.Active)
													{
														return false;
													}
													return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
												});
												binaryWriter.Close();
												Session.Data.Special[num12] = new ProviderData
												{
													Time = Item.Growtime(Session.Data.Foreground[num12])
												};
												Experience(randomize: true, 50);
												PlayerQuests.Event(this, PlayerEvent.BaitBox, index);
												Database.ProfileSave(Profile.Data);
												Database.SessionSave(Session.Data);
											}
										}
									}
									if (Session.Data.Foreground[num12] == 889 || Session.Data.Foreground[num12] == 891 || Session.Data.Foreground[num12] == 893)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(30));
										binaryWriter.Write(Convert.ToUInt16(1));
										binaryWriter.Write(Convert.ToUInt16(num9 * 32 + 16));
										binaryWriter.Write(Convert.ToUInt16(num10 * 32 + 16));
										int val = num9 * 32 + 16 + Server.Random.Next(-96, 96);
										int val2 = num10 * 32 + 16 - Server.Random.Next(96, 160);
										val = Math.Max(0, Math.Min(val, Session.Data.SizeX * 32));
										val2 = Math.Max(0, Math.Min(val2, Session.Data.SizeY * 32));
										binaryWriter.Write(Convert.ToUInt16(val));
										binaryWriter.Write(Convert.ToUInt16(val2));
										if (Session.Data.Foreground[num12] == 889)
										{
											binaryWriter.Write(Convert.ToUInt16(1));
										}
										if (Session.Data.Foreground[num12] == 891)
										{
											binaryWriter.Write(Convert.ToUInt16(2));
										}
										if (Session.Data.Foreground[num12] == 893)
										{
											binaryWriter.Write(Convert.ToUInt16(3));
										}
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Server.Broadcast(input.ToArray(), delegate(Player player)
										{
											if (!player.Active)
											{
												return false;
											}
											if (!player.Profile.Active)
											{
												return false;
											}
											if (!player.Session.Active)
											{
												return false;
											}
											return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
										});
										binaryWriter.Close();
									}
									if (Session.Data.Foreground[num12] == 899 && Session.TileAccess(Profile.Data.Filename, num12))
									{
										ChestData chestData = Session.GetChestData(num12);
										chestData.Open = ((!chestData.Open) ? true : false);
										Session.Data.Special[num12] = chestData;
										Database.SessionSave(Session.Data);
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(11));
										binaryWriter.Write(Convert.ToUInt16(Session.GetTileX(num12)));
										binaryWriter.Write(Convert.ToUInt16(Session.GetTileY(num12)));
										binaryWriter.Write(Convert.ToUInt16(3));
										binaryWriter.Write(Convert.ToUInt16(chestData.Open));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Server.Broadcast(input.ToArray(), delegate(Player player)
										{
											if (!player.Active)
											{
												return false;
											}
											if (!player.Profile.Active)
											{
												return false;
											}
											if (!player.Session.Active)
											{
												return false;
											}
											return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
										});
										binaryWriter.Close();
									}
									if (Session.Data.Foreground[num12] == 1143)
									{
										data3 = Session.GetHalloweenEnemyData(num12);
										if (data3.Candies == 0)
										{
											PlayerCore.UpdateDialog(this, num12, "Event.Halloween.Enemy", delegate(BinaryWriter dialog)
											{
												Dialog.ItemText(dialog, breaker: true, "~1Candy request", 75, 1143);
												Dialog.Text(dialog, breaker: true, "Hi, I don't want a candy this time, do you", 50);
												Dialog.Text(dialog, breaker: true, "want me to take you to your destination?", 50);
												Dialog.Button(dialog, breaker: false, "Accept", "Accept");
												Dialog.Button(dialog, breaker: false, "Cancel", "Cancel");
											});
										}
										if (data3.Candies != 0)
										{
											PlayerCore.UpdateDialog(this, num12, "Event.Halloween.Enemy", delegate(BinaryWriter dialog)
											{
												Dialog.ItemText(dialog, breaker: true, "~1Candy request", 75, 1143);
												Dialog.Text(dialog, breaker: true, "Hi, I'd like to have a candy. Would", 50);
												Dialog.Text(dialog, breaker: true, $"you mind feeding me {data3.Candies}?", 50);
												Dialog.Button(dialog, breaker: false, "Accept", "Feed");
												Dialog.Button(dialog, breaker: false, "Refuse", "Refuse");
												Dialog.Button(dialog, breaker: false, "Cancel", "Cancel");
											});
										}
									}
								}
								if (!Session.TileAccess(Profile.Data.Filename, num12))
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(17));
									binaryWriter.Write(Convert.ToUInt16(100));
									binaryWriter.Write(Convert.ToUInt16(73));
									if (Session.Data.Owner.Length > 0)
									{
										string text2 = $"This world is locked by ~1{Session.Data.Owner}~0.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text2 + "\0"));
									}
									else if (Session.Data.Parent[num12] != null)
									{
										Parent parent = (Parent)Session.Data.Parent[num12];
										string text3 = $"This area is locked by ~1{Session.GetLockData(parent.X + parent.Y * Session.Data.SizeX).Owner}~0.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text3 + "\0"));
									}
									else
									{
										string text4 = "This area is locked.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text4 + "\0"));
									}
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								else if (Session.Data.Foreground[num12] == 5 && !Rewards.Permission(Profile.Data.Filename, Permissions.Admin))
								{
									PlayerConsole.Message(this, "Bedrock is way too strong to be broken.");
								}
								else if (Session.Data.Foreground[num12] == 7 && !Rewards.Permission(Profile.Data.Filename, Permissions.Admin))
								{
									PlayerConsole.Message(this, "World entrance is way too strong to be broken.");
								}
								else if (Item.Type(Session.Data.Foreground[num12]) == 3)
								{
									if (Item.Harvestable(Session.GetSeedData(num12).Time))
									{
										Profile.Data.QuestLeft -= Quest.BreakSeed(Profile.Data.QuestType, Profile.Data.QuestItem, Session.Data.Foreground[num12]);
										Challenge.BreakSeed(Profile.Data.Filename, Session.Data.Foreground[num12]);
										Achievement(AchievementType.HarvestSeed, increase: true, 1);
										PlayerQuests.Event(this, PlayerEvent.Harvest, Session.Data.Foreground[num12]);
										BinaryWriter binaryWriter;
										if (Item.Farmability(Session.Data.Foreground[num12]) != 0)
										{
											int num14 = Item.SeedToItem(Session.Data.Foreground[num12]);
											int num15 = Server.Random.Next(5) + 1;
											if (!Session.CanDrop())
											{
												PlayerConsole.Message(this, "There are too many dropped items in this world, so nothing falls out.");
											}
											else if (num14 != 0 && num15 != 0)
											{
												input = new MemoryStream();
												binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(12));
												Session.Drop(binaryWriter, num14, num15, num9 * 32 + 6, num10 * 32 + 6);
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Server.Broadcast(input.ToArray(), delegate(Player player)
												{
													if (!player.Active)
													{
														return false;
													}
													if (!player.Profile.Active)
													{
														return false;
													}
													if (!player.Session.Active)
													{
														return false;
													}
													return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
												});
												binaryWriter.Close();
											}
											num14 = Item.BreakExtra(Session.Data.Foreground[num12], this);
											num15 = 1;
											if (Session.CanDrop() && num14 != 0 && num15 != 0)
											{
												input = new MemoryStream();
												binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(12));
												Session.Drop(binaryWriter, num14, num15, num9 * 32 + 6, num10 * 32 + 6);
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Server.Broadcast(input.ToArray(), delegate(Player player)
												{
													if (!player.Active)
													{
														return false;
													}
													if (!player.Profile.Active)
													{
														return false;
													}
													if (!player.Session.Active)
													{
														return false;
													}
													return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
												});
												binaryWriter.Close();
											}
										}
										int num16 = Server.Random.Next(Item.SeedGems(Session.Data.Foreground[num12]));
										int currency = Server.Random.Next(Item.SeedCurrency(Session.Data.Foreground[num12]));
										if (num16 > 0)
										{
											PlayerGems.Add(this, num16);
											if (Event.Type == EventType.Space && DateTime.UtcNow > Event.AvailableFrom && DateTime.UtcNow < Event.AvailableTo)
											{
												PlayerSpecialCurrency.Add(this, currency);
											}
										}
										Session.Data.Foreground[num12] = 0;
										Session.Data.Special[num12] = null;
										Database.ProfileSave(Profile.Data);
										Database.SessionSave(Session.Data);
										input = new MemoryStream();
										binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(11));
										binaryWriter.Write(Convert.ToUInt16(num9));
										binaryWriter.Write(Convert.ToUInt16(num10));
										binaryWriter.Write(Convert.ToUInt16(2));
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Server.Broadcast(input.ToArray(), delegate(Player player)
										{
											if (!player.Active)
											{
												return false;
											}
											if (!player.Profile.Active)
											{
												return false;
											}
											if (!player.Session.Active)
											{
												return false;
											}
											return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
										});
										binaryWriter.Close();
										Experience(randomize: true, 10);
									}
									else if (Item.Hardness(Session.Data.Foreground[num12]) > num8 + Profile.GetBreakSpeed())
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(19));
										binaryWriter.Write(Convert.ToUInt16(num9));
										binaryWriter.Write(Convert.ToUInt16(num10));
										binaryWriter.Write(Convert.ToUInt16(num8 + 1));
										binaryWriter.Write(Convert.ToUInt16(Item.Hardness(Session.Data.Foreground[num12])));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Server.Broadcast(input.ToArray(), delegate(Player player)
										{
											if (!player.Active)
											{
												return false;
											}
											if (!player.Profile.Active)
											{
												return false;
											}
											if (!player.Session.Active)
											{
												return false;
											}
											return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
										});
										binaryWriter.Close();
									}
									else
									{
										Session.Data.Foreground[num12] = 0;
										Session.Data.Special[num12] = null;
										Database.ProfileSave(Profile.Data);
										Database.SessionSave(Session.Data);
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(11));
										binaryWriter.Write(Convert.ToUInt16(num9));
										binaryWriter.Write(Convert.ToUInt16(num10));
										binaryWriter.Write(Convert.ToUInt16(2));
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Server.Broadcast(input.ToArray(), delegate(Player player)
										{
											if (!player.Active)
											{
												return false;
											}
											if (!player.Profile.Active)
											{
												return false;
											}
											if (!player.Session.Active)
											{
												return false;
											}
											return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
										});
										binaryWriter.Close();
									}
								}
								else if (Item.Type(Session.Data.Foreground[num12]) == 2)
								{
									if (Item.Hardness(Session.Data.Foreground[num12]) > num8 + Profile.GetBreakSpeed())
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(19));
										binaryWriter.Write(Convert.ToUInt16(num9));
										binaryWriter.Write(Convert.ToUInt16(num10));
										binaryWriter.Write(Convert.ToUInt16(num8 + 1));
										binaryWriter.Write(Convert.ToUInt16(Item.Hardness(Session.Data.Foreground[num12])));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Server.Broadcast(input.ToArray(), delegate(Player player)
										{
											if (!player.Active)
											{
												return false;
											}
											if (!player.Profile.Active)
											{
												return false;
											}
											if (!player.Session.Active)
											{
												return false;
											}
											return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
										});
										binaryWriter.Close();
									}
									else if (Session.Data.Foreground[num12] == 73 || Session.Data.Foreground[num12] == 493)
									{
										if (!Session.WorldOwner(Profile.Data.Filename))
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(4));
											string text5 = "Only the owner of this lock can break it.";
											binaryWriter.Write(Encoding.UTF8.GetBytes(text5 + "\0"));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else if (!Profile.CanGetItem(Session.Data.Foreground[num12], 1))
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(4));
											string text6 = "Couldn't break this item, not enough inventory space.";
											binaryWriter.Write(Encoding.UTF8.GetBytes(text6 + "\0"));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else
										{
											Session.Data.Owner = "";
											Session.Data.Admin.Clear();
											Session.Data.Public = false;
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(4));
											string text7 = "~0[~1" + Profile.Data.Username + "~0 has removed the lock]";
											binaryWriter.Write(Encoding.UTF8.GetBytes(text7 + "\0"));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Server.Broadcast(input.ToArray(), delegate(Player player)
											{
												if (!player.Active)
												{
													return false;
												}
												if (!player.Profile.Active)
												{
													return false;
												}
												if (!player.Session.Active)
												{
													return false;
												}
												return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
											});
											binaryWriter.Close();
											Profile.SetItem(Session.Data.Foreground[num12], Profile.GetItem(Session.Data.Foreground[num12]) + 1);
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(6));
											Profile.WriteItems(binaryWriter);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
											Session.Data.Foreground[num12] = 0;
											Session.Data.Special[num12] = null;
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(11));
											binaryWriter.Write(Convert.ToUInt16(num9));
											binaryWriter.Write(Convert.ToUInt16(num10));
											binaryWriter.Write(Convert.ToUInt16(2));
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Server.Broadcast(input.ToArray(), delegate(Player player)
											{
												if (!player.Active)
												{
													return false;
												}
												if (!player.Profile.Active)
												{
													return false;
												}
												if (!player.Session.Active)
												{
													return false;
												}
												return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
											});
											binaryWriter.Close();
											if (Profile.Data.Worlds.Contains(Session.Data.Name))
											{
												Profile.Data.Worlds.Remove(Session.Data.Name);
											}
											Database.ProfileSave(Profile.Data);
											Database.SessionSave(Session.Data);
											Player[] array2 = Server.Online.ToArray();
											foreach (Player player4 in array2)
											{
												if (player4.Active && player4.Profile.Active && player4.Session.Active && !(player4.Session.Data.Name != Session.Data.Name))
												{
													input = new MemoryStream();
													binaryWriter = new BinaryWriter(input);
													binaryWriter.Write(Convert.ToUInt16(0));
													binaryWriter.Write(Convert.ToUInt16(10));
													player4.Session.WriteTiles(binaryWriter, player4.Profile.Data.Filename);
													binaryWriter.Seek(0, SeekOrigin.Begin);
													binaryWriter.Write(Convert.ToUInt16(input.Length));
													player4.Send(input.ToArray());
													binaryWriter.Close();
												}
											}
										}
									}
									else if (Session.Data.Foreground[num12] == 319 || Session.Data.Foreground[num12] == 321 || Session.Data.Foreground[num12] == 323)
									{
										LockData lockData = Session.GetLockData(num12);
										if (!Session.TileOwner(Profile.Data.Filename, num12))
										{
											PlayerConsole.Message(this, "Only the owner can break this lock.");
										}
										else if (!Profile.CanGetItem(Session.Data.Foreground[num12], 1))
										{
											PlayerConsole.Message(this, "You don't have enough inventory space to break this lock.");
										}
										else
										{
											Profile.SetItem(Session.Data.Foreground[num12], Profile.GetItem(Session.Data.Foreground[num12]) + 1);
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(6));
											Profile.WriteItems(binaryWriter);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
											Session.Data.Foreground[num12] = 0;
											Session.Data.Special[num12] = null;
											Session.UnlockArea(new Parent
											{
												X = num9,
												Y = num10
											});
											Database.ProfileSave(Profile.Data);
											Database.SessionSave(Session.Data);
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(11));
											binaryWriter.Write(Convert.ToUInt16(num9));
											binaryWriter.Write(Convert.ToUInt16(num10));
											binaryWriter.Write(Convert.ToUInt16(2));
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Server.Broadcast(input.ToArray(), delegate(Player player)
											{
												if (!player.Active)
												{
													return false;
												}
												if (!player.Profile.Active)
												{
													return false;
												}
												if (!player.Session.Active)
												{
													return false;
												}
												return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
											});
											binaryWriter.Close();
											Player[] array3 = Server.Online.ToArray();
											foreach (Player player5 in array3)
											{
												if (player5.Active && player5.Profile.Active && player5.Session.Active && !(player5.Session.Data.Name != Session.Data.Name))
												{
													input = new MemoryStream();
													binaryWriter = new BinaryWriter(input);
													binaryWriter.Write(Convert.ToUInt16(0));
													binaryWriter.Write(Convert.ToUInt16(10));
													player5.Session.WriteTiles(binaryWriter, player5.Profile.Data.Filename);
													binaryWriter.Seek(0, SeekOrigin.Begin);
													binaryWriter.Write(Convert.ToUInt16(input.Length));
													player5.Send(input.ToArray());
													binaryWriter.Close();
												}
											}
										}
									}
									else if (Session.Data.Foreground[num12] == 313)
									{
										VendingData vendingData = Session.GetVendingData(num12);
										if (!Profile.CanGetItem(Session.Data.Foreground[num12], 1))
										{
											PlayerConsole.Message(this, "Not enough inventory space.");
										}
										else if (vendingData.Index != 0 || vendingData.Count != 0 || vendingData.Price != 0)
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(4));
											string text8 = "Cannot break a vending machine with items in it.";
											binaryWriter.Write(Encoding.UTF8.GetBytes(text8 + "\0"));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else
										{
											Profile.SetItem(Session.Data.Foreground[num12], Profile.GetItem(Session.Data.Foreground[num12]) + 1);
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(6));
											Profile.WriteItems(binaryWriter);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
											Session.Data.Foreground[num12] = 0;
											Session.Data.Special[num12] = null;
											Database.ProfileSave(Profile.Data);
											Database.SessionSave(Session.Data);
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(11));
											binaryWriter.Write(Convert.ToUInt16(num9));
											binaryWriter.Write(Convert.ToUInt16(num10));
											binaryWriter.Write(Convert.ToUInt16(2));
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Server.Broadcast(input.ToArray(), delegate(Player player)
											{
												if (!player.Active)
												{
													return false;
												}
												if (!player.Profile.Active)
												{
													return false;
												}
												if (!player.Session.Active)
												{
													return false;
												}
												return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
											});
											binaryWriter.Close();
										}
									}
									else if (Session.Data.Foreground[num12] == 495 || Session.Data.Foreground[num12] == 497 || Session.Data.Foreground[num12] == 499)
									{
										if (!Profile.CanGetItem(Session.Data.Foreground[num12], 1))
										{
											PlayerConsole.Message(this, "Not enough inventory space.");
										}
										else
										{
											Profile.SetItem(Session.Data.Foreground[num12], Profile.GetItem(Session.Data.Foreground[num12]) + 1);
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(6));
											Profile.WriteItems(binaryWriter);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
											if (Session.Data.Foreground[num12] == 495)
											{
												Session.Data.AntiPunch = false;
											}
											if (Session.Data.Foreground[num12] == 497)
											{
												Session.Data.AntiTalk = false;
											}
											if (Session.Data.Foreground[num12] == 499)
											{
												Session.Data.AntiDrop = false;
											}
											Session.Data.Foreground[num12] = 0;
											Session.Data.Special[num12] = null;
											Database.ProfileSave(Profile.Data);
											Database.SessionSave(Session.Data);
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(11));
											binaryWriter.Write(Convert.ToUInt16(num9));
											binaryWriter.Write(Convert.ToUInt16(num10));
											binaryWriter.Write(Convert.ToUInt16(2));
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Server.Broadcast(input.ToArray(), delegate(Player player)
											{
												if (!player.Active)
												{
													return false;
												}
												if (!player.Profile.Active)
												{
													return false;
												}
												if (!player.Session.Active)
												{
													return false;
												}
												return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
											});
											binaryWriter.Close();
										}
									}
									else if (Session.Data.Foreground[num12] == 535)
									{
										DisplayBoxData displayBoxData = Session.GetDisplayBoxData(num12);
										if (!Profile.CanGetItem(Session.Data.Foreground[num12], 1))
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(4));
											string text9 = "Not enough inventory space.";
											binaryWriter.Write(Encoding.UTF8.GetBytes(text9 + "\0"));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else if (displayBoxData.Index != 0)
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(4));
											string text10 = "Cannot break a display box with items in it.";
											binaryWriter.Write(Encoding.UTF8.GetBytes(text10 + "\0"));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else
										{
											Profile.SetItem(Session.Data.Foreground[num12], Profile.GetItem(Session.Data.Foreground[num12]) + 1);
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(6));
											Profile.WriteItems(binaryWriter);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
											Session.Data.Foreground[num12] = 0;
											Session.Data.Special[num12] = null;
											Database.ProfileSave(Profile.Data);
											Database.SessionSave(Session.Data);
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(11));
											binaryWriter.Write(Convert.ToUInt16(num9));
											binaryWriter.Write(Convert.ToUInt16(num10));
											binaryWriter.Write(Convert.ToUInt16(2));
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Server.Broadcast(input.ToArray(), delegate(Player player)
											{
												if (!player.Active)
												{
													return false;
												}
												if (!player.Profile.Active)
												{
													return false;
												}
												if (!player.Session.Active)
												{
													return false;
												}
												return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
											});
											binaryWriter.Close();
										}
									}
									else if (Session.Data.Foreground[num12] == 557)
									{
										SmokehouseData smokehouseData = Session.GetSmokehouseData(num12);
										if (!Profile.CanGetItem(Session.Data.Foreground[num12], 1))
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(4));
											string text11 = "Not enough inventory space.";
											binaryWriter.Write(Encoding.UTF8.GetBytes(text11 + "\0"));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else if (smokehouseData.Index != 0 || smokehouseData.Count != 0)
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(4));
											string text12 = "Cannot break a smokehouse while there's fish inside.";
											binaryWriter.Write(Encoding.UTF8.GetBytes(text12 + "\0"));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else
										{
											Profile.SetItem(Session.Data.Foreground[num12], Profile.GetItem(Session.Data.Foreground[num12]) + 1);
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(6));
											Profile.WriteItems(binaryWriter);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
											Session.Data.Foreground[num12] = 0;
											Session.Data.Special[num12] = null;
											Database.ProfileSave(Profile.Data);
											Database.SessionSave(Session.Data);
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(11));
											binaryWriter.Write(Convert.ToUInt16(num9));
											binaryWriter.Write(Convert.ToUInt16(num10));
											binaryWriter.Write(Convert.ToUInt16(2));
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Server.Broadcast(input.ToArray(), delegate(Player player)
											{
												if (!player.Active)
												{
													return false;
												}
												if (!player.Profile.Active)
												{
													return false;
												}
												if (!player.Session.Active)
												{
													return false;
												}
												return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
											});
											binaryWriter.Close();
										}
									}
									else if (Session.Data.Foreground[num12] == 157 || Session.Data.Foreground[num12] == 559 || Session.Data.Foreground[num12] == 597 || Session.Data.Foreground[num12] == 599 || Session.Data.Foreground[num12] == 601)
									{
										if (!Profile.CanGetItem(Session.Data.Foreground[num12], 1))
										{
											PlayerConsole.Message(this, "Not enough inventory space.");
										}
										else
										{
											Profile.SetItem(Session.Data.Foreground[num12], Profile.GetItem(Session.Data.Foreground[num12]) + 1);
											PlayerCore.UpdateInventory(this);
											Session.Data.Foreground[num12] = 0;
											Session.Data.Special[num12] = null;
											Database.ProfileSave(Profile.Data);
											Database.SessionSave(Session.Data);
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(11));
											binaryWriter.Write(Convert.ToUInt16(num9));
											binaryWriter.Write(Convert.ToUInt16(num10));
											binaryWriter.Write(Convert.ToUInt16(2));
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Server.Broadcast(input.ToArray(), delegate(Player player)
											{
												if (!player.Active)
												{
													return false;
												}
												if (!player.Profile.Active)
												{
													return false;
												}
												if (!player.Session.Active)
												{
													return false;
												}
												return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
											});
											binaryWriter.Close();
										}
									}
									else
									{
										Profile.Data.QuestLeft -= Quest.BreakForeground(Profile.Data.QuestType, Profile.Data.QuestItem, Session.Data.Foreground[num12]);
										Challenge.BreakForeground(Profile.Data.Filename, Session.Data.Foreground[num12]);
										Achievement(AchievementType.BreakBackground, increase: true, 1);
										PlayerQuests.Event(this, PlayerEvent.Break, Session.Data.Foreground[num12]);
										BinaryWriter binaryWriter;
										if (Item.Farmability(Session.Data.Foreground[num12]) != 0 && (Session.LastSeedX != num9 || Session.LastSeedY != num10))
										{
											int num17 = Item.BreakExtra(Session.Data.Foreground[num12], this);
											int num18 = 1;
											if (num17 == 0)
											{
												if (Server.Random.Next(3) == 0)
												{
													num17 = Session.Data.Foreground[num12];
												}
												else if (Server.Random.Next(5 - Item.Farmability(Session.Data.Foreground[num12]) + 1) == 0)
												{
													num17 = Item.ItemToSeed(Session.Data.Foreground[num12]);
												}
											}
											if (!Session.CanDrop())
											{
												input = new MemoryStream();
												binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(4));
												string text13 = "~3There are too many dropped items in this world, so nothing falls out.";
												binaryWriter.Write(Encoding.UTF8.GetBytes(text13 + "\0"));
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Send(input.ToArray());
												binaryWriter.Close();
											}
											else if (num17 != 0 && num18 != 0)
											{
												input = new MemoryStream();
												binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(12));
												Session.Drop(binaryWriter, num17, num18, num9 * 32 + 6, num10 * 32 + 6);
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Server.Broadcast(input.ToArray(), delegate(Player player)
												{
													if (!player.Active)
													{
														return false;
													}
													if (!player.Profile.Active)
													{
														return false;
													}
													if (!player.Session.Active)
													{
														return false;
													}
													return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
												});
												binaryWriter.Close();
											}
											Session.LastSeedX = num9;
											Session.LastSeedY = num10;
										}
										int num19 = Server.Random.Next(Item.SeedGems(Session.Data.Foreground[num12]));
										int currency2 = Server.Random.Next(Item.SeedCurrency(Session.Data.Foreground[num12]));
										if (num19 > 0)
										{
											PlayerGems.Add(this, num19);
											if (Event.Type == EventType.Space && DateTime.UtcNow > Event.AvailableFrom && DateTime.UtcNow < Event.AvailableTo)
											{
												PlayerSpecialCurrency.Add(this, currency2);
											}
										}
										if (Session.Data.Foreground[num12] == 481 || Session.Data.Foreground[num12] == 483)
										{
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(28));
											Session.WriteMusic(binaryWriter);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Server.Broadcast(input.ToArray(), delegate(Player player)
											{
												if (!player.Active)
												{
													return false;
												}
												if (!player.Profile.Active)
												{
													return false;
												}
												if (!player.Session.Active)
												{
													return false;
												}
												return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
											});
											binaryWriter.Close();
										}
										Session.Data.Foreground[num12] = 0;
										if (Session.Data.Special[num12] != null)
										{
											Session.Data.Special[num12] = null;
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(11));
											binaryWriter.Write(Convert.ToUInt16(num9));
											binaryWriter.Write(Convert.ToUInt16(num10));
											binaryWriter.Write(Convert.ToUInt16(3));
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Server.Broadcast(input.ToArray(), delegate(Player player)
											{
												if (!player.Active)
												{
													return false;
												}
												if (!player.Profile.Active)
												{
													return false;
												}
												if (!player.Session.Active)
												{
													return false;
												}
												return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
											});
											binaryWriter.Close();
										}
										Database.ProfileSave(Profile.Data);
										Database.SessionSave(Session.Data);
										input = new MemoryStream();
										binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(11));
										binaryWriter.Write(Convert.ToUInt16(num9));
										binaryWriter.Write(Convert.ToUInt16(num10));
										binaryWriter.Write(Convert.ToUInt16(2));
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Server.Broadcast(input.ToArray(), delegate(Player player)
										{
											if (!player.Active)
											{
												return false;
											}
											if (!player.Profile.Active)
											{
												return false;
											}
											if (!player.Session.Active)
											{
												return false;
											}
											return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
										});
										binaryWriter.Close();
										Experience(randomize: true, 10);
									}
								}
								else if (Item.Type(Session.Data.Background[num12]) == 1)
								{
									if (Item.Hardness(Session.Data.Background[num12]) > num8 + Profile.GetBreakSpeed())
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(19));
										binaryWriter.Write(Convert.ToUInt16(num9));
										binaryWriter.Write(Convert.ToUInt16(num10));
										binaryWriter.Write(Convert.ToUInt16(num8 + 1));
										binaryWriter.Write(Convert.ToUInt16(Item.Hardness(Session.Data.Background[num12])));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Server.Broadcast(input.ToArray(), delegate(Player player)
										{
											if (!player.Active)
											{
												return false;
											}
											if (!player.Profile.Active)
											{
												return false;
											}
											if (!player.Session.Active)
											{
												return false;
											}
											return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
										});
										binaryWriter.Close();
									}
									else
									{
										Profile.Data.QuestLeft -= Quest.BreakBackground(Profile.Data.QuestType, Profile.Data.QuestItem, Session.Data.Background[num12]);
										Challenge.BreakBackground(Profile.Data.Filename, Session.Data.Background[num12]);
										Achievement(AchievementType.BreakBackground, increase: true, 1);
										PlayerQuests.Event(this, PlayerEvent.Break, Session.Data.Background[num12]);
										BinaryWriter binaryWriter;
										if (Item.Farmability(Session.Data.Background[num12]) != 0 && (Session.LastSeedX != num9 || Session.LastSeedY != num10))
										{
											int num20 = Item.BreakExtra(Session.Data.Foreground[num12], this);
											int num21 = 1;
											if (num20 == 0)
											{
												if (Server.Random.Next(3) == 0)
												{
													num20 = Session.Data.Background[num12];
													num21 = 1;
												}
												else if (Server.Random.Next(5 - Item.Farmability(Session.Data.Background[num12]) + 1) == 0)
												{
													num20 = Item.ItemToSeed(Session.Data.Background[num12]);
													num21 = 1;
												}
											}
											if (!Session.CanDrop())
											{
												input = new MemoryStream();
												binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(4));
												string text14 = "~3There are too many dropped items in this world, so nothing falls out.";
												binaryWriter.Write(Encoding.UTF8.GetBytes(text14 + "\0"));
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Send(input.ToArray());
												binaryWriter.Close();
											}
											else if (num20 != 0 && num21 != 0)
											{
												input = new MemoryStream();
												binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(12));
												Session.Drop(binaryWriter, num20, num21, num9 * 32 + 6, num10 * 32 + 6);
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Server.Broadcast(input.ToArray(), delegate(Player player)
												{
													if (!player.Active)
													{
														return false;
													}
													if (!player.Profile.Active)
													{
														return false;
													}
													if (!player.Session.Active)
													{
														return false;
													}
													return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
												});
												binaryWriter.Close();
											}
											Session.LastSeedX = num9;
											Session.LastSeedY = num10;
										}
										int num22 = Server.Random.Next(Item.SeedGems(Session.Data.Foreground[num12]));
										int currency3 = Server.Random.Next(Item.SeedCurrency(Session.Data.Foreground[num12]));
										if (num22 > 0)
										{
											PlayerGems.Add(this, num22);
											if (Event.Type == EventType.Space && DateTime.UtcNow > Event.AvailableFrom && DateTime.UtcNow < Event.AvailableTo)
											{
												PlayerSpecialCurrency.Add(this, currency3);
											}
										}
										Session.Data.Background[num12] = 0;
										Database.ProfileSave(Profile.Data);
										Database.SessionSave(Session.Data);
										input = new MemoryStream();
										binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(11));
										binaryWriter.Write(Convert.ToUInt16(num9));
										binaryWriter.Write(Convert.ToUInt16(num10));
										binaryWriter.Write(Convert.ToUInt16(1));
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Server.Broadcast(input.ToArray(), delegate(Player player)
										{
											if (!player.Active)
											{
												return false;
											}
											if (!player.Profile.Active)
											{
												return false;
											}
											if (!player.Session.Active)
											{
												return false;
											}
											return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
										});
										binaryWriter.Close();
										Experience(randomize: true, 10);
									}
								}
							}
							else if (player2 != null && num7 == 3)
							{
								if (player2 == this)
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Wrench.Self");
									Dialog.ItemText(binaryWriter, breaker: true, $"~1{Profile.Data.Username}", 75, 3);
									Dialog.Text(binaryWriter, breaker: true, $"You are level ~1{Profile.Data.Level}~0, you need ~1{Profile.Data.Level * 500 - Profile.Data.Experience} ~0more XP to level up.", 50);
									Dialog.Text(binaryWriter, breaker: true, $"You have completed ~1{Profile.Data.QuestLevel} ~0quests.", 50);
									if (Session.WorldOwner(Profile.Data.Filename))
									{
										Dialog.Text(binaryWriter, breaker: true, "You have ~4owner access ~0in this world.", 50);
									}
									else if (Session.WorldAdmin(Profile.Data.Filename))
									{
										Dialog.Text(binaryWriter, breaker: true, "You have ~6admin access ~0in this world.", 50);
									}
									else
									{
										Dialog.Text(binaryWriter, breaker: true, "You have ~3no access ~0in this world.", 50);
									}
									if (Rewards.Capitalization.Contains(Profile.Data.Filename))
									{
										Dialog.Text(binaryWriter, breaker: true, "You have auto capitalization enabled.", 50);
									}
									if (GameID != 0)
									{
										Dialog.Text(binaryWriter, breaker: true, "You are in the mini-game queue.", 50);
									}
									Dialog.Space(binaryWriter);
									Dialog.Button(binaryWriter, breaker: true, "Challenge", "View weekly challenge");
									Dialog.Button(binaryWriter, breaker: true, "Worlds", "View owned worlds");
									Dialog.Button(binaryWriter, breaker: true, "Achievements", "View achievements");
									Dialog.Button(binaryWriter, breaker: true, "ItemQuest", "View item quest (~3NEW~1)");
									Dialog.Space(binaryWriter);
									Dialog.Text(binaryWriter, breaker: true, string.Format("Your account was created at ~1{0}~0.", Profile.Data.RegisterDate.ToString("f")), 50);
									Dialog.Text(binaryWriter, breaker: true, $"You have spent ~1{Text.TimeLong(Profile.GetTotalOnline())} ~0online.", 50);
									Dialog.Text(binaryWriter, breaker: true, $"You are standing on tile ~1{CurrentX / 32}:{CurrentY / 32}~0.", 50);
									Dialog.Text(binaryWriter, breaker: true, $"There are ~1{Session.Players(Session.Data.Filename)} ~0players in this world.", 50);
									Dialog.Space(binaryWriter);
									Dialog.Button(binaryWriter, breaker: true, "Cancel", "Cancel");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								if (player2 != this)
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, 0, "Wrench.Other");
									Dialog.ItemText(binaryWriter, breaker: true, $"~1{player2.Profile.Data.Username}", 75, 3);
									int num23 = ((player2.Profile.Data.FakeLevel == 0) ? 11 : ((player2.Profile.Data.FakeLevel != 0 || Profile.Data.Level <= 99) ? player2.Profile.Data.FakeLevel : 6));
									if (Rewards.Permission(player2.Profile.Data.Filename, Permissions.Mod) && player2.Profile.Data.Username != "~r" + player2.Profile.Data.Filename)
									{
										Dialog.Text(binaryWriter, breaker: true, $"Player is level ~1{num23}~0.", 50);
									}
									else if (Rewards.Permission(player2.Profile.Data.Filename, Permissions.Mod) && player2.Profile.Data.Username == "~r" + player2.Profile.Data.Filename)
									{
										Dialog.Text(binaryWriter, breaker: true, $"Player is level ~1{player2.Profile.Data.Level}~0.", 50);
									}
									else
									{
										Dialog.Text(binaryWriter, breaker: true, $"Player is level ~1{player2.Profile.Data.Level}~0.", 50);
									}
									if (Session.WorldOwner(player2.Profile.Data.Filename))
									{
										Dialog.Text(binaryWriter, breaker: true, "Has ~4owner access ~0in this world.", 50);
									}
									else if (Session.WorldAdmin(player2.Profile.Data.Filename))
									{
										Dialog.Text(binaryWriter, breaker: true, "Has ~6admin access ~0in this world.", 50);
									}
									else
									{
										Dialog.Text(binaryWriter, breaker: true, "Has ~3no access ~0in this world.", 50);
									}
									Dialog.Space(binaryWriter);
									if (!Trading)
									{
										Dialog.Button(binaryWriter, breaker: true, "Trade", "Trade");
									}
									if (Profile.Data.Friends.Contains(player2.Profile.Data.Filename))
									{
										Dialog.Button(binaryWriter, breaker: true, "RemoveFriend", "Remove as friend");
									}
									else
									{
										Dialog.Button(binaryWriter, breaker: true, "AddFriend", "Add as friend");
									}
									if (PlayerControl.CanBan(this, player2))
									{
										Dialog.Button(binaryWriter, breaker: true, "WorldBan", "Ban from world");
									}
									if (PlayerControl.CanPull(this, player2))
									{
										Dialog.Button(binaryWriter, breaker: true, "Pull", "Pull player");
									}
									if (PlayerControl.CanKill(this, player2))
									{
										Dialog.Button(binaryWriter, breaker: true, "Kill", "Kill player");
									}
									if (PlayerControl.CanFreeze(this, player2))
									{
										Dialog.Button(binaryWriter, breaker: true, "Freeze", player2.Profile.Frozen ? "Unfreeze player" : "Freeze player");
									}
									if (Rewards.Permission(Profile.Data.Filename, Permissions.Mod))
									{
										Dialog.Button(binaryWriter, breaker: true, "Punish", "Punish player");
										Dialog.Button(binaryWriter, breaker: true, "Lookup", "Lookup player");
									}
									else
									{
										Dialog.Button(binaryWriter, breaker: true, "Report", "Report player");
									}
									Dialog.Space(binaryWriter);
									Dialog.Button(binaryWriter, breaker: true, "Cancel", "Cancel");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
							}
							else if (player2 == null && num7 == 3)
							{
								int sizeX3 = Session.Data.SizeX;
								int sizeY3 = Session.Data.SizeY;
								int num24 = num9 + num10 * sizeX3;
								if (Session.Data.Foreground[num24] == 73 || Session.Data.Foreground[num24] == 493)
								{
									Database.SessionLoad(ref Session.Data, Session.Data.Name);
									if (Session.WorldOwner(Profile.Data.Filename))
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Wrench.WorldLock.Owner");
										Dialog.ItemText(binaryWriter, breaker: true, $"~1{Item.Name(Session.Data.Foreground[num24])} options", 75, Session.Data.Foreground[num24]);
										if (Session.Data.Admin.Count == 0)
										{
											Dialog.Text(binaryWriter, breaker: true, "No one has access yet.", 50);
										}
										else
										{
											foreach (string item6 in Session.Data.Admin)
											{
												Dialog.Checkbox(binaryWriter, breaker: true, value: true, item6, item6, 75);
											}
										}
										Dialog.Button(binaryWriter, breaker: true, "Access", "Give access");
										Dialog.Button(binaryWriter, breaker: true, "GetKey", "Get world key");
										Dialog.Space(binaryWriter);
										Dialog.Checkbox(binaryWriter, breaker: true, Session.Data.Public, "Public", "This world is public", 75);
										Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (Session.Data.Admin.Contains(Profile.Data.Filename))
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Wrench.WorldLock.Admin");
										Dialog.ItemText(binaryWriter, breaker: true, $"~1{Item.Name(Session.Data.Foreground[num24])} options", 75, Session.Data.Foreground[num24]);
										Dialog.Text(binaryWriter, breaker: true, $"World is owned by ~1{Session.Data.Owner}~0, but you have access.", 50);
										Dialog.Button(binaryWriter, breaker: true, "RemoveAccess", "Remove my access");
										Dialog.Button(binaryWriter, breaker: true, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Wrench.WorldLock.Guest");
										Dialog.ItemText(binaryWriter, breaker: true, $"~1{Item.Name(Session.Data.Foreground[num24])} options", 75, Session.Data.Foreground[num24]);
										if (Session.Data.Public)
										{
											Dialog.Text(binaryWriter, breaker: true, $"World is owned by ~1{Session.Data.Owner}~0, this world is public.", 50);
										}
										else
										{
											Dialog.Text(binaryWriter, breaker: true, $"World is owned by ~1{Session.Data.Owner}~0, you have no access.", 50);
										}
										Dialog.Button(binaryWriter, breaker: true, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
								else if (Session.Data.Foreground[num24] == 221)
								{
									TimeSpan timeSpan = DateTime.UtcNow.Subtract(Server.Date);
									if (Profile.Data.Level >= 5)
									{
										if ((double)Profile.Data.RewardCooldown <= timeSpan.TotalSeconds)
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, num24, "Wrench.RewardBox1");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Daily reward", 75, 3);
											Dialog.Text(binaryWriter, breaker: true, "You will get one of the following", 50);
											Dialog.Text(binaryWriter, breaker: true, "items or ~1250 to ~11000 ~0gems.", 50);
											foreach (RewardItem item7 in Rewards.Daily)
											{
												Dialog.ItemText(binaryWriter, breaker: true, $"x{item7.Count} {Item.Name(item7.Index)}", 50, item7.Index);
											}
											Dialog.Button(binaryWriter, breaker: true, "Claim", "Claim reward");
											Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else if (Profile.Data.AllowRespin)
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, num24, "Wrench.RewardBox1.Respin");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Daily reward", 75, 3);
											Dialog.Text(binaryWriter, breaker: true, "Second chance is available, would you like to try again?", 50);
											Dialog.Button(binaryWriter, breaker: false, "Respin", "Respin");
											Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else
										{
											int time = (int)Math.Round((double)Profile.Data.RewardCooldown - timeSpan.TotalSeconds);
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, num24, "Message");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Daily reward", 75, 3);
											Dialog.Text(binaryWriter, breaker: true, $"Another daily reward will be available in ~1{Text.Time(time)}~0.", 50);
											Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
									}
									else
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Daily reward", 75, 3);
										Dialog.Text(binaryWriter, breaker: true, "Sorry, daily rewards unlock at level ~15~0.", 50);
										Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
								else if (Session.Data.Foreground[num24] == 237)
								{
									if (Profile.Data.Level >= 3)
									{
										TimeSpan timeSpan2 = DateTime.UtcNow.Subtract(Server.Date);
										if ((double)Profile.Data.VideoCooldown <= timeSpan2.TotalSeconds)
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, num24, "Wrench.RewardBox2");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Free gems", 75, 3);
											Dialog.Text(binaryWriter, breaker: true, "Would you like to watch an advertisement to", 50);
											Dialog.Text(binaryWriter, breaker: true, "get free gems?", 50);
											Dialog.Button(binaryWriter, breaker: false, "Watch", "Watch");
											Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else
										{
											int time2 = (int)Math.Round((double)Profile.Data.VideoCooldown - timeSpan2.TotalSeconds);
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, num24, "Message");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Free gems", 75, 3);
											Dialog.Text(binaryWriter, breaker: true, $"Another video will be available in ~1{Text.Time(time2)}~0.", 50);
											Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
									}
									else
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Free gems", 75, 3);
										Dialog.Text(binaryWriter, breaker: true, "Sorry, this feature unlocks at level ~13~0.", 50);
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
								else if (Session.Data.Foreground[num24] == 257 || Session.Data.Foreground[num24] == 1233)
								{
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, num24, "Wrench.QuestNpc");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Quest", 75, 3);
									if (Profile.Data.QuestType == 0)
									{
										Dialog.Text(binaryWriter, breaker: true, "Currently you have no active quests going on.", 50);
										Dialog.Button(binaryWriter, breaker: true, "Start", "Start a new quest");
									}
									else if (Profile.Data.QuestLeft <= 0)
									{
										Dialog.Text(binaryWriter, breaker: true, "You are done with your quest, you", 50);
										Dialog.Text(binaryWriter, breaker: true, "can now claim your quest rewards.", 50);
										Dialog.Button(binaryWriter, breaker: true, "Finish", "Finish quest");
									}
									else
									{
										Dialog.Text(binaryWriter, breaker: true, "You are currently in a quest.", 50);
										if (Profile.Data.QuestType == 1 && Profile.Data.QuestItem == 0)
										{
											Dialog.Text(binaryWriter, breaker: false, "Break any background blocks.", 50);
										}
										if (Profile.Data.QuestType == 1 && Profile.Data.QuestItem != 0)
										{
											Dialog.Text(binaryWriter, breaker: false, "Break ~1" + Item.Name(Profile.Data.QuestItem) + "~0.", 50);
										}
										if (Profile.Data.QuestType == 2 && Profile.Data.QuestItem == 0)
										{
											Dialog.Text(binaryWriter, breaker: false, "Place any background blocks.", 50);
										}
										if (Profile.Data.QuestType == 2 && Profile.Data.QuestItem != 0)
										{
											Dialog.Text(binaryWriter, breaker: false, "Place ~1" + Item.Name(Profile.Data.QuestItem) + "~0.", 50);
										}
										if (Profile.Data.QuestType == 3 && Profile.Data.QuestItem == 0)
										{
											Dialog.Text(binaryWriter, breaker: false, "Break any foreground blocks.", 50);
										}
										if (Profile.Data.QuestType == 3 && Profile.Data.QuestItem != 0)
										{
											Dialog.Text(binaryWriter, breaker: false, "Break ~1" + Item.Name(Profile.Data.QuestItem) + "~0.", 50);
										}
										if (Profile.Data.QuestType == 4 && Profile.Data.QuestItem == 0)
										{
											Dialog.Text(binaryWriter, breaker: false, "Place any foreground blocks.", 50);
										}
										if (Profile.Data.QuestType == 4 && Profile.Data.QuestItem != 0)
										{
											Dialog.Text(binaryWriter, breaker: false, "Place ~1" + Item.Name(Profile.Data.QuestItem) + "~0.", 50);
										}
										if (Profile.Data.QuestType == 5 && Profile.Data.QuestItem == 0)
										{
											Dialog.Text(binaryWriter, breaker: false, "Harvest any kind of seeds.", 50);
										}
										if (Profile.Data.QuestType == 5 && Profile.Data.QuestItem != 0)
										{
											Dialog.Text(binaryWriter, breaker: false, "Harvest ~1" + Item.Name(Profile.Data.QuestItem) + "~0.", 50);
										}
										if (Profile.Data.QuestType == 6 && Profile.Data.QuestItem == 0)
										{
											Dialog.Text(binaryWriter, breaker: false, "Plant any kind of seeds.", 50);
										}
										if (Profile.Data.QuestType == 6 && Profile.Data.QuestItem != 0)
										{
											Dialog.Text(binaryWriter, breaker: false, "Plant ~1" + Item.Name(Profile.Data.QuestItem) + "~0.", 50);
										}
										if (Profile.Data.QuestType == 7 && Profile.Data.QuestItem == 0)
										{
											Dialog.Text(binaryWriter, breaker: false, "Splice any kind of seeds.", 50);
										}
										if (Profile.Data.QuestType == 7 && Profile.Data.QuestItem != 0)
										{
											Dialog.Text(binaryWriter, breaker: false, "Splice ~1" + Item.Name(Profile.Data.QuestItem) + "~0.", 50);
										}
										if (Profile.Data.QuestType == 8 && Profile.Data.QuestItem == 0)
										{
											Dialog.Text(binaryWriter, breaker: false, "Catch any kind of fish.", 50);
										}
										if (Profile.Data.QuestType == 8 && Profile.Data.QuestItem != 0)
										{
											Dialog.Text(binaryWriter, breaker: false, "Catch ~1" + Item.Name(Profile.Data.QuestItem) + "~0.", 50);
										}
										Dialog.Text(binaryWriter, breaker: true, "~1" + Profile.Data.QuestLeft + "~0 left.", 50);
										Dialog.Button(binaryWriter, breaker: true, "Skip", "Skip quest");
									}
									Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								else if (Session.Data.Foreground[num24] == 307)
								{
									MailBoxData mailBoxData = Session.GetMailBoxData(num24);
									if (mailBoxData.Text == null)
									{
										mailBoxData.Text = new List<CommentData>();
									}
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, num24, "Wrench.Mailbox");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Mailbox", 75, 307);
									if (Session.TileAccess(Profile.Data.Filename, num24))
									{
										foreach (CommentData item8 in mailBoxData.Text)
										{
											Dialog.ItemText(binaryWriter, breaker: true, "~1" + item8.Name + "~0: " + item8.Text, 50, 307);
										}
										if (mailBoxData.Text.Count == 0)
										{
											Dialog.Text(binaryWriter, breaker: true, "Noone has left any messages yet.", 50);
										}
										else
										{
											Dialog.Button(binaryWriter, breaker: true, "Clear", "Clear all messages");
										}
									}
									else
									{
										Dialog.Text(binaryWriter, breaker: true, "What would you like to write?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Text", "", 32);
										Dialog.Button(binaryWriter, breaker: true, "Post", "Post message");
									}
									Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								else if (Session.Data.Foreground[num24] == 311)
								{
									BulletinData bulletinData = Session.GetBulletinData(num24);
									if (bulletinData.Text == null)
									{
										bulletinData.Text = new List<CommentData>();
									}
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, num24, "Wrench.BulletinBoard");
									Dialog.ItemText(binaryWriter, breaker: true, "~1Bulletin Board", 75, 311);
									if (bulletinData.Text.Count == 0)
									{
										Dialog.Text(binaryWriter, breaker: true, "No one has yet posted anything.", 50);
									}
									else
									{
										foreach (CommentData item9 in bulletinData.Text)
										{
											if (bulletinData.Author)
											{
												Dialog.ItemText(binaryWriter, breaker: true, "~1" + item9.Name + "~0: " + item9.Text, 50, 69);
											}
											else
											{
												Dialog.Text(binaryWriter, breaker: true, item9.Text, 50);
											}
										}
									}
									Dialog.Space(binaryWriter);
									if (Session.TileAccess(Profile.Data.Filename, num24) || bulletinData.Public)
									{
										Dialog.Text(binaryWriter, breaker: true, "What would you like to post?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Text", "", 32);
										Dialog.Button(binaryWriter, breaker: true, "Post", "Post message");
									}
									Dialog.Space(binaryWriter);
									if (Session.TileAccess(Profile.Data.Filename, num24))
									{
										Dialog.ItemText(binaryWriter, breaker: true, "~1Settings", 75, 73);
										if (bulletinData.Text.Count != 0)
										{
											Dialog.Button(binaryWriter, breaker: true, "Clear", "Clear all messages");
										}
										Dialog.Checkbox(binaryWriter, breaker: true, bulletinData.Public, "Public", "Anyone can post", 50);
										Dialog.Checkbox(binaryWriter, breaker: true, bulletinData.Author, "Author", "Show author names", 50);
										Dialog.Button(binaryWriter, breaker: true, "Update", "Save settings");
									}
									Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								else if (Session.Data.Foreground[num24] == 313)
								{
									VendingData vendingData2 = Session.GetVendingData(num24);
									if (Session.TileAccess(Profile.Data.Filename, num24))
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Wrench.VendingMachine");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Vending Machine", 75, 313);
										if (vendingData2.Index == 0)
										{
											Dialog.Text(binaryWriter, breaker: true, "This machine has nothing for sale.", 50);
											Dialog.Button(binaryWriter, breaker: true, "Add", "Add item");
										}
										else if (vendingData2.Sold)
										{
											Dialog.Text(binaryWriter, breaker: true, $"You have earned ~1x{vendingData2.Price} {Item.Name(73)}", 50);
											Dialog.Button(binaryWriter, breaker: true, "Earn", "Claim earnings");
										}
										else
										{
											Dialog.Text(binaryWriter, breaker: true, $"This machine sells ~1x{vendingData2.Count} {Item.Name(vendingData2.Index)}", 50);
											Dialog.Text(binaryWriter, breaker: true, $"The price is ~1x{vendingData2.Price} {Item.Name(73)}", 50);
											Dialog.Button(binaryWriter, breaker: true, "Take", "Take items");
										}
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (!vendingData2.Sold && vendingData2.Index != 0 && vendingData2.Count != 0 && vendingData2.Price != 0)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Wrench.VendingMachine");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Vending Machine", 75, 313);
										Dialog.Text(binaryWriter, breaker: true, $"This machine sells ~1x{vendingData2.Count} {Item.Name(vendingData2.Index)}", 50);
										Dialog.Text(binaryWriter, breaker: true, $"The price is ~1x{vendingData2.Price} {Item.Name(73)}", 50);
										Dialog.Button(binaryWriter, breaker: true, "Buy", "Buy this item");
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Vending Machine", 75, 313);
										Dialog.Text(binaryWriter, breaker: true, "This machine is out of order.", 50);
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
								else if (Session.Data.Foreground[num24] == 319 || Session.Data.Foreground[num24] == 321 || Session.Data.Foreground[num24] == 323)
								{
									LockData lockData2 = Session.GetLockData(num24);
									if (Session.TileOwner(Profile.Data.Filename, num24))
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Wrench.Lock.Owner");
										Dialog.ItemText(binaryWriter, breaker: true, $"~1{Item.Name(Session.Data.Foreground[num24])} options", 75, Session.Data.Foreground[num24]);
										if (lockData2.Admin.Count == 0)
										{
											Dialog.Text(binaryWriter, breaker: true, "No one has access yet.", 50);
										}
										else
										{
											foreach (string item10 in lockData2.Admin)
											{
												Dialog.Checkbox(binaryWriter, breaker: true, value: true, item10, item10, 75);
											}
										}
										Dialog.Button(binaryWriter, breaker: true, "Access", "Give access");
										Dialog.Space(binaryWriter);
										Dialog.Checkbox(binaryWriter, breaker: true, lockData2.Public, "Public", "This lock is public", 75);
										Dialog.Checkbox(binaryWriter, breaker: true, lockData2.Ignore, "Ignore", "Ignore empty tiles", 75);
										Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (Session.Data.Admin.Contains(Profile.Data.Filename))
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Wrench.Lock.Admin");
										Dialog.ItemText(binaryWriter, breaker: true, $"~1{Item.Name(Session.Data.Foreground[num24])} options", 75, Session.Data.Foreground[num24]);
										Dialog.Text(binaryWriter, breaker: true, $"Lock is owned by ~1{Session.Data.Owner}~0, but you have access.", 50);
										Dialog.Button(binaryWriter, breaker: true, "RemoveAccess", "Remove my access");
										Dialog.Button(binaryWriter, breaker: true, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Wrench.Lock.Guest");
										Dialog.ItemText(binaryWriter, breaker: true, $"~1{Item.Name(Session.Data.Foreground[num24])} options", 75, Session.Data.Foreground[num24]);
										if (lockData2.Public)
										{
											Dialog.Text(binaryWriter, breaker: true, $"Lock is owned by ~1{lockData2.Owner}~0, this lock is public.", 50);
										}
										else
										{
											Dialog.Text(binaryWriter, breaker: true, $"Lock is owned by ~1{lockData2.Owner}~0, you have no access.", 50);
										}
										Dialog.Button(binaryWriter, breaker: true, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
								else if (Session.Data.Foreground[num24] == 433)
								{
									if (Event.Active)
									{
										if (Event.World != Session.Data.Name)
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, num24, "Message");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Event reward", 75, 3);
											Dialog.Text(binaryWriter, breaker: true, "The reward box can only be opened", 50);
											Dialog.Text(binaryWriter, breaker: true, "in currently active event world.", 50);
											Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else if (Event.TicketActive(Profile.Data.TicketDuration))
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, num24, "Wrench.RewardBox3");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Event reward", 75, 3);
											Dialog.Text(binaryWriter, breaker: true, "Congratulations, you have finished in", 50);
											Dialog.Text(binaryWriter, breaker: true, "time, you can now claim your prize!", 50);
											Dialog.Button(binaryWriter, breaker: false, "Claim", "Claim prize");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(5));
											Window = Dialog.Create(binaryWriter, num24, "Message");
											Dialog.ItemText(binaryWriter, breaker: true, "~1Event reward", 75, 3);
											Dialog.Text(binaryWriter, breaker: true, "Sorry, you didn't finish in time,", 50);
											Dialog.Text(binaryWriter, breaker: true, "so there are no prizes for you.", 50);
											Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
									}
									else
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Event reward", 75, 3);
										Dialog.Text(binaryWriter, breaker: true, "Sorry, the event is over and there", 50);
										Dialog.Text(binaryWriter, breaker: true, "are no prizes left for you anymore.", 50);
										Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
								else if (Session.Data.Foreground[num24] == 535)
								{
									DisplayBoxData displayBoxData2 = Session.GetDisplayBoxData(num24);
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(5));
									Window = Dialog.Create(binaryWriter, num24, "Wrench.DisplayBox");
									Dialog.ItemText(binaryWriter, breaker: true, $"~1{Item.Name(Session.Data.Foreground[num24])} options", 75, Session.Data.Foreground[num24]);
									if (Session.TileAccess(Profile.Data.Filename, num24))
									{
										if (displayBoxData2.Index == 0)
										{
											Dialog.Text(binaryWriter, breaker: true, "This display box contains nothing.", 50);
											Dialog.Text(binaryWriter, breaker: true, "Which item would you like to place?", 50);
											Dialog.ItemPicker(binaryWriter, breaker: true, "Index", 0, 64, 64);
											Dialog.Button(binaryWriter, breaker: true, "Add", "Add this item");
										}
										else
										{
											Dialog.Text(binaryWriter, breaker: true, $"This display box contains ~1{Item.Name(displayBoxData2.Index)}~0.", 50);
											Dialog.Button(binaryWriter, breaker: true, "Take", "Take the item out");
										}
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
									}
									else
									{
										if (displayBoxData2.Index == 0)
										{
											Dialog.Text(binaryWriter, breaker: true, "This display box contains nothing.", 50);
										}
										else
										{
											Dialog.Text(binaryWriter, breaker: true, $"This display box contains ~1{Item.Name(displayBoxData2.Index)}~0.", 50);
										}
										Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
									}
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								else if (Session.TileAccess(Profile.Data.Filename, num24))
								{
									if (Session.Data.Foreground[num24] == 69 || Session.Data.Foreground[num24] == 201 || Session.Data.Foreground[num24] == 509)
									{
										SignData signData = Session.GetSignData(num24);
										if (signData.Text == null)
										{
											signData.Text = "";
										}
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Wrench.Sign");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Sign options", 75, Session.Data.Foreground[num24]);
										Dialog.Text(binaryWriter, breaker: true, "What should this sign say?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Text", signData.Text, 64);
										Dialog.Text(binaryWriter, breaker: true, "Offensive messages will be removed and may get you banned.", 25);
										Dialog.Button(binaryWriter, breaker: false, "Update", "Update");
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (Session.Data.Foreground[num24] == 71 || Session.Data.Foreground[num24] == 203 || Session.Data.Foreground[num24] == 511)
									{
										DoorData doorData = Session.GetDoorData(num24);
										if (doorData.Target == null)
										{
											doorData.Target = "";
										}
										if (doorData.World == null)
										{
											doorData.World = "";
										}
										if (doorData.Name == null)
										{
											doorData.Name = "";
										}
										if (doorData.Password == null)
										{
											doorData.Password = "";
										}
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Wrench.Door");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Door options", 75, Session.Data.Foreground[num24]);
										Dialog.Text(binaryWriter, breaker: true, "What's the target world name?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "World", doorData.World, 32);
										Dialog.Text(binaryWriter, breaker: true, "Leave empty if target door is in this world.", 25);
										Dialog.Text(binaryWriter, breaker: true, "What's the name of target door?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Target", doorData.Target, 16);
										Dialog.Text(binaryWriter, breaker: true, "What's the name of this door?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Name", doorData.Name, 16);
										Dialog.Text(binaryWriter, breaker: true, "What's the password to enter?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Password", doorData.Password, 32);
										Dialog.Text(binaryWriter, breaker: true, "Leave empty if you want no password.", 25);
										Dialog.Checkbox(binaryWriter, breaker: true, doorData.Public, "Public", "Public", 75);
										Dialog.Button(binaryWriter, breaker: false, "Update", "Update");
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (Session.Data.Foreground[num24] == 155)
									{
										FurnaceData furnaceData = Session.GetFurnaceData(num24);
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Wrench.Furnace");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Furnace", 75, 155);
										if (Session.Data.Special[num24] == null)
										{
											Dialog.Button(binaryWriter, breaker: true, "Polyethylene", "Polyethylene");
											Dialog.Button(binaryWriter, breaker: true, "PlasticWings", "Plastic wings");
											Dialog.Button(binaryWriter, breaker: true, "GoldenPlasticWings", "Golden plastic wings");
											Dialog.Button(binaryWriter, breaker: true, "DiamondPlasticWings", "Diamond plastic wings");
											Dialog.Button(binaryWriter, breaker: true, "EmeraldPlasticWings", "Emerald plastic wings");
											Dialog.Button(binaryWriter, breaker: true, "RubyPlasticWings", "Ruby plastic wings");
											Dialog.Button(binaryWriter, breaker: true, "GoldenRustyWings", "Golden rusty wings");
										}
										else if (Item.Harvestable(furnaceData.Time))
										{
											Dialog.Button(binaryWriter, breaker: true, "Collect", "Collect");
										}
										else
										{
											TimeSpan timeSpan3 = TimeSpan.FromSeconds(Item.GetHarvest(furnaceData.Time));
											ArrayList arrayList = new ArrayList();
											float num25 = timeSpan3.Days;
											float num26 = timeSpan3.Hours;
											float num27 = timeSpan3.Minutes;
											float num28 = timeSpan3.Seconds;
											if (num25 > 0f)
											{
												arrayList.Add($"{num25}d");
											}
											if (num26 > 0f)
											{
												arrayList.Add($"{num26}h");
											}
											if (num27 > 0f)
											{
												arrayList.Add($"{num27}m");
											}
											if (num28 > 0f)
											{
												arrayList.Add($"{num28}s");
											}
											string text16 = string.Join(" ", arrayList.ToArray());
											Dialog.Text(binaryWriter, breaker: true, "Ready in ~1" + text16, 50);
										}
										Dialog.Button(binaryWriter, breaker: true, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (Session.Data.Foreground[num24] == 195 || Session.Data.Foreground[num24] == 205)
									{
										EntranceData entranceData = Session.GetEntranceData(num24);
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Wrench.Entrance");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Entrance options", 75, Session.Data.Foreground[num24]);
										Dialog.Checkbox(binaryWriter, breaker: true, entranceData.Closed, "Closed", "Closed for others", 75);
										Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (Session.Data.Foreground[num24] == 207 || Session.Data.Foreground[num24] == 209 || Session.Data.Foreground[num24] == 211 || Session.Data.Foreground[num24] == 213)
									{
										PortalData portalData = Session.GetPortalData(num24);
										if (portalData.Target == null)
										{
											portalData.Target = "";
										}
										if (portalData.World == null)
										{
											portalData.World = "";
										}
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Wrench.Portal");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Portal options", 75, Session.Data.Foreground[num24]);
										Dialog.Text(binaryWriter, breaker: true, "What's the target world name?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "World", portalData.World, 32);
										Dialog.Text(binaryWriter, breaker: true, "Leave empty if target door is in this world.", 25);
										Dialog.Text(binaryWriter, breaker: true, "What's the name of target door?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Target", portalData.Target, 16);
										Dialog.Checkbox(binaryWriter, breaker: true, portalData.Public, "Public", "Public", 75);
										Dialog.Button(binaryWriter, breaker: false, "Update", "Update");
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (Session.Data.Foreground[num24] == 253 || Session.Data.Foreground[num24] == 443 || Session.Data.Foreground[num24] == 1087 || Session.Data.Foreground[num24] == 1089 || Session.Data.Foreground[num24] == 1091 || Session.Data.Foreground[num24] == 1093)
									{
										LampData lampData = Session.GetLampData(num24);
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Wrench.Lamp");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Lamp properties", 75, Session.Data.Foreground[num24]);
										Dialog.Checkbox(binaryWriter, breaker: true, lampData.On, "On", "Lamp is on", 75);
										Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (Session.Data.Foreground[num24] == 419)
									{
										TrafficLightData trafficLightData = Session.GetTrafficLightData(num24);
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Wrench.TrafficLight");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Traffic Light options", 75, Session.Data.Foreground[num24]);
										if (trafficLightData.Light != 1)
										{
											Dialog.Button(binaryWriter, breaker: true, "Red", "Red light");
										}
										if (trafficLightData.Light != 2)
										{
											Dialog.Button(binaryWriter, breaker: true, "Yellow", "Yellow light");
										}
										if (trafficLightData.Light != 3)
										{
											Dialog.Button(binaryWriter, breaker: true, "Green", "Green light");
										}
										if (trafficLightData.Light != 0)
										{
											Dialog.Button(binaryWriter, breaker: true, "Off", "Light off");
										}
										Dialog.Button(binaryWriter, breaker: true, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (Session.Data.Foreground[num24] == 439)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Wrench.ElectricPole");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Pole options", 75, Session.Data.Foreground[num24]);
										Dialog.Text(binaryWriter, breaker: true, "Pole ID: ~1" + num24, 50);
										Dialog.Button(binaryWriter, breaker: true, "ConnectWire", "Connect wire");
										Dialog.Button(binaryWriter, breaker: true, "DisconnectWire", "Disconnect wire");
										Dialog.Button(binaryWriter, breaker: true, "ViewNetwork", "View network");
										Dialog.Button(binaryWriter, breaker: true, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (Session.Data.Foreground[num24] == 441)
									{
										SignalSenderData signalSenderData = Session.GetSignalSenderData(num24);
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Wrench.SignalButton");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Button options", 75, Session.Data.Foreground[num24]);
										Dialog.Text(binaryWriter, breaker: true, "Unit ID: ~1" + num24, 50);
										Dialog.Text(binaryWriter, breaker: true, "What signal should this send?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Signal", signalSenderData.Signal.ToString(), 8);
										Dialog.Checkbox(binaryWriter, breaker: true, signalSenderData.Public, "Public", "Public", 75);
										Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (Session.Data.Foreground[num24] == 485 || Session.Data.Foreground[num24] == 487 || Session.Data.Foreground[num24] == 489 || Session.Data.Foreground[num24] == 491)
									{
										MusicBlockData musicBlockData = Session.GetMusicBlockData(num24);
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Wrench.MusicBlock");
										Dialog.ItemText(binaryWriter, breaker: true, $"~1{Item.Name(Session.Data.Foreground[num24])} options", 75, Session.Data.Foreground[num24]);
										Dialog.Text(binaryWriter, breaker: true, "What sound should this play?", 50);
										if (musicBlockData.Sound == 0)
										{
											Dialog.Textbox(binaryWriter, breaker: true, "Sound", "A", 8);
										}
										else if (musicBlockData.Sound == 1)
										{
											Dialog.Textbox(binaryWriter, breaker: true, "Sound", "B", 8);
										}
										else if (musicBlockData.Sound == 2)
										{
											Dialog.Textbox(binaryWriter, breaker: true, "Sound", "C", 8);
										}
										else if (musicBlockData.Sound == 3)
										{
											Dialog.Textbox(binaryWriter, breaker: true, "Sound", "D", 8);
										}
										else if (musicBlockData.Sound == 4)
										{
											Dialog.Textbox(binaryWriter, breaker: true, "Sound", "E", 8);
										}
										else if (musicBlockData.Sound == 5)
										{
											Dialog.Textbox(binaryWriter, breaker: true, "Sound", "F", 8);
										}
										else if (musicBlockData.Sound == 5)
										{
											Dialog.Textbox(binaryWriter, breaker: true, "Sound", "G", 8);
										}
										else
										{
											Dialog.Textbox(binaryWriter, breaker: true, "Sound", string.Empty, 8);
										}
										Dialog.Text(binaryWriter, breaker: true, "This can be A, B, C, D, E, F or G.", 25);
										Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (Session.Data.Foreground[num24] == 557)
									{
										SmokehouseData smokehouseData2 = Session.GetSmokehouseData(num24);
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Wrench.Smokehouse");
										Dialog.ItemText(binaryWriter, breaker: true, "~1Smokehouse", 75, Session.Data.Foreground[num24]);
										if (smokehouseData2.Index == 0 && smokehouseData2.Index == 0)
										{
											Dialog.Text(binaryWriter, breaker: true, "This smokehouse has no fish in it.", 50);
											Dialog.Text(binaryWriter, breaker: true, "What type of fish would you like to place in?", 50);
											Dialog.ItemPicker(binaryWriter, breaker: true, "Index", 0, 64, 64);
											Dialog.Text(binaryWriter, breaker: true, "How much would you like to place in?", 50);
											Dialog.Textbox(binaryWriter, breaker: true, "Count", "", 8);
											Dialog.Button(binaryWriter, breaker: true, "Place", "Add this item");
											Dialog.Button(binaryWriter, breaker: true, "Cancel", "Cancel");
										}
										else if (Item.Harvestable(smokehouseData2.Time))
										{
											Dialog.Text(binaryWriter, breaker: true, "Alright, fish inside is now ready to", 50);
											Dialog.Text(binaryWriter, breaker: true, "be sold, would you like to sell it?", 50);
											Dialog.Button(binaryWriter, breaker: true, "Sell", "Yes, sell it");
											Dialog.Button(binaryWriter, breaker: true, "Cancel", "Cancel");
										}
										else
										{
											Dialog.Text(binaryWriter, breaker: true, "Your fish preparation is still in progress,", 50);
											Dialog.Text(binaryWriter, breaker: true, $"please come back in {Text.Time(Item.GetHarvest(smokehouseData2.Time))}.", 50);
											Dialog.Button(binaryWriter, breaker: true, "Okay", "Okay");
										}
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (Session.Data.Foreground[num24] == 597)
									{
										GameJoinData gameJoinData = Session.GetGameJoinData(num24);
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Wrench.GameJoin");
										Dialog.ItemText(binaryWriter, breaker: true, "~1" + Item.Name(597), 75, 597);
										Dialog.Text(binaryWriter, breaker: true, "What's the game ID?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Game", gameJoinData.Game.ToString(), 8);
										Dialog.Text(binaryWriter, breaker: true, "What's the game size?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Size", gameJoinData.Size.ToString(), 8);
										Dialog.Text(binaryWriter, breaker: true, "What's the block color?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Color", gameJoinData.Color.ToString(), 8);
										Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (Session.Data.Foreground[num24] == 599)
									{
										GameSpawnData gameSpawnData = Session.GetGameSpawnData(num24);
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Wrench.GameSpawn");
										Dialog.ItemText(binaryWriter, breaker: true, "~1" + Item.Name(599), 75, 599);
										Dialog.Text(binaryWriter, breaker: true, "What's the game ID?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Game", gameSpawnData.Game.ToString(), 8);
										Dialog.Text(binaryWriter, breaker: true, "What's the block color?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Color", gameSpawnData.Color.ToString(), 8);
										Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (Session.Data.Foreground[num24] == 601)
									{
										GameFinishData gameFinishData = Session.GetGameFinishData(num24);
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Wrench.GameFinish");
										Dialog.ItemText(binaryWriter, breaker: true, "~1" + Item.Name(601), 75, 601);
										Dialog.Text(binaryWriter, breaker: true, "What's the game ID?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Game", gameFinishData.Game.ToString(), 8);
										Dialog.Text(binaryWriter, breaker: true, "What's the block color?", 50);
										Dialog.Textbox(binaryWriter, breaker: true, "Color", gameFinishData.Color.ToString(), 8);
										Dialog.Button(binaryWriter, breaker: false, "Accept", "Accept");
										Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (Session.Data.Foreground[num24] == 899)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(5));
										Window = Dialog.Create(binaryWriter, num24, "Message");
										Dialog.ItemText(binaryWriter, breaker: true, "~1" + Item.Name(899), 75, 899);
										DroppedItem[] array4 = Session.Data.Drop.ToArray();
										for (int m = 0; m < array4.Length; m++)
										{
											DroppedItem droppedItem = array4[m];
											int num29 = (droppedItem.X + 10) / 32;
											int num30 = (droppedItem.Y + 10) / 32;
											if (num29 == num9 && num30 == num10)
											{
												Dialog.ItemText(binaryWriter, breaker: true, $"x{droppedItem.Count} {Item.Name(droppedItem.Index)}", 50, droppedItem.Index);
											}
										}
										Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (Session.Data.Foreground[num24] == 1143)
									{
										data2 = Session.GetHalloweenEnemyData(num24);
										if (data2.Target == null)
										{
											data2.Target = "";
										}
										PlayerCore.UpdateDialog(this, num24, "Wrench.Halloween.Enemy", delegate(BinaryWriter dialog)
										{
											Dialog.ItemText(dialog, breaker: true, "~1Enemy properties", 75, 1143);
											Dialog.Text(dialog, breaker: true, "What's the image ID? (1-100)", 50);
											Dialog.Textbox(dialog, breaker: true, "Image", data2.Image.ToString(), 32);
											Dialog.Text(dialog, breaker: true, "How many candies to take (1-500)?", 50);
											Dialog.Textbox(dialog, breaker: true, "Candies", data2.Candies.ToString(), 32);
											Dialog.Text(dialog, breaker: true, "Which door should it take the player to?", 50);
											Dialog.Textbox(dialog, breaker: true, "Target", data2.Target, 32);
											Dialog.Button(dialog, breaker: false, "Accept", "Accept");
											Dialog.Button(dialog, breaker: false, "Cancel", "Cancel");
										});
									}
								}
								else if (Session.Data.Owner.Length > 0)
								{
									PlayerLayout.Notification(this, 100, 73, "This world is locked by ~1{0}~0.", Session.Data.Owner);
								}
								else if (Session.Data.Parent[num24] != null)
								{
									Parent parent2 = (Parent)Session.Data.Parent[num24];
									LockData lockData3 = Session.GetLockData(parent2.X + parent2.Y * Session.Data.SizeX);
									if (lockData3.Owner != null)
									{
										PlayerLayout.Notification(this, 100, 319, "This area is locked by ~1{0}~0.", lockData3.Owner);
									}
								}
							}
							else
							{
								int sizeX4 = Session.Data.SizeX;
								int sizeY4 = Session.Data.SizeY;
								int num31 = num9 + num10 * sizeX4;
								bool flag = false;
								if (Item.Solid(num7))
								{
									Player[] array5 = Server.Online.ToArray();
									foreach (Player player6 in array5)
									{
										if (player6.Active && player6.Profile.Active && player6.Session.Active && !(player6.Session.Data.Name != Session.Data.Name) && player6.Profile.VisibleTo(this))
										{
											bool flag2 = num9 * 32 < player6.CurrentX + 16 && num9 * 32 + 32 > player6.CurrentX;
											bool flag3 = num10 * 32 < player6.CurrentY + 32 && num10 * 32 + 32 > player6.CurrentY;
											if (flag2 && flag3)
											{
												flag = true;
											}
										}
									}
								}
								if (!flag || !Item.Solid(num7))
								{
									if (Item.Blocked(num7) && !Session.ValidateBuilding(CurrentX, CurrentY, num9, num10, Profile))
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										string text17 = "Something is blocking the way to build this here.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text17 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (Item.Type(num7) == 1)
									{
										if (Session.Data.Background[num31] != num7)
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(30));
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(CurrentX + 8));
											binaryWriter.Write(Convert.ToUInt16(CurrentY + 8));
											binaryWriter.Write(Convert.ToUInt16(num9 * 32 + 16));
											binaryWriter.Write(Convert.ToUInt16(num10 * 32 + 16));
											binaryWriter.Write(Convert.ToUInt16(num7));
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Server.Broadcast(input.ToArray(), delegate(Player player)
											{
												if (player == this)
												{
													return true;
												}
												if (!player.Active)
												{
													return false;
												}
												if (!player.Profile.Active)
												{
													return false;
												}
												if (!player.Session.Active)
												{
													return false;
												}
												if (player.Session.Data.Name != Session.Data.Name)
												{
													return false;
												}
												return Profile.VisibleTo(player) ? true : false;
											});
											binaryWriter.Close();
										}
										if (!Session.TileAccess(Profile.Data.Filename, num31))
										{
											if (Session.Data.Owner.Length > 0)
											{
												PlayerLayout.Notification(this, 100, 73, "This world is locked by ~1{0}~0.", Session.Data.Owner);
											}
											else if (Session.Data.Parent[num31] != null)
											{
												Parent parent3 = (Parent)Session.Data.Parent[num31];
												LockData lockData4 = Session.GetLockData(parent3.X + parent3.Y * Session.Data.SizeX);
												if (lockData4.Owner != null)
												{
													PlayerLayout.Notification(this, 100, 319, "This area is locked by ~1{0}~0.", lockData4.Owner);
												}
											}
										}
										else if (Session.Data.Background[num31] != num7)
										{
											Profile.Data.QuestLeft -= Quest.BuildBackground(Profile.Data.QuestType, Profile.Data.QuestItem, num7);
											Challenge.BuildBackground(Profile.Data.Filename, num7);
											Achievement(AchievementType.BuildBackground, increase: true, 1);
											PlayerQuests.Event(this, PlayerEvent.Build, num7);
											Profile.SetItem(num7, Profile.GetItem(num7) - 1);
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(6));
											Profile.WriteItems(binaryWriter);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
											Session.Data.Background[num31] = num7;
											Database.ProfileSave(Profile.Data);
											Database.SessionSave(Session.Data);
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(11));
											binaryWriter.Write(Convert.ToUInt16(num9));
											binaryWriter.Write(Convert.ToUInt16(num10));
											binaryWriter.Write(Convert.ToUInt16(1));
											binaryWriter.Write(Convert.ToUInt16(num7));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Server.Broadcast(input.ToArray(), delegate(Player player)
											{
												if (!player.Active)
												{
													return false;
												}
												if (!player.Profile.Active)
												{
													return false;
												}
												if (!player.Session.Active)
												{
													return false;
												}
												return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
											});
											binaryWriter.Close();
										}
									}
									else if (Item.Type(num7) == 2)
									{
										if (Session.Data.Foreground[num31] == 0)
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(30));
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(CurrentX + 8));
											binaryWriter.Write(Convert.ToUInt16(CurrentY + 8));
											binaryWriter.Write(Convert.ToUInt16(num9 * 32 + 16));
											binaryWriter.Write(Convert.ToUInt16(num10 * 32 + 16));
											binaryWriter.Write(Convert.ToUInt16(num7));
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Server.Broadcast(input.ToArray(), delegate(Player player)
											{
												if (player == this)
												{
													return true;
												}
												if (!player.Active)
												{
													return false;
												}
												if (!player.Profile.Active)
												{
													return false;
												}
												if (!player.Session.Active)
												{
													return false;
												}
												if (player.Session.Data.Name != Session.Data.Name)
												{
													return false;
												}
												return Profile.VisibleTo(player) ? true : false;
											});
											binaryWriter.Close();
										}
										if (!Session.TileAccess(Profile.Data.Filename, num31))
										{
											if (Session.Data.Owner.Length > 0)
											{
												PlayerLayout.Notification(this, 100, 73, "This world is locked by ~1{0}~0.", Session.Data.Owner);
											}
											else if (Session.Data.Parent[num31] != null)
											{
												Parent parent4 = (Parent)Session.Data.Parent[num31];
												LockData lockData5 = Session.GetLockData(parent4.X + parent4.Y * Session.Data.SizeX);
												if (lockData5.Owner != null)
												{
													PlayerLayout.Notification(this, 100, 319, "This area is locked by ~1{0}~0.", lockData5.Owner);
												}
											}
										}
										else if (Session.Data.Foreground[num31] == 0)
										{
											if (num7 == 5 && !Rewards.Permission(Profile.Data.Filename, Permissions.Admin))
											{
												PlayerConsole.Message(this, "It's impossible to place a bedrock block, rather trash it.");
											}
											else if (num7 == 7)
											{
												if (Session.Data.Foreground[num31] != 0 || Session.Data.Foreground[num31 + Session.Data.SizeX] != 0)
												{
													PlayerConsole.Message(this, "You need to find two empty tiles in order to move the world entrance.");
												}
												else if (!Session.WorldOwner(Profile.Data.Filename))
												{
													PlayerConsole.Message(this, "This world must be fully owned by you before you can move the world entrance.");
												}
												else
												{
													int spawn = Session.GetSpawn();
													int tileX = Session.GetTileX(spawn);
													int tileY = Session.GetTileY(spawn);
													Profile.SetItem(num7, Profile.GetItem(num7) - 1);
													input = new MemoryStream();
													BinaryWriter binaryWriter = new BinaryWriter(input);
													binaryWriter.Write(Convert.ToUInt16(0));
													binaryWriter.Write(Convert.ToUInt16(6));
													Profile.WriteItems(binaryWriter);
													binaryWriter.Seek(0, SeekOrigin.Begin);
													binaryWriter.Write(Convert.ToUInt16(input.Length));
													Send(input.ToArray());
													binaryWriter.Close();
													if (Session.Data.Foreground[tileX + tileY * Session.Data.SizeX] == 7)
													{
														Session.Data.Foreground[tileX + tileY * Session.Data.SizeX] = 0;
														input = new MemoryStream();
														binaryWriter = new BinaryWriter(input);
														binaryWriter.Write(Convert.ToUInt16(0));
														binaryWriter.Write(Convert.ToUInt16(11));
														binaryWriter.Write(Convert.ToUInt16(tileX));
														binaryWriter.Write(Convert.ToUInt16(tileY));
														binaryWriter.Write(Convert.ToUInt16(2));
														binaryWriter.Write(Convert.ToUInt16(0));
														binaryWriter.Seek(0, SeekOrigin.Begin);
														binaryWriter.Write(Convert.ToUInt16(input.Length));
														Server.Broadcast(input.ToArray(), delegate(Player player)
														{
															if (!player.Active)
															{
																return false;
															}
															if (!player.Profile.Active)
															{
																return false;
															}
															if (!player.Session.Active)
															{
																return false;
															}
															return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
														});
														binaryWriter.Close();
													}
													if (Session.Data.Foreground[tileX + tileY * Session.Data.SizeX + Session.Data.SizeX] == 5)
													{
														Session.Data.Foreground[tileX + tileY * Session.Data.SizeX + Session.Data.SizeX] = 0;
														input = new MemoryStream();
														binaryWriter = new BinaryWriter(input);
														binaryWriter.Write(Convert.ToUInt16(0));
														binaryWriter.Write(Convert.ToUInt16(11));
														binaryWriter.Write(Convert.ToUInt16(tileX));
														binaryWriter.Write(Convert.ToUInt16(tileY + 1));
														binaryWriter.Write(Convert.ToUInt16(2));
														binaryWriter.Write(Convert.ToUInt16(0));
														binaryWriter.Seek(0, SeekOrigin.Begin);
														binaryWriter.Write(Convert.ToUInt16(input.Length));
														Server.Broadcast(input.ToArray(), delegate(Player player)
														{
															if (!player.Active)
															{
																return false;
															}
															if (!player.Profile.Active)
															{
																return false;
															}
															if (!player.Session.Active)
															{
																return false;
															}
															return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
														});
														binaryWriter.Close();
													}
													if (Session.Data.Foreground[num9 + num10 * Session.Data.SizeX] != 7)
													{
														Session.Data.Foreground[num9 + num10 * Session.Data.SizeX] = 7;
														input = new MemoryStream();
														binaryWriter = new BinaryWriter(input);
														binaryWriter.Write(Convert.ToUInt16(0));
														binaryWriter.Write(Convert.ToUInt16(11));
														binaryWriter.Write(Convert.ToUInt16(num9));
														binaryWriter.Write(Convert.ToUInt16(num10));
														binaryWriter.Write(Convert.ToUInt16(2));
														binaryWriter.Write(Convert.ToUInt16(7));
														binaryWriter.Seek(0, SeekOrigin.Begin);
														binaryWriter.Write(Convert.ToUInt16(input.Length));
														Server.Broadcast(input.ToArray(), delegate(Player player)
														{
															if (!player.Active)
															{
																return false;
															}
															if (!player.Profile.Active)
															{
																return false;
															}
															if (!player.Session.Active)
															{
																return false;
															}
															return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
														});
														binaryWriter.Close();
													}
													if (Session.Data.Foreground[num9 + (num10 + 1) * Session.Data.SizeX] != 5)
													{
														Session.Data.Foreground[num9 + (num10 + 1) * Session.Data.SizeX] = 5;
														input = new MemoryStream();
														binaryWriter = new BinaryWriter(input);
														binaryWriter.Write(Convert.ToUInt16(0));
														binaryWriter.Write(Convert.ToUInt16(11));
														binaryWriter.Write(Convert.ToUInt16(num9));
														binaryWriter.Write(Convert.ToUInt16(num10 + 1));
														binaryWriter.Write(Convert.ToUInt16(2));
														binaryWriter.Write(Convert.ToUInt16(5));
														binaryWriter.Seek(0, SeekOrigin.Begin);
														binaryWriter.Write(Convert.ToUInt16(input.Length));
														Server.Broadcast(input.ToArray(), delegate(Player player)
														{
															if (!player.Active)
															{
																return false;
															}
															if (!player.Profile.Active)
															{
																return false;
															}
															if (!player.Session.Active)
															{
																return false;
															}
															return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
														});
														binaryWriter.Close();
													}
													input = new MemoryStream();
													binaryWriter = new BinaryWriter(input);
													binaryWriter.Write(Convert.ToUInt16(0));
													binaryWriter.Write(Convert.ToUInt16(4));
													string text18 = $"~0[~1{Profile.Data.Username}~0 has changed location of the world entrance]";
													binaryWriter.Write(Encoding.UTF8.GetBytes(text18 + "\0"));
													binaryWriter.Seek(0, SeekOrigin.Begin);
													binaryWriter.Write(Convert.ToUInt16(input.Length));
													Server.Broadcast(input.ToArray(), delegate(Player player)
													{
														if (!player.Active)
														{
															return false;
														}
														if (!player.Profile.Active)
														{
															return false;
														}
														if (!player.Session.Active)
														{
															return false;
														}
														return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
													});
													binaryWriter.Close();
													Database.ProfileSave(Profile.Data);
													Database.SessionSave(Session.Data);
												}
											}
											else if (num7 == 73 || num7 == 493)
											{
												Database.SessionLoad(ref Session.Data, Session.Data.Name);
												if (Session.Data.Owner != string.Empty)
												{
													PlayerConsole.Message(this, "This world is already locked.");
												}
												else if (Session.OtherLocks(Profile.Data.Filename))
												{
													PlayerConsole.Message(this, "Other players must remove their locks before you can lock this world.");
												}
												else
												{
													Achievement(AchievementType.LockWorld, increase: true, 1);
													Session.Data.Owner = Profile.Data.Filename;
													Database.SessionSave(Session.Data);
													input = new MemoryStream();
													BinaryWriter binaryWriter = new BinaryWriter(input);
													binaryWriter.Write(Convert.ToUInt16(0));
													binaryWriter.Write(Convert.ToUInt16(4));
													string text19 = $"~0[~1{Profile.Data.Username}~0 has locked this world]";
													binaryWriter.Write(Encoding.UTF8.GetBytes(text19 + "\0"));
													binaryWriter.Seek(0, SeekOrigin.Begin);
													binaryWriter.Write(Convert.ToUInt16(input.Length));
													Server.Broadcast(input.ToArray(), delegate(Player player)
													{
														if (!player.Active)
														{
															return false;
														}
														if (!player.Profile.Active)
														{
															return false;
														}
														if (!player.Session.Active)
														{
															return false;
														}
														return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
													});
													binaryWriter.Close();
													Profile.SetItem(num7, Profile.GetItem(num7) - 1);
													input = new MemoryStream();
													binaryWriter = new BinaryWriter(input);
													binaryWriter.Write(Convert.ToUInt16(0));
													binaryWriter.Write(Convert.ToUInt16(6));
													Profile.WriteItems(binaryWriter);
													binaryWriter.Seek(0, SeekOrigin.Begin);
													binaryWriter.Write(Convert.ToUInt16(input.Length));
													Send(input.ToArray());
													binaryWriter.Close();
													Session.Data.Foreground[num31] = num7;
													input = new MemoryStream();
													binaryWriter = new BinaryWriter(input);
													binaryWriter.Write(Convert.ToUInt16(0));
													binaryWriter.Write(Convert.ToUInt16(11));
													binaryWriter.Write(Convert.ToUInt16(num9));
													binaryWriter.Write(Convert.ToUInt16(num10));
													binaryWriter.Write(Convert.ToUInt16(2));
													binaryWriter.Write(Convert.ToUInt16(num7));
													binaryWriter.Seek(0, SeekOrigin.Begin);
													binaryWriter.Write(Convert.ToUInt16(input.Length));
													Server.Broadcast(input.ToArray(), delegate(Player player)
													{
														if (!player.Active)
														{
															return false;
														}
														if (!player.Profile.Active)
														{
															return false;
														}
														if (!player.Session.Active)
														{
															return false;
														}
														return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
													});
													binaryWriter.Close();
													if (!Profile.Data.Worlds.Contains(Session.Data.Name))
													{
														Profile.Data.Worlds.Add(Session.Data.Name);
													}
													Database.ProfileSave(Profile.Data);
													Database.SessionSave(Session.Data);
													Player[] array6 = Server.Online.ToArray();
													foreach (Player player7 in array6)
													{
														if (player7.Active && player7.Profile.Active && player7.Session.Active && !(player7.Session.Data.Name != Session.Data.Name))
														{
															input = new MemoryStream();
															binaryWriter = new BinaryWriter(input);
															binaryWriter.Write(Convert.ToUInt16(0));
															binaryWriter.Write(Convert.ToUInt16(10));
															player7.Session.WriteTiles(binaryWriter, player7.Profile.Data.Filename);
															binaryWriter.Seek(0, SeekOrigin.Begin);
															binaryWriter.Write(Convert.ToUInt16(input.Length));
															player7.Send(input.ToArray());
															binaryWriter.Close();
														}
													}
												}
											}
											else if (num7 == 319 || num7 == 321 || num7 == 323)
											{
												Database.SessionLoad(ref Session.Data, Session.Data.Name);
												if (Session.Data.Owner.Length > 0 && !Session.WorldOwner(Profile.Data.Filename))
												{
													PlayerConsole.Message(this, "Only the owner can place locks in this world.");
												}
												else if (!Session.TileOwner(Profile.Data.Filename, num31))
												{
													PlayerConsole.Message(this, "Only the owner can place locks in this area.");
												}
												else if (Session.Data.Parent[num31] != null)
												{
													PlayerConsole.Message(this, "This tile is already occupied by another lock.");
												}
												else
												{
													switch (num7)
													{
													case 319:
														Session.LockCapacity = 9;
														break;
													case 321:
														Session.LockCapacity = 49;
														break;
													case 323:
														Session.LockCapacity = 225;
														break;
													default:
														Session.LockCapacity = 0;
														break;
													}
													Session.LockRectangle(new Parent
													{
														X = num9,
														Y = num10
													}, num9, num10, Session.LockCapacity);
													Session.Data.Special[num31] = new LockData
													{
														Owner = Profile.Data.Filename,
														Admin = new ArrayList()
													};
													Database.SessionSave(Session.Data);
													Profile.SetItem(num7, Profile.GetItem(num7) - 1);
													input = new MemoryStream();
													BinaryWriter binaryWriter = new BinaryWriter(input);
													binaryWriter.Write(Convert.ToUInt16(0));
													binaryWriter.Write(Convert.ToUInt16(6));
													Profile.WriteItems(binaryWriter);
													binaryWriter.Seek(0, SeekOrigin.Begin);
													binaryWriter.Write(Convert.ToUInt16(input.Length));
													Send(input.ToArray());
													binaryWriter.Close();
													Session.Data.Foreground[num31] = num7;
													input = new MemoryStream();
													binaryWriter = new BinaryWriter(input);
													binaryWriter.Write(Convert.ToUInt16(0));
													binaryWriter.Write(Convert.ToUInt16(11));
													binaryWriter.Write(Convert.ToUInt16(num9));
													binaryWriter.Write(Convert.ToUInt16(num10));
													binaryWriter.Write(Convert.ToUInt16(2));
													binaryWriter.Write(Convert.ToUInt16(num7));
													binaryWriter.Seek(0, SeekOrigin.Begin);
													binaryWriter.Write(Convert.ToUInt16(input.Length));
													Server.Broadcast(input.ToArray(), delegate(Player player)
													{
														if (!player.Active)
														{
															return false;
														}
														if (!player.Profile.Active)
														{
															return false;
														}
														if (!player.Session.Active)
														{
															return false;
														}
														return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
													});
													binaryWriter.Close();
													Database.ProfileSave(Profile.Data);
													Database.SessionSave(Session.Data);
													Player[] array7 = Server.Online.ToArray();
													foreach (Player player8 in array7)
													{
														if (player8.Active && player8.Profile.Active && player8.Session.Active && !(player8.Session.Data.Name != Session.Data.Name))
														{
															input = new MemoryStream();
															binaryWriter = new BinaryWriter(input);
															binaryWriter.Write(Convert.ToUInt16(0));
															binaryWriter.Write(Convert.ToUInt16(10));
															player8.Session.WriteTiles(binaryWriter, player8.Profile.Data.Filename);
															binaryWriter.Seek(0, SeekOrigin.Begin);
															binaryWriter.Write(Convert.ToUInt16(input.Length));
															player8.Send(input.ToArray());
															binaryWriter.Close();
														}
													}
												}
											}
											else if (num7 == 495 || num7 == 497 || num7 == 499)
											{
												if (num7 == 495 && Session.Data.AntiPunch)
												{
													PlayerConsole.Message(this, "One {0} already exists in this world, there's no need for another.", Item.Name(num7));
												}
												else if (num7 == 497 && Session.Data.AntiTalk)
												{
													PlayerConsole.Message(this, "One {0} already exists in this world, there's no need for another.", Item.Name(num7));
												}
												else if (num7 == 499 && Session.Data.AntiDrop)
												{
													PlayerConsole.Message(this, "One {0} already exists in this world, there's no need for another.", Item.Name(num7));
												}
												else
												{
													Profile.SetItem(num7, Profile.GetItem(num7) - 1);
													input = new MemoryStream();
													BinaryWriter binaryWriter = new BinaryWriter(input);
													binaryWriter.Write(Convert.ToUInt16(0));
													binaryWriter.Write(Convert.ToUInt16(6));
													Profile.WriteItems(binaryWriter);
													binaryWriter.Seek(0, SeekOrigin.Begin);
													binaryWriter.Write(Convert.ToUInt16(input.Length));
													Send(input.ToArray());
													binaryWriter.Close();
													Session.Data.Foreground[num31] = num7;
													if (num7 == 495)
													{
														Session.Data.AntiPunch = true;
													}
													if (num7 == 497)
													{
														Session.Data.AntiTalk = true;
													}
													if (num7 == 499)
													{
														Session.Data.AntiDrop = true;
													}
													input = new MemoryStream();
													binaryWriter = new BinaryWriter(input);
													binaryWriter.Write(Convert.ToUInt16(0));
													binaryWriter.Write(Convert.ToUInt16(11));
													binaryWriter.Write(Convert.ToUInt16(num9));
													binaryWriter.Write(Convert.ToUInt16(num10));
													binaryWriter.Write(Convert.ToUInt16(2));
													binaryWriter.Write(Convert.ToUInt16(num7));
													binaryWriter.Seek(0, SeekOrigin.Begin);
													binaryWriter.Write(Convert.ToUInt16(input.Length));
													Server.Broadcast(input.ToArray(), delegate(Player player)
													{
														if (!player.Active)
														{
															return false;
														}
														if (!player.Profile.Active)
														{
															return false;
														}
														if (!player.Session.Active)
														{
															return false;
														}
														return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
													});
													binaryWriter.Close();
													Database.ProfileSave(Profile.Data);
													Database.SessionSave(Session.Data);
												}
											}
											else if (num7 == 157 || num7 == 559)
											{
												Profile.SetItem(num7, Profile.GetItem(num7) - 1);
												input = new MemoryStream();
												BinaryWriter binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(6));
												Profile.WriteItems(binaryWriter);
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Send(input.ToArray());
												binaryWriter.Close();
												Session.Data.Foreground[num31] = num7;
												Session.Data.Special[num31] = new ProviderData
												{
													Time = Item.Growtime(num7)
												};
												input = new MemoryStream();
												binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(11));
												binaryWriter.Write(Convert.ToUInt16(num9));
												binaryWriter.Write(Convert.ToUInt16(num10));
												binaryWriter.Write(Convert.ToUInt16(2));
												binaryWriter.Write(Convert.ToUInt16(num7));
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Server.Broadcast(input.ToArray(), delegate(Player player)
												{
													if (!player.Active)
													{
														return false;
													}
													if (!player.Profile.Active)
													{
														return false;
													}
													if (!player.Session.Active)
													{
														return false;
													}
													return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
												});
												binaryWriter.Close();
												Database.ProfileSave(Profile.Data);
												Database.SessionSave(Session.Data);
											}
											else
											{
												Profile.Data.QuestLeft -= Quest.BuildForeground(Profile.Data.QuestType, Profile.Data.QuestItem, num7);
												Challenge.BuildForeground(Profile.Data.Filename, num7);
												Achievement(AchievementType.BuildBackground, increase: true, 1);
												PlayerQuests.Event(this, PlayerEvent.Build, num7);
												Profile.SetItem(num7, Profile.GetItem(num7) - 1);
												input = new MemoryStream();
												BinaryWriter binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(6));
												Profile.WriteItems(binaryWriter);
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Send(input.ToArray());
												binaryWriter.Close();
												Session.Data.Foreground[num31] = num7;
												Session.Data.Special[num31] = null;
												if (Session.Data.Foreground[num31] == 481 || Session.Data.Foreground[num31] == 483)
												{
													input = new MemoryStream();
													binaryWriter = new BinaryWriter(input);
													binaryWriter.Write(Convert.ToUInt16(0));
													binaryWriter.Write(Convert.ToUInt16(28));
													Session.WriteMusic(binaryWriter);
													binaryWriter.Seek(0, SeekOrigin.Begin);
													binaryWriter.Write(Convert.ToUInt16(input.Length));
													Server.Broadcast(input.ToArray(), delegate(Player player)
													{
														if (!player.Active)
														{
															return false;
														}
														if (!player.Profile.Active)
														{
															return false;
														}
														if (!player.Session.Active)
														{
															return false;
														}
														return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
													});
													binaryWriter.Close();
												}
												Database.ProfileSave(Profile.Data);
												Database.SessionSave(Session.Data);
												input = new MemoryStream();
												binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(11));
												binaryWriter.Write(Convert.ToUInt16(num9));
												binaryWriter.Write(Convert.ToUInt16(num10));
												binaryWriter.Write(Convert.ToUInt16(2));
												binaryWriter.Write(Convert.ToUInt16(num7));
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Server.Broadcast(input.ToArray(), delegate(Player player)
												{
													if (!player.Active)
													{
														return false;
													}
													if (!player.Profile.Active)
													{
														return false;
													}
													if (!player.Session.Active)
													{
														return false;
													}
													return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
												});
												binaryWriter.Close();
											}
										}
									}
									else if (Item.Type(num7) == 3)
									{
										if (Session.Data.Foreground[num31] == 0 || Item.Type(Session.Data.Foreground[num31]) == 3)
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(30));
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(CurrentX + 8));
											binaryWriter.Write(Convert.ToUInt16(CurrentY + 8));
											binaryWriter.Write(Convert.ToUInt16(num9 * 32 + 16));
											binaryWriter.Write(Convert.ToUInt16(num10 * 32 + 24));
											binaryWriter.Write(Convert.ToUInt16(num7));
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Server.Broadcast(input.ToArray(), delegate(Player player)
											{
												if (player == this)
												{
													return true;
												}
												if (!player.Active)
												{
													return false;
												}
												if (!player.Profile.Active)
												{
													return false;
												}
												if (!player.Session.Active)
												{
													return false;
												}
												if (player.Session.Data.Name != Session.Data.Name)
												{
													return false;
												}
												return Profile.VisibleTo(player) ? true : false;
											});
											binaryWriter.Close();
										}
										if (!Session.TileAccess(Profile.Data.Filename, num31))
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(17));
											binaryWriter.Write(Convert.ToUInt16(100));
											string text20 = "This area is locked.";
											if (Session.Data.Parent[num31] != null)
											{
												Parent parent5 = (Parent)Session.Data.Parent[num31];
												int num34 = parent5.X + parent5.Y * Session.Data.SizeX;
												LockData lockData6 = Session.GetLockData(num34);
												binaryWriter.Write(Convert.ToUInt16(Session.Data.Foreground[num34]));
												if (lockData6.Owner != null)
												{
													text20 = $"This area is locked by ~1{lockData6.Owner}~0.";
												}
											}
											else if (Session.Data.Owner.Length > 0)
											{
												binaryWriter.Write(Convert.ToUInt16(73));
												text20 = $"This world is locked by ~1{Session.Data.Owner}~0.";
											}
											binaryWriter.Write(Encoding.UTF8.GetBytes(text20 + "\0"));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else if (Session.Data.Foreground[num31 + Session.Data.SizeX] == 0)
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(4));
											string text21 = "It's not really possible to plant a tree in the air.";
											binaryWriter.Write(Encoding.UTF8.GetBytes(text21 + "\0"));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else if (Item.Type(Session.Data.Foreground[num31 + Session.Data.SizeX]) == 3)
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(4));
											string text22 = "A tree won't grow on another tree.";
											binaryWriter.Write(Encoding.UTF8.GetBytes(text22 + "\0"));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
										else if (Session.Data.Foreground[num31] == 0)
										{
											Profile.Data.QuestLeft -= Quest.BuildSeed(Profile.Data.QuestType, Profile.Data.QuestItem, num7);
											Challenge.BuildSeed(Profile.Data.Filename, num7);
											Achievement(AchievementType.PlantSeed, increase: true, 1);
											PlayerQuests.Event(this, PlayerEvent.Plant, num7);
											Profile.SetItem(num7, Profile.GetItem(num7) - 1);
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(6));
											Profile.WriteItems(binaryWriter);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
											Session.Data.Foreground[num31] = num7;
											Session.Data.Special[num31] = new SeedData
											{
												Time = Item.Growtime(num7),
												Spliced = false
											};
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(11));
											binaryWriter.Write(Convert.ToUInt16(num9));
											binaryWriter.Write(Convert.ToUInt16(num10));
											binaryWriter.Write(Convert.ToUInt16(2));
											binaryWriter.Write(Convert.ToUInt16(num7));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Server.Broadcast(input.ToArray(), delegate(Player player)
											{
												if (!player.Active)
												{
													return false;
												}
												if (!player.Profile.Active)
												{
													return false;
												}
												if (!player.Session.Active)
												{
													return false;
												}
												return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
											});
											binaryWriter.Close();
											Database.ProfileSave(Profile.Data);
											Database.SessionSave(Session.Data);
										}
										else if (Item.Type(Session.Data.Foreground[num31]) == 3)
										{
											if (Session.GetSeedData(num31).Spliced)
											{
												input = new MemoryStream();
												BinaryWriter binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(17));
												binaryWriter.Write(Convert.ToUInt16(250));
												binaryWriter.Write(Convert.ToUInt16(num7));
												binaryWriter.Write(Encoding.UTF8.GetBytes("You can only mix two seeds at once\0"));
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Send(input.ToArray());
												binaryWriter.Close();
											}
											else if (Item.Combine(num7, Session.Data.Foreground[num31]) == 0)
											{
												input = new MemoryStream();
												BinaryWriter binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(17));
												binaryWriter.Write(Convert.ToUInt16(250));
												binaryWriter.Write(Convert.ToUInt16(num7));
												binaryWriter.Write(Encoding.UTF8.GetBytes("Those seeds can't be spliced together\0"));
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Send(input.ToArray());
												binaryWriter.Close();
											}
											else
											{
												Profile.Data.QuestLeft -= Quest.SpliceSeed(Profile.Data.QuestType, Profile.Data.QuestItem, Item.Combine(num7, Session.Data.Foreground[num31]));
												Challenge.SpliceSeed(Profile.Data.Filename, Item.Combine(num7, Session.Data.Foreground[num31]));
												Achievement(AchievementType.SpliceSeed, increase: true, 1);
												PlayerQuests.Event(this, PlayerEvent.Splice, Item.Combine(num7, Session.Data.Foreground[num31]));
												Profile.SetItem(num7, Profile.GetItem(num7) - 1);
												input = new MemoryStream();
												BinaryWriter binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(6));
												Profile.WriteItems(binaryWriter);
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Send(input.ToArray());
												binaryWriter.Close();
												num7 = Item.Combine(num7, Session.Data.Foreground[num31]);
												Session.Data.Foreground[num31] = num7;
												Session.Data.Special[num31] = new SeedData
												{
													Time = Item.Growtime(num7),
													Spliced = true
												};
												Database.ProfileSave(Profile.Data);
												Database.SessionSave(Session.Data);
												input = new MemoryStream();
												binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(11));
												binaryWriter.Write(Convert.ToUInt16(num9));
												binaryWriter.Write(Convert.ToUInt16(num10));
												binaryWriter.Write(Convert.ToUInt16(2));
												binaryWriter.Write(Convert.ToUInt16(num7));
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Server.Broadcast(input.ToArray(), delegate(Player player)
												{
													if (!player.Active)
													{
														return false;
													}
													if (!player.Profile.Active)
													{
														return false;
													}
													if (!player.Session.Active)
													{
														return false;
													}
													return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
												});
												binaryWriter.Close();
											}
										}
									}
									else if (Item.Type(num7) == 4)
									{
										input = new MemoryStream();
										BinaryWriter binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(4));
										string text23 = "Wearable items are supposed to be equipped, not placed.";
										binaryWriter.Write(Encoding.UTF8.GetBytes(text23 + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									else if (Item.Type(num7) == 5)
									{
										if (num7 == 289 || num7 == 609)
										{
											SeedData seedData = Session.GetSeedData(num31);
											if (Item.Type(Session.Data.Foreground[num31]) != 3)
											{
												input = new MemoryStream();
												BinaryWriter binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(4));
												string text24 = "You can only use a watering can on a tree.";
												binaryWriter.Write(Encoding.UTF8.GetBytes(text24 + "\0"));
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Send(input.ToArray());
												binaryWriter.Close();
											}
											else if (Item.Harvestable(seedData.Time))
											{
												input = new MemoryStream();
												BinaryWriter binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(4));
												string text25 = "This tree can already be harvested, no need to water it.";
												binaryWriter.Write(Encoding.UTF8.GetBytes(text25 + "\0"));
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Send(input.ToArray());
												binaryWriter.Close();
											}
											else
											{
												int num35 = 0;
												if (num7 == 289)
												{
													num35 = 60;
												}
												if (num7 == 609)
												{
													num35 = 6000;
												}
												Profile.SetItem(num7, Profile.GetItem(num7) - 1);
												Session.Data.Special[num31] = new SeedData
												{
													Spliced = seedData.Spliced,
													Time = seedData.Time - num35
												};
												Database.ProfileSave(Profile.Data);
												Database.SessionSave(Session.Data);
												input = new MemoryStream();
												BinaryWriter binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(4));
												string text26 = $"Used a ~1{Item.Name(num7)}~0, tree will grow up ~1{Text.TimeLong(num35)} ~0earlier.";
												binaryWriter.Write(Encoding.UTF8.GetBytes(text26 + "\0"));
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Send(input.ToArray());
												binaryWriter.Close();
												input = new MemoryStream();
												binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(6));
												Profile.WriteItems(binaryWriter);
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Send(input.ToArray());
												binaryWriter.Close();
											}
										}
										else if (num7 == 537 || num7 == 539 || num7 == 541)
										{
											Profile.SetItem(num7, Profile.GetItem(num7) - 1);
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(6));
											Profile.WriteItems(binaryWriter);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(30));
											binaryWriter.Write(Convert.ToUInt16(1));
											binaryWriter.Write(Convert.ToUInt16(CurrentX + 8));
											binaryWriter.Write(Convert.ToUInt16(CurrentY - 8));
											int val3 = CurrentX + Server.Random.Next(-96, 96);
											int val4 = CurrentY - Server.Random.Next(96, 160);
											val3 = Math.Max(0, Math.Min(val3, Session.Data.SizeX * 32));
											val4 = Math.Max(0, Math.Min(val4, Session.Data.SizeY * 32));
											binaryWriter.Write(Convert.ToUInt16(val3));
											binaryWriter.Write(Convert.ToUInt16(val4));
											if (num7 == 537)
											{
												binaryWriter.Write(Convert.ToUInt16(1));
											}
											if (num7 == 539)
											{
												binaryWriter.Write(Convert.ToUInt16(2));
											}
											if (num7 == 541)
											{
												binaryWriter.Write(Convert.ToUInt16(3));
											}
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Server.Broadcast(input.ToArray(), delegate(Player player)
											{
												if (!player.Active)
												{
													return false;
												}
												if (!player.Profile.Active)
												{
													return false;
												}
												if (!player.Session.Active)
												{
													return false;
												}
												return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
											});
											binaryWriter.Close();
										}
										else if (Item.Bait(num7) != 0 && !Fishing)
										{
											if (Session.Data.Foreground[num31] != 181)
											{
												PlayerConsole.Message(this, "Can't fish there, how would you fish without water?");
											}
											else if (!Item.Rod(Profile.GetPartItem(7)))
											{
												input = new MemoryStream();
												BinaryWriter binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(4));
												string text27 = "Fishing without a rod would be too hard, go find one.";
												binaryWriter.Write(Encoding.UTF8.GetBytes(text27 + "\0"));
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Send(input.ToArray());
												binaryWriter.Close();
											}
											else
											{
												Profile.SetItem(num7, Profile.GetItem(num7) - 1);
												input = new MemoryStream();
												BinaryWriter binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(6));
												Profile.WriteItems(binaryWriter);
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Send(input.ToArray());
												binaryWriter.Close();
												Fishing = true;
												FishingDone = false;
												FishingBait = Item.Bait(num7);
												FishingIdentifier = Server.Random.Next(int.MinValue, int.MaxValue);
												FishingX = num5;
												FishingY = num6;
												FishingTries = 0;
												input = new MemoryStream();
												binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(31));
												binaryWriter.Write(Convert.ToInt32(0));
												binaryWriter.Write(Convert.ToBoolean(value: true));
												binaryWriter.Write(Convert.ToUInt16(FishingX));
												binaryWriter.Write(Convert.ToUInt16(FishingY));
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Send(input.ToArray());
												binaryWriter.Close();
												input = new MemoryStream();
												binaryWriter = new BinaryWriter(input);
												binaryWriter.Write(Convert.ToUInt16(0));
												binaryWriter.Write(Convert.ToUInt16(31));
												binaryWriter.Write(Convert.ToInt32(Identifier));
												binaryWriter.Write(Convert.ToBoolean(1));
												binaryWriter.Write(Convert.ToUInt16(FishingX));
												binaryWriter.Write(Convert.ToUInt16(FishingY));
												binaryWriter.Seek(0, SeekOrigin.Begin);
												binaryWriter.Write(Convert.ToUInt16(input.Length));
												Server.Broadcast(input.ToArray(), delegate(Player player)
												{
													if (player == this)
													{
														return false;
													}
													if (!player.Active)
													{
														return false;
													}
													if (!player.Profile.Active)
													{
														return false;
													}
													if (!player.Session.Active)
													{
														return false;
													}
													return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
												});
												binaryWriter.Close();
												int delay = 60;
												if (Profile.GetPartItem(7) == 545)
												{
													delay = Server.Random.Next(1, 80);
												}
												if (Profile.GetPartItem(7) == 573)
												{
													delay = Server.Random.Next(1, 60);
												}
												PlayerCore.Schedule(this, delay, FishingIdentifier, delegate(Player invoker, int identifier)
												{
													if (invoker.Fishing && !invoker.FishingDone && FishingIdentifier == identifier)
													{
														PlayerConsole.Message(invoker, "You caught a fish, tap anywhere to take it out!");
														PlayerLayout.Notification(invoker, 200, 545, "You caught a fish, tap anywhere to take it out!");
														invoker.FishingDone = true;
													}
												});
											}
										}
										else if (num7 == 903)
										{
											Profile.SetItem(num7, Profile.GetItem(num7) - 1);
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(6));
											Profile.WriteItems(binaryWriter);
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
											input = new MemoryStream();
											binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(36));
											binaryWriter.Write(Convert.ToUInt16(CurrentX + 8));
											binaryWriter.Write(Convert.ToUInt16(CurrentY + 16));
											binaryWriter.Write(Convert.ToUInt16(num5));
											binaryWriter.Write(Convert.ToUInt16(num6));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Server.Broadcast(input.ToArray(), delegate(Player player)
											{
												if (!player.Active)
												{
													return false;
												}
												if (!player.Profile.Active)
												{
													return false;
												}
												if (!player.Session.Active)
												{
													return false;
												}
												return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
											});
											binaryWriter.Close();
										}
										else
										{
											input = new MemoryStream();
											BinaryWriter binaryWriter = new BinaryWriter(input);
											binaryWriter.Write(Convert.ToUInt16(0));
											binaryWriter.Write(Convert.ToUInt16(4));
											string text28 = "It's impossible to place a consumable item as a block.";
											binaryWriter.Write(Encoding.UTF8.GetBytes(text28 + "\0"));
											binaryWriter.Seek(0, SeekOrigin.Begin);
											binaryWriter.Write(Convert.ToUInt16(input.Length));
											Send(input.ToArray());
											binaryWriter.Close();
										}
									}
								}
							}
						}
						if (Profile.Data.QuestType != 0 && Profile.Data.QuestLeft == 0)
						{
							PlayerLayout.Notification(this, 100, 3, "You have completed your quest");
						}
						break;
					}
					case 12:
					{
						int num175 = binaryReader.ReadUInt16();
						int num176 = binaryReader.ReadUInt16();
						int num177 = binaryReader.ReadUInt16();
						int num178 = binaryReader.ReadUInt16();
						binaryReader.Close();
						if (!Profile.Active || !Session.Active)
						{
							break;
						}
						Database.SessionLoad(ref Session.Data, Session.Data.Name);
						int num179 = (num175 + 10) / 32;
						int num180 = (num176 + 10) / 32;
						int num181 = num179 + num180 * Session.Data.SizeX;
						bool flag18 = CurrentX > num175 - 16 - 6 && CurrentX < num175 + 20 + 6;
						bool flag19 = CurrentY > num176 - 32 - 6 && CurrentY < num176 + 20 + 6;
						if (!Loaded || !Profile.Visible || Profile.Noclip || Profile.Frozen || !flag18 || !flag19 || Item.Solid(Session.Data.Foreground[num181]) || (Session.Data.Foreground[num181] == 309 && !Session.TileAccess(Profile.Data.Filename, num181)) || (Session.Data.Foreground[num181] == 899 && !Session.GetChestData(num181).Open))
						{
							break;
						}
						if (!Profile.CanGetItem(num177, 1))
						{
							PlayerLayout.Notification(this, 250, num177, "Not enough space to pick this up.");
							break;
						}
						int num182 = 0;
						input = new MemoryStream();
						BinaryWriter binaryWriter = new BinaryWriter(input);
						binaryWriter.Write(Convert.ToUInt16(0));
						binaryWriter.Write(Convert.ToUInt16(12));
						bool flag20 = Session.Collect(binaryWriter, num177, num178, num175, num176);
						binaryWriter.Seek(0, SeekOrigin.Begin);
						binaryWriter.Write(Convert.ToUInt16(input.Length));
						if (flag20)
						{
							if (Profile.CanGetItem(num177, num178))
							{
								Profile.SetItem(num177, Profile.GetItem(num177) + num178);
								PlayerConsole.Message(this, "Picked up ~1x{0} {1}~0.", num178, Item.Name(num177));
							}
							else
							{
								num182 = Profile.GetItem(num177) + num178 - Profile.Stack;
								PlayerConsole.Message(this, "Picked up ~1x{0} {1}~0.", num178 - num182, Item.Name(num177));
								Profile.SetItem(num177, Profile.Stack);
							}
							Server.Broadcast(input.ToArray(), delegate(Player player)
							{
								if (!player.Active)
								{
									return false;
								}
								if (!player.Profile.Active)
								{
									return false;
								}
								if (!player.Session.Active)
								{
									return false;
								}
								return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
							});
						}
						binaryWriter.Close();
						input = new MemoryStream();
						binaryWriter = new BinaryWriter(input);
						binaryWriter.Write(Convert.ToUInt16(0));
						binaryWriter.Write(Convert.ToUInt16(6));
						Profile.WriteItems(binaryWriter);
						binaryWriter.Seek(0, SeekOrigin.Begin);
						binaryWriter.Write(Convert.ToUInt16(input.Length));
						Send(input.ToArray());
						binaryWriter.Close();
						if (num182 <= 0)
						{
							break;
						}
						input = new MemoryStream();
						binaryWriter = new BinaryWriter(input);
						binaryWriter.Write(Convert.ToUInt16(0));
						binaryWriter.Write(Convert.ToUInt16(12));
						Session.Drop(binaryWriter, num177, num182, num175, num176);
						binaryWriter.Seek(0, SeekOrigin.Begin);
						binaryWriter.Write(Convert.ToUInt16(input.Length));
						Server.Broadcast(input.ToArray(), delegate(Player player)
						{
							if (!player.Active)
							{
								return false;
							}
							if (!player.Profile.Active)
							{
								return false;
							}
							if (!player.Session.Active)
							{
								return false;
							}
							return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
						});
						binaryWriter.Close();
						break;
					}
					case 13:
					{
						int num41 = binaryReader.ReadUInt16();
						int num42 = binaryReader.ReadUInt16();
						binaryReader.Close();
						if (!Profile.Active || !Session.Active)
						{
							break;
						}
						Database.SessionLoad(ref Session.Data, Session.Data.Name);
						if (!Loaded)
						{
							break;
						}
						if (Session.ValidateMovement(CurrentX, CurrentY, num41, num42, Profile))
						{
							PreviousX = CurrentX;
							PreviousY = CurrentY;
							CurrentX = num41;
							CurrentY = num42;
							input = new MemoryStream();
							BinaryWriter binaryWriter = new BinaryWriter(input);
							binaryWriter.Write(Convert.ToUInt16(0));
							binaryWriter.Write(Convert.ToUInt16(13));
							binaryWriter.Write(Convert.ToInt32(Identifier));
							binaryWriter.Write(Convert.ToBoolean(value: false));
							binaryWriter.Write(Convert.ToUInt16(CurrentX));
							binaryWriter.Write(Convert.ToUInt16(CurrentY));
							binaryWriter.Seek(0, SeekOrigin.Begin);
							binaryWriter.Write(Convert.ToUInt16(input.Length));
							Server.Broadcast(input.ToArray(), delegate(Player player)
							{
								if (player == this)
								{
									return false;
								}
								if (!player.Active)
								{
									return false;
								}
								if (!player.Profile.Active)
								{
									return false;
								}
								if (!player.Session.Active)
								{
									return false;
								}
								return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
							});
							binaryWriter.Close();
							int num43 = (CurrentX + 8) / 32;
							int num44 = (CurrentY + 16) / 32;
							int num45 = num43 + num44 * Session.Data.SizeX;
							int num46 = (PreviousX + 8) / 32;
							int num47 = (PreviousY + 16) / 32;
							int num48 = num46 + num47 * Session.Data.SizeX;
							if (num45 != num48)
							{
								int num49 = Session.Data.Foreground[num45];
								if (Item.Type(num49) == 3)
								{
									SeedData seedData2 = Session.GetSeedData(num45);
									string text30;
									if (Item.Harvestable(seedData2.Time))
									{
										string arg = Item.Name(Item.SeedToItem(Session.Data.Foreground[num45]));
										text30 = $"~1{arg} ~0tree, ready to harvest";
									}
									else
									{
										string arg2 = Item.Name(Item.SeedToItem(Session.Data.Foreground[num45]));
										text30 = $"~1{arg2} ~0tree, will be ready to harvest in ~1{Text.Time(Item.GetHarvest(seedData2.Time))}";
									}
									input = new MemoryStream();
									binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(17));
									binaryWriter.Write(Convert.ToUInt16(100));
									binaryWriter.Write(Convert.ToUInt16(Session.Data.Foreground[num45]));
									binaryWriter.Write(Encoding.UTF8.GetBytes(text30 + "\0"));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								else if (num49 == 7)
								{
									string text31 = "Tap to open the world menu";
									input = new MemoryStream();
									binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(17));
									binaryWriter.Write(Convert.ToUInt16(250));
									binaryWriter.Write(Convert.ToUInt16(Session.Data.Foreground[num45]));
									binaryWriter.Write(Encoding.UTF8.GetBytes(text31 + "\0"));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								else if (num49 == 69 || num49 == 201 || num49 == 509)
								{
									SignData signData2 = Session.GetSignData(num45);
									if (!string.IsNullOrEmpty(signData2.Text))
									{
										input = new MemoryStream();
										binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(17));
										binaryWriter.Write(Convert.ToUInt16(250));
										binaryWriter.Write(Convert.ToUInt16(Session.Data.Foreground[num45]));
										binaryWriter.Write(Encoding.UTF8.GetBytes(signData2.Text + "\0"));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
								}
								else if (num49 == 71 || num49 == 203 || num49 == 511)
								{
									string text32 = "Tap on the door to enter it";
									input = new MemoryStream();
									binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(17));
									binaryWriter.Write(Convert.ToUInt16(250));
									binaryWriter.Write(Convert.ToUInt16(Session.Data.Foreground[num45]));
									binaryWriter.Write(Encoding.UTF8.GetBytes(text32 + "\0"));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								else if (num49 == 207 || num49 == 209 || num49 == 211 || num49 == 213)
								{
									Enter(num45, null);
								}
								else if (num49 == 221 || num49 == 237 || num49 == 433)
								{
									input = new MemoryStream();
									binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(17));
									binaryWriter.Write(Convert.ToUInt16(100));
									binaryWriter.Write(Convert.ToUInt16(Session.Data.Foreground[num45]));
									string text33 = "Wrench the box to get a reward";
									binaryWriter.Write(Encoding.UTF8.GetBytes(text33 + "\0"));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								else if (num49 == 313)
								{
									VendingData vendingData3 = Session.GetVendingData(num45);
									string text34 = "Out of order";
									if (vendingData3.Index != 0 && !vendingData3.Sold)
									{
										text34 = "Sells ~1" + Item.Name(vendingData3.Index);
									}
									input = new MemoryStream();
									binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(17));
									binaryWriter.Write(Convert.ToUInt16(100));
									binaryWriter.Write(Convert.ToUInt16(Session.Data.Foreground[num45]));
									binaryWriter.Write(Encoding.UTF8.GetBytes(text34 + "\0"));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								else if (num49 == 429)
								{
									if (Session.Checkpoint)
									{
										input = new MemoryStream();
										binaryWriter = new BinaryWriter(input);
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Write(Convert.ToUInt16(11));
										binaryWriter.Write(Convert.ToUInt16(Session.CheckpointX));
										binaryWriter.Write(Convert.ToUInt16(Session.CheckpointY));
										binaryWriter.Write(Convert.ToUInt16(3));
										binaryWriter.Write(Convert.ToUInt16(0));
										binaryWriter.Seek(0, SeekOrigin.Begin);
										binaryWriter.Write(Convert.ToUInt16(input.Length));
										Send(input.ToArray());
										binaryWriter.Close();
									}
									Session.Checkpoint = true;
									Session.CheckpointX = num43;
									Session.CheckpointY = num44;
									input = new MemoryStream();
									binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(11));
									binaryWriter.Write(Convert.ToUInt16(Session.CheckpointX));
									binaryWriter.Write(Convert.ToUInt16(Session.CheckpointY));
									binaryWriter.Write(Convert.ToUInt16(3));
									binaryWriter.Write(Convert.ToUInt16(1));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								else if (num49 == 157 || num49 == 559)
								{
									ProviderData providerData3 = Session.GetProviderData(num45);
									if (Item.Harvestable(providerData3.Time))
									{
										PlayerLayout.Notification(this, 100, Session.Data.Foreground[num45], "~1{0}~0, ready to harvest", Item.Name(Session.Data.Foreground[num45]));
									}
									else
									{
										PlayerLayout.Notification(this, 100, Session.Data.Foreground[num45], "~1{0}~0, will be ready to harvest in ~1{1}", Item.Name(Session.Data.Foreground[num45]), Text.Time(Item.GetHarvest(providerData3.Time)));
									}
								}
								else
								{
									switch (num49)
									{
									case 597:
										GameJoin(num45);
										break;
									case 601:
										GameFinish(num45, reached: true);
										break;
									case 1143:
										PlayerLayout.Notification(this, 100, num49, "Punch to continue");
										break;
									}
								}
							}
							if (!Fishing)
							{
								break;
							}
							Fishing = false;
							input = new MemoryStream();
							binaryWriter = new BinaryWriter(input);
							binaryWriter.Write(Convert.ToUInt16(0));
							binaryWriter.Write(Convert.ToUInt16(31));
							binaryWriter.Write(Convert.ToInt32(0));
							binaryWriter.Write(Convert.ToBoolean(value: false));
							binaryWriter.Write(Convert.ToUInt16(0));
							binaryWriter.Write(Convert.ToUInt16(0));
							binaryWriter.Seek(0, SeekOrigin.Begin);
							binaryWriter.Write(Convert.ToUInt16(input.Length));
							Send(input.ToArray());
							binaryWriter.Close();
							input = new MemoryStream();
							binaryWriter = new BinaryWriter(input);
							binaryWriter.Write(Convert.ToUInt16(0));
							binaryWriter.Write(Convert.ToUInt16(31));
							binaryWriter.Write(Convert.ToInt32(Identifier));
							binaryWriter.Write(Convert.ToBoolean(0));
							binaryWriter.Write(Convert.ToUInt16(0));
							binaryWriter.Write(Convert.ToUInt16(0));
							binaryWriter.Seek(0, SeekOrigin.Begin);
							binaryWriter.Write(Convert.ToUInt16(input.Length));
							Server.Broadcast(input.ToArray(), delegate(Player player)
							{
								if (player == this)
								{
									return false;
								}
								if (!player.Active)
								{
									return false;
								}
								if (!player.Profile.Active)
								{
									return false;
								}
								if (!player.Session.Active)
								{
									return false;
								}
								return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
							});
							binaryWriter.Close();
						}
						else
						{
							input = new MemoryStream();
							BinaryWriter binaryWriter = new BinaryWriter(input);
							binaryWriter.Write(Convert.ToUInt16(0));
							binaryWriter.Write(Convert.ToUInt16(13));
							binaryWriter.Write(Convert.ToInt32(0));
							binaryWriter.Write(Convert.ToBoolean(value: false));
							binaryWriter.Write(Convert.ToUInt16(CurrentX));
							binaryWriter.Write(Convert.ToUInt16(CurrentY));
							binaryWriter.Seek(0, SeekOrigin.Begin);
							binaryWriter.Write(Convert.ToUInt16(input.Length));
							Send(input.ToArray());
							binaryWriter.Close();
						}
						break;
					}
					case 14:
						binaryReader.Close();
						break;
					case 15:
					{
						Action = binaryReader.ReadUInt16();
						binaryReader.Close();
						if (!Profile.Active || !Session.Active)
						{
							break;
						}
						input = new MemoryStream();
						BinaryWriter binaryWriter = new BinaryWriter(input);
						binaryWriter.Write(Convert.ToUInt16(0));
						binaryWriter.Write(Convert.ToUInt16(15));
						binaryWriter.Write(Convert.ToInt32(0));
						binaryWriter.Write(Convert.ToUInt16(Action));
						binaryWriter.Seek(0, SeekOrigin.Begin);
						binaryWriter.Write(Convert.ToUInt16(input.Length));
						Send(input.ToArray());
						binaryWriter.Close();
						input = new MemoryStream();
						binaryWriter = new BinaryWriter(input);
						binaryWriter.Write(Convert.ToUInt16(0));
						binaryWriter.Write(Convert.ToUInt16(15));
						binaryWriter.Write(Convert.ToInt32(Identifier));
						binaryWriter.Write(Convert.ToUInt16(Action));
						binaryWriter.Seek(0, SeekOrigin.Begin);
						binaryWriter.Write(Convert.ToUInt16(input.Length));
						Server.Broadcast(input.ToArray(), delegate(Player player)
						{
							if (!player.Active)
							{
								return false;
							}
							if (!player.Profile.Active)
							{
								return false;
							}
							if (!player.Session.Active)
							{
								return false;
							}
							return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
						});
						binaryWriter.Close();
						break;
					}
					case 16:
						binaryReader.Close();
						break;
					case 17:
						binaryReader.Close();
						break;
					case 18:
						binaryReader.Close();
						break;
					case 19:
						binaryReader.Close();
						break;
					case 20:
						binaryReader.Close();
						break;
					case 21:
						binaryReader.Close();
						break;
					case 22:
					{
						bool flag4 = binaryReader.ReadBoolean();
						ushort num36 = binaryReader.ReadUInt16();
						string text29 = binaryReader.ReadString();
						binaryReader.Close();
						if (Profile.Active && flag4)
						{
							if (text29 == "RewardBox1" || text29 == "Daily" || text29 == "Respin")
							{
								Reward1();
							}
							if (text29 == "RewardBox2" || text29 == "Gems")
							{
								Reward2();
							}
							if (text29 == "RewardBox3" || text29 == "Event")
							{
								Reward3();
							}
							if (text29 == "SkipQuest" || text29 == "Skip")
							{
								Profile.Data.QuestType = 0;
								Profile.Data.QuestItem = 0;
								Profile.Data.QuestLeft = 0;
								Database.ProfileSave(Profile.Data);
								input = new MemoryStream();
								BinaryWriter binaryWriter = new BinaryWriter(input);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(5));
								Window = Dialog.Create(binaryWriter, 0, "Message");
								Dialog.ItemText(binaryWriter, breaker: true, "~1Skip quest", 75, 3);
								Dialog.Text(binaryWriter, breaker: true, "Your quest has been skipped.", 50);
								Dialog.Button(binaryWriter, breaker: false, "Okay", "Okay");
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(input.Length));
								Send(input.ToArray());
								binaryWriter.Close();
							}
						}
						break;
					}
					case 23:
					{
						binaryReader.Close();
						if (!Profile.Active || !Session.Active)
						{
							break;
						}
						int spawn2 = Session.GetSpawn();
						CurrentX = Session.GetTileX(spawn2) * 32 + 8;
						CurrentY = Session.GetTileY(spawn2) * 32;
						if (Session.Checkpoint)
						{
							Database.SessionLoad(ref Session.Data, Session.Data.Name);
							int num50 = Session.CheckpointY * Session.Data.SizeX + Session.CheckpointX;
							if (Session.Data.Foreground[num50] == 429)
							{
								CurrentX = Session.CheckpointX * 32 + 8;
								CurrentY = Session.CheckpointY * 32;
							}
						}
						input = new MemoryStream();
						BinaryWriter binaryWriter = new BinaryWriter(input);
						binaryWriter.Write(Convert.ToUInt16(0));
						binaryWriter.Write(Convert.ToUInt16(13));
						binaryWriter.Write(Convert.ToInt32(0));
						binaryWriter.Write(Convert.ToBoolean(value: false));
						binaryWriter.Write(Convert.ToUInt16(CurrentX));
						binaryWriter.Write(Convert.ToUInt16(CurrentY));
						binaryWriter.Seek(0, SeekOrigin.Begin);
						binaryWriter.Write(Convert.ToUInt16(input.Length));
						Send(input.ToArray());
						binaryWriter.Close();
						GameCancel();
						break;
					}
					case 24:
						binaryReader.Close();
						if (Profile.Active && Session.Active)
						{
							Friendlist(offline: false);
						}
						break;
					case 25:
					{
						ushort count3 = binaryReader.ReadUInt16();
						string @string = Encoding.UTF8.GetString(binaryReader.ReadBytes(count3));
						string text103 = binaryReader.ReadString();
						binaryReader.Close();
						if (!Profile.Active)
						{
							break;
						}
						string orderID = GoogleIap.GetOrderID(text103, @string);
						if (GoogleIap.GetOrderState(text103, @string) && !Profile.Data.Purchases.Contains(orderID))
						{
							Profile.Data.Purchases.Add(orderID);
							int num169 = 0;
							if (text103 == "gems.a")
							{
								num169 = 22000;
							}
							if (text103 == "gems.b")
							{
								num169 = 57500;
							}
							if (text103 == "gems.c")
							{
								num169 = 240000;
							}
							if (text103 == "gems.d")
							{
								num169 = 675000;
							}
							PlayerGems.Add(this, num169);
							Database.ProfileSave(Profile.Data);
							if (text103 == "gems.a")
							{
								Experience(randomize: true, 100);
							}
							if (text103 == "gems.b")
							{
								Experience(randomize: true, 250);
							}
							if (text103 == "gems.c")
							{
								Experience(randomize: true, 500);
							}
							if (text103 == "gems.d")
							{
								Experience(randomize: true, 1500);
							}
							if (Event.Type == EventType.Summer)
							{
								int num170 = 0;
								if (text103 == "gems.a")
								{
									num170 = 1;
								}
								if (text103 == "gems.b")
								{
									num170 = 1;
								}
								if (text103 == "gems.c")
								{
									num170 = 5;
								}
								if (text103 == "gems.d")
								{
									num170 = 10;
								}
								if (num170 != 0 && Profile.CanGetItem(965, num170))
								{
									Profile.SetItem(965, Profile.GetItem(965) + num170);
									PlayerConsole.Message(this, "You've been gifted x{0} {1}, thank you for your purchase.", num170, Item.Name(965));
									PlayerCore.UpdateInventory(this);
								}
							}
							if (Event.Type == EventType.Halloween)
							{
								if (text103 == "gems.a" && Profile.CanGetItem(1145, 10))
								{
									Profile.SetItem(1145, Profile.GetItem(1145) + 10);
									PlayerConsole.Message(this, "You've been gifted x10 {0}, thank you for your purchase.", Item.Name(1145));
									PlayerCore.UpdateInventory(this);
								}
								if (text103 == "gems.b" && Profile.CanGetItem(1149, 10))
								{
									Profile.SetItem(1149, Profile.GetItem(1149) + 10);
									PlayerConsole.Message(this, "You've been gifted x10 {0}, thank you for your purchase.", Item.Name(1149));
									PlayerCore.UpdateInventory(this);
								}
								if (text103 == "gems.c" && Profile.CanGetItem(1151, 10))
								{
									Profile.SetItem(1151, Profile.GetItem(1151) + 10);
									PlayerConsole.Message(this, "You've been gifted x10 {0}, thank you for your purchase.", Item.Name(1151));
									PlayerCore.UpdateInventory(this);
								}
								if (text103 == "gems.d" && Profile.CanGetItem(1153, 10))
								{
									Profile.SetItem(1153, Profile.GetItem(1153) + 10);
									PlayerConsole.Message(this, "You've been gifted x10 {0}, thank you for your purchase.", Item.Name(1153));
									PlayerCore.UpdateInventory(this);
								}
							}
							if (Event.Type == EventType.Space)
							{
								if (text103 == "gems.a")
								{
									PlayerSpecialCurrency.Add(this, 5);
									PlayerConsole.Message(this, "You've been gifted x5 Moon Rocks, thank you for your purchase.", Item.Name(1145));
									PlayerCore.UpdateInventory(this);
								}
								if (text103 == "gems.b")
								{
									PlayerSpecialCurrency.Add(this, 10);
									PlayerConsole.Message(this, "You've been gifted x10 Moon Rocks, thank you for your purchase.", Item.Name(1149));
									PlayerCore.UpdateInventory(this);
								}
								if (text103 == "gems.c")
								{
									PlayerSpecialCurrency.Add(this, 15);
									PlayerConsole.Message(this, "You've been gifted x15 Moon Rocks, thank you for your purchase.", Item.Name(1151));
									PlayerCore.UpdateInventory(this);
								}
								if (text103 == "gems.d")
								{
									PlayerSpecialCurrency.Add(this, 30);
									PlayerConsole.Message(this, "You've been gifted x30 Moon Rocks, thank you for your purchase.", Item.Name(1153));
									PlayerCore.UpdateInventory(this);
								}
							}
							PlayerLayout.Warning(this, 200, 0, "~1PURCHASE COMPLETE!", $"You have received ~1{Text.Delimit(num169)}~0 gems.", "Thank you for your purchase.");
							Server.SendLog(Profile.Data.Filename, $"Has purchased {num169} gems, order ID is {orderID}");
						}
						else
						{
							Server.SendLog(Profile.Data.Filename, $"Has canceled their order.");
						}
						break;
					}
					case 26:
					{
						int year = binaryReader.ReadUInt16();
						int month = binaryReader.ReadUInt16();
						int day = binaryReader.ReadUInt16();
						int hour = binaryReader.ReadUInt16();
						int minute = binaryReader.ReadUInt16();
						int second = binaryReader.ReadUInt16();
						binaryReader.Close();
						DateTime utcNow = DateTime.UtcNow;
						DateTime clientPingTime = new DateTime(year, month, day, hour, minute, second);
						if (Profile.Active && Session.Active && ServerPingTime.Year != 1 && ClientPingTime.Year != 1)
						{
							int num38 = (int)utcNow.Subtract(ServerPingTime).TotalSeconds;
							int num39 = (int)clientPingTime.Subtract(ClientPingTime).TotalSeconds;
							if (num38 < 1)
							{
								num38 = 1;
							}
							if (num39 < 1)
							{
								num39 = 1;
							}
							int num40 = num39 / num38;
							if (num40 > 1 && num39 == 10)
							{
								PlayerConsole.StaffMessage(this, "*Potential Speed Hacker*");
							}
						}
						ServerPingTime = utcNow;
						ClientPingTime = clientPingTime;
						break;
					}
					case 27:
						binaryReader.Close();
						break;
					case 28:
						binaryReader.Close();
						break;
					case 29:
					{
						int value = binaryReader.ReadUInt16();
						binaryReader.Close();
						if (!Profile.Active || !Session.Active)
						{
							break;
						}
						input = new MemoryStream();
						BinaryWriter binaryWriter = new BinaryWriter(input);
						binaryWriter.Write(Convert.ToUInt16(0));
						binaryWriter.Write(Convert.ToUInt16(29));
						binaryWriter.Write(Convert.ToInt32(Identifier));
						binaryWriter.Write(Convert.ToUInt16(value));
						binaryWriter.Seek(0, SeekOrigin.Begin);
						binaryWriter.Write(Convert.ToUInt16(input.Length));
						Server.Broadcast(input.ToArray(), delegate(Player player)
						{
							if (player == this)
							{
								return false;
							}
							if (!player.Active)
							{
								return false;
							}
							if (!player.Profile.Active)
							{
								return false;
							}
							if (!player.Session.Active)
							{
								return false;
							}
							return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
						});
						binaryWriter.Close();
						break;
					}
					case 30:
						binaryReader.Close();
						break;
					case 31:
						binaryReader.Close();
						break;
					case 32:
						binaryReader.Close();
						break;
					case 33:
					{
						bool flag5 = binaryReader.ReadBoolean();
						int num37 = binaryReader.ReadUInt16();
						binaryReader.Close();
						if (!Profile.Active || !Session.Active)
						{
							break;
						}
						if (flag5)
						{
							if (ShopPage == 0)
							{
								if (Platform.Equals(Profile.PlatformType, PlatformType.Android))
								{
									if (GoogleIap.Disabled)
									{
										PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
										{
											Dialog.ItemText(dialog, breaker: true, "~1Not available", 75, 3);
											Dialog.Text(dialog, breaker: true, "Sorry, in-app purchases are currently disabled", 50);
											Dialog.Text(dialog, breaker: true, "and will be available again shortly.", 50);
											Dialog.Button(dialog, breaker: false, "Okay", "Okay");
										});
										break;
									}
									input = new MemoryStream();
									BinaryWriter binaryWriter = new BinaryWriter(input);
									binaryWriter.Write(Convert.ToUInt16(0));
									binaryWriter.Write(Convert.ToUInt16(25));
									if (num37 == 0)
									{
										binaryWriter.Write(Encoding.UTF8.GetBytes("gems.a\0"));
									}
									if (num37 == 1)
									{
										binaryWriter.Write(Encoding.UTF8.GetBytes("gems.b\0"));
									}
									if (num37 == 2)
									{
										binaryWriter.Write(Encoding.UTF8.GetBytes("gems.c\0"));
									}
									if (num37 == 3)
									{
										binaryWriter.Write(Encoding.UTF8.GetBytes("gems.d\0"));
									}
									binaryWriter.Write(Encoding.UTF8.GetBytes(Profile.Data.Filename + "\0"));
									binaryWriter.Seek(0, SeekOrigin.Begin);
									binaryWriter.Write(Convert.ToUInt16(input.Length));
									Send(input.ToArray());
									binaryWriter.Close();
								}
								else
								{
									PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
									{
										Dialog.ItemText(dialog, breaker: true, "~1IAP unavailable", 75, 3);
										Dialog.Text(dialog, breaker: true, "Sorry, in-app purchases are unavailable on the", 50);
										Dialog.Text(dialog, breaker: true, "platform that you're currently playing on.", 50);
										Dialog.Button(dialog, breaker: false, "Okay", "Okay");
									});
								}
								break;
							}
							if (ShopPage == 1)
							{
								if (num37 != 0)
								{
									break;
								}
								PlayerCore.UpdateDialog(this, 0, "Shop.Upgrade.Slots", delegate(BinaryWriter dialog)
								{
									Dialog.ItemText(dialog, breaker: true, "~1Inventory upgrade", 75, 3);
									if (Profile.Data.ItemSlots >= 250)
									{
										Dialog.Text(dialog, breaker: true, "You already have the maximum amount", 50);
										Dialog.Text(dialog, breaker: true, "of inventory slots available.", 50);
										Dialog.Button(dialog, breaker: false, "Okay", "Okay");
									}
									else
									{
										int num189 = (Profile.Data.ItemSlots - 20) * 10;
										Dialog.Text(dialog, breaker: true, $"You currently have ~1{Profile.Data.ItemSlots} ~0inventory slots,", 50);
										Dialog.Text(dialog, breaker: true, $"would you like to buy more for ~1{num189} ~0gems?", 50);
										Dialog.Button(dialog, breaker: false, "Accept", "Accept");
										Dialog.Button(dialog, breaker: false, "Cancel", "Cancel");
									}
								});
								break;
							}
							category = Shop.Categories[ShopPage];
							listing2 = category.Listings[num37];
							if (Shop.ListingAvailable(listing2))
							{
								PlayerCore.UpdateDialog(this, num37, "Shop.Purchase", delegate(BinaryWriter dialog)
								{
									Dialog.ItemText(dialog, breaker: true, $"Buy {listing2.Text1}", 75, 3);
									if (category.Currency == CurrencyType.Gems)
									{
										if (listing2.Amount == listing2.Items.Length)
										{
											Dialog.Text(dialog, breaker: true, $"You will pay ~1{Text.Delimit(listing2.Price)} ~0gems for all of the items below", 50);
										}
										else
										{
											Dialog.Text(dialog, breaker: true, $"You will pay ~1{Text.Delimit(listing2.Price)} ~0gems for ~1{listing2.Amount} ~0of the items below", 50);
										}
									}
									else if (category.Currency == CurrencyType.Tokens)
									{
										if (listing2.Amount == listing2.Items.Length)
										{
											Dialog.Text(dialog, breaker: true, $"You will pay ~1{Text.Delimit(listing2.Price)} ~0tokens for all of the items below", 50);
										}
										else
										{
											Dialog.Text(dialog, breaker: true, $"You will pay ~1{Text.Delimit(listing2.Price)} ~0tokens for ~1{listing2.Amount} ~0of the items below", 50);
										}
									}
									else if (category.Currency == CurrencyType.MoonRocks)
									{
										if (listing2.Amount == listing2.Items.Length)
										{
											Dialog.Text(dialog, breaker: true, $"You will pay ~1{Text.Delimit(listing2.Price)} ~0moon rocks for all of the items below", 50);
										}
										else
										{
											Dialog.Text(dialog, breaker: true, $"You will pay ~1{Text.Delimit(listing2.Price)} ~0moon rocks for ~1{listing2.Amount} ~0of the items below", 50);
										}
									}
									if (listing2.Items.Length > 10)
									{
										Dialog.Space(dialog);
										Dialog.Button(dialog, breaker: false, "Accept", "Accept");
										Dialog.Button(dialog, breaker: true, "Cancel", "Cancel");
									}
									Dialog.Space(dialog);
									for (int num188 = 0; num188 < listing2.Items.Length; num188++)
									{
										ShopItem shopItem7 = listing2.Items[num188];
										Dialog.ItemText(dialog, breaker: true, $"x{shopItem7.Count} {Item.Name(shopItem7.Index)}", 50, shopItem7.Index);
									}
									Dialog.Button(dialog, breaker: false, "Accept", "Accept");
									Dialog.Button(dialog, breaker: true, "Cancel", "Cancel");
								});
							}
							else if (Shop.ListingAvailableLater(listing2))
							{
								PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
								{
									Dialog.ItemText(dialog, breaker: true, "~1Item unavailable!", 75, 3);
									Dialog.Text(dialog, breaker: true, $"This item will be available in {Text.Time(Shop.ListingAvailableAfter(listing2))}!", 50);
									Dialog.Button(dialog, breaker: false, "Okay", "Okay");
								});
							}
							else
							{
								PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
								{
									Dialog.ItemText(dialog, breaker: true, "~1Item unavailable!", 75, 3);
									Dialog.Text(dialog, breaker: true, "Sorry, you're late, this item is not available anymore.", 50);
									Dialog.Button(dialog, breaker: false, "Okay", "Okay");
								});
							}
						}
						else
						{
							ShopPage = num37;
							input = new MemoryStream();
							BinaryWriter binaryWriter = new BinaryWriter(input);
							binaryWriter.Write(Convert.ToUInt16(0));
							binaryWriter.Write(Convert.ToUInt16(33));
							Shop.WriteCategories(binaryWriter, ShopPage);
							Shop.WriteListings(binaryWriter, ShopPage);
							binaryWriter.Seek(0, SeekOrigin.Begin);
							binaryWriter.Write(Convert.ToUInt16(input.Length));
							Send(input.ToArray());
							binaryWriter.Close();
						}
						break;
					}
					case 34:
					{
						int num4 = binaryReader.ReadByte();
						binaryReader.Close();
						if (Profile.Active && Session.Active)
						{
							if (num4 == 1)
							{
								TradeCancel();
							}
							if (num4 == 2)
							{
								TradeAccept();
							}
						}
						break;
					}
					case 35:
						binaryReader.Close();
						break;
					case 36:
						binaryReader.Close();
						break;
					case 37:
						binaryReader.Close();
						break;
					case 38:
						binaryReader.Close();
						break;
					default:
						binaryReader.Close();
						Close();
						break;
					}
				}
			}
			catch (Exception exception2)
			{
				Terminal.Exception(exception2);
				Close();
			}
		}

		public void Send(byte[] data)
		{
			try
			{
				Socket.BeginSend(data, 0, data.Length, SocketFlags.None, null, null);
			}
			catch (Exception)
			{
				Close();
			}
		}

		public void Enter(int index, string password)
		{
			try
			{
				if (Session.Data.Foreground[index] == 71 || Session.Data.Foreground[index] == 203 || Session.Data.Foreground[index] == 511)
				{
					DoorData doorData = Session.GetDoorData(index);
					if (!string.IsNullOrEmpty(doorData.Password) && password == null)
					{
						PlayerCore.UpdateDialog(this, index, "Door.Password", delegate(BinaryWriter dialog)
						{
							Dialog.ItemText(dialog, breaker: true, "~1Door password", 75, Session.Data.Foreground[index]);
							Dialog.Text(dialog, breaker: true, "This door requires a password to enter.", 50);
							Dialog.Textbox(dialog, breaker: true, "Password", "", 32);
							Dialog.Text(dialog, breaker: true, "The password is set by owner of the door.", 25);
							Dialog.Button(dialog, breaker: false, "Accept", "Accept");
							Dialog.Button(dialog, breaker: false, "Cancel", "Cancel");
						});
					}
					else if (!string.IsNullOrEmpty(doorData.Password) && password != doorData.Password)
					{
						PlayerConsole.Message(this, "The password that you entered for the door is incorrect.");
					}
					else if (!Session.TileAccess(Profile.Data.Filename, index) && !doorData.Public)
					{
						PlayerLayout.Notification(this, 100, Session.Data.Foreground[index], "The door is locked.");
					}
					else if (!string.IsNullOrEmpty(doorData.World))
					{
						Warp(doorData.World, unban: false);
						if (!string.IsNullOrEmpty(doorData.Target))
						{
							Session.Door = doorData.Target;
						}
					}
					else if (!string.IsNullOrEmpty(doorData.Target))
					{
						int door = Session.GetDoor(doorData.Target);
						PlayerCore.UpdatePosition(this, Session.GetTileX(door) * 32 + 8, Session.GetTileY(door) * 32);
					}
					else
					{
						PlayerLayout.Notification(this, 100, Session.Data.Foreground[index], "Target door name is not set.");
					}
				}
				else if (Session.Data.Foreground[index] == 207 || Session.Data.Foreground[index] == 209 || Session.Data.Foreground[index] == 211 || Session.Data.Foreground[index] == 213)
				{
					PortalData portalData = Session.GetPortalData(index);
					if (Profile.Noclip)
					{
						PlayerLayout.Notification(this, 100, Session.Data.Foreground[index], "Noclip is enabled, ignoring portal.");
					}
					else if (!Session.TileAccess(Profile.Data.Filename, index) && !portalData.Public)
					{
						PlayerLayout.Notification(this, 100, Session.Data.Foreground[index], "Portal is locked.");
					}
					else if (!string.IsNullOrEmpty(portalData.World))
					{
						Warp(portalData.World, unban: false);
						if (!string.IsNullOrEmpty(portalData.Target))
						{
							Session.Door = portalData.Target;
						}
					}
					else if (!string.IsNullOrEmpty(portalData.Target))
					{
						int door2 = Session.GetDoor(portalData.Target);
						PlayerCore.UpdatePosition(this, Session.GetTileX(door2) * 32 + 8, Session.GetTileY(door2) * 32);
					}
					else
					{
						PlayerLayout.Notification(this, 100, Session.Data.Foreground[index], "Target door name is not set.");
					}
				}
				else if (Session.Data.Foreground[index] == 1143)
				{
					HalloweenEnemyData halloweenEnemyData = Session.GetHalloweenEnemyData(index);
					if (!string.IsNullOrEmpty(halloweenEnemyData.Target))
					{
						int door3 = Session.GetDoor(halloweenEnemyData.Target);
						PlayerCore.UpdatePosition(this, Session.GetTileX(door3) * 32 + 8, Session.GetTileY(door3) * 32);
					}
					else
					{
						PlayerLayout.Notification(this, 100, Session.Data.Foreground[index], "Target door name is not set.");
					}
				}
				else
				{
					PlayerConsole.Message(this, "The item you tried to use has disappeared.");
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				Close();
			}
		}

		public void Warp(string world, bool unban)
		{
			try
			{
				if (LastWarped + 1000 > DateTime.UtcNow.Ticks / 10000)
				{
					PlayerConsole.Message(this, "~3Warp failed. ~0Please wait a little before going to another world.");
					return;
				}
				if (world.ToUpper() == Session.Data.Name && world.Length > 0)
				{
					PlayerConsole.Message(this, "~3Warp failed. ~0You're already in the world you tried to warp to.");
					return;
				}
				int num = Session.TryWarp(world, Profile, unban);
				switch (num)
				{
				case 0:
				{
					Loaded = false;
					CurrentX = 0;
					CurrentY = 0;
					PlayerCore.CancelTimer(this);
					MemoryStream memoryStream;
					BinaryWriter binaryWriter;
					if (Session.Active)
					{
						Player[] array = Server.Online.ToArray();
						foreach (Player player2 in array)
						{
							if (player2.Active && player2.Profile.Active && player2.Session.Active && !(player2.Session.Data.Name != Session.Data.Name) && Profile.VisibleTo(player2))
							{
								memoryStream = new MemoryStream();
								binaryWriter = new BinaryWriter(memoryStream);
								binaryWriter.Write(Convert.ToUInt16(0));
								binaryWriter.Write(Convert.ToUInt16(4));
								string text9 = $"~0[~1{Profile.Data.Username} ~0has left the world]";
								binaryWriter.Write(Encoding.UTF8.GetBytes(text9 + "\0"));
								binaryWriter.Seek(0, SeekOrigin.Begin);
								binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
								player2.Send(memoryStream.ToArray());
								binaryWriter.Close();
							}
						}
						memoryStream = new MemoryStream();
						binaryWriter = new BinaryWriter(memoryStream);
						binaryWriter.Write(Convert.ToUInt16(0));
						binaryWriter.Write(Convert.ToUInt16(13));
						binaryWriter.Write(Convert.ToInt32(Identifier));
						binaryWriter.Write(Convert.ToBoolean(1));
						binaryWriter.Seek(0, SeekOrigin.Begin);
						binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
						Server.Broadcast(memoryStream.ToArray(), delegate(Player player)
						{
							if (player == this)
							{
								return false;
							}
							if (!player.Active)
							{
								return false;
							}
							if (!player.Profile.Active)
							{
								return false;
							}
							if (!player.Session.Active)
							{
								return false;
							}
							return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
						});
						binaryWriter.Close();
						GameCancel();
						Session.Leave();
					}
					Profile.Noclip = false;
					Session.Warp(world);
					Session.Door = string.Empty;
					memoryStream = new MemoryStream();
					binaryWriter = new BinaryWriter(memoryStream);
					binaryWriter.Write(Convert.ToUInt16(0));
					binaryWriter.Write(Convert.ToUInt16(8));
					binaryWriter.Write(Convert.ToBoolean(1));
					if (Ratings.Data.Winner == Session.Data.Filename)
					{
						string text10 = $"Entered ~1{Session.Data.Name}~0, world of the day. There are ~1{Session.Players(Session.Data.Name)} ~0players here.";
						binaryWriter.Write(Encoding.UTF8.GetBytes(text10 + "\0"));
					}
					else
					{
						string text11 = $"Entered ~1{Session.Data.Name}~0, rated as ~1{Text.Ordinal(Ratings.Position(Session.Data.Filename))} ~0today. There are ~1{Session.Players(Session.Data.Name)} ~0players here.";
						binaryWriter.Write(Encoding.UTF8.GetBytes(text11 + "\0"));
					}
					binaryWriter.Write(Encoding.UTF8.GetBytes(Session.Data.Name + "\0"));
					binaryWriter.Seek(0, SeekOrigin.Begin);
					binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
					Send(memoryStream.ToArray());
					binaryWriter.Close();
					if (Session.Data.Owner.Length > 0)
					{
						PlayerConsole.Message(this, "This world is locked by ~1{0}~0.", Session.Data.Owner);
					}
					if (Session.Data.Banned > 0)
					{
						PlayerConsole.Message(this, "~3World is banned. ~0This world is banned, other players won't be able to enter it.");
					}
					if (Session.Data.Owner == Profile.Data.Filename && !Profile.Data.Worlds.Contains(Session.Data.Name))
					{
						Profile.Data.Worlds.Add(Session.Data.Name);
					}
					if (Session.Data.Owner != Profile.Data.Filename && Profile.Data.Worlds.Contains(Session.Data.Name))
					{
						Profile.Data.Worlds.Remove(Session.Data.Name);
					}
					if (Profile.HasItem(273, 1))
					{
						Profile.SetItem(273, 0);
						PlayerCore.UpdateInventory(this);
					}
					break;
				}
				case 1:
				{
					MemoryStream memoryStream = new MemoryStream();
					BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
					binaryWriter.Write(Convert.ToUInt16(0));
					binaryWriter.Write(Convert.ToUInt16(8));
					binaryWriter.Write(Convert.ToBoolean(0));
					string text8 = "~3Warp failed. ~0That world name is not valid, it can onlycontain symbols A-Z and 0-9.";
					binaryWriter.Write(Encoding.UTF8.GetBytes(text8 + "\0"));
					binaryWriter.Seek(0, SeekOrigin.Begin);
					binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
					Send(memoryStream.ToArray());
					binaryWriter.Close();
					break;
				}
				case 2:
				{
					MemoryStream memoryStream = new MemoryStream();
					BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
					binaryWriter.Write(Convert.ToUInt16(0));
					binaryWriter.Write(Convert.ToUInt16(8));
					binaryWriter.Write(Convert.ToBoolean(0));
					string text7 = "~3Warp failed. ~0The world name you picked is inappropriate and isn't allowed.";
					binaryWriter.Write(Encoding.UTF8.GetBytes(text7 + "\0"));
					binaryWriter.Seek(0, SeekOrigin.Begin);
					binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
					Send(memoryStream.ToArray());
					binaryWriter.Close();
					break;
				}
				case 3:
				{
					MemoryStream memoryStream = new MemoryStream();
					BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
					binaryWriter.Write(Convert.ToUInt16(0));
					binaryWriter.Write(Convert.ToUInt16(8));
					binaryWriter.Write(Convert.ToBoolean(0));
					string text6 = "~3Warp failed. ~0World name is too long or too short, it must be between 1 and 32 characters long.";
					binaryWriter.Write(Encoding.UTF8.GetBytes(text6 + "\0"));
					binaryWriter.Seek(0, SeekOrigin.Begin);
					binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
					Send(memoryStream.ToArray());
					binaryWriter.Close();
					break;
				}
				case 4:
				{
					MemoryStream memoryStream = new MemoryStream();
					BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
					binaryWriter.Write(Convert.ToUInt16(0));
					binaryWriter.Write(Convert.ToUInt16(8));
					binaryWriter.Write(Convert.ToBoolean(0));
					string text5 = "~3Warp failed. ~0This world name is not allowed.";
					binaryWriter.Write(Encoding.UTF8.GetBytes(text5 + "\0"));
					binaryWriter.Seek(0, SeekOrigin.Begin);
					binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
					Send(memoryStream.ToArray());
					binaryWriter.Close();
					break;
				}
				case 5:
				{
					MemoryStream memoryStream = new MemoryStream();
					BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
					binaryWriter.Write(Convert.ToUInt16(0));
					binaryWriter.Write(Convert.ToUInt16(8));
					binaryWriter.Write(Convert.ToBoolean(0));
					string text4 = "~3Warp failed. ~0This world has been banned for violating the game rules.";
					binaryWriter.Write(Encoding.UTF8.GetBytes(text4 + "\0"));
					binaryWriter.Seek(0, SeekOrigin.Begin);
					binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
					Send(memoryStream.ToArray());
					binaryWriter.Close();
					break;
				}
				case 6:
				{
					MemoryStream memoryStream = new MemoryStream();
					BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
					binaryWriter.Write(Convert.ToUInt16(0));
					binaryWriter.Write(Convert.ToUInt16(8));
					binaryWriter.Write(Convert.ToBoolean(0));
					string text3 = "~3Warp failed. ~0You've been banned from this world by it's owner.";
					binaryWriter.Write(Encoding.UTF8.GetBytes(text3 + "\0"));
					binaryWriter.Seek(0, SeekOrigin.Begin);
					binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
					Send(memoryStream.ToArray());
					binaryWriter.Close();
					break;
				}
				case 7:
				{
					MemoryStream memoryStream = new MemoryStream();
					BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
					binaryWriter.Write(Convert.ToUInt16(0));
					binaryWriter.Write(Convert.ToUInt16(8));
					binaryWriter.Write(Convert.ToBoolean(0));
					string text2 = "~3Warp failed. ~0There are too many players in this world, try again later.";
					binaryWriter.Write(Encoding.UTF8.GetBytes(text2 + "\0"));
					binaryWriter.Seek(0, SeekOrigin.Begin);
					binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
					Send(memoryStream.ToArray());
					binaryWriter.Close();
					break;
				}
				case 8:
				{
					MemoryStream memoryStream = new MemoryStream();
					BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
					binaryWriter.Write(Convert.ToUInt16(0));
					binaryWriter.Write(Convert.ToUInt16(8));
					binaryWriter.Write(Convert.ToBoolean(0));
					string text = "~3Warp failed. ~0You have discovered too much worlds recently, try later.";
					binaryWriter.Write(Encoding.UTF8.GetBytes(text + "\0"));
					binaryWriter.Seek(0, SeekOrigin.Begin);
					binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
					Send(memoryStream.ToArray());
					binaryWriter.Close();
					break;
				}
				}
				if (num > 0 && !Session.Active)
				{
					PlayerConsole.Message(this, "Looking for another available world.");
					Warp(Session.Random(this), unban: false);
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				Close();
			}
			finally
			{
				LastWarped = DateTime.UtcNow.Ticks / 10000;
			}
		}

		public void Experience(bool randomize, int amount)
		{
			try
			{
				if (randomize)
				{
					amount = Server.Random.Next(amount);
				}
				PlayerQuests.Event(this, PlayerEvent.Experience, amount);
				if (Profile.Data.Level >= 100)
				{
					return;
				}
				Profile.Data.Experience += amount;
				if (Profile.Data.Experience < Profile.Data.Level * 500)
				{
					return;
				}
				Profile.Data.Experience = 0;
				Profile.Data.Level++;
				Database.ProfileSave(Profile.Data);
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(4));
				string text = $"~0[~1{Profile.Data.Username}~0 has reached level ~1{Profile.Data.Level}~0]";
				binaryWriter.Write(Encoding.UTF8.GetBytes(text + "\0"));
				binaryWriter.Seek(0, SeekOrigin.Begin);
				binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
				Server.Broadcast(memoryStream.ToArray(), delegate(Player player)
				{
					if (!player.Active)
					{
						return false;
					}
					if (!player.Profile.Active)
					{
						return false;
					}
					if (!player.Session.Active)
					{
						return false;
					}
					return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
				});
				binaryWriter.Close();
				memoryStream = new MemoryStream();
				binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(35));
				binaryWriter.Write(Convert.ToUInt16(200));
				binaryWriter.Write(Convert.ToUInt16(2));
				binaryWriter.Write(Encoding.UTF8.GetBytes("~1LEVEL UP!\0"));
				binaryWriter.Write(Encoding.UTF8.GetBytes($"Congratulations, you are now level ~1{Profile.Data.Level}~0." + "\0"));
				binaryWriter.Write(Encoding.UTF8.GetBytes($"You've been rewarded with ~1{Profile.Data.Level * 100} ~0gems." + "\0"));
				binaryWriter.Seek(0, SeekOrigin.Begin);
				binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
				Send(memoryStream.ToArray());
				binaryWriter.Close();
				if (Profile.Data.Level > 0)
				{
					PlayerGems.Add(this, Profile.Data.Level * 100);
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				Close();
			}
		}

		public void Achievement(AchievementType type, bool increase, int amount)
		{
			try
			{
				int num = Convert.ToInt32(type);
				Achievement achievement = Achievements.List[num];
				if (Profile.Data.Achievements[num] >= achievement.Amount)
				{
					return;
				}
				if (!increase)
				{
					Profile.Data.Achievements[num] = amount;
				}
				else
				{
					Profile.Data.Achievements[num] += amount;
				}
				if (Profile.Data.Achievements[num] >= achievement.Amount)
				{
					PlayerLayout.Warning(this, 200, 2, "~1ACHIEVEMENT ~4COMPLETED~1!", "You have completed the achievement", $"~1{achievement.Name}");
					if (achievement.Reward.Count <= 0)
					{
					}
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				Close();
			}
		}

		public void TradeStart(int id)
		{
			try
			{
				Player interact = GetInteract(id);
				if (interact == null)
				{
					PlayerConsole.Message(this, "The other player is not in this world anymore.");
					return;
				}
				if (interact.Trading && interact.TradeID != Identifier)
				{
					PlayerConsole.Message(this, "~3Trade failed. ~1{0} ~0is trading with someone else, try later.", interact.Profile.Data.Username);
					return;
				}
				TradeID = SetInteract(interact);
				TradingOffer = new List<SlotData>();
				Trading = true;
				TradeAccepted = false;
				TradeCanceled = false;
				TradeReviewed = false;
				if (interact.Trading && interact.TradeID == Identifier)
				{
					PlayerConsole.Message(this, "You are now trading with ~1{0}~0.", interact.Profile.Data.Username);
					PlayerConsole.Message(interact, "You are now trading with ~1{0}~0.", Profile.Data.Username);
				}
				else
				{
					PlayerConsole.Message(this, "Waiting for ~1{0} ~0to accept the trading request.", interact.Profile.Data.Username);
					PlayerConsole.Message(interact, "~1{0} ~0wants to trade with you, wrench them to accept.", Profile.Data.Username);
				}
				TradeRefresh();
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				Close();
			}
		}

		public void TradeRefresh()
		{
			try
			{
				Player interact = GetInteract(TradeID);
				if (interact == null)
				{
					PlayerConsole.Message(this, "~3Trade failed. ~0The other player has disappeared.");
					TradeClose();
					return;
				}
				if (interact.Trading && interact.TradeID == Identifier && interact.TradeAccepted && TradeAccepted)
				{
					MemoryStream memoryStream = new MemoryStream();
					BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
					binaryWriter.Write(Convert.ToUInt16(0));
					binaryWriter.Write(Convert.ToUInt16(34));
					binaryWriter.Write(Convert.ToBoolean(value: false));
					binaryWriter.Seek(0, SeekOrigin.Begin);
					binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
					Send(memoryStream.ToArray());
					binaryWriter.Close();
					memoryStream = new MemoryStream();
					binaryWriter = new BinaryWriter(memoryStream);
					binaryWriter.Write(Convert.ToUInt16(0));
					binaryWriter.Write(Convert.ToUInt16(5));
					Window = Dialog.Create(binaryWriter, 0, "Trading.Review");
					Dialog.ItemText(binaryWriter, breaker: true, "~1Trading review", 75, 3);
					Dialog.Text(binaryWriter, breaker: true, "~3Items that you will give:", 50);
					if (TradingOffer.Count == 0)
					{
						Dialog.Text(binaryWriter, breaker: true, "Nothing.", 50);
					}
					else
					{
						foreach (SlotData item in TradingOffer)
						{
							Dialog.ItemText(binaryWriter, breaker: true, $"~1x{item.Count} {Item.Name(item.Index)}", 50, item.Index);
						}
					}
					Dialog.Space(binaryWriter);
					Dialog.Text(binaryWriter, breaker: true, "~4Items that you will get:", 50);
					if (interact.TradingOffer.Count == 0)
					{
						Dialog.Text(binaryWriter, breaker: true, "Nothing.", 50);
					}
					else
					{
						foreach (SlotData item2 in interact.TradingOffer)
						{
							Dialog.ItemText(binaryWriter, breaker: true, $"~1x{item2.Count} {Item.Name(item2.Index)}", 50, item2.Index);
						}
					}
					Dialog.Space(binaryWriter);
					Dialog.Button(binaryWriter, breaker: true, "Accept", "~3Finish the trade");
					Dialog.Space(binaryWriter);
					Dialog.Text(binaryWriter, breaker: true, "~3Note~0: We do not restore items traded by accident,", 50);
					Dialog.Text(binaryWriter, breaker: true, "if you aren't sure about this trade, deny it now.", 50);
					Dialog.Space(binaryWriter);
					Dialog.Button(binaryWriter, breaker: true, "Cancel", "Cancel trade");
					binaryWriter.Seek(0, SeekOrigin.Begin);
					binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
					Send(memoryStream.ToArray());
					return;
				}
				MemoryStream memoryStream2 = new MemoryStream();
				BinaryWriter binaryWriter2 = new BinaryWriter(memoryStream2);
				binaryWriter2.Write(Convert.ToUInt16(0));
				binaryWriter2.Write(Convert.ToUInt16(34));
				binaryWriter2.Write(Convert.ToBoolean(value: true));
				binaryWriter2.Write(Encoding.UTF8.GetBytes("~3Items that you will give:\0"));
				for (int i = 0; i < 4; i++)
				{
					if (TradingOffer.Count > i)
					{
						SlotData slotData = TradingOffer[i];
						binaryWriter2.Write(Convert.ToUInt16(slotData.Index));
						binaryWriter2.Write(Convert.ToUInt16(slotData.Count));
					}
					else
					{
						binaryWriter2.Write(Convert.ToUInt16(0));
						binaryWriter2.Write(Convert.ToUInt16(0));
					}
				}
				binaryWriter2.Write(Encoding.UTF8.GetBytes("~4Items that you will get:\0"));
				if (interact.Trading && interact.TradeID == Identifier)
				{
					for (int j = 0; j < 4; j++)
					{
						if (interact.TradingOffer.Count > j)
						{
							SlotData slotData2 = interact.TradingOffer[j];
							binaryWriter2.Write(Convert.ToUInt16(slotData2.Index));
							binaryWriter2.Write(Convert.ToUInt16(slotData2.Count));
						}
						else
						{
							binaryWriter2.Write(Convert.ToUInt16(0));
							binaryWriter2.Write(Convert.ToUInt16(0));
						}
					}
				}
				else
				{
					for (int k = 0; k < 4; k++)
					{
						binaryWriter2.Write(Convert.ToUInt16(0));
						binaryWriter2.Write(Convert.ToUInt16(0));
					}
				}
				binaryWriter2.Seek(0, SeekOrigin.Begin);
				binaryWriter2.Write(Convert.ToUInt16(memoryStream2.Length));
				Send(memoryStream2.ToArray());
				binaryWriter2.Close();
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				Close();
			}
		}

		public void TradeItem(int index, int count)
		{
			try
			{
				Player interact = GetInteract(TradeID);
				if (interact == null)
				{
					PlayerConsole.Message(this, "~3Trade failed. ~0The other player has disappeared.");
					TradeClose();
				}
				else if (TradingOffer.Count >= 4)
				{
					PlayerConsole.Message(this, "Couldn't add the item, you can only trade 4 items at once.");
				}
				else if (interact.Trading && interact.TradeID == Identifier && interact.TradeAccepted && TradeAccepted)
				{
					PlayerConsole.Message(this, "Couldn't edit offer, you are not allowed to do that during review.");
				}
				else if (!Item.Tradeable(index))
				{
					PlayerConsole.Message(this, "The item that you've selected is too special to be sold.");
				}
				else if (index == 273 && !Session.WorldOwner(Profile.Data.Filename))
				{
					PlayerConsole.Message(this, "Couldn't add world key, you don't own the world.");
				}
				else if (index == 273 && Session.OtherLocks(string.Empty))
				{
					PlayerConsole.Message(this, "Couldn't add world key, there are smaller locks in this world, remove them.");
				}
				else if (Profile.HasItem(index, count))
				{
					SlotData[] array = TradingOffer.ToArray();
					for (int i = 0; i < array.Length; i++)
					{
						SlotData item = array[i];
						if (item.Index == index)
						{
							TradingOffer.Remove(item);
						}
					}
					if (count > 0)
					{
						TradingOffer.Add(new SlotData
						{
							Index = (ushort)index,
							Count = (ushort)count
						});
						PlayerConsole.Message(this, "~1{0} ~0has added ~1x{1} {2}~0.", Profile.Data.Username, count, Item.Name(index));
						if (interact.Trading && interact.TradeID == Identifier)
						{
							PlayerConsole.Message(interact, "~1{0} ~0has added ~1x{1} {2}~0.", Profile.Data.Username, count, Item.Name(index));
						}
					}
					else
					{
						PlayerConsole.Message(this, "~1{0} ~0has removed ~1{1}~0.", Profile.Data.Username, Item.Name(index));
						if (interact.Trading && interact.TradeID == Identifier)
						{
							PlayerConsole.Message(interact, "~1{0} ~0has removed ~1{1}~0.", Profile.Data.Username, Item.Name(index));
						}
					}
					if (interact.Trading && interact.TradeID == Identifier)
					{
						interact.TradeAccepted = false;
						TradeAccepted = false;
						interact.TradeRefresh();
						TradeRefresh();
					}
					else
					{
						TradeAccepted = false;
						TradeRefresh();
					}
				}
				else
				{
					PlayerConsole.Message(this, "You don't have that much of this item.");
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				Close();
			}
		}

		public void TradeAccept()
		{
			try
			{
				Player interact = GetInteract(TradeID);
				if (interact == null)
				{
					PlayerConsole.Message(this, "~3Trade failed. ~0The other player has disappeared.");
					TradeClose();
				}
				else if (TradeAccepted)
				{
					PlayerConsole.Message(this, "You have already accepted the trade, wait for the other player to accept.");
				}
				else if (interact.Trading && interact.TradeID == Identifier)
				{
					TradeAccepted = true;
					if (interact.TradeAccepted)
					{
						interact.TradeRefresh();
						TradeRefresh();
						return;
					}
					PlayerConsole.Message(this, "You've accepted the trade, waiting for ~1{0} ~0to accept it.", interact.Profile.Data.Username);
					PlayerConsole.Message(interact, "~1{0} ~0has accepted the trade, waiting for you to accept.", Profile.Data.Filename);
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				Close();
			}
		}

		public void TradeCancel()
		{
			try
			{
				Player interact = GetInteract(TradeID);
				if (interact != null && interact.Trading && interact.TradeID == Identifier)
				{
					PlayerConsole.Message(this, "Trade has been canceled by ~1{0}~0.", Profile.Data.Username);
					PlayerConsole.Message(interact, "Trade has been canceled by ~1{0}~0.", Profile.Data.Username);
				}
				TradeClose();
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				Close();
			}
		}

		public void TradeReview()
		{
			try
			{
				Player interact = GetInteract(TradeID);
				if (interact == null)
				{
					PlayerConsole.Message(this, "~3Trade failed. ~0The other player has disappeared.");
					TradeClose();
				}
				else if (TradeReviewed)
				{
					PlayerConsole.Message(this, "You already have reviewed the trade.");
				}
				else
				{
					if (!interact.Trading || interact.TradeID != Identifier)
					{
						return;
					}
					TradeReviewed = true;
					if (!interact.TradeReviewed)
					{
						return;
					}
					if (TradeValidate() && interact.TradeValidate())
					{
						PlayerConsole.Message(interact, "Trading has been completed successfully.");
						interact.TradeProcess();
						int num = 0;
						int num2 = 0;
						int num3 = 0;
						int num4 = 0;
						foreach (SlotData item in interact.TradingOffer)
						{
							num = item.Count;
							num2 = item.Index;
							if (Item.Farmability(num2) < 4)
							{
								Server.TradeLogs("Trade Logs (" + interact.Profile.Data.Filename + " and " + Profile.Data.Filename + ")", string.Format("**" + interact.Profile.Data.Filename + "** completed a trade with **" + Profile.Data.Filename + "** (Gave x" + num + " of " + Item.Name(num2) + ") "));
							}
						}
						foreach (SlotData item2 in TradingOffer)
						{
							num3 = item2.Count;
							num4 = item2.Index;
							if (Item.Farmability(num4) < 4)
							{
								Server.TradeLogs("Trade Logs (" + interact.Profile.Data.Filename + " and " + Profile.Data.Filename + ")", string.Format("**" + Profile.Data.Filename + "** completed a trade with **" + interact.Profile.Data.Filename + "** (Gave x" + num3 + " of " + Item.Name(num4) + ") "));
							}
						}
						PlayerConsole.Message(this, "Trading has been completed successfully.");
						TradeProcess();
					}
					else
					{
						PlayerConsole.Message(this, "Something went wrong and the trade has failed, try again.");
						PlayerConsole.Message(interact, "Something went wrong and the trade has failed, try again.");
					}
					interact.TradeClose();
					TradeClose();
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				Close();
			}
		}

		public bool TradeValidate()
		{
			try
			{
				Player interact = GetInteract(TradeID);
				if (interact == null)
				{
					return false;
				}
				if (interact.Trading && interact.TradeID == Identifier)
				{
					foreach (SlotData item in TradingOffer)
					{
						if (item.Index == 273 && !Session.WorldOwner(Profile.Data.Filename))
						{
							return false;
						}
						if (item.Index == 273 && Session.OtherLocks(string.Empty))
						{
							return false;
						}
						if (!Profile.HasItem(item.Index, item.Count))
						{
							return false;
						}
					}
					foreach (SlotData item2 in interact.TradingOffer)
					{
						if (!Profile.CanGetItem(item2.Index, item2.Count))
						{
							PlayerConsole.Message(this, "~3Trade failed. ~0You don't have enough inventory space.");
							PlayerConsole.Message(interact, "~3Trade failed. ~1{0} ~0doesn't have enough inventory space.", Profile.Data.Username);
							return false;
						}
					}
					return true;
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				Close();
			}
			return false;
		}

		public void TradeProcess()
		{
			try
			{
				Player interact = GetInteract(TradeID);
				if (interact == null || !interact.Trading || interact.TradeID != Identifier)
				{
					return;
				}
				foreach (SlotData item in TradingOffer)
				{
					Profile.SetItem(item.Index, Profile.GetItem(item.Index) - item.Count);
				}
				foreach (SlotData item2 in interact.TradingOffer)
				{
					if (item2.Index == 273)
					{
						Database.SessionLoad(ref Session.Data, Session.Data.Name);
						Session.Data.Owner = Profile.Data.Filename;
						Session.Data.Admin.Clear();
						Database.SessionSave(Session.Data);
					}
					else
					{
						Profile.SetItem(item2.Index, Profile.GetItem(item2.Index) + item2.Count);
					}
				}
				Database.ProfileSave(Profile.Data);
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(6));
				Profile.WriteItems(binaryWriter);
				binaryWriter.Seek(0, SeekOrigin.Begin);
				binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
				Send(memoryStream.ToArray());
				binaryWriter.Close();
				memoryStream = new MemoryStream();
				binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(14));
				binaryWriter.Write(Convert.ToInt32(0));
				Profile.WriteData(binaryWriter, this);
				binaryWriter.Seek(0, SeekOrigin.Begin);
				binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
				Send(memoryStream.ToArray());
				binaryWriter.Close();
				Player[] array = Server.Online.ToArray();
				foreach (Player player in array)
				{
					if (player != this && player.Active && player.Profile.Active && player.Session.Active && !(player.Session.Data.Name != Session.Data.Name))
					{
						memoryStream = new MemoryStream();
						binaryWriter = new BinaryWriter(memoryStream);
						binaryWriter.Write(Convert.ToUInt16(0));
						binaryWriter.Write(Convert.ToUInt16(14));
						binaryWriter.Write(Convert.ToInt32(Identifier));
						Profile.WriteData(binaryWriter, player);
						binaryWriter.Seek(0, SeekOrigin.Begin);
						binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
						player.Send(memoryStream.ToArray());
						binaryWriter.Close();
					}
				}
				if (Session.Data.Owner == Profile.Data.Filename && !Profile.Data.Worlds.Contains(Session.Data.Name))
				{
					Profile.Data.Worlds.Add(Session.Data.Name);
				}
				if (Session.Data.Owner != Profile.Data.Filename && Profile.Data.Worlds.Contains(Session.Data.Name))
				{
					Profile.Data.Worlds.Remove(Session.Data.Name);
				}
				Achievement(AchievementType.TradeItems, increase: true, 1);
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				Close();
			}
		}

		public void TradeClose()
		{
			try
			{
				Player interact = GetInteract(TradeID);
				TradeID = 0;
				Trading = false;
				TradeAccepted = false;
				TradeCanceled = false;
				TradeReviewed = false;
				TradingOffer = null;
				if (interact != null && interact.Trading && interact.TradeID == Identifier)
				{
					interact.TradeClose();
				}
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(34));
				binaryWriter.Write(Convert.ToBoolean(value: false));
				binaryWriter.Seek(0, SeekOrigin.Begin);
				binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
				Send(memoryStream.ToArray());
				binaryWriter.Close();
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				Close();
			}
		}

		public void Friendlist(bool offline)
		{
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(5));
				Window = Dialog.Create(binaryWriter, 0, "FriendList");
				Dialog.ItemText(binaryWriter, breaker: true, $"~1Friend list ({Profile.Data.Friends.Count} of 100 friends)", 75, 3);
				if (Profile.Data.Friends.Count == 0)
				{
					Dialog.Text(binaryWriter, breaker: true, "You don't have any friends, wrench someone and", 50);
					Dialog.Text(binaryWriter, breaker: true, "click ~1Add as friend ~0to add them as friend.", 50);
				}
				else
				{
					if (offline)
					{
						Dialog.Text(binaryWriter, breaker: true, "Showing both online and offline friends.", 50);
					}
					else
					{
						Dialog.Text(binaryWriter, breaker: true, "Showing only currently online friends.", 50);
					}
					Dialog.Space(binaryWriter);
					foreach (string friend in Profile.Data.Friends)
					{
						Player player = null;
						Player[] array = Server.Online.ToArray();
						foreach (Player player2 in array)
						{
							if (player2.Profile.Data.Filename == friend)
							{
								player = player2;
							}
						}
						string name = Profile.Data.Friends.IndexOf(friend).ToString();
						if (player == null)
						{
							if (offline)
							{
								Dialog.Button(binaryWriter, breaker: true, name, $"~1{friend} ~0(~3Offline~0)");
							}
						}
						else if (player.Profile.Data.FriendOffline)
						{
							if (offline)
							{
								Dialog.Button(binaryWriter, breaker: true, name, $"~1{friend} ~0(~3Offline~0)");
							}
						}
						else if (player.Profile.Data.FriendUnknown)
						{
							Dialog.Button(binaryWriter, breaker: true, name, $"~1{friend} ~0(~6Unknown~0)");
						}
						else if (!player.Profile.Visible)
						{
							Dialog.Button(binaryWriter, breaker: true, name, $"~1{friend} ~0(~6Unknown~0)");
						}
						else if (player.Session.Active)
						{
							Dialog.Button(binaryWriter, breaker: true, name, $"~1{friend} ~0(in ~4{player.Session.Data.Name}~0)");
						}
						else
						{
							Dialog.Button(binaryWriter, breaker: true, name, $"~1{friend} ~0(in menus~0)");
						}
					}
					Dialog.Space(binaryWriter);
					if (!offline)
					{
						Dialog.Button(binaryWriter, breaker: true, "ShowOffline", "Show offline friends as well");
					}
					else
					{
						Dialog.Button(binaryWriter, breaker: true, "HideOffline", "Show online friends only");
					}
				}
				Dialog.Button(binaryWriter, breaker: false, "Cancel", "Cancel");
				binaryWriter.Seek(0, SeekOrigin.Begin);
				binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
				Send(memoryStream.ToArray());
				binaryWriter.Close();
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				Close();
			}
		}

		public void GameJoin(int index)
		{
			try
			{
				GameJoinData gameJoinData = Session.GetGameJoinData(index);
				if (GameID != 0 || gameJoinData.Game == 0)
				{
					return;
				}
				int num = 0;
				int num2 = 0;
				Player[] array = Server.Online.ToArray();
				foreach (Player player2 in array)
				{
					if (player2.Active && player2.Profile.Active && player2.Session.Active && !(player2.Session.Data.Name != Session.Data.Name))
					{
						if (player2.GameID == gameJoinData.Game && player2.GameWaiting)
						{
							num++;
						}
						if (player2.GameID == gameJoinData.Game && player2.GamePlaying)
						{
							num2++;
						}
					}
				}
				if (num2 > 0)
				{
					MemoryStream memoryStream = new MemoryStream();
					BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
					binaryWriter.Write(Convert.ToUInt16(0));
					binaryWriter.Write(Convert.ToUInt16(35));
					binaryWriter.Write(Convert.ToUInt16(200));
					binaryWriter.Write(Convert.ToUInt16(3));
					binaryWriter.Write(Encoding.UTF8.GetBytes("~1GAME IS ~3FULL~1!\0"));
					binaryWriter.Write(Encoding.UTF8.GetBytes("Other players haven't yet finished the game.\0"));
					binaryWriter.Write(Encoding.UTF8.GetBytes("It would be unfair to join in the middle.\0"));
					binaryWriter.Seek(0, SeekOrigin.Begin);
					binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
					Send(memoryStream.ToArray());
					binaryWriter.Close();
				}
				else if (num < gameJoinData.Size)
				{
					num++;
					GameID = gameJoinData.Game;
					GameWaiting = true;
					GamePlaying = false;
					MemoryStream memoryStream2 = new MemoryStream();
					BinaryWriter binaryWriter2 = new BinaryWriter(memoryStream2);
					binaryWriter2.Write(Convert.ToUInt16(0));
					binaryWriter2.Write(Convert.ToUInt16(4));
					string text = $"~0[~1{Profile.Data.Username}~0 has joined a game]";
					binaryWriter2.Write(Encoding.UTF8.GetBytes(text + "\0"));
					binaryWriter2.Seek(0, SeekOrigin.Begin);
					binaryWriter2.Write(Convert.ToUInt16(memoryStream2.Length));
					Server.Broadcast(memoryStream2.ToArray(), delegate(Player player)
					{
						if (!player.Active)
						{
							return false;
						}
						if (!player.Profile.Active)
						{
							return false;
						}
						if (!player.Session.Active)
						{
							return false;
						}
						return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
					});
					binaryWriter2.Close();
					if (num < gameJoinData.Size)
					{
						int num3 = gameJoinData.Size - num;
						memoryStream2 = new MemoryStream();
						binaryWriter2 = new BinaryWriter(memoryStream2);
						binaryWriter2.Write(Convert.ToUInt16(0));
						binaryWriter2.Write(Convert.ToUInt16(35));
						binaryWriter2.Write(Convert.ToUInt16(200));
						binaryWriter2.Write(Convert.ToUInt16(2));
						binaryWriter2.Write(Encoding.UTF8.GetBytes("~1YOU'VE ~4JOINED~1 A GAME!\0"));
						binaryWriter2.Write(Encoding.UTF8.GetBytes($"You have joined a game, waiting for ~1{num3} ~0more players." + "\0"));
						binaryWriter2.Write(Encoding.UTF8.GetBytes("\0"));
						binaryWriter2.Seek(0, SeekOrigin.Begin);
						binaryWriter2.Write(Convert.ToUInt16(memoryStream2.Length));
						Send(memoryStream2.ToArray());
						binaryWriter2.Close();
					}
					else
					{
						if (num < gameJoinData.Size)
						{
							return;
						}
						Player[] array2 = Server.Online.ToArray();
						foreach (Player player3 in array2)
						{
							if (player3.Active && player3.Profile.Active && player3.Session.Active && !(player3.Session.Data.Name != Session.Data.Name) && player3.GameID == gameJoinData.Game)
							{
								player3.GameSpawn();
							}
						}
					}
				}
				else
				{
					MemoryStream memoryStream3 = new MemoryStream();
					BinaryWriter binaryWriter3 = new BinaryWriter(memoryStream3);
					binaryWriter3.Write(Convert.ToUInt16(0));
					binaryWriter3.Write(Convert.ToUInt16(35));
					binaryWriter3.Write(Convert.ToUInt16(200));
					binaryWriter3.Write(Convert.ToUInt16(3));
					binaryWriter3.Write(Encoding.UTF8.GetBytes("~1GAME IS ~3FULL~1!\0"));
					binaryWriter3.Write(Encoding.UTF8.GetBytes("The game you tried to join is full, look for another one.\0"));
					binaryWriter3.Write(Encoding.UTF8.GetBytes("\0"));
					binaryWriter3.Seek(0, SeekOrigin.Begin);
					binaryWriter3.Write(Convert.ToUInt16(memoryStream3.Length));
					Send(memoryStream3.ToArray());
					binaryWriter3.Close();
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				Close();
			}
		}

		public void GameSpawn()
		{
			try
			{
				if (GameID != 0 && GameWaiting)
				{
					GameWaiting = false;
					GamePlaying = true;
					int gameSpawn = Session.GetGameSpawn(GameID);
					CurrentX = Session.GetTileX(gameSpawn) * 32 + 8;
					CurrentY = Session.GetTileY(gameSpawn) * 32;
					PlayerCore.UpdatePosition(this, CurrentX, CurrentY);
					PlayerLayout.Warning(this, 200, 2, "~1GAME HAS BEEN ~4STARTED~1!", "Game has been started, first one to reach the finish wins.", "");
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				Close();
			}
		}

		public void GameCancel()
		{
			try
			{
				if (GameID == 0)
				{
					return;
				}
				GameID = 0;
				GameWaiting = false;
				GamePlaying = false;
				PlayerLayout.Warning(this, 200, 3, "~1YOU ~3LOST~1!", "You have been disqualified from the game.", "");
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(4));
				string text = $"~0[~1{Profile.Data.Username}~0 has been disqualified]";
				binaryWriter.Write(Encoding.UTF8.GetBytes(text + "\0"));
				binaryWriter.Seek(0, SeekOrigin.Begin);
				binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
				Server.Broadcast(memoryStream.ToArray(), delegate(Player player)
				{
					if (!player.Active)
					{
						return false;
					}
					if (!player.Profile.Active)
					{
						return false;
					}
					if (!player.Session.Active)
					{
						return false;
					}
					return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
				});
				binaryWriter.Close();
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				Close();
			}
		}

		public void GameFinish(int index, bool reached)
		{
			try
			{
				GameFinishData gameFinishData = Session.GetGameFinishData(index);
				if (GameID == 0 || GameID != gameFinishData.Game)
				{
					return;
				}
				GameID = 0;
				GameWaiting = false;
				GamePlaying = false;
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(29));
				binaryWriter.Write(Convert.ToInt32(0));
				binaryWriter.Write(Convert.ToUInt16(2));
				binaryWriter.Seek(0, SeekOrigin.Begin);
				binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
				Send(memoryStream.ToArray());
				binaryWriter.Close();
				memoryStream = new MemoryStream();
				binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Convert.ToUInt16(0));
				binaryWriter.Write(Convert.ToUInt16(29));
				binaryWriter.Write(Convert.ToInt32(Identifier));
				binaryWriter.Write(Convert.ToUInt16(2));
				binaryWriter.Seek(0, SeekOrigin.Begin);
				binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
				Server.Broadcast(memoryStream.ToArray(), delegate(Player player)
				{
					if (player == this)
					{
						return false;
					}
					if (!player.Active)
					{
						return false;
					}
					if (!player.Profile.Active)
					{
						return false;
					}
					if (!player.Session.Active)
					{
						return false;
					}
					return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
				});
				binaryWriter.Close();
				if (reached)
				{
					Player[] array = Server.Online.ToArray();
					foreach (Player player2 in array)
					{
						if (player2.Active && player2.Profile.Active && player2.Session.Active && !(player2.Session.Data.Name != Session.Data.Name) && player2.GameID == gameFinishData.Game)
						{
							player2.GameFinish(index, reached: false);
						}
					}
					PlayerLayout.Warning(this, 200, 2, "~1YOU ~4WON~1!", "Congratulations, you've taken the first place in this game.", "");
					PlayerQuests.Event(this, PlayerEvent.WinGame);
					memoryStream = new MemoryStream();
					binaryWriter = new BinaryWriter(memoryStream);
					binaryWriter.Write(Convert.ToUInt16(0));
					binaryWriter.Write(Convert.ToUInt16(4));
					string text = $"~0[~1{Profile.Data.Username}~0 has won a game]";
					binaryWriter.Write(Encoding.UTF8.GetBytes(text + "\0"));
					binaryWriter.Seek(0, SeekOrigin.Begin);
					binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
					Server.Broadcast(memoryStream.ToArray(), delegate(Player player)
					{
						if (!player.Active)
						{
							return false;
						}
						if (!player.Profile.Active)
						{
							return false;
						}
						if (!player.Session.Active)
						{
							return false;
						}
						return (!(player.Session.Data.Name != Session.Data.Name)) ? true : false;
					});
					binaryWriter.Close();
				}
				else
				{
					memoryStream = new MemoryStream();
					binaryWriter = new BinaryWriter(memoryStream);
					binaryWriter.Write(Convert.ToUInt16(0));
					binaryWriter.Write(Convert.ToUInt16(35));
					binaryWriter.Write(Convert.ToUInt16(200));
					binaryWriter.Write(Convert.ToUInt16(3));
					binaryWriter.Write(Encoding.UTF8.GetBytes("~1YOU ~3LOST~1!\0"));
					binaryWriter.Write(Encoding.UTF8.GetBytes("Someone else has finished faster than you,\0"));
					binaryWriter.Write(Encoding.UTF8.GetBytes("good luck next time.\0"));
					binaryWriter.Seek(0, SeekOrigin.Begin);
					binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
					Send(memoryStream.ToArray());
					binaryWriter.Close();
				}
				PlayerQuests.Event(this, PlayerEvent.PlayGame);
				if (Session.Checkpoint)
				{
					Session.Checkpoint = false;
					Session.CheckpointX = 0;
					Session.CheckpointY = 0;
					memoryStream = new MemoryStream();
					binaryWriter = new BinaryWriter(memoryStream);
					binaryWriter.Write(Convert.ToUInt16(0));
					binaryWriter.Write(Convert.ToUInt16(11));
					binaryWriter.Write(Convert.ToUInt16(Session.CheckpointX));
					binaryWriter.Write(Convert.ToUInt16(Session.CheckpointY));
					binaryWriter.Write(Convert.ToUInt16(3));
					binaryWriter.Write(Convert.ToUInt16(0));
					binaryWriter.Seek(0, SeekOrigin.Begin);
					binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
					Send(memoryStream.ToArray());
					binaryWriter.Close();
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				Close();
			}
		}

		public int SetInteract(Player player)
		{
			return player?.Identifier ?? 0;
		}

		public Player GetInteract(int id)
		{
			Player[] array = Server.Online.ToArray();
			foreach (Player player in array)
			{
				if (player.Active && player.Profile.Active && player.Session.Active && player.Identifier == id && !(player.Session.Data.Name != Session.Data.Name))
				{
					return player;
				}
			}
			return null;
		}

		public void Reward1()
		{
			try
			{
				TimeSpan timeSpan = DateTime.UtcNow.Subtract(Server.Date);
				if (!((double)Profile.Data.RewardCooldown <= timeSpan.TotalSeconds) && !Profile.Data.AllowRespin)
				{
					return;
				}
				if ((double)Profile.Data.RewardCooldown <= timeSpan.TotalSeconds)
				{
					Profile.Data.RewardCooldown = Convert.ToInt32(timeSpan.TotalSeconds) + 28800;
					Profile.Data.AllowRespin = true;
				}
				else
				{
					Profile.Data.AllowRespin = false;
				}
				RewardItem reward = Rewards.Daily[Server.Random.Next(Rewards.Daily.Count)];
				if (Server.Random.Next(reward.Priority) == 0 && Profile.CanGetReward(Rewards.Daily.ToArray()))
				{
					Profile.SetItem(reward.Index, Profile.GetItem(reward.Index) + reward.Count);
					PlayerCore.UpdateInventory(this);
					PlayerCore.UpdateDialog(this, 0, "Wrench.RewardBox1.Respin", delegate(BinaryWriter dialog)
					{
						Dialog.ItemText(dialog, breaker: true, "~1Daily reward", 75, 3);
						Dialog.Text(dialog, breaker: true, "You won the grand prize!", 75);
						Dialog.ItemText(dialog, breaker: true, $"x{reward.Count} {Item.Name(reward.Index)}", 50, reward.Index);
						if (Profile.Data.AllowRespin)
						{
							Dialog.Button(dialog, breaker: false, "Respin", "Try again");
						}
						Dialog.Button(dialog, breaker: false, "Okay", "Okay");
					});
					Server.SendLog(Profile.Data.Filename, $"Won {Item.Name(reward.Index)} from daily rewards");
				}
				else
				{
					int gems = 0;
					if (Server.Random.Next(5) == 0)
					{
						gems = Server.Random.Next(500) + 500;
					}
					else
					{
						gems = Server.Random.Next(250) + 250;
					}
					PlayerGems.Add(this, gems);
					PlayerCore.UpdateDialog(this, 0, "Wrench.RewardBox1.Respin", delegate(BinaryWriter dialog)
					{
						Dialog.ItemText(dialog, breaker: true, "~1Daily reward", 75, 3);
						Dialog.Text(dialog, breaker: true, $"You've been rewarded with ~1{gems} ~0gems!", 75);
						if (Profile.Data.AllowRespin)
						{
							Dialog.Button(dialog, breaker: false, "Respin", "Try again");
						}
						Dialog.Button(dialog, breaker: false, "Okay", "Okay");
					});
				}
				Experience(randomize: true, 200);
				Database.ProfileSave(Profile.Data);
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				Close();
			}
		}

		public void Reward2()
		{
			try
			{
				TimeSpan timeSpan = DateTime.UtcNow.Subtract(Server.Date);
				if ((double)Profile.Data.VideoCooldown <= timeSpan.TotalSeconds)
				{
					int gems = Server.Random.Next(50) + 50;
					PlayerGems.Add(this, gems);
					PlayerCore.UpdateDialog(this, 0, "Message", delegate(BinaryWriter dialog)
					{
						Dialog.ItemText(dialog, breaker: true, "~1Advertisement", 75, 3);
						Dialog.Text(dialog, breaker: true, $"You've been rewarded with ~1{gems} ~0gems.", 50);
						Dialog.Button(dialog, breaker: false, "Okay", "Okay");
					});
					Experience(randomize: true, 50);
					Profile.Data.VideoCooldown = Convert.ToInt32(timeSpan.TotalSeconds) + 900;
					Database.ProfileSave(Profile.Data);
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				Close();
			}
		}

		public void Reward3()
		{
			try
			{
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				Close();
			}
		}

		public void Close()
		{
			if (Closed)
			{
				return;
			}
			Closed = true;
			if (Profile != null && Profile.Active)
			{
				try
				{
					Profile.Data.Online += Profile.GetOnline();
					if (!Profile.Data.FriendOffline)
					{
						PlayerConsole.FriendNotification(this, "has logged out");
					}
					Database.ProfileSave(Profile.Data);
					Database.ProfileClose(Profile.Data.Filename, binded: true);
				}
				catch (Exception exception)
				{
					Terminal.Exception(exception);
				}
				finally
				{
					Profile.Active = false;
				}
			}
			if (Session != null && Session.Active)
			{
				try
				{
					PlayerCore.DestroyCharacter(this);
					Player[] array = Server.Online.ToArray();
					foreach (Player player in array)
					{
						if (player.Active && player.Profile.Active && player.Session.Active && !(player.Session.Data.Name != Session.Data.Name) && Profile.VisibleTo(player))
						{
							MemoryStream memoryStream = new MemoryStream();
							BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
							binaryWriter.Write(Convert.ToUInt16(0));
							binaryWriter.Write(Convert.ToUInt16(4));
							string text = $"~0[~1{Profile.Data.Username} ~0has logged out]";
							binaryWriter.Write(Encoding.UTF8.GetBytes(text + "\0"));
							binaryWriter.Seek(0, SeekOrigin.Begin);
							binaryWriter.Write(Convert.ToUInt16(memoryStream.Length));
							player.Send(memoryStream.ToArray());
							binaryWriter.Close();
						}
					}
					Session.Leave();
				}
				catch (Exception exception2)
				{
					Terminal.Exception(exception2);
				}
				finally
				{
					Session.Active = false;
				}
			}
			Active = false;
			Server.Disconnect(this);
		}
	}
}
