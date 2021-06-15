using AssignmentManager.Server.Persistence.Contexts;

namespace AssignmentManager.Server.Persistence
{
    public abstract class BaseService
    {
        protected readonly AppDbContext _context;

        public BaseService(AppDbContext context)
        {
            _context = context;
        }
    }
}