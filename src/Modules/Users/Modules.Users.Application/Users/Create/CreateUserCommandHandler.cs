using Application.Data;
using Application.Messaging;
using Domain.Results;
using MediatR;
using Modules.Users.Application.Users.Create;
using Modules.Users.Domain.Users;

namespace Modules.Users.Application.Users.Register;

/// <summary>
/// Represents the <see cref="CreateUserCommand"/> handler.
/// </summary>
/// <param name="repository">The user repository.</param>
/// <param name="publisher">The domain event publisher.</param>
/// <param name="unitOfWork">The unit of work.</param>
internal sealed class CreateUserCommandHandler(
    IUserRepository repository,
    IPublisher publisher,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(Guid.NewGuid(), request.Email, request.FirstName, request.LastName);

        if (await repository.ExistEmailAsync(user.Email, cancellationToken))
        {
            return Result.Failure<Guid>(UserErrors.EmailAlreadyExists);
        }

        repository.Add(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var events = user.PullDomainEvents();

        foreach (var domainEvent in events)
        {
            await publisher.Publish(domainEvent, cancellationToken);
        }

        return user.Id;
    }
}
