using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using netDumbster.WebServer.Client;
using NUnit.Framework;

namespace netDumbster.WebServer.Tests
{
    [TestFixture]
    public class ClientIntegrationTests
    {
        [Test]
        public void Start_Send_Get_Delete_Stop()
        {
            var client = new SimpleSmtpClient("http://localhost:64055");
            client.Start();

            var smtpClient = new SmtpClient("localhost", 25);
            smtpClient.Send("lea@markedspartner.no", "lars-erik@markedspartner.no", "test", "Here's\r\na test");

            var messages = client.All();
            Assert.IsNotNull(messages);
            Assert.AreEqual(1, messages.Length);

            var message = client.Item(0);
            Assert.IsInstanceOf<SmtpMessage>(message);

            client.Clear();
            Assert.AreEqual(0, client.All().Length);

            client.Stop();
        }
    }
}
