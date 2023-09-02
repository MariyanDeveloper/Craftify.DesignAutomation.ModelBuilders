namespace Craftify.DesignAutomation.ModelBuilders.Builders.CommandLineBuilderStages;

public interface IArgumentAppendStage
{
    IContinuationFromArgumentAppendStage Append(string argumentName);
    IContinuationFromArgumentAppendStage Append(params string[] argumentNames);
}