using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ArabianCo.Domain.Attachments;
using static ArabianCo.Enums.Enum;

namespace ArabianCo.Attachments.Dto;

[AutoMapFrom(typeof(Attachment))]
public class LiteAttachmentDto : EntityDto
{
    /// <summary>
    /// Profile = 1
    /// RequestForJoinAsDeliveryAgent = 2,
    /// </summary>
    public AttachmentRefType RefType { get; set; }
    public string Url { get; set; }
}
