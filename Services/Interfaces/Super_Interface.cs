namespace Services.Interfaces;
using Models;

interface ISuper_Interface<Model>
{
  List<Model> GetAll(List<Model> Model);
}