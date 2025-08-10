using System;
using System.Collections.Generic;

namespace proyecto_auditoria_seguridad.Models;

public partial class PreguntasEncuesta
{
    public int idPreguntaEncuesta { get; set; }

    public int idEncuesta { get; set; }

    public int idPreguntaBase { get; set; }

    public int idSeccion { get; set; }

    public string? descripcion { get; set; }

    public virtual ICollection<PreguntasEncuestaItems> PreguntasEncuestaItems { get; set; } = new List<PreguntasEncuestaItems>();

    public virtual Encuestas idEncuestaNavigation { get; set; } = null!;

    public virtual PreguntasBase idPreguntaBaseNavigation { get; set; } = null!;

    public virtual Secciones idSeccionNavigation { get; set; } = null!;
}
