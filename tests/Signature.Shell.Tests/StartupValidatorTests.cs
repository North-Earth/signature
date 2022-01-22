using Xunit;

namespace Signature.Shell.Tests;

public class StartupValidatorTests
{
    private const string FakePath = @"C:\Path";
    private const string HelpArg = "--help";
    private const string ShortHelpArg = "-h";
    private const string ChunkSizeArg = "--chunkSize";
    private const string MemoryBufferLimitArg = "--bufferLimit";

    [Theory]
    [InlineData(ShortHelpArg)]
    [InlineData(HelpArg)]
    public void IsNeedHelp_OnlyHelpFlag_True(string commandLineArg)
    {
        // Arrange
        var commandLineArgs = new string[] { commandLineArg };

        // Act
        var isNeedHelp = StartupValidator.IsNeedHelp(commandLineArgs);

        // Assert
        Assert.True(isNeedHelp);
    }

    [Theory]
    [InlineData("", ShortHelpArg)]
    [InlineData(ShortHelpArg, "")]
    [InlineData("", ShortHelpArg, "")]
    [InlineData("", HelpArg)]
    [InlineData(HelpArg, "")]
    [InlineData("", HelpArg, "")]
    public void IsNeedHelp_HelpFlagWithOtherFlags_True(params string[] args)
    {
        // Arrange
        var commandLineArgs = args;

        // Act
        var isNeedHelp = StartupValidator.IsNeedHelp(commandLineArgs);

        // Assert
        Assert.True(isNeedHelp);
    }

    [Fact]
    public void IsNeedHelp_EmptyFlags_True()
    {
        // Arrange
        var commandLineArgs = System.Array.Empty<string>();

        // Act
        var isNeedHelp = StartupValidator.IsNeedHelp(commandLineArgs);

        // Assert
        Assert.True(isNeedHelp);
    }

    [Fact]
    public void ValidateStartupArgs_SinglePathArg_NotException()
    {
        // Arrange
        var commandLineArgs = new string[] { FakePath };

        // Act
        var exception = Record.Exception(() => StartupValidator.ValidateStartupArgs(commandLineArgs));

        // Assert
        Assert.Null(exception);
    }

    [Theory]
    [InlineData(FakePath, ChunkSizeArg)]
    [InlineData(FakePath, ChunkSizeArg, "0", MemoryBufferLimitArg)]
    public void ValidateStartupArgs_ParamWithoutNumber_ThrowException(params string[] args)
    {
        // Arrange
        var commandLineArgs = args;

        // Act
        var exception = Record.Exception(() => StartupValidator.ValidateStartupArgs(commandLineArgs));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void ValidateStartupArgs_ParamNumberNotNumber_ThrowException()
    {
        // Arrange
        var commandLineArgs = new string[] { FakePath, ChunkSizeArg, "number" };

        // Act
        var exception = Record.Exception(() => StartupValidator.ValidateStartupArgs(commandLineArgs));

        // Assert
        Assert.NotNull(exception);
    }
}