using LoanSimulator.Domain.Entities;
using LoanSimulator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LoanSimulator.Infrastructure.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Loan> _dbSet;

        public LoanRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Loan>();
        }

        public async Task<IEnumerable<Loan>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Loan loan, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(loan, cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        // Pagination implementation:
        public async Task<IReadOnlyList<Loan>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.CountAsync(cancellationToken);
        }
    }
}
