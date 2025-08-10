using System;
using System.Collections.Generic;

namespace proyecto_auditoria_seguridad.Models;

public partial class Facultades
{
    public int idFacultad { get; set; }

    public string? nombre { get; set; }

    public virtual ICollection<UbicacionesInstitucionales> UbicacionesInstitucionales { get; set; } = new List<UbicacionesInstitucionales>();
}
