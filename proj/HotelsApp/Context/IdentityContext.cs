using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelsApp.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HotelsApp.Context
{
    public class IdentityContext : IdentityDbContext<User>
    {
        public IdentityContext() : base("IdentityDb") { }

        public static IdentityContext Create()
        {
            return new IdentityContext();
        }
    }
}