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
    public class DifficultyLevelTest
    {
        public static IEnumerable<DifficultyLevel> levels;

        public DifficultyLevelTest()
        {
            InitContext();
            controller = new DifficultyLevelsController(context);

        }
        private DifficultyLevelsController controller;

        private TestGeneratorContext context;

        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder<TestGeneratorContext>()
                .UseInMemoryDatabase();
            var _context = new TestGeneratorContext(builder.Options);
            if (levels == null)
            {
                levels = Enumerable.Range(1, 10)
                    .Select(i => new DifficultyLevel { DifficultyLevelId = i, Level = i });
                _context.DifficultyLevel.AddRange(levels);
            }
            int changed = _context.SaveChanges();
            context = _context;
        }
        [Fact]
        public void A1GetAllDifficultyLevel()
        {
            List<DifficultyLevel> yearOfStudies = ModelFromActionResult<List<DifficultyLevel>>((IActionResult)controller.Get());
            Assert.Equal(yearOfStudies.Count, 10);
        }
        [Fact]
        public void GetByIdDifficultyLevel()
        {
            DifficultyLevel DifficultyLevel = ModelFromActionResult<DifficultyLevel>((IActionResult)controller.Get(3));
            Assert.Equal(DifficultyLevel.DifficultyLevelId, 3);
        }
        [Fact]
        public void DeleteDifficultyLevel()
        {
            controller.Delete(5);
            var result = controller.Get(5);
            Assert.Equal(result.GetType(), typeof(NotFoundObjectResult));
        }

        [Fact]
        public void CreateDifficultyLevel()
        {
            var result = controller.Create(new DifficultyLevel { DifficultyLevelId = 11,  Level = 11 });
            Assert.Equal(result.GetType(), typeof(CreatedAtActionResult));
            var result2 = ModelFromActionResult<DifficultyLevel>((IActionResult)controller.Get(11));
            Assert.Equal(result2.Level, 11);

        }

        [Fact]
        public void UpdateDifficultyLevel()
        {
            var result1 = ModelFromActionResult<DifficultyLevel>((IActionResult)controller.Get(2));
            Assert.Equal(result1.Level, 2);
            controller.Update(2, new DifficultyLevel { DifficultyLevelId = 22, Level = 22 });
            var result = ModelFromActionResult<DifficultyLevel>((IActionResult)controller.Get(2));
            Assert.Equal(result.Level, 22);

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
