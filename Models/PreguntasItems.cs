using System;
using System.Collections.Generic;

namespace proyecto_auditoria_seguridad.Models;

public partial class PreguntasItems
{
    public int idPreguntaItem { get; set; }

    public int idPregunta { get; set; }

    public int idItem { get; set; }

    public virtual ICollection<RespuestasItems> RespuestasItems { get; set; } = new List<RespuestasItems>();

    public virtual Items idItemNavigation { get; set; } = null!;

    public virtual Preguntas idPreguntaNavigation { get; set; } = null!;
}
