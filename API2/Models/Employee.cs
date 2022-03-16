using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API2.Models
{
    [Table ("tb_m_employee")]
    public class Employee
    {
        [Key]
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime BirtDate { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
    }
    public enum Gender
    {
        Male,
        Female

    }
}
