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
    public class AreaTest
    {
        public static IEnumerable<Area> areas;

        public AreaTest()
        {
            InitContext();
            controller = new AreasController(context);

        }
        private AreasController controller;

        private TestGeneratorContext context;

        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder<TestGeneratorContext>()
                .UseInMemoryDatabase();
            var _context = new TestGeneratorContext(builder.Options);
            if (areas == null)
            {
                areas = Enumerable.Range(1, 10)
                    .Select(i => new Area { AreaId = i, AreaName = $"Sample{i}", YearOfStudy = new YearOfStudy { YearOfStudyId = i, YearOfStudyName = $"YOS{i}" } });
                _context.Area.AddRange(areas);
            }
            int changed = _context.SaveChanges();
            context = _context;
        }
        [Fact]
        public void A1GetAllAreasTest()
        {
            List<Area> areas = ModelFromActionResult<List<Area>>((IActionResult)controller.Get());
            Assert.Equal(areas.Count, 10);
        }
        [Fact]
        public void GetByIdAreaTest()
        {
            Area areas = ModelFromActionResult<Area>((IActionResult)controller.Get(3));
            Assert.Equal(areas.AreaId, 3);
        }
        [Fact]
        public void DeleteAreaTest()
        {
            controller.Delete(5);
            IActionResult result = controller.Get(5);
            Assert.Equal(result.GetType(), typeof(NotFoundObjectResult));
        }

        [Fact]
        public void CreateArea()
        {
            var i = controller.Create(new Area { AreaId = 11, AreaName = $"Sample{11}", YearOfStudy = new YearOfStudy { YearOfStudyId = 11, YearOfStudyName = $"YOS{11}" } });
            Assert.Equal(i.GetType(), typeof(CreatedAtActionResult));
        }

        [Fact]
        public void UpdateArea()
        {
            var result1 = ModelFromActionResult<Area>((IActionResult)controller.Get(2));
            Assert.Equal(result1.AreaName, "Sample2");
            controller.Update(2, new Area { AreaName = $"Sample{22}", YearOfStudy = new YearOfStudy { YearOfStudyId = 22, YearOfStudyName = $"YOS{22}" } });
            var result = ModelFromActionResult<Area>((IActionResult)controller.Get(2));
            Assert.Equal(result.AreaName, "Sample22");

        }

        [Fact]
        public void AreaGetYearOfStudy()
        {
            Area areas = ModelFromActionResult<Area>((IActionResult)controller.Get(3));
            Assert.Equal(areas.YearOfStudy.YearOfStudyId, 3);
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
