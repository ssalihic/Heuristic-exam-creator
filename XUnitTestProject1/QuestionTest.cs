using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGeneratorv1;
using TestGeneratorv1.Controllers;
using Xunit;

namespace XUnitTestProject1
{
    public class QuestionTest
    {
        public static IEnumerable<Question> questions;

        public QuestionTest()
        {
            InitContext();

        }
      private QuestionController controller;

        private TestGeneratorContext context;

        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder<TestGeneratorContext>()
                .UseInMemoryDatabase();
            var _context = new TestGeneratorContext(builder.Options);
            if (questions == null)
            {
                questions = Enumerable.Range(1, 10)
                    .Select(i => new Question
                    {
                        QuestionId = i,
                        QuestionText = $"Question{i}",
                        Area = new Area { AreaId = i + 1, AreaName = $"Sample{i}", YearOfStudy = new YearOfStudy { YearOfStudyId = i, YearOfStudyName = $"YOS{i}" } },
                        Points = i,
                        Answer = new Answer { },
                        Subject = new Subject { },
                        DifficultyLevel = new DifficultyLevel { },
                        Status = new Status { },
                        Visibility = new Visibility { },
                        QuestionType = new QuestionType { },
                        CreatedDate = new DateTime { },
                        ModifiedDate = new DateTime { },
                        Note = $"Note{i}"
                    });

                _context.Question.AddRange(questions);
            }
                int changed = _context.SaveChanges();
            context = _context;
            controller = new QuestionController(context);

        }


        [Fact]
        public void A1GetAllQuestion()
        {
            List<Question> yearOfStudies = ModelFromActionResult<List<Question>>((IActionResult)controller.Get());
            Assert.Equal(yearOfStudies.Count, 10);
        }
        [Fact]
        public void GetByIdQuestion()
        {
            Question Question = ModelFromActionResult<Question>((IActionResult)controller.Get(3));
            Assert.Equal(Question.QuestionId, 3);
        
        }
        [Fact]
        public void DeleteQuestion()
        {
            controller.Delete(5);
            IActionResult result = controller.Get(5);
            Assert.Equal(result.GetType(), typeof(NotFoundObjectResult));
        }

        [Fact]
        public void CreateQuestion()
        {
            var result = controller.Create(new Question
              {
                  QuestionId = 12,
                  QuestionText = $"Question{12}",
                  Area = new Area { AreaId = 12 , AreaName = $"Sample{12}", YearOfStudy = new YearOfStudy { YearOfStudyId = 12, YearOfStudyName = $"YOS{12}" } },
                  Points = 12,
                  Answer = new Answer { },
                  Subject = new Subject { SubjectName= "12" },
                  DifficultyLevel = new DifficultyLevel { },
                  Status = new Status { },
                  Visibility = new Visibility { },
                  QuestionType = new QuestionType { },
                  CreatedDate = new DateTime { },
                  ModifiedDate = new DateTime { },
                  Note = $"Note{12}"
              });
            Question Question = ModelFromActionResult<Question>((IActionResult)controller.Get(12));
            Assert.NotNull(Question.Subject);
            Assert.Equal(result.GetType(), typeof(CreatedAtActionResult));
        }

        [Fact]
        public void UpdateQuestion()
        {
            var result1 = ModelFromActionResult<Question>((IActionResult)controller.Get(2));
            Assert.Equal(result1.QuestionText, "Question2");
            controller.Update(2, new Question { QuestionText = $"Question{22}" });
            var result = ModelFromActionResult<Question>((IActionResult)controller.Get(2));
            Assert.Equal(result.QuestionText, "Question22");

        }

        public T ModelFromActionResult<T>(IActionResult actionResult)
        {
            object model;
            OkObjectResult partialViewResult = (OkObjectResult)actionResult;
            model = partialViewResult.Value;
            T typedModel = (T)model;
            return typedModel;
        }
    }
}
