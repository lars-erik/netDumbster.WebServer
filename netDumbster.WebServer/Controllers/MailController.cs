using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using netDumbster.smtp;

namespace netDumbster.WebServer.Controllers
{
    public class MailController : ApiController
    {
        private static readonly object LockObj = new object();
        private static SimpleSmtpServer server;
        private static bool isRunning = false;

        [Route("~/start")]
        [HttpPost]
        public void Start()
        {
            lock(LockObj)
            {
                if (isRunning)
                    return;
                server = SimpleSmtpServer.Start(25);
                isRunning = true;
            }
        }

        [Route("~/stop")]
        [HttpPost]
        public void Stop()
        {
            lock(LockObj)
            {
                if (!isRunning)
                    return;
                server.Stop();
                server = null;
                isRunning = false;
            }
        }

        // GET api/<controller>
        public SmtpMessage[] Get()
        {
            return server.ReceivedEmail;
        }

        // GET api/<controller>/5
        public SmtpMessage Get(int id)
        {
            return server.ReceivedEmail[id];
        }

        // DELETE api/<controller>5
        public void Delete()
        {
            server.ClearReceivedEmail();
        }
    }
}