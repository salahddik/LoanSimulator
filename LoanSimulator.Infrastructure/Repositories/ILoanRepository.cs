using LoanSimulator.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LoanSimulator.Infrastructure.Repositories
{
    public interface ILoanRepository
    {
        Task<IEnumerable<Loan>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Loan loan, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Loan>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task<int> CountAsync(CancellationToken cancellationToken = default);
    }
}
