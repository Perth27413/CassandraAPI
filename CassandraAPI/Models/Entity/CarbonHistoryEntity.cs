using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
        public int carbonAmount { get; set; }

        [Column("distance")]
        public int distanceTotal { get; set; }

        [Column("created_at")]
        public DateTime createdAt { get; set; }

        [ForeignKey("userId")]
        public UserEntity userEntity { get; set; }
    }
}
