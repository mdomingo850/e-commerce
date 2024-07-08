namespace SharedKernel.Models;

public record EnvironmentVariables : IEnvironmentVariables
{
    public string ApiName { get; set; }

    public EnvironmentVariables(string apiName)
    {
        ApiName = apiName;
    }
}
