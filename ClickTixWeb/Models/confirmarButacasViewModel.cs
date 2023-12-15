namespace ClickTixWeb.Models
{
    public class confirmarButacasViewModel
    {
        public string portadaDePelicula {  get; set; }
        public Funcion Funcion { get; set; }
        public List<int> AsientosId { get; set; }
        public List<int> AsientosFilas { get; set; }

        public List<Asiento> Asientos { get; set; }
        public List<int> AsientosColumnas { get; set; }

        public FuncionStrings FuncionStrings { get; set; }
     
    }
}
