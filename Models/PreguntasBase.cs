using System;
using System.Collections.Generic;

namespace proyecto_auditoria_seguridad.Models;

public partial class PreguntasBase
{
    public int idPreguntaBase { get; set; }

    public string descripcion { get; set; } = null!;

    public int version { get; set; }

    public DateTime fechaCreacion { get; set; }

    public bool activo { get; set; }

    public virtual ICollection<PreguntasEncuesta> PreguntasEncuesta { get; set; } = new List<PreguntasEncuesta>();
}
