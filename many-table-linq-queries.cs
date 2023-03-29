 using(var db = new NorthwindContext())
              {
                var customers = db
                .Customers
                .Where(c => c.Orders.Any())
                .Select(c => new CustomerDemo{
                    Name = c.ContactName,
                    OrderCount = c.Orders.Count(),
                    Orders = c.Orders.Select(p => new OrderDemo{
                      OrderId = p.OrderId,
                      Total  = db.OrderDetails.Where(od => od.OrderId==p.OrderId).Sum(od => od.UnitPrice * od.Quantity),
                      Products = db.OrderDetails
                      .Where(od => od.OrderId == p.OrderId)
                      .Select(od => new ProductDemo{
                        Id = od.Product.ProductId,
                        Name = od.Product.ProductName,
                        Price  = od.Product.UnitPrice,
                        Quantity = od.Quantity
                      })
                      .ToList()
                    }).ToList()
                })
                .OrderBy(p => p.Orders.Count())
                .ToList();

                foreach(var c  in customers)
                {
                    Console.WriteLine($"Name : {c.Name} - Order Count : {c.OrderCount}");

                    foreach(var orders in c.Orders)
                    {
                        Console.WriteLine($"OrderId: {orders.OrderId} - {orders.Total}");

                        foreach(var p in orders.Products)
                        {
                            Console.WriteLine($"-- Product Name : {p.Name} - Price : {p.Price} : Quantity :{p.Quantity} Total : {p.Price * p.Quantity}");
                            
                        }
                        
                    }
                    
                }
              }
