using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.Extensions.Primitives;

namespace TestGeneratorv1.Controllers
{
    [Produces("application/json")]
    [Route("api/question")]
    public class QuestionController : Controller
    {

        private readonly TestGeneratorContext context;

        public QuestionController(TestGeneratorContext _context)
        {
            context = _context;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            var result = context.Question.OfType<Question>().Include(c => c.Answer)
                                                            .Include(c => c.DifficultyLevel)
                                                            .Include(c => c.QuestionType)
                                                            .Include(c => c.Visibility)
                                                            .Include(c => c.Subject)
                                                            .Include(c => c.Subject.Areas)
                                                            .Include(c => c.Area)
                                                            .Include(c => c.Creator)
                                                            .Include(c => c.Area.YearOfStudy)
                                                            .Include(c => c.Status).ToList();

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
            if (!IsAuthorized(Request)) return Unauthorized();

            var result = context.Question.OfType<Question>().Include(c => c.Answer)
                                                            .Include(c => c.DifficultyLevel)
                                                            .Include(c => c.QuestionType)
                                                            .Include(c => c.Visibility)
                                                            .Include(c => c.Subject)
                                                            .Include(c => c.Subject.Areas)
                                                            .Include(c => c.Area)
                                                            .Include(c => c.Area.YearOfStudy)
                                                            .Include(c => c.Status)
                                                            .Include(c => c.Creator)
                                                            .SingleOrDefault(Question => Question.QuestionId == id);
            if (result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] Question question)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            question.CreatedDate = DateTime.Now;

            question.ModifiedDate = DateTime.Now;

            try
            {
                context.Entry(question.Subject).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

                context.Entry(question.Status).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

                context.Entry(question.Visibility).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

                context.Entry(question.DifficultyLevel).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

                context.Entry(question.QuestionType).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

                context.Entry(question.Area).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

                context.Entry(question.Creator).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

                context.Question.Add(question);

                context.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return CreatedAtAction("Create", question);

        }

        [Microsoft.AspNetCore.Mvc.HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, [FromBody] Question question)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            var entity = context.Question.FirstOrDefault(Question => Question.QuestionId == id);
            if (entity == null)
            {
                return NotFound(question);
            }
            try
            {
                entity.Answer = question.Answer;
                entity.QuestionImage = question.QuestionImage;
                entity.QuestionText = question.QuestionText;
                entity.Subject = question.Subject;
                entity.DifficultyLevel = question.DifficultyLevel;
                entity.Status = question.Status;
                entity.Visibility = question.Visibility;
                entity.QuestionType = question.QuestionType;
                entity.Points = question.Points;
                entity.CreatedDate = question.CreatedDate;
                entity.ModifiedDate = DateTime.Now;
                entity.Note = question.Note;
                entity.Points = question.Points;

                entity.QuestionType = question.QuestionType;
                entity.Creator = question.Creator;
                question = new Question();

                context.Entry(entity.Answer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.Entry(entity.Creator).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.Entry(entity.DifficultyLevel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.Entry(entity.Visibility).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                context.Entry(entity.QuestionType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.Entry(entity.Status).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.Entry(entity.Area.YearOfStudy).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.Entry(entity.Subject).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                foreach (var i in entity.Subject.Areas)
                {
                    if (i != null) context.Entry(i).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    if (i.YearOfStudy != null) context.Entry(i.YearOfStudy).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                foreach (var i in entity.Creator.Subjects)
                {
                    if (i != null) context.Entry(i).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    foreach (var j in i.Areas)
                    {
                        if (j != null) context.Entry(j).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        if (j.YearOfStudy != null) context.Entry(j.YearOfStudy).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                }


                context.Question.Update(entity);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok(question);
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            Question result = context.Question.OfType<Question>().Where(Question => Question.QuestionId == id).SingleOrDefault();
            if (result == null)
            {
                return NotFound(result);
            }
            context.Question.Remove(result);
            context.SaveChanges();
            return Ok(result);
        }
       
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Route("pagination/limit={limit:int}&skip={skip:int}&id={id}")]
        public IActionResult GetAll([FromBody]List<int> index, int limit, int skip, string id)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            var questions = filter(index, id);
            if (questions == null) return BadRequest();
            int totalCount = questions.Count();
            var result = questions.Skip((int)skip).Take((int)limit);

            if (!result.Any())
            {
                return Ok(result);
            }
            return Ok(new { count = questions.Count, data = result });


        }
        [Microsoft.AspNetCore.Mvc.ApiExplorerSettings(IgnoreApi = true)]
        public List<Question> filter(List<int> index, string id)
        {
            var questions = context.Question.OfType<Question>().Include(c => c.Answer)
                                                           .Include(c => c.DifficultyLevel)
                                                           .Include(c => c.QuestionType)
                                                           .Include(c => c.Visibility)
                                                           .Include(c => c.Subject)
                                                           .Include(c => c.Subject.Areas)
                                                           .Include(c => c.Area)
                                                           .Include(c => c.Creator)
                                                           .Include(c => c.Area.YearOfStudy)
                                                           .Include(c => c.Status).Where(x => x.Creator.Id == id || x.Visibility.VisibilityName == "Public").ToList();
            List<Question> questionsFilter = new List<Question>();

            try
            {
                for (var i = 0; i < questions.Count; i++)
                {
                    if ((index[0] <= 0 || questions[i].Subject.SubjectId == index[0])
                       && (index[1] <= 0 || questions[i].Area.AreaId == index[1])
                       && (index[2] <= 0 || questions[i].Area.YearOfStudy.YearOfStudyId == index[2])
                       && (index[3] <= 0 || questions[i].Status.StatusId == index[3])
                       && (index[4] <= 0 || questions[i].Visibility.VisibilityId == index[4])
                       && (index[5] <= 0 || questions[i].DifficultyLevel.DifficultyLevelId == index[5])
                       && (index[6] <= 0 || questions[i].QuestionType.QuestionTypeId == index[6])
                    )
                        questionsFilter.Add(questions[i]);
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return questionsFilter;
        }
        [Microsoft.AspNetCore.Mvc.ApiExplorerSettings(IgnoreApi = true)]
        private bool IsAuthorized(HttpRequest request)
        {
            StringValues a = "";
            if (request.Headers.TryGetValue("Authorization", out a))
            {
                var check = context.UserTokens.Where(x => x.Value == a).SingleOrDefault();
                if (check == null) return false;
            }
            else return false;

            return true;
        }
    }
}