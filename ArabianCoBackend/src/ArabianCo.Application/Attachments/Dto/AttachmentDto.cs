using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ArabianCo.Domain.Attachments;
using static ArabianCo.Enums.Enum;

namespace ArabianCo.Attachments.Dto;

/// <summary>
/// AttachmentDto
/// </summary>
[AutoMapFrom(typeof(Attachment))]
public class AttachmentDto : EntityDto
{
    /// <summary>
    /// Profile = 1
    /// </summary>
    public AttachmentRefType RefType { get; set; }

   
    /// <summary>
    /// Attachment Type:
    /// 1- Pdf,
    /// 2- Word,
    /// 3- Jpeg,
    /// 4- Png,
    /// 5- Jpg
    /// </summary>

    /// <summary>
    /// Attachment Url
    /// </summary>
    public string Url { get; set; }

}