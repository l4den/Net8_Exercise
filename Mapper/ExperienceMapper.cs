using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Experience;
using api.Models;

namespace api.Mapper
{
    public static class ExperienceMapper
    {
        public static ExperienceDto ToExperienceDto(this Experience experienceModel)
        {
            return new ExperienceDto
            {
                Id = experienceModel.Id,
                CompanyName = experienceModel.CompanyName,
                Position = experienceModel.Position,
                StartDate = experienceModel.StartDate,
                EndDate = experienceModel.EndDate,
                PersonId = experienceModel.PersonId,
            };
        }

        public static Experience ToExperienceFromCreate(this CreateExperienceDto experienceDto, int personId)
        {
            return new Experience
            {
                CompanyName = experienceDto.CompanyName,
                Position = experienceDto.Position,
                StartDate = experienceDto.StartDate,
                EndDate = experienceDto.EndDate,
                PersonId = personId,
            };
        }

        public static Experience ToExperienceFromUpdate(this UpdateExperienceRequestDto experienceDto)
        {
            return new Experience
            {
                CompanyName = experienceDto.CompanyName,
                Position = experienceDto.Position,
                StartDate = experienceDto.StartDate,
                EndDate = experienceDto.EndDate,
                //PersonId = personId,
            };
        }


    }
}