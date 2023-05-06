using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Models;
using Services;


    public class Order_Service:Connection
    {
        public List<Order> list_orders = new List<Order>();
        public bool check_Match;
        public bool check_Valid;
        public int id_order;
        public void select(){
            connection.Open();
            MySqlCommand command=new MySqlCommand($"Select * from Orders ;",connection);
            using (MySqlDataReader reader= command.ExecuteReader()){
                while (reader.Read())
                {
                    int id_order=int.Parse($"{reader["ID_order"]}");
                    int id_Customer=int.Parse($"{reader["ID_Customer"]}");
                    string customer_name=$"{reader["customer_name"]}";
                    string phone =$"{reader["phone"]}";
                    string address=$"{reader["Address"]}";
                    string order_date=$"{reader["order_date"]}";
                    int status=int.Parse($"{reader["order_status"]}");
                    list_orders.Add(new Order{ID_Order=id_order,ID_Customer=id_Customer,Customer_Name=customer_name,Customer_Phone=phone,Address=address,Date=order_date,Status=status});
                }
                reader.Close();
            }
              connection.Close();
        }
         string trangthaidon;
        public void getAll(){
                list_orders.Clear();
                select();
                 Console.WriteLine("================================================");
                foreach (Order item in list_orders)
                {   
                    if(item.Status==0){trangthaidon="Wainting";}
                    else if(item.Status==1){trangthaidon="Delivered";}
                     else if(item.Status==2){trangthaidon="Canceled";}
                     Console.WriteLine(item.ID_Order+"\t"+item.ID_Customer+"\t"+item.Customer_Name+"\t"+item.Customer_Phone+"\t"+item.Address+"\t"+trangthaidon);
                 Console.WriteLine("================================================");
                }
        }
        public void getorderwaiting(){
            list_orders.Clear();
                select();
                 Console.WriteLine("================================================");
            foreach (Order item in list_orders)
            {
                if(item.Status==0){
                    trangthaidon="Wainting";
                    Console.WriteLine("ID Order: "+item.ID_Order+"\nID Customer"+item.ID_Customer+"\nCustomer name: "+item.Customer_Name+"\nPhone: "+item.Customer_Phone+"\nAddress: "+item.Address+"\nStatus"+trangthaidon);
                     Console.WriteLine("================================================");
                
                }
            }
        }
        public void getbyperson(string name){ // su dung cho Customer
                list_orders.Clear();
                select();
                foreach (Order item in list_orders)
                {
                    if(string.Compare(item.Customer_Name,name,true) == 0){
                        connection.Open();
                         MySqlCommand command=new MySqlCommand($"Select * from order_detail where id_order='{item.ID_Order}';",connection);
                          Console.WriteLine("================================================");
                         using (MySqlDataReader reader= command.ExecuteReader())
                         {  while(reader.Read()){
                            double total=int.Parse($"{reader["price_each_product"]}")*int.Parse($"{reader["quantity"]}");
                            Console.WriteLine("ID product: "+reader["ID_product"]+"\n"+"Product: "+reader["product"]+"\n"+"Price: "+reader["price_each_product"]+"\n"+"Quantity: "+reader["quantity"]+"\n"+$"Total: {total}");
                            if(item.Status==0){trangthaidon="Wainting";}
                        else if(item.Status==1){trangthaidon="Delivered";}
                        else if(item.Status==2){trangthaidon="Canceled";}
                        Console.WriteLine("ID Order: "+item.ID_Order+"\nID Customer"+item.ID_Customer+"\nCustomer: "+item.Customer_Name+"\nPhone"+item.Customer_Phone+"\nAddress: "+item.Address+"\nStatus: "+trangthaidon+"\n");
                        
                         } Console.WriteLine("================================================");
                            reader.Close();
                            }
                        connection.Close();
                         
                    }
                }
        }
        public void accept(int id_or,int id_staff){ // su dung cho Staff
               
                        connection.Open();
                         MySqlCommand command=new MySqlCommand($"Update Orders set order_status='1' where id_order='{id_or}' and id_staff='{id_staff}';",connection);
                        using (MySqlDataReader reader= command.ExecuteReader()){reader.Close();}
                        connection.Close();
                        Console.ForegroundColor=ConsoleColor.DarkBlue;
                        Console.WriteLine("--> Order is accepted");
                        Console.ForegroundColor=ConsoleColor.White;
                 
        }
        public void checkid(int id){
            int count=0;
            foreach (Order item in list_orders)
                {   check_Match=false;
                    if(item.ID_Order==id && item.Status==0){
                        connection.Open();
                         MySqlCommand command=new MySqlCommand($"Select * from order_detail where id_order='{id}';",connection);
                         Console.WriteLine("================================================");
                         using (MySqlDataReader reader= command.ExecuteReader())
                         {  while(reader.Read()){
                            double total=int.Parse($"{reader["price_each_product"]}")*int.Parse($"{reader["quantity"]}");
                            Console.WriteLine("ID product: "+reader["ID_product"]+"\n"+"Product: "+reader["product"]+"\n"+"Price: "+reader["price_each_product"]+"\n"+"Quantity: "+reader["quantity"]+"\n"+$"Total: {total}");
                         }
                            reader.Close();
                            }
                        connection.Close();
                        trangthaidon="Wainting";
                        Console.WriteLine("ID Order: "+item.ID_Order+"\nID Customer: "+item.ID_Customer+"\nCustomer: "+item.Customer_Name+"\nPhone: "+item.Customer_Phone+"\nAddress: "+item.Address+"\nStatus: "+trangthaidon);
                          Console.WriteLine("================================================");
                         check_Match=true;
                         count++;
                    }
                }
                check_Valid=false;
            if(count>0){
                check_Valid=true;
            }
        }
        public void cancel(int id_or,int id_staff){ // su dung cho Staff
                
                        connection.Open();
                         MySqlCommand command=new MySqlCommand($"Update Orders set order_status='2' where id_order='{id_or}'and id_staff='{id_staff}';",connection);
                        using (MySqlDataReader reader= command.ExecuteReader()){reader.Close();}
                        connection.Close();
                        Console.ForegroundColor=ConsoleColor.DarkBlue;
                        Console.WriteLine("--> Order is denied");
                        Console.ForegroundColor=ConsoleColor.White;
                      
        }
    }