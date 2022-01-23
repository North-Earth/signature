using Xunit;

namespace Signature.Core.Tests;

public class ValidatorTests
{
    [Fact]
    public void CheckChunkSize_NegativeChunkSize_Exception()
    {
        // Arrange
        int chunkSize = -100;
        int fileSize = 1;
        int bufferLimitBytes = 1;

        // Act
        System.Exception exception = Record.Exception(
            () => Validator.CheckChunkSize(chunkSize, fileSize, bufferLimitBytes));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void CheckChunkSize_ChunkSizeLargerThanFileSize_Exception()
    {
        // Arrange
        int chunkSize = 10;
        int fileSize = 1;
        int bufferLimitBytes = 100;

        // Act
        System.Exception exception = Record.Exception(
            () => Validator.CheckChunkSize(chunkSize, fileSize, bufferLimitBytes));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void CheckChunkSize_ChunkSizeLargerThanBuffer_Exception()
    {
        // Arrange
        int chunkSize = 10;
        int fileSize = 1;
        int bufferLimitBytes = 1;

        // Act
        System.Exception exception = Record.Exception(
            () => Validator.CheckChunkSize(chunkSize, fileSize, bufferLimitBytes));

        // Assert
        Assert.NotNull(exception);
    }
}
