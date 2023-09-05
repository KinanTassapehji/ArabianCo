using Abp.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArabianCo.Domain.Areas
{
    public interface IAreaManager : IDomainService
    {
        Task<Area> GetEntityByIdAsync(int id);
        Task<bool> CheckIfAreaIsExist(List<AreaTranslation> Translations);

        Task<Area> GetLiteEntityByIdAsync(int id);
        Task IsEntityExistAsync(int id);
        Task <List<string>> GetAllAreasNameForAutoComplete(string inputAutoComplete);
    }
}
