using System.Diagnostics;
using System.Reflection;
using AssignmentManager.Server.Persistence.Contexts;

namespace AssignmentManager.Server.Persistence
{
    public abstract class BaseService
    {
        protected readonly AppDbContext _context;

        public string GetErrorString(MethodBase method, string message )
        {
            return $"An error occurred while {method.ReflectedType?.Name} in {GetType().Name}: {message}";
        }
        public BaseService(AppDbContext context)
        {
            _context = context;
        }
    }
}