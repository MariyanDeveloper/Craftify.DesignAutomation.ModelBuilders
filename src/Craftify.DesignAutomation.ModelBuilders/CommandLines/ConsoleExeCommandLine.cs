using System.Text;
using Craftify.DesignAutomation.ModelBuilders.CommandLines.Interfaces;
using Craftify.DesignAutomation.Shared;
using Craftify.DesignAutomation.Shared.Constants;

namespace Craftify.DesignAutomation.ModelBuilders.CommandLines;

public class ConsoleExeCommandLine : ICommandLineDecorator
{
    private readonly ICommandLine _commandLine;
    private readonly Product _product;

    public ConsoleExeCommandLine(
        ICommandLine commandLine, Product product)
    {
        _commandLine = commandLine ?? throw new ArgumentNullException(nameof(commandLine));
        _product = product;
    }
    public StringBuilder Construct()
    {
        var builder = _commandLine.Construct();
        var exeName = GetExecutableName();
        return builder.Append(@$"\{exeName}");
    }

    private string GetExecutableName()
    {
        var exeName = _product switch
        {
            Product.Revit => AutodeskConsoleApps.Revit,
            _ => throw new ArgumentException("Unsupported product")
        };
        return exeName;
    }
}