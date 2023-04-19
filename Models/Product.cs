namespace Models;

public class Product
{
  public int ID_Product { get; set; }
  public int ID_Staff { get; set; }
  public int ID_Category { get; set; }
  public string Name { get; set; }
  public int Quantity { get; set; }
  public double Price { get; set; }
  public int Status { get; set; }

}