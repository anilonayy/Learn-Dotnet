    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.EntityFrameworkCore;
    using _netcore.Data.Entities;


    namespace MyApp
    {
        // Entity Class
        // Product (Id,Name,Price) => Product(Id,Name,Price)  ( Veri tabanı tablosundaki )

        // Convention - Direkt ID , CategoryId Olarak tanımalamalara denir.
        // Data Annotation - Idsi Belirsiz alana [Key] ile verilen ifadelere veya [MaxLength()] gibi ifadelere denir.
        // Fluent API - Many To Many  , Fluent API Olmadan yapılamaz.
        public class ShopContext : DbContext
        {
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder
                .UseSqlServer(@"Data Source=ANIL\SQLEXPRESS;Initial Catalog=Shop;Integrated Security=SSPI;TrustServerCertificate=true");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {

                modelBuilder.Entity<User>()
                    .HasIndex(u => u.Username)
                    .IsUnique();

                // ProductCategory Entitysini çağırdık ve bu entityin anahtarlarını belirttik.
                modelBuilder.Entity<ProductCategory>()
                    .HasKey(t => new {t.CategoryId,t.ProductId});
                
                modelBuilder.Entity<ProductCategory>()
                    .HasOne(pc => pc.Product)
                    .WithMany(pc => pc.ProductsCategories)
                    .HasForeignKey(pc => pc.ProductId);

                modelBuilder.Entity<ProductCategory>()
                    .HasOne(c => c.Category)
                    .WithMany(c => c.ProductsCategories)
                    .HasForeignKey(c => c.CategoryId);

            }

            public DbSet<Product> Products {get;set;}
            public DbSet<Category> Categories { get; set; }

            public DbSet<User> Users { get; set; }        
            public DbSet<Address> Addresses { get; set; }

            public DbSet<Customer> Customers {get;set;}
            public DbSet<Supplier> Suppliers {get;set;}
            
        }

        public static class DataSeeding
        {
            public static void Seed(DbContext context)
            {
                if(context.Database.GetPendingMigrations().Count()==0) // Eğer veritabanına iletilicek tüm migrationlar tamamlandıysa bekleyen yok ise
                {
                    if(context is ShopContext)
                    {
                        ShopContext db = context as ShopContext;

                        if(db.Products.Count()==0)
                        {
                            // Ürünleri Ekle
                            var products = new List<Product>{
                                new Product{Name="Samsung S7",Price=2500},
                                new Product{Name="Samsung S8",Price=3500},
                                new Product{Name="Samsung S9",Price=4500},
                                new Product{Name="Samsung S10",Price=7000}
                            };
                            db.Products.AddRange(products);
                        }
                        
                        if(db.Categories.Count()==0)
                        {
                            // Kategorileri Ekle
                            var categories = new List<Category>{
                                new Category{Name="Telefon"},
                                new Category{Name="Elektronik"},
                                new Category{Name="Bilgisayar"}
                            };
                            db.Categories.AddRange(categories);
                        }

                        db.SaveChanges();
                    }
                }
            }
        }
        public class User
        {

            public int Id { get; set; }

            public string Username { get; set; }
            
            
            public string Name { get; set; }
            [Column(TypeName ="varchar(20)")]
            public string Email { get; set; }
            
            public Customer Customer {get;set;}
            public Supplier Supplier {get;set;}
            public List<Address> Adresses { get; set; }  // Navigation Property
        }

            public class Customer
            {
                public int Id { get; set; }
                
                public string IdentityNumber { get; set; }
                
                public string FirstName { get; set; }
                public string LastName { get; set; }
                
                public User User { get; set; } // Navigation Property - Foreign Key Görevi Görür.
                
                public int UserId { get; set; }
                
            }

        public class Supplier
        {
            public int Id { get; set; }
            
            public string Name { get; set; }
            
            public string TaxNumber { get; set; }
            
            
            public User User { get; set; } // Navigation Property - Foreign Key Görevi Görür.
            
            public int UserId { get; set; }
            
            
        }

        public class Address
        {
            public int Id { get; set; }
            public string Fullname { get; set; }
            public string Title { get; set; }
            
            public string Body { get; set; }
            
            public User User { get; set; } // Navigation Property
            public int? UserId { get; set; }
        }

        public class Product 
        {
            // primary key (Id,<type_name>Id)
            // Değer farklı bir isimle id yapmak isteseydik [Key] anahtarını yazmalıydık.

            // Database'in kendi kendine  artan değer vermesini engeller.
            // [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }

            [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Buradaki anlamı Oluşturulduğu tarihi ekler ve değiştirilemez.
            public DateTime InsertedDate {get;set;} = DateTime.Now;
            [DatabaseGenerated(DatabaseGeneratedOption.Computed)] // Buradaki anlamı ise çalıştığı son tarihi baz alır.
            public DateTime LastUpdatedDate {get;set;} = DateTime.Now;
            public List<ProductCategory> ProductsCategories { get; set; }
            
        }
        public class Category
        {
            public int Id { get; set; }
            public string Name { get; set;}

            public List<ProductCategory> ProductsCategories { get; set; }
            
            

        }
    
        // [NotMapped]
        // [Table("UrunKategorileri")]
        public class ProductCategory
        {
            [Key] // Data Annotation - Primary Key Olarak Atamak.
            public int ProductId { get; set; }
            
            public Product Product { get; set; } // Navigation Property - Foreign Key Görevi Görür.
            [Key] // Data Annotation - Primary Key Olarak Atamak.
            public int CategoryId { get; set; }
            
            public Category Category { get; set; } // Navigation Property - Foreign Key Görevi Görür.
        }

    
        internal class Program
        {

            public class CustomerDemo
            {
                public int CustomerId { get; set; }
                
                public string Name { get; set; }
                
                public int OrderCount { get; set; }
                
                
                public List<OrderDemo> Orders { get; set; }
                
                
            }

            public class OrderDemo
            {
                public int OrderId { get; set; }
                public decimal Total { get; set; }
                public List<ProductDemo> Products { get; set; }
                
                
                
                
            }

            public class ProductDemo
            {
                public int Id { get; set; }
                
                public string Name { get; set; }
                public decimal? Price {get ; set;}

                public int Quantity { get; set; }
                
                
                
            }


            static void Main(string[] args)
            {

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
            }
            

            static void AddUsers()
            {
                using(var db = new ShopContext())
                {
                    var users = new List<User>(){
                        new User{Name="Anıl",Email="anilonay123@gmail.com"},
                        new User{Name="Batuhan",Email="anilonay123@gmail.com"},
                        new User{Name="Sale",Email="salecilerdenn@gmail.com"},
                        new User{Name="Rümeysa",Email="rumeysaetbir@gmail.com"}
                    };

                    db.Users.AddRange(users);
                    db.SaveChanges();
                    Console.WriteLine($"Users Added.");
                    
                }
            }

            static void AddAddresses()
            {
                using (var db = new ShopContext())
                {
                    var adresses = new List<Address>(){
                        new Address(){Fullname="Ev Adresim",Title="Ev",Body="Samsunun bilinmez en ücra köşesinden.",UserId=1},
                        new Address(){Fullname="Okul Adresim",Title="Okul",Body="Manisanin köylerinden herhangibiri.",UserId=1},
                        new Address(){Fullname="İZmir Adresim",Title="İzmir",Body="İzmirin en elit yeri.",UserId=2},
                        new Address(){Fullname="Hakkari Adresim",Title="HAkkari",Body="Hakkari çukurovanın biraz yukarısı.",UserId=3},
                        new Address(){Fullname="Ankara Yolu",Title="Ankara",Body="Ankaradan çıkınca ilk sağ.",UserId=4},
                        new Address(){Fullname="Kebapçı Adresim",Title="Kebapçı",Body="Şanlıurfa kebapçısnıın yanı",UserId=4},
                    };

                    db.Addresses.AddRange(adresses);
                    db.SaveChanges();
                    Console.WriteLine($"Addresses Added.");
                    
                }
            }

        


        
        }
    }
