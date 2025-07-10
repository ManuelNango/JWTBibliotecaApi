using Microsoft.AspNetCore.Mvc;

namespace PL_web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        //Instalar esto en Nuget:
        //Install-Package Microsoft.AspNet.WebApi.Client

        [HttpPost]
        public JsonResult AutenticarUsuario(string email, string contrasena)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5075/");
            var loginData = new
            {
                email = email,
                contrasena = contrasena
            };

            var response = client.PostAsJsonAsync("api/Login/Login", loginData).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<dynamic>().Result;
                string token = result.token;

                // Devolver el token al cliente para que lo guarde en una cookie
                return Json(new { success = true, token = token });
            }
            else
            {
                return Json(new { success = false });
            }
        }
    }
}