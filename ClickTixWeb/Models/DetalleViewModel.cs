namespace ClickTixWeb.Models
{
    public class DetalleViewModel
    {
        public Pelicula Pelicula { get; set; }
        public List<Funcion> ProximasFunciones { get; set; }

        public List<FuncionStrings> ProximasFuncionesStrings {  get; set; }

        public List<DateOnly> FechasUnicas { get; set; }

        public int? sucursalId { get; set; }
        public string? sucursalNombre { get; set; }
        public List<Sucursal> Sucursales { get; set; }


    }
}
