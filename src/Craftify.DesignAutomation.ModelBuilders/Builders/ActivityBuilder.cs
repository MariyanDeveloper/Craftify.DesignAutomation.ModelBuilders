using Autodesk.Forge.DesignAutomation.Model;
using Craftify.DesignAutomation.ModelBuilders.Builders.CommandLineBuilderStages;
using Craftify.DesignAutomation.Shared;
using Craftify.DesignAutomation.Shared.Extensions;

namespace Craftify.DesignAutomation.ModelBuilders.Builders;

public class ActivityBuilder 
{
    private string _activityName;
    private Product _product;
    private int _productVersion;
    private List<string> _commandLines = new();
    private Dictionary<string, Parameter> _parameters = new();
    private List<string> _appBundles = new();
    private int? _activityVersion;
    private string _description;
    public ActivityBuilder OfName(string name)
    {
        _activityName = name ?? throw new ArgumentNullException(nameof(name));
        return this;
    }

    public ActivityBuilder Describe(string description)
    {
        _description = description ?? throw new ArgumentNullException(nameof(description));
        return this;
    }

    public ActivityBuilder OfVersion(int version)
    {
        _activityVersion = version;
        return this;
    }

    public ActivityBuilder ForEngineOfProduct(Product product)
    {
        _product = product;
        return this;
    }

    public ActivityBuilder OfProductVersion(int version)
    {
        _productVersion = version;
        return this;
    }

    public ActivityBuilder WithCommandLine(string commandLine)
    {
        if (commandLine is null) throw new ArgumentNullException(nameof(commandLine));
        _commandLines.Add(commandLine);
        return this;
    }
    public ActivityBuilder WithCommandLine(Action<ICommandLineBuilder> commandLineDescriptor)
    {
        if (commandLineDescriptor is null) throw new ArgumentNullException(nameof(commandLineDescriptor));
        var commandLineBuilder = CommandLineBuilder.Create();
        commandLineDescriptor(commandLineBuilder);
        if (commandLineBuilder is not ICommandLineBuildStage commandLineBuildStage)
        {
            throw new InvalidOperationException("CommandLine cannot be built");
        }
        _commandLines.Add(commandLineBuildStage.Build());
        return this;
    }

    public ActivityBuilder IncludeParameters(Action<ParametersBuilder> parametersDescriptor)
    {
        if (parametersDescriptor is null) throw new ArgumentNullException(nameof(parametersDescriptor));
        var parametersBuilder = new ParametersBuilder();
        parametersDescriptor(parametersBuilder);
        _parameters = parametersBuilder.Build();
        return this;
    }

    public ActivityBuilder IncludeParameters(Dictionary<string, Parameter> parameters)
    {
        _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        return this;
    }

    public ActivityBuilder ForAppBundles(params string[] qualifiedAppBundleNames)
    {
        if (qualifiedAppBundleNames is null) throw new ArgumentNullException(nameof(qualifiedAppBundleNames));
        _appBundles = qualifiedAppBundleNames.ToList();
        return this;
    }

    public Activity Build()
    {
        return new Activity()
        {
            Id = _activityName,
            CommandLine = _commandLines,
            Parameters = _parameters,
            Engine = _product.GetEngine(_productVersion),
            Appbundles = _appBundles,
            Version = _activityVersion,
            Description = _description
        };
    }
}