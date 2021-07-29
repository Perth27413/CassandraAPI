using System;
using System.Collections.Generic;

namespace CassandraAPI.Models
{
    public class CarbonPerHourResponse
    {
        public DateTime dateTime { get; set; }
        public double carbon { get; set; }
    }
}