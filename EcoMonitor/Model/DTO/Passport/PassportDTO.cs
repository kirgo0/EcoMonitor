using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.Passport
{
    public class PassportDTO
    {

        [Required]
        public int id { get; set; }

        [Required]
        public int year { get; set; }
        public string data2 { get; set; }
        public string data3 { get; set; }
        public string data4 { get; set; }
        public string data5 { get; set; }

        [Required]
        public int company_id { get; set; }
    }
}
