using System;
using System.Collections.Generic;

namespace proyecto_auditoria_seguridad.Models;

public partial class Encuestas
{
    public int idEncuesta { get; set; }

    public int? idEncuestaAnterior { get; set; }

    public int idUbicacionInstitucional { get; set; }

    public DateTime fechaCreacion { get; set; }

    public string? descripcion { get; set; }

    public virtual Auditorias? Auditorias { get; set; }

    public virtual ICollection<AuditoriasProgramadas> AuditoriasProgramadas { get; set; } = new List<AuditoriasProgramadas>();

    public virtual ICollection<Encuestas> InverseidEncuestaAnteriorNavigation { get; set; } = new List<Encuestas>();

    public virtual ICollection<Preguntas> Preguntas { get; set; } = new List<Preguntas>();

    public virtual Encuestas? idEncuestaAnteriorNavigation { get; set; }

    public virtual UbicacionesInstitucionales idUbicacionInstitucionalNavigation { get; set; } = null!;
}
