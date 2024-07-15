using System.Security.Cryptography;

namespace PaymentSystems;

public class Program
{
    //Выведите платёжные ссылки для трёх разных систем платежа: 
    //pay.system1.ru/order?amount=12000RUB&hash={MD5 хеш ID заказа}
    //order.system2.ru/pay?hash={MD5 хеш ID заказа + сумма заказа}
    //system3.com/pay?amount=12000&curency=RUB&hash={SHA-1 хеш сумма заказа + ID заказа + секретный ключ от системы}
    
    private static void Main(string[] args)
    {
        const string secretKey = "it is really very secret key";

        var md5StringHashComputer = new StringHashComputer(MD5.Create());
        var sha1StringHashComputer = new StringHashComputer(SHA1.Create());
        
        var idPaymentSystem = new PaymentSystem(
            stringHashComputer: md5StringHashComputer, 
            stringToHash: order => $"{order.Id}", 
            linkSignature: (order, hashString) => $"pay.system1.ru/order?amount={order.Amount}RUB&hash={hashString}");
        
        var idAndAmountPaymentSystem = new PaymentSystem(
            stringHashComputer: md5StringHashComputer, 
            stringToHash: order => $"{order.Id}{order.Amount}",
            linkSignature: (order, hashString) => $"order.system2.ru/pay?hash={hashString}");
        
        var sha1PaymentSystem = new PaymentSystem(
            stringHashComputer: sha1StringHashComputer, 
            stringToHash: order => $"{order.Id}{order.Amount}{secretKey}",
            linkSignature: (order, hashString) => $"system3.com/pay?amount={order.Amount}&curency=RUB&hash={hashString}");
        
        var order = new Order(1, 2000);

        string link = idPaymentSystem.GetPayingLink(order);
        Console.WriteLine(link);

        link = idAndAmountPaymentSystem.GetPayingLink(order);
        Console.WriteLine(link);

        link = sha1PaymentSystem.GetPayingLink(order);
        Console.WriteLine(link);
    }
}