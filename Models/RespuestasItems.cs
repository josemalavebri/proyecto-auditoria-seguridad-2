using System;
using System.Collections.Generic;

namespace proyecto_auditoria_seguridad.Models;

public partial class RespuestasItems
{
    public int idRespuestaItem { get; set; }

    public int idPreguntaItem { get; set; }

    public int idAuditoria { get; set; }

    public decimal? porcentajeCumplimiento { get; set; }

    public bool? respuestaBinaria { get; set; }

    public string? comentario { get; set; }

    public DateTime fechaRespuesta { get; set; }

    public virtual Auditorias idAuditoriaNavigation { get; set; } = null!;

    public virtual PreguntasItems idPreguntaItemNavigation { get; set; } = null!;
}
