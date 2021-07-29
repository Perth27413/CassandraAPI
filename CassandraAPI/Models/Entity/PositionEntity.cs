using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CassandraAPI.Models
{
    [Table("Position", Schema = "public")]
    public class PositionEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(true)]

        [Column("position_id")]
        public int position_id { get; set; }

        [Column("position_name")]
        public string position_name { get; set; }

    }
}
