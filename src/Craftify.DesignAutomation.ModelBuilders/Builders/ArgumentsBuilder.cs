using Autodesk.Forge.DesignAutomation.Model;
using Craftify.DesignAutomation.Shared.Constants;

namespace Craftify.DesignAutomation.ModelBuilders.Builders;

public class ArgumentsBuilder
{
    private readonly Dictionary<string, IArgument> _arguments = new();
    private ArgumentBuilder? _argumentBuilder;

    public ArgumentsBuilder Add(string name, IArgument argument)
    {
        ThrowIfNameAlreadyAdded(name);
        _arguments.Add(name, argument);
        return this;
    }
    public ArgumentsBuilder AddInputSignedUrl(string name, string signedUrl)
    {
        //TODO - why we checked it in the first place?
        AddSignedUrl(name, signedUrl, true);
        return this;
    }
    
    public ArgumentsBuilder AddOutputSignedUrl(string name, string signedUrl)
    {
        //TODO - why we checked it in the first place?
        AddSignedUrl(name, signedUrl, false);
        return this;
    }

    public ArgumentsBuilder AddOnCompleteCallback(string callbackUrl)
    {
        AssignArgumentBuilderIfNotExists();
        var argument = _argumentBuilder
            .BuildCallback(callbackUrl);
        _arguments.Add(SpecialOutputArguments.OnComplete, argument);
        return this;
    }
    
    public ArgumentsBuilder AddJson(string name, object @object)
    {
        ThrowIfNameAlreadyAdded(name);
        AssignArgumentBuilderIfNotExists();
        var argument = _argumentBuilder.BuildJson(@object);
        _arguments.Add(name, argument);
        return this;
    }
    
    public Dictionary<string, IArgument> Build() => _arguments;

    private void AddSignedUrl(string name, string signedUrl, bool isInput)
    {        
        ThrowIfNameAlreadyAdded(name);
        AssignArgumentBuilderIfNotExists();
        var argument = isInput
            ? _argumentBuilder.BuildInputSignedUrl(signedUrl)
            : _argumentBuilder.BuildOutputSignedUrl(signedUrl);
        _arguments.Add(name, argument);

    }
    private void ThrowIfNameAlreadyAdded(string name)
    {
        if (_arguments.ContainsKey(name))
        {
            throw new InvalidOperationException();
        }
    }
    private void AssignArgumentBuilderIfNotExists()
    {
        _argumentBuilder ??= new ArgumentBuilder();
    }

}