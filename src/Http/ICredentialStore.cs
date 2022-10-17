using System.Threading.Tasks;

namespace Penzle.Core.Http;

/// <summary>
///     Abstraction that allows for the interaction of credentials
/// </summary>
public interface ICredentialStore<T>
{
    /// <summary>
    ///     Find the credentials in the underlying store and retrieve them.
    /// </summary>
    /// <returns>A continuation that includes credential information</returns>
    Task<T> GetCredentials();
}