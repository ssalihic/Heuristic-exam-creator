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
    public class QuestionTypeTest
    {
        public static IEnumerable<QuestionType> yearOfStudies;

        public QuestionTypeTest()
        {
            InitContext();
            controller = new QuestionTypesController(context);

        }
        private QuestionTypesController controller;

        private TestGeneratorContext context;

        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder<TestGeneratorContext>()
                .UseInMemoryDatabase();
            var _context = new TestGeneratorContext(builder.Options);
            if (yearOfStudies == null)
            {
                yearOfStudies = Enumerable.Range(1, 10)
                    .Select(i => new QuestionType { QuestionTypeId = i, QuesstionTypeName = $"QuestionType{i}" });
                _context.QuestionType.AddRange(yearOfStudies);
            }
            int changed = _context.SaveChanges();
            context = _context;
        }
        [Fact]
        public void A1GetAllQuestionType()
        {
            List<QuestionType> yearOfStudies = ModelFromActionResult<List<QuestionType>>((IActionResult)controller.Get());
            Assert.Equal(yearOfStudies.Count, 10);
        }
        [Fact]
        public void GetByIdQuestionType()
        {
            QuestionType QuestionType = ModelFromActionResult<QuestionType>((IActionResult)controller.Get(3));
            Assert.Equal(QuestionType.QuestionTypeId, 3);
        }
        [Fact]
        public void DeleteQuestionType()
        {
            controller.Delete(5);
            IActionResult result = controller.Get(5);
            Assert.Equal(result.GetType(), typeof(NotFoundObjectResult));
        }

        [Fact]
        public void CreateQuestionType()
        {
            var result = controller.Create(new QuestionType { QuestionTypeId = 11, QuesstionTypeName = $"QuestionType{11}" });
            Assert.Equal(result.GetType(), typeof(CreatedAtActionResult));
        }

        [Fact]
        public void UpdateQuestionType()
        {
            var result1 = ModelFromActionResult<QuestionType>((IActionResult)controller.Get(2));
            Assert.Equal(result1.QuesstionTypeName, "QuestionType2");
            controller.Update(2, new QuestionType { QuestionTypeId = 22, QuesstionTypeName = $"QuestionType{22}" });
            var result = ModelFromActionResult<QuestionType>((IActionResult)controller.Get(2));
            Assert.Equal(result.QuesstionTypeName, "QuestionType22");

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
