using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Models;

    public class Staff_Service:Services.Connection
    {   
        public List<Staff> list_staff = new List<Staff>();
        public int id;
        public void Select(string name)
        {
        connection.Open();
        MySqlCommand command = new MySqlCommand($"SELECT * FROM staff where Staff_username='{name}';", connection);// thay doi user = tablekhac
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                    while (reader.Read())
                    {   
                        string username =$"{reader["Staff_username"]}";
                        int id =int.Parse($"{reader["ID_Staff"]}");
                        string fullname =$"{reader["fullname"]}";
                        string phone =$"{reader["phone"]}";
                        string email =$"{reader["email"]}";
                        int status =int.Parse($"{reader["status"]}");
                        list_staff.Add(new Staff(){ID=id,Fullname=fullname,Username=username,Phone=phone,Email=email,Status=status});
                        this.id=id;
                    }
                    reader.Close();
                    }
                    connection.Close();
        }  
       
        public void getInformation(string name){
                    list_staff.Clear();
                    Select(name);
                   
                    foreach (Staff item in list_staff)
                    {       
                         Console.WriteLine("================================================");
                            Console.WriteLine("ID: "+item.ID+"\nFullname: "+item.Fullname+"\nUsername: "+item.Username+"\nPhone: "+item.Phone+"\nEmail: "+item.Email+"\n");
                        Console.WriteLine("================================================");
                    }
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
        }   
        public void editPhone(string name){
            Console.WriteLine("Enter new phone:");
            string phone = Console.ReadLine();
            if(string.IsNullOrEmpty(phone)){
                 Console.WriteLine("Please complete all information!");
            }
            else{
                connection.Open();
                MySqlCommand command = new MySqlCommand($"Update staff set phone='{phone}' where id_staff='{list_staff[0].ID}';", connection);// thay doi user = tablekhac
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Close();
                    }
                    connection.Close();
                    getInformation(name);
                    
            }
            Console.ReadKey();
            Console.Clear();
        }
        public void editEmail(string name){
            Console.WriteLine("Enter new email:");
            string email= Console.ReadLine();
            if(string.IsNullOrEmpty(email)){
                 Console.WriteLine("Please complete all information!");
            }
            else{
                connection.Open();
                MySqlCommand command = new MySqlCommand($"Update staff set Email='{email}' where id_staff='{list_staff[0].ID}';", connection);// thay doi user = tablekhac
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Close();
                    }
                    connection.Close();
                    getInformation(name);
                    
            }
            Console.ReadKey();
            Console.Clear();
        }
    }