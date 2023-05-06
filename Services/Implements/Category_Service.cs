using MySql.Data;
using MySql.Data.MySqlClient;
using Models;


public class Category_Service:Services.Connection
{ 
  public bool check_Match;
  public bool check_Valid;
   public bool check_Match_new_category;
  static string readKey="Press any key to Continue";
  public List<Category> list_category=new List<Category>();

      
  public void Select()
  {
    connection.Open();
   MySqlCommand command = new MySqlCommand("SELECT * FROM Category ORDER BY category;", connection);// thay doi user = tablekhac
            using (MySqlDataReader reader = command.ExecuteReader())
            {
            while (reader.Read())
            {   
                string name =$"{reader["category"]}";
                int id =int.Parse($"{reader["ID_Category"]}");
                int status =int.Parse($"{reader["status"]}");
                int id_staff =int.Parse($"{reader["id_staff"]}");
                list_category.Add(new Category(){Name=name,ID=id,Status=status,ID_Staff=id_staff});
            }
            reader.Close();
            }
            connection.Close();
  } 
  public void Add(string category,int id_staff){ 
      foreach (Category item in list_category)
      {  check_Match=false;
        if (string.Compare(item.Name,category,true)==0 && item.Status==0){
          check_Match=true; break;
        }
      }
      if(string.IsNullOrEmpty(category)==true){
        Console.ForegroundColor=ConsoleColor.DarkRed;
        Console.WriteLine("Please complete all information!");
        Console.ForegroundColor=ConsoleColor.White;
        check_Valid=false;
      }
      else if(check_Match==true){
                Console.ForegroundColor=ConsoleColor.DarkRed;
                Console.WriteLine("The category is already! Try another category.");
                Console.ForegroundColor=ConsoleColor.White;
                check_Valid=false;
      }
      else{
        foreach (Category item in list_category)
        { check_Match=false;
          if(string.Compare(item.Name,category,true)==0 && item.Status==1){
            check_Match=true;
              connection.Open();
          MySqlCommand command = new MySqlCommand($"Update Category set status='0' where category='{category}';", connection); 
            using (MySqlDataReader reader = command.ExecuteReader())
          {
          reader.Close();
          }
          connection.Close();
          break;
            }
          
        }
        if(check_Match==false){
              connection.Open();
        MySqlCommand command = new MySqlCommand($"insert into Category(Category,ID_Staff)value('{category}','{id_staff}');", connection); 
           using (MySqlDataReader reader = command.ExecuteReader())
        {
        reader.Close();
        }
        connection.Close();
          
        }
        
                Console.ForegroundColor=ConsoleColor.DarkBlue;
                Console.WriteLine("Added successfully");
                Console.ForegroundColor=ConsoleColor.White;
        getAll();
        check_Valid=true;
      }
       
  }
  public void check_empty_duplicate(string category){
    list_category.Clear();
    Select();
    foreach (Category item in list_category)
      {  check_Match=false;
        if (string.Compare(item.Name.ToLower(),category.ToLower(),true)==0){
          check_Match=true;
          check_Valid=true; break;
        }
      }
       if(string.IsNullOrEmpty(category)==true){
        Console.Clear();
        Console.ForegroundColor=ConsoleColor.DarkRed;
        Console.WriteLine("Please complete all information!");
        
        Console.ForegroundColor=ConsoleColor.White;
        check_Valid=false;
      }
      else if(check_Match==false){
        Console.Clear();
                Console.ForegroundColor=ConsoleColor.DarkRed;
                Console.WriteLine("The category is not exist!");
                Console.ForegroundColor=ConsoleColor.White;
                check_Valid=false;
      }
      
  }

  // public void Update(string category){ // cần check trùng category
  //       Console.WriteLine("================================================");                                                   // khi nhập không được để trống
  //     foreach (Category item in list_category)
  //     {  check_Match=false;
  //       if (string.Compare(item.Name,category,true)==0&& item.Status==0){

  //         Console.WriteLine("ID: "+item.ID+"\n"+"Category: "+item.Name+"\n");
  //                                   Console.WriteLine("================================================");

  //         check_Match=true; break;
  //       }
  //     }
  //      if(string.IsNullOrEmpty(category)==true){
  //       Console.Clear();
  //       Console.ForegroundColor=ConsoleColor.DarkRed;
  //       Console.WriteLine("Please complete all information!");
  //       Console.ForegroundColor=ConsoleColor.White;
  //       check_Valid=false;
  //     }
  //     else if(check_Match==false){
  //       Console.Clear();
  //               Console.ForegroundColor=ConsoleColor.DarkRed;
  //               Console.WriteLine("The category is not exist!");
  //               Console.ForegroundColor=ConsoleColor.White;
  //               check_Valid=false;
                
  //     }
  //     else{
  //       Console.WriteLine("Enter new category");
  //       string new_category=Console.ReadLine();
  //       if(string.IsNullOrEmpty(new_category)==true){
  //       Console.ForegroundColor=ConsoleColor.DarkRed;
  //       Console.WriteLine("Please complete all information!");
  //       Console.ForegroundColor=ConsoleColor.White;
  //       check_Valid=false;
  //     }
  //       else{
  //         foreach (Category item in list_category)
  //       {  check_Match_new_category=false;
  //         if (string.Compare(item.Name,new_category,true)==0){
  //           check_Match_new_category=true; 
  //           Console.Clear();
  //           Console.ForegroundColor=ConsoleColor.DarkRed;
  //           Console.WriteLine("The category already exists!");
  //           Console.ForegroundColor=ConsoleColor.White;
  //           check_Valid=false;
  //           break;
  //         }
  //     }
  //   }            
  //     if(check_Match==true&&check_Match_new_category==false){
  //       check_Valid=true;
  //       Console.Clear();
  //       connection.Open();
  //       MySqlCommand command = new MySqlCommand($"Update category Set Category='{new_category}' where Category='{category}';", connection); 
  //          using (MySqlDataReader reader = command.ExecuteReader())
  //       {
  //       reader.Close();
  //       }
  //       connection.Close();
  //       Console.Clear();
  //       Console.ForegroundColor=ConsoleColor.DarkBlue;
  //         Console.WriteLine("Update successfully");
  //         Console.ForegroundColor=ConsoleColor.White;
  //       getAll();
  //     }
  //     }
    
  // }
  
  //  public void Delete(string category){ // cần check trùng category
  //                                       // khi nhập không được để trống
  //     foreach (Category item in list_category)
  //     {  check_Match=false;
  //       if (string.Compare(item.Name,category,true)==0){
  //         check_Match=true; break;
  //       }
  //     }
  //      if(string.IsNullOrEmpty(category)==true){
  //       Console.Clear();
  //       Console.ForegroundColor=ConsoleColor.DarkRed;
  //       Console.WriteLine("Please complete all information!");
  //       Console.ForegroundColor=ConsoleColor.White;
  //       check_Valid=false;
  //     }
  //     else if(check_Match==false){       
  //       Console.Clear();         
  //               Console.ForegroundColor=ConsoleColor.DarkRed;
  //               Console.WriteLine("The category is not exist!");
  //               Console.ForegroundColor=ConsoleColor.White;
  //               check_Valid=false;
                
  //     }
  //     else{
  //       Console.Clear();
  //       Console.ForegroundColor=ConsoleColor.DarkBlue;
  //       Console.WriteLine("Warning, if you delete this category, the products in this category will also be deleted");
  //       Console.WriteLine("Are you sure?");
  //       Console.WriteLine("1.Yes");
  //       Console.WriteLine("0.No");
  //       Console.ForegroundColor=ConsoleColor.White;
  //       Console.WriteLine("Enter your choose");
  //       int ask=int.Parse(Console.ReadLine());
  //       if(ask==1){
  //         connection.Open();
  //         MySqlCommand command = new MySqlCommand($"Update category Set status='1' where Category='{category}';", connection); 
  //          using (MySqlDataReader reader = command.ExecuteReader())
  //       {
  //       reader.Close();
  //       } connection.Close();
  //       foreach (Category item in list_category){
  //           if(string.Compare(item.Name, category,true) == 0){
  //             connection.Open();
  //             command = new MySqlCommand($"Update Product Set status='1' where ID_category='{item.ID}';", connection); 
  //           using (MySqlDataReader reader = command.ExecuteReader())
  //           {
  //           reader.Close();
  //           }
  //           connection.Close();
  //           }
  //       }
         
      
        
  //         Console.Clear();
  //         Console.ForegroundColor=ConsoleColor.DarkBlue;
  //         Console.WriteLine("Delete successfully");
  //         Console.ForegroundColor=ConsoleColor.White;
  //        check_Valid=true;
  //       getAll();
  //       }
  //       else if(ask==0)
  //       {
  //         check_Valid=true;//vua thay doi ket qua 10:30 false=>true
  //       }
  //       else{
  //         Console.Clear();
  //         Console.WriteLine("Invalid Selection");
  //         check_Valid=false;
  //       }
        
  //     }
  // }
    public void Find(string category,int id_staff){
      
        int count = 0;
        Console.WriteLine("Search Results");
        foreach (Category item in list_category)
        {     
                                                      Console.WriteLine("================================================");

            if (item.Status == 0 && item.Name.ToLower().Contains(category.ToLower())==true&&item.ID_Staff==id_staff){
              count++;
                Console.WriteLine("ID: "+item.ID+"\n"+"Category: "+item.Name+"\n");
                                          Console.WriteLine("================================================");

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
            check_Valid=true;
          }
          else if(ask==1){
            check_Valid=false;
          }
          else {
            Console.Clear();
            Console.WriteLine("Invalid selection");
            check_Valid=false;
          }
        }
    }
    public void getByID(int id_staff){
       list_category.Clear();
        Select();
          Console.WriteLine("-->List category");
           Console.WriteLine("================================================");
        foreach (Category item in list_category)
        {
          if(item.ID_Staff==id_staff){
              Console.WriteLine("ID: "+ item.ID+"\n"+"Category: "+ item.Name+"\n");
                                          Console.WriteLine("================================================");
          }
        }
    }
    public void getAll(){
        list_category.Clear();
        Select();
          Console.WriteLine("-->List category");
           Console.WriteLine("================================================");
          for (int i = 0; i < list_category.Count; i++)
          { 
            if(list_category[i].Status==0){
                Console.WriteLine("ID: "+list_category[i].ID+"\n"+"Category: "+list_category[i].Name+"\n");
                                          Console.WriteLine("================================================");

            }
            
          }
        Console.WriteLine(readKey);
        Console.ReadKey();
     
      
    }
}