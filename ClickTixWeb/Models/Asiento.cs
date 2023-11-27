using System;
using System.Collections.Generic;

namespace ClickTixWeb.Models;

public partial class Asiento
{
    public int Id { get; set; }

    public int? Fila { get; set; }

    public sbyte? Disponible { get; set; }

    public int? Columna { get; set; }

    public int IdFuncion { get; set; }

    public virtual Funcion IdFuncionNavigation { get; set; } = null!;
}
