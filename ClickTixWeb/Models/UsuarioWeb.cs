using System;
using System.Collections.Generic;

namespace ClickTixWeb.Models;

public partial class UsuarioWeb
{
    public int IdUsuario { get; set; }

    public string? nombre { get; set; }

    public string? apellido { get; set; }

    public string? pass { get; set; }

    public string? email { get; set; }

    public DateOnly fnac { get; set; }

    public string? genero { get; set; }


    public string? celular { get; set; }

    public int sucursal_habitual { get; set; }

  
}
