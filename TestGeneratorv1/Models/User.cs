using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGeneratorv1
{
    public class User : IdentityUser
    {
        public virtual ICollection<Subject> Subjects { get; set; }

        public string Name { get; set; }

        public virtual ICollection<UserSubjects> UserSubjects { get; set; }

    }


}
