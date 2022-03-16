using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API2.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        //relasi 
        public virtual ICollection<AccountRole>AccountRoles { get; set; }

    }
}
