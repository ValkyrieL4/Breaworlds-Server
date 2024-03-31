using System;

namespace Breaworlds.Server
{
	public class PlayerControl
	{
		public static bool CanPull(Player invoker, Player target)
		{
			try
			{
				int num = (invoker.CurrentX + 8) / 32;
				int num2 = (invoker.CurrentY + 16) / 32;
				int num3 = (target.CurrentX + 8) / 32;
				int num4 = (target.CurrentY + 16) / 32;
				int tile = num + invoker.Session.Data.SizeX * num2;
				int tile2 = num3 + invoker.Session.Data.SizeX * num4;
				if (invoker == target)
				{
					return false;
				}
				if (invoker.Session.Data.Name != target.Session.Data.Name)
				{
					return false;
				}
				if (Rewards.Permission(invoker.Profile.Data.Filename, Permissions.Mod))
				{
					return true;
				}
				if (target.Session.TileOwner(target.Profile.Data.Filename, tile2))
				{
					return false;
				}
				if (!invoker.Session.TileLocked(tile))
				{
					return false;
				}
				if (!invoker.Session.TileLocked(tile2))
				{
					return false;
				}
				if (!invoker.Session.TileAdmin(invoker.Profile.Data.Filename, tile))
				{
					return false;
				}
				if (!invoker.Session.TileAdmin(invoker.Profile.Data.Filename, tile2))
				{
					return false;
				}
				return target.Profile.Pullable;
			}
			catch (Exception)
			{
				invoker.Close();
				return false;
			}
		}

		public static bool CanKill(Player invoker, Player target)
		{
			try
			{
				int num = (target.CurrentX + 8) / 32;
				int num2 = (target.CurrentY + 16) / 32;
				int tile = num + invoker.Session.Data.SizeX * num2;
				if (invoker == target)
				{
					return false;
				}
				if (invoker.Session.Data.Name != target.Session.Data.Name)
				{
					return false;
				}
				if (Rewards.Permission(invoker.Profile.Data.Filename, Permissions.Mod))
				{
					return true;
				}
				if (target.Session.TileOwner(target.Profile.Data.Filename, tile))
				{
					return false;
				}
				if (!invoker.Session.TileLocked(tile))
				{
					return false;
				}
				if (!invoker.Session.TileAdmin(invoker.Profile.Data.Filename, tile))
				{
					return false;
				}
				return target.Profile.Killable;
			}
			catch (Exception)
			{
				invoker.Close();
				return false;
			}
		}

		public static bool CanBan(Player invoker, Player target)
		{
			try
			{
				if (invoker == target)
				{
					return false;
				}
				if (invoker.Session.Data.Name != target.Session.Data.Name)
				{
					return false;
				}
				if (target.Session.WorldAdmin(target.Profile.Data.Filename))
				{
					return false;
				}
				if (!invoker.Session.WorldLocked())
				{
					return false;
				}
				if (Rewards.Permission(invoker.Profile.Data.Filename, Permissions.Mod))
				{
					return true;
				}
				if (!invoker.Session.WorldAdmin(invoker.Profile.Data.Filename))
				{
					return false;
				}
				return target.Profile.Bannable;
			}
			catch (Exception)
			{
				invoker.Close();
				return false;
			}
		}

		public static bool CanUnban(Player invoker)
		{
			try
			{
				if (!invoker.Session.WorldLocked())
				{
					return false;
				}
				if (Rewards.Permission(invoker.Profile.Data.Filename, Permissions.Mod))
				{
					return true;
				}
				if (!invoker.Session.WorldAdmin(invoker.Profile.Data.Filename))
				{
					return false;
				}
				return true;
			}
			catch (Exception)
			{
				invoker.Close();
				return false;
			}
		}

		public static bool CanFreeze(Player invoker, Player target)
		{
			try
			{
				if (invoker == target)
				{
					return false;
				}
				if (!Rewards.Permission(invoker.Profile.Data.Filename, Permissions.Mod))
				{
					return false;
				}
				return true;
			}
			catch (Exception)
			{
				invoker.Close();
				return false;
			}
		}

		public static void Pull(Player invoker, Player target)
		{
			try
			{
				if (!CanPull(invoker, target))
				{
					PlayerConsole.Message(invoker, "You have no access to pull ~1{0}~0!", target.Profile.Data.Username);
					return;
				}
				PlayerCore.UpdatePosition(target, invoker.CurrentX, invoker.CurrentY);
				PlayerConsole.Message(invoker, "Player ~1{0} ~0has been pulled.", target.Profile.Data.Username);
				PlayerConsole.Message(target, "You have been pulled by ~1{0}~0!", invoker.Profile.Data.Username);
			}
			catch (Exception)
			{
				invoker.Close();
			}
		}

		public static void Kill(Player invoker, Player target)
		{
			try
			{
				if (!CanKill(invoker, target))
				{
					PlayerConsole.Message(invoker, "You have no access to kill ~1{0}~0!", target.Profile.Data.Username);
					return;
				}
				PlayerCore.UpdateAnimation(target, 2);
				PlayerConsole.Message(invoker, "Player ~1{0} ~0has been killed.", target.Profile.Data.Username);
				PlayerConsole.Message(target, "You have been killed by ~1{0}~0!", invoker.Profile.Data.Username);
			}
			catch (Exception)
			{
				invoker.Close();
			}
		}

		public static void Ban(Player invoker, Player target)
		{
			try
			{
				if (!CanBan(invoker, target))
				{
					PlayerConsole.Message(invoker, "You have no access to world ban ~1{0}~0!", target.Profile.Data.Username);
					return;
				}
				if (invoker.Session.Data.Bans.Count >= 32)
				{
					PlayerConsole.Message(invoker, "There are too many banned players in this world, wait a little and try again.");
					return;
				}
				if (!Rewards.Permission(target.Profile.Data.Filename, Permissions.Mod))
				{
					TimeSpan timeSpan = DateTime.UtcNow.Subtract(Server.Date);
					Database.SessionLoad(ref invoker.Session.Data, invoker.Session.Data.Name);
					invoker.Session.Data.Bans.Add(new BanData
					{
						Time = (int)timeSpan.TotalSeconds + 3600,
						Name = target.Profile.Data.Filename
					});
					Database.SessionSave(invoker.Session.Data);
				}
				target.Warp(Session.Random(target), unban: false);
				PlayerConsole.Message(invoker, "Player ~1{0} ~0has been world banned for one hour.", target.Profile.Data.Username);
				PlayerConsole.Message(target, "You have been world banned by ~1{0}~0 for one hour!", invoker.Profile.Data.Username);
			}
			catch (Exception)
			{
				invoker.Close();
			}
		}

		public static void Unban(Player invoker, string username)
		{
			try
			{
				if (!CanUnban(invoker))
				{
					PlayerConsole.Message(invoker, "You don't have access to unban players in this world.");
					return;
				}
				bool flag = false;
				BanData[] array = invoker.Session.Data.Bans.ToArray();
				for (int i = 0; i < array.Length; i++)
				{
					BanData item = array[i];
					if (item.Name.ToLower().StartsWith(username.ToLower()))
					{
						PlayerConsole.Message(invoker, "Player ~1{0} ~0has been unbanned from the world.", item.Name);
						invoker.Session.Data.Bans.Remove(item);
						flag = true;
					}
				}
				if (!flag)
				{
					PlayerConsole.Message(invoker, "There are no banned players matching your specification.");
				}
			}
			catch (Exception)
			{
				invoker.Close();
			}
		}

		public static void Freeze(Player invoker, Player target)
		{
			try
			{
				if (!CanFreeze(invoker, target))
				{
					PlayerConsole.Message(invoker, "You have no access to freeze ~1{0}~0!", target.Profile.Data.Username);
					return;
				}
				target.Profile.Frozen = !target.Profile.Frozen;
				PlayerCore.UpdateCharacter(target);
				if (target.Profile.Frozen)
				{
					PlayerLayout.Warning(target, 200, 1, "~1YOU HAVE BEEN ~5FROZEN~1!", "You have been frozen, you are now unable to walk.", "");
					PlayerConsole.Message(invoker, "Player ~1{0} ~0has been frozen.", target.Profile.Data.Username);
				}
				else
				{
					PlayerLayout.Warning(target, 200, 1, "~1YOU HAVE BEEN ~5UNFROZEN~1!", "You have been unfrozen, you are now able to walk.", "");
					PlayerConsole.Message(invoker, "Player ~1{0} ~0has been unfrozen.", target.Profile.Data.Username);
				}
			}
			catch (Exception)
			{
				invoker.Close();
			}
		}
	}
}
