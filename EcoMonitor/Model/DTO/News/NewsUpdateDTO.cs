using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.News
{
    public class NewsUpdateDTO
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string body { get; set; }
        [Required]
        public DateTime post_date { get; set; }
        public DateTime? update_date { get; set; }
        [Url]
        public string? source_url { get; set; }
        [Required]
        public string author_id { get; set; }

        public int? company_id { get; }

        public int? region_id { get; }
    }
}
