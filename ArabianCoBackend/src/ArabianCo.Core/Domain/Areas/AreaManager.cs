using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArabianCo.Domain.Areas
{
    internal class AreaManager : DomainService, IAreaManager
    {
        private readonly IRepository<Area> _areaRepository;
        private readonly IRepository<AreaTranslation> _areaTranslationRepository;

        public AreaManager(IRepository<Area> areaRepository,
            IRepository<AreaTranslation> areaTranslationRepository)
        {
            _areaRepository = areaRepository;
            _areaTranslationRepository = areaTranslationRepository;
        }

        public async Task<Area> GetEntityByIdAsync(int id)
        {
            var entity = await _areaRepository.GetAll()
                 .Include(c => c.Translations)
                 .Include(c => c.City).ThenInclude(c => c.Translations)
                 .Include(c => c.City).ThenInclude(c => c.Country)
                 .ThenInclude(c => c.Translations)
                 .FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                throw new EntityNotFoundException(typeof(Area), id);
            return entity;
        }

        public async Task<Area> GetLiteEntityByIdAsync(int id)
        {
            var entity = await _areaRepository.GetAsync(id);
            if (entity == null)
                throw new EntityNotFoundException(typeof(Area), id);
            return entity;
        }
        public async Task IsEntityExistAsync(int id)
        {
            var entity = await _areaRepository.GetAsync(id);
            if (entity == null)
                throw new EntityNotFoundException(typeof(Area), id);
        }

        public async Task<bool> CheckIfAreaIsExist(List<AreaTranslation> Translations)
        {
            var areas = await _areaTranslationRepository.GetAll().ToListAsync();
            foreach (var Translation in Translations)
            {
                foreach (var area in areas)
                    if (area.Name == Translation.Name && area.Language == Translation.Language)
                        return true;
            }
            return false;
        }

        public async Task<List<string>> GetAllAreasNameForAutoComplete(string inputAutoComplete)
        {
            return await _areaTranslationRepository.GetAll().Where(x =>x.Name.Contains(inputAutoComplete)).Select(x=>x.Name).ToListAsync();
        }
    }
}

