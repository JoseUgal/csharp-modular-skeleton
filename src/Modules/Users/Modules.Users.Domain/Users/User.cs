using Domain.Primitives;

namespace Modules.Users.Domain.Users;

public sealed class User : Entity, IAuditable
{
    /// <summary>
    /// Creates a new user with the specified parameters.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="email">The email.</param>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    /// <returns>The new user instance.</returns>
    public static User Create(Guid id, string email, string firstName, string lastName)
    {
        var user = new User(id, email, firstName, lastName);

        user.Record(new UserCreatedDomainEvent(Guid.NewGuid(), user.Id, DateTime.UtcNow));

        return user;
    }

    /// <summary>
    /// Gets the email.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets the first name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets the last name.
    /// </summary>
    public string LastName { get; set; }

    /// <inheritdoc />
    public DateTime CreatedOnUtc { get; private set; }

    /// <inheritdoc />
    public DateTime? ModifiedOnUtc { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="email">The email.</param>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    private User(Guid id, string email, string firstName, string lastName) : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }
}
