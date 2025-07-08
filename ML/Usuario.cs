using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Contrasena { get; set; } = null!;

        public ML.Rol? Rol { get; set; }
        public ML.Direccion? Direccion { get; set; }
        public List<object>? Usuarios { get; set; }
    }
}
