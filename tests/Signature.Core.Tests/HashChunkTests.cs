using Signature.Core.Models;
using System.Text;
using Xunit;

namespace Signature.Core.Tests;

public class HashChunkTests
{
    [Fact]
    public void GetHash_StringToBytes_ShouldBeSHA256()
    {
        // Arrange
        Hash expectedHash = new Hash(0, "4AD756D0D06A4331CB28F504FDEC768C53205CA62BAF83E9DDA5668E1C5CBFBA");
        byte[] bytes = Encoding.UTF8.GetBytes("Aleksey Kukushkin");
        Chunk chunk = new Chunk(0, bytes);

        // Act
        Hash actualHash = HashChunk.GetHash(chunk);

        // Assert
        Assert.Equal(expectedHash.Id, actualHash.Id);
        Assert.Equal(expectedHash.HexadecimalValue, actualHash.HexadecimalValue);
        Assert.Equal(expectedHash.ToString(), actualHash.ToString());
    }

    [Fact]
    public void ToHexadecimalString_StringToBytes_HexadecimalString()
    {
        // Arrange
        string expectedHexadecimal = "416C656B736579204B756B7573686B696E";
        byte[] bytes = Encoding.UTF8.GetBytes("Aleksey Kukushkin");

        // Act
        string actualHexadecimal = bytes.ToHexadecimalString();

        // Assert
        Assert.Equal(expectedHexadecimal, actualHexadecimal);
    }
}
