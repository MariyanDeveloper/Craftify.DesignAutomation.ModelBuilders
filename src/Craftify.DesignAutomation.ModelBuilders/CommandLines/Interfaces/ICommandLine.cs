using System.Text;

namespace Craftify.DesignAutomation.ModelBuilders.CommandLines.Interfaces;

public interface ICommandLine
{
    StringBuilder Construct();
}