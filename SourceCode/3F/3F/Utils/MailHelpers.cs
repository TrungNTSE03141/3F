using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace _3F.Utils
{
    public class MailHelpers
    {
        private const string senderEmailAddress = "FlashFoodAndFriends@gmail.com";
        private const string senderEmailPassword = "123@123s1";
        private readonly SmtpClient _smtp;
        private readonly MailMessage _mail;

        public MailHelpers()
        {
            _mail = new MailMessage();
            _smtp = new SmtpClient
            {
                Host = "smtp.gmail.com", 
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(senderEmailAddress, senderEmailPassword),
                Timeout = 30000,
            };
            _mail.From = new MailAddress(senderEmailAddress);
            _mail.IsBodyHtml = true;
        }

        public bool SendMailWelcome(string email, string firstName, string lastName)
        {
            try
            {
                _mail.To.Add(email);
                _mail.Subject = "3F Title Demo";
                _mail.Body = "Welcome " + firstName + " " + lastName + " to Flash Food & Friends";
                _smtp.Send(_mail);
            }
            catch (Exception ex)
            {
                Logging.Log(ex.ToString());
                return false;
            }
            return true;
        }

        public bool SendMail(string addressTo, string subject ,string content)
        {
            try {
                _mail.To.Add(addressTo);
                _mail.Subject = "[3F] - "+subject;
                _mail.Body = content;
                _smtp.Send(_mail);
                return true;
            }catch(Exception e)
            {
                Logging.Log(e.ToString());
                return false;
            }
        }


        public bool SendMailResetPassword(string addressTo, string newPassword)
        {
            string content = "Your new password is: " + newPassword;
            return SendMail(addressTo, "Reset Your Password", content);
        }
    }
}