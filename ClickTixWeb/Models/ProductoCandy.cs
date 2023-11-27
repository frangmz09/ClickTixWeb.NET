using System;
using System.Collections.Generic;

namespace ClickTixWeb.Models;

public partial class ProductoCandy
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public double? Precio { get; set; }

    public int IdCategoria { get; set; }

    public string? Imagen { get; set; }

    public string? Descripcion { get; set; }

    public virtual CategoriaCandy IdCategoriaNavigation { get; set; } = null!;

    public virtual ICollection<TicketCandy> IdTickets { get; } = new List<TicketCandy>();
}
