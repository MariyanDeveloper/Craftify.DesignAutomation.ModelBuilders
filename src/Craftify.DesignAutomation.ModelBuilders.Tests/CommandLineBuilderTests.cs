using Craftify.DesignAutomation.ModelBuilders.Builders;
using Craftify.DesignAutomation.Shared;

namespace Craftify.DesignAutomation.ModelBuilders.Tests;

public class CommandLineBuilderTests
{
    [Fact]
    public void Build_ForRevitProductOnly_ReturnsCorrectCommandLine()
    {
        var product = Product.Revit;
        
        var commandLine =  CommandLineBuilder
            .Create()
            .ForProduct(product)
            .Build();

        var expectedResult = """
                             $(engine.path)\revitcoreconsole.exe
                             """;
        
        Assert.Equal(commandLine, expectedResult);

    }
    [Fact]
    public void Build_ForRevitProduct_WithPerArgumentAppend_AndAppBundle_OfLanguage_ReturnsCorrectCommandLine()
    {
        var product = Product.Revit;
        var appBundleName = "appBundle";
        
        var commandLine =  CommandLineBuilder
            .Create()
            .ForProduct(product)
            .IntroduceInputFlag()
                .Append("revit_document")
                .Append("revit_params")
            .IncludeAppBundle(appBundleName)
            .OfLanguage(RevitSupportedLanguage.AmericanEnglish)
            .Build();

        var expectedResult = """
                             $(engine.path)\revitcoreconsole.exe /i "$(args[revit_document].path)" "$(args[revit_params].path)" /al "$(appbundles[appBundle].path)" /l ENU
                             """;
        
        Assert.Equal(commandLine, expectedResult);
    }
    
    [Fact]
    public void Build_ForRevitProduct_MultipleArgumentsAppendAtOnce_AndAppBundle_OfLanguage_ReturnsCorrectCommandLine()
    {
        var product = Product.Revit;
        var appBundleName = "appBundle";
        var commandLine =  CommandLineBuilder
            .Create()
            .ForProduct(product)
            .IntroduceInputFlag()
            .Append("revit_document", "revit_params")
            .IncludeAppBundle(appBundleName)
            .OfLanguage(RevitSupportedLanguage.AmericanEnglish)
            .Build();

        var expectedResult = """
                             $(engine.path)\revitcoreconsole.exe /i "$(args[revit_document].path)" "$(args[revit_params].path)" /al "$(appbundles[appBundle].path)" /l ENU
                             """;
        
        Assert.Equal(commandLine, expectedResult);
    }
}