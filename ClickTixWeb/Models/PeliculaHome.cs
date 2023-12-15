namespace ClickTixWeb.Models
{
    public class PeliculaHome

    {
        public int Id { get; set; }

        public string? Titulo { get; set; }
        public string? Portada { get; set; }
        public Sucursal Sucursal{ get; set; }
    }
}
