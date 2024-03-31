using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace Breaworlds.Server
{
	public class GoogleIap
	{
		public static readonly bool Disabled = false;

		private static readonly string Application = "com.breaworlds.app";

		private static readonly string RefreshToken = "1//09aCrqX5jfjzICgYIARAAGAkSNwF-L9IrY6_yffyiDHGky4wE2TKEiiYSIV9r6o6DwZETn3ZhJs-wosOd-Rcu6q2nMy1HokSou3I";

		private static readonly string ClientID = "755257053187-ga1onjqah19nl7hohcs95blbfgqn10pj.apps.googleusercontent.com";

		private static readonly string ClientSecret = "45F5WmTGigwZq5E9U6vFKsHv";

		public static string GetAccessToken()
		{
			try
			{
				using WebClient webClient = new WebClient();
				NameValueCollection data = new NameValueCollection
				{
					{ "grant_type", "refresh_token" },
					{ "client_id", ClientID },
					{ "client_secret", ClientSecret },
					{ "refresh_token", RefreshToken }
				};
				byte[] bytes = webClient.UploadValues("https://accounts.google.com/o/oauth2/token", data);
				Dictionary<string, object> dictionary = BaseIap.Deserialize(Encoding.UTF8.GetString(bytes));
				if (dictionary.ContainsKey("access_token"))
				{
					return dictionary["access_token"].ToString();
				}
				return string.Empty;
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				return string.Empty;
			}
		}

		private static string GetOrderData(string product, string token)
		{
			try
			{
				string accessToken = GetAccessToken();
				if (!string.IsNullOrEmpty(accessToken))
				{
					using (WebClient webClient = new WebClient())
					{
						return webClient.DownloadString($"https://www.googleapis.com/androidpublisher/v3/applications/{Application}/purchases/products/{product}/tokens/{token}?access_token={accessToken}");
					}
				}
				return string.Empty;
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				return string.Empty;
			}
		}

		public static string GetOrderID(string product, string token)
		{
			try
			{
				string orderData = GetOrderData(product, token);
				if (!string.IsNullOrEmpty(orderData))
				{
					Dictionary<string, object> dictionary = BaseIap.Deserialize(orderData);
					if (dictionary.ContainsKey("orderId"))
					{
						return dictionary["orderId"].ToString();
					}
				}
				return string.Empty;
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				return string.Empty;
			}
		}

		public static bool GetOrderState(string product, string token)
		{
			try
			{
				string orderData = GetOrderData(product, token);
				if (!string.IsNullOrEmpty(orderData))
				{
					Dictionary<string, object> dictionary = BaseIap.Deserialize(orderData);
					if (dictionary.ContainsKey("purchaseState") && Convert.ToInt32(dictionary["purchaseState"]) == 0)
					{
						return true;
					}
				}
				return false;
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				return false;
			}
		}
	}
}
