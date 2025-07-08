using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class MedioGetByIdSP
    {
        public int IdMedio { get; set; }
        public string Titulo { get; set; } = null!;
        public int IdAutor { get; set; }
        public string Autor { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string FechaPublicacion { get; set; } = null!;
        public byte[]? ArchivoPDF { get; set; }
        public byte[]? ImagenPortada { get; set; }
        public int IdTipoMedio { get; set; }
        public string TipoMedio { get; set; } = null!;
        public int IdEditorial { get; set; }
        public string Editorial { get; set; } = null!;
        public int IdArea { get; set; }
        public string Area { get; set; } = null!;
        public int IdIdioma { get; set; }
        public string Idioma { get; set; } = null!;
    }
}
