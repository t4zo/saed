namespace SAED.Web.Areas.Administrador.ViewModels
{
    public class TableHeader
    {
        public string Header { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string CurrentFilter { get; set; }
        public bool CantAdd { get; set; }
    }
}
