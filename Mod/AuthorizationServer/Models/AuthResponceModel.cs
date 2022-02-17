using AuthorizationServer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Models
{
    public class AuthResponceModel
    {
        public AuthResponceModel(StatusCodes statusCode, string msg = "")
        {
            status = statusCode.ToString();
            message = msg;
        }
        public string status { get; set; }
        public string message { get; set; }
    }
}
