using System;
using System.Collections.Generic;

namespace ClickTixWeb.Models;

public partial class Idioma
{
    public int Id { get; set; }

    public string? Idioma1 { get; set; }

    public virtual ICollection<Pelicula> IdPeliculas { get; } = new List<Pelicula>();
}
