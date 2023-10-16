using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Brimborium.ReturnValue;

public enum OptionalMode { NoValue, Success }

public readonly struct Optional<T>
{
    public readonly OptionalMode Mode;
    [AllowNull] private readonly T _Value;

    public Optional()
    {
        this.Mode = OptionalMode.NoValue;
        this._Value = default;
    }

    public Optional(
        T value
    )
    {
        this.Mode = OptionalMode.Success;
        this._Value = value;
    }

    public Optional(
        OptionalMode Mode,
        T? Value
    )
    {
        this.Mode = (Mode == OptionalMode.Success) ? OptionalMode.Success : OptionalMode.NoValue;
        this._Value = Value;
    }


    public readonly T Value
    {
        get
        {
            if (this.Mode == OptionalMode.NoValue)
            {
                throw new NoValueAccessingException();
            } else
            {
                return this._Value!;
            }
        }
    }

    public bool TryGetNoValue()
    {
        return (this.Mode == OptionalMode.NoValue);
    }

    public bool TryGetSuccess([MaybeNullWhen(false)] out T value)
    {
        if (this.Mode == OptionalMode.NoValue)
        {
            value = default;
            return false;
        } else
        {
            value = this._Value!;
            return true;
        }
    }

    public T GetValueOrDefault(T defaultValue)
        => (this.Mode == OptionalMode.NoValue)
        ? defaultValue
        : this._Value!;

#pragma warning disable IDE0060 // Remove unused parameter
    public static implicit operator Optional<T>(NoValue value) => new Optional<T>();
#pragma warning restore IDE0060 // Remove unused parameter

    public static implicit operator Optional<T>(T value) => new Optional<T>(value);

}

public static class Optional {
    public static Optional<T> ToOptional<T>(this T value) {
        return new Optional<T>(value);
    }
}