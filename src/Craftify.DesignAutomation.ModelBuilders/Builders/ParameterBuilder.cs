using Autodesk.Forge.DesignAutomation.Model;
using Craftify.DesignAutomation.ModelBuilders.Builders.Options;

namespace Craftify.DesignAutomation.ModelBuilders.Builders;

public class ParameterBuilder
{
    public Parameter BuildInput(string localName, Action<ParameterOptions>? configInput = null)
    {
        return Create(localName, Verb.Get, configInput);
    }
    public Parameter BuildOutput(string localName, Action<ParameterOptions>? configInput = null)
    {
        return Create(localName, Verb.Put, configInput);
    }
    private Parameter Create(string localName, Verb verb, Action<ParameterOptions>? configOptions = null)
    {
        var options = CreateParameterOptions(configOptions);
        var parameter = new Parameter()
        {
            LocalName = localName,
            Description = options.Description,
            Required = options.Required,
            Verb = verb,
            Ondemand = options.OnDemand,
            Zip = options.Zip
        };
        return parameter;
    }
    private ParameterOptions CreateParameterOptions(Action<ParameterOptions>? configOptions)
    {
        var options = new ParameterOptions();
        configOptions?.Invoke(options);
        return options;
    }
}