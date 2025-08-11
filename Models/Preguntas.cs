using System;
using System.Collections.Generic;

namespace proyecto_auditoria_seguridad.Models;

public partial class Preguntas
{
    public int idPregunta { get; set; }

    public int idEncuesta { get; set; }

    public string descripcion { get; set; } = null!;

    public virtual ICollection<PreguntasItems> PreguntasItems { get; set; } = new List<PreguntasItems>();

    public virtual Encuestas idEncuestaNavigation { get; set; } = null!;
}
