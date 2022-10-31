using System.ComponentModel;
using Penzle.Core.Authentication;

namespace Penzle.Core.Http;

/// <summary>
///     The base class for credentials for extending type of authentication.
/// </summary>
public abstract class Credentials
{
    protected Credentials(AuthenticationType authenticationType)
    {
        if (!Enum.IsDefined(enumType: typeof(AuthenticationType), value: authenticationType))
        {
            throw new InvalidEnumArgumentException(argumentName: nameof(authenticationType), invalidValue: (int)authenticationType, enumClass: typeof(AuthenticationType));
        }

        AuthenticationType = authenticationType;
    }

    /// <summary>
    ///     The various authentication methods that are provided by the Penzle API.
    /// </summary>
    public AuthenticationType AuthenticationType { get; }
}
