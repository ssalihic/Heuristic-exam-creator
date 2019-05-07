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
    [Route("api/visibility")]
    public class VisibilityController : Controller
    {

        private readonly TestGeneratorContext context;

        public VisibilityController(TestGeneratorContext _context)
        {
            context = _context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            var result = context.Visibility.OfType<Visibility>().ToList();
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

            var result = context.Visibility.OfType<Visibility>().SingleOrDefault(visibility => visibility.VisibilityId == id);
            if (result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Visibility visibility)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Visibility.Add(visibility);
            context.SaveChanges();
            return CreatedAtAction("Create", visibility);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, [FromBody] Visibility visibility)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            var entity = context.Visibility.FirstOrDefault(Visibility => Visibility.VisibilityId == id);
            if (entity == null)
            {
                return NotFound(visibility);
            }
            entity.VisibilityName = visibility.VisibilityName;

            context.Visibility.Update(entity);
            context.SaveChanges();
            return Ok(visibility);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            Visibility result = context.Visibility.OfType<Visibility>().Where(visibility => visibility.VisibilityId == id).SingleOrDefault();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Visibility.Remove(result);
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