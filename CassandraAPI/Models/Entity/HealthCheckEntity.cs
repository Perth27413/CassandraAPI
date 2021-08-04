using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CassandraAPI.Models
{
    [Table("healthcheck", Schema = "public")]
    public class HealthCheckEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(true)]

        [Column("message")]
        public string message { get; set; }

    }
}
