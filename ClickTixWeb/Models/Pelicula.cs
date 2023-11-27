using System;
using System.Collections.Generic;

namespace ClickTixWeb.Models;

public partial class Pelicula
{
    public int Id { get; set; }

    public string? Titulo { get; set; }

    public int IdCategoria { get; set; }

    public string? Descripcion { get; set; }

    public int IdClasificacion { get; set; }

    public string? Portada { get; set; }

    public int? Duracion { get; set; }

    public string? Director { get; set; }

    public DateOnly? FechaEstreno { get; set; }

    public sbyte? EstaActiva { get; set; }

    public virtual ICollection<Funcion> Funcions { get; } = new List<Funcion>();

    public virtual Categorium IdCategoriaNavigation { get; set; } = null!;

    public virtual Clasificacion IdClasificacionNavigation { get; set; } = null!;

    public virtual ICollection<Idioma> IdIdiomas { get; } = new List<Idioma>();
}
