using System.Threading.Tasks;

namespace AssignmentManager.Server.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}