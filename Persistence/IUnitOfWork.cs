using System.Threading.Tasks;

namespace ShoppingCart.Persistence
{
    public interface IUnitOfWork
   {
       Task Complete();
   }
}
