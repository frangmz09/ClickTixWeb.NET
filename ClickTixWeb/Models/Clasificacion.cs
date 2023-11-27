using System;
using System.Collections.Generic;

namespace ClickTixWeb.Models;

public partial class Clasificacion
{
    public int Id { get; set; }

    public string? Clasificacion1 { get; set; }

    public virtual ICollection<Pelicula> Peliculas { get; } = new List<Pelicula>();
}
