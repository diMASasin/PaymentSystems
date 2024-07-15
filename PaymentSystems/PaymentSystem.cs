namespace PaymentSystems;

public class PaymentSystem
{
    private readonly StringHashComputer _stringHashComputer;
    private readonly Func<Order, string> _getStringToHash;
    private readonly Func<Order, string, string> _getLink;

    public PaymentSystem(StringHashComputer stringHashComputer, Func<Order, string> stringToHash, 
        Func<Order, string, string> linkSignature)
    {
        _getLink = linkSignature;
        _getStringToHash = stringToHash;
        _stringHashComputer = stringHashComputer;
    }
    
    public string GetPayingLink(Order order)
    {
        string toHash = _getStringToHash(order);
        string hash = _stringHashComputer.HashString(toHash);
        string link = _getLink(order, hash);
        
        return link;
    }
}