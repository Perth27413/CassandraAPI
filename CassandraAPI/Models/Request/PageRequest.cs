using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CassandraAPI.Models
{
    public class PageRequest
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}
