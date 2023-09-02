using System.Text;
using Craftify.DesignAutomation.ModelBuilders.CommandLines.Interfaces;

namespace Craftify.DesignAutomation.ModelBuilders.CommandLines;

public class EngineCommandLine : ICommandLine
{
    public StringBuilder Construct() =>
        new StringBuilder("$(engine.path)");
}