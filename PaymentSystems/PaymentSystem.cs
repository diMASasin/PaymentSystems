namespace PaymentSystems;

public class PaymentSystem
{
    private readonly StringHashComputer _stringHashComputer;
    private readonly Func<Order, string> _getStringToHash;
    private readonly Func<Order, string, string> _getLink;

    public PaymentSystem(StringHashComputer stringHashComputer, Func<Order, string> stringToHash,
        Func<Order, string, string> linkSignature)
    {
        _getLink = linkSignature ?? throw new ArgumentNullException(nameof(linkSignature));
        _getStringToHash = stringToHash ?? throw new ArgumentNullException(nameof(stringToHash));
        _stringHashComputer = stringHashComputer ?? throw new ArgumentNullException(nameof(stringHashComputer));
    }

    public string GetPayingLink(Order order)
    {
        if (order == null)
            throw new ArgumentNullException(nameof(order));
        
        string toHash = _getStringToHash(order);
        string hash = _stringHashComputer.HashString(toHash);
        string link = _getLink(order, hash);

        return link;
    }
}