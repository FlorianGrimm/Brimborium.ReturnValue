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

    public static implicit operator bool(Optional<T> that) => that.Mode == OptionalMode.Success;
    public static bool operator true(Optional<T> that) => that.Mode == OptionalMode.Success;
    public static bool operator false(Optional<T> that) => that.Mode != OptionalMode.Success;
    public static explicit operator T(Optional<T> that) => (that.Mode == OptionalMode.Success) ? that.Value : throw new InvalidCastException();

}
