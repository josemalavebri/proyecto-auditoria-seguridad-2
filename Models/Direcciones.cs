using System;
using System.Collections.Generic;

namespace proyecto_auditoria_seguridad.Models;

public partial class Direcciones
{
    public int idDireccion { get; set; }

    public string nombre { get; set; } = null!;

    public virtual ICollection<UbicacionesInstitucionales> UbicacionesInstitucionales { get; set; } = new List<UbicacionesInstitucionales>();
}
