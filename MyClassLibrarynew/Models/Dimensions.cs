using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace MyClassLibrarynew.Models
{
    public class Dimensions
    {
        [Key]
        public int? Id { get; set; }

       
        public string? name { get; set; }

        public string? email { get; set; }
        public string? dimensionfield { get; set; }
    }
}
