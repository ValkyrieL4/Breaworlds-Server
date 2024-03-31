using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Breaworlds.Server
{
	internal class Terminal
	{
		public static void Message(string format, params object[] args)
		{
			Console.WriteLine(string.Format(format, args));
		}

		public static void Exception(Exception exception)
		{
			if (!(exception is SocketException))
			{
				Message(exception.ToString());
			}
		}

		public static void SubmitLog(string username, string content)
		{
			try
			{
				NameValueCollection data = new NameValueCollection
				{
					{ "username", username },
					{ "content", content }
				};
				Thread thread = new Thread((ThreadStart)delegate
				{
					using WebClient webClient = new WebClient();
					webClient.UploadValues("https://discordapp.com/api/webhooks/611888223739838466/C8BDSgSw6DR-49_HXAaLtiZPOlTeo7Yv2ihX_izwxw7pc8T1W8ScLqIBGnwULexEAvMN", data);
				});
				thread.Start();
			}
			catch (Exception exception)
			{
				Exception(exception);
			}
		}

		public static void SubmitReport(string username, string content)
		{
			try
			{
				NameValueCollection data = new NameValueCollection
				{
					{ "username", username },
					{ "content", content }
				};
				Thread thread = new Thread((ThreadStart)delegate
				{
					using WebClient webClient = new WebClient();
					webClient.UploadValues("https://discordapp.com/api/webhooks/612347363943514132/sSG91EVzOrt8UxE05O1MIVipb7gNTsweagHVJxTFjqtBoCEsFVMPupSiS0xowJFEtNF4", data);
				});
				thread.Start();
			}
			catch (Exception exception)
			{
				Exception(exception);
			}
		}
	}
}
