using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGeneratorv1
{
    public class UserSubjects
    {

        public UserSubjects()
        {

        }

        public UserSubjects(Subject subject)
        {
            Subject = subject;
        }

        public int UserSubjectsId { get; set; }


        public Subject Subject { get; set; }
    }
}
