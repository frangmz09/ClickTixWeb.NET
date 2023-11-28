using System;
using System.Collections.Generic;

namespace ClickTixWeb.Models;

public partial class UsuarioWeb
{
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Pass { get; set; }
}
