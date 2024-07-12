using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ExperienceRepository : IExperienceRepository
    {
        private readonly ApplicationDBContext _context;
        public ExperienceRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Experience> CreateAsync(Experience experienceModel)
        {
            await _context.Experiences.AddAsync(experienceModel);
            await _context.SaveChangesAsync();
            return experienceModel;
        }

        public async Task<Experience?> DeleteAsync(int id)
        {
            var experienceModel = await _context.Experiences.FirstOrDefaultAsync(x => x.Id == id);
            if (experienceModel == null)
            {
                return null;
            }

            _context.Experiences.Remove(experienceModel);
            await _context.SaveChangesAsync();
            return experienceModel;
        }

        public async Task<List<Experience>> GetAllAsync()
        {
            return await _context.Experiences.ToListAsync();
        }

        public async Task<List<Experience>?> GetAllByPersonIdAsync(int id)
        {
            return await _context.Experiences
                            .Where(x => x.PersonId == id)
                            .OrderBy(x => x.StartDate)
                            .ToListAsync();
        }


        public async Task<Experience?> GetByIdAsync(int id)
        {
            return await _context.Experiences.FindAsync(id);
        }

        public async Task<bool> IsTimeIntervalFreeAsync(int Id, DateTime checkStart, DateTime checkEnd, int? excludeId)
        {
            var experiences = await GetAllByPersonIdAsync(Id);
            
            // Does the user have existing experiences?
            if(experiences != null || experiences?.Count > 0)
            {
                // Is the user trying to edit an existing experience?
                if(excludeId != null)
                {
                    experiences = experiences.Where(x => x.Id != excludeId).ToList();
                }

                foreach(var exp in experiences)
                {
                    if ((checkStart <= exp.StartDate && exp.StartDate <= checkEnd) ||
                        (checkStart <= exp.EndDate && exp.EndDate <= checkEnd) ||
                        (exp.StartDate <= checkStart && checkEnd <= exp.EndDate) ||
                        (checkStart <= exp.StartDate && exp.EndDate <= checkEnd))
                        {
                            return false;
                        }
                }
            }
            return true;
        }

        public async Task<Experience?> UpdateAsync(int id, Experience experienceModel)
        {
            var existingExperence = await _context.Experiences.FindAsync(id);

            if (existingExperence == null){
                return null;
            }

            existingExperence.CompanyName = experienceModel.CompanyName;
            existingExperence.Position = experienceModel.Position;
            existingExperence.StartDate = experienceModel.StartDate;
            existingExperence.EndDate = experienceModel.EndDate;

            await _context.SaveChangesAsync();
            return existingExperence;
        }
    }
}