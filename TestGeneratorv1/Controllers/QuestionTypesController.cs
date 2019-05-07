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
    [Route("api/questionType")]
    public class QuestionTypesController : Controller
    {


        private readonly TestGeneratorContext context;

        public QuestionTypesController(TestGeneratorContext _context)
        {
            context = _context;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            var result = context.QuestionType.OfType<QuestionType>().ToList();
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

            var result = context.QuestionType.OfType<QuestionType>().SingleOrDefault(questionType => questionType.QuestionTypeId == id);
            if (result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] QuestionType questionType)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.QuestionType.Add(questionType);
            context.SaveChanges();
            return CreatedAtAction("Create", questionType);
        }

        [Microsoft.AspNetCore.Mvc.HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, [FromBody] QuestionType questionType)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            var entity = context.QuestionType.FirstOrDefault(QuestionType => QuestionType.QuestionTypeId == id);
            if (entity == null)
            {
                return NotFound(questionType);
            }
            entity.QuesstionTypeName = questionType.QuesstionTypeName;

            context.QuestionType.Update(entity);
            context.SaveChanges();
            return Ok(questionType);
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (!IsAuthorized(Request)) return Unauthorized();

            QuestionType result = context.QuestionType.OfType<QuestionType>().Where(QuestionType => QuestionType.QuestionTypeId == id).SingleOrDefault();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.QuestionType.Remove(result);
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