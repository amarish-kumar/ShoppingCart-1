using System.Threading.Tasks;
using ShoppingCart.Core;

namespace ShoppingCart.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private ShoppingCartDbContext _context;

        public UnitOfWork(ShoppingCartDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}