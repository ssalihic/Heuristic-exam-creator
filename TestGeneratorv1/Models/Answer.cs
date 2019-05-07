using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGeneratorv1
{
    public class Answer
    {

        public Answer()
        {

        }
        public int AnswerId { get; set; }

        public string AnswerText { get; set; }

        public string AnswerPicture { get; set; }

        public string Note { get; set; } //Note - Comment na odgovor

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

    }
}
