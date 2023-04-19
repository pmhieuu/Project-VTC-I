using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using Models;
namespace Services
{
    public class Login_Service:Connection
    {
        public bool Check_Valid;
        public bool check_Match;
        static string readKey="Press any key to Continue";
        List<Account> list=new List<Account>();
        public int checkrole=0;
        public string us;
        public void Select(){
           connection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM Account;", connection);// thay doi user = tablekhac
            using (MySqlDataReader reader = command.ExecuteReader())
            {
            while (reader.Read())
            {   
                string username =$"{reader["username"]}";
                string password =$"{reader["password"]}";
                int role =int.Parse($"{reader["role"]}");
                int id_account=int.Parse($"{reader["ID_Account"]}");
                list.Add(new Account(){Username=username,Password=password,Role=role,ID_Account=id_account}); 
            }
            reader.Close();
            }
            connection.Close();
        }
      
        public void Create_Account(string username, string password)// them thong tin cua doi tuong user 
        {   connection.Open();
            MySqlCommand command = new MySqlCommand($"insert into Account(username,password)value('{username}','{password}');", connection); 
           using (MySqlDataReader reader = command.ExecuteReader())
        {
        reader.Close();
        }connection.Close();
}       
public void Create_Information(string username,string fullname, string phone,string email,string address)
        {   connection.Open();
            MySqlCommand command = new MySqlCommand($"insert into Customer(Customer_username,fullname,phone,email,address)value('{username}','{fullname}','{phone}','{email}','{address}');", connection); 
           using (MySqlDataReader reader = command.ExecuteReader())
        {
        reader.Close();
        }connection.Close();
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
                 Check_Valid=true;
            }
            else if(username.Length>=16||password.Length>=16){
                Console.ForegroundColor=ConsoleColor.DarkRed;
                Console.WriteLine("Please enter less than 16 characters!");
                Console.ForegroundColor=ConsoleColor.White;               
                Check_Valid=true;
            }
             else if(phone.Length >10){
                Console.WriteLine("service only support in Vietnam area!");
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
                    Create_Information(username,fullname,phone,email,address);
                    Create_Account(username,password);
                    Console.ForegroundColor=ConsoleColor.DarkBlue;
                    Console.WriteLine("Successful account registration");
                    Console.WriteLine("Login to explore our products.");
                    Console.WriteLine(readKey);
                    Console.ReadKey();
                    Console.Clear();
                    Sign_In(username, password);
                    Console.ForegroundColor=ConsoleColor.White;
                    Check_Valid=false;
                }        
            }      
        }
        public void Sign_In(string username,string password){
                list.Clear();
                Console.Clear();
                Select();
                foreach(Account item in list){
                check_Match=false;
                if(String.Compare(item.Username, username,true) == 0 && String.Compare(item.Password, password,false) == 0){
                Console.ForegroundColor=ConsoleColor.Cyan;
                this.checkrole=item.Role;
                this.us=item.Username;
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


