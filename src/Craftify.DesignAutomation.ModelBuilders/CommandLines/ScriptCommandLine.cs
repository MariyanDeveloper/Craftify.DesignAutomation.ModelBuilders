using System.Text;
using Craftify.DesignAutomation.ModelBuilders.CommandLines.Interfaces;

namespace Craftify.DesignAutomation.ModelBuilders.CommandLines;

public class ScriptCommandLine : ICommandLineDecorator
{
    private readonly ICommandLine _commandLine;
    private readonly string _scriptName;

    public ScriptCommandLine(ICommandLine commandLine, string scriptName)
    {
        _commandLine = commandLine ?? throw new ArgumentNullException(nameof(commandLine));
        _scriptName = scriptName ?? throw new ArgumentNullException(nameof(scriptName));
    }
    public StringBuilder Construct()
    {
        var builder = _commandLine.Construct();
        //TODO - check if we can add multiples scripts. If yes, create another class called ScriptInputFlag
        var script = """
                 /s "$(settings[{_scriptName}].path)"
                """;
        return builder.Append(script);
    }
}