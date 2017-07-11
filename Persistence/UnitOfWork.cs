using System.Threading.Tasks;

namespace ShoppingCart.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private ShoppingCartDbContext _context;

        public UnitOfWork(ShoppingCartDbContext context)
        {
            _context = context;
        }

        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }
    }
}