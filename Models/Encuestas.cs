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

    public virtual ICollection<EjecucionesEncuesta> EjecucionesEncuesta { get; set; } = new List<EjecucionesEncuesta>();

    public virtual ICollection<Encuestas> InverseidEncuestaAnteriorNavigation { get; set; } = new List<Encuestas>();

    public virtual ICollection<ItemsEncuesta> ItemsEncuesta { get; set; } = new List<ItemsEncuesta>();

    public virtual ICollection<PreguntasEncuesta> PreguntasEncuesta { get; set; } = new List<PreguntasEncuesta>();

    public virtual Encuestas? idEncuestaAnteriorNavigation { get; set; }

    public virtual UbicacionesInstitucionales idUbicacionInstitucionalNavigation { get; set; } = null!;
}
