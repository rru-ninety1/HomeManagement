using HomeManagement.Infrastructure.DAL;

namespace HomeManagement.Infrastructure;
public class Bootstraper
{
    private readonly ApplicationDbContext _applicationDbContext;

    public Bootstraper(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public void Execute()
    {
        _applicationDbContext.Migrate();
    }
}
