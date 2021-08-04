using System;
using System.Collections.Generic;

namespace CassandraAPI.Models
{
    public class HistoryResponse
    {
        public double carbon { get; set; }
        public double distance { get; set; }
        public double time { get; set; }
        public DateTime createAt { get; set; }
    }
}