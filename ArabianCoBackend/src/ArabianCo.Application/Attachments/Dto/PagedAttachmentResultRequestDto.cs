using Abp.Application.Services.Dto;
using static ArabianCo.Enums.Enum;

namespace ArabianCo.Attachments.Dto;

/// <summary>
/// PagedAttachmentResultRequestDto
/// </summary>
public class PagedAttachmentResultRequestDto : PagedAndSortedResultRequestDto
{
    public long RefId { get; set; }

    public AttachmentRefType RefType { get; set; }
}