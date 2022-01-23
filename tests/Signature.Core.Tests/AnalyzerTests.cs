using Xunit;

namespace Signature.Core.Tests;

public class AnalyzerTests
{
    [Fact]
    public void ToBytes_Megabyte_BinaryBytes()
    {
        // Arrange
        int expectedBytes = 1048576;
        int mb = 1;

        // Act
        long actualBytes = mb.ToBytes();

        // Assert
        Assert.Equal(expectedBytes, actualBytes);
    }

    [Theory]
    [InlineData(10, 100, 10)]
    [InlineData(20, 100, 5)]
    [InlineData(50, 100, 2)]
    [InlineData(1048576, 103809024, 99)]
    public void GetChunkBuffer_CorrectSizes_Quotient(int chunkSize, long bufferLimit, int bufferSize)
    {
        // Arrange
        int expectedBufferSize = bufferSize;

        // Act
        int actualBufferSize = Analyzer.GetChunkBuffer(chunkSize, bufferLimit);

        // Assert
        Assert.Equal(expectedBufferSize, actualBufferSize);
    }
}

