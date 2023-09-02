using Autodesk.Forge.DesignAutomation.Model;
using Craftify.DesignAutomation.ModelBuilders.Builders;
using Craftify.DesignAutomation.Shared;

namespace Craftify.DesignAutomation.ModelBuilders.Tests;

public class ActivityBuilderTests
{
    [Fact]
    public void Build_WhenBuild_ReturnsCorrectActivity()
    {
        var appBundleName = "someAppBundle";
        var activity = new ActivityBuilder()
            .OfName("activty")
            .Describe("description")
            .OfVersion(1)
            .ForEngineOfProduct(Product.Revit)
            .OfProductVersion(2023)
            .WithCommandLine(commandLineDescriptor =>
            {
                commandLineDescriptor
                    .ForProduct(Product.Revit)
                    .IntroduceInputFlag()
                        .Append("revit_document", "revit_params")
                    .IncludeAppBundle(appBundleName)
                    .OfLanguage(RevitSupportedLanguage.AmericanEnglish);
            })
            .IncludeParameters(parametersDescriptor =>
            {
                parametersDescriptor
                    .AddInputParameter("input", "input.rvt")
                    .AddInputParameter("params", "params.json")
                    .AddOutputParameter("output", "output.rvt");
            })
            .ForAppBundles(appBundleName)
            .Build();
        //TODO - create an expected result
        var expectedResult = new Activity();
        Assert.False(true);

    }
}