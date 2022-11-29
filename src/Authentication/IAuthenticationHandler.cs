namespace Penzle.Core.Authentication;

internal interface IAuthenticationHandler
{
    void Authenticate(IRequest request, Credentials credentials);
}
