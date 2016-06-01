using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace AliceApi.Logic
{
    public class Postal
    {
        public void SendMail(IdentityMessage message)
        {
            #region formatter
            string text = string.Format("Please click on this link to {0}: {1}", message.Subject, message.Body);
            string html = "Please confirm your account by clicking this link: <a href=\"" + message.Body + "\">link</a><br/>";

            html += HttpUtility.HtmlEncode(@"Or click on the copy the following link on the browser:" + message.Body);
            #endregion

            var destination = message.Destination;
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("jonforst@cableone.net");
            msg.To.Add(new MailAddress(message.Destination));
            msg.Subject = message.Subject;
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            //SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
            //System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("jonforst2000@gmail.com", "draco2199");
            SmtpClient smtpClient = new SmtpClient("mail.cableone.net", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("jforst@cableone.net", "Admin$#1");
            smtpClient.Credentials = credentials;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            smtpClient.Send(msg);
        }
    }
}
