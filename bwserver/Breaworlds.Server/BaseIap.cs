using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Breaworlds.Server
{
	public class BaseIap
	{
		private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

		public static string Serialize(Dictionary<string, object> data)
		{
			return Serializer.Serialize(data);
		}

		public static Dictionary<string, object> Deserialize(string data)
		{
			return Serializer.Deserialize<Dictionary<string, object>>(data);
		}
	}
}
