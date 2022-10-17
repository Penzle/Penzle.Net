using System;
using System.ComponentModel;
using Penzle.Core.Authentication;

namespace Penzle.Core.Http;

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

    public AuthenticationType AuthenticationType { get; }
}