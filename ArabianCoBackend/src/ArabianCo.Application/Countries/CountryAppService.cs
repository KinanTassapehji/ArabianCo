using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using ArabianCo.Countries.Dto;
using ArabianCo.CrudAppServiceBase;
using ArabianCo.Domain.Countries;
using ArabianCo.Localization.SourceFiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Abp.Extensions;

namespace ArabianCo.Countries;

public class CountryAppService : ArabianCoAsyncCrudAppService<Country, CountryDetailsDto, int, CountryDto,
        PagedCountryResultRequestDto, CreateCountryDto, UpdateCountryDto>,
        ICountryAppService
{
    private readonly ICountryManager _countryManager;
    public CountryAppService(IRepository<Country, int> repository, ICountryManager countryManager) : base(repository)
    {
        _countryManager = countryManager;
    }
    public override async Task<CountryDetailsDto> GetAsync(EntityDto<int> input)
    {
        var country = await _countryManager.GetEntityByIdAsync(input.Id);
        if (country is null)
            throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.Country));
        return MapToEntityDto(country);
    }
    /// <summary>
    /// Get All Countries Details 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public override async Task<PagedResultDto<CountryDto>> GetAllAsync(PagedCountryResultRequestDto input)
    {

        return await base.GetAllAsync(input);
    }
    /// <summary>
    /// Add New Country  Details
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public override async Task<CountryDetailsDto> CreateAsync(CreateCountryDto input)
    {
        CheckCreatePermission();
        var Translation = ObjectMapper.Map<List<CountryTranslation>>(input.Translations);
        if (await _countryManager.CheckIfCountryExist(Translation))
            throw new UserFriendlyException(string.Format(Exceptions.ObjectIsAlreadyExist, Tokens.Country));
        var country = ObjectMapper.Map<Country>(input);
        country.IsActive = true;
        await _countryManager.InsertAsync(country);


        var result = MapToEntityDto(country);



        return result;
    }
    /// <summary>
    /// Update Country Details
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public override async Task<CountryDetailsDto> UpdateAsync(UpdateCountryDto input)
    {
        CheckUpdatePermission();
        var country = await _countryManager.GetEntityByIdAsync(input.Id);
        if (country is null)
            throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.Country));
        country.Translations.Clear();
        MapToEntity(input, country);
        country.LastModificationTime = DateTime.UtcNow;
        //await _countryRepository.UpdateAsync(country);
        await _countryManager.UpdateAsync(country);
        return MapToEntityDto(country);

    }
    /// <summary>
    /// Delete Country Details
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public override async Task DeleteAsync(EntityDto<int> input)
    {
        CheckDeletePermission();
        var country = await _countryManager.GetEntityByIdAsync(input.Id);
        if (country is null)
            throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.Country));
        if (country.Cities.Count > 0)
        {
            throw new UserFriendlyException(string.Format(Exceptions.ObjectCantBeDelete, Tokens.City));
        }
        await Repository.DeleteAsync(input.Id);
    }

    /// <summary>
    /// Filter For A Group of Countries
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    protected override IQueryable<Country> CreateFilteredQuery(PagedCountryResultRequestDto input)
    {
        var data = base.CreateFilteredQuery(input);
        data = data.Include(x => x.Translations);
        if (!input.Keyword.IsNullOrEmpty())
            data = data.Where(x => x.Translations.Where(x => x.Name.Contains(input.Keyword)).Any() || x.DialCode.Contains(input.Keyword));
        if (input.IsActive.HasValue)
            data = data.Where(x => x.IsActive == input.IsActive.Value);
        return data;
    }
    /// <summary>
    /// Sorting Filtered Countries
    /// </summary>
    /// <param name="query"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    protected override IQueryable<Country> ApplySorting(IQueryable<Country> query, PagedCountryResultRequestDto input)
    {
        return query.OrderByDescending(r => r.CreationTime);
    }
    /// <summary>
    /// Switch Activation For A Country
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<CountryDetailsDto> SwitchActivationAsync(SwitchActivationInputDto input)
    {
        CheckUpdatePermission();
        var country = await _countryManager.GetLiteEntityByIdAsync(input.Id);
        if (country is null)
            throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.Country));
        country.IsActive = !country.IsActive;
        country.LastModificationTime = DateTime.UtcNow;
        await _countryManager.UpdateAsync(country);
        return MapToEntityDto(country);

    }
}
