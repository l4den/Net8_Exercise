using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Person;
using api.Models;

namespace api.Mapper
{
    public static class PersonMappers
    {
        public static PersonDto ToPersonDto(this Person personModel)
        {
            return new PersonDto
            {
                Id = personModel.Id,
                Embg = personModel.Embg,
                Name = personModel.Name,
                LastName = personModel.LastName,
                Address = personModel.Address,
                Experiences = personModel.Experiences.Select(x => x.ToExperienceDto()).ToList(),
            };
        }

        public static Person ToPersonFromCreateDTO(this CreatePersonRequestDto personDto)
        {
            return new Person
            {
                Embg = personDto.Embg,
                Name = personDto.Name,
                LastName = personDto.LastName,
                Address = personDto.Address,
            };
        }
    }
}