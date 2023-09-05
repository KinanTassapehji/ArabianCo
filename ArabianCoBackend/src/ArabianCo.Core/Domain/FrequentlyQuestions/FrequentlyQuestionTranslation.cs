using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace ArabianCo.Domain.FrequentlyQuestions;

public class FrequentlyQuestionTranslation : FullAuditedEntity, IEntityTranslation<FrequentlyQuestion>
{
    public string Question { get; set; }
    public string Answer { get; set; }
    public FrequentlyQuestion Core { get; set; }
    public int CoreId { get; set; }
    public string Language { get; set; }
}
