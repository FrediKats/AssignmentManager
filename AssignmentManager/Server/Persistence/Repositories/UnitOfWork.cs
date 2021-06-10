using System.Threading.Tasks;
using AssignmentManager.Server.Persistence.Contexts;
using AssignmentManager.Server.Repositories;

namespace AssignmentManager.Server.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}