using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace netDumbster.WebServer.SendMail
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new SmtpClient(IPAddress.Loopback.ToString(), 25);
            client.Send(args[0], args[1], args[2], args[3]);
        }
    }
}
