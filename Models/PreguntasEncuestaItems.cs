using System;
using System.Collections.Generic;

namespace proyecto_auditoria_seguridad.Models;

public partial class PreguntasEncuestaItems
{
    public int idPreguntasEncuestaItems { get; set; }

    public int idPreguntaEncuesta { get; set; }

    public int idItem { get; set; }

    public virtual ICollection<RespuestasItems> RespuestasItems { get; set; } = new List<RespuestasItems>();

    public virtual Items idItemNavigation { get; set; } = null!;

    public virtual PreguntasEncuesta idPreguntaEncuestaNavigation { get; set; } = null!;
}
