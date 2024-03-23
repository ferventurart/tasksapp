using MediatR;
using Web.Api.Common.Features;
using Web.Api.Extensions;
using Web.Api.Features.Todos.Commands;
using Web.Api.Features.Todos.Models;
using Web.Api.Features.Todos.Queries;
using Web.Api.Host;

namespace Web.Api.Features.Todos.Endpoints;

public class TodoEndpoints : IEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("api/todos")
            .WithTags(nameof(Todo));

        group.MapPost("",
                async (CreateTodoCommand command, ISender sender, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);
                    return result.Match(Results.Created, CustomResults.Problem);
                })
            .Produces<Todo>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithTags(nameof(Todo));

        group.MapPut("/{id:guid}",
                async (Guid id, UpdateTodoCommand command, ISender sender, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command with { Id = id }, cancellationToken);
                    return result.Match(Results.NoContent, CustomResults.Problem);
                })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithTags(nameof(Todo));

        group.MapDelete("/{id:guid}",
                async (Guid id, ISender sender, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(new DeleteTodoCommand(id), cancellationToken);
                    return result.Match(Results.NoContent, CustomResults.Problem);
                })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithTags(nameof(Todo));

        group.MapGet("/{id:guid}",
                async (Guid id, ISender sender, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(new GetTodoByIdQuery(id), cancellationToken);
                    return result.Match(Results.Ok, CustomResults.Problem);
                })
            .Produces<TodoResponse>()
            .Produces(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithTags(nameof(Todo));
        
        group.MapGet("",
                async (ISender sender, CancellationToken cancellationToken, string? status = "all", string? category = "all") =>
                {
                    var result = await sender.Send(new GetAllTodosQuery(status, category), cancellationToken);
                    return result.Match(Results.Ok, CustomResults.Problem);
                })
            .Produces<IReadOnlyList<TodoResponse>>()
            .Produces(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithTags(nameof(Todo));
    }
}