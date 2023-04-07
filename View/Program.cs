using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Models;
using Services;
 public class Program:Login_Service{
 public static void Main(string[] args){
            Program program=new Program();
            program.connection.Open();
            program.Select();
            string username;
            string password;
            string fullname;
            string phone;
            string email;
            string address;
            int choose;
            do
            {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("========================================================");
            Console.WriteLine("Welcome to Login page");
            Console.WriteLine("1.Signin");
            Console.WriteLine("2.Signup");
            Console.WriteLine("0.Exit");
            Console.Write("Enter your choose:");
            choose=int.Parse(Console.ReadLine());
            Console.Clear();
            switch (choose)
            {
                case 2:
                do{
            program.Check_Valid=false;
            Console.Write("Enter username:");
            username=Console.ReadLine();
            Console.Write("Enter password:");
            password=Console.ReadLine(); 
            Console.Write("Enter fullname:");
            fullname=Console.ReadLine(); 
            Console.Write("Enter your numberphone:");
            phone=Console.ReadLine();
            Console.Write("Enter your email:");
            email=Console.ReadLine(); 
             Console.Write("Enter your address:");
            address=Console.ReadLine();
            program.Sign_Up(username,password,fullname,phone,email,address);          
            }while(program.Check_Valid==true);
                break;
                case 1:
                do
            {  
            program.check_Match=true;
            Console.Write("Enter username:");
            username=Console.ReadLine();
            Console.Write("Enter password:");
            password=Console.ReadLine(); 
            program.Sign_In(username,password);
            } while (program.check_Match==false);
                break;
                default:
                break;
            }
           
        } while (choose!=0);
    }
 }