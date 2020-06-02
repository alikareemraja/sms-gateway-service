using System;
using System.Collections.Generic;

namespace SmsGateway.DTOs.Response
{
    public class BatchResponse
    {
        public Guid BatchId { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}