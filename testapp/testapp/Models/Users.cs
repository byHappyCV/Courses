using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testapp.Models
{
    public class Users
    {
        public Users()
        {
            this.Reservations = new HashSet<Reservations>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public virtual ICollection<Reservations> Reservations { get; set; }
    }
}
