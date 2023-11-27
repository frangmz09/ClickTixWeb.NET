using System;
using System.Collections.Generic;

namespace ClickTixWeb.Models;

public partial class Sala
{
    public int Id { get; set; }

    public int IdSucursal { get; set; }

    public int? Filas { get; set; }

    public int? Columnas { get; set; }

    public int? Capacidad { get; set; }

    public int? NroSala { get; set; }

    public virtual ICollection<Funcion> Funcions { get; } = new List<Funcion>();

    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;
}
