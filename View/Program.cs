using MySql.Data.MySqlClient;
using Models;
using Services;
  int choose;   
  Console.Clear();
        Login_Service login_Service = new Login_Service();
        Order_Service order_Service = new Order_Service();
        int choose_customer=0;
            string category;
            string product1;
            Category_Service category_Service = new Category_Service();
            Product_Service product_Service=new Product_Service();
            Order_Detail_Service order_Detail_Service = new Order_Detail_Service();
            Customer_Service customer_Service = new Customer_Service();    
do
{
        
           
         
          
          Staff_Service staff_Service = new Staff_Service();
        //login_Service.connection.Open();
        
    

    string username;
    string password;
    string fullname;
    string phone;
    string email;
    string address;


    product_Service.list_product.Clear();
  order_Service.list_orders.Clear();
              product_Service.list_product_queue.Clear();
              category_Service.list_category.Clear();
              order_Detail_Service.list_order_details.Clear();
        order_Service.select();
        order_Detail_Service.Select();
        category_Service.Select();
         product_Service.Select();
        Console.Clear();
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
   
        do{
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
            
           do
            {
              Console.WriteLine("Home");
            Console.WriteLine("1.Buy now");
            Console.WriteLine("2.Cart");
            Console.WriteLine("3.History");
            Console.WriteLine("4.Setting information");
            Console.WriteLine("5.Become a seller");
            Console.WriteLine("0.Exit");
            Console.WriteLine("Enter your choose:");
            choose_customer=int.Parse(Console.ReadLine());
            Console.Clear();
            switch (choose_customer)
            {
              case 0:
              break;
              case 1:
              int choose_buy=0;
              
                do
                { 
                  Console.WriteLine("--> List product page");
                  Console.WriteLine("1.View Product by category");
                  Console.WriteLine("2.Find product");
                  Console.WriteLine("0.Exit");
                  Console.WriteLine("Enter your choose:");
                  choose_buy=int.Parse(Console.ReadLine());
                  Console.Clear();
                  switch (choose_buy){
                    case 0:
                    break;
                    case 1:
                    Console.Clear();
                    category_Service.list_category.Clear();
                    category_Service.Select();
                    category_Service.getAll();
                     do
                    { 
                    Console.WriteLine("--> View products by category");
                    Console.WriteLine("Enter category:");
                    category=Console.ReadLine();
                     Console.Clear();
                    category_Service.check_empty_duplicate(category);
                   
                    } while (category_Service.check_Valid!=true);
                    product_Service.getByProduct(category);
                    product_Service.Buy();
                   
                    break;
                    case 2:
                    do
                    {Console.Clear();
                      product_Service.Check_Valid = false;
                    Console.WriteLine("--> Find product");
                    Console.WriteLine("Enter product:");
                    product1=Console.ReadLine();
                    product_Service.findProduct(product1);
                    } while (product_Service.Check_Valid =false);
                    product_Service.Buy();
                    break;
                  }
                } while (choose_buy!=0);
              break;
              case 2:
                int choose_cart=0;
                if(product_Service.list_product_queue.Count==0){
                  Console.WriteLine("Cart is empty");
                  Console.ReadKey();
                }
                else{
                do{
                  
                Console.WriteLine("1.Checkout");
                Console.WriteLine("2.Cancel");
                Console.WriteLine("0.Exit");
                Console.WriteLine("Enter your choose:");
                choose_cart=int.Parse(Console.ReadLine());
                customer_Service.Select(login_Service.us);
                if(choose_cart==0){Console.Clear();}
                else if(choose_cart==1){
                  product_Service.cart();
                 // if(order_Detail_Service.id_order>0){
                      order_Detail_Service.list_order_details.Clear();
                      order_Detail_Service.Select();
                //  }
                //  else{
                  // id product price quantity van chua hien thi
                  order_Detail_Service.payment(customer_Service.id,customer_Service.fullname,customer_Service.phone,customer_Service.address,product_Service.id_product,product_Service.product,product_Service.price,product_Service.quantity,product_Service.list_product_queue);
                  if(order_Detail_Service.check_valid==true){
                    choose_cart=0;
                    product_Service.list_product_queue.Clear();
                     Console.ReadKey();
                Console.Clear();
                  }
             // }
                }
                else if(choose_cart==2){
                  product_Service.cart();
                  System.Console.WriteLine("Enter name product:");
                  product1=Console.ReadLine();
                  product_Service.huycart(product1);
                  if(product_Service.list_product_queue.Count==0){
                    System.Console.WriteLine("Cart is empty");
                    choose_cart=0;
                     Console.ReadKey();
                Console.Clear();
                  }
                }
               
              }while(choose_cart!=0);
              }
              break;
              case 3:// lich su hoa don
              int lshd=0;
              do {
               order_Detail_Service.list_order_details.Clear();
              order_Detail_Service.Select();
              order_Service.list_orders.Clear();
              order_Service.select();
              customer_Service.list_customer.Clear();
              customer_Service.Select(login_Service.us);
              System.Console.WriteLine("--> History");
              Console.WriteLine("1.Waiting");
              Console.WriteLine("2.Delivered");
              Console.WriteLine("3.Cancelled");
              Console.WriteLine("0.Exit");
              System.Console.WriteLine("Enter your chooose:");
              lshd=int.Parse(Console.ReadLine());
              if(lshd==0){Console.Clear();}
              else if(lshd==1){
                System.Console.WriteLine("Waiting");
                order_Detail_Service.getbystatus(0,order_Service.list_orders,customer_Service.fullname);}
              else if(lshd==2){
                System.Console.WriteLine("Delivered");
                order_Detail_Service.getbystatus(1,order_Service.list_orders,customer_Service.fullname);}
              else if(lshd==3){
                System.Console.WriteLine("Cancelled");
                order_Detail_Service.getbystatus(2,order_Service.list_orders,customer_Service.fullname);}
              else{
                Console.WriteLine("Invalid selection");
              }
              }while(lshd!=0);
              break;
              case 4:
              int edit_info=0;
              do
              {
              Console.WriteLine("--> Setting information");
              Console.WriteLine("1.View Information");
              Console.WriteLine("2.Edit Information");
              Console.WriteLine("0.Exit");
              Console.WriteLine("Enter your choose:");
              edit_info=int.Parse(Console.ReadLine());
              Console.Clear();
              if(edit_info==1){
                customer_Service.getInformation(login_Service.us);
              }
              else if(edit_info==2){
                int choose_info_edit=0;
                do
                {
                Console.WriteLine("--> Edit your information");
                Console.WriteLine("1.Edit phone");
                Console.WriteLine("2.Edit email");
                Console.WriteLine("3.Edit address");
                Console.WriteLine("0.Exit");
                Console.WriteLine("Enter your choose");
                choose_info_edit=int.Parse(Console.ReadLine());
                Console.Clear();
                if(choose_info_edit==1){
                  customer_Service.editPhone(login_Service.us);
                }
                else if(choose_info_edit==2){
                  customer_Service.editEmail(login_Service.us);
                }
                else if(choose_info_edit==3){
                  customer_Service.editAddress(login_Service.us);
                }
                else if(choose_info_edit==0){}
                else{
                  Console.WriteLine("Invalid selection");
                }
                } while (choose_info_edit!=0);
              }
              else{
                 Console.WriteLine("Invalid selection");
              }
              } while (edit_info!=0);
              break;
              case 5:
              Console.WriteLine("--> Register to become a seller");
              customer_Service.become_seller(login_Service.us);
              Console.ForegroundColor=ConsoleColor.DarkGreen;
              Console.WriteLine("--> Done");
              Console.ForegroundColor=ConsoleColor.White;
              Console.WriteLine("Please login again to use the service");
              Console.ReadKey();
              Console.Clear();
              choose_customer=0;
              break;
              default:
              Console.WriteLine("Invalid selection!");
              break;
            }
            } while (choose_customer!=0);  
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
          
          staff_Service.list_staff.Clear();
          staff_Service.Select(login_Service.us);
          //Console.WriteLine(staff_Service.id);
          product_Service.list_product.Clear();
          product_Service.Select();
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
            category_Service.list_category.Clear();
             category_Service.Select();
           
            int choose_category=0;
            do
            {
            Console.WriteLine("========================================================");
            Console.WriteLine("Manage category");
            Console.WriteLine("1.Displaying all category");// sua lai category chi xem duoc caterory chinh banthan staff dang
            Console.WriteLine("2.Add a new category");
            // Console.WriteLine("3.Update a category");
            // Console.WriteLine("4.Delete a category");
            Console.WriteLine("3.Find");
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
              category_Service.getByID(staff_Service.id);
              break;
              case 2:
              do
              {
              category_Service.check_Valid=true;
              Console.WriteLine("--> Adding new category");
              Console.WriteLine("Enter category:");
              category=Console.ReadLine();
              category_Service.Add(category,staff_Service.id);
              } while (category_Service.check_Valid!=true);
              break;
              // case 3:
              // do
              // {
              // category_Service.check_Valid=true;
              // Console.WriteLine("--> Updating category");
              // Console.WriteLine("Enter category what you want to update:");
              // category=Console.ReadLine();
              // category_Service.Update(category);
              // } while (category_Service.check_Valid!=true);
              // break;

              // case 4:
              //  do
              // {
              // category_Service.check_Valid=true;
              // Console.WriteLine("--> Deleting category");
              // Console.WriteLine("Enter category what you want to delete:");
              // category=Console.ReadLine();
              // category_Service.Delete(category);
              // } while (category_Service.check_Valid!=true);
              // break;
              case 3:
               do
              {
              category_Service.check_Valid=true;
              Console.WriteLine("--> Finding category");
              Console.WriteLine("Enter category:");
              category=Console.ReadLine();
              category_Service.Find(category,staff_Service.id);
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
            
              do
              { product_Service.list_product.Clear();
                product_Service.Select();
              product_Service.checklist(staff_Service.id);
                Console.WriteLine("========================================================");
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
                  break;
                  case 1:
                  do{
                  do
                  { 
                    Console.Clear();
                    category_Service.list_category.Clear();
                    category_Service.Select();
                    
                    category_Service.getAll();
                    Console.WriteLine("--> View products by category");
                    Console.WriteLine("Enter category:");
                    category=Console.ReadLine();
                    Console.Clear();
                    category_Service.check_empty_duplicate(category);
                  } while (category_Service.check_Valid!=true);
                    // category_Service.connection.Close();
                    product_Service.getProductByStaff(category,staff_Service.id);
                    }while(product_Service.Check_Valid!=true);
                  break;
                  case 2:
                  
                    Console.WriteLine("--> Adding new product");
                    Console.WriteLine("Enter new product:");
                    product=Console.ReadLine();
                    Console.WriteLine("Enter category:");
                    category=Console.ReadLine();
                    Console.WriteLine("Enter price:");
                    price=double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter quantity:");
                    quantity=int.Parse(Console.ReadLine());
                    product_Service.Add(staff_Service.id,category,product,quantity,price);
                 
                  Console.ReadKey();
                  Console.Clear();
                  break;
                  case 3:
                  
                  if(product_Service.counter==0){
                      Console.WriteLine("You have not posted any products yet");
                      Console.ReadKey();
                      Console.Clear();
                  }
                  else{
                    product_Service.getAll(staff_Service.id);
                 
                    Console.WriteLine("--> Updating product");
                    Console.WriteLine("Enter product:");
                    product=Console.ReadLine();
                    product_Service.Update(product,staff_Service.id);
                    
                    Console.ReadKey();
                      Console.Clear();
                    }
                    
                  break;
                  case 4:
                  if(product_Service.counter==0){
                      Console.WriteLine("You have not posted any products yet");
                       Console.ReadKey();
                      Console.Clear();
                  }
                  else{
                    product_Service.getAll(staff_Service.id);
                  
                    
                    Console.WriteLine("--> Deleting product");
                    Console.WriteLine("Enter product:");
                    product=Console.ReadLine();
                    product_Service.Delete(product,staff_Service.id);
                   
                    Console.ReadKey();
                      Console.Clear();
                  }
                  break;
                  case 5:
                  if(product_Service.counter==0){
                      Console.WriteLine("You have not posted any products yet");
                       Console.ReadKey();
                      Console.Clear();
                  }
                  else{
                   
                    do
                    {
                      Console.WriteLine("--> Finding product");
                    Console.WriteLine("Enter product:");
                    product=Console.ReadLine();
                    Console.Clear();
                    product_Service.findProductByStaff(product,staff_Service.id);
                    } while (product_Service.Check_Valid!=true);
                    Console.ReadKey();
                      Console.Clear();
                  }
                  break;
                  default:
                  break;
                }
              } while (choose_product!=0);
            break;
            case 3:
         
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
            // sádad
              break;
              case 1:
              Console.WriteLine("--> Your information");
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
            case 4: //neu chua co don thi ghi la chua co don hang
            int choose_mana_order=0;
            do
            { order_Service.list_orders.Clear();
              order_Service.select();
              product_Service.list_product.Clear();
              product_Service.Select();
              Console.Clear();
              Console.WriteLine("--> Manage order");
              Console.WriteLine("1.View order");
              Console.WriteLine("2.Accept or Cancel order");
             
              Console.WriteLine("0.Exit");
              choose_mana_order=int.Parse(Console.ReadLine());// mai lam tiep...
              Console.Clear();
              if(choose_mana_order==0){}
              else if(choose_mana_order==1){
                Console.Clear();
                Console.WriteLine("View order");
                order_Detail_Service.displayProductForEachIdStaff(staff_Service.id,product_Service.list_product);
                Console.ReadKey();
                // order_Service.getAll();
              }
            
              else if(choose_mana_order==2){
                Console.Clear();
                order_Detail_Service.displayProductForEachIdStaffWaiting(staff_Service.id,product_Service.list_product);
                if(order_Detail_Service.list_id_product.Count==0){}
                else{
                string name_product;
                int id_order_dt=0;
                do
                {
                Console.WriteLine("Enter Product:");
                 name_product=Console.ReadLine();
                 Console.WriteLine("Enter ID Order:");
                 id_order_dt=int.Parse(Console.ReadLine());
                order_Detail_Service.checkid(name_product,id_order_dt);
                } while (order_Detail_Service.check_valid!=true);
                int AD=0;
                do
                {
                  
                
                Console.WriteLine("1. Accept this order");
                Console.WriteLine("2. Cancel this order");
                AD=int.Parse(Console.ReadLine());
                if(AD==1){
                  order_Detail_Service.accept(name_product,id_order_dt,staff_Service.id);
                  //order_Detail_Service.delete(id_or,1);
                  AD=0;
                }
                else if(AD==2){
                  order_Detail_Service.cancel(name_product,id_order_dt,staff_Service.id);
                   //order_Detail_Service.delete(id_or,2);
                   AD=0;
                }
                else{
                  Console.ForegroundColor=ConsoleColor.DarkRed;
                  Console.WriteLine("Invalid selection");
                  Console.ForegroundColor=ConsoleColor.White;
                }
                } while (AD!=0);
              }
              }
             else{
                  Console.ForegroundColor=ConsoleColor.DarkRed;
                  Console.WriteLine("Invalid selection");
                  Console.ForegroundColor=ConsoleColor.White;
                }
               } while (choose_mana_order!=0);
               
            break;
            default:
            Console.WriteLine("Invalid selection");
            break;
          }
          } while (choose_home!=0);
        }
        
        else if(login_Service.checkrole==0){
            
            do
            {
              Console.WriteLine("Home");
            Console.WriteLine("1.Buy now");
            Console.WriteLine("2.Cart");
            Console.WriteLine("3.History");
            Console.WriteLine("4.Setting information");
            Console.WriteLine("5.Become a seller");
            Console.WriteLine("0.Exit");
            Console.WriteLine("Enter your choose:");
            choose_customer=int.Parse(Console.ReadLine());
            Console.Clear();
            switch (choose_customer)
            {
              case 0:
              break;
              case 1:
              int choose_buy=0;
              
                do
                { 
                  Console.WriteLine("--> List product page");
                  Console.WriteLine("1.View Product by category");
                  Console.WriteLine("2.Find product");
                  Console.WriteLine("0.Exit");
                  Console.WriteLine("Enter your choose:");
                  choose_buy=int.Parse(Console.ReadLine());
                  Console.Clear();
                  switch (choose_buy){
                    case 0:
                    break;
                    case 1:
                    Console.Clear();
                    category_Service.list_category.Clear();
                    category_Service.Select();
                    category_Service.getAll();
                     do
                    { 
                    Console.WriteLine("--> View products by category");
                    Console.WriteLine("Enter category:");
                    category=Console.ReadLine();
                     Console.Clear();
                    category_Service.check_empty_duplicate(category);
                   
                    } while (category_Service.check_Valid!=true);
                    product_Service.getByProduct(category);
                    product_Service.Buy();
                   
                    break;
                    case 2:
                    do
                    {Console.Clear();
                      product_Service.Check_Valid = false;
                    Console.WriteLine("--> Find product");
                    Console.WriteLine("Enter product:");
                    product1=Console.ReadLine();
                    product_Service.findProduct(product1);
                    } while (product_Service.Check_Valid =false);
                    product_Service.Buy();
                    break;
                  }
                } while (choose_buy!=0);
              break;
              case 2:
                int choose_cart=0;
                if(product_Service.list_product_queue.Count==0){
                  Console.WriteLine("Cart is empty");
                  Console.ReadKey();
                }
                else{
                do{
                  
                Console.WriteLine("1.Checkout");
                Console.WriteLine("2.Cancel");
                Console.WriteLine("0.Exit");
                Console.WriteLine("Enter your choose:");
                choose_cart=int.Parse(Console.ReadLine());
                customer_Service.Select(login_Service.us);
                if(choose_cart==0){Console.Clear();}
                else if(choose_cart==1){
                  product_Service.cart();
                 // if(order_Detail_Service.id_order>0){
                      order_Detail_Service.list_order_details.Clear();
                      order_Detail_Service.Select();
                //  }
                //  else{
                  // id product price quantity van chua hien thi
                  order_Detail_Service.payment(customer_Service.id,customer_Service.fullname,customer_Service.phone,customer_Service.address,product_Service.id_product,product_Service.product,product_Service.price,product_Service.quantity,product_Service.list_product_queue);
                  if(order_Detail_Service.check_valid==true){
                    choose_cart=0;
                    product_Service.list_product_queue.Clear();
                     Console.ReadKey();
                Console.Clear();
                  }
             // }
                }
                else if(choose_cart==2){
                  product_Service.cart();
                  System.Console.WriteLine("Enter name product:");
                  product1=Console.ReadLine();
                  product_Service.huycart(product1);
                  if(product_Service.list_product_queue.Count==0){
                    System.Console.WriteLine("Cart is empty");
                    choose_cart=0;
                     Console.ReadKey();
                Console.Clear();
                  }
                }
               
              }while(choose_cart!=0);
              }
              break;
              case 3:// lich su hoa don
              int lshd=0;
              do {
               order_Detail_Service.list_order_details.Clear();
              order_Detail_Service.Select();
              order_Service.list_orders.Clear();
              order_Service.select();
              customer_Service.list_customer.Clear();
              customer_Service.Select(login_Service.us);
              System.Console.WriteLine("--> History");
              Console.WriteLine("1.Waiting");
              Console.WriteLine("2.Delivered");
              Console.WriteLine("3.Cancelled");
              Console.WriteLine("0.Exit");
              System.Console.WriteLine("Enter your chooose:");
              lshd=int.Parse(Console.ReadLine());
              if(lshd==0){Console.Clear();}
              else if(lshd==1){
                System.Console.WriteLine("Waiting");
                order_Detail_Service.getbystatus(0,order_Service.list_orders,customer_Service.fullname);}
              else if(lshd==2){
                System.Console.WriteLine("Delivered");
                order_Detail_Service.getbystatus(1,order_Service.list_orders,customer_Service.fullname);}
              else if(lshd==3){
                System.Console.WriteLine("Cancelled");
                order_Detail_Service.getbystatus(2,order_Service.list_orders,customer_Service.fullname);}
              else{
                Console.WriteLine("Invalid selection");
              }
              }while(lshd!=0);
              break;
              case 4:
              int edit_info=0;
              do
              {
              Console.WriteLine("--> Setting information");
              Console.WriteLine("1.View Information");
              Console.WriteLine("2.Edit Information");
              Console.WriteLine("0.Exit");
              Console.WriteLine("Enter your choose:");
              edit_info=int.Parse(Console.ReadLine());
              Console.Clear();
              if(edit_info==1){
                customer_Service.getInformation(login_Service.us);
              }
              else if(edit_info==2){
                int choose_info_edit=0;
                do
                {
                Console.WriteLine("--> Edit your information");
                Console.WriteLine("1.Edit phone");
                Console.WriteLine("2.Edit email");
                Console.WriteLine("3.Edit address");
                Console.WriteLine("0.Exit");
                Console.WriteLine("Enter your choose");
                choose_info_edit=int.Parse(Console.ReadLine());
                Console.Clear();
                if(choose_info_edit==1){
                  customer_Service.editPhone(login_Service.us);
                }
                else if(choose_info_edit==2){
                  customer_Service.editEmail(login_Service.us);
                }
                else if(choose_info_edit==3){
                  customer_Service.editAddress(login_Service.us);
                }
                else if(choose_info_edit==0){}
                else{
                  Console.WriteLine("Invalid selection");
                }
                } while (choose_info_edit!=0);
              }
              else{
                 Console.WriteLine("Invalid selection");
              }
              } while (edit_info!=0);
              break;
              case 5:
              Console.WriteLine("--> Register to become a seller");
              customer_Service.become_seller(login_Service.us);
              Console.ForegroundColor=ConsoleColor.DarkGreen;
              Console.WriteLine("--> Done");
              Console.ForegroundColor=ConsoleColor.White;
              Console.WriteLine("Please login again to use the service");
              Console.ReadKey();
              Console.Clear();
              choose_customer=0;
              break;
              default:
              Console.WriteLine("Invalid selection!");
              break;
            }
            } while (choose_customer!=0);        }
      break;
    default:
    Console.WriteLine("Invalid selection");
      break;
  }

} while (choose != 0);
