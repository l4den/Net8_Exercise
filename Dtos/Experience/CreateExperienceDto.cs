using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Experience
{
    public class CreateExperienceDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "Company name must be at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Company name can't be more than 50 characters")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [MinLength(2, ErrorMessage = "Position must be at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Position can't be more than 50 characters")]
        public string Position { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}