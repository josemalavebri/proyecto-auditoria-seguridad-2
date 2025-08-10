using System;
using System.Collections.Generic;

namespace proyecto_auditoria_seguridad.Models;

public partial class EjecucionesEncuesta
{
    public int idEjecucion { get; set; }

    public int idEncuesta { get; set; }

    public DateTime fechaEjecucion { get; set; }

    public virtual ICollection<RespuestasItems> RespuestasItems { get; set; } = new List<RespuestasItems>();

    public virtual Encuestas idEncuestaNavigation { get; set; } = null!;
}
