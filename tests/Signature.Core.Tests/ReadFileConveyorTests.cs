using Signature.Core.Models;
using Xunit;

namespace Signature.Core.Tests;
public class ReadFileConveyorTests
{
    [Fact]
    public void DequeueChunk_GetChunkFromEmptyQueue_Null()
    {
        // Arrange
        var conveyor = new ReadFileConveyor(1);

        // Act
        var chunk = conveyor.DequeueChunk();

        // Assert
        Assert.Null(chunk);
    }

    [Fact]
    public void DequeueChunk_GetChunkFromNotEmptyQueue_Chunk()
    {
        // Arrange
        var conveyor = new ReadFileConveyor(3);
        var chunkFirst = new Chunk(0, new byte[1] { 1 });
        var chunkSecond = new Chunk(1, new byte[1] { 2 });
        var chunkThird = new Chunk(2, new byte[1] { 3 });
        conveyor.EnqueueChunk(chunkFirst);
        conveyor.EnqueueChunk(chunkSecond);
        conveyor.EnqueueChunk(chunkThird);

        // Act
        var chunk = conveyor.DequeueChunk();

        // Assert
        Assert.Equal(chunkFirst, chunk);
    }

    [Fact]
    public void EnqueueChunk_AddChunckToQueue_MustBeAdded()
    {
        // Arrange
        var conveyor = new ReadFileConveyor(1);
        var expected = new Chunk(0, new byte[1] { 1 });

        // Act
        conveyor.EnqueueChunk(expected);
        conveyor.ChunkQueue.TryPeek(out Chunk? actual);

        // Assert
        Assert.Equal(expected, actual);
    }
}
