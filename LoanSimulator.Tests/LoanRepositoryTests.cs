using LoanSimulator.Domain.Entities;
using LoanSimulator.Infrastructure.Data;
using LoanSimulator.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LoanSimulator.Tests
{
    public class LoanRepositoryTests
    {
        /// <summary>
        /// Creates a brand-new ApplicationDbContext backed by a uniquely named
        /// in-memory database, so no data carries over between tests.
        /// </summary>
        private ApplicationDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task AddAsync_ShouldAddLoan()
        {
            // Arrange: fresh context + repo
            using var context = CreateInMemoryDbContext();
            var repository = new LoanRepository(context);

            var loan = new Loan(1000m, 12, "test@example.com");

            // Act
            await repository.AddAsync(loan);
            await repository.SaveChangesAsync();

            // Assert
            var allLoans = await repository.GetAllAsync();
            Assert.Single(allLoans);
            Assert.Equal(1000m, allLoans.First().Amount);
        }

        [Fact]
        public async Task GetPagedAsync_ShouldReturnPagedResults()
        {
            // Arrange
            using var context = CreateInMemoryDbContext();
            var repository = new LoanRepository(context);

            for (int i = 0; i < 20; i++)
                await repository.AddAsync(new Loan(1000 + i, 12, $"user{i}@example.com"));
            await repository.SaveChangesAsync();

            // Act
            var page1 = await repository.GetPagedAsync(1, 10);
            var page2 = await repository.GetPagedAsync(2, 10);

            // Assert
            Assert.Equal(10, page1.Count);
            Assert.Equal(10, page2.Count);
            Assert.NotEqual(page1.First().Amount, page2.First().Amount);
        }

        [Fact]
        public async Task CountAsync_ShouldReturnCorrectCount()
        {
            // Arrange
            using var context = CreateInMemoryDbContext();
            var repository = new LoanRepository(context);

            var before = await repository.CountAsync();

            await repository.AddAsync(new Loan(2000m, 24, "counttest@example.com"));
            await repository.SaveChangesAsync();

            // Act
            var after = await repository.CountAsync();

            // Assert
            Assert.Equal(before + 1, after);
        }
    }
}
