﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HotelsApp.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
        public User()
        {
        }
    }
}