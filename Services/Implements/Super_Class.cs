namespace Services.Implements;
using Models;

abstract public class Super_Class<Model>
{
  abstract public void Add(List<Model> Models);
  abstract public void Update(List<Model> Models);
  abstract public void Remove(List<Model> Models);
}
