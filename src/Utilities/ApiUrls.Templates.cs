namespace Penzle.Core.Utilities;

internal static partial class ApiUrls
{
    internal static Uri GetTemplate(Guid templateId)
    {
        return "templates/{0}/schema".FormatUri(templateId);
    }

    internal static Uri GetTemplateByCodeName(string codeName)
    {
        return "templates/{0}/schema".FormatUri(codeName);
    }
}
