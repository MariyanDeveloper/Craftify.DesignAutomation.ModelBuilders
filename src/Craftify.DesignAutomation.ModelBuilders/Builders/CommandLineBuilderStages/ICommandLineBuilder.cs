using Craftify.DesignAutomation.Shared;

namespace Craftify.DesignAutomation.ModelBuilders.Builders.CommandLineBuilderStages;

public interface ICommandLineBuilder
{
    IBuildWithArgumentsStage ForProduct(Product product);
}