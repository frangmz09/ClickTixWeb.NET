using System;
using System.Collections.Generic;

namespace ClickTixWeb.Models;

public partial class Dimension
{
    public int Id { get; set; }

    public string? Dimension1 { get; set; }

    public double? Precio { get; set; }

    public virtual ICollection<Funcion> Funcions { get; } = new List<Funcion>();
}
