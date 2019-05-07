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
    public class YearOfStudyTest
    {
        public static IEnumerable<YearOfStudy> yearOfStudies;

        public YearOfStudyTest()
        {
            InitContext();
            controller = new YearOfStudyController(context);

        }
        private YearOfStudyController controller;

        private TestGeneratorContext context;

        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder<TestGeneratorContext>()
                .UseInMemoryDatabase();
            var _context = new TestGeneratorContext(builder.Options);
            if (yearOfStudies == null)
            {
                yearOfStudies = Enumerable.Range(1, 10)
                   .Select(i => new YearOfStudy { YearOfStudyId = i, YearOfStudyName = $"YOS{i}" });
                _context.YearOfStudy.AddRange(yearOfStudies);
            }
            int changed = _context.SaveChanges();
            context = _context;
        }
        [Fact]
        public void A1GetAllYearOfStudes()
        {
            List<YearOfStudy> yearOfStudies = ModelFromActionResult<List<YearOfStudy>>((IActionResult)controller.Get());
            Assert.Equal(yearOfStudies.Count, 10);
        }
        [Fact]
        public void GetByIdYearOfStudy()
        {
            YearOfStudy yearOfStudy = ModelFromActionResult<YearOfStudy>((IActionResult)controller.Get(3));
            Assert.Equal(yearOfStudy.YearOfStudyId, 3);
        }
        [Fact]
        public void DeleteYearOfStudy()
        {
            controller.Delete(5);
            IActionResult result = controller.Get(5);
            Assert.Equal(result.GetType(), typeof(NotFoundObjectResult));
        }

        [Fact]
        public void CreateYearOfStudy()
        {
            var result = controller.Create( new YearOfStudy { YearOfStudyId = 11, YearOfStudyName = $"YOS{11}" });
            Assert.Equal(result.GetType(), typeof(CreatedAtActionResult));
        }

        [Fact]
        public void UpdateYearOfStudy()
        {
            var result1 = ModelFromActionResult<YearOfStudy>((IActionResult)controller.Get(2));
            Assert.Equal(result1.YearOfStudyName, "YOS2");
            controller.Update(2, new YearOfStudy { YearOfStudyId = 22, YearOfStudyName = $"YOS{22}" } );
            var result = ModelFromActionResult<YearOfStudy>((IActionResult)controller.Get(2));
            Assert.Equal(result.YearOfStudyName, "YOS22");

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
