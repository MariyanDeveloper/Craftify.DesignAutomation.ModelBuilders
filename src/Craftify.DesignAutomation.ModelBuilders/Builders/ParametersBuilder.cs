using Autodesk.Forge.DesignAutomation.Model;
using Craftify.DesignAutomation.ModelBuilders.Builders.Options;

namespace Craftify.DesignAutomation.ModelBuilders.Builders;

public class ParametersBuilder
{
    private readonly Dictionary<string, Parameter> _parameters = new();
    private ParameterBuilder? _parameterBuilder;

    // public ParametersBuilder AddInputForModelFile(string inputKeyName, Action<ParameterOptions>? configModelFileInput = null)
    // {
    //     AddParameter(inputKeyName, $"$({inputKeyName})", Verb.Get, configModelFileInput);
    //     return this;
    // }

    public ParametersBuilder Add(string keyName, Parameter parameter)
    {
        ThrowIfAlreadyAdded(keyName);
        _parameters.Add(keyName, parameter);
        return this;
    }
    public ParametersBuilder AddInputParameter(string inputKeyName, string localName, Action<ParameterOptions>? configInput = null)
    {
        // AddParameter(inputKeyName, localName, Verb.Get, configInput);
        ThrowIfAlreadyAdded(inputKeyName);
        AssignParameterBuilderIfNotExists();
        var inputParameter = _parameterBuilder.BuildInput(localName, configInput);
        _parameters.Add(inputKeyName, inputParameter);
        return this;
    }

    public ParametersBuilder AddOutputParameter(string outputKeyName, string localName, Action<ParameterOptions>? configInput = null)
    {
        ThrowIfAlreadyAdded(outputKeyName);
        AssignParameterBuilderIfNotExists();
        var inputParameter = _parameterBuilder.BuildOutput(localName, configInput);
        _parameters.Add(outputKeyName, inputParameter);
        return this;
        // AddParameter(outputKeyName, localName, Verb.Put, configInput);
        // return this;
    }

    private void AssignParameterBuilderIfNotExists()
    {
        _parameterBuilder ??= new ParameterBuilder();
    }

    public Dictionary<string, Parameter> Build() => _parameters;

    // private void AddParameter(string keyName, string localName, Verb verb, Action<ParameterOptions>? configOptions = null)
    // {
    //     ThrowIfAlreadyAdded(keyName);
    //     var options = CreateParameterOptions(configOptions);
    //     var parameter = new Parameter()
    //     {
    //         LocalName = localName,
    //         Description = options.Description,
    //         Required = options.Required,
    //         Verb = verb,
    //         Ondemand = options.OnDemand,
    //         Zip = options.Zip
    //     };
    //     _parameters.Add(keyName, parameter);
    // }
    //
    // private ParameterOptions CreateParameterOptions(Action<ParameterOptions>? configOptions)
    // {
    //     var options = new ParameterOptions();
    //     configOptions?.Invoke(options);
    //     return options;
    // }

    private void ThrowIfAlreadyAdded(string name)
    {
        if (_parameters.ContainsKey(name))
        {
            throw new ApplicationException($"Parameters already have given key: {name}");
        }
    }
}