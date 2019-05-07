using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestGeneratorv1
{
    public class TestQuestion
    {

        public TestQuestion()
        {

        }

        public TestQuestion(Test test, Question question)
        {
            Question = question;
        }

        public int TestQuestionId { get; set; }


        public Question Question { get; set; }



    }
}
