/// Taken from <a href="https://github.com/JasonGT/NorthwindTraders/blob/master/Src/Persistence/NorthwindDbContextFactory.cs">
/// Jason Taylor's impl</a>
using Microsoft.EntityFrameworkCore;

namespace SolarTally.Persistence
{
    public class SolarTallyDbContextFactory :
        DesignTimeDbContextFactoryBase<SolarTallyDbContext>
    {
        protected override SolarTallyDbContext CreateNewInstance(DbContextOptions<SolarTallyDbContext> options)
        {
            return new SolarTallyDbContext(options);
        }
    }
}