using System.Net;
using System.Net.Mail;

namespace Company_PL.helpers
{
    public class EmailSettings
    {

        public static bool SendEmail(Email email)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("rahma.abulfatouh1@gmail.com", "xpgeuypwyxpqderh");//sender
                                                                                                              //xpgeuypwyxpqderh
                client.Send("rahma.abulfatouh1@gmail.com", email.To, email.Subject, email.Body);


                return true;
            }
            catch (Exception e)
            {
                return false;
            }
                }
    }
}
