using System;
using System.Collections.Generic;

namespace CassandraAPI.Models
{
    public class UserInfoResponse
    {
        public int userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string position { get; set; }
        public string profilePic { get; set; }
        public int carbon { get; set; }
    }
}