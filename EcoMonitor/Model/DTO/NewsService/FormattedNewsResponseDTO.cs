namespace EcoMonitor.Model.DTO.NewsService
{
    public class FormattedNewsResponseDTO
    {
        public int remainingRowsCount { get; set; }
        public bool isItEnd { get; set; }
        public List<FormattedNewsDTO> selectedNews { get; set; }
    }
}
