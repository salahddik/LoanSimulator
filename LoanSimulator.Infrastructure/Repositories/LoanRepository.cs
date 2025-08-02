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

        public LoanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Loan>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Loans.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Loan loan, CancellationToken cancellationToken = default)
        {
            await _context.Loans.AddAsync(loan, cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
