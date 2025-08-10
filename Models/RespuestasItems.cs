using System;
using System.Collections.Generic;

namespace proyecto_auditoria_seguridad.Models;

public partial class RespuestasItems
{
    public int idRespuestaItem { get; set; }

    public int idPreguntasEncuestaItems { get; set; }

    public int idEjecucion { get; set; }

    public bool respuesta { get; set; }

    public DateTime fechaRespuesta { get; set; }

    public string? comentario { get; set; }

    public virtual EjecucionesEncuesta idEjecucionNavigation { get; set; } = null!;

    public virtual PreguntasEncuestaItems idPreguntasEncuestaItemsNavigation { get; set; } = null!;
}
