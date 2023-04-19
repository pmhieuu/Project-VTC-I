using MySql.Data.MySqlClient;
using Models;
using Services;

        Login_Service login_Service = new Login_Service();
        //login_Service.connection.Open();
        
        
    
    
    
        
    

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
  Console.WriteLine("Welcome to Order Aplication");
  Console.WriteLine("Account Order");
  Console.WriteLine("1.Signin");
  Console.WriteLine("2.Signup");
  Console.WriteLine("0.Exit");
  Console.Write("Enter your choose:");
  choose = int.Parse(Console.ReadLine());
  Console.Clear();
  switch (choose)
  {
    case 2:
        do
        {
            login_Service.Check_Valid = false;
            Console.Write("Enter username:");
            username = Console.ReadLine();
            Console.Write("Enter password:");
            password = Console.ReadLine();
            Console.Write("Enter fullname:");
            fullname = Console.ReadLine();
            Console.Write("Enter your numberphone:");
            phone = Console.ReadLine();
            Console.Write("Enter your email:");
            email = Console.ReadLine();
            Console.Write("Enter your address:");
            address = Console.ReadLine();
            login_Service.Select();
            login_Service.Sign_Up(username, password, fullname, phone, email, address);
            
        } while (login_Service.Check_Valid == true);
        //login_Service.connection.Close();
        
       
      
      
     
        
      break;
    case 1:
      do
      {
        login_Service.check_Match = true;
        Console.Write("Enter username:");
        username = Console.ReadLine();
        Console.Write("Enter password:");
        password = Console.ReadLine();
        login_Service.Sign_In(username, password);
        } while (login_Service.check_Match == false);
       
        if(login_Service.checkrole==1){    //Staff
          Category_Service category_Service = new Category_Service();
          Product_Service product_Service=new Product_Service();
          
          Staff_Service staff_Service = new Staff_Service();
          //staff_Service.connection.Open();
          staff_Service.Select(login_Service.us);
        
          int choose_home=0;
          do
          {
          
          Console.WriteLine("========================================================");
          Console.WriteLine("Home");
          Console.WriteLine("1.Manage category");
          Console.WriteLine("2.Manage product");
          Console.WriteLine("3.Manage information");
          Console.WriteLine("4.Manage order");
          Console.WriteLine("0.Exit");
          Console.Write("Enter your choose:");
          choose_home = int.Parse(Console.ReadLine());
          Console.Clear();
          switch (choose_home)
          {
            case 0:
            break;
            case 1:
             
            //category_Service.connection.Open();
             category_Service.Select();
            string category;
            int choose_category=0;
            do
            {
            Console.WriteLine("========================================================");
            Console.WriteLine("Manage category");
            Console.WriteLine("1.Displaying all category");
            Console.WriteLine("2.Add a new category");
            Console.WriteLine("3.Update a category");
            Console.WriteLine("4.Delete a category");
            Console.WriteLine("5.Find");
            Console.WriteLine("0.Exit");
            Console.Write("Enter your choose:");
            choose_category=int.Parse(Console.ReadLine());
            Console.Clear();
            switch (choose_category)
            {
              
              case 0:
              //category_Service.connection.Close();
              break;
              case 1:
              category_Service.getAll();
              break;
              case 2:
              do
              {
              category_Service.check_Valid=true;
              Console.WriteLine("Adding new category");
              Console.WriteLine("Enter category:");
              category=Console.ReadLine();
              category_Service.Add(category);
              } while (category_Service.check_Valid!=true);
              break;
              case 3:
              do
              {
              category_Service.check_Valid=true;
              Console.WriteLine("Updating category");
              Console.WriteLine("Enter category what you want to update:");
              category=Console.ReadLine();
              category_Service.Update(category);
              } while (category_Service.check_Valid!=true);
              break;

              case 4:
               do
              {
              category_Service.check_Valid=true;
              Console.WriteLine("Deleting category");
              Console.WriteLine("Enter category what you want to delete:");
              category=Console.ReadLine();
              category_Service.Delete(category);
              } while (category_Service.check_Valid!=true);
              break;
              case 5:
               do
              {
              category_Service.check_Valid=true;
              Console.WriteLine("Finding category");
              Console.WriteLine("Enter category:");
              category=Console.ReadLine();
              category_Service.Find(category);
              } while (category_Service.check_Valid!=true);
              break;
              default:
              Console.WriteLine("Invalid selection");
              break;
            }
              
            } while (choose_category!=0);
            break;
            case 2:
              int choose_product=0;
              int category_id=0;
              string product;
              double price=0;
              int quantity=0;
              // product_Service.connection.Close();
              // product_Service.connection.Open();
              
              //staff_Service.connection.Open();
              staff_Service.Select(login_Service.us);
              Console.WriteLine(login_Service.us);
              do
              { product_Service.Select();
                Console.WriteLine("========================================================");
                Console.WriteLine("List of categories");
                Console.WriteLine("1.View products by category");
                Console.WriteLine("2.Add a new product");
                Console.WriteLine("3.Update a product");
                Console.WriteLine("4.Delete a product");
                Console.WriteLine("5.Find");
                Console.WriteLine("0.Exit");
                Console.Write("Enter your choose:");
                choose_product=int.Parse(Console.ReadLine());
                Console.Clear();
                switch (choose_product)
                {
                  case 0:
                  // product_Service.connection.Close();
                  // staff_Service.connection.Close();
                  break;
                  case 1:
                  
                  do
                  { 
                    Console.Clear();
                    category_Service.list_category.Clear();
                    category_Service.Select();
                    
                    foreach (Category item in category_Service.list_category)
                    {
                        if (item.Status == 0 ){
                         
                            Console.WriteLine(item.ID+"\t"+item.Name);
                        }
                    }
                    Console.WriteLine("View products by category");
                    Console.WriteLine("Enter category:");
                    category=Console.ReadLine();
                    category_Service.check_empty_duplicate(category);
                  } while (category_Service.check_Valid!=true);
                    // category_Service.connection.Close();
                    product_Service.getByProduct(category);
                  break;
                  case 2:
                  do {
                    Console.WriteLine("Adding new product");
                    Console.WriteLine("Enter new product:");
                    product=Console.ReadLine();
                    Console.WriteLine("Enter category id:");
                    category_id=int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter price:");
                    price=double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter quantity:");
                    quantity=int.Parse(Console.ReadLine());
                    product_Service.Add(staff_Service.id,category_id,product,quantity,price);
                  } while(product_Service.Check_Valid!=true);
                  break;
                  case 3:
                  do
                  {
                    Console.WriteLine("Updating product");
                    Console.WriteLine("Enter product:");
                    product=Console.ReadLine();
                    product_Service.Update(product,staff_Service.id);
                    } while (product_Service.Check_Valid!=true);
                  break;
                  case 4:
                  do{
                    Console.WriteLine("Deleting product");
                    Console.WriteLine("Enter product:");
                    product=Console.ReadLine();
                    product_Service.Delete(product);
                    }while(product_Service.Check_Valid!=true);
                  break;
                  case 5:
                    do
                    {
                      Console.WriteLine("Finding product");
                    Console.WriteLine("Enter product:");
                    product=Console.ReadLine();
                    product_Service.findProduct(product);
                    } while (product_Service.Check_Valid!=true);
                  break;
                  default:
                  break;
                }
              } while (choose_product!=0);
            break;
            case 3:
          //  staff_Service.connection.Close();
          //   staff_Service.connection.Open();
            //staff_Service.Select(login_Service.us);
            int choose_info = 0;
            do
            {
            Console.WriteLine("1.View your information");
            Console.WriteLine("2.Edit personal information");
            Console.WriteLine("0.Exit");
            Console.WriteLine("Enter your choose:");
            choose_info=int.Parse(Console.ReadLine());
            switch (choose_info)
            {
              case 0:
             // staff_Service.connection.Close();
              break;
              case 1:
              Console.WriteLine("Your information");
              staff_Service.getInformation(login_Service.us);
              break;
              case 2:
               int edit=0;
                do
                {
                  Console.WriteLine("1.Edit your phone");
                  Console.WriteLine("2.Edit Email");
                  Console.WriteLine("0.Exit");
                  Console.WriteLine("Enter your choose:");
                  edit=int.Parse(Console.ReadLine());
                  switch (edit)
                  {
                    case 0:
                    // staff_Service.connection.Close();
                    break;
                    case 1:
                    staff_Service.editPhone(login_Service.us);
                    break;
                    case 2:
                    staff_Service.editEmail(login_Service.us);
                    break;
                    default:
                    break;
                  }
                } while (edit!=0);
              break;
              default:
              break;
            }
            } while (choose_info!=0);
            break;
            default:
            Console.WriteLine("Invalid selection");
            break;
          }
          } while (choose_home!=0);
        }
        else if(login_Service.checkrole==0){
          //category_Service.Select();
          //category_Service.getAll();
        }
      break;
    default:
    Console.WriteLine("Invalid selection");
      break;
  }

} while (choose != 0);
