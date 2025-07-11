namespace Modules.Users.Domain.Users;

/// <summary>
/// Represents the user repository interface.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Adds the specified user to the repository.
    /// </summary>
    /// <param name="user">The user.</param>
    void Add(User user);

    /// <summary>
    /// Gets the user with the specified identifier, if it exists.
    /// </summary>
    /// <param name="id">The user identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The user with the specified identifier if it exists, otherwise null.</returns>
    Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if the specified email exist.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The success 'true' if the email exist, otherwise 'false'.</returns>
    Task<bool> ExistEmailAsync(string email, CancellationToken cancellationToken = default);
}
