using Xunit;
using Microsoft.EntityFrameworkCore;
using LoanSimulator.Infrastructure.Data;
using LoanSimulator.Infrastructure.Repositories;
using LoanSimulator.Domain.Entities;
using System.Threading.Tasks;
using System.Linq;

namespace LoanSimulator.Tests.Infrastructure.Repositories
{
    public class LoanRepositoryTests
    {
        private DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public LoanRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;
        }


        [Fact]
        public async Task AddAsync_AddsLoanToDatabase()
        {
            // Arrange
            await using var context = new ApplicationDbContext(_dbContextOptions);
            var repository = new LoanRepository(context);

            var loan = new Loan
            {
                Amount = 1000m,
                DurationMonths = 12,
                Email = "test@example.com",
                InterestRate = 4.1,
                MonthlyPayment = 85.6m
            };

            // Act
            await repository.AddAsync(loan);
            await repository.SaveChangesAsync();

            // Assert
            var loans = await context.Loans.ToListAsync();
            Assert.Single(loans);
            Assert.Equal(loan.Amount, loans.First().Amount);
            Assert.Equal(loan.Email, loans.First().Email);
        }

        [Fact]
        public async Task CountAsync_ReturnsCorrectCount()
        {
            // Arrange
            await using var context = new ApplicationDbContext(_dbContextOptions);
            var repository = new LoanRepository(context);

            context.Loans.Add(new Loan { Amount = 1000m, DurationMonths = 6, Email = "a@example.com", InterestRate = 4.1, MonthlyPayment = 170 });
            context.Loans.Add(new Loan { Amount = 2000m, DurationMonths = 12, Email = "b@example.com", InterestRate = 4.1, MonthlyPayment = 175 });
            await context.SaveChangesAsync();

            // Act
            var count = await repository.CountAsync();

            // Assert
            Assert.Equal(2, count);
        }

        [Fact]
        public async Task GetPagedAsync_ReturnsCorrectPage()
        {
            // Arrange
            await using var context = new ApplicationDbContext(_dbContextOptions);
            var repository = new LoanRepository(context);

            context.Loans.RemoveRange(context.Loans); // clear db
            await context.SaveChangesAsync();

            for (int i = 1; i <= 20; i++)
            {
                context.Loans.Add(new Loan
                {
                    Amount = 1000m + i,
                    DurationMonths = 12,
                    Email = $"user{i}@example.com",
                    InterestRate = 4.1,
                    MonthlyPayment = 85.6m
                });
            }
            await context.SaveChangesAsync();

            int pageNumber = 2;
            int pageSize = 5;

            // Act
            var page = await repository.GetPagedAsync(pageNumber, pageSize);

            // Assert
            Assert.Equal(pageSize, page.Count);
            Assert.Equal("user6@example.com", page.First().Email);
        }
    }
}
