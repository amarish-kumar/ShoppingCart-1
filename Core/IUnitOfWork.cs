using System.Threading.Tasks;

namespace ShoppingCart.Core
{
    public interface IUnitOfWork
   {
       Task CompleteAsync();
   }
}
