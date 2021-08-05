using System;
using System.Collections.Generic;

namespace CassandraAPI.Models
{
    public class TypeResponse
    {
        public int typeId { get; set; }
        public string type { get; set; }
        public List<ModelResponse> modelResponse { get; set; }
    }
}