namespace Penzle.Core.Utilities;

internal static partial class ApiUrls
{
    internal static Uri GetForm(Guid formId, string language)
    {
        return !string.IsNullOrWhiteSpace(value: language) ? "forms/{0}?language={1}".FormatUri(formId, language) : "forms/{0}".FormatUri(formId);
    }

    public static Uri CreateForm()
    {
        return "forms".FormatUri();
    }

    public static Uri UpdateForm(Guid formId)
    {
        return "forms/{0}".FormatUri(formId);
    }

    public static Uri DeleteForm(Guid formId)
    {
        return "forms/{0}".FormatUri(formId);
    }
}
