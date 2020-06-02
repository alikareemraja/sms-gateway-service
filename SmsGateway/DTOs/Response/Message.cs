using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmsGateway.DTOs.Response
{
    public class Message
    {
        public Guid MessageId { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public string Content { get; set; }
        public string To { get; set; }
    }
}