using System;
using System.Collections.Generic;

namespace ClickTixWeb.Models;

public partial class CategoriaCandy
{
    public int Id { get; set; }

    public string? Categoria { get; set; }

    public virtual ICollection<ProductoCandy> ProductoCandies { get; } = new List<ProductoCandy>();
}
