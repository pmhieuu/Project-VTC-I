using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using  Models;
using Services;

    public class Customer_Service:Connection
    {
        public List<Customer> list_customer=new List<Customer>();
        public int id;
        public string email;
        public string fullname;
        public string phone;
        public string address;
        public void Select(string name){
            connection.Open();
            MySqlCommand command = new MySqlCommand($"SELECT * FROM Customer where Customer_username='{name}';", connection);// thay doi user = tablekhac
             using (MySqlDataReader reader = command.ExecuteReader())
                    {
                    while (reader.Read())
                    {   
                        string username =$"{reader["Customer_username"]}";
                        int id =int.Parse($"{reader["ID_Customer"]}");
                        this.fullname =$"{reader["fullname"]}";
                        this.phone =$"{reader["phone"]}";
                        this.email =$"{reader["email"]}";
                        this.address =$"{reader["address"]}";
                        this.id=id;
                        list_customer.Add(new Customer(){ID=id,Fullname=fullname,Username=username,Phone=phone,Email=email,Address=address});
                    }
                    reader.Close();
                    }
          connection.Close(); 
        }
        public void become_seller(string name){
                Select(name);
                 connection.Open();
                MySqlCommand command = new MySqlCommand($"Update Account Set role='1' where Username='{name}';", connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                    {
                    reader.Close();
                    }
                    connection.Close();
                connection.Open();
                command = new MySqlCommand($"Insert into Staff (Staff_username,fullname,phone,email) value ('{name}','{this.fullname}','{this.phone}','{this.email}');", connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                    {
                    reader.Close();
                    }
                    connection.Close();
                    connection.Open();
                    command = new MySqlCommand($"Update Customer set Status='1' WHERE customer_username='{name}';", connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                    {
                    reader.Close();
                    }
                    connection.Close();
                    
        }
        public void getInformation(string name){
                    list_customer.Clear();
                    Select(name);
                    Console.WriteLine("--> Your information");
                    foreach (Customer item in list_customer)
                    {       
                         Console.WriteLine("================================================");
                            Console.WriteLine("ID: "+item.ID+"\n"+"Fullname: "+item.Fullname+"\n"+"Username: "+item.Username+"\n"+"Phone: "+item.Phone+"\n"+"Email: "+item.Email+"\n"+"Address"+item.Address+"\n");
                          Console.WriteLine("================================================");

                    }
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Console.Clear();
        }   
        public void editPhone(string name){
            Console.WriteLine("Enter new phone:");
            string phone = Console.ReadLine();
            if(string.IsNullOrEmpty(phone)){
                Console.ForegroundColor=ConsoleColor.DarkRed;
                 Console.WriteLine("Please complete all information!");
                 Console.ForegroundColor=ConsoleColor.White;
                 Console.ReadKey();
                Console.Clear();
            }
            else{
                connection.Open();
                MySqlCommand command = new MySqlCommand($"Update Customer set phone='{phone}' where id_Customer='{list_customer[0].ID}';", connection);// thay doi user = tablekhac
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Close();
                    }
                     connection.Close();
                    Console.ForegroundColor=ConsoleColor.DarkBlue;
                    Console.WriteLine("Update successful");
                    Console.ForegroundColor=ConsoleColor.White;
                   
                    getInformation(name);
                    
            }
            
        }
        public void editEmail(string name){
            Console.WriteLine("Enter new email:");
            string email= Console.ReadLine();
            if(string.IsNullOrEmpty(email)){
                 
                 Console.ForegroundColor=ConsoleColor.DarkRed;
                 Console.WriteLine("Please complete all information!");
                 Console.ForegroundColor=ConsoleColor.White;
                   Console.ReadKey();
                Console.Clear();
            }
            else{
                connection.Open();
                MySqlCommand command = new MySqlCommand($"Update Customer set Email='{email}' where id_Customer='{list_customer[0].ID}';", connection);// thay doi user = tablekhac
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Close();
                    }
                    connection.Close();
                     Console.ForegroundColor=ConsoleColor.DarkBlue;
                    Console.WriteLine("Update successful");
                    Console.ForegroundColor=ConsoleColor.White;
                    getInformation(name);
                    
            }
            
        }
        public void editAddress(string name){
            Console.WriteLine("Enter new address:");
            string address= Console.ReadLine();
            if(string.IsNullOrEmpty(address)){
                 Console.ForegroundColor=ConsoleColor.DarkRed;
                 Console.WriteLine("Please complete all information!");
                 Console.ForegroundColor=ConsoleColor.White;
                   Console.ReadKey();
                Console.Clear();
            }
            else{
                connection.Open();
                MySqlCommand command = new MySqlCommand($"Update Customer set Address='{address}' where id_Customer='{list_customer[0].ID}';", connection);// thay doi user = tablekhac
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Close();
                    }
                    connection.Close();
                     Console.ForegroundColor=ConsoleColor.DarkBlue;
                    Console.WriteLine("Update successful");
                    Console.ForegroundColor=ConsoleColor.White;
                    getInformation(name);
                    
            }
            
        }
    }
