using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class UsuarioGetAllSP
    {
        public int IdUsuario {  get; set; }
        public string NombreCompleto { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Contrasena { get; set; } = null!;

        //Rol
        public int IdRol { get; set; }
        public string NombreRol { get; set; } = null!;

        //Dirección
        public int IdDireccion { get; set; }
        public string Calle { get; set; } = null!;
        public string? NumeroInterior { get; set; }
        public string NumeroExterior { get; set; } = null!;

        //Colonia
        public int IdColonia { get; set; }
        public string NombreColonia { get; set; } = null!;
        public string CodigoPostal { get; set; } = null!;

        //Municipio
        public int IdMunicipio { get; set; }
        public string NombreMunicipio { get; set; } = null!;

        //Estado
        public int IdEstado { get; set; }
        public string NombreEstado { get; set; } = null!;
    }
}