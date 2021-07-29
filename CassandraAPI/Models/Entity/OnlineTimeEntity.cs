using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CassandraAPI.Models;

namespace CassandraAPI.Models
{
    [Table("online_time", Schema = "public")]
    public class OnlineTimeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(true)]

        [Column("user_id")]
        public int userId { get; set; }

        [Column("time_online")]
        public int timeOnline { get; set; }

        [Column("updated_at")]
        public DateTime updatedAt { get; set; }

        [ForeignKey("userId")]
        public UserEntity userEntity { get; set; }
    }
}
