using System;
using System.Collections.Generic;

namespace ClickTixWeb.Models;

public partial class Categorium
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Pelicula> Peliculas { get; } = new List<Pelicula>();
}
