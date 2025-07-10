using Domain.Results;
using MediatR;

namespace Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
