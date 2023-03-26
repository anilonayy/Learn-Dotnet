using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

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


    public class User
    {

        public int Id { get; set; }

        public string Name { get; set; }
        
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
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<ProductCategory> ProductsCategories { get; set; }
        

    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set;}

        public List<ProductCategory> ProductsCategories { get; set; }
        
        

    }

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
        static void Main(string[] args)
        {
           using(var db = new ShopContext())
           {
                var products = new List<Product>(){
                    new Product{Name  ="Sinbo Elektrikli Kettle",Price=80},
                    new Product{Name  ="Iphone 14 Pro Max",Price=25000},
                    new Product{Name  ="Macbook Pro m2",Price=35000},
                    new Product{Name  ="JB Kulaklık",Price=150},
                    new Product{Name  ="Ses Bombası",Price=200},
                    new Product{Name  ="ViewSonic Monitör",Price=1000},
                    new Product{Name  ="LogiTech Klavye",Price=700},
                };

                var categories = new List<Category>(){
                    new Category{Name="Elektronik"},
                    new Category{Name="Ev Eşyası"},
                    new Category{Name="Bilgisayar"}
                };

                db.Products.AddRange(products);
                db.Categories.AddRange(categories);

                // Find Metodu bizden bir id değeri bekler.
                var ids =  new int[2]{1,2};
                Product p = db.Products.Find(1);
         
                if(p!=null)
                {
                    p.ProductsCategories = ids.Select(cid => new ProductCategory(){
                        ProductId = p.Id,
                        CategoryId = cid
                    }).ToList();
                }
                db.SaveChanges();
                
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
