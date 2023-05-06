
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

using Models;
using Services;

    public class Order_Detail_Service:Connection
    {
        public List<Order_Detail> list_order_details = new List<Order_Detail>();
        // List<Product> list_product = new List<Product>();
        public List<int> list_id_product=new List<int>();
        public bool check_valid;
       
        public bool check_Match;
        public int id_order;
        public int count;
        public void Select(){
             connection.Open();
             MySqlCommand command = new MySqlCommand($"SELECT * FROM order_detail ORDER BY product;", connection);// thay doi user = tablekhac
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                    while (reader.Read())
                    {   
                    
                        int id_order_detail =int.Parse($"{reader["ID_order_detail"]}");
                        int id_product =int.Parse($"{reader["ID_product"]}");
                        int id_order =int.Parse($"{reader["ID_order"]}");
                        double price_each_product =int.Parse($"{reader["price_each_product"]}");
                        int quantity =int.Parse($"{reader["quantity"]}");
                        int status =int.Parse($"{reader["status"]}");
                        string name=$"{reader["product"]}";
                        list_order_details.Add(new Order_Detail(){ID_Order_detail=id_order_detail,ID_Order=id_order,ID_Product=id_product,Price=price_each_product,Quantity=quantity,Status=status,Name=name});
                        
                    }
                    reader.Close();
                    }
                    connection.Close();
        }
        public void Selectidorder(){
                    
                  MySqlCommand command = new MySqlCommand($"SELECT MAX(ID_order) FROM orders;", connection);// thay doi user = tablekhac
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                    while (reader.Read())
                    {   
                        
                        
                            this.id_order =reader.GetInt32(0);
                        
                    }
                    reader.Close();
                    }
                    connection.Close();            
                    
                    
        }  
        public void cancel(string name,int id_order_dt,int id_staff){ // su dung cho Staff
                
                        connection.Open();
                         MySqlCommand command=new MySqlCommand($"Update Order_detail set status='2' where  product='{name}'and id_order='{id_order_dt}';",connection);
                        using (MySqlDataReader reader= command.ExecuteReader()){reader.Close();}
                        connection.Close();
                        Console.ForegroundColor=ConsoleColor.DarkBlue;
                        Console.WriteLine("--> Order is denied");
                        Console.ForegroundColor=ConsoleColor.White;
                        Console.ReadKey();
                      
        }
        public void accept(string name,int id_order_dt,int id_staff){ // su dung cho Staff
                
                        connection.Open();
                         MySqlCommand command=new MySqlCommand($"Update Order_detail set status='1' where product='{name}'and id_order='{id_order_dt}';",connection);
                        using (MySqlDataReader reader= command.ExecuteReader()){reader.Close();}
                        connection.Close();
                        Console.ForegroundColor=ConsoleColor.DarkBlue;
                        Console.WriteLine("--> Order is accepted");
                        Console.ForegroundColor=ConsoleColor.White;
                        Console.ReadKey();
                      
        }
        string trangthaidon;
         public void displayProductForEachIdStaff(int id_staff,List<Product>list_product){
          list_id_product.Clear();
          list_order_details.Clear();
          Select();
          
            foreach (Product item in list_product)
            {
              if(item.ID_Staff==id_staff){
                list_id_product.Add(item.ID_Product);
              }
            }
            if(list_id_product.Count==0){
              System.Console.WriteLine("No orders yet");
            }
            else{
            int i=0;

             Console.WriteLine("================================================");
            for(int j=0;j<list_order_details.Count;j++)
            {
              if(list_order_details[j].ID_Product==list_id_product[i]){
                      if(list_order_details[j].Status==0){trangthaidon="Wainting";}
                        else if(list_order_details[j].Status==1){trangthaidon="Delivered";}
                        else if(list_order_details[j].Status==2){trangthaidon="Canceled";}
                  Console.WriteLine("Product: "+list_order_details[j].Name+"\nPrice: "+list_order_details[j].Price+"\nQuantity: "+list_order_details[j].Quantity+"\nStatus: "+trangthaidon);
                
                 int a=list_order_details[j].ID_Order;
                connection.Open();
                    MySqlCommand command = new MySqlCommand($"SELECT * FROM orders where id_order='{a}';", connection);// thay doi user = tablekhac
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                    while (reader.Read())
                    {   
                        Console.WriteLine("ID order: "+reader["ID_order"]+"\nID customer: "+reader["ID_Customer"].ToString()+"\nCustomer name: "+reader["Customer_name"]+"\nPhone: "+reader["Phone"]+"\nAddress: "+reader["Address"]+"\nOrder date: "+reader["Order_date"]);                    
                }
                    reader.Close();
                    }
                    connection.Close();
                     Console.WriteLine("================================================");
              }
              if(j==list_order_details.Count-1){
                 if(i==list_id_product.Count-1){
                  break;
                }
                j=0; i++;
               
              }
            }
          }
        }
  public void getbystatus(int status,List<Order> list_order,string customer){
    list_id_product.Clear();
    
    foreach (Order item in list_order)
    { 
      if(string.Compare(customer,item.Customer_Name,true)==0)
      list_id_product.Add(item.ID_Order);
    }

    // select idcustmer o bang or der turuoc
    // sau ddo lay dc Iid order 
    // select id_ order
    if(list_id_product.Count==0){
      System.Console.WriteLine("No orders yet");
    }
    else {
    int i=0;
    Console.WriteLine("================================================");
          for(int j=0;j<list_order_details.Count;j++)
          {
            if(list_order_details[j].Status==status&& list_id_product[i]==list_order_details[j].ID_Order){
              double total=list_order_details[j].Price*list_order_details[j].Quantity;
              if(status==0){trangthaidon="Waiting";}else if(status==1){trangthaidon="Delivered";}else if(status==2){trangthaidon="Cancelled";}
              Console.WriteLine("ID: "+list_order_details[j].ID_Order_detail+"\nID Product: "+list_order_details[j].ID_Product+"\nID Order: "+list_order_details[j].ID_Order+"\nPrice: "+list_order_details[j].Price+"\nQuantity: "+list_order_details[j].Quantity+"\nStatus: "+trangthaidon+"\nTotal: "+total);
            Console.WriteLine("================================================");
            }
            if(j==list_order_details.Count-1){
                 if(i==list_id_product.Count-1){
                  break;
                }
                j=0; i++;
               
              }
          }
        }
          Console.ReadKey();
          Console.Clear();
  }
 public void checkid(string name,int id_order_dt){
            int count=0;
            foreach (Order_Detail item in list_order_details)
                {   check_Match=false;
                    if(string.Compare(item.Name,name,true)==0 && item.Status==0 &&item.ID_Order_detail==id_order_dt){
                      Console.WriteLine(item.Name);
                      trangthaidon="Wainting";
                      double total=item.Price*item.Quantity;
                       Console.WriteLine("================================================");
                        Console.WriteLine("ID Order: "+item.ID_Order+"\nID Product: "+item.ID_Product+"\nID Order detail: "+item.ID_Order_detail+"\nPrice: "+item.Price+"\nQuantity: "+item.Quantity+"\nStatus: "+trangthaidon+"\nTotal: "+total);
                          
                         check_Match=true;
                         count++;
                        connection.Open();
                         MySqlCommand command=new MySqlCommand($"Select * from orders where id_order='{item.ID_Order}';",connection);
                         
                         using (MySqlDataReader reader= command.ExecuteReader())
                         {  while(reader.Read()){
                            
                            Console.WriteLine("ID Order: "+reader["ID_Order"]+"\n"+"ID Customer: "+reader["ID_customer"]+"\n"+"Customer name: "+reader["Customer_name"]+"\n"+"Phone: "+reader["Phone"]+"\n"+"Address: "+reader["Address"]+"\n"+"Order date: "+reader["Order_date"]);
                          Console.WriteLine("================================================");
                         }
                            reader.Close();
                            }
                        connection.Close();
                        break;
                        
                    }
                }
                if(check_Match==true){
                  check_valid=true;
                }
                else{
                  check_valid=false;
                }

        }


        public void displayProductForEachIdStaffWaiting(int id_staff,List<Product>list_product){
          list_id_product.Clear();
          list_order_details.Clear();
          Select();
          
            foreach (Product item in list_product)
            {
              if(item.ID_Staff==id_staff){
                list_id_product.Add(item.ID_Product);
              }
            }
            if(list_id_product.Count==0){
              System.Console.WriteLine("No orders yet");
              Console.ReadKey();
            }
            else{
            int i=0;
             Console.WriteLine("================================================");
            for(int j=0;j<list_order_details.Count;j++)
            {
              if(list_order_details[j].ID_Product==list_id_product[i]&&list_order_details[j].Status==0){
                if(list_order_details[j].Status==0){trangthaidon="Wainting";}
                        
                  Console.WriteLine("ID: "+list_order_details[j].ID_Order_detail+"\nProduct: "+list_order_details[j].Name+"\nPrice: "+list_order_details[j].Price+"\nQuantity: "+list_order_details[j].Quantity+"\nStatus: "+trangthaidon);
                
                 int a=list_order_details[j].ID_Order;
                connection.Open();
                    MySqlCommand command = new MySqlCommand($"SELECT * FROM orders where id_order='{a}';", connection);// thay doi user = tablekhac
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                    while (reader.Read())
                    {   
                        Console.WriteLine("ID order: "+reader["ID_order"]+"\nID customer: "+reader["ID_Customer"].ToString()+"\nCustomer name: "+reader["Customer_name"]+"\nPhone: "+reader["Phone"]+"\nAddress"+reader["Address"]+"\nOrder date: "+reader["Order_date"]);                    
                }
                    reader.Close();
                    }
                    connection.Close();
                     Console.WriteLine("================================================");
              }
              if(j==list_order_details.Count-1){
                 if(i==list_id_product.Count-1){
                  break;
                }
                j=0; i++;
               
              }
            }
          }
        }
        public void payment(int ID_customer, string name,string phone,string address,int id_product,string product,double price,int quantity,List<Product>list_queue){
              Console.WriteLine("You want to checkout this cart?"); 
               double total=0;
               foreach (Product item in list_queue)
               {
                total+=(item.Price*item.Quantity);
               }
              double price_each=quantity*price ;
              Console.ForegroundColor=ConsoleColor.Green;
              Console.WriteLine("Your total bill is: "+(total));
               Console.ForegroundColor=ConsoleColor.White;
              Console.WriteLine("1.Yes");
              Console.WriteLine("0.No");
              Console.WriteLine("Enter your choose:");
              int ask=int.Parse(Console.ReadLine());  
                connection.Open();
                 Selectidorder();
              Console.Clear();             
                if (ask==1){
                   
                    connection.Open();
                    MySqlCommand command = new MySqlCommand($"INSERT into orders (ID_Customer, Customer_name,phone,Address,Order_date) value ('{ID_customer}','{name}','{phone}','{address}','{DateTime.Now.ToString()}');", connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                    reader.Close();
                    }
                    connection.Close();
                   connection.Open();
                   
                   Selectidorder();
                  
                  foreach (Product item in list_queue)
                  {
                    connection.Open();
                    command = new MySqlCommand($"insert into order_detail(ID_order,ID_product,product,Price_each_product,Quantity) value('{this.id_order}','{item.ID_Product}','{item.Name}','{item.Price}','{item.Quantity}');", connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                    reader.Close();
                    }
                    connection.Close();
                    }
                  
                   list_queue.Clear();
                   check_valid=true;
                    Console.ForegroundColor=ConsoleColor.Green;
                    Console.WriteLine("=> Done");
                    Console.ForegroundColor=ConsoleColor.White;
                    // Console.ReadKey();
                    // Console.Clear();
                }
                    
                else if(ask==0){
                  check_valid=false;
                }
                else{
                  check_valid=false;
                  Console.ForegroundColor=ConsoleColor.DarkRed;
                  Console.WriteLine("Invalid selection");
                  Console.ForegroundColor=ConsoleColor.White;
                }
                    
        }
        public void delete(int id,int stt){
            connection.Open();
            MySqlCommand command = new MySqlCommand($"Update order_detail set status='{stt}' where id_order='{id}';", connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                    reader.Close();
                    }
            connection.Close();
        }
    }
