using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Experience;
using api.Models;

namespace api.Dtos.Person
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string Embg { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        //Experiences        
        public List<ExperienceDto>? Experiences { get; set; }
    }
}