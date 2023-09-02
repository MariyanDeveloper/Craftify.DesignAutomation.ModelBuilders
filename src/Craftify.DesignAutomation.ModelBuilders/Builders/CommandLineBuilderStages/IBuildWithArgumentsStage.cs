using Craftify.DesignAutomation.Shared;

namespace Craftify.DesignAutomation.ModelBuilders.Builders.CommandLineBuilderStages;

public interface IBuildWithArgumentsStage : ICommandLineBuildStage, IIntroduceInputFlagStage
{
    
    IBuildWithArgumentsStage OfLanguage(RevitSupportedLanguage language);
    IBuildWithArgumentsStage WithScript(string scriptName);
    IBuildWithArgumentsStage IncludeAppBundle(string appBundle);
}