using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Modules.Users.Domain.Users;

namespace Infrastructure.Repositories;

internal sealed class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public void Add(User user)
    {
        context.Users.Add(user);
    }

    public async Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> ExistEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await context.Users.AnyAsync(x => x.Email == email, cancellationToken);
    }
}
