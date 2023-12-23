namespace EcoMonitor.Model.DTO.NewsService
{
    public class NewsFilterDTO
    {
        public int? page { get; set; }
        public int? count { get; set; }
        public bool? byRelevance { get; set; }
        public bool? newerToOlder { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public string source_url { get; set; }
        public List<int> region_ids { get; set; }
        public List<string> author_ids { get; set; }
        public List<int> company_ids { get; set; }
    }
}
