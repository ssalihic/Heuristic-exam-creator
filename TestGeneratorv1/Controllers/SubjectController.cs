using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace TestGeneratorv1.Controllers
{
    [Produces("application/json")]
    [Route("api/subject")]
    public class SubjectController : Controller
    {
        private readonly TestGeneratorContext context;

        public SubjectController(TestGeneratorContext _context)
        {
            context = _context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            var result = context.Subject.OfType<Subject>().Include(c => c.Areas).Select(i => new Subject
            {
                SubjectId = i.SubjectId,
                SubjectName = i.SubjectName,
                Areas = new List<Area>(i.Areas.Select(k => new Area
                {
                    AreaId = k.AreaId,
                    AreaName = k.AreaName,
                    YearOfStudy = k.YearOfStudy
                })
                )
            }).ToList();

            if (!result.Any())
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("yearOfStudies/{id:int}")]
        public IActionResult YearOfStudies(int id)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            Subject result = context.Subject.OfType<Subject>()
                     .Select(i => new Subject
                     {
                         SubjectId = i.SubjectId,
                         SubjectName = i.SubjectName,
                         Areas = new List<Area>(i.Areas.Select(k => new Area
                         {
                             AreaId = k.AreaId,
                             AreaName = k.AreaName,
                             YearOfStudy = k.YearOfStudy

                         })
                       )
                     })
                     .SingleOrDefault(Subject => Subject.SubjectId == id);

            if (result == null)
            {
                return NotFound(result);
            }
            ISet<YearOfStudy> yearOfStudies = new HashSet<YearOfStudy>();
            foreach (var i in result.Areas)
                yearOfStudies.Add(i.YearOfStudy);


            return Ok(yearOfStudies);
        }

        [HttpGet]
        [Route("areas/{idSubject:int}/{idYearOfStudy:int}")]
        public IActionResult GetYearOfStudies(int idSubject, int idYearOfStudy)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            Subject result = context.Subject.OfType<Subject>()
                     .Select(i => new Subject
                     {
                         SubjectId = i.SubjectId,
                         SubjectName = i.SubjectName,
                         Areas = new List<Area>(i.Areas.Select(k => new Area
                         {
                             AreaId = k.AreaId,
                             AreaName = k.AreaName,
                             YearOfStudy = k.YearOfStudy

                         })
                       )
                     })
                     .SingleOrDefault(Subject => Subject.SubjectId == idSubject);

            if (result == null)
            {
                return NotFound(result);
            }
            List<Area> areas = new List<Area>();
            foreach (var i in result.Areas)
                if (i.YearOfStudy.YearOfStudyId == idYearOfStudy)
                    areas.Add(i);

            return Ok(areas);
        }


        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            Subject result = context.Subject.OfType<Subject>()
                     .Select(i => new Subject
                     {
                         SubjectId = i.SubjectId,
                         SubjectName = i.SubjectName,
                         Areas = new List<Area>(i.Areas.Select(k => new Area
                         {
                             AreaId = k.AreaId,
                             AreaName = k.AreaName,
                             YearOfStudy = k.YearOfStudy
                         })
                             )
                     })
                     .SingleOrDefault(Subject => Subject.SubjectId == id);


            if (result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Subject subject)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                foreach (var i in subject.Areas)
                {
                    if (i.AreaId != 0)
                        context.Entry(i).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                }
                context.Subject.Add(subject);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return CreatedAtAction("Create", subject);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, [FromBody] Subject subject)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            var entity = context.Subject.FirstOrDefault(Subject => Subject.SubjectId == id);
            if (entity == null)
            {
                return NotFound(subject);
            }
            try
            {
                entity.SubjectName = subject.SubjectName;
                if (subject.Areas != null)
                {
                    foreach (var i in subject.Areas)
                    {
                        if (context.Entry(i).Entity.AreaId <= 0)
                        {
                            context.Entry(i).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }

                        else context.Entry(i).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                    }
                    entity.Areas = subject.Areas;
                }
                context.Subject.Update(entity);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok(subject);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            Subject result = context.Subject.OfType<Subject>()
                               .Select(i => new Subject
                               {
                                   SubjectId = i.SubjectId,
                                   SubjectName = i.SubjectName,
                                   Areas = new List<Area>(i.Areas.Select(k => new Area
                                   {
                                       AreaId = k.AreaId,
                                       AreaName = k.AreaName,
                                       YearOfStudy = k.YearOfStudy
                                   })
                                       )
                               })
                               .SingleOrDefault(Subject => Subject.SubjectId == id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var i in result.Areas)
                context.Area.Remove(i);
            var listOfUserSubjects = context.UserSubjects.Where(x => x.Subject.SubjectId == id).ToList();
            foreach (var i in listOfUserSubjects)
            {
                context.UserSubjects.Remove(i);

            }
            context.Subject.Remove(result);
            context.SaveChanges();

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