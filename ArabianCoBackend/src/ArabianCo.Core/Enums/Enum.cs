namespace ArabianCo.Enums;
public class Enum
{
    public enum ConfirmationCodeType : byte
    {
        ForgotPassword = 1,
        ConfirmPhoneNumber = 2,
        ConfirmEmail = 3,

    }
    public enum AttachmentRefType : byte
    {
        Product = 1,
        Brand = 2,
        Category = 3,
        ProductCover = 4,
    }
    public enum AttachmentType : byte
    {
        PDF = 1,
        WORD = 2,
        JPEG = 3,
        PNG = 4,
        JPG = 5,
        MP4 = 6,
        MP3 = 7,
        APK = 8,
    }
    public enum ImageType : byte
    {
        JPEG = 1,
        PNG = 2,
        JPG = 3
    }
    public enum MaintenanceRequestsStatus: byte
    {
        Pending = 1,
        Approved = 2,
        Rejected = 3,
    }

}

