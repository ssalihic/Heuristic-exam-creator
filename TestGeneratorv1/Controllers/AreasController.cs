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
    [Route("api/areas")]
    public class AreasController : Controller
    {
        private readonly TestGeneratorContext context;

        public AreasController(TestGeneratorContext _context)
        {
            context = _context;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            var result = context.Area.OfType<Area>().Include(i=> i.YearOfStudy).ToList();
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

            var result = context.Area.OfType<Area>().Include(i => i.YearOfStudy).SingleOrDefault(Area => Area.AreaId == id);
            if (result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult Create([FromBody] Area area)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                context.Entry(area.YearOfStudy).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                context.Area.Add(area);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return CreatedAtAction("Create", area);
        }

        [Microsoft.AspNetCore.Mvc.HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, [FromBody] Area area)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            var entity = context.Area.FirstOrDefault(Area => Area.AreaId == id);
            if (entity == null)
            {
                return NotFound(area);
            }

            context.Entry(area.YearOfStudy).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

            entity.AreaName = area.AreaName;
            entity.YearOfStudy = area.YearOfStudy;

            context.Area.Update(entity);
            context.SaveChanges();
            return Ok(area);
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            Area result = context.Area.OfType<Area>().Where(area => area.AreaId == id).SingleOrDefault();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Area.Remove(result);
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