using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CassandraAPI.Models
{
    [Table("vehicle_brand", Schema = "public")]
    public class BrandEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(true)]

        [Column("brand_id")]
        public int brandId { get; set; }

        [Column("brand")]
        public string brand { get; set; }

    }
}
