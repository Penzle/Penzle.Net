namespace Penzle.Core.Utilities;

internal static partial class Extensions
{
    internal static int ToInt32OrDefault(this object @this)
    {
        try
        {
            return Convert.ToInt32(value: @this);
        }
        catch (Exception)
        {
            return default;
        }
    }

    internal static int ToInt32OrDefault(this object @this, int defaultValue)
    {
        try
        {
            return Convert.ToInt32(value: @this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    internal static int ToInt32OrDefault(this object @this, int defaultValue, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValue;
        }

        try
        {
            return Convert.ToInt32(value: @this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    internal static int ToInt32OrDefault(this object @this, Func<int> defaultValueFactory)
    {
        try
        {
            return Convert.ToInt32(value: @this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    internal static int ToInt32OrDefault(this object @this, Func<int> defaultValueFactory, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValueFactory();
        }

        try
        {
            return Convert.ToInt32(value: @this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }
}