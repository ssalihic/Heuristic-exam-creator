using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGeneratorv1
{
    public class Subject
    {

        public Subject()
        {

        }

        public int SubjectId { get; set; }

        public string SubjectName { get; set; }

        public virtual ICollection<Area> Areas { get; set; }

    }
}
