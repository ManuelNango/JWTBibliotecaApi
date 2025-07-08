using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class MedioGetAllSP
    {
        public int IdMedio {  get; set; }
        public string Titulo { get; set; } = null!;
        public string Autor { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string FechaPublicacion { get; set; } = null!;
        public byte[]? ArchivoPDF { get; set; }
        public byte[]? ImagenPortada { get; set; }
        public string TipoMedio { get; set; } = null!;
        public string Editorial { get; set; } = null!;
        public string Area { get; set; } = null!;
        public string Idioma { get; set; } = null!;

    }
}
