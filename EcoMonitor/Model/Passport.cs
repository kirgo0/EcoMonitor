using MySql.Data.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMonitor.Model
{
    public class Passport
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(45)]
        public string name { get; set; }
        [Required]

        [Column(TypeName = "YEAR(4)")]
        public int year { get; set; }
        public string data2 { get; set; }
        public string data3 { get; set; }
        public string data4 { get; set; }
        public string data5 { get; set; }

        [Required]
        [ForeignKey("Company")]
        public int company_id { get; set; }
        public Company Company { get; set; }
    }
}
