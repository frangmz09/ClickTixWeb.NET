namespace ClickTixWeb.Models
{
    public class SeleccionButacasViewModel
    {

       
            public Funcion Funcion { get; set; }
            public int Filas { get; set; }
            public int Columnas { get; set; }
            public List<Asiento> Asientos { get; set; }
        
            public FuncionStrings FuncionStrings { get; set; }
    }
}
