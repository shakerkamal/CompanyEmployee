using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        IUserRepository User { get; }
        Task SaveAsync();
        ValueTask DisposeAsync();
    }
}
