using JetBrains.Annotations;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace MediatingApis;

[PublicAPI]
public interface IHttpRequest : IRequest<IResult>
{ }