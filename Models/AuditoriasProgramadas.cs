using System;
using System.Collections.Generic;

namespace proyecto_auditoria_seguridad.Models;

public partial class AuditoriasProgramadas
{
    public int idProgramacion { get; set; }

    public int idEncuesta { get; set; }

    public DateTime fechaProgramada { get; set; }

    public virtual Encuestas idEncuestaNavigation { get; set; } = null!;
}
