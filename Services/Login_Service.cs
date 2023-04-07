using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using Models;

namespace Services
{
    public class Login_Service
    {
         
        public bool Check_Valid;
        public bool check_Match;
        static string readKey="Press any key to Continue";
        List<Account> list=new List<Account>();
            public MySqlConnection connection = new MySqlConnection
            {
            ConnectionString = @"server=localhost;userid=root;password=hoangapache;port=3306;database=project_1_VTCA;"
            };
           
        public void Select(){
           
            MySqlCommand command = new MySqlCommand("SELECT * FROM Account;", connection);// thay doi user = tablekhac
            using (MySqlDataReader reader = command.ExecuteReader())
            {
            while (reader.Read())
            {   
                string username =$"{reader["username"]}";
                string password =$"{reader["password"]}";
                int role =int.Parse($"{reader["role"]}");
                list.Add(new Account(){Username=username,Password=password,Role=role}); 
            }
            reader.Close();
            }
        }
        public void Create_Account(string username, string password)// them thong tin cua doi tuong user 
        {
            MySqlCommand command = new MySqlCommand($"insert into Account(username,password)value('{username}','{password}');", connection); 
           using (MySqlDataReader reader = command.ExecuteReader())
        {
        reader.Close();
        }
}       
public void Create_Information(string username,string fullname, string phone,string email,string address)
        {
            MySqlCommand command = new MySqlCommand($"insert into Customer(FK_Customer_Account,fullname,phone,email,address)value('{username}','{fullname}','{phone}','{email}','{address}');", connection); 
           using (MySqlDataReader reader = command.ExecuteReader())
        {
        reader.Close();
        }
}
        public void Sign_Up(string username,string password,string fullname,string phone,string email,string address){
            Console.Clear();
            if(String.IsNullOrEmpty(username)==true||String.IsNullOrEmpty(password)==true || 
            String.IsNullOrEmpty(fullname)==true||String.IsNullOrEmpty(phone)==true||
            String.IsNullOrEmpty(email)==true||String.IsNullOrEmpty(address)==true){
                Console.ForegroundColor=ConsoleColor.DarkRed;
                Console.WriteLine("Please complete all information!");
                Console.ForegroundColor=ConsoleColor.White;
                Check_Valid=true;
            }
            else if(username.Contains(" ")){
                Console.WriteLine("Username cannot contain spaces");
            }
            else if(username.Length>=16||password.Length>=16){
                Console.ForegroundColor=ConsoleColor.DarkRed;
                Console.WriteLine("Please enter less than 16 characters!");
                Console.ForegroundColor=ConsoleColor.White;               
                Check_Valid=true;
            }
            else {
                foreach (Account item in   list)
                { 
                  check_Match=false;  
                if(String.Compare(item.Username, username,true) == 0){
                check_Match=true;
                Console.ForegroundColor=ConsoleColor.DarkRed;
                Console.WriteLine("The account already has a user!");
                Console.ForegroundColor=ConsoleColor.White;
                Check_Valid=true;
                break;  
                    } 
                }  
                 if(check_Match==false){
                    Create_Account(username,password);
                    Create_Information(username,fullname,phone,email,address);
                    Console.ForegroundColor=ConsoleColor.DarkBlue;
                    Console.WriteLine("Successful account registration");
                    Console.WriteLine("Login to explore our products.");
                    Console.WriteLine(readKey);
                    Console.ReadKey();
                    Console.Clear();
                    Console.ForegroundColor=ConsoleColor.White;
                    Check_Valid=false;
                }        
            }      
        }
        public void Sign_In(string username,string password){
                Console.Clear();
            foreach(Account item in list){
                check_Match=false;
                if(String.Compare(item.Username, username,true) == 0 && String.Compare(item.Password, password,false) == 0){
                Console.ForegroundColor=ConsoleColor.Cyan;
                Console.WriteLine($"Hello {username}!");
                Console.WriteLine("There are many products for you to choose from <3");
                Console.ForegroundColor=ConsoleColor.White;
                Console.WriteLine(readKey);
                Console.ReadKey();
                Console.Clear();
                check_Match=true;
                break;
                }
            }
            if(check_Match==false){
                Console.ForegroundColor=ConsoleColor.DarkRed;
                Console.WriteLine("Login failed, please check your account information and password!");
                Console.ForegroundColor=ConsoleColor.White;
            }
        }
        
    }
}


