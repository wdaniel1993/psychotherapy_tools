using TherapyTools.Client.Maui.Shared.Services;

namespace TherapyTools.Client.Maui.Web.Services;

public class FormFactor : IFormFactor
{
    public string GetFormFactor()
    {
        return "Web";
    }

    public string GetPlatform()
    {
        return Environment.OSVersion.ToString();
    }
}
