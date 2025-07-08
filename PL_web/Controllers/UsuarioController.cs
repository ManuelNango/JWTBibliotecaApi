using Microsoft.AspNetCore.Mvc;

namespace PL_web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly BL.IUsuario _Iusuario;
        public UsuarioController(BL.IUsuario Iusuario)
        {
            _Iusuario = Iusuario;
        }

        public IActionResult GetAll()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }
    }
}
