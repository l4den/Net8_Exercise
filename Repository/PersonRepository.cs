using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Person;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDBContext _context;
        public PersonRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Person> CreateAsync(Person personModel)
        {
            await _context.Person.AddAsync(personModel);
            await _context.SaveChangesAsync();
            return personModel;
        }

        public async Task<Person?> DeleteAsync(int id)
        {
            var personModel = await _context.Person.FirstOrDefaultAsync(x => x.Id == id);

            if (personModel == null)
            {
                return null;
            }

            _context.Person.Remove(personModel);
            await _context.SaveChangesAsync();
            return personModel;
        }

        public async Task<bool> EmbgAlreadyExistsAsync(string embg, int? excludeId = null)
        {
            return await _context.Person.AnyAsync(p => p.Embg == embg && excludeId != p.Id);
        }

        public async Task<(List<Person>, int)> GetAllAsync(QueryObject query)
        {
            var people = _context.Person.Include(c => c.Experiences).AsQueryable();

            // FILTERING
            if(!string.IsNullOrWhiteSpace(query.SearchWord))
            {
                people = people.Where(x =>
                        EF.Functions.Like(x.Embg, $"%{query.SearchWord}%") ||
                        EF.Functions.Like(x.Name, $"%{query.SearchWord}%") ||
                        EF.Functions.Like(x.LastName, $"%{query.SearchWord}%") ||
                        EF.Functions.Like(x.Address, $"%{query.SearchWord}%"));
            
            }
            int totalRows = await people.CountAsync();

            // SORTING
            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("Embg", StringComparison.OrdinalIgnoreCase))
                {
                    people = query.IsDescending ? people.OrderByDescending(s => s.Embg) : people.OrderBy(s => s.Embg);
                }

                if(query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    people = query.IsDescending ? people.OrderByDescending(s => s.Name) : people.OrderBy(s => s.Name);
                }

                if(query.SortBy.Equals("LastName", StringComparison.OrdinalIgnoreCase))
                {
                    people = query.IsDescending ? people.OrderByDescending(s => s.LastName) : people.OrderBy(s => s.LastName);
                }

                if(query.SortBy.Equals("Address", StringComparison.OrdinalIgnoreCase))
                {
                    people = query.IsDescending ? people.OrderByDescending(s => s.Address) : people.OrderBy(s => s.Address);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            List<Person> peopleList = await people.Skip(skipNumber).Take(query.PageSize).ToListAsync();

            return (peopleList, totalRows);
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            return await _context.Person.Include(c => c.Experiences).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> PersonExists(int id)
        {
            return await _context.Person.AnyAsync(s => s.Id == id);
        }

        public async Task<Person?> UpdateAsync(int id, UpdatePersonRequestDto personDto)
        {
            var exisingPerson = await _context.Person.FirstOrDefaultAsync(x => x.Id == id);

            if(exisingPerson == null)
            {
                return null;
            }

            exisingPerson.Embg = personDto.Embg;
            exisingPerson.Name = personDto.Name;
            exisingPerson.LastName = personDto.LastName;
            exisingPerson.Address = personDto.Address;

            await _context.SaveChangesAsync();

            return exisingPerson;
        }
    }
}