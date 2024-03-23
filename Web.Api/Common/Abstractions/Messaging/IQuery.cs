using MediatR;
using Web.Api.Common.Models;

namespace Web.Api.Common.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
