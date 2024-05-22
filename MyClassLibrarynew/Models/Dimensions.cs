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
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            var RegoStatus = false;
            if (!string.IsNullOrEmpty(name))
            {
               
                    errors.Add(new ValidationResult("Name is required for Dimensions"));
                

            }
            if (!string.IsNullOrEmpty(email))
            {

                errors.Add(new ValidationResult("email is required for Dimensions"));


            }
            if (!string.IsNullOrEmpty(dimensionfield))
            {

                errors.Add(new ValidationResult("Dimension field is required for Dimensions"));


            }
            if (!string.IsNullOrEmpty(email))
            {
                if (!email.Contains('@'))
                {
                    errors.Add(new ValidationResult("Email is invalid"));
                }
               
            }
            return errors;
        }
        }
}
