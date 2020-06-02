using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmsGateway.Common
{
    public static class Utils
    {
        public static string SmsGatewayServiceUrl => System.Configuration.ConfigurationManager.AppSettings["SmsGatewayService"];

    }
}