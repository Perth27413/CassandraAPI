using System;
using System.Collections.Generic;

namespace CassandraAPI.Models
{
    public class CarbonUserResponse
    {
        public UserEntity UserInfo { get; set; }
        public double carbon { get; set; }
    }
}