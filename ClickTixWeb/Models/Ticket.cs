using System;
using System.Collections.Generic;

namespace ClickTixWeb.Models;

public partial class Ticket
{
    public int Id { get; set; }

    public int IdFuncion { get; set; }

    public DateTime? Fecha { get; set; }

    public int? Fila { get; set; }

    public int? Columna { get; set; }

    public double? PrecioAlMomento { get; set; }

    public string? UidFb { get; set; }

    public string? IdLabel { get; set; }

    public bool? IsWithdrawn { get; set; }

    public virtual Funcion IdFuncionNavigation { get; set; } = null!;
}
