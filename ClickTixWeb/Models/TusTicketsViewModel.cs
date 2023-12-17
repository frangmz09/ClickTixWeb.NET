namespace ClickTixWeb.Models
{
    internal class TusTicketsViewModel
    {
        public List<FuncionStrings> funcionesStrings { get; set; }

        public List<Ticket> tickets { get; set; }

        public TusTicketsViewModel(List<FuncionStrings> funcionesStrings, List<Ticket> tickets)
        {
            this.funcionesStrings = funcionesStrings;
            this.tickets = tickets; 
        }
    }
}