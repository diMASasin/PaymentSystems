using System.Security.Cryptography;
using static System.Text.Encoding;

namespace PaymentSystems;

public class StringHashComputer
{
    private readonly HashAlgorithm _hashAlgorithm;

    public StringHashComputer(HashAlgorithm hashAlgorithm)
    {
        _hashAlgorithm = hashAlgorithm ?? throw new ArgumentNullException(nameof(hashAlgorithm));
    }
    
    public string HashString(string toHash)
    {
        if (string.IsNullOrEmpty(toHash) == true) 
            throw new ArgumentNullException(nameof(toHash));
        
        byte[] toHashBytes = ASCII.GetBytes(toHash);
        byte[] hash = _hashAlgorithm.ComputeHash(toHashBytes);
        string hashString = BitConverter.ToString(hash).Replace("-", "").ToLower();
        
        return hashString;
    }
}