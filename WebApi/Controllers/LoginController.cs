using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using WebApi.Custom;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly DL.JwtexamenBibliotecaContext _context;
        private readonly Utilidades _utilidades;
        //private readonly BL.IUsuario _Iusuario;

        public LoginController(DL.JwtexamenBibliotecaContext context, Utilidades utilidades/*, BL.IUsuario iusuario*/)
        {
            _context = context;
            _utilidades = utilidades;
            //_Iusuario = iusuario;
        }

        [HttpPost]
        [Route("Registrarse")]
        public async Task<IActionResult> Registrarse(ML.Usuario usuario)
        {
            var modeloUsuario = new DL.Usuario
            {
                NombreCompleto = usuario.NombreCompleto,
                Email = usuario.Email,
                Contrasena = _utilidades.EncriptarSHA256(usuario.Contrasena)
            };

            /*
            var modeloRol = new DL.Rol
            {
                IdRol = usuario.Rol.IdRol
            };

            var modeloDireccion = new DL.Direccion
            {
                Calle = usuario.Direccion.Calle,
                NumeroInterior = usuario.Direccion.NumeroInterior,
                NumeroExterior = usuario.Direccion.NumeroExterior
            };

            var modeloColonia = new DL.Colonium
            {
                IdColonia = usuario.Direccion.Colonia.IdColonia
            };

            var modeloMunicipio = new DL.Municipio
            {
                IdMunicipio = usuario.Direccion.Colonia.Municipio.IdMunicipio
            };

            var modeloEstado = new DL.Estado
            {
                IdEstado = usuario.Direccion.Colonia.Municipio.Estado.IdEstado
            };


            //Lo de CGPT
            // Agregar la dirección al usuario
            modeloUsuario.Direccions = new List<DL.Direccion> { modeloDireccion };

            // Asegurarse de agregar la dirección al contexto de la base de datos
            await _context.Direccions.AddAsync(modeloDireccion);
        */

            // Agregar el usuario al contexto de la base de datos
            await _context.Usuarios.AddAsync(modeloUsuario);
            await _context.SaveChangesAsync();

            if(modeloUsuario.IdUsuario != 0)
            {
                return StatusCode(StatusCodes.Status200OK, new {isSuccess = true});
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new {isSuccess = false});
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(DL.LoginDTO usuario)
        {
            var usuarioEncontrado = await _context.Usuarios
                .Where(u =>
                    u.Email == usuario.Email &&
                    u.Contrasena == usuario.Contrasena).FirstOrDefaultAsync();

            if (usuarioEncontrado == null)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token = "" });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = _utilidades.GenerarJWT(usuarioEncontrado)});
            }
        }
    }
}
