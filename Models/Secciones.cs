using System;
using System.Collections.Generic;

namespace proyecto_auditoria_seguridad.Models;

public partial class Secciones
{
    public int idSeccion { get; set; }

    public string descripcion { get; set; } = null!;

    public virtual ICollection<PreguntasEncuesta> PreguntasEncuesta { get; set; } = new List<PreguntasEncuesta>();
}
