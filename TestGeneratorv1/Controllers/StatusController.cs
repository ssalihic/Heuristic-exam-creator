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
    [Route("api/status")]
    public class StatusController : Controller
    {

        private readonly TestGeneratorContext context;

        public StatusController(TestGeneratorContext _context)
        {
            context = _context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            var result = context.Status.OfType<Status>().ToList();
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

            var result = context.Status.OfType<Status>().SingleOrDefault(status => status.StatusId == id);
            if (result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Status status)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Status.Add(status);
            context.SaveChanges();
            return CreatedAtAction("Create", status);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, [FromBody] Status status)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            var entity = context.Status.FirstOrDefault(Status => Status.StatusId == id);
            if (entity == null)
            {
                return NotFound(status);
            }
            entity.StatusName = status.StatusName;

            context.Status.Update(entity);
            context.SaveChanges();
            return Ok(status);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            Status result = context.Status.OfType<Status>().Where(Status => Status.StatusId == id).SingleOrDefault();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Status.Remove(result);
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
