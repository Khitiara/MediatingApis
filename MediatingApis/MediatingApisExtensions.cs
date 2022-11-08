using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace MediatingApis;

/// <summary>
/// Provides extension methods for <see cref="IEndpointRouteBuilder"/> to add endpoints.
/// </summary>
[PublicAPI]
public static class MediatingApisExtensions
{
    // Avoid creating a new array every call
    private static readonly string[] GetVerb    = { HttpMethods.Get, };
    private static readonly string[] PostVerb   = { HttpMethods.Post, };
    private static readonly string[] PutVerb    = { HttpMethods.Put, };
    private static readonly string[] DeleteVerb = { HttpMethods.Delete, };
    private static readonly string[] PatchVerb  = { HttpMethods.Patch, };

    /// <summary>
    /// Adds a <see cref="RouteEndpoint"/> to the <see cref="IEndpointRouteBuilder"/> that matches HTTP requests
    /// for the specified pattern, with a handler which wraps request parameters in an instance of
    /// <typeparamref name="TRequest"/> and delegates request handling details to an <see cref="IMediator"/>
    /// retrieved from dependency injection.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to add the route to.</param>
    /// <param name="pattern">The route pattern.</param>
    /// <typeparam name="TRequest"> The type of the request object, to be retrieved from the request by <see cref="AsParametersAttribute"/></typeparam>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    public static RouteHandlerBuilder Mediate<TRequest>(this IEndpointRouteBuilder endpoints,
        [StringSyntax("Route"), RouteTemplate,] 
        string pattern)
        where TRequest : IHttpRequest => endpoints.Map(pattern,
        async ([FromServices] IMediator mediator, [AsParameters] TRequest request,
            CancellationToken cancellationToken) => await mediator.Send(request, cancellationToken));

    /// <summary>
    /// Adds a <see cref="RouteEndpoint"/> to the <see cref="IEndpointRouteBuilder"/> that matches HTTP requests
    /// for the specified HTTP methods and pattern, with a handler which wraps request parameters in an instance of
    /// <typeparamref name="TRequest"/> and delegates request handling details to an <see cref="IMediator"/>
    /// retrieved from dependency injection.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to add the route to.</param>
    /// <param name="pattern">The route pattern.</param>
    /// <param name="httpMethods">HTTP methods that the endpoint will match.</param>
    /// <typeparam name="TRequest"> The type of the request object, to be retrieved from the request by <see cref="AsParametersAttribute"/></typeparam>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    public static RouteHandlerBuilder MediateMethods<TRequest>(this IEndpointRouteBuilder endpoints,
        [StringSyntax("Route"), RouteTemplate,] 
        string pattern, IEnumerable<string> httpMethods)
        where TRequest : IHttpRequest => endpoints.MapMethods(pattern, httpMethods,
        async ([FromServices] IMediator mediator, [AsParameters] TRequest request,
            CancellationToken cancellationToken) => await mediator.Send(request, cancellationToken));

    /// <summary>
    /// Adds a <see cref="RouteEndpoint"/> to the <see cref="IEndpointRouteBuilder"/> that matches HTTP GET requests
    /// for the specified pattern, with a handler which wraps request parameters in an instance of
    /// <typeparamref name="TRequest"/> and delegates request handling details to an <see cref="IMediator"/>
    /// retrieved from dependency injection.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to add the route to.</param>
    /// <param name="pattern">The route pattern.</param>
    /// <typeparam name="TRequest"> The type of the request object, to be retrieved from the request by <see cref="AsParametersAttribute"/></typeparam>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    public static RouteHandlerBuilder MediateGet<TRequest>(this IEndpointRouteBuilder endpoints,
        [StringSyntax("Route"), RouteTemplate,] 
        string pattern)
        where TRequest : IHttpRequest => MediateMethods<TRequest>(endpoints, pattern, GetVerb);

    /// <summary>
    /// Adds a <see cref="RouteEndpoint"/> to the <see cref="IEndpointRouteBuilder"/> that matches HTTP POST requests
    /// for the specified pattern, with a handler which wraps request parameters in an instance of
    /// <typeparamref name="TRequest"/> and delegates request handling details to an <see cref="IMediator"/>
    /// retrieved from dependency injection.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to add the route to.</param>
    /// <param name="pattern">The route pattern.</param>
    /// <typeparam name="TRequest"> The type of the request object, to be retrieved from the request by <see cref="AsParametersAttribute"/></typeparam>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    public static RouteHandlerBuilder MediatePost<TRequest>(this IEndpointRouteBuilder endpoints,
        [StringSyntax("Route"), RouteTemplate,] 
        string pattern)
        where TRequest : IHttpRequest => MediateMethods<TRequest>(endpoints, pattern, PostVerb);

    /// <summary>
    /// Adds a <see cref="RouteEndpoint"/> to the <see cref="IEndpointRouteBuilder"/> that matches HTTP PUT requests
    /// for the specified pattern, with a handler which wraps request parameters in an instance of
    /// <typeparamref name="TRequest"/> and delegates request handling details to an <see cref="IMediator"/>
    /// retrieved from dependency injection.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to add the route to.</param>
    /// <param name="pattern">The route pattern.</param>
    /// <typeparam name="TRequest"> The type of the request object, to be retrieved from the request by <see cref="AsParametersAttribute"/></typeparam>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    public static RouteHandlerBuilder MediatePut<TRequest>(this IEndpointRouteBuilder endpoints,
        [StringSyntax("Route"), RouteTemplate,] 
        string pattern)
        where TRequest : IHttpRequest => MediateMethods<TRequest>(endpoints, pattern, PutVerb);

    /// <summary>
    /// Adds a <see cref="RouteEndpoint"/> to the <see cref="IEndpointRouteBuilder"/> that matches HTTP DELETE requests
    /// for the specified pattern, with a handler which wraps request parameters in an instance of
    /// <typeparamref name="TRequest"/> and delegates request handling details to an <see cref="IMediator"/>
    /// retrieved from dependency injection.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to add the route to.</param>
    /// <param name="pattern">The route pattern.</param>
    /// <typeparam name="TRequest"> The type of the request object, to be retrieved from the request by <see cref="AsParametersAttribute"/></typeparam>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    public static RouteHandlerBuilder MediateDelete<TRequest>(this IEndpointRouteBuilder endpoints,
        [StringSyntax("Route"), RouteTemplate,] 
        string pattern)
        where TRequest : IHttpRequest => MediateMethods<TRequest>(endpoints, pattern, DeleteVerb);

    /// <summary>
    /// Adds a <see cref="RouteEndpoint"/> to the <see cref="IEndpointRouteBuilder"/> that matches HTTP PATCH requests
    /// for the specified pattern, with a handler which wraps request parameters in an instance of
    /// <typeparamref name="TRequest"/> and delegates request handling details to an <see cref="IMediator"/>
    /// retrieved from dependency injection.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to add the route to.</param>
    /// <param name="pattern">The route pattern.</param>
    /// <typeparam name="TRequest"> The type of the request object, to be retrieved from the request by <see cref="AsParametersAttribute"/></typeparam>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    public static RouteHandlerBuilder MediatePatch<TRequest>(this IEndpointRouteBuilder endpoints,
        [StringSyntax("Route"), RouteTemplate,] 
        string pattern)
        where TRequest : IHttpRequest => MediateMethods<TRequest>(endpoints, pattern, PatchVerb);
}