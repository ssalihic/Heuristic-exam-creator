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
using TestGeneratorv1.ViewModel;
using Microsoft.Extensions.Primitives;
using System.Text;

namespace TestGeneratorv1.Controllers
{
    [Produces("application/json")]
    [Route("api/test")]
    public class TestController : Controller
    {
        private readonly TestGeneratorContext context;

        public TestController(TestGeneratorContext _context)
        {
            context = _context;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            var result = GetAll();
            foreach (var i in result)
                foreach (var j in i.TestQuestions) i.Questions.Add(j.Question);

            if (!result.Any())
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        //Ovo je full test sa pitanjima i svim podacima, treba vracati objekte samo sa id-evima unutra :D
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            var result = context.Test.OfType<Test>()
                                               .Select(i => new Test
                                               {
                                                   TestId = i.TestId,
                                                   Subject = (new Subject
                                                   {
                                                       SubjectId = i.Subject.SubjectId,
                                                       SubjectName = i.Subject.SubjectName,
                                                       Areas = new List<Area>(i.Subject.Areas.Select(k => new Area
                                                       {
                                                           AreaId = k.AreaId,
                                                           AreaName = k.AreaName,
                                                           YearOfStudy = k.YearOfStudy
                                                       }))
                                                   }),
                                                   NumberOfQuestions = i.NumberOfQuestions,
                                                   TotalDifficultyLevel = i.TotalDifficultyLevel,
                                                   AreasSelected = i.AreasSelected,
                                                   YearOfStudy = i.YearOfStudy,
                                                   Questions = i.Questions,
                                                   MaxDifficultyLevel = i.MaxDifficultyLevel,
                                                   Group = i.Group,
                                                   TestGroup = i.TestGroup,
                                                   CreatedDate = i.CreatedDate,
                                                   TestQuestions = new List<TestQuestion>(i.TestQuestions
                                                   .Select(j => new TestQuestion
                                                   {
                                                       TestQuestionId = j.TestQuestionId,
                                                       Question = (new Question
                                                       {
                                                           QuestionId = j.Question.QuestionId,
                                                           QuestionText = j.Question.QuestionText,
                                                           QuestionImage = j.Question.QuestionImage,
                                                           Answer = j.Question.Answer,
                                                           Subject = (new Subject
                                                           {
                                                               SubjectId = j.Question.Subject.SubjectId,
                                                               SubjectName = j.Question.Subject.SubjectName,
                                                               Areas = new List<Area>(j.Question.Subject.Areas.Select(k => new Area
                                                               {
                                                                   AreaId = k.AreaId,
                                                                   AreaName = k.AreaName,
                                                                   YearOfStudy = k.YearOfStudy
                                                               }))

                                                           }),
                                                           Area = j.Question.Area,
                                                           DifficultyLevel = j.Question.DifficultyLevel,
                                                           Status = j.Question.Status,
                                                           Visibility = j.Question.Visibility,
                                                           QuestionType = j.Question.QuestionType,
                                                           Points = j.Question.Points,
                                                           CreatedDate = j.Question.CreatedDate,
                                                           ModifiedDate = j.Question.ModifiedDate,
                                                           Note = j.Question.Note

                                                       })
                                                   })),
                                                   TestArea = new List<TestArea>(i.TestArea
                                                   .Select(j => new TestArea
                                                   {
                                                       TestAreaId = j.TestAreaId,
                                                       Area = (new Area
                                                       {


                                                           AreaId = j.Area.AreaId,
                                                           AreaName = j.Area.AreaName,
                                                           YearOfStudy = j.Area.YearOfStudy

                                                       }
                                                       )

                                                   })
                                                    )


                                               })
                                               .SingleOrDefault(test => test.TestId == id);


            if (result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] Test test)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                context.Entry(test.Subject).State = EntityState.Unchanged;
                context.Entry(test.MaxDifficultyLevel).State = EntityState.Unchanged;
                context.Entry(test.YearOfStudy).State = EntityState.Unchanged;
                context.Entry(test.Creator).State = EntityState.Unchanged;
                test.Questions = new List<Question>();

                if (test.TestQuestions.Count != 0)
                {
                    foreach (var item in test.TestQuestions)
                        test.Questions.Add(item.Question);
                }
                test.TestArea = new List<TestArea>();
                test.TestQuestions = new List<TestQuestion>();
                test.AreasSelected = null;
                foreach (var question in test.Questions)
                {
                    question.Area = null;
                    question.DifficultyLevel = null;
                    question.QuestionType = null;
                    question.Status = null;
                    question.Subject = null;
                    question.Visibility = null;
                    context.Entry(question).State = EntityState.Unchanged;
                    test.TestQuestions.Add(new TestQuestion(test, question));
                }
                test.Questions = null;
                test.CreatedDate = DateTime.Now;
                context.Test.Add(test);

                context.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return CreatedAtAction("Create", test);
        }

        [Microsoft.AspNetCore.Mvc.HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, [FromBody] Test test)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            var entity = context.Test.FirstOrDefault(Test => Test.TestId == id);
            if (entity == null)
            {
                return NotFound(test);
            }

            entity.Subject = test.Subject;
            entity.AreasSelected = test.AreasSelected;
            entity.YearOfStudy = test.YearOfStudy;
            entity.MaxDifficultyLevel = test.MaxDifficultyLevel;
            entity.TotalDifficultyLevel = test.TotalDifficultyLevel;
            entity.NumberOfQuestions = test.NumberOfQuestions;

            context.Test.Update(entity);
            context.SaveChanges();
            return Ok(test);
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            var result = context.Test.OfType<Test>()
                                                         .Select(i => new Test
                                                         {
                                                             TestId = i.TestId,
                                                             TestQuestions = new List<TestQuestion>(i.TestQuestions
                                                             .Select(j => new TestQuestion
                                                             {
                                                                 TestQuestionId = j.TestQuestionId

                                                             })),
                                                             TestArea = new List<TestArea>(i.TestArea
                                                             .Select(j => new TestArea
                                                             {
                                                                 TestAreaId = j.TestAreaId
                                                             })
                                                              )


                                                         })
                                                         .SingleOrDefault(test => test.TestId == id);
            if (result == null)
            {
                return BadRequest(ModelState);
            }
            foreach (var i in result.TestQuestions)
            {
                var r = context.TestQuestion.OfType<TestQuestion>().Where(tq => tq.TestQuestionId == i.TestQuestionId).SingleOrDefault();
                context.TestQuestion.Remove(r);
            }
            context.Test.Remove(result);
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

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Route("generateTest")]
        public IActionResult Generate([FromBody] TestViewModel test)
        {
            if (!IsAuthorized(Request)) return Unauthorized();
        
            List<Question> questions = GenerateTest(test);
            if (questions == null) return BadRequest();
            return Ok(questions);
        }
        [Microsoft.AspNetCore.Mvc.ApiExplorerSettings(IgnoreApi = true)]
        public List<Question> GenerateTest([FromBody]TestViewModel test)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            List<Question> questions = new List<Question>();

            try
            {
                List<Question> ALL = new List<Question>();
                ALL = context.Question.OfType<Question>().Include(c => c.Answer)
                                                                .Include(c => c.DifficultyLevel)
                                                                .Include(c => c.QuestionType)
                                                                .Include(c => c.Visibility)
                                                                .Include(c => c.Subject)
                                                                .Include(c => c.Subject.Areas)
                                                                .Include(c => c.Area)
                                                                .Include(c => c.Area.YearOfStudy)
                                                                .Include(c => c.Status).Where(x => x.Subject.SubjectId == test.Subject.SubjectId && (x.Creator.Id == test.Creator.Id || x.Visibility.VisibilityName == "Public") && x.Status.StatusName == "Active").ToList();
                var questionTypes = context.QuestionType.OfType<QuestionType>().ToList();
                foreach (var i in test.AreasSelected)
                    foreach (var item in ALL) if (item.Area.AreaId == i.AreaId) questions.Add(item);

                ALL = questions;
                questions = new List<Question>();
                List<List<Question>> A = new List<List<Question>>();
                for (int i = 0; i < questionTypes.Count; i++) A.Add(new List<Question>());
                int j = 0;
                for (int i = 0; i < questionTypes.Count; i++)
                {
                    A[i] = ALL.Where(x => x.QuestionType.QuestionTypeId == questionTypes[i].QuestionTypeId).ToList();
                }

                Random rnd = new Random();
                int index = 0;
                for (int i = 0; i < A.Count; i++)
                {
                    if (A[i].Count == 0) continue;
                    var numberOfQuestion = test.Count.Find(x => x.Key.QuestionTypeId == A[i][0].QuestionType.QuestionTypeId).Value;
                    index = rnd.Next(A[i].Count);
                    for (int l = 0; l < numberOfQuestion; l++)
                    {
                        if (index >= 0 && index < A[i].Count)
                        {
                            questions.Add(A[i][index]);
                            A[i].Remove(A[i][index]);
                        }
                        index = rnd.Next(A[i].Count);
                    }

                }


                double minRange = (test.TotalDifficultyLevel - (test.TotalDifficultyLevel / test.NumberOfQuestions)) * test.NumberOfQuestions;
                double maxRange = (test.TotalDifficultyLevel + (test.TotalDifficultyLevel / test.NumberOfQuestions)) * test.NumberOfQuestions;

                int k = 0;

                while (k < ALL.Count * 2)
                {
                    double sum = questions.Sum(x => x.DifficultyLevel.Level);
                    index = rnd.Next(test.NumberOfQuestions);
                    if (index < 0 || index >= questions.Count) continue;
                    if (minRange <= sum && maxRange >= sum) break;

                    if (sum > maxRange && questions[index].DifficultyLevel.Level > test.TotalDifficultyLevel)
                    {
                        j = 0;
                        for (int i = 0; i < questionTypes.Count; i++)
                        {
                            if (questionTypes[i] == questions[index].QuestionType && A[i].Count != 0)
                            {
                                while (true)
                                {
                                    var l = A[i][rnd.Next(A[i].Count)];
                                    if (questions.Contains(l) == false && l.DifficultyLevel.Level <= test.TotalDifficultyLevel && l.DifficultyLevel.Level <= test.MaxDifficultyLevel.Level)
                                    {
                                        questions[index] = l;
                                        break;
                                    }
                                    j++;
                                    if (j > A[i].Count) break;
                                }
                                break;

                            }
                        }


                    }
                    else if (sum < minRange && questions[index].DifficultyLevel.Level < test.TotalDifficultyLevel)
                    {
                        j = 0;
                        for (int i = 0; i < questionTypes.Count; i++)
                        {
                            if (questionTypes[i] == questions[index].QuestionType && A[i].Count != 0)
                            {
                                while (true)
                                {
                                    var l = A[i][rnd.Next(A[i].Count)];
                                    if (questions.Contains(l) == false && l.DifficultyLevel.Level >= test.TotalDifficultyLevel && l.DifficultyLevel.Level <= test.MaxDifficultyLevel.Level)
                                    {
                                        questions[index] = l;
                                        break;
                                    }
                                    j++;
                                    if (j > A[i].Count) break;
                                }
                                break;

                            }
                        }

                    }
                    k++;

                }
            }
            catch (Exception e)
            {
                return null;
            }
            return questions;

        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Route("testValidation")]
        public IActionResult ValidateTest([FromBody] TestViewModel test)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string error = "";
            try
            {
                if (test.TotalDifficultyLevel > test.MaxDifficultyLevel.Level)
                    error += "Total difficulty level can't be greater than Max difficulty level!<br>";
                if (test.NumberOfQuestions <= 0)
                    error += "Number of questions can't be zero!<br>";
                if (test.AreasSelected.Count == 0)
                    error += "Select an area!<br>";
                if (test.Count.Sum(x => x.Value) != test.NumberOfQuestions)
                    error += "The sum of question type numbers must be equal to number of questions!<br>";
                //Tip pitanja i broj
                List<Question> ALL = new List<Question>();
                List<Question> questions = new List<Question>();

                ALL = context.Question.OfType<Question>().Include(c => c.Answer)
                                                                .Include(c => c.DifficultyLevel)
                                                                .Include(c => c.QuestionType)
                                                                .Include(c => c.Visibility)
                                                                .Include(c => c.Subject)
                                                                .Include(c => c.Subject.Areas)
                                                                .Include(c => c.Area)
                                                                .Include(c => c.Area.YearOfStudy)
                                                                .Include(c => c.Status).Where(x => x.Subject.SubjectId == test.Subject.SubjectId && (x.Creator.Id == test.Creator.Id || x.Visibility.VisibilityName == "Public") && x.Status.StatusName == "Active").ToList();
                foreach (var i in test.AreasSelected)
                    foreach (var j in ALL) if (j.Area.AreaId == i.AreaId) questions.Add(j);


                foreach (var i in test.Count)
                {
                    if (questions.FindAll(x => x.QuestionType.QuestionTypeId == i.Key.QuestionTypeId).Count < i.Value)
                        error += "There are not " + i.Value + " questions with type " + i.Key.QuesstionTypeName + "!<br>";
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok(error);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Route("pagination/limit={limit:int}&skip={skip:int}&id={id}")]
        public IActionResult Pagination([FromBody]int index, int limit, int skip, string id)
        {
            if (!IsAuthorized(Request)) return Unauthorized();
            var result = GetAll().Where(x => x.Creator.Id == id && (index <= 0 || x.Subject.SubjectId == index)).ToList();
            return Ok(new { count = result.Count, data = result.Skip((int)skip).Take((int)limit).ToList() });
        }


        [Microsoft.AspNetCore.Mvc.ApiExplorerSettings(IgnoreApi = true)]
        public List<Test> GetAll()
        {
            return context.Test.OfType<Test>().Select(i => new Test
                                            {
                                                TestId = i.TestId,
                                                Subject = (new Subject
                                                {
                                                    SubjectId = i.Subject.SubjectId,
                                                    SubjectName = i.Subject.SubjectName,
                                                    Areas = new List<Area>(i.Subject.Areas.Select(k => new Area
                                                    {
                                                        AreaId = k.AreaId,
                                                        AreaName = k.AreaName,
                                                        YearOfStudy = k.YearOfStudy
                                                    }))
                                                }),
                                                NumberOfQuestions = i.NumberOfQuestions,
                                                TotalDifficultyLevel = i.TotalDifficultyLevel,
                                                AreasSelected = i.AreasSelected,
                                                YearOfStudy = i.YearOfStudy,
                                                Questions = i.Questions,
                                                MaxDifficultyLevel = i.MaxDifficultyLevel,
                                                Group = i.Group,
                                                TestGroup = i.TestGroup,
                                                CreatedDate = i.CreatedDate,
                                                Creator = i.Creator,
                                                Date = i.Date,
                                                Note = i.Note,
                                                TestQuestions = new List<TestQuestion>(i.TestQuestions
                                                .Select(j => new TestQuestion
                                                {
                                                    TestQuestionId = j.TestQuestionId,
                                                    Question = (new Question
                                                    {
                                                        QuestionId = j.Question.QuestionId,
                                                        QuestionText = j.Question.QuestionText,
                                                        QuestionImage = j.Question.QuestionImage,
                                                        Answer = j.Question.Answer,
                                                        Subject = (new Subject
                                                        {
                                                            SubjectId = j.Question.Subject.SubjectId,
                                                            SubjectName = j.Question.Subject.SubjectName,
                                                            Areas = new List<Area>(j.Question.Subject.Areas.Select(k => new Area
                                                            {
                                                                AreaId = k.AreaId,
                                                                AreaName = k.AreaName,
                                                                YearOfStudy = k.YearOfStudy
                                                            }))

                                                        }),
                                                        Area = j.Question.Area,
                                                        DifficultyLevel = j.Question.DifficultyLevel,
                                                        Status = j.Question.Status,
                                                        Visibility = j.Question.Visibility,
                                                        QuestionType = j.Question.QuestionType,
                                                        Points = j.Question.Points,
                                                        CreatedDate = j.Question.CreatedDate,
                                                        ModifiedDate = j.Question.ModifiedDate,
                                                        Note = j.Question.Note

                                                    })
                                                })),
                                                TestArea = new List<TestArea>(i.TestArea
                                                .Select(j => new TestArea
                                                {
                                                    TestAreaId = j.TestAreaId,
                                                    Area = (new Area
                                                    {


                                                        AreaId = j.Area.AreaId,
                                                        AreaName = j.Area.AreaName,
                                                        YearOfStudy = j.Area.YearOfStudy

                                                    }
                                                    )

                                                })
                                                )
                                            }).ToList();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Route("recentTests/{userId}")]
        public IActionResult RecentTests(string userId)
        {
           if(!IsAuthorized(Request)) return Unauthorized();

            var result = GetAll().Where(x => x.Creator.Id == userId).OrderByDescending(x => x.CreatedDate).Take(5).ToList();
            return Ok(result);
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