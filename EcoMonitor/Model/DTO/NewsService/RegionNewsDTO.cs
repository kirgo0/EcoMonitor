namespace EcoMonitor.Model.DTO.NewsService
{
    public class RegionNewsDTO
    {
        public string region_name { get; set; }
        public List<NarrowNewsDTO> news { get; set; }
    }
}
