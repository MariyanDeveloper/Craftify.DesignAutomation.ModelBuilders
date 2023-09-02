using System.Text;
using Craftify.DesignAutomation.ModelBuilders.CommandLines.Interfaces;

namespace Craftify.DesignAutomation.ModelBuilders.CommandLines;

public class InputFlagCommandLine : IInputCommandDecorator
{
    private readonly ICommandLine _commandLine;

    public InputFlagCommandLine(ICommandLine commandLine)
    {
        _commandLine = commandLine ?? throw new ArgumentNullException(nameof(commandLine));
    }
    public StringBuilder Construct()
    {
        var builder = _commandLine.Construct();
        return builder.Append(" /i");
    }
}