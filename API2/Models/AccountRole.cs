using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API2.Models
{
    public class AccountRole
    {
        [Key]
        public int Id { get; set; }
        public string AccountNIK { get; set; }
        public int RoleId { get; set; }

//relasi
public virtual Role Role { get; set; }
        public virtual Account Account { get; set; }
    }
}
