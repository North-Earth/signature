namespace Signature.Core.Models;

internal class Hash
{
    public int Id { get; }

    public string HexadecimalValue { get; }

    public Hash(int id, string hexadecimalValue)
    {
        Id = id;
        HexadecimalValue = hexadecimalValue;
    }

    public override string ToString() => $"{Id} - {HexadecimalValue}";
}