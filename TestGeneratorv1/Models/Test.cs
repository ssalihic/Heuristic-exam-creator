using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGeneratorv1
{
    public class Test
    {

        public Test()
        {
            AreasSelected = new List<Area>();
        }

        public int TestId { get; set; }

        public Subject Subject { get; set; }

        public List<Question> Questions { get; set; }

        public virtual ICollection<Area> AreasSelected { get; set; }

        public virtual ICollection<TestQuestion> TestQuestions { get; set; }

        public virtual ICollection<TestArea> TestArea { get; set; }

        public YearOfStudy YearOfStudy { get; set; } 

        public DifficultyLevel MaxDifficultyLevel { get; set; }

        public double TotalDifficultyLevel { get; set; }

        public int NumberOfQuestions { get; set; }

        public DateTime CreatedDate { get; set; } // Nema ModifiedDate jer kada uredi postojeci test mi samo stvaramo novi

        public string Date { get; set; } 

        public User Creator { get; set; }

        public string TestGroup { get; set; } // Grupa na testu, npr. A; B..

        public string Group { get; set; } // Misli se na odijeljenje - promijeniti ako ima bolji naziv
        
        public string Note { get; set; } //Note - Comment na test

    }
}
