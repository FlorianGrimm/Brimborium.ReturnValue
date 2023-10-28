namespace Brimborium.ReturnValue;

public record struct SuccessValue<T>(T Value) {
    public readonly SuccessValue<T> WithValue(T Value) => new SuccessValue<T>(Value);

    public readonly Result<T> WithError(Exception error) => new Result<T>(error);

    public static implicit operator SuccessValue<T>(T Value) => new SuccessValue<T>(Value);

    public static implicit operator T(SuccessValue<T> that) => that.Value;
}
