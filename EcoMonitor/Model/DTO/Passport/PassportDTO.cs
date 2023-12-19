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

        public double? source_operating_time { get; set; }

        [Required]
        public int company_id { get; set; }
    }
}
