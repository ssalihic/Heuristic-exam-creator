using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace TestGeneratorv1.Controllers
{
    [Produces("application/json")]
    [Route("api/yearOfStudy")]
    public class YearOfStudyController : Controller
    {

        private readonly TestGeneratorContext context;

        public YearOfStudyController(TestGeneratorContext _context)
        {
            context = _context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            var result = context.YearOfStudy.OfType<YearOfStudy>().ToList();
            if (!result.Any())
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            var result = context.YearOfStudy.OfType<YearOfStudy>().SingleOrDefault(YearOfStudy => YearOfStudy.YearOfStudyId == id);
            if (result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] YearOfStudy YearOfStudy)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                context.YearOfStudy.Add(YearOfStudy);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
            return CreatedAtAction("Create", YearOfStudy);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, [FromBody] YearOfStudy YearOfStudy)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            var entity = context.YearOfStudy.Single(yearOfStudy => yearOfStudy.YearOfStudyId == id);
            if (entity == null)
            {
                return NotFound(YearOfStudy);
            }
            entity.YearOfStudyName = YearOfStudy.YearOfStudyName;

            context.YearOfStudy.Update(entity);
            context.SaveChanges();
            return Ok(YearOfStudy);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            YearOfStudy result = context.YearOfStudy.OfType<YearOfStudy>().Where(YearOfStudy => YearOfStudy.YearOfStudyId == id).SingleOrDefault();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.YearOfStudy.Remove(result);
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