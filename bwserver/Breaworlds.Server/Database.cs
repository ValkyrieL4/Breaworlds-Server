using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Breaworlds.Server
{
	internal class Database
	{
		public static readonly bool Debugging = false;

		public static readonly bool Synchronized = true;

		public static readonly string ProfileDirectory = "./profiles";

		public static readonly string ProfileExtension = "profile.bin";

		public static readonly string ProfileTemporary = "profile.tmp";

		public static readonly string ProfileCompressed = "profile.cmp";

		public static Dictionary<string, ProfileDataHandle> ProfileCache = new Dictionary<string, ProfileDataHandle>();

		public static readonly string SessionDirectory = "./sessions";

		public static readonly string SessionExtension = "session.bin";

		public static readonly string SessionTemporary = "session.tmp";

		public static readonly string SessionCompressed = "session.cmp";

		public static Dictionary<string, SessionDataHandle> SessionCache = new Dictionary<string, SessionDataHandle>();

		public static readonly string ChallengeDirectory = "./challenges";

		public static readonly string ChallengeExtension = "challenge.bin";

		public static readonly string ChallengeTemporary = "challenge.tmp";

		public static readonly string ChallengeCompressed = "challenge.cmp";

		public static Dictionary<string, ChallengeDataHandle> ChallengeCache = new Dictionary<string, ChallengeDataHandle>();

		public static void Initialize()
		{
			Loop(10000);
		}

		public static void QueryProfiles(Action<ProfileData, FileInfo> action)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(ProfileDirectory);
			FileInfo[] files = directoryInfo.GetFiles("*.profile.bin");
			FileInfo[] array = files;
			foreach (FileInfo fileInfo in array)
			{
				using BinaryReader reader = new BinaryReader(fileInfo.Open(FileMode.Open));
				action(Serializer.DeserializeProfile(reader), fileInfo);
			}
		}

		public static void QuerySessions(Action<SessionData, FileInfo> action)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(SessionDirectory);
			FileInfo[] files = directoryInfo.GetFiles("*.session.bin");
			FileInfo[] array = files;
			foreach (FileInfo fileInfo in array)
			{
				using BinaryReader reader = new BinaryReader(fileInfo.Open(FileMode.Open));
				action(Serializer.DeserializeSession(reader), fileInfo);
			}
		}

		public static void CleanTemporaryProfiles()
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(SessionDirectory);
			FileInfo[] files = directoryInfo.GetFiles("*.session.tmp");
			FileInfo[] array = files;
			foreach (FileInfo fileInfo in array)
			{
				fileInfo.Delete();
			}
		}

		public static void CleanTemporarySessions()
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(ProfileDirectory);
			FileInfo[] files = directoryInfo.GetFiles("*.profile.tmp");
			FileInfo[] array = files;
			foreach (FileInfo fileInfo in array)
			{
				fileInfo.Delete();
			}
		}

		public static void CleanTemporaryChallenges()
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(ChallengeDirectory);
			FileInfo[] files = directoryInfo.GetFiles("*.challenge.tmp");
			FileInfo[] array = files;
			foreach (FileInfo fileInfo in array)
			{
				fileInfo.Delete();
			}
		}

		public static bool ProfileExists(string name)
		{
			string path = $"{ProfileDirectory}/{name.ToUpper()}.{ProfileExtension}";
			if (name.Length > 0)
			{
				return File.Exists(path);
			}
			return false;
		}

		public static bool SessionExists(string name)
		{
			string path = $"{SessionDirectory}/{name.ToUpper()}.{SessionExtension}";
			if (name.Length > 0)
			{
				return File.Exists(path);
			}
			return false;
		}

		public static bool ChallengeExists(string name)
		{
			string path = $"{ChallengeDirectory}/{name.ToUpper()}.{ChallengeExtension}";
			if (name.Length > 0)
			{
				return File.Exists(path);
			}
			return false;
		}

		public static void ProfileSave(ProfileDataHandle data)
		{
			try
			{
				if (ProfileCache.TryGetValue(data.Filename.ToUpper(), out var value))
				{
					value.State = CacheState.Awaiting;
				}
				else
				{
					ProfileCache[data.Filename.ToUpper()] = new ProfileDataHandle(binded: true)
					{
						State = CacheState.Awaiting,
						Data = data.Data
					};
				}
				if (Debugging)
				{
					Terminal.Message("Profile {0} saving.", data.Filename.ToUpper());
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
		}

		public static void SessionSave(SessionDataHandle data)
		{
			try
			{
				if (SessionCache.TryGetValue(data.Filename.ToUpper(), out var value))
				{
					value.State = CacheState.Awaiting;
				}
				else
				{
					SessionCache[data.Filename.ToUpper()] = new SessionDataHandle(binded: true)
					{
						State = CacheState.Awaiting,
						Data = data.Data
					};
				}
				if (Debugging)
				{
					Terminal.Message("Session {0} saving.", data.Filename.ToUpper());
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
		}

		public static void ChallengeSave(ChallengeDataHandle data)
		{
			try
			{
				if (ChallengeCache.TryGetValue(data.Filename.ToUpper(), out var value))
				{
					value.State = CacheState.Awaiting;
				}
				else
				{
					ChallengeCache[data.Filename.ToUpper()] = new ChallengeDataHandle(binded: true)
					{
						State = CacheState.Awaiting,
						Data = data.Data
					};
				}
				if (Debugging)
				{
					Terminal.Message("Challenge {0} saving.", data.Filename.ToUpper());
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
		}

		public static void ProfileLoad(ref ProfileDataHandle data, string name)
		{
			try
			{
				if (ProfileCache.TryGetValue(name.ToUpper(), out var value))
				{
					data = value;
					return;
				}
				string path = $"{ProfileDirectory}/{name.ToUpper()}.{ProfileExtension}";
				if (File.Exists(path) && name.Length > 0)
				{
					FileStream input = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
					using (BinaryReader reader = new BinaryReader(input))
					{
						ProfileCache[name.ToUpper()] = new ProfileDataHandle(binded: true)
						{
							Data = Serializer.DeserializeProfile(reader),
							State = CacheState.Complete
						};
					}
					data = ProfileCache[name.ToUpper()];
				}
				if (Debugging)
				{
					Terminal.Message("Profile {0} has been cached.", name.ToUpper());
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
		}

		public static void SessionLoad(ref SessionDataHandle data, string name)
		{
			try
			{
				if (SessionCache.TryGetValue(name.ToUpper(), out var value))
				{
					data = value;
					return;
				}
				string path = $"{SessionDirectory}/{name.ToUpper()}.{SessionExtension}";
				if (File.Exists(path) && name.Length > 0)
				{
					FileStream input = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
					using (BinaryReader reader = new BinaryReader(input))
					{
						SessionCache[name.ToUpper()] = new SessionDataHandle(binded: true)
						{
							Data = Serializer.DeserializeSession(reader),
							State = CacheState.Complete
						};
					}
					data = SessionCache[name.ToUpper()];
				}
				if (Debugging)
				{
					Terminal.Message("Session {0} has been cached.", name.ToUpper());
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
		}

		public static void ChallengeLoad(ref ChallengeDataHandle data, string name)
		{
			try
			{
				if (ChallengeCache.TryGetValue(name.ToUpper(), out var value))
				{
					data = value;
					return;
				}
				string path = $"{ChallengeDirectory}/{name.ToUpper()}.{ChallengeExtension}";
				if (File.Exists(path) && name.Length > 0)
				{
					FileStream input = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
					using (BinaryReader reader = new BinaryReader(input))
					{
						ChallengeCache[name.ToUpper()] = new ChallengeDataHandle(binded: true)
						{
							Data = Serializer.DeserializeChallenge(reader),
							State = CacheState.Complete
						};
					}
					data = ChallengeCache[name.ToUpper()];
				}
				if (Debugging)
				{
					Terminal.Message("Challenge {0} has been cached.", name.ToUpper());
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
		}

		public static void ProfileFlush(string name)
		{
			try
			{
				if (ProfileCache.TryGetValue(name.ToUpper(), out var value) && value.State == CacheState.Awaiting)
				{
					string destFileName = $"{ProfileDirectory}/{name.ToUpper()}.{ProfileExtension}";
					string text = $"{ProfileDirectory}/{name.ToUpper()}.{ProfileTemporary}";
					FileStream output = new FileStream(text, FileMode.Create, FileAccess.Write, FileShare.None);
					using (BinaryWriter writer = new BinaryWriter(output))
					{
						Serializer.SerializeProfile(writer, value.Data);
					}
					File.Copy(text, destFileName, overwrite: true);
					File.Delete(text);
					value.State = CacheState.Complete;
					if (Debugging)
					{
						Terminal.Message("Profile {0} has been flushed.", name);
					}
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
		}

		public static void SessionFlush(string name)
		{
			try
			{
				if (SessionCache.TryGetValue(name.ToUpper(), out var value) && value.State == CacheState.Awaiting)
				{
					string destFileName = $"{SessionDirectory}/{name.ToUpper()}.{SessionExtension}";
					string text = $"{SessionDirectory}/{name.ToUpper()}.{SessionTemporary}";
					FileStream output = new FileStream(text, FileMode.Create, FileAccess.Write, FileShare.None);
					using (BinaryWriter writer = new BinaryWriter(output))
					{
						Serializer.SerializeSession(writer, value.Data);
					}
					File.Copy(text, destFileName, overwrite: true);
					File.Delete(text);
					value.State = CacheState.Complete;
					if (Debugging)
					{
						Terminal.Message("Session {0} has been flushed.", name);
					}
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
		}

		public static void ChallengeFlush(string name)
		{
			try
			{
				if (ChallengeCache.TryGetValue(name.ToUpper(), out var value) && value.State == CacheState.Awaiting)
				{
					string destFileName = $"{ChallengeDirectory}/{name.ToUpper()}.{ChallengeExtension}";
					string text = $"{ChallengeDirectory}/{name.ToUpper()}.{ChallengeTemporary}";
					FileStream output = new FileStream(text, FileMode.Create, FileAccess.Write, FileShare.None);
					using (BinaryWriter writer = new BinaryWriter(output))
					{
						Serializer.SerializeChallenge(writer, value.Data);
					}
					File.Copy(text, destFileName, overwrite: true);
					File.Delete(text);
					value.State = CacheState.Complete;
					if (Debugging)
					{
						Terminal.Message("Challenge {0} has been flushed.", name);
					}
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
		}

		public static void ProfileClose(string name, bool binded)
		{
			try
			{
				int num = 0;
				Player[] array = Server.Online.ToArray();
				foreach (Player player in array)
				{
					if (player.Profile != null && player.Profile.Data != null && player.Profile.Active && player.Profile.Data.Filename.ToUpper() == name.ToUpper())
					{
						num++;
					}
				}
				if (binded)
				{
					num--;
				}
				if (num <= 0 && ProfileCache.ContainsKey(name.ToUpper()))
				{
					ProfileFlush(name.ToUpper());
					ProfileCache.Remove(name.ToUpper());
					if (Debugging)
					{
						Terminal.Message("Profile {0} has been finalized and closed.", name.ToUpper());
					}
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
		}

		public static void SessionClose(string name, bool binded)
		{
			try
			{
				int num = 0;
				Player[] array = Server.Online.ToArray();
				foreach (Player player in array)
				{
					if (player.Session != null && player.Session.Data != null && player.Session.Active && player.Session.Data.Filename.ToUpper() == name.ToUpper())
					{
						num++;
					}
				}
				if (binded)
				{
					num--;
				}
				if (num <= 0 && SessionCache.ContainsKey(name.ToUpper()))
				{
					SessionFlush(name.ToUpper());
					SessionCache.Remove(name.ToUpper());
					if (Debugging)
					{
						Terminal.Message("Session {0} has been finalized and closed.", name.ToUpper());
					}
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
		}

		public static void ChallengeClose(string name)
		{
			try
			{
				if (ChallengeCache.ContainsKey(name.ToUpper()))
				{
					ChallengeFlush(name.ToUpper());
					ChallengeCache.Remove(name.ToUpper());
					if (Debugging)
					{
						Terminal.Message("Challenge {0} has been finalized and closed.", name.ToUpper());
					}
				}
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
			}
		}

		public static void Flush(bool profiles, bool sessions, bool challenges)
		{
			try
			{
				if (profiles)
				{
					string[] array = ProfileCache.Keys.ToArray();
					foreach (string name in array)
					{
						ProfileFlush(name);
					}
				}
				if (sessions)
				{
					string[] array2 = SessionCache.Keys.ToArray();
					foreach (string name2 in array2)
					{
						SessionFlush(name2);
					}
				}
				if (challenges)
				{
					string[] array3 = ChallengeCache.Keys.ToArray();
					foreach (string name3 in array3)
					{
						ChallengeFlush(name3);
					}
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
				Flush(profiles: true, sessions: true, challenges: true);
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
