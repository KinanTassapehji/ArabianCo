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
using System.Threading;
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

    private static readonly Dictionary<string, AttachmentType> MimeTypeAttachmentTypeMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["application/pdf"] = AttachmentType.PDF,
        ["application/x-pdf"] = AttachmentType.PDF,
        ["application/acrobat"] = AttachmentType.PDF,
        ["text/pdf"] = AttachmentType.PDF,
        ["image/pdf"] = AttachmentType.PDF,
        ["application/msword"] = AttachmentType.WORD,
        ["application/vnd.openxmlformats-officedocument.wordprocessingml.document"] = AttachmentType.WORD,
        ["application/vnd.ms-word"] = AttachmentType.WORD,
        ["application/vnd.msword"] = AttachmentType.WORD,
        ["image/jpeg"] = AttachmentType.JPEG,
        ["image/jpg"] = AttachmentType.JPG,
        ["image/pjpeg"] = AttachmentType.JPEG,
        ["image/png"] = AttachmentType.PNG,
        ["video/mp4"] = AttachmentType.MP4,
        ["application/mp4"] = AttachmentType.MP4,
        ["audio/mpeg"] = AttachmentType.MP3,
        ["audio/mp3"] = AttachmentType.MP3,
        ["audio/mpeg3"] = AttachmentType.MP3,
        ["application/vnd.android.package-archive"] = AttachmentType.APK
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
                if (file == null || file.Length == 0)
                        throw new ArgumentException("Uploaded file is empty.", nameof(file));

                var fileInfo = new UploadedFileInfo { Type = GetAndCheckFileType(file) };

                // 2) Resolve a fixed base folder you control (e.g., from config)
                var uniqueName = GenerateUniqueFileName(file);
                var attachmentsDirectory = GetAttachmentsDirectory();
                if (string.IsNullOrWhiteSpace(attachmentsDirectory))
                        throw new InvalidOperationException("Attachments path is not configured.");

                var baseFull = Path.GetFullPath(attachmentsDirectory);
                var fullPath = Path.GetFullPath(Path.Combine(attachmentsDirectory, uniqueName));
                if (!fullPath.StartsWith(baseFull, StringComparison.OrdinalIgnoreCase))
                        throw new InvalidOperationException("Invalid path traversal attempt.");

                try
                {
                        // 4) Ensure directory exists
                        Directory.CreateDirectory(baseFull);

                        // 5) Save file (use async, no sharing)
                        await using var target = new FileStream(
                                fullPath,
                                FileMode.Create,       // overwrite if same unique name somehow collides
                                FileAccess.Write,
                                FileShare.None,
                                81920,
                                useAsync: true);

                        await file.CopyToAsync(target);

                        Logger.Info($"Attachment saved to '{fullPath}' successfully.");

                        // 6) Store a relative/virtual path as needed
                        fileInfo.RelativePath = GetAttachmentRelativePath(uniqueName);
                        return fileInfo;
                }
                catch (Exception ex)
                {
                        Logger.Error($"I/O error writing '{fullPath}'. {ex.Message}");
                        throw;
                }
        }

	private static string SanitizeFileName(string name)
	{
		foreach (var c in Path.GetInvalidFileNameChars())
			name = name.Replace(c, '_');
		return name.Trim();
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
        if (TryResolveAttachmentTypeFromMime(file.ContentType, out var typeFromMime))
            return typeFromMime;

        if (TryResolveAttachmentTypeFromExtension(file.FileName, out var typeFromExtension))
            return typeFromExtension;

        throw new UserFriendlyException(L("TheAttachedFileTypeIsNotAcceptable"), $"FileName: {file.FileName}");
    }

    private static bool TryResolveAttachmentTypeFromMime(string contentType, out AttachmentType type)
    {
        type = default;

        if (string.IsNullOrWhiteSpace(contentType))
            return false;

        if (MimeTypeAttachmentTypeMap.TryGetValue(contentType, out type))
            return true;

        var sanitizedContentType = contentType.Split(';')[0];
        if (MimeTypeAttachmentTypeMap.TryGetValue(sanitizedContentType, out type))
            return true;

        var mimeParts = sanitizedContentType.Split('/');
        if (mimeParts.Length > 1)
        {
            var mimeSubType = mimeParts[mimeParts.Length - 1];
            if (!string.IsNullOrEmpty(mimeSubType))
            {
                if (FileExtensionAttachmentTypeMap.TryGetValue(mimeSubType, out type))
                    return true;

                if (Enum.TryParse(mimeSubType, true, out type))
                    return true;
            }
        }

        foreach (var kvp in MimeTypeAttachmentTypeMap)
        {
            if (contentType.IndexOf(kvp.Key, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                type = kvp.Value;
                return true;
            }
        }

        return false;
    }

    private static bool TryResolveAttachmentTypeFromExtension(string fileName, out AttachmentType type)
    {
        type = default;

        if (string.IsNullOrWhiteSpace(fileName))
            return false;

        var extension = Path.GetExtension(fileName);
        if (string.IsNullOrWhiteSpace(extension))
            return false;

        var normalizedExtension = extension.TrimStart('.');

        if (FileExtensionAttachmentTypeMap.TryGetValue(normalizedExtension, out type))
            return true;

        return Enum.TryParse(normalizedExtension, true, out type);
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

    private string GetAttachmentsDirectory()
    {
        var basePath = _webHostEnvironment.WebRootPath;
        return Path.Combine(basePath, AttachmentsFolder);
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
        var contentType = file.ContentType;
        if (!string.IsNullOrWhiteSpace(contentType))
        {
            foreach (ImageType type in Enum.GetValues(typeof(ImageType)))
            {
                if (contentType.IndexOf(type.ToString(), StringComparison.OrdinalIgnoreCase) >= 0)
                    return type;
            }
        }

        if (TryResolveImageTypeFromExtension(file.FileName, out var imageTypeFromExtension))
            return imageTypeFromExtension;

        throw new UserFriendlyException(L("UploadedImageFileTypeIsNotAcceptable"), $"FileName: {file.FileName}");
    }

    private static bool TryResolveImageTypeFromExtension(string fileName, out ImageType imageType)
    {
        imageType = default;

        if (string.IsNullOrWhiteSpace(fileName))
            return false;

        var extension = Path.GetExtension(fileName);
        if (string.IsNullOrWhiteSpace(extension))
            return false;

        return Enum.TryParse(extension.TrimStart('.'), true, out imageType);
    }
}
