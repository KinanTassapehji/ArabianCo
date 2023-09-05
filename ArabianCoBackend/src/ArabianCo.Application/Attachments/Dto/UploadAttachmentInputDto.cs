using Abp.AutoMapper;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using ArabianCo.Domain.Attachments;
using static ArabianCo.Enums.Enum;

namespace ArabianCo.Attachments.Dto;

/// <summary>
/// UploadAttachmentInputDto
/// </summary>
[AutoMapTo(typeof(Attachment))]
public class UploadAttachmentInputDto
{
    /// <summary>
    ///  Product = 1,
    ///  Brand = 2,
    ///  Category = 3,
    /// </summary>
    public AttachmentRefType RefType { get; set; }

    /// <summary>
    /// Accepted File Types: 1- Pdf, 2- Word, 3- Jpeg, 4- Png, 5- Jpg
    /// </summary>
    [Required(ErrorMessage = "Required")]
    public IFormFile File { get; set; }

    /// <summary>
    /// AddValidationErrors
    /// </summary>
    /// <param name="context"></param>
}