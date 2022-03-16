using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API2.ViewModel
{
    public class ChangeVM
    {
        public int Otp { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPass { get; set; }
    }
}
