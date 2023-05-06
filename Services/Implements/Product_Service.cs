using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using  Models;
using Services;

    public class Product_Service:Services.Connection
    {   
        public bool Check_Valid;
        public bool check_Match;
        public bool check_Match1;
        List<Staff> list_staff=new List<Staff>();
        List<Category> list_category=new List<Category>();
        public List<Product> list_product=new List<Product>();
         public List<Product> list_product_queue=new List<Product>();
         public int id_product;
          public int id_category;
          public string product;
         public double price;
         public string name;
         public int quantity;
        public int counter;

  public void Select()
  {
    connection.Open();
    MySqlCommand command = new MySqlCommand("SELECT * FROM Product ORDER BY product;", connection);// thay doi user = tablekhac
            using (MySqlDataReader reader = command.ExecuteReader())
            {
            while (reader.Read())
            {   int id_product=int.Parse($"{reader["ID_product"]}");
                int id_staff =int.Parse($"{reader["ID_Staff"]}");
                int id_category =int.Parse($"{reader["ID_Category"]}");
                string name =$"{reader["product"]}";
                int quantity =int.Parse($"{reader["Quantity"]}");
                double price =double.Parse($"{reader["Price"]}");
                int status =int.Parse($"{reader["status"]}");
                list_product.Add(new Product(){ID_Product=id_product,ID_Staff=id_staff,ID_Category=id_category,Name=name,Quantity=quantity,Price=price,Status=status});
            }
            reader.Close();
            }   
          connection.Close();  
  } 
  public void ID_Staff(){
    connection.Open();
     MySqlCommand command = new MySqlCommand("SELECT * FROM Staff;", connection);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
            while (reader.Read())
            {   
                int id_staff=int.Parse($"{reader["ID_Staff"]}");
                list_staff.Add(new Staff(){ID = id_staff});
                 }
            reader.Close();
            }
            connection.Close();
  }
  public void checklist(int id_staff){
    foreach (Product item in list_product)
    {
        if(item.Status==0&&item.ID_Staff==id_staff){this.counter++;}
    }
  }
  public void ID_Category(){
            connection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM Category;", connection);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
            while (reader.Read())
            {   
                int id_category=int.Parse($"{reader["ID_Category"]}");
                string name=$"{reader["Category"]}";
                list_category.Add(new Category(){ID = id_category, Name = name});
                 }
            reader.Close();
            }
            connection.Close();
  }
  public void cart(){
    Console.Clear();
    
    Console.WriteLine("================================================");
    foreach (Product item in list_product_queue)
    {
      Console.WriteLine("Product: "+item.Name+"\nPrice: "+item.Price+"\nQuantity: "+item.Quantity);
       Console.WriteLine("================================================");
    }
   
  }
  public void huycart(string product){
  
    for(int i = 0; i < list_product_queue.Count;i++){
      check_Match=false;
      if(string.Compare(list_product_queue[i].Name,product,true) == 0){
        Console.WriteLine("================================================");
        Console.WriteLine("Product: "+list_product_queue[i].Name+"\nPrice: "+list_product_queue[i].Price+"\nQuantity: "+list_product_queue[i].Quantity);
       Console.WriteLine("================================================");
        Console.WriteLine("Cancel this");
        System.Console.WriteLine("1.Yes");
         System.Console.WriteLine("2.No");
         System.Console.WriteLine("Enter your choose:");
         int ask=int.Parse(Console.ReadLine());
         Console.Clear();
         if(ask==1){
           list_product_queue.RemoveAt(i);
           counter--;
         }
        else if(ask==2){}
        else{
          System.Console.WriteLine("Invalid selection");
        }
        check_Match=true;
        break;
      }
    }
    if(check_Match==false){
      System.Console.WriteLine($"{product} is not exist");
    }
  }
  public void Buy(){
                list_product.Clear();
                Select();
                    int ask=0;
                    do
                    {
                    Console.WriteLine("Buy now?");
                    Console.WriteLine("1.Yes");
                    Console.WriteLine("0.No");
                    Console.WriteLine("Enter your choose:");
                    ask=int.Parse(Console.ReadLine());
                    Console.Clear();
                    //select product sau đó thêm vào order detail
                    if(ask==1){
                      Console.WriteLine("Enter product:");
                      string product=Console.ReadLine();
                      
                      foreach (Product item in list_product)
                      { Check_Valid=false;
                        if(string.Compare(item.Name,product,true)==0&&item.Status==0){
                          
                          
                            Console.WriteLine("Enter quantity");
                            int quantity=int.Parse(Console.ReadLine());
                            Console.Clear();
                            if(quantity>=1 && quantity<item.Quantity){
                              this.id_product=item.ID_Product;
                              this.product=item.Name;
                              this.quantity=quantity;
                              this.price=item.Price;
                              Console.Clear();
                              Console.ForegroundColor=ConsoleColor.DarkGreen;
                                Console.WriteLine("The product has been added to cart");
                                Console.ForegroundColor=ConsoleColor.White;
                                Console.ReadKey();
                                Console.Clear();
                                list_product_queue.Add(new Product{ID_Product=item.ID_Product,Name = item.Name, Price=item.Price,Quantity=quantity});
                              Check_Valid=true;
                              cart();
                            }
                            else{
                              Console.Clear();
                              Console.ForegroundColor=ConsoleColor.DarkRed;
                                Console.WriteLine("Invalid selection!");
                                Console.ForegroundColor=ConsoleColor.White;
                            }
                           break;
                                  }
                                }
                                if(Check_Valid==false){
                                  Console.Clear();
                                  Console.ForegroundColor=ConsoleColor.DarkRed;
                                  Console.WriteLine("The product is not exist");
                                  Console.ForegroundColor=ConsoleColor.White;
                                }
                              }
                      else if(ask==0){
                        
                      }
                      else{
                         //
                      }
                          } while (ask!=0);
         
  }
  public void Add(int id_staff,string category,string product,int quantity,double price){ // cần check trùng Product
            list_category.Clear();
            list_product.Clear();
            ID_Category();
            Select();

            foreach (Product item in list_product)
            {   
                this.check_Match=false;
                if(string.Compare(item.Name,product,true)==0&&item.Status==0){
                    this.check_Match=true;
                      
              Console.ForegroundColor=ConsoleColor.DarkRed;
              Console.WriteLine("Product duplicated!");
              Console.ForegroundColor=ConsoleColor.White;
                    break;
                  }      
            }
            foreach (Category item in list_category)
            {   
                this.Check_Valid=false;
                if(string.Compare(item.Name,category,true)==0){
                  this.id_category=item.ID;
                    this.Check_Valid=true;
                  break;
                  }      
            }
            if(this.Check_Valid==true && this.check_Match==false){
              foreach (Product item in list_product)
            {   
                this.check_Match=false;
                if(string.Compare(item.Name,product,true)==0&&item.Status==1){
                    connection.Open();
                MySqlCommand command = new MySqlCommand($"Update Product set status='0',id_category='{this.id_category}',Quantity='{quantity}',Price='{price}' where product='{product}';", connection); 
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                reader.Close();
                } 
                connection.Close();
                check_Match=true;
                break;
                  } 
                     
            }
              if(check_Match==false){
                connection.Open();
                MySqlCommand command = new MySqlCommand($"insert into Product(id_staff,id_category,Product,quantity,price)value('{id_staff}','{this.id_category}','{product}','{quantity}','{price}');", connection); 
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                reader.Close();
                } 
                connection.Close();
              } 
              Console.ForegroundColor=ConsoleColor.DarkBlue;
              System.Console.WriteLine("--> Added successfully");
              Console.ForegroundColor=ConsoleColor.White;
                getAll(id_staff);
                this.Check_Valid=true;
                
            }
            
          if(String.IsNullOrEmpty(id_category.ToString())==true||
            String.IsNullOrEmpty(quantity.ToString())==true||
            String.IsNullOrEmpty(price.ToString())==true||
            String.IsNullOrEmpty(product)==true){
              Console.ForegroundColor=ConsoleColor.DarkRed;
                Console.WriteLine("Please complete all information!");
                Console.ForegroundColor=ConsoleColor.White;
                Check_Valid=false;
            }
            else if(this.Check_Valid==false){
              Console.ForegroundColor=ConsoleColor.DarkRed;
              Console.WriteLine("Category is not exist!");
              Console.ForegroundColor=ConsoleColor.White;
            }
            
            
  }
  public void Update(string Product,int id_staff){ // cần check trùng Product
            Check_Valid=false;
            list_category.Clear();
            list_product.Clear();
            ID_Category();
            Select();
            int id_category=0;
            int quantity=0;
            double price=0;  
            string product; 
            string category;                                 // khi nhập không được để trống
           
           Console.WriteLine("================================================");
            foreach (Product item in list_product)
            {   
                this.check_Match=false;
                
                if(string.Compare(item.Name,Product,true)==0&& item.Status==0){
                    Console.WriteLine("ID_Product: "+item.ID_Product.ToString()+"\nID Staff: "+item.ID_Staff+"\nID Category: "+item.ID_Category+"\nProduct: "+item.Name+"\nQuantity: "+item.Quantity+"\nPrice: "+item.Price);
                    Console.WriteLine("================================================");
                    this.check_Match=true;
                  break;
                  }      
            }
             if(check_Match==false){
              Console.WriteLine("The product is not exists");
              Check_Valid=false;
                      }
            //// Kiem tra xem cac thong tin nhu new product co trung lap hay không??
            //// id category có ton tai hay khong??
            // sau do moi update
            if(this.check_Match==true)
            {
              do
              {
              Console.WriteLine("Enter new product:");
              product=Console.ReadLine();
              Console.WriteLine("Enter new category:");
              category=Console.ReadLine();
              Console.WriteLine("Enter quantity:");
              quantity=int.Parse(Console.ReadLine());
              Console.WriteLine("Enter new price");
              price=double.Parse(Console.ReadLine());
              foreach(Category item in list_category){ //Kiem tra id_Category co ton tai k?
                  check_Match=false;
                  if(string.Compare(item.Name,category,true)==0&&item.Status==0){
                    this.id_category=item.ID;
                    check_Match=true;
                  break;
                }
              }
              
              
              if(string.IsNullOrEmpty(Product)==true||
              string.IsNullOrEmpty(category)==true||
              string.IsNullOrEmpty(quantity.ToString())==true||
              string.IsNullOrEmpty(price.ToString())==true){
                Console.ForegroundColor=ConsoleColor.DarkRed;
                Console.WriteLine("Please complete all information!");
                Console.ForegroundColor=ConsoleColor.White;
                check_Match=false;
              }
              else if(check_Match==false){
                Console.WriteLine("The category is not exist");
              }
              else if (check_Match=true){
                connection.Open();
                 MySqlCommand command = new MySqlCommand($"Update Product Set ID_category='{this.id_category}',ID_Staff='{id_staff}',Product='{product}',Quantity='{quantity}',Price='{price}' where Product='{Product}';", connection); 
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                    reader.Close();
                    }  
                    connection.Close();
                    Console.ForegroundColor=ConsoleColor.DarkBlue;
              System.Console.WriteLine("--> Updated successfully");
              Console.ForegroundColor=ConsoleColor.White;
                    Check_Valid=true;
                    getAll(id_staff);
              }
              } while (check_Match!=true);
            }
         
    
  }
   public void Delete(string Product,int id_staff){ 
            list_category.Clear();
            list_product.Clear();
            ID_Category();
            Select();
        foreach (Product item in list_product)
        { 
          Check_Valid=false;
          if(string.Compare(item.Name,Product,true)==0&&item.Status==0){
             Console.Clear();
             Console.WriteLine("================================================");
     Console.WriteLine("ID_Product: "+item.ID_Product.ToString()+"\nID Category: "+item.ID_Category+"\nProduct: "+item.Name+"\nQuantity: "+item.Quantity+"\nPrice: "+item.Price);
            Console.WriteLine("================================================");
              Console.ForegroundColor=ConsoleColor.DarkBlue;
              Console.WriteLine("Are you sure?");
              Console.WriteLine("1.Yes");
              Console.WriteLine("0.No");
              Console.ForegroundColor=ConsoleColor.White;
              Console.WriteLine("Enter your choose");
              int ask=int.Parse(Console.ReadLine());
              if(ask==1){
                connection.Open();
                    MySqlCommand command = new MySqlCommand($"Update Product Set status='1' where Product='{Product}';", connection); 
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                    reader.Close();
                    }
                    connection.Close();
                    Console.Clear();
                    Console.ForegroundColor=ConsoleColor.DarkBlue;
                    Console.WriteLine("Deleted successfully");
                    Console.ForegroundColor=ConsoleColor.White;
                    getAll(id_staff);
                  Check_Valid=true;
                  break;
                  
              }
              else if(ask==0){
                Check_Valid=true;
                break;
              }
              else{
                Console.Clear();
                Console.WriteLine("Invalid Selection");
                Check_Valid=false;
                break;
              }
          }

        }  if (Check_Valid==false){
          Console.WriteLine("Product is not exists");
        } 
  } 
  public void findProductByStaff(string category,int id_staff){
      int count = 0;
            list_category.Clear();
            list_product.Clear();
            ID_Category();
            Select();
             Console.WriteLine("================================================");
             Check_Valid=false;
              foreach (Product item in list_product)
              { check_Match=false;
                if(item.Status==0 && item.Name.ToLower().Contains(category.ToLower())&&item.ID_Staff==id_staff){
                 
                 Console.WriteLine("ID_Product: "+item.ID_Product.ToString()+"\nID Category: "+item.ID_Category+"\nProduct: "+item.Name+"\nQuantity: "+item.Quantity+"\nPrice: "+item.Price);
                    Console.WriteLine("================================================");
                  count++;
                  check_Match=true;
                  Check_Valid=true;
                }
              }
               if(count == 0){
          Console.Clear();
          Console.WriteLine("No results found");
          Console.ForegroundColor=ConsoleColor.DarkYellow;
          Console.WriteLine("Do you want to try again?");
          Console.WriteLine("1.Continue");
          Console.WriteLine("0.Exit");
          Console.ForegroundColor=ConsoleColor.White;
           Console.WriteLine("Enter your choose");
          int ask=int.Parse(Console.ReadLine());
          if(ask==0){
            Check_Valid=true;
            Console.Clear();
          }
          else if(ask==1){
            Console.Clear();
            Check_Valid=false;
          }
          else {
            Console.Clear();
            Console.WriteLine("Invalid selection");
            Check_Valid=true;
          }
        }
     }
     public void findProduct(string category){
      int count = 0;
            list_category.Clear();
            list_product.Clear();
            ID_Category();
            Select();
            if(string.IsNullOrEmpty(category)&&string.IsNullOrWhiteSpace(category)){
              Console.WriteLine("Invalid");
            }
            else{
             Console.WriteLine("================================================");
              foreach (Product item in list_product)
              { check_Match=false;
                if(item.Status==0 && item.Name.ToLower().Contains(category.ToLower())){
                 
                 Console.WriteLine("ID_Product: "+item.ID_Product.ToString()+"\nID Category: "+item.ID_Category+"\nProduct: "+item.Name+"\nQuantity: "+item.Quantity+"\nPrice: "+item.Price);
                    Console.WriteLine("================================================");
                  count++;
                  check_Match=true;
                }
              }
               if(count == 0){
          Console.Clear();
          Console.WriteLine("No results found");
          Console.ForegroundColor=ConsoleColor.DarkYellow;
          Console.WriteLine("Do you want to try again?");
          Console.WriteLine("1.Continue");
          Console.WriteLine("0.Exit");
          Console.ForegroundColor=ConsoleColor.White;
           Console.WriteLine("Enter your choose");
          int ask=int.Parse(Console.ReadLine());
          if(ask==0){
            Check_Valid=true;
            Console.Clear();
          }
          else if(ask==1){
            Console.Clear();
            Check_Valid=false;
          }
          else {
            Console.Clear();
            Console.WriteLine("Invalid selection");
            Check_Valid=true;
          }
        }
        }
     }
    public void getProductByStaff(string category,int id_staff){// Cần phải check id_category dựa vào category sau đó mới
           list_category.Clear();
            list_product.Clear();
            ID_Category();
            Select();                                   // trả về sản phẩm của id đó
       
        int count=0;
        list_product.Clear();
        Select();
        list_category.Clear();
        ID_Category();
                    connection.Open();
                    MySqlCommand command = new MySqlCommand($"select id_category from Category where category='{category}';", connection); 
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                      while (reader.Read())
                      {   this.id_category=int.Parse($"{reader["id_category"]}");
                    
                    }reader.Close();
                    }
                    connection.Close();
                Console.WriteLine($"{category}");
               Console.WriteLine("================================================");      
        foreach(Product item in list_product)
        { 
          if(item.Status==0 && item.ID_Category==this.id_category&&item.ID_Staff==id_staff){
            
                 Console.WriteLine("ID_Product: "+item.ID_Product.ToString()+"\nID Category: "+item.ID_Category+"\nProduct: "+item.Name+"\nQuantity: "+item.Quantity+"\nPrice: "+item.Price);
                    Console.WriteLine("================================================");
            count++;
            if (count>0){Check_Valid=true;}
          }
        }
        if(count == 0){
          Console.Clear();
          Console.WriteLine("This category is empty");
          Console.ForegroundColor=ConsoleColor.DarkYellow;
          Console.WriteLine("Do you want to try again?");
          Console.WriteLine("1.Continue");
          Console.WriteLine("0.Exit");
          Console.ForegroundColor=ConsoleColor.White;
           Console.WriteLine("Enter your choose");
          int ask=int.Parse(Console.ReadLine());
          do{
          if(ask==0){
            Check_Valid=true;
          }
          else if(ask==1){
            Check_Valid=false;
          }
          else {
            Console.Clear();
            Console.WriteLine("Invalid selection");
          }
          }while(ask!=0&&ask!=1);
        }
    }
    public void getByProduct(string category){// Cần phải check id_category dựa vào category sau đó mới
           list_category.Clear();
            list_product.Clear();
            ID_Category();
            Select();                                   // trả về sản phẩm của id đó
       
        int count=0;
        list_product.Clear();
        Select();
        list_category.Clear();
        ID_Category();
                    connection.Open();
                    MySqlCommand command = new MySqlCommand($"select id_category from Category where category='{category}';", connection); 
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                      while (reader.Read())
                      {   this.id_category=int.Parse($"{reader["id_category"]}");
                    
                    }reader.Close();
                    }
                    connection.Close();
                Console.WriteLine($"{category}");
               Console.WriteLine("================================================");      
        foreach(Product item in list_product)
        { 
          if(item.Status==0 && item.ID_Category==this.id_category){
            
                 Console.WriteLine("ID_Product: "+item.ID_Product.ToString()+"\nID Category: "+item.ID_Category+"\nProduct: "+item.Name+"\nQuantity: "+item.Quantity+"\nPrice: "+item.Price);
                    Console.WriteLine("================================================");
            count++;
            if (count>0){Check_Valid=true;}
          }
        }
        if(count == 0){
          Console.Clear();
          Console.WriteLine("No results found");
          Console.ForegroundColor=ConsoleColor.DarkYellow;
          Console.WriteLine("Do you want to try again?");
          Console.WriteLine("1.Continue");
          Console.WriteLine("0.Exit");
          Console.ForegroundColor=ConsoleColor.White;
           Console.WriteLine("Enter your choose");
          int ask=int.Parse(Console.ReadLine());
          do{
          if(ask==0){
            Check_Valid=true;
          }
          else if(ask==1){
            Check_Valid=false;
          }
          else {
            Console.Clear();
            Console.WriteLine("Invalid selection");
          }
          }while(ask!=0&&ask!=1);
        }
    }
    public void getAll(int id){
        list_product.Clear();
        Select();
       Console.WriteLine("================================================");
        foreach(Product item in list_product)
        {
          if(item.Status==0&&item.ID_Staff==id){
            
                 Console.WriteLine("ID_Product: "+item.ID_Product.ToString()+"\nID Category: "+item.ID_Category+"\nProduct: "+item.Name+"\nQuantity: "+item.Quantity+"\nPrice: "+item.Price);
                    Console.WriteLine("================================================");
          }
        
        }
     
      
    }
    }
