using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using ArabianCo;
using ArabianCo.Cities.Dto;
using ArabianCo.CrudAppServiceBase;
using ArabianCo.Domain.Cities;
using ArabianCo.Domain.Countries;
using ArabianCo.Localization.SourceFiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArabianCo.Cities
{

    /// <summary>
    /// 
    /// </summary>
    public class CityAppService : ArabianCoAsyncCrudAppService<City, CityDetailsDto, int, LiteCityDto,
        PagedCityResultRequestDto, CreateCityDto, UpdateCityDto>,
        ICityAppService
    {
        private readonly ICityManager _cityManager;
        private readonly ICountryManager _countryManager;
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<CityTranslation> _cityTranslationRepository;
        private readonly IRepository<City> _cityRepository;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="cityManager"></param>
        /// <param name="countryManager"></param>
        /// <param name="countryRepository"></param>
        /// <param name="cityTranslationRepository"></param>
        /// <param name="cityRepository"></param>
        /// <param name="attachmentManager"></param>
        public CityAppService(IRepository<City> repository,
            ICityManager cityManager,
            ICountryManager countryManager,
            IRepository<Country> countryRepository,
            IRepository<CityTranslation> cityTranslationRepository
,
            IRepository<City> cityRepository)
         : base(repository)
        {
            _cityManager = cityManager;
            _countryManager = countryManager;
            _countryRepository = countryRepository;
            _cityTranslationRepository = cityTranslationRepository;
            _cityRepository = cityRepository;
        }

        /// <summary>
        /// Get City ByID
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<CityDetailsDto> GetAsync(EntityDto<int> input)
        {
            var city = await _cityManager.GetEntityByIdAsync(input.Id);

            var cityDetailsDto = MapToEntityDto(city);
            return cityDetailsDto;
        }
        /// <summary>
        /// Get All City
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public override async Task<PagedResultDto<LiteCityDto>> GetAllAsync(PagedCityResultRequestDto input)
        {
            var result = await base.GetAllAsync(input);
            return result;
        }

        [AbpAuthorize]
        /// <summary>
        /// Add New City
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<CityDetailsDto> CreateAsync(CreateCityDto input)
        {
            CheckCreatePermission();
            var country = await _countryManager.GetLiteEntityByIdAsync(input.CountryId);
            if (country is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.Country));
            var Translation = ObjectMapper.Map<List<CityTranslation>>(input.Translations);
            if (await _cityManager.CheckIfCityIsExist(Translation))
                throw new UserFriendlyException(string.Format(Exceptions.ObjectIsAlreadyExist, Tokens.City));
            var city = ObjectMapper.Map<City>(input);
            city.IsActive = true;
            city.CreationTime = DateTime.UtcNow;
            await Repository.InsertAsync(city);
            await CurrentUnitOfWork.SaveChangesAsync();
            return MapToEntityDto(city);

        }
        /// <summary>
        /// Update City Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public override async Task<CityDetailsDto> UpdateAsync(UpdateCityDto input)
        {
            CheckUpdatePermission();
            var city = await _cityManager.GetEntityByIdAsync(input.Id);
            if (city is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.City));
            city.Translations.Clear();
            var country = _countryRepository.GetAsync(input.CountryId);
            if (country is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.Country));
            MapToEntity(input, city);
            await _cityRepository.UpdateAsync(city);
            return MapToEntityDto(city);
        }


        /// <summary>
        /// Delete A City 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();
            var city = await _cityManager.GetEntityByIdAsync(input.Id);
            if (city is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.City));
            if (city.Areas.Count > 0)
            {
                throw new UserFriendlyException(string.Format(Exceptions.ObjectCantBeDelete, Tokens.Area));
            }
            foreach (var translation in city.Translations.ToList())
            {
                await _cityTranslationRepository.DeleteAsync(translation);
                city.Translations.Remove(translation);
            }
            await _cityRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// Filter for  A Group of City
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<City> CreateFilteredQuery(PagedCityResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input);
            data = data.Include(x => x.Translations);
            if (input.isActive.HasValue)
                data = data.Where(x => x.IsActive == input.isActive.Value);
            if (!input.Keyword.IsNullOrEmpty())
                data = data.Where(x => x.Translations.Where(x => x.Name.Contains(input.Keyword)).Any());
            if (input.CountryId.HasValue)
                data = data.Where(x => x.CountryId == input.CountryId);
            data = data.Include(x => x.Country).ThenInclude(x => x.Translations);
            return data;
        }
        /// <summary>
        /// Sort Filtered Cities
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<City> ApplySorting(IQueryable<City> query, PagedCityResultRequestDto input)
        {
            return query.OrderBy(r => r.Id);
        }
        /// <summary>
        /// Switch Activation of A City
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [HttpPut]
        public async Task<CityDetailsDto> SwitchActivationAsync(SwitchActivationInputDto input)
        {
            CheckUpdatePermission();
            var city = await _cityManager.GetLiteEntityByIdAsync(input.Id);
            city.IsActive = !city.IsActive;
            city.LastModificationTime = DateTime.UtcNow;
            await _cityRepository.UpdateAsync(city);
            return MapToEntityDto(city);
        }

    }
}
