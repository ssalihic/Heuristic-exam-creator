using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestGeneratorv1.Controllers
{
    [Produces("application/json")]
    [Route("api/testQuestion")]
    public class TestQuestionController : Controller
    {
        private readonly TestGeneratorContext context;

        public TestQuestionController(TestGeneratorContext _context)
        {
            context = _context;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            var result = context.TestQuestion.OfType<TestQuestion>().Include(c => c.Question)
                                                                    .Include(c => c.Question.Answer)
                                                                    .Include(c => c.Question.Area)
                                                                    .Include(c => c.Question.Area.YearOfStudy)
                                                                    .Include(c => c.Question.DifficultyLevel)
                                                                    .Include(c => c.Question.QuestionType)
                                                                    .Include(c => c.Question.Status)
                                                                    .Include(c => c.Question.Points)
                                                                    .Include(c => c.Question.Subject)
                                                                    .Include(c => c.Question.Visibility)
                                                                    .ToList();

            if (!result.Any())
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {

            var result = context.TestQuestion.OfType<TestQuestion>().Include(c => c.Question)
                                                                    .Include(c => c.Question.Answer)
                                                                    .Include(c => c.Question.Points)
                                                                    .Include(c => c.Question.DifficultyLevel)
                                                                    .Include(c => c.Question.Area)
                                                                    .Include(c => c.Question.Area.YearOfStudy)
                                                                    .Include(c => c.Question.QuestionType)
                                                                    .Include(c => c.Question.Status)
                                                                    .Include(c => c.Question.Subject)
                                                                    .Include(c => c.Question.Visibility)
                                                                    .Where(TestQuestion => TestQuestion.TestQuestionId == id);
            if (!result.Any())
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        //  Vraca listu pitanja za odredjeni test
        //[Microsoft.AspNetCore.Mvc.HttpGet]
        //[Route("{id:int}")]
        //public IActionResult GetTestQuestions(int testId)
        //{

        //    var result = context.TestQuestion.OfType<TestQuestion>().Include(c => c.Question.Answer)
        //                                                            .Include(c => c.Question.DifficultyLevel)
        //                                                            .Include(c => c.Question.Area)
        //                                                            .Include(c => c.Question.Area.YearOfStudy)
        //                                                            .Include(c => c.Question.QuestionType)
        //                                                            .Include(c => c.Question.Status)
        //                                                            .Include(c => c.Question.Subject)
        //                                                            .Include(c => c.Question.Visibility)
        //                                                            .Where(TestQuestion => TestQuestion.Test.TestId == testId);
        //    List<Question> questions = new List<Question>();

        //    foreach (var i in result) questions.Add(i.Question);

        //    if (!result.Any())
        //    {
        //        return NotFound(result);
        //    }
        //    return Ok(questions);
        //}
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] TestQuestion testQuestion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Entry(testQuestion.Question).State = EntityState.Unchanged;

            context.TestQuestion.Add(testQuestion);

            context.SaveChanges();
            return CreatedAtAction("Create", testQuestion);
        }

        [Microsoft.AspNetCore.Mvc.HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, [FromBody] TestQuestion testQuestion)
        {
            var entity = context.TestQuestion.FirstOrDefault(TestQuestion => TestQuestion.TestQuestionId == id);
            if (entity == null)
            {
                return NotFound(testQuestion);
            }
            context.Entry(testQuestion.Question).State = EntityState.Unchanged;
            entity.Question = testQuestion.Question;
            context.SaveChanges();
            return Ok(testQuestion);
        }

        //Moguce i primati samo Test i da se u njemu nalazi lista pitanja
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Route("{id:int}")]
        public IActionResult Save(int id, [FromBody] List<Question> questions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TestController testController = new TestController(context);
            List<Test> tests = new List<Test>();

            tests = ModelFromActionResult<List<Test>>((IActionResult)testController.Get());
            Test test = tests.Find(x => x.TestId == id);
            context.Entry(test).State = EntityState.Unchanged;
            foreach (var question in questions)
            {
                context.Entry(question).State = EntityState.Unchanged;
                context.TestQuestion.Add(new TestQuestion(test, question));
            }

            context.SaveChanges();
            return CreatedAtAction("Create", questions);
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            TestQuestion result = context.TestQuestion.OfType<TestQuestion>().Where(TestQuestion => TestQuestion.TestQuestionId == id).SingleOrDefault();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.TestQuestion.Remove(result);
            context.SaveChanges();
            return Ok(result);
        }

        [Microsoft.AspNetCore.Mvc.ApiExplorerSettings(IgnoreApi = true)]
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