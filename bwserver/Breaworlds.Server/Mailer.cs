using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Breaworlds.Server
{
	internal class Mailer
	{
		public static async void SendRecoveryEmail(string email, string username, string password)
		{
			try
			{
				await Task.Delay(100);
				using MemoryStream stream = new MemoryStream();
				XmlWriterSettings settings = new XmlWriterSettings
				{
					Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false),
					OmitXmlDeclaration = true
				};
				using (XmlWriter writer = XmlWriter.Create(stream, settings))
				{
					writer.WriteStartElement("div");
					writer.WriteStartElement("div");
					writer.WriteAttributeString("style", "background-color: #f0f0f0; font-family: verdana; font-size: 12px; color: #555555; max-width: 320px; padding: 20px; margin: 10px 0px;");
					writer.WriteElementString("h4", $"Hello {username},");
					writer.WriteElementString("p", "Someone has requested us to remind you of your login token, if this was not you, just ignore this email.");
					writer.WriteElementString("br", string.Empty);
					writer.WriteElementString("p", "Your login token:");
					writer.WriteStartElement("p");
					writer.WriteAttributeString("style", "border: 1px solid #ddd; background-color: white; padding: 5px;");
					writer.WriteString(password);
					writer.WriteEndElement();
					writer.WriteElementString("p", "It's recommended to change your login token once you access your account.");
					writer.WriteElementString("br", string.Empty);
					writer.WriteElementString("p", "Sincerely, Breaworlds");
					writer.WriteEndElement();
					writer.WriteEndElement();
				}
				using SmtpClient client = new SmtpClient();
				string from = "Breaworlds Support support@breaworldsgame.com";
				client.Host = "smtp.gmail.com";
				client.Port = 587;
				client.DeliveryMethod = SmtpDeliveryMethod.Network;
				client.UseDefaultCredentials = false;
				client.EnableSsl = true;
				client.Credentials = new NetworkCredential
				{
					UserName = "gamebreaworlds@gmail.com",
					Password = "myT#n_8p2^a=sA="
				};
				using MailMessage message = new MailMessage(from, email, "Account recovery", Encoding.UTF8.GetString(stream.ToArray()));
				message.IsBodyHtml = true;
				client.Send(message);
			}
			catch (Exception ex)
			{
				Exception exception = ex;
				Terminal.Exception(exception);
			}
		}
	}
}
