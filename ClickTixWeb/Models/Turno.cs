using System;
using System.Collections.Generic;

namespace ClickTixWeb.Models;

public partial class Turno
{
    public int Id { get; set; }

    public TimeOnly? Hora { get; set; }

    public virtual ICollection<Funcion> Funcions { get; } = new List<Funcion>();
}
