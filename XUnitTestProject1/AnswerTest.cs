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
    public class AnswerTest
    {
        public static IEnumerable<Answer> answers;

        public AnswerTest()
        {
            InitContext();
            controller = new AnswerController(context);

        }
        private AnswerController controller;

        private TestGeneratorContext context;

        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder<TestGeneratorContext>()
                .UseInMemoryDatabase();
            var _context = new TestGeneratorContext(builder.Options);
            if (answers == null)
            {
                answers = Enumerable.Range(1, 10)
                   .Select(i => new Answer { AnswerId = i, AnswerText = $"Answer{i}", AnswerPicture = $"Picture{i}" });
                _context.Answer.AddRange(answers);
            }
            int changed = _context.SaveChanges();
            context = _context;
        }
        [Fact]
        public void GetAllAnswer()
        {
            List<Answer> yearOfStudies = ModelFromActionResult<List<Answer>>((IActionResult)controller.Get());
            Assert.Equal(yearOfStudies.Count, 10);
        }
        [Fact]
        public void GetByIdAnswer()
        {
            Answer Answer = ModelFromActionResult<Answer>((IActionResult)controller.Get(3));
            Assert.Equal(Answer.AnswerId, 3);
        }
        [Fact]
        public void DeleteAnswer()
        {
            controller.Delete(5);
            IActionResult result = controller.Get(5);
            Assert.Equal(result.GetType(), typeof(NotFoundObjectResult));
        }

        [Fact]
        public void CreateAnswer()
        {
            var result = controller.Create(new Answer { AnswerId = 11, AnswerText = $"Answer11", AnswerPicture = $"Picture11" });
            Assert.Equal(result.GetType(), typeof(CreatedAtActionResult));
        }

        [Fact]
        public void UpdateAnswer()
        {
            var result1 = ModelFromActionResult<Answer>((IActionResult)controller.Get(2));
            Assert.Equal(result1.AnswerText, "Answer2");
            controller.Update(2, new Answer { AnswerId = 22, AnswerText = $"Answer22", AnswerPicture = $"Picture22" });
            var result = ModelFromActionResult<Answer>((IActionResult)controller.Get(2));
            Assert.Equal(result.AnswerText, "Answer22");

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
