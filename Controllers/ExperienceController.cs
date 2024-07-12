using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Experience;
using api.Helpers;
using api.Interfaces;
using api.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/experience")]
    [ApiController]
    public class ExperienceController : ControllerBase
    {
        private readonly IExperienceRepository _experienceRepo;
        private readonly IPersonRepository _personRepo;

        public ExperienceController(IExperienceRepository experienceRepo, IPersonRepository personRepo)
        {
            _experienceRepo = experienceRepo;
            _personRepo = personRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var experiences = await _experienceRepo.GetAllAsync();
            
            var experienceDto = experiences.Select(s => s.ToExperienceDto());

            return Ok(experienceDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var experience = await _experienceRepo.GetByIdAsync(id);

            if (experience == null)
            {
                return NotFound();
            }

            return Ok(experience.ToExperienceDto());
        }

        [HttpPost("{personId:int}")]
        public async Task<IActionResult> Create([FromRoute] int personId, CreateExperienceDto experienceDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!await _personRepo.PersonExists(personId)){
                return BadRequest("Person does not exist");
            }

            var experienceModel = experienceDto.ToExperienceFromCreate(personId);
            await _experienceRepo.CreateAsync(experienceModel);

            return CreatedAtAction(nameof(GetById), new {id = experienceModel.Id}, experienceModel.ToExperienceDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateExperienceRequestDto experienceDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var experience = await _experienceRepo.UpdateAsync(id, experienceDto.ToExperienceFromUpdate());
            Console.WriteLine("Experience Company=", experience!.CompanyName);
            if (experience == null)
            {
                return NotFound("Experience not found.");
            }

            return Ok(experience.ToExperienceDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var experienceModel = await _experienceRepo.DeleteAsync(id);
            if(experienceModel == null)
            {
                return NotFound("Experience does not exist");
            }

            return Ok(experienceModel);
        }

        [HttpGet("personId/{id:int}")]
        public async Task<IActionResult> GetAllByPersonId([FromRoute] int id)
        {
             if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var experiences = await _experienceRepo.GetAllByPersonIdAsync(id);
            if(experiences == null)
            {
                return NotFound($"No experiences for user with id= {id}");
            }

            return Ok(experiences);
        }

        [HttpGet("valid-interval/{id:int}")]
        public async Task<IActionResult> IsTimeIntervalFree([FromRoute] int id, [FromQuery] QueryObject query)
        {
            return Ok(await _experienceRepo.IsTimeIntervalFreeAsync(id, query.StartDate, query.EndDate, query.excludeId));
        }
    }
}