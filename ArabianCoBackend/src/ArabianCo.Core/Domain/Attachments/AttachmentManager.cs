using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.EntityFrameworkCore.Repositories;
using Abp.UI;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ArabianCo.Enums.Enum;

namespace ArabianCo.Domain.Attachments;

internal class AttachmentManager : DomainService, IAttachmentManager
{
    private readonly IRepository<Attachment> _repository;
    private readonly string _appBaseUrl;

    public AttachmentManager(IRepository<Attachment> repository, IConfiguration configuration)

    {
        _repository = repository;
        _appBaseUrl = configuration[ArabianCoConsts.AppServerRootAddressKey] ?? "/";
        LocalizationSourceName = ArabianCoConsts.LocalizationSourceName;
    }

    public async Task<Attachment> GetByIdAsync(int id)
    {
        var attachment = await _repository.FirstOrDefaultAsync(id);

        if (attachment == null)
            throw new UserFriendlyException(L("AttachmentIsNotFound"), $"Id: {id}");

        return attachment;
    }

    public async Task<Attachment> GetAndCheckAsync(int id, AttachmentRefType refType)
    {
        var attachment = await GetByIdAsync(id);

        if (attachment.RefType != refType)
            throw new UserFriendlyException(L("InvalidAttachmentRefType"),
                $"Id: {id}, RefType: {attachment.RefType} and should be {(byte)refType}");

        return attachment;
    }

    public string GetUrl(Attachment attachment)
    {
        var baseUri = new Uri(_appBaseUrl);
        return (new Uri(baseUri, attachment.RelativePath)).AbsoluteUri;
    }
    public async Task UpdateRefIdAsync(Attachment attachment, int refId, string color = null)
    {
        if (attachment.RefId != null)
            throw new UserFriendlyException(L("AttachmentAlreadyRelatedToEntity"),
                $"Id: {attachment.Id}, RefType: {attachment.RefType}");

        attachment.RefId = refId;
        if (color != null)
            attachment.Color = color;
        await _repository.UpdateAsync(attachment);
    }

    public async Task<Attachment> CheckAndUpdateRefIdAsync(int id, AttachmentRefType refType, int refId, string color = null)
    {
        //Check if type is correct and update refId
        var attachment = await GetAndCheckAsync(id, refType);
        await UpdateRefIdAsync(attachment, refId, color);

        return attachment;
    }

    public async Task DeleteRefIdAsync(Attachment attachment)
    {
        attachment.RefId = null;
        await _repository.UpdateAsync(attachment);
    }

    public async Task DeleteAllRefIdAsync(int refId, AttachmentRefType refType)
    {
        var list = await GetListByRefAsync(refId, refType);
        foreach (var attachment in list)
        {
            attachment.RefId = null;
        }
        _repository.GetDbContext().UpdateRange(list);
    }

    public void CheckAttachmentRefType(AttachmentRefType refType, AttachmentType fileType)
    {
        if (!AcceptedTypesFor(refType).Contains(fileType))
            throw new UserFriendlyException(L("FileTypeIncompatibleWithRefType"),
                $"Type:{fileType.ToString()}, RefType:{refType.ToString()}");
    }

    public async Task<Attachment> GetByRefAsync(int refId, AttachmentRefType refType)
    {
        return await _repository.FirstOrDefaultAsync(x => x.RefId == refId && x.RefType == refType);
    }

    public async Task<Attachment> GetAttachmentByRefAsync(int refId, AttachmentRefType refType)
    {
        var refTypeString = ((byte)refType).ToString();
        var Attachments = await _repository.GetAllListAsync(x => x.RefId == refId && x.RefType == refType);
        if (Attachments is not null)
            return Attachments.FirstOrDefault();

        return await Task.FromResult<Attachment>(null);
    }

    private static IEnumerable<AttachmentType> AcceptedTypesFor(AttachmentRefType refType)
    {
        switch (refType)
        {
            case AttachmentRefType.ProductCover:
                return ImagesAcceptedTypes;

            case AttachmentRefType.Brand:
                return ImagesAcceptedTypes;

            case AttachmentRefType.Category:
                return ImagesAcceptedTypes;

            case AttachmentRefType.Product:
                return ImagesAcceptedTypes;
            case AttachmentRefType.MaintenanceRequests:
                return ImagesAcceptedTypes;
            case AttachmentRefType.AboutUs:
                return ImagesAcceptedTypes;
            case AttachmentRefType.CategoryIcon:
                return ImagesAcceptedTypes;
        }

        return new AttachmentType[] { };
    }

    public Task<bool> CreateAttachments(List<Attachment> attachments)
    {
        try
        {
            foreach (var attachment in attachments)
            {
                _repository.InsertAsync(attachment);
            }
            return Task.FromResult(true);
        }
        catch (Exception e)
        {
            throw new UserFriendlyException(L("ErrorOnInsertingAttachments"));
        }
    }

    public async Task<Attachment> CreateAttachment(Attachment attachment)
    {
        try
        {
            var attachmentResult = await _repository.InsertAsync(attachment);
            await CurrentUnitOfWork.SaveChangesAsync();
            return attachmentResult;
        }
        catch (Exception e)
        {
            throw new UserFriendlyException(L("ErrorOnInsertingAttachment"));
        }
    }

    public Task<List<Attachment>> GetListByRefAsync(int refId, AttachmentRefType refType)
    {
        return _repository.GetAllListAsync(x => x.RefType == refType && x.RefId == refId);
    }


    private static readonly AttachmentType[] AllAcceptedTypes =
        { AttachmentType.JPEG, AttachmentType.JPG, AttachmentType.PDF, AttachmentType.PNG, AttachmentType.WORD };

    private static readonly AttachmentType[] ImagesAcceptedTypes =
        { AttachmentType.JPEG, AttachmentType.JPG, AttachmentType.PNG, AttachmentType.MP4,AttachmentType.PDF,AttachmentType.MP3 };
}
