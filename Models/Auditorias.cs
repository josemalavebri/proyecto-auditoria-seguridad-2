using System;
using System.Collections.Generic;

namespace proyecto_auditoria_seguridad.Models;

public partial class Auditorias
{
    public int idAuditoria { get; set; }

    public int idEncuesta { get; set; }

    public DateTime fechaAuditoria { get; set; }

    public int idPersonaAuditada { get; set; }

    public int idAuditor { get; set; }

    public virtual ICollection<RespuestasItems> RespuestasItems { get; set; } = new List<RespuestasItems>();

    public virtual Personas idAuditorNavigation { get; set; } = null!;

    public virtual Encuestas idEncuestaNavigation { get; set; } = null!;

    public virtual Personas idPersonaAuditadaNavigation { get; set; } = null!;
}
