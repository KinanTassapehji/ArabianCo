using Abp.Configuration;
using Abp.Localization;
using Abp.Localization.Sources;
using Abp.Timing;
using Abp.UI;
using ArabianCo.Configuration;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static ArabianCo.Enums.Enum;

namespace ArabianCo.FileUploadService;

/// <summary>
/// File Upload Service
/// </summary>
public class FileUploadService : IFileUploadService
{
    //Can get those constants from configuration
    private static readonly string AttachmentsFolder = Path.Combine(AppConsts.UploadsFolderName, AppConsts.AttachmentsFolderName);
    private static readonly string ImagesFolder = Path.Combine(AppConsts.UploadsFolderName, AppConsts.ImagesFolderName);
    private static readonly Dictionary<string, AttachmentType> FileExtensionAttachmentTypeMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["pdf"] = AttachmentType.PDF,
        ["doc"] = AttachmentType.WORD,
        ["docx"] = AttachmentType.WORD,
        ["jpeg"] = AttachmentType.JPEG,
        ["jpg"] = AttachmentType.JPG,
        ["png"] = AttachmentType.PNG,
        ["mp4"] = AttachmentType.MP4,
        ["mp3"] = AttachmentType.MP3,
        ["apk"] = AttachmentType.APK
    };

    private readonly ISettingManager _settingManager;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ILocalizationSource _localizationSource;
    /// <summary>
    /// Logger
    /// </summary>
    public ILogger Logger { get; set; }
    /// <summary>
    ///  File Upload Service
    /// </summary>
    /// <param name="localizationManager"></param>
    /// <param name="settingManager"></param>
    /// <param name="webHostEnvironment"></param>
    public FileUploadService(ILocalizationManager localizationManager,
        ISettingManager settingManager,
        IWebHostEnvironment webHostEnvironment)
    {
        _settingManager = settingManager;
        _webHostEnvironment = webHostEnvironment;
        _localizationSource = localizationManager.GetSource(ArabianCoConsts.LocalizationSourceName);

        Logger = NullLogger.Instance;
    }
    /// <summary>
    /// Save Attachment Async
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public async Task<UploadedFileInfo> SaveAttachmentAsync(IFormFile file)
    {
        var fileInfo = new UploadedFileInfo { Type = GetAndCheckFileType(file) };

        var fileName = GenerateUniqueFileName(file);
        var pathToSave = GetPathToSaveAttachment(fileName);
        using (var stream = new FileStream(pathToSave, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        Logger.Info($"Base Attachment File was saved to ({pathToSave}) successfully.");

        fileInfo.RelativePath = GetAttachmentRelativePath(fileName);
        return fileInfo;
    }
    /// <summary>
    /// Save Image Async
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public async Task<UploadedImageInfo> SaveImageAsync(IFormFile file)
    {
        var fileInfo = new UploadedImageInfo { Type = GetAndCheckImageFileType(file) };
        await CheckFileSizeAsync(file);

        var fileName = GenerateUniqueImageFileName(file.FileName);
        var pathToSave = GetPathToSaveImage(fileName);
        using (var stream = new FileStream(pathToSave, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        Logger.Info($"Base Image File was saved to ({pathToSave}) successfully.");

        fileInfo.PathToSave = pathToSave;
        fileInfo.RelativePath = GetImageRelativePath(fileName);
        return fileInfo;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="patientInfo"></param>
    /// <returns></returns>
    public UploadedImageInfo GeneratePathToSave(string patientInfo)
    {

        var fileName = $"QRImage-{patientInfo}-{Guid.NewGuid()}.png";
        var pathToSave = GetPathToSaveAttachment(fileName);

        var fileInfo = new UploadedImageInfo
        {
            PathToSave = pathToSave,
            RelativePath = GetAttachmentRelativePath(fileName),
            Type = ImageType.PNG
        };
        return fileInfo;
    }
    /// <summary>
    /// Delete Attachment
    /// </summary>
    /// <param name="fileRelativePath"></param>
    public void DeleteAttachment(string fileRelativePath)
    {
        var pathFile = GetAbsolutePath(fileRelativePath);

        if (!File.Exists(pathFile))
        {
            Logger.Error($"Attachment File ({pathFile}) is not found.");
            return;
        }

        File.Delete(pathFile);

        Logger.Info($"Attachment File ({pathFile}) was deleted successfully.");
    }
    /// <summary>
    /// Check File Size Async
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public async Task CheckFileSizeAsync(IFormFile file)
    {
        var maxFileSize = await _settingManager.GetSettingValueAsync<decimal>(AppSettingNames.MaxFileSize);
      

        if (file.Length >= maxFileSize * 1024 * 1024)
            throw new UserFriendlyException(L("FileSizeExceedsMaxFileSize{0}", maxFileSize));
    }
    /// <summary>
    /// Get And Check File Type
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public AttachmentType GetAndCheckFileType(IFormFile file)
    {
        var contentType = file.ContentType?.ToLowerInvariant();
        foreach (AttachmentType type in Enum.GetValues(typeof(AttachmentType)))
        {
            if (!string.IsNullOrEmpty(contentType) && contentType.Contains(type.ToString().ToLowerInvariant()))
                return type;
        }

        var fileExtension = Path.GetExtension(file.FileName)?.TrimStart('.');
        if (!string.IsNullOrEmpty(fileExtension) && FileExtensionAttachmentTypeMap.TryGetValue(fileExtension, out var typeFromExtension))
            return typeFromExtension;

        throw new UserFriendlyException(L("TheAttachedFileTypeIsNotAcceptable"), $"FileName: {file.FileName}");
    }

    private string L(string key)
    {
        return _localizationSource.GetString(key);
    }

    private string L(string key, params object[] args)
    {
        return _localizationSource.GetString(key, args);
    }

    private string GetAbsolutePath(string fileRelativePath)
    {
        var basePath = _webHostEnvironment.WebRootPath;
        return Path.Combine(basePath, fileRelativePath);
    }

    private string GetPathToSaveAttachment(string fileName)
    {
        var basePath = _webHostEnvironment.WebRootPath;
        return Path.Combine(basePath, AttachmentsFolder, fileName);
    }

    private string GetAttachmentRelativePath(string fileName)
    {
        return Path.Combine(AttachmentsFolder, fileName);
    }

    private string GenerateUniqueFileName(IFormFile file)
    {
        var fileName = $"{Guid.NewGuid()}_{Clock.Now.Ticks}{Path.GetExtension(file.FileName)}";
        return fileName;
    }

    private string GetPathToSaveImage(string fileName)
    {
        var basePath = _webHostEnvironment.WebRootPath;
        return Path.Combine(basePath, ImagesFolder, fileName);
    }
    /// <summary>
    /// Get Image Relative Path
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public string GetImageRelativePath(string fileName)
    {
        return Path.Combine(ImagesFolder, fileName);
    }

    private string GenerateUniqueImageFileName(string fileName)
    {
        return $"ItemImage-{Guid.NewGuid()}{Path.GetExtension(fileName)}";
    }

    private ImageType GetAndCheckImageFileType(IFormFile file)
    {
        foreach (ImageType type in Enum.GetValues(typeof(ImageType)))
        {
            if (file.ContentType.Contains(type.ToString().ToLower()))
                return type;
        }

        throw new UserFriendlyException(L("UploadedImageFileTypeIsNotAcceptable"), $"FileName: {file.FileName}");
    }
}