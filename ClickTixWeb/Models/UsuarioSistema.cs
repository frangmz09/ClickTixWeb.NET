using System;
using System.Collections.Generic;

namespace ClickTixWeb.Models;

public partial class UsuarioSistema
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Pass { get; set; }

    public int IdSucursal { get; set; }

    public string? Usuario { get; set; }

    public sbyte? IsAdmin { get; set; }

    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;
}
