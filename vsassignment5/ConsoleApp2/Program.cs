using System;
using System.Collections.Generic;
using System.Linq;

public class Order
{
    public string OrderId { get; set; }
    public string Customer { get; set; }
    public List<OrderDetails> Details { get; set; } = new List<OrderDetails>();

    public double TotalAmount => CalculateTotalAmount();

    private double CalculateTotalAmount()
    {
        return Details.Sum(detail => detail.Amount);
    }

    public override string ToString()
    {
        return $"订单号: {OrderId}, 客户: {Customer}, 总金额: {TotalAmount}";
    }
}

public class OrderDetails
{
    public string ProductName { get; set; }
    public double Amount { get; set; }

    public override string ToString()
    {
        return $"商品名称: {ProductName}, 金额: {Amount}";
    }
}

public class OrderService
{
    private Dictionary<string, Order> orders = new Dictionary<string, Order>();

    public void AddOrder(Order order)
    {
        if (orders.ContainsKey(order.OrderId))
        {
            throw new InvalidOperationException("订单已存在！");
        }
        orders.Add(order.OrderId, order);
    }

    public void DeleteOrder(string orderId)
    {
        if (!orders.ContainsKey(orderId))
        {
            throw new InvalidOperationException("订单不存在！");
        }
        orders.Remove(orderId);
    }

    public void UpdateOrder(string orderId, string newCustomer)
    {
        if (!orders.ContainsKey(orderId))
        {
            throw new InvalidOperationException("订单不存在！");
        }
        orders[orderId].Customer = newCustomer;
    }

    public List<Order> QueryOrders(string keyword = "", double? minAmount = null, double? maxAmount = null)
    {
        var query = orders.Values.AsQueryable();

        if (!string.IsNullOrEmpty(keyword))
        {
            query = query.Where(o => o.OrderId.Contains(keyword) || o.Customer.Contains(keyword));
        }
        if (minAmount.HasValue)
        {
            query = query.Where(o => o.TotalAmount >= minAmount);
        }
        if (maxAmount.HasValue)
        {
            query = query.Where(o => o.TotalAmount <= maxAmount);
        }

        return query.OrderBy(o => o.TotalAmount).ToList();
    }
}

class Program
{
    static void Main()
    {
        var orderService = new OrderService();
        while (true)
        {
            Console.WriteLine("请选择操作：");
            Console.WriteLine("1. 添加订单");
            Console.WriteLine("2. 删除订单");
            Console.WriteLine("3. 修改订单");
            Console.WriteLine("4. 查询订单");
            Console.WriteLine("5. 退出");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddOrder(orderService);
                    break;
                case "2":
                    DeleteOrder(orderService);
                    break;
                case "3":
                    UpdateOrder(orderService);
                    break;
                case "4":
                    QueryOrders(orderService);
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("无效的输入，请重新选择！");
                    break;
            }
        }
    }

    static void AddOrder(OrderService orderService)
    {
        Console.Write("请输入订单号：");
        var orderId = Console.ReadLine();
        Console.Write("请输入客户名称：");
        var customer = Console.ReadLine();
        var order = new Order { OrderId = orderId, Customer = customer };

        while (true)
        {
            Console.Write("请输入商品名称（输入'exit'结束）：");
            var productName = Console.ReadLine();
            if (productName.ToLower() == "exit") break;
            Console.Write("请输入商品金额：");
            if (!double.TryParse(Console.ReadLine(), out double amount))
            {
                Console.WriteLine("金额输入无效！");
                continue;
            }
            order.Details.Add(new OrderDetails { ProductName = productName, Amount = amount });
        }

        try
        {
            orderService.AddOrder(order);
            Console.WriteLine("订单添加成功！");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void DeleteOrder(OrderService orderService)
    {
        Console.Write("请输入要删除的订单号：");
        var orderId = Console.ReadLine();
        try
        {
            orderService.DeleteOrder(orderId);
            Console.WriteLine("订单删除成功！");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void UpdateOrder(OrderService orderService)
    {
        Console.Write("请输入要修改的订单号：");
        var orderId = Console.ReadLine();
        Console.Write("请输入新的客户名称：");
        var newCustomer = Console.ReadLine();
        try
        {
            orderService.UpdateOrder(orderId, newCustomer);
            Console.WriteLine("订单修改成功！");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void QueryOrders(OrderService orderService)
    {
        Console.Write("请输入查询关键字（可选）：");
        var keyword = Console.ReadLine();
        Console.Write("请输入最小金额（可选）：");
        double? minAmount = null;
        if (!string.IsNullOrEmpty(Console.ReadLine()) && double.TryParse(Console.ReadLine(), out double min))
        {
            minAmount = min;
        }
        Console.Write("请输入最大金额（可选）：");
        double? maxAmount = null;
        if (!string.IsNullOrEmpty(Console.ReadLine()) && double.TryParse(Console.ReadLine(), out double max))
        {
            maxAmount = max;
        }

        var orders = orderService.QueryOrders(keyword, minAmount, maxAmount);
        Console.WriteLine("查询结果：");
        foreach (var order in orders)
        {
            Console.WriteLine(order);
        }
    }
}
