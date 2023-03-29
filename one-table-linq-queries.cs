// Tüm Müşteri Kayıtlarını Getir -- BAŞLA
                        // var customers = db.Customers.ToList(); 
                        // if(customers != null)
                        // {
                        //     foreach (var c in customers)
                        //     {
                        //         Console.WriteLine($"{c.ContactName}");
                                
                        //     }
                        // }
                    // Tüm Müşteri Kayıtlarını Getir -- BİTİR


                    // Tüm müşteri kayıtlarından sadece first_name ve last_name getir. -- BAŞLA
                        // var customers = db.
                        // Customers.
                        // Select(c => new {c.ContactName,c.ContactTitle})
                        // .ToList();

                        // if(customers!=null)
                        // {
                        //     foreach(var c in customers)
                        //     {
                        //         Console.WriteLine($"{c.ContactTitle}");
                                
                        //     }
                        // }
                    // Tüm müşteri kayıtlarından sadece first_name ve last_name getir. -- BİTİR

                    // Germany'de yaşayan müşterileri isim sırasına göre getir. -- BAŞLA
                        // var customers = db
                        // .Customers
                        // .Where(c => c.Country == "Germany" )
                        // .OrderBy(c => c.ContactName)
                        // .ToList();

                        // if(customers.Count()!=0)
                        // {
                        //     foreach(var c in customers)
                        //     {
                        //         Console.WriteLine($"{c.ContactName} - {c.Country}");
                                
                        //     }
                        // }
                        // Germany'de yaşayan müşterileri isim sırasına göre getir. -- BİTİR


                        // Beverages Kategorisindeki Ürünleri Getir -- BAŞLA
                        
                            // var products = db
                            // .Products
                            // .Where(p => p.Category.CategoryName == "Beverages")
                            // .Include(p => p.Category)
                            // .ToList();

                            // if(products.Count() != 0)
                            // {
                            //     foreach(var p in products)
                            //     {
                            //         Console.WriteLine($"{p.ProductName}  - {p.Category.CategoryName}");
                                    
                            //     }
                            // }
                        //  Beverages Kategorisindeki Ürünleri Getir -- BİTİR

                        // Son Eklenen 5 Ürünü Getir -- BAŞLA

                            // var products = db
                            // .Products
                            // .Select(p => new {p.ProductName,p.ProductId})
                            // .OrderByDescending(p => p.ProductId)
                            // .Take(5);

                            // if(products.Count()!=0)
                            // {
                            //     foreach(var p in products)
                            //     {
                            //         Console.WriteLine($"{p.ProductName}");
                                    
                            //     }
                            // }
                         // Son Eklenen 5 Ürünü Getir -- BİTİR

                         
                         // Fiyatı 5 ila 30 dolar arası ürünleri getir -- BAŞLA
                            // var products = db
                            // .Products
                            // .Where(p => p.UnitPrice <= 30 && p.UnitPrice >=5)
                            // .ToList();

                            // if(products.Count()!=0)
                            // {
                            //     foreach(var p in products)
                            //     {
                            //         Console.WriteLine($"{p.ProductName} - {p.UnitPrice}");
                                    
                            //     }
                            // }
                         // Fiyatı 5 ila 30 dolar arası ürünleri getir -- BİTİR


                        // Beverages Kategorisindeki Ürünlerin Ortalama Fiyatı -- BAŞLA
                            // var products = db
                            // .Products
                            // .Where(p => p.UnitPrice!=0 && p.Category.CategoryName=="Beverages")
                            // .Average(p => p.UnitPrice);

                            // Console.WriteLine($"Ortalama Fiyat : {products}");
                        // Beverages Kategorisindeki Ürünlerin Ortalama Fiyatı -- BİTİR


                        // Beverages kategorisinde kaç ürün vardır ? -- BAŞLA
                        // int sayi = db.Products.Count(p => p.Category.CategoryName=="Beverages");
                        // Console.WriteLine($"Beverages kategorisinde {sayi} ürün var.");

                        // Beverages ve Condiments kategorilerindeki ürünlerin toplam fiyatı
                        // var toplam = db.Products.Where(p => p.Category.CategoryName =="Beverages" || p.Category.CategoryName=="Condiments").Sum(p => p.UnitPrice);
                        // Console.WriteLine($"Ürünlerin Toplamı : {toplam}");
                        // Beverages kategorisinde kaç ürün vardır ? -- BAŞLA

                        // İçinde tea kelimesi geeçn ürünleri getirin // -- BAŞLA
                            // var tea = db.Products.Where(p => p.ProductName.ToLower().Contains("a")).ToList();

                            // if(tea.Count()!=0)
                            // {
                            //     foreach(var t in tea)
                            //     {
                            //         Console.WriteLine($"{t.ProductName}");
                                    
                            //     }
                            // }
                        // İçinde tea kelimesi geeçn ürünleri getirin // -- BİTİR
