using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API2.Models
{
    [Table("tb_tr_education")]
    public class Education
    {
        [Key]
        public int Id { get; set; }
        public string Degree { get; set; }
        public string GPA { get; set; }
        [JsonIgnore]
        //relation
        public int University_id { get; set; }
        public virtual ICollection<Profiling> Profilings { get; set; }
        public virtual University University { get; set; }

    }
}
