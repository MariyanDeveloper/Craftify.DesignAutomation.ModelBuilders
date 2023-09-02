using Craftify.DesignAutomation.ModelBuilders.Builders.CommandLineBuilderStages;
using Craftify.DesignAutomation.ModelBuilders.CommandLines;
using Craftify.DesignAutomation.ModelBuilders.CommandLines.Interfaces;
using Craftify.DesignAutomation.Shared;

namespace Craftify.DesignAutomation.ModelBuilders.Builders;


public class CommandLineBuilder : ICommandLineBuilder, IContinuationFromArgumentAppendStage
{
    private ICommandLine _commandLine = new EngineCommandLine();
    private CommandLineBuilder(){}

    public static ICommandLineBuilder Create()
    {
        return new CommandLineBuilder();
    }
    public IBuildWithArgumentsStage ForProduct(Product product)
    {
        _commandLine = new ConsoleExeCommandLine(_commandLine, product);
        return this;
    }

    public IBuildWithArgumentsStage OfLanguage(RevitSupportedLanguage language)
    {
        _commandLine = new LanguageCommandLine(_commandLine, language);
        return this;
    }

    public IBuildWithArgumentsStage WithScript(string scriptName)
    {
        if (scriptName is null) throw new ArgumentNullException(nameof(scriptName));
        _commandLine = new ScriptCommandLine(_commandLine, scriptName);
        return this;
    }

    public IBuildWithArgumentsStage IncludeAppBundle(string appBundle)
    {
        if (appBundle is null) throw new ArgumentNullException(nameof(appBundle));
        _commandLine = new AppBundleCommandLine(_commandLine, appBundle);
        return this;
    }
    public IArgumentAppendStage IntroduceInputFlag()
    {
        _commandLine = new InputFlagCommandLine(_commandLine);
        return this;
    }
    public IContinuationFromArgumentAppendStage Append(string argumentName)
    {
        if (argumentName is null) throw new ArgumentNullException(nameof(argumentName));
        if (_commandLine is not IInputCommandDecorator inputCommandDecorator)
        {
            throw new InvalidOperationException(
                $"You can only append decorators of type: ${typeof(IInputCommandDecorator)}");
        }
        _commandLine = new ArgumentCommandLineDecorator(inputCommandDecorator, argumentName);
        return this;
    }

    public IContinuationFromArgumentAppendStage Append(params string[] argumentNames)
    {
        foreach (var argumentName in argumentNames)
        {
            Append(argumentName);
        }

        return this;
    }

    public string Build()
    {
        return _commandLine.Construct().ToString();
    }
    
}