using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CassandraAPI.Models
{
    [Table("carbon_ranking", Schema = "public")]
    public class CarbonRankingEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(true)]

        [Column("user_id")]
        public int userId { get; set; }

        [Column("carbon_amount")]
        public int carbonAmount { get; set; }

        [Column("distance")]
        public int distanceTotal { get; set; }

        [Column("updated_at")]
        public DateTime updatedAt { get; set; }
        [JsonIgnore]
        [ForeignKey("userId")]
        public UserEntity userEntity { get; set; }
    }
}
