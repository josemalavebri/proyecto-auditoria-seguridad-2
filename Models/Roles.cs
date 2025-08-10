using System;
using System.Collections.Generic;

namespace proyecto_auditoria_seguridad.Models;

public partial class Roles
{
    public int idRol { get; set; }

    public string nombre { get; set; } = null!;

    public virtual ICollection<Personas> Personas { get; set; } = new List<Personas>();
}
