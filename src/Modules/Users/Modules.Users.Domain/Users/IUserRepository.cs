namespace Modules.Users.Domain.Users;

public interface IUserRepository
{
    void Add(User user);

    Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> ExistEmailAsync(string email, CancellationToken cancellationToken = default);
}
