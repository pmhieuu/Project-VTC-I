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
         List<Product> list_product=new List<Product>();
// sau khi add product xong thi phair thong bao va hien thi

  public void Select()
  {
    connection.Open();
    MySqlCommand command = new MySqlCommand("SELECT * FROM Product;", connection);// thay doi user = tablekhac
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
  public void Add(int id_staff,int id_category,string product,int quantity,double price){ // cần check trùng Product
            list_category.Clear();
            list_product.Clear();
            ID_Category();
            Select();
            foreach (Product item in list_product)
            {   
                this.check_Match=false;
                if(string.Compare(item.Name,product,true)==0){
                    this.check_Match=true;
                    break;
                  }      
            }
            foreach (Category item in list_category)
            {   
                this.Check_Valid=false;
                if(item.ID==id_category){
                    this.Check_Valid=true;
                  break;
                  }      
            }
            if(this.Check_Valid==true && this.check_Match==false){
              connection.Open();
                MySqlCommand command = new MySqlCommand($"insert into Product(id_staff,id_category,Product,quantity,price)value('{id_staff}','{id_category}','{product}','{quantity}','{price}');", connection); 
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                reader.Close();
                } 
                connection.Close();
                getAll();
                this.Check_Valid=true;
                
            }
            if(this.check_Match==true){
              Console.ForegroundColor=ConsoleColor.DarkRed;
              Console.WriteLine("Product duplicated!");
              Console.ForegroundColor=ConsoleColor.White;
            }
            else if(String.IsNullOrEmpty(id_category.ToString())==true||
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
            string product;                                      // khi nhập không được để trống
            foreach (Product item in list_product)
            {   
                this.check_Match=false;
                if(string.Compare(item.Name,Product,true)==0){
                    Console.WriteLine(item.ID_Product.ToString()+"\t"+item.ID_Staff+"\t"+item.ID_Category+"\t"+item.Name+"\t"+item.Quantity+"\t"+item.Price);
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
              Console.WriteLine("Enter new category id:");
              id_category=int.Parse(Console.ReadLine());
              Console.WriteLine("Enter quantity:");
              quantity=int.Parse(Console.ReadLine());
              Console.WriteLine("Enter new price");
              price=double.Parse(Console.ReadLine());
              foreach(Category item in list_category){ //Kiem tra id_Category co ton tai k?
                  check_Match=false;
                  if(item.ID==id_category){
                    check_Match=true;
                  break;
                }
              }
              
              
              if(string.IsNullOrEmpty(Product)==true||
              string.IsNullOrEmpty(id_category.ToString())==true||
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
                 MySqlCommand command = new MySqlCommand($"Update Product Set ID_category='{id_category}',ID_Staff='{id_staff}',Product='{product}',Quantity='{quantity}',Price='{price}' where Product='{Product}';", connection); 
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                    reader.Close();
                    }  
                    connection.Close();
                    Check_Valid=true;
                    getAll();
              }
              } while (check_Match!=true);
            }
         
    
  }
   public void Delete(string Product){ 
            list_category.Clear();
            list_product.Clear();
            ID_Category();
            Select();
        foreach (Product item in list_product)
        { 
          Check_Valid=false;
          if(string.Compare(item.Name,Product,true)==0){
             Console.Clear();
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
                    Console.WriteLine("Delete successfully");
                    Console.ForegroundColor=ConsoleColor.White;
                    getAll();
                  Check_Valid=true;
                  break;
                  
              }
              else if(ask==0){
                Check_Valid=false;
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
     public void findProduct(string category){
      int count = 0;
            list_category.Clear();
            list_product.Clear();
            ID_Category();
            Select();
              foreach (Product item in list_product)
              { check_Match=false;
                if(item.Status==0 && item.Name.ToLower().Contains(category.ToLower())){
                  Console.WriteLine(item.ID_Product+"\t"+item.ID_Category+"\t"+item.Name+"\t"+item.Quantity+"\t"+item.Price);
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
          }
          else if(ask==1){
            Check_Valid=false;
          }
          else {
            Console.Clear();
            Console.WriteLine("Invalid selection");
            Check_Valid=true;
          }
        }
     }
    public void getByProduct(string category){// Cần phải check id_category dựa vào category sau đó mới
           list_category.Clear();
            list_product.Clear();
            ID_Category();
            Select();                                   // trả về sản phẩm của id đó
        int id=0;
        list_product.Clear();
        Select();
        list_category.Clear();
        ID_Category();
                    connection.Open();
                    MySqlCommand command = new MySqlCommand($"select id_category from Category where category='{category}';", connection); 
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                      while (reader.Read())
                      {   id=int.Parse($"{reader["id_category"]}");
                    
                    }reader.Close();
                    }
                    connection.Close();
        foreach(Product item in list_product)
        {
          if(item.Status==0 && item.ID_Category==id){
            Console.WriteLine(item.ID_Product+"\t"+item.ID_Category+"\t"+item.Name+"\t"+item.Quantity+"\t"+item.Price);
          }
        }
        if(id == 0){
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
          }
          else if(ask==1){
            Check_Valid=false;
          }
          else {
            Console.Clear();
            Console.WriteLine("Invalid selection");
            Check_Valid=true;
          }
        }
    }
    public void getAll(){
        list_product.Clear();
        Select();
        foreach(Product item in list_product)
        {
          if(item.Status==0){
            Console.WriteLine(item.ID_Product+"\t"+item.ID_Category+"\t"+item.Name+"\t"+item.Quantity+"\t"+item.Price);
          }
        
        }
     
      
    }
    }
