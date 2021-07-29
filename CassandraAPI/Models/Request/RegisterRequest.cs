using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CassandraAPI.Models
{
    public class RegisterRequest
    {
        public string userName { get; set; }
        public string password { get; set; }
        public int vehicle { get; set; }

        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public int year { get; set; }
    }
}
