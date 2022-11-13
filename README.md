# MediatingApis
Quick and simple library with extensions to use 
[ASP.NET Core Minimal APIs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-7.0) 
with [martinothamar/Mediator](https://github.com/martinothamar/Mediator), 
inspired by [this video](https://www.youtube.com/watch?v=euUg_IHo7-s) from Nick Chapsas.

Contains a single marker interface, `IHttpRequest`, to mark suitable request types, and a number of extensions on `IEndpointRouteBuilder`
for common HTTP verbs and use cases. Request types can use the standard minimal api binding attributes on member properties to
customize binding behavior, as the request object is annotated with `[AsParameters]`.

The mediated request handler is expected to return standard results as with normal minimal api code.