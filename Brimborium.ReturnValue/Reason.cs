namespace Brimborium.ReturnValue;

public record struct Reason(
    string Explanation) {
    public OptionalReason<T> WithNoValue<T>()
        => new OptionalReason<T>(this, OptionalMode.NoValue, default(T));
}

public record struct ReasonCondition(
    string ExplanationTrue,
    string ExplanationFalse
    ) {
    //public OptionalReason<T> WithNoValue<T>()
    //    => new OptionalReason<T>(this, OptionalMode.NoValue, default(T));
}

//public record struct ReasonValue<T>(Reason Reason, T Value):IMeaning<T>;
//public struct ReasonValue<T> : IMeaning<T> {
//    public Reason Reason { get; set; }
//    public T Value { get; set }
//}

public readonly struct OptionalReason<T> {
    [JsonInclude]
    public readonly OptionalMode Mode;

    [EditorBrowsable(EditorBrowsableState.Never)]
    [JsonInclude]
    [AllowNull]
    public readonly T Value;

    public readonly Reason Reason;

    public OptionalReason(Reason reason) {
        this.Mode = OptionalMode.NoValue;
        this.Value = default;
        this.Reason = reason;
    }

    public OptionalReason(
        Reason reason,
        T value
    ) {
        this.Mode = OptionalMode.Success;
        this.Value = value;
        this.Reason = reason;
    }

    [JsonConstructor]
    public OptionalReason(
        Reason reason,
        OptionalMode Mode,
        [AllowNull]
        T Value
    ) {
        if (Mode == OptionalMode.Success) {
            this.Mode = OptionalMode.Success;
            this.Value = Value;
            this.Reason = reason;
        } else {
            this.Mode = OptionalMode.NoValue;
            this.Value = Value;
            this.Reason = reason;
        }
    }
            
    public void Deconstruct(out OptionalMode mode, [AllowNull] out T value, out Reason reason) {
        mode = this.Mode;
        value = this.Value;
        reason = this.Reason;
    }

    public OptionalReason<T> WithValue(T value)
        => new OptionalReason<T>(this.Reason, value);

    public bool TryGetNoValue() {
        return (this.Mode == OptionalMode.NoValue);
    }

    public bool TryGetSuccess([MaybeNullWhen(false)] out T value) {
        if (this.Mode == OptionalMode.NoValue) {
            value = default;
            return false;
        } else {
            value = this.Value!;
            return true;
        }
    }

    public T GetValueOrDefault(T defaultValue)
        => (this.Mode == OptionalMode.NoValue)
        ? defaultValue
        : this.Value!;

#pragma warning disable IDE0060 // Remove unused parameter
    public static implicit operator OptionalReason<T>(Reason reason) => new OptionalReason<T>(reason, OptionalMode.NoValue, default(T));
#pragma warning restore IDE0060 // Remove unused parameter

    public static OptionalReason<T> operator +(Reason reason, NoValue value)
        => new OptionalReason<T>(reason, OptionalMode.NoValue, default(T));

    public static OptionalReason<T> operator +(Reason reason, T value)
        => new OptionalReason<T>(reason, OptionalMode.Success, value);

    public static implicit operator bool(OptionalReason<T> that) => that.Mode == OptionalMode.Success;
    public static bool operator true(OptionalReason<T> that) => that.Mode == OptionalMode.Success;
    public static bool operator false(OptionalReason<T> that) => that.Mode != OptionalMode.Success;

    public static explicit operator T(OptionalReason<T> that) => (that.Mode == OptionalMode.Success) ? that.Value : throw new InvalidCastException();

}
