namespace EcoMonitor.Model.DTO
{
    public class FormattedNewsDTO
    {
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public DateTime post_date { get; set; }
        public DateTime? update_date { get; set; }
        public string authors { get; set; }
        public string? region_names { get; set; }
        public string? company_names { get; set; }
        public int likes { get; set; }
    }
}
