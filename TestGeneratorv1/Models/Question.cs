using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGeneratorv1
{
    public class Question
    {

        public Question()
        {

        }

        public int QuestionId { get; set; }

        public string QuestionText { get; set; }

        public string QuestionImage { get; set; }

        public double Points { get; set; }

        public Answer Answer { get; set; }

        public Subject Subject { get; set; }

        public Area Area { get; set; }

        public DifficultyLevel DifficultyLevel { get; set; }

        public Status Status { get; set; }

        public Visibility Visibility { get; set; }

        public QuestionType QuestionType { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public User Creator { get; set; }

        public string Note { get; set; } //Note - Comment na pitanje

    }
}
