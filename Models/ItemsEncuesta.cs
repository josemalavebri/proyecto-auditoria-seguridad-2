using System;
using System.Collections.Generic;

namespace proyecto_auditoria_seguridad.Models;

public partial class ItemsEncuesta
{
    public int idItemsEncuesta { get; set; }

    public int idItem { get; set; }

    public int idEncuesta { get; set; }

    public decimal porcentajeCumplimiento { get; set; }

    public virtual Encuestas idEncuestaNavigation { get; set; } = null!;

    public virtual Items idItemNavigation { get; set; } = null!;
}
