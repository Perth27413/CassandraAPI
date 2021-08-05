using System;
using System.Collections.Generic;

namespace CassandraAPI.Models
{
    public class BrandResponse
    {
        public int modelId { get; set; }
        public string model { get; set; }
        public List<TypeResponse> typeResponse { get; set; }
    }
}