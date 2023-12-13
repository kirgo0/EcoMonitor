using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.News
{
    public class NewsCreateDTO
    {
        [Required]
        public string title { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public int author_id { get; set; }

        public int? company_id { get; }

        public int? region_id { get; }
    }
}
