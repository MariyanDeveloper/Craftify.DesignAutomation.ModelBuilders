using System.Text;
using Craftify.DesignAutomation.ModelBuilders.CommandLines.Interfaces;
using Craftify.DesignAutomation.Shared;
using Craftify.DesignAutomation.Shared.Extensions;

namespace Craftify.DesignAutomation.ModelBuilders.CommandLines;

public class LanguageCommandLine : ICommandLineDecorator
{
    private readonly ICommandLine _commandLine;
    private readonly RevitSupportedLanguage _language;

    public LanguageCommandLine(ICommandLine commandLine, RevitSupportedLanguage language)
    {
        _commandLine = commandLine ?? throw new ArgumentNullException(nameof(commandLine));
        _language = language;
    }
    public StringBuilder Construct()
    {
        var builder = _commandLine.Construct();
        return builder.Append($" /l {_language.GetCode()}");
    }
}