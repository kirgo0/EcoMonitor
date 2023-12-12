using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model
{
    public class User : IdentityUser
    {
        public virtual List<News> news { get; set; }
    }
}
