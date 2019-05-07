using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace TestGeneratorv1.Controllers
{
    [Produces("application/json")]
    [Route("api/difficultyLevel")]
    public class DifficultyLevelsController : Controller
    {

        private readonly TestGeneratorContext context;

        public DifficultyLevelsController(TestGeneratorContext _context)
        {
            context = _context;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            var result = context.DifficultyLevel.OfType<DifficultyLevel>().ToList();
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

            var result = context.DifficultyLevel.OfType<DifficultyLevel>().SingleOrDefault(difficultyLevel => difficultyLevel.DifficultyLevelId == id);
            if (result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] DifficultyLevel difficultyLevel)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.DifficultyLevel.Add(difficultyLevel);
            context.SaveChanges();
            return CreatedAtAction("Create", difficultyLevel);
        }

        [Microsoft.AspNetCore.Mvc.HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, [FromBody] DifficultyLevel difficultyLevel)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            var entity = context.DifficultyLevel.FirstOrDefault(DifficultyLevel => DifficultyLevel.DifficultyLevelId == id);
            if (entity == null)
            {
                return NotFound(difficultyLevel);
            }

            entity.Level = difficultyLevel.Level;

            context.DifficultyLevel.Update(entity);
            context.SaveChanges();
            return Ok(difficultyLevel);
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            DifficultyLevel result = context.DifficultyLevel.OfType<DifficultyLevel>().Where(DifficultyLevel => DifficultyLevel.DifficultyLevelId == id).SingleOrDefault();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.DifficultyLevel.Remove(result);
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