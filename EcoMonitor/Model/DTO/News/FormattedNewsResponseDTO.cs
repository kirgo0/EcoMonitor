namespace EcoMonitor.Model.DTO.News
{
    public class FormattedNewsResponseDTO
    {
        public int remainingRowsCount { get; set; }
        public bool isItEnd { get; set; }
        public List<FormattedNewsDTO> selectedNews { get; set; }
    }
}
