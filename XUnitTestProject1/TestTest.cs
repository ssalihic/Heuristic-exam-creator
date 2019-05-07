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
    public class TestTest
    {
        public static IEnumerable<Test> tests;

        public TestTest()
        {
            InitContext();

        }
        private TestController controller;

        private TestGeneratorContext context;

        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder<TestGeneratorContext>()
                .UseInMemoryDatabase();
            var _context = new TestGeneratorContext(builder.Options);
            if (tests == null)
            {
                 tests = Enumerable.Range(1, 10)
                    .Select(i => new Test
                    {
                        TestId = i,
                        AreasSelected = new List<Area> { new Area { AreaId = i + 1, AreaName = $"Sample{i}", YearOfStudy = new YearOfStudy { YearOfStudyId = i + 1, YearOfStudyName = $"YOS{i}" } } },
                        Subject = new Subject { },
                        MaxDifficultyLevel = new DifficultyLevel { },
                        CreatedDate = new DateTime { },
                        Note = $"Note{i}",
                        TotalDifficultyLevel = i,
                        NumberOfQuestions = i,
                        TestGroup = $"A{i}",
                        Group = $"{i}",
                        Questions = new List<Question>
                        {
                          new Question
                {
                    QuestionId = i,
                    QuestionText = $"Question{i}",
                    Area = new Area { AreaId = i + 30, AreaName = $"Sample{i}", YearOfStudy = new YearOfStudy { YearOfStudyId = i+30, YearOfStudyName = $"YOS{i}" } },
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
                }, new Question
                {
                    QuestionId = i+11,
                    QuestionText = $"Question{i}",
                    Area = new Area { AreaId = i + 31, AreaName = $"Sample{i}", YearOfStudy = new YearOfStudy { YearOfStudyId = i+31, YearOfStudyName = $"YOS{i}" } },
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
                } }
                    });


                _context.Test.AddRange(tests);
            }
            int changed = _context.SaveChanges();
            context = _context;
            controller = new TestController(context);


        }




        [Fact]
        public void A1GetAllTest()
        {
            List<Test> yearOfStudies = ModelFromActionResult<List<Test>>((IActionResult)controller.Get());
            Assert.Equal(yearOfStudies.Count, 10);
        }
        [Fact]
        public void GetByIdTest()
        {
            Test Test = ModelFromActionResult<Test>((IActionResult)controller.Get(3));
            Assert.Equal(Test.TestId, 3);

        }
        [Fact]
        public void DeleteTest()
        {
            controller.Delete(5);
            IActionResult result = controller.Get(5);
            Assert.Equal(result.GetType(), typeof(NotFoundObjectResult));
        }

        [Fact]
        public void CreateTest()
        {
          
            var result = controller.Create(new Test
            {
                TestId = 22,
                AreasSelected = new List<Area> { new Area { AreaName = $"Sample{22}", YearOfStudy = new YearOfStudy { YearOfStudyId = 22, YearOfStudyName = $"YOS{22}" } } },
                Subject = new Subject { },
                YearOfStudy = new YearOfStudy { },
                MaxDifficultyLevel = new DifficultyLevel { },
                CreatedDate = new DateTime { },
                Note = $"Note{22}",
                TotalDifficultyLevel = 22,
                NumberOfQuestions = 22,
                TestGroup = $"A{22}",
                Group = $"{22}",
                Questions = new List<Question>
                        {
                          new Question
                {
                    QuestionId = 100,
                    QuestionText = $"Question{100}",
                    Area = new Area { AreaId = 130, AreaName = $"Sample{10}", YearOfStudy = new YearOfStudy { YearOfStudyId = 130, YearOfStudyName = $"YOS{10}" } },
                    Points = 100,
                    Answer = new Answer { },
                    Subject = new Subject { },
                    DifficultyLevel = new DifficultyLevel { },
                    Status = new Status { },
                    Visibility = new Visibility { },
                    QuestionType = new QuestionType { },
                    CreatedDate = new DateTime { },
                    ModifiedDate = new DateTime { },
                    Note = $"Note{101}"
                }, new Question
                {
                    QuestionId = 111,
                    QuestionText = $"Question{100}",
                    Area = new Area { AreaId = 130, AreaName = $"Sample{10}", YearOfStudy = new YearOfStudy { YearOfStudyId = 130, YearOfStudyName = $"YOS{10}" } },
                    Points = 100,
                    Answer = new Answer { },
                    Subject = new Subject { },
                    DifficultyLevel = new DifficultyLevel { },
                    Status = new Status { },
                    Visibility = new Visibility { },
                    QuestionType = new QuestionType { },
                    CreatedDate = new DateTime { },
                    ModifiedDate = new DateTime { },
                    Note = $"Note{101}"
                } }
            });
            Test Test = ModelFromActionResult<Test>((IActionResult)controller.Get(22));
            Assert.NotNull(Test.Subject);
            Assert.Equal(result.GetType(), typeof(CreatedAtActionResult));
        }

        [Fact]
        public void UpdateTest()
        {
            var result1 = ModelFromActionResult<Test>((IActionResult)controller.Get(2));
            Assert.Equal(result1.NumberOfQuestions, 2);
            controller.Update(2, new Test
            {
                AreasSelected = new List<Area> { new Area { AreaId = 22 + 1, AreaName = $"Sample{22}", YearOfStudy = new YearOfStudy { YearOfStudyId = 52, YearOfStudyName = $"YOS{22}" } } },
                Subject = new Subject { },
                MaxDifficultyLevel = new DifficultyLevel { },
                YearOfStudy = new YearOfStudy { },
                    CreatedDate = new DateTime { },
                    Note = $"Note{22}",
                    TotalDifficultyLevel = 22,
                    NumberOfQuestions = 22,
                    TestGroup = $"A{22}",
                    Group = $"{22}",});
            var result = ModelFromActionResult<Test>((IActionResult)controller.Get(2));
            Assert.Equal(result.NumberOfQuestions,22);

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
