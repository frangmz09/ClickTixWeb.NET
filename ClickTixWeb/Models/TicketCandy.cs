using System;
using System.Collections.Generic;

namespace ClickTixWeb.Models;

public partial class TicketCandy
{
    public int Id { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual ICollection<Qr> Qrs { get; } = new List<Qr>();

    public virtual ICollection<ProductoCandy> IdProductos { get; } = new List<ProductoCandy>();
}
