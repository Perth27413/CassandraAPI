using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CassandraAPI.Models
{
    [Table("vehicle", Schema = "public")]
    public class VehicleEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(true)]

        [Column("vehicle_id")]
        public int vehicleId { get; set; }

        [Column("brand_id")]
        public int brandId { get; set; }

        [Column("type_id")]
        public int typeId { get; set; }

        [Column("model_id")]
        public int modelId { get; set; }

        [Column("co2")]
        public double co2 { get; set; }
        
        [ForeignKey("brandId")]
        public BrandEntity brandEntity { get; set; }
        
        [ForeignKey("typeId")]
        public TypeEntity typeEntity { get; set; }
        
        [ForeignKey("modelId")]
        public ModelEntity modelEntity { get; set; }

    }
}
