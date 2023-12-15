namespace ClickTixWeb.Models
{
    public class HomeViewModel
    {
        public List<Pelicula> Peliculas { get; set; }
        public List<Sucursal> Sucursales { get; set; }
        public int? SucursalId { get; set; }
    }
}