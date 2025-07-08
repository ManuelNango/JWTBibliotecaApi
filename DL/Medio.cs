using System;
using System.Collections.Generic;

namespace DL;

public partial class Medio
{
    public int IdMedio { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateOnly FechaPublicacion { get; set; }

    public byte[]? ArchivoPdf { get; set; }

    public byte[]? ImagenPortada { get; set; }

    public int? IdTipoMedio { get; set; }

    public int? IdEditorial { get; set; }

    public int? IdArea { get; set; }

    public int? IdIdioma { get; set; }

    public virtual Area? IdAreaNavigation { get; set; }

    public virtual Editorial? IdEditorialNavigation { get; set; }

    public virtual Idioma? IdIdiomaNavigation { get; set; }

    public virtual TipoMedio? IdTipoMedioNavigation { get; set; }

    public virtual ICollection<Autor> IdAutors { get; set; } = new List<Autor>();
}
