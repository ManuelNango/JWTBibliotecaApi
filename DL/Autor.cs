using System;
using System.Collections.Generic;

namespace DL;

public partial class Autor
{
    public int IdAutor { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Medio> IdMedios { get; set; } = new List<Medio>();
}
