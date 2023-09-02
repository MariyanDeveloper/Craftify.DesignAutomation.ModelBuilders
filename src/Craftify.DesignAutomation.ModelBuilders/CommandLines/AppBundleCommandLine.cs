using System.Text;
using Craftify.DesignAutomation.ModelBuilders.CommandLines.Interfaces;

namespace Craftify.DesignAutomation.ModelBuilders.CommandLines;

public class AppBundleCommandLine : ICommandLineDecorator
{
    private readonly ICommandLine _commandLine;
    private readonly string _appBundle;

    public AppBundleCommandLine(ICommandLine commandLine, string appBundle)
    {
        _commandLine = commandLine ?? throw new ArgumentNullException(nameof(commandLine));
        _appBundle = appBundle ?? throw new ArgumentNullException(nameof(appBundle));
    }
    public StringBuilder Construct()
    {
        var builder = _commandLine.Construct();
        //TODO - it seems like we cannot include multiple app bundles in a single command line, therefore we need to create multiple : https://aps.autodesk.com/blog/use-multiple-app-bundles
        var lineToAppend = $"""
                 /al "$(appbundles[{_appBundle}].path)"
                """;
        builder.Append(lineToAppend);
        return builder;
    }
}