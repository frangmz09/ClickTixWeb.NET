using System;
using System.Collections.Generic;

namespace ClickTixWeb.Models;

public partial class Qr
{
    public int Id { get; set; }

    public int IdTicket { get; set; }

    public int IdTicketcandy { get; set; }

    public string? Codigo { get; set; }

    public virtual Ticket IdTicketNavigation { get; set; } = null!;

    public virtual TicketCandy IdTicketcandyNavigation { get; set; } = null!;
}
