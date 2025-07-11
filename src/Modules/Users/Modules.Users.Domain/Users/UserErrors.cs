using Domain.Errors;

namespace Modules.Users.Domain.Users;

public static class UserErrors
{
    public static readonly Error EmailAlreadyExists = Error.Conflict(
        "Users.EmailAlreadyExists",
        "The provided email already exists"
    );
}
