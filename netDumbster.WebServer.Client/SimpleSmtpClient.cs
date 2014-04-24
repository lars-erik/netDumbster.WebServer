using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace netDumbster.WebServer.Client
{
    public class SimpleSmtpClient
    {
        private readonly Uri baseUri;
        private readonly HttpClient client = new HttpClient();
        private readonly TimeSpan timeout = TimeSpan.FromSeconds(10);

        public SimpleSmtpClient(string baseUri)
        {
            this.baseUri = new Uri(baseUri);
        }

        public SmtpMessage[] All()
        {
            SmtpMessage[] messages = null;
            client.GetStringAsync(new Uri(baseUri, "mail"))
                .ContinueWith(r => messages = JsonConvert.DeserializeObject<SmtpMessage[]>(r.Result))
                .Wait(timeout);
            return messages;
        }

        public SmtpMessage Item(int index)
        {
            SmtpMessage message = null;
            client.GetStringAsync(new Uri(baseUri, "mail/" + index))
                .ContinueWith(r => message = JsonConvert.DeserializeObject<SmtpMessage>(r.Result))
                .Wait(timeout);
            return message;
        }

        public void Clear()
        {
            client.DeleteAsync(new Uri(baseUri, "mail"))
                .Wait(timeout);
        }

        public void Start()
        {
            client.PostAsync(new Uri(baseUri, "start"), new StringContent(""))
                .Wait(timeout);
        }

        public void Stop()
        {
            client.PostAsync(new Uri(baseUri, "stop"), new StringContent(""))
                .Wait(timeout);
        }
    }

    public class SmtpMessage
    {
        public string Data { get; set; }
        public EmailAddress FromAddress { get; set; }
        public string[] Headers { get; set; }
        public string Importance { get; set; }
        public IPAddress LocalIPAddress { get; set; }
        public int LocalPort { get; set; }
        public MessagePart[] MessageParts { get; set; }
        public string Priority { get; set; }
        public IPAddress RemoteIPAddress { get; set; }
        public int RemotePort { get; set; }
        public EmailAddress[] ToAddresses { get; set; }
        public string XPriority { get; set; }
    }

    public class MessagePart
    {
        public string BodyData { get; set; }
        public string HeaderData { get; set; }
        public string[] Headers { get; set; }
    }

    public class EmailAddress
    {
        public string Address { get; set; }
        public string Domain { get; set; }
        public string Username { get; set; }
    }

    public class IPAddress
    {
        public long m_Address { get; set; }
        public int m_Family { get; set; }
        public int[] m_Numbers { get; set; }
        public int m_ScopeId { get; set; }
    }
}
