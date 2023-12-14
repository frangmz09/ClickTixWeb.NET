using System;
using System.Collections.Generic;

namespace ClickTixWeb.Models;

public partial class Funcion
{
    public int Id { get; set; }

    public DateOnly? Fecha { get; set; }

    public int IdDimension { get; set; }

    public int IdPelicula { get; set; }

    public int IdSala { get; set; }

    public string? IdiomaFuncion { get; set; }

    public int TurnoId { get; set; }

    public virtual ICollection<Asiento> Asientos { get; } = new List<Asiento>();

    public virtual Dimension IdDimensionNavigation { get; set; } = null!;

    public virtual Pelicula IdPeliculaNavigation { get; set; } = null!;

    public virtual Sala IdSalaNavigation { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; } = new List<Ticket>();

    public virtual Turno Turno { get; set; } = null!;
}
