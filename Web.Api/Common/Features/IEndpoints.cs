namespace Web.Api.Common.Features;

public interface IEndpoints
{
    static abstract void MapEndpoints(IEndpointRouteBuilder endpoints);
}