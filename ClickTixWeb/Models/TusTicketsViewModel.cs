namespace ClickTixWeb.Models
{
    internal class TusTicketsViewModel
    {
        public List<FuncionStrings> funcionesStrings { get; set; }

        public TusTicketsViewModel(List<FuncionStrings> funcionesStrings)
        {
            this.funcionesStrings = funcionesStrings;
        }
    }
}