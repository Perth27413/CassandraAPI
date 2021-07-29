using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CassandraAPI.Models
{
    [Table("carbon_per_day", Schema = "public")]
    public class CarbonPerDayEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(true)]

        [Column("time")]
        public DateTime time { get; set; }

        [Column("carbon_total")]
        public int carbonPerDay { get; set; }

        [Column("distance_total")]
        public double distanceTotal { get; set; }
        [Column("user_id")]
        public int userId { get; set; }

        [ForeignKey("userId")]
        public UserEntity userEntity { get; set; }
    }
}
