using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreADOAPI.DataAPI
{
    public class ResponseMessage
    {
        public int Status { get; set; }
        public string Msg { get; set; }

        public ResponseMessage(int status, string msg)
        {
            Status = status;
            Msg = msg;
        }
    }
}