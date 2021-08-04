using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CassandraAPI.Models
{
    [Table("User", Schema = "public")]
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(true)]

        [Column("user_id")]
        public int userId { get; set; }

        [Column("first_name")]
        public string firstName { get; set; }

        [Column("last_name")]
        public string lastName { get; set; }

        [Column("user_name")]
        public string userName { get; set; }

        [Column("password")]
        public string password { get; set; }

        [Column("position")]
        public int position { get; set; }

        [Column("vehicle")]
        public int vehicle { get; set; }

        [Column("vehicle_year")]
        public int vehicleYear { get; set; }

        [Column("profile_pic")]
        public string profilePic { get; set; }
        
        [ForeignKey("position")]
        public PositionEntity positionEntity { get; set; }
        
        [ForeignKey("vehicle")]
        public VehicleEntity vehicleEntity { get; set; }
    }
    
}
