using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CassandraAPI.Models
{
    [Table("vehicle_model", Schema = "public")]
    public class ModelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(true)]

        [Column("model_id")]
        public int modelId { get; set; }

        [Column("model")]
        public string model { get; set; }

    }
}
