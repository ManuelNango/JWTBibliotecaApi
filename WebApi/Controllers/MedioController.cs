using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Custom;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    //Solo accederán usuarios autorizados (parentesis si hay más autenticaciones)
    [Authorize/*(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)*/] 
    [ApiController]
    public class MedioController : ControllerBase
    {
        private readonly DL.JwtexamenBibliotecaContext _context;
        //Interfaz de BL
        private readonly BL.IMedio _Imedio;

        public MedioController(DL.JwtexamenBibliotecaContext context, BL.IMedio Imedio)
        {
            _context = context;
            _Imedio = Imedio;
        }

        //Crear los endpoints CRUD Medio
        [HttpPost]
        [Route("GetAll")]
        public IActionResult GetAll([FromBody] ML.Medio medio)
        {
            //medio.Titulo = "";
            ML.Result result = _Imedio.GetAll(medio);

            if(result.Correct)
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
        [Route("GetById/{idMedio}")]
        public IActionResult GetById(int idMedio)
        {
            ML.Result result = _Imedio.GetById(idMedio);

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
        public IActionResult Add([FromBody] ML.Medio medio)
        {
            ML.Result result = _Imedio.Add(medio);

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
        public IActionResult Update([FromBody] ML.Medio medio)
        {
            ML.Result result = _Imedio.Update(medio);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, result.ErrorMessage);
            }
        }

        [HttpDelete("Delete{idMedio}")]
        public IActionResult Delete(int idMedio)
        {
            ML.Result result = _Imedio.Delete(idMedio);

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
