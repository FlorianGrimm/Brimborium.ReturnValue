using System.Diagnostics.CodeAnalysis;

namespace Brimborium.ReturnValue;

public enum OptionalMode { NoValue, Success }

public struct Optional<T>
{
    public readonly OptionalMode Mode;
    public readonly T? Value;
    public Optional()
    {
        this.Mode = OptionalMode.NoValue;
        this.Value = default;
    }

    public Optional(
        T? Value
    )
    {
        this.Mode = OptionalMode.Success;
        this.Value = Value;
    }

    public Optional(
        OptionalMode Mode,
        T? Value
    )
    {
        this.Mode = (Mode == OptionalMode.Success) ? OptionalMode.Success : OptionalMode.NoValue;
        this.Value = Value;
    }

    public bool TryGetNoValue()
    {
        return (this.Mode == OptionalMode.NoValue);
    }
    
    public bool TryGetSuccess([MaybeNullWhen(false)] out T value)
    {
        if (this.Mode == OptionalMode.Success)
        {
            value = this.Value!;
            return true;
        }
        else
        {
            value = default;
            return false;
        }
    }
}

public static class Optional
{

}