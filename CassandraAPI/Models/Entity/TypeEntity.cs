using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CassandraAPI.Models
{
    [Table("vehicle_type", Schema = "public")]
    public class TypeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(true)]

        [Column("id_type")]
        public int typeId { get; set; }

        [Column("type")]
        public string type { get; set; }

    }
}
