using Abp.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using static ArabianCo.Enums.Enum;

namespace ArabianCo.Domain.Attachments;

public interface IAttachmentManager : IDomainService
{
    /// <summary>
    /// Get Attachment by Id.
    /// Throws exception if there is error.
    /// </summary>
    /// <param name="id">Id of the Attachment</param>
    /// <returns>Attachment Entity</returns>
    Task<Attachment> GetByIdAsync(int id);
    /// <summary>
    /// Check if attachment id exists, and has a specific related entity type.
    /// Throws exception if there is error.
    /// </summary>
    /// <param name="id">Id of the Attachment</param>
    /// <param name="refType">Type of related Entity</param>
    /// <returns>Attachment Entity</returns>
    Task<Attachment> GetAndCheckAsync(int id, AttachmentRefType refType);

    /// <summary>
    /// Get navigable Url of an attachment,
    /// using attachment relative path and a configured base uri.
    /// </summary>
    /// <param name="attachment">Attachment Entity</param>
    /// <returns>Url for the attachment</returns>
    string GetUrl(Attachment attachment);

    /// <summary>
    /// Update RefId to passed refId, so attachment is now related to entity.
    /// </summary>
    /// <param name="attachment">Attachment Entity</param>
    /// <param name="refId">Id of related entity</param>
    Task UpdateRefIdAsync(Attachment attachment, int refId);

    /// <summary>
    /// Check if attachment id exists, and has a specific related entity type.
    /// Update RefId of the attachment to refId.
    /// </summary>
    /// <param name="id">Id of the Attachment</param>
    /// <param name="refType">Type of related Entity</param>
    /// <param name="refId">Id of related entity</param>
    /// <returns>The Attachment after modification</returns>
    Task<Attachment> CheckAndUpdateRefIdAsync(int id, AttachmentRefType refType, int refId);

    /// <summary>
    /// Update RefId to be null, so it can be removed by background service.
    /// </summary>
    /// <param name="attachment">Attachment Entity</param>
    Task DeleteRefIdAsync(Attachment attachment);

    /// <summary>
    /// Update RefId of all attachments of refId and refType to be null,
    /// so they can be removed by background service.
    /// </summary>
    /// <param name="refId">Id of related entity</param>
    /// <param name="refType">Type of related Entity</param>
    Task DeleteAllRefIdAsync(int refId, AttachmentRefType refType);

    /// <summary>
    /// Checks if file type is compatible with related entity type.
    /// </summary>
    /// <param name="refType">Type of related Entity</param>
    /// <param name="fileType">Type of Attachment (file type)</param>
    void CheckAttachmentRefType(AttachmentRefType refType, AttachmentType fileType);

    /// <summary>
    /// Get list of attachments that are related to specific Entity Id and Type.
    /// </summary>
    /// <param name="refId">Id of related entity</param>
    /// <param name="refType">Type of related Entity</param>
    /// <returns>List of Attachment Entities</returns>
    Task<Attachment> GetByRefAsync(int refId, AttachmentRefType refType);
    Task<List<Attachment>> GetListByRefAsync(int refId, AttachmentRefType refType);

    Task<Attachment> GetAttachmentByRefAsync(int refId, AttachmentRefType refType);

    /// <summary>
    /// CreateAttachments
    /// </summary>
    /// <param name="attachments"></param>
    Task<bool> CreateAttachments(List<Attachment> attachments);

    /// <summary>
    /// CreateAttachment
    /// </summary>
    /// <param name="attachment"></param>
    /// <returns></returns>
    Task<Attachment> CreateAttachment(Attachment attachment);
}
