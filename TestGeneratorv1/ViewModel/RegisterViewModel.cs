using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGeneratorv1.ViewModel
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public List<Subject> Subjects { get; set; }
        public string Name { get; set; }
        public Boolean AppAdmin { get; set; }

    }
}
