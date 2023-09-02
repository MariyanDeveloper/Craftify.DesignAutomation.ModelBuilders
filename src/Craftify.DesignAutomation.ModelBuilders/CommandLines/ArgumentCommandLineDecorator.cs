using System.Text;
using Craftify.DesignAutomation.ModelBuilders.CommandLines.Interfaces;

namespace Craftify.DesignAutomation.ModelBuilders.CommandLines;

public class ArgumentCommandLineDecorator : IInputCommandDecorator
{
    private readonly IInputCommandDecorator _inputCommandDecorator;
    private readonly string _argumentName;

    public ArgumentCommandLineDecorator(
        IInputCommandDecorator inputCommandDecorator,
        string argumentName)
    {
        _inputCommandDecorator = inputCommandDecorator ?? throw new ArgumentNullException(nameof(inputCommandDecorator));
        _argumentName = argumentName ?? throw new ArgumentNullException(nameof(argumentName));
    }
    public StringBuilder Construct()
    {
        var builder = _inputCommandDecorator.Construct();
        var lineToAppend = $"""
                 "$(args[{_argumentName}].path)"
                """;
        return builder.Append(lineToAppend);
    }
}