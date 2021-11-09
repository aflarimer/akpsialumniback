using System.Collections.Generic;
using api.Model;
namespace api.Interfaces
{
    public interface IPersonDataHandler
    {
         public List<Person> Select();
         public void Delete(Person person);
         public void Insert(Person person);
         public void Update(Person person);
    }
}