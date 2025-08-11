using System;
using System.Collections.Generic;

namespace proyecto_auditoria_seguridad.Models;

public partial class UbicacionesInstitucionales
{
    public int idUbicacionInstitucional { get; set; }

    public int? idFacultad { get; set; }

    public int idDepartamento { get; set; }

    public int idDireccion { get; set; }

    public virtual ICollection<Encuestas> Encuestas { get; set; } = new List<Encuestas>();

    public virtual Departamentos idDepartamentoNavigation { get; set; } = null!;

    public virtual Direcciones idDireccionNavigation { get; set; } = null!;

    public virtual Facultades? idFacultadNavigation { get; set; }
}
