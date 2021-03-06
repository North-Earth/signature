using Signature.Core.Models;
using System.Security.Cryptography;
using System.Text;

namespace Signature.Core;

internal static class HashChunk
{
    public static Hash GetHash(Chunk chunk)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            try
            {
                return new Hash(chunk.Id, sha256.ComputeHash(chunk.Bytes).ToHexadecimalString());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public static string ToHexadecimalString(this byte[] bytes)
    {
        var stringBuilder = new StringBuilder();

        foreach (var value in bytes)
        {
            stringBuilder.AppendFormat($"{value:X2}");
        }

        return stringBuilder.ToString();
    }

}