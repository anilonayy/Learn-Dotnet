using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MySql;

namespace MyApp
{
    // Entity Class
    // Product (Id,Name,Price) => Product(Id,Name,Price)  ( Veri tabanı tablosundaki )

    public class ShopContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            // .UseSqlite("Data Source=shop.db;");
            .UseSqlServer(@"Data Source=ANIL\SQLEXPRESS;Initial Catalog=Shop;Integrated Security=SSPI;TrustServerCertificate=true");
            // .UseMySQL("Server=localhost;Database=myDataBase;Uid=myUsername;Pwd=myPassword;");
        }

        public DbSet<Product> Products {get;set;}
        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        
    }


    public class User
    {
        public User()
        {
            Adresses = new List<Address>(){};
        }
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public List<Address> Adresses { get; set; }  // Navigation Property
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

        [MaxLength(100)] // Maksimum 100 Karakter
        [Required] // Zorunlu Alan
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        
        
                
        
    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set;}

    }

 
    internal class Program
    {
        static void Main(string[] args)
        {
            
           using(var db = new ShopContext())
           {
                var user = db.Users.FirstOrDefault(i=>i.Name=="Anıl");

                if(user!=null)
                {
                    
                    user.Adresses.AddRange(new List<Address>{
                          new Address(){Fullname="Birinci Adreslerden",Title="HAkkari",Body="Hakkari çukurovanın biraz yukarısı."},
                          new Address(){Fullname="İkinci Adreslerden",Title="HAkkari",Body="Hakkari çukurovanın biraz yukarısı."},
                          new Address(){Fullname="Üçüncü Adreslerden",Title="HAkkari",Body="Hakkari çukurovanın biraz yukarısı."}
                    });
                    db.SaveChanges();
                    Console.WriteLine($"User's Address Added");
                    
                   
    
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
