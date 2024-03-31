using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Breaworlds.Server
{
	public class PayPalIap
	{
		public static readonly bool Disabled = false;

		private static readonly string Username = "u";

		private static readonly string Password = "p";

		private static readonly string Credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(Username + ":" + Password));

		public static string CreateOrder(string price)
		{
			try
			{
				using WebClient webClient = new WebClient();
				webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
				webClient.Headers[HttpRequestHeader.Authorization] = $"Basic {Credentials}";
				Dictionary<string, object> dictionary = new Dictionary<string, object>
				{
					{ "intent", "CAPTURE" },
					{ "note_to_payer", "Purchases can not be refunded." }
				};
				dictionary.Add("application_context", new Dictionary<string, object>
				{
					{ "return_url", "http://127.0.0.1:20001/approved?user=10" },
					{ "cancel_url", "http://127.0.0.1:20001/canceled?user=10" }
				});
				dictionary.Add("purchase_units", new object[1]
				{
					new Dictionary<string, object> { 
					{
						"amount",
						new Dictionary<string, object>
						{
							{ "currency_code", "EUR" },
							{ "value", price }
						}
					} }
				});
				string data = webClient.UploadString("https://api.sandbox.paypal.com/v2/checkout/orders", BaseIap.Serialize(dictionary));
				Dictionary<string, object> dictionary2 = BaseIap.Deserialize(data);
				if (dictionary2.ContainsKey("id"))
				{
					return dictionary2["id"].ToString();
				}
				return string.Empty;
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				return string.Empty;
			}
		}

		private static string CaptureOrder(string id)
		{
			try
			{
				using WebClient webClient = new WebClient();
				webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
				webClient.Headers[HttpRequestHeader.Authorization] = $"Basic {Credentials}";
				string data = webClient.UploadString($"https://api.sandbox.paypal.com/v2/checkout/orders/{id}/capture", "{}");
				Dictionary<string, object> dictionary = BaseIap.Deserialize(data);
				if (dictionary.ContainsKey("status"))
				{
					return dictionary["status"].ToString();
				}
				return string.Empty;
			}
			catch (Exception exception)
			{
				Terminal.Exception(exception);
				return string.Empty;
			}
		}
	}
}
