using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Embg { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        
        public List<Experience> Experiences { get; set; } = new List<Experience>();
    }
}