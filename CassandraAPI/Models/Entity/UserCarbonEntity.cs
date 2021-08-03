using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CassandraAPI.Models
{
    [Table("user_carbon", Schema = "public")]
    public class UserCarbonEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(true)]

        [Column("user_id")]
        public int userId { get; set; }

        [Column("carbon")]
        public double carbon { get; set; }
        
        [ForeignKey("userId")]
        public UserEntity userEntity { get; set; }

    }
}
