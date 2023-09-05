using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using ArabianCo.Domain.Areas;
using ArabianCo.Domain.Countries;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArabianCo.Domain.Cities
{
    //city manager
    internal class CityManager : DomainService, ICityManager
    {
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<CityTranslation> _cityTranslationRepository;


        public CityManager(IRepository<City> cityRepository,
            IRepository<Country> countryRepository,
            ICountryManager countryManager,
            IAreaManager areaManager,
            IRepository<CityTranslation> cityTranslationRepository)
        {
            _cityRepository = cityRepository;
            _cityTranslationRepository = cityTranslationRepository;
        }

        public async Task<bool> CheckIfCityIsExist(List<CityTranslation> Translations)
        {

            var cities = await _cityTranslationRepository.GetAll().ToListAsync();
            foreach (var Translation in Translations)
            {
                foreach (var city in cities)
                    if (city.Name == Translation.Name && city.Language == Translation.Language)
                        return true;
            }

            return false;
        }

        public async Task<int> GetCitiesCounts()
        {
            return await _cityRepository.GetAll().Where(x => x.IsActive).CountAsync();
        }

        public async Task<City> GetEntityByIdAsync(int id)
        {
            var entity = await _cityRepository.GetAll()
                .Include(c => c.Translations)
                .Include(c => c.Country).ThenInclude(c => c.Translations)
                .Include(c => c.Areas).ThenInclude(c => c.Translations)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                throw new EntityNotFoundException(typeof(City), id);
            return entity;
        }


        public async Task<City> GetLiteEntityByIdAsync(int id)
        {
            var entity = await _cityRepository.GetAsync(id);
            if (entity == null)
                throw new EntityNotFoundException(typeof(City), id);
            return entity;
        }

        public async Task<List<string>> GetAllCityNameForAutoComplete(string inputAutoComplete)
        {
            return await _cityTranslationRepository.GetAll().Where(x => x.Name.Contains(inputAutoComplete)).Select(x => x.Name).ToListAsync();
        }

    }
}
