using System.Security.Cryptography;
using System.Text;
using Signature.Service.Models;

namespace Signature.Service;

public static class HashChunk
{
    public static Hash GetHash(Chunk chunk)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            try
            {
                return new Hash(chunk.Id, sha256.ComputeHash(chunk.Bytes).ToHexadecimalString());
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }

    private static string ToHexadecimalString(this byte[] bytes)
    {
        var stringBuilder = new StringBuilder();

        foreach (var value in bytes)
        {
            stringBuilder.AppendFormat($"{value:X2}");
        }

        return stringBuilder.ToString();
    }

}