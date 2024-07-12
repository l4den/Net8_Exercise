using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IExperienceRepository
    {
        Task<List<Experience>> GetAllAsync();
        Task<Experience?> GetByIdAsync(int id);

        Task<Experience> CreateAsync(Experience experienceModel);

        Task<Experience?> UpdateAsync(int id, Experience experienceModel);

        Task<Experience?> DeleteAsync(int id);

        Task<List<Experience>?> GetAllByPersonIdAsync(int id);

        Task<bool> IsTimeIntervalFreeAsync(int Id, DateTime startDate, DateTime endDate, int? excludeId);
    }
}