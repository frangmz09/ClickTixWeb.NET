namespace ClickTixWeb.Models
{
    internal class TusTicketsViewModel
    {
        public List<FuncionStrings> funcionesStrings;

        public FuncionStrings funcionStrings {  get; set; }
        //public List<Ticket> Tickets { get; set; }
       // public List<Funcion> Funciones { get; set; }
        //public List<Pelicula> Peliculas { get; set; }



        public TusTicketsViewModel(List<FuncionStrings> funcionesStrings)
        {
            this.funcionesStrings = funcionesStrings;
        }
    }
}