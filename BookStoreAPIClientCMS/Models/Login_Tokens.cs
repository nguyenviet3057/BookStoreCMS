﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreAPIClientCMS.Models
{
    public class Login_Tokens
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
    }
}