namespace Penzle.Core.Models;

public sealed class MimeType
{
    public MimeType(string extension, params string[] type)
    {
        Type = type;
        Extension = extension;
    }

    public static MimeType None => new(type: "N/A", extension: "N/A");
    public static MimeType Txt => new(type: "text/plain", extension: ".txt");
    public static MimeType Pdf => new(type: "application/pdf", extension: ".pdf");
    public static MimeType Docx => new(extension: ".docx", "application/ms-doc", "application/doc", "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
    public static MimeType Doc => new(type: "application/msword", extension: ".doc");
    public static MimeType Xlsx => new(type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", extension: ".xlsx");
    public static MimeType Xls => new(type: "application/vnd.ms-excel", extension: ".xls");
    public static MimeType Pptx => new(extension: ".pptx", "application/application/vnd.openxmlformats-officedocument.presentationml.slideshow", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    public static MimeType Ppt => new(extension: ".ppt", "application/vnd.ms-powerpoint");
    public static MimeType Jpeg => new(type: "image/jpeg", extension: ".jpeg");
    public static MimeType Jpg => new(type: "image/jpeg", extension: ".jpg");
    public static MimeType Jpe => new(type: "image/jpeg", extension: ".jpe");
    public static MimeType Png => new(type: "image/png", extension: ".png");
    public static MimeType Bmp => new(type: "image/bmp", extension: ".bmp");
    public static MimeType Zip => new(extension: ".zip", "application/zip", "application/octet-stream", "application/x-zip-compressed", "multipart/x-zip");
    public static MimeType Mp4 => new(type: "video/mp4", extension: ".mp4");
    public static MimeType Mp3 => new(type: "audio/mpeg", extension: ".mp3");

    public string[] Type { get; }
    private string Extension { get; }

    public static MimeType Parse(string value)
    {
        return value switch
        {
            ".pdf" => Pdf,
            ".docx" => Docx,
            ".doc" => Doc,
            ".xlsx" => Xlsx,
            ".xls" => Xls,
            ".pptx" => Pptx,
            ".ppt" => Ppt,
            ".jpeg" => Jpeg,
            ".jpg" => Jpg,
            ".jpe" => Jpe,
            ".png" => Png,
            ".bmp" => Bmp,
            ".mp4" => Mp4,
            ".mp3" => Mp3,
            ".zip" => Zip,
            ".txt" => Txt,
            _ => None
        };
    }

    public static implicit operator MimeType(string value)
    {
        return Parse(value: value);
    }

    public static implicit operator string(MimeType value)
    {
        return value.ToString();
    }

    public override string ToString()
    {
        return Extension;
    }
}
