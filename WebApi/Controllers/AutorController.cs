using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly DL.JwtexamenBibliotecaContext _context;
        private readonly BL.IAutor _Iautor;

        public AutorController(DL.JwtexamenBibliotecaContext context, BL.IAutor Iautor)
        {
            _context = context;
            _Iautor = Iautor;
        }

        [HttpPost]
        [Route("GetAll")]
        public IActionResult GetAll([FromBody] ML.Autor autor)
        {
            //medio.Titulo = "";
            ML.Result result = _Iautor.GetAll(autor);

            if (result.Correct)
            {
                //return StatusCode(StatusCodes.Status200OK, new { value = result);
                return Ok(result);
            }
            else
            {
                //return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token = "" });
                return StatusCode(500, result.ErrorMessage);
            }
        }

        [HttpPost]
        [Route("GetById/{idAutor}")]
        public IActionResult GetById(int idAutor)
        {
            ML.Result result = _Iautor.GetById(idAutor);

            if (result.Correct)
            {
                //return StatusCode(StatusCodes.Status200OK, new { value = result);
                return Ok(result);
            }
            else
            {
                //return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token = "" });
                return StatusCode(500, result.ErrorMessage);
            }
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] ML.Autor autor)
        {
            ML.Result result = _Iautor.Add(autor);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, result.ErrorMessage);
            }
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] ML.Autor autor)
        {
            ML.Result result = _Iautor.Update(autor);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, result.ErrorMessage);
            }
        }

        [HttpDelete("Delete{idAutor}")]
        public IActionResult Delete(int idAutor)
        {
            ML.Result result = _Iautor.Delete(idAutor);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, result.ErrorMessage);
            }
        }
    }
}
