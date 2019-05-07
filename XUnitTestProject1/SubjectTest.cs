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
    public class SubjectTest
    {
        public static IEnumerable<Subject> subjects;

        public SubjectTest()
        {
            InitContext();

        }
        private SubjectController controller;

        private TestGeneratorContext context;

        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder<TestGeneratorContext>()
                .UseInMemoryDatabase();
            var _context = new TestGeneratorContext(builder.Options);
            if (subjects == null)
            {
                subjects = Enumerable.Range(1, 10)
                    .Select(i => new Subject { SubjectId = i, SubjectName = $"Subject{i}", Areas = new List<Area> { new Area { AreaId = i + 1, AreaName = $"Sample{i}", YearOfStudy = new YearOfStudy { YearOfStudyId = i, YearOfStudyName = $"YOS{i}" } } } });
                _context.Subject.AddRange(subjects);
            }
            int changed = _context.SaveChanges();
                
            context = _context;
            controller = new SubjectController(context);
            
        }

    
        [Fact]
        public void A1GetAllSubject()
        {
            List<Subject> yearOfStudies = ModelFromActionResult<List<Subject>>((IActionResult)controller.Get());
            Assert.Equal(yearOfStudies.Count, 10);
        }
        [Fact]
        public void GetByIdSubject()
        {
            Subject Subject = ModelFromActionResult<Subject>((IActionResult)controller.Get(3));
            Assert.Equal(Subject.SubjectId, 3);
            Assert.Equal(Subject.SubjectName, "Subject3");
            Assert.Equal(Subject.Areas.Count, 1);
            Assert.Equal(Subject.Areas.First().AreaId, 4);
            Assert.Equal(Subject.Areas.First().YearOfStudy.YearOfStudyId, 3);
        }
        [Fact]
        public void DeleteSubject()
        {
            controller.Delete(5);
            IActionResult result = controller.Get(5);
            Assert.Equal(result.GetType(), typeof(NotFoundObjectResult));
        }

        [Fact]
        public void CreateSubject()
        {
            var result = controller.Create(new Subject { SubjectId = 11, SubjectName = $"Subject11", Areas = new List<Area>{ new Area {AreaName = $"Sample11", YearOfStudy = new YearOfStudy { YearOfStudyId = 11, YearOfStudyName = $"YOS{11}" } } } });
            Assert.Equal(result.GetType(), typeof(CreatedAtActionResult));
        }

        [Fact]
        public void UpdateSubject()
        {
            var result1 = ModelFromActionResult<Subject>((IActionResult)controller.Get(2));
            Assert.Equal(result1.SubjectName, "Subject2");
            controller.Update(2, new Subject { SubjectName = $"Subject{22}" });
            var result = ModelFromActionResult<Subject>((IActionResult)controller.Get(2));
            Assert.Equal(result.SubjectName, "Subject22");

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
