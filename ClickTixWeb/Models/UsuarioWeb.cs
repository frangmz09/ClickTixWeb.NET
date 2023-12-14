using System;
using System.Collections.Generic;

namespace ClickTixWeb.Models;

public partial class UsuarioWeb
{
    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Pass { get; set; }

    public string? Email { get; set; }

    public DateOnly? Fnac { get; set; }

    public string? Genero { get; set; }

    public string? Celular { get; set; }

    public int? SucursalHabitual { get; set; }

    public int IdUsuario { get; set; }

    public virtual Sucursal? SucursalHabitualNavigation { get; set; }

    public virtual ICollection<Ticket> Tickets { get; } = new List<Ticket>();
}
