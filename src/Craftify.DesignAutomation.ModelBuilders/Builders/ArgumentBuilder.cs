using Autodesk.Forge.DesignAutomation.Model;
using Newtonsoft.Json;

namespace Craftify.DesignAutomation.ModelBuilders.Builders;

public class ArgumentBuilder
{
    public IArgument BuildInputSignedUrl(string signedUrl)
    {
        return BuildSingedUrl(signedUrl, Verb.Get);
    }
    public IArgument BuildOutputSignedUrl(string signedUrl)
    {
        return BuildSingedUrl(signedUrl, Verb.Put);
    }
    public IArgument BuildJson(object @object)
    {
        var serializedValue = JsonConvert.SerializeObject(@object);
        var argument = new XrefTreeArgument
        {
            Url = $"data:application/json,{serializedValue}",
            Verb = Verb.Get
        };
        return argument;
    }

    public IArgument BuildCallback(string callbackUrl)
    {
        return new XrefTreeArgument()
        {
            Url = callbackUrl,
            Verb = Verb.Post
        };
    }

    private IArgument BuildSingedUrl(string signedUrl, Verb verb)
    {
        var argument = new XrefTreeArgument()
        {
            Url = signedUrl,
            Verb = verb
        };
        return argument;
    }
}