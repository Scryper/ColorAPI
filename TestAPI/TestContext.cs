using Microsoft.EntityFrameworkCore;
using TestAPI.Models;

namespace TestAPI
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> options) : base(options) { }
    
        public DbSet<Color> Colors { get; set; }
    }
}
