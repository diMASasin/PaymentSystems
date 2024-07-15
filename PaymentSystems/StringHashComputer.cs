using System.Security.Cryptography;
using static System.Text.Encoding;

namespace PaymentSystems;

public class StringHashComputer
{
    private readonly HashAlgorithm _hashAlgorithm;

    public StringHashComputer(HashAlgorithm hashAlgorithm)
    {
        _hashAlgorithm = hashAlgorithm;
    }
    
    public string HashString(string toHash)
    {
        byte[] toHashBytes = ASCII.GetBytes(toHash);
        byte[] hash = _hashAlgorithm.ComputeHash(toHashBytes);
        string hashString = BitConverter.ToString(hash).Replace("-", "").ToLower();
        
        return hashString;
    }
}