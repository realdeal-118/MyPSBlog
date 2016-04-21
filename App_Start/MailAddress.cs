using System.Net.Mail;

namespace MyPSBlog
{
    internal class MailAddress : System.Net.Mail.MailAddress
    {
        public MailAddress(string address) : base(address)
        {
        }
    }
}