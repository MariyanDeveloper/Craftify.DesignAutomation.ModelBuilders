namespace Craftify.DesignAutomation.ModelBuilders.Builders.Options;

public class ParameterOptions
{
    public string Description { get; set; } = "Default Description";
    public bool Required { get; set; } = true;
    public bool OnDemand { get; set; } = false;
    public bool Zip { get; set; } = false;
}