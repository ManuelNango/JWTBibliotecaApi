using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Medio
    {
        public int IdMedio { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string FechaPublicacion { get; set; } = null!;
        public byte[]? ArchivoPDF { get; set; }
        public byte[]? ImagenPortada { get; set; }

        //Propiedades de navegación
        public ML.Autor? Autor { get; set; }
        public ML.TipoMedio? TipoMedio { get; set; }
        public ML.Editorial? Editorial { get; set; }
        public ML.Area? Area { get; set; }
        public ML.Idioma? Idioma { get; set; }

        //Lista de Medios
        public List<object>? Medios { get; set; }
    }
}
