using System;

namespace SmsGateway.Models
{
    public class Message
    {
        public Message()
        {
            ID = Guid.NewGuid();
        }
        public Guid ID { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
        public string To { get; set; }


    }
}