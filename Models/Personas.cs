using System;
using System.Collections.Generic;

namespace proyecto_auditoria_seguridad.Models;

public partial class Personas
{
    public int idPersona { get; set; }

    public string nombre { get; set; } = null!;

    public string apellido { get; set; } = null!;

    public int idRol { get; set; }

    public virtual ICollection<Auditorias> AuditoriasidAuditorNavigation { get; set; } = new List<Auditorias>();

    public virtual ICollection<Auditorias> AuditoriasidPersonaAuditadaNavigation { get; set; } = new List<Auditorias>();

    public virtual Roles idRolNavigation { get; set; } = null!;
}
