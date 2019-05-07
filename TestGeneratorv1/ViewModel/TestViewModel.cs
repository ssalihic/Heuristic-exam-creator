using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGeneratorv1.ViewModel
{
    public class TestViewModel
    {

        public Subject Subject { get; set; }

        public List<Question> Questions { get; set; }

        public List<Area> AreasSelected { get; set; }

        public List<KeyValuePair<QuestionType, int>> Count { get; set; }

        public YearOfStudy YearOfStudy { get; set; }

        public DifficultyLevel MaxDifficultyLevel { get; set; }

        public User Creator { get; set; }

        public double TotalDifficultyLevel { get; set; }

        public int NumberOfQuestions { get; set; }

        public string Date { get; set; }

        public string TestGroup { get; set; } // Grupa na testu, npr. A; B..

        public string Group { get; set; } // Misli se na odijeljenje - promijeniti ako ima bolji naziv

        public string Note { get; set; } //Note - Comment na test
    }
  
}
