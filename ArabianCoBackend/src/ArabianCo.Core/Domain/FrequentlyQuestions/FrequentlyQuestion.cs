using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace ArabianCo.Domain.FrequentlyQuestions;

public class FrequentlyQuestion : FullAuditedEntity, IMultiLingualEntity<FrequentlyQuestionTranslation>
{
    public ICollection<FrequentlyQuestionTranslation> Translations { get; set; }
    public bool IsActive { get; set; }
}
