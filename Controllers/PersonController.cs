using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Person;
using api.Exporters;
using api.Helpers;
using api.Interfaces;
using api.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IPersonRepository _personRepo;

        private readonly CsvFileHandler _csvFileHandler;
        private readonly PdfFileHandler _pdfFileHandler;
        private readonly XlxsFileHandler _xlxsFileHandler;
        public PersonController(ApplicationDBContext context, 
                                IPersonRepository personRepo, 
                                CsvFileHandler csvFileHandler,
                                PdfFileHandler pdfFileHandler,
                                XlxsFileHandler xlxsFileHandler)
        {
            _personRepo = personRepo;
            _context = context;
            _csvFileHandler = csvFileHandler;
            _pdfFileHandler =  pdfFileHandler;
            _xlxsFileHandler = xlxsFileHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var (people, totalRows) = await _personRepo.GetAllAsync(query);

            var personDetail = people.Select(s => s.ToPersonDto());

            var response = new{
                People = people,
                TotalRows = totalRows,
            };

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var person = await _personRepo.GetByIdAsync(id);

            if(person == null)
            {
                return NotFound();
            }
            
            return Ok(person.ToPersonDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePersonRequestDto personDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var personModel = personDto.ToPersonFromCreateDTO();

            if(await _personRepo.EmbgAlreadyExistsAsync(personDto.Embg))
            {
                return BadRequest("EMBG already exists");
            }
            await _personRepo.CreateAsync(personModel);
            return CreatedAtAction(nameof(GetById), new { id = personModel.Id }, personModel.ToPersonDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePersonRequestDto updateDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!await _personRepo.PersonExists(id))
                return NotFound("Person does not exist");

            if(await _personRepo.EmbgAlreadyExistsAsync(updateDto.Embg, id))
                return BadRequest("EMBG already exists");

            var personModel = await _personRepo.UpdateAsync(id, updateDto);

            return Ok(personModel!.ToPersonDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var personModel = await _personRepo.DeleteAsync(id);
            
            if(personModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }


        [HttpGet("count")]
        public async Task<ActionResult<int>> CountPeople()
        {
            var count = await _context.Person.CountAsync();
            return Ok(count);
        }

        [HttpGet("download-csv")]
        public async Task<IActionResult> GetCsvFile([FromQuery] QueryObject query)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
                
            string[] columns = ["Embg", "Name", "LastName", "Address"];  

            var (people, totalRows) = await _personRepo.GetAllAsync(query);

            var personDetail = people.Select(s => s.ToPersonDto());
            
            byte[] fileBytes = _csvFileHandler.ExportCSV(columns, people);
            return File(fileBytes, "text/csv", "all_people.csv");
        }

        [HttpGet("download-pdf")]
        public async Task<IActionResult> GetPdfFile([FromQuery] QueryObject query)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
                
            string[] columns = ["Embg", "Name", "LastName", "Address"]; 
            var (people, totalRows) = await _personRepo.GetAllAsync(query);

            var personDetail = people.Select(s => s.ToPersonDto());

            byte[] fileBytes = _pdfFileHandler.ExportPDF(columns, people);
            return File(fileBytes, "application/pdf", "all_people.pdf");
        }

        [HttpGet("download-xlxs")]
        public async Task<IActionResult> GetXlxsFile([FromQuery] QueryObject query)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
                
            string[] columns = ["Embg", "Name", "LastName", "Address"]; 
            var (people, totalRows) = await _personRepo.GetAllAsync(query);

            var personDetail = people.Select(s => s.ToPersonDto());

            byte[] fileBytes = _xlxsFileHandler.ExportExcel(columns, people);
            return File(fileBytes,  "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "all_people.xlsx");
        }
    }
}