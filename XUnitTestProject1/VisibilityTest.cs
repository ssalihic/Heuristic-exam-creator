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
    public class VisibilityTest
    {
        public static IEnumerable<Visibility> yearOfStudies;

        public VisibilityTest()
        {
            InitContext();
            controller = new VisibilityController(context);

        }
        private VisibilityController controller;

        private TestGeneratorContext context;

        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder<TestGeneratorContext>()
                .UseInMemoryDatabase();
            var _context = new TestGeneratorContext(builder.Options);
            if (yearOfStudies == null)
            {
                yearOfStudies = Enumerable.Range(1, 10)
                   .Select(i => new Visibility { VisibilityId = i, VisibilityName = $"Visibility{i}" });
                _context.Visibility.AddRange(yearOfStudies);
            }
            int changed = _context.SaveChanges();
            context = _context;
        }
        [Fact]
        public void A1GetAllVisibilities()
        {
            List<Visibility> yearOfStudies = ModelFromActionResult<List<Visibility>>((IActionResult)controller.Get());
            Assert.Equal(yearOfStudies.Count, 10);
        }
        [Fact]
        public void GetByIdVisibility()
        {
            Visibility Visibility = ModelFromActionResult<Visibility>((IActionResult)controller.Get(3));
            Assert.Equal(Visibility.VisibilityId, 3);
        }
        [Fact]
        public void DeleteVisibility()
        {
            controller.Delete(5);
            IActionResult result = controller.Get(5);
            Assert.Equal(result.GetType(), typeof(NotFoundObjectResult));
        }

        [Fact]
        public void CreateVisibility()
        {
            var result = controller.Create(new Visibility { VisibilityId = 11, VisibilityName = $"Visibility{11}" });
            Assert.Equal(result.GetType(), typeof(CreatedAtActionResult));
        }

        [Fact]
        public void UpdateVisibility()
        {
            var result1 = ModelFromActionResult<Visibility>((IActionResult)controller.Get(2));
            Assert.Equal(result1.VisibilityName, "Visibility2");
            controller.Update(2, new Visibility { VisibilityId = 22, VisibilityName = $"Visibility{22}" });
            var result = ModelFromActionResult<Visibility>((IActionResult)controller.Get(2));
            Assert.Equal(result.VisibilityName, "Visibility22");

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
