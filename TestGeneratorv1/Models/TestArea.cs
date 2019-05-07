using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGeneratorv1
{
    public class TestArea
    {
        public TestArea()
        {

        }

        public TestArea(Test test, Area area)
        {
            Area = area;
        }

        public int TestAreaId { get; set; }


        public Area Area { get; set; }


    }
}
