using Abp.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArabianCo.Domain.Attributes;

public interface IAttributeManger : IDomainService
{
    Task<bool> CheckIfAttributeIsExist(List<AttributeTranslation> translations);
    Task<Attribute> GetEntityByIdAsync(int id);
    Task InsertAsync(Attribute attribute);
}
