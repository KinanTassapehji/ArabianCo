using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using ArabianCo.Areas.Dto;
using ArabianCo.CrudAppServiceBase;
using ArabianCo.Domain.Areas;
using ArabianCo.Domain.Countries;
using ArabianCo.Localization.SourceFiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArabianCo.Areas
{
    public class AreaAppService : ArabianCoAsyncCrudAppService<Area, AreaDetailsDto, int, LiteAreaDto,
        PagedAreaResultRequestDto, CreateAreaDto, UpdateAreaDto>,
        IAreaAppService
    {
        private readonly IAreaManager _areaManager;
        private readonly ICountryManager _countryManager;



        /// <summary>
        /// Region AppService
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="regionManager"></param>
        /// <param name="countryManager"></param>
        /// <param name="regionTranslationRepository"></param>

        public AreaAppService(IRepository<Area> repository, IAreaManager areaManager,
            ICountryManager countryManager)
        : base(repository)
        {

            _areaManager = areaManager;
            _countryManager = countryManager;
        }
        /// <summary>
        /// Get Region Details ById
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<AreaDetailsDto> GetAsync(EntityDto<int> input)
        {
            var area = await _areaManager.GetEntityByIdAsync(input.Id);
            if (area is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.Area));
            return MapToEntityDto(area);
        }
        /// <summary>
        /// Get All Regions Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public override async Task<PagedResultDto<LiteAreaDto>> GetAllAsync(PagedAreaResultRequestDto input)
        {

            return await base.GetAllAsync(input);
        }
        /// <summary>
        /// Add New Region Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public override async Task<AreaDetailsDto> CreateAsync(CreateAreaDto input)
        {
            CheckCreatePermission();
            var Translation = ObjectMapper.Map<List<AreaTranslation>>(input.Translations);
            if (await _areaManager.CheckIfAreaIsExist(Translation))
                throw new UserFriendlyException(string.Format(Exceptions.ObjectIsAlreadyExist, Tokens.Area));
            var area = ObjectMapper.Map<Area>(input);
            area.IsActive = true;
            area.CreationTime = DateTime.UtcNow;
            await Repository.InsertAsync(area);
            return MapToEntityDto(area);
        }
        /// <summary>
        /// Update Region Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public override async Task<AreaDetailsDto> UpdateAsync(UpdateAreaDto input)
        {
            CheckUpdatePermission();
            var area = await _areaManager.GetEntityByIdAsync(input.Id);
            if (area is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.Area));
            area.Translations.Clear();
            MapToEntity(input, area);
            area.LastModificationTime = DateTime.UtcNow;
            await Repository.UpdateAsync(area);
            return MapToEntityDto(area);
        }

        /// <summary>
        /// Delete Region Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();
            var region = await _areaManager.GetEntityByIdAsync(input.Id);
            if (region is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.Area));
            await Repository.DeleteAsync(region);
        }

        /// <summary>
        /// Filter For A Group Of Regions
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<Area> CreateFilteredQuery(PagedAreaResultRequestDto input)
        {
        var data = base.CreateFilteredQuery(input);
        data = data.Include(x => x.Translations.Where(t => !t.IsDeleted));
        data = data.Include(x => x.City)
            .ThenInclude(x => x.Translations.Where(t => !t.IsDeleted));
        data = data.Include(x => x.City)
            .ThenInclude(x => x.Areas)
            .ThenInclude(x => x.Translations.Where(t => !t.IsDeleted));
            if (!input.Keyword.IsNullOrEmpty())
                data = data.Where(x => x.Translations.Where(x => x.Name.Contains(input.Keyword)).Any());
            if (input.CityId.HasValue)
                data = data.Where(x => x.CityId == input.CityId);
            if (input.IsActive.HasValue)
                data = data.Where(x => x.IsActive == input.IsActive.Value);
            return data;
        }

        /// <summary>
        /// Sorting Filtered Regions
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<Area> ApplySorting(IQueryable<Area> query, PagedAreaResultRequestDto input)
        {
            return query.OrderByDescending(r => r.CreationTime);
        }

        /// <summary>
        /// Switch Activation Of A Region
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<AreaDetailsDto> SwitchActivationAsync(SwitchActivationInputDto input)
        {
            CheckUpdatePermission();
            var region = await _areaManager.GetLiteEntityByIdAsync(input.Id);
            if (region is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.Area));
            region.IsActive = !region.IsActive;
            return MapToEntityDto(region);
        }

    }
}

