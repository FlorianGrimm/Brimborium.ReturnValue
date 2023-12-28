namespace Brimborium.ReturnValue;

public enum OptionalMode { NoValue, Success }

public readonly struct Optional<T>
    :IOptionalValue<T>
{
    [JsonInclude]
    public readonly OptionalMode Mode;

    [EditorBrowsable(EditorBrowsableState.Never)]
    [JsonInclude]
    [AllowNull]
    public readonly T Value;

    public Optional()
    {
        this.Mode = OptionalMode.NoValue;
        this.Value = default;
    }

    public Optional(
        T value
    )
    {
        this.Mode = OptionalMode.Success;
        this.Value = value;
    }

    [JsonConstructor]
    public Optional(
        OptionalMode Mode,
        [AllowNull]
        T Value
    )
    {
        if (Mode == OptionalMode.Success) {
            this.Mode = OptionalMode.Success;
            this.Value = Value;
        } else { 
            this.Mode = OptionalMode.NoValue;
            this.Value = Value;
        }
    }

    public void Deconstruct(out OptionalMode mode, [AllowNull] out T value) {
        mode = this.Mode;
        value = this.Value;
    }

    public bool TryGetNoValue()
    {
        return (this.Mode == OptionalMode.NoValue);
    }

    public bool TryGetValue([MaybeNullWhen(false)] out T value)
    {
        if (this.Mode == OptionalMode.NoValue)
        {
            value = default;
            return false;
        } else
        {
            value = this.Value!;
            return true;
        }
    }

    public T GetValueOrDefault(T defaultValue)
        => (this.Mode == OptionalMode.NoValue)
        ? defaultValue
        : this.Value!;


#pragma warning disable IDE0060 // Remove unused parameter
    public static implicit operator Optional<T>(NoValue value) => new Optional<T>();
#pragma warning restore IDE0060 // Remove unused parameter

    public static implicit operator Optional<T>(T value) => new Optional<T>(value);

    public static implicit operator bool(Optional<T> that) => that.Mode == OptionalMode.Success;
    public static bool operator true(Optional<T> that) => that.Mode == OptionalMode.Success;
    public static bool operator false(Optional<T> that) => that.Mode != OptionalMode.Success;
    
    public static explicit operator T(Optional<T> that) => (that.Mode == OptionalMode.Success) ? that.Value : throw new InvalidCastException();

}
