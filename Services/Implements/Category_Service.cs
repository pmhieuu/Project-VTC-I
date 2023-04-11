using MySql.Data;
using MySql.Data.MySqlClient;
using Models;
using Services.Interfaces;

public class Category_Service : ISuper_Interface<Category>
{
  List<Category> list_category = new List<Category>();
  public MySqlConnection connection = new MySqlConnection
  {
    ConnectionString = @"server=localhost;userid=root;password=ttth17562;port=3306;database=Project_1_VTCA;"
  };
  public void Select()
  {

    MySqlCommand command = new MySqlCommand("SELECT * FROM Category;", connection);// thay doi user = tablekhac
    using (MySqlDataReader reader = command.ExecuteReader())
    {
      while (reader.Read())
      {
        int id = Convert.ToInt32($"{reader["ID_Category"]}");
        string name = $"{reader["Category"]}";
        list_category.Add(new Category() { ID = id, Name = name });
      }
      reader.Close();
    }
  }
  public List<Category> GetAll(List<Category> list_category)
  {
    Console.WriteLine(list_category[0].ID + "       " + list_category[0].Name);
    return list_category[0].ID;
  }

}