using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Person;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IPersonRepository
    {
        Task<(List<Person>, int)> GetAllAsync(QueryObject query);
        Task<Person?> GetByIdAsync(int id);
        Task<Person> CreateAsync(Person personModel);
        Task<Person?> UpdateAsync(int id, UpdatePersonRequestDto personDto);
        Task<Person?> DeleteAsync(int id);
        Task<bool> PersonExists(int id);
        Task<bool> EmbgExistsAsync(string embg);
    }
}