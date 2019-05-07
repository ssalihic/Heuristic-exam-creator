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
    [Route("api/answer")]
    public class AnswerController : Controller
    {

        private readonly TestGeneratorContext context;

        public AnswerController(TestGeneratorContext _context)
        {
            context = _context;
        }
        // GET api/answer
        /// <summary>
        /// This is how we create a documentation
        /// </summary>
        /// <returns></returns>
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            var result = context.Answer.OfType<Answer>().ToList();
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

            var result = context.Answer.OfType<Answer>().SingleOrDefault(Answer => Answer.AnswerId == id);
            if (result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] Answer answer)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Answer.Add(answer);
            context.SaveChanges();
            return CreatedAtAction("Create", answer);
        }

        [Microsoft.AspNetCore.Mvc.HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, [FromBody] Answer answer)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            var entity = context.Answer.FirstOrDefault(Answer => Answer.AnswerId == id);
            if (entity == null)
            {
                return NotFound(answer);
            }
            entity.AnswerPicture = answer.AnswerPicture;
            entity.AnswerText = answer.AnswerText;
            context.Answer.Update(entity);

            context.SaveChanges();
            return Ok(answer);
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            Answer result = context.Answer.OfType<Answer>().Where(Answer => Answer.AnswerId == id).SingleOrDefault();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Answer.Remove(result);
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