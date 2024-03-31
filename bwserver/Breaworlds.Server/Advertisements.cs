using System.IO;
using System.Text;

namespace Breaworlds.Server
{
	public class Advertisements
	{
		public static int SkipQuest;

		public static int RewardBox1;

		public static int RewardBox2;

		public static int RewardBox3;

		public static void Write(BinaryWriter writer, string name)
		{
			switch (name)
			{
			case "SkipQuest":
				writer.Write(Encoding.UTF8.GetBytes("ca-app-pub-5281869300652392~3997763293\0"));
				writer.Write(Encoding.UTF8.GetBytes("ca-app-pub-5281869300652392/8079765800\0"));
				SkipQuest++;
				break;
			case "RewardBox1":
				writer.Write(Encoding.UTF8.GetBytes("ca-app-pub-5281869300652392~3997763293\0"));
				writer.Write(Encoding.UTF8.GetBytes("ca-app-pub-5281869300652392/4276747004\0"));
				RewardBox1++;
				break;
			case "RewardBox2":
				writer.Write(Encoding.UTF8.GetBytes("ca-app-pub-5281869300652392~3997763293\0"));
				writer.Write(Encoding.UTF8.GetBytes("ca-app-pub-5281869300652392/2698628375\0"));
				RewardBox2++;
				break;
			case "RewardBox3":
				writer.Write(Encoding.UTF8.GetBytes("ca-app-pub-5281869300652392~3997763293\0"));
				writer.Write(Encoding.UTF8.GetBytes("ca-app-pub-5281869300652392/3729457255\0"));
				RewardBox3++;
				break;
			default:
				writer.Write(Encoding.UTF8.GetBytes("\0"));
				writer.Write(Encoding.UTF8.GetBytes("\0"));
				break;
			}
		}
	}
}
