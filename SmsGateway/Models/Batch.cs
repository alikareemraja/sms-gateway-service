using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmsGateway.Models
{
    public class Batch
    {
        public Batch()
        {
            ID = Guid.NewGuid();
        }
        public Guid ID { get; set; }
    }
}