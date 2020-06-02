using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmsGateway.Models
{
    public class BatchMessage
    {
        public BatchMessage()
        {
            ID = Guid.NewGuid();
        }
        public Guid ID { get; set; }
        public Batch Batch { get; set; }
        public Guid BatchId { get; set; }
        public Message Message { get; set; }
        public Guid MessageId { get; set; }
        public int Status { get; set; }
    }
}