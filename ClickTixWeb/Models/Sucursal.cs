using System;
using System.Collections.Generic;

namespace ClickTixWeb.Models;

public partial class Sucursal
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Cuit { get; set; }

    public string? Direccion { get; set; }

    public virtual ICollection<Sala> Salas { get; } = new List<Sala>();

    public virtual ICollection<UsuarioSistema> UsuarioSistemas { get; } = new List<UsuarioSistema>();
}
