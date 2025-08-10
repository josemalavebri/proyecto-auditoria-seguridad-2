using System;
using System.Collections.Generic;

namespace proyecto_auditoria_seguridad.Models;

public partial class Departamentos
{
    public int idDepartamento { get; set; }

    public string nombre { get; set; } = null!;

    public virtual ICollection<UbicacionesInstitucionales> UbicacionesInstitucionales { get; set; } = new List<UbicacionesInstitucionales>();
}
