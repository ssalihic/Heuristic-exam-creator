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
    public class StatusTest
    {
        public static IEnumerable<Status> yearOfStudies;
        public StatusTest()
        {
                InitContext();
            controller = new StatusController(context);

        }
        private StatusController controller;

        private TestGeneratorContext context;

        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder<TestGeneratorContext>()
                .UseInMemoryDatabase();
            var _context = new TestGeneratorContext(builder.Options);
            if (yearOfStudies == null)
            {
                yearOfStudies = Enumerable.Range(1, 10)
                    .Select(i => new Status { StatusId = i, StatusName = $"Status{i}" });
                _context.Status.AddRange(yearOfStudies);
            }
            int changed = _context.SaveChanges();
            context = _context;
        }
        [Fact]
        public void A1GetAllStatus()
        {
            List<Status> yearOfStudies = ModelFromActionResult<List<Status>>((IActionResult)controller.Get());
            Assert.Equal(yearOfStudies.Count, 10);
        }
        [Fact]
        public void GetByIdStatus()
        {
            Status Status = ModelFromActionResult<Status>((IActionResult)controller.Get(3));
            Assert.Equal(Status.StatusId, 3);
        }
        [Fact]
        public void DeleteStatus()
        {
            controller.Delete(5);
            IActionResult result = controller.Get(5);
            Assert.Equal(result.GetType(), typeof(NotFoundObjectResult));
        }

        [Fact]
        public void CreateStatus()
        {
            var result = controller.Create(new Status { StatusId = 11, StatusName = $"Status{11}" });
            Assert.Equal(result.GetType(), typeof(CreatedAtActionResult));
        }

        [Fact]
        public void UpdateStatus()
        {
            var result1 = ModelFromActionResult<Status>((IActionResult)controller.Get(2));
            Assert.Equal(result1.StatusName, "Status2");
            controller.Update(2, new Status { StatusId = 22, StatusName = $"Status{22}" });
            var result = ModelFromActionResult<Status>((IActionResult)controller.Get(2));
            Assert.Equal(result.StatusName, "Status22");

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
