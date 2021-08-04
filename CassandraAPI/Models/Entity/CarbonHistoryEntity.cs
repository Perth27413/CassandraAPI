using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CassandraAPI.Models;

namespace CassandraAPI.Models
{
    [Table("carbon_history", Schema = "public")]
    public class CarbonHistoryEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(true)]

        [Column("user_id")]
        public int userId { get; set; }

        [Column("carbon_amount")]
        public double carbonAmount { get; set; }

        [Column("distance")]
        public double distanceTotal { get; set; }

        [Column("created_at")]
        public DateTime createdAt { get; set; }

        [Column("round_time")]
        public double tripTime { get; set; }
        [JsonIgnore]
        [ForeignKey("userId")]
        public UserEntity userEntity { get; set; }
    }
}
