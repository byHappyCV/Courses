using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace testing
{
    class Users
    {
        public string Name { get; set; }
    }
    class Program
    {
        static void Main()
        {
            Entities1 dbEntities = new Entities1();
            var f = dbEntities.Friendships.ToList();
            Console.ReadKey();
        }
    }
}
