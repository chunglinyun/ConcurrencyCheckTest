using ConcurrencyCheckTest.Entity;
using Microsoft.EntityFrameworkCore;

namespace TestConcurrencyCheck
{
    public class UnitTest1
    {
        [Fact]
        public void ConcurrentUpdates_ShouldThrowDbUpdateConcurrencyException()
        {
            // Arrange

            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new MyDbContext(options))
            {
                var entity = new Wager { Id = 1, Status = 0, UpdateDate = default };
                context.Wagers.Add(entity);
                context.SaveChanges();
            }

            using (var context1 = new MyDbContext(options))
            using (var context2 = new MyDbContext(options))
            {
                // Act
                var entity1 = context1.Wagers.First();
                var entity2 = context2.Wagers.First();

                entity1.UpdateDate = DateTimeOffset.Now;
                entity2.UpdateDate = DateTimeOffset.Now.AddDays(1);

                context1.SaveChanges();

                // Assert
                Xunit.Assert.Throws<DbUpdateConcurrencyException>(() => context2.SaveChanges());
            }
        }
    }
}