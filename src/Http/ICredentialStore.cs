using System.Threading.Tasks;

namespace Penzle.Core.Http;

/// <summary>
///     Abstraction that makes it possible for credentials to communicate with one another.
/// </summary>
public interface ICredentialStore<T>
{
    /// <summary>
    ///     Find the credentials in the underlying store and retrieve them.
    /// </summary>
    /// <returns>A continuation that includes credential information</returns>
    Task<T> GetCredentials();
}