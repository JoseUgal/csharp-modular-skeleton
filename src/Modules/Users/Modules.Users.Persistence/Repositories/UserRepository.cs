using Application.ServiceLifetimes;
using Microsoft.EntityFrameworkCore;
using Modules.Users.Domain.Users;

namespace Modules.Users.Persistence.Repositories;

/// <summary>
/// Represents the user repository.
/// </summary>
internal sealed class UserRepository(UsersDbContext context) : IUserRepository, IScoped
{
    /// <inheritdoc />
    public void Add(User user)
    {
        context.Set<User>().Add(user);
    }

    /// <inheritdoc />
    public async Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Set<User>().SingleOrDefaultAsync(user =>
            user.Id == id,
            cancellationToken
        );
    }

    /// <inheritdoc />
    public async Task<bool> ExistEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await context.Set<User>().AnyAsync(user =>
            user.Email == email,
            cancellationToken
        );
    }
}
