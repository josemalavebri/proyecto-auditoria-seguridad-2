using System;
using System.Collections.Generic;

namespace proyecto_auditoria_seguridad.Models;

public partial class Items
{
    public int idItem { get; set; }

    public string titulo { get; set; } = null!;

    public string codigo { get; set; } = null!;

    public string descripcion { get; set; } = null!;

    public int version { get; set; }

    public bool activo { get; set; }

    public virtual ICollection<ItemsEncuesta> ItemsEncuesta { get; set; } = new List<ItemsEncuesta>();

    public virtual ICollection<PreguntasEncuestaItems> PreguntasEncuestaItems { get; set; } = new List<PreguntasEncuestaItems>();
}
