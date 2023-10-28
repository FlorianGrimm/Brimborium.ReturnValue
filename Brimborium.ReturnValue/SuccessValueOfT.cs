namespace Brimborium.ReturnValue;

public record struct SuccessValue<T>(T Value) {
    public readonly SuccessValue<T> WithValue(T Value) => new SuccessValue<T>(Value);

    public readonly Result<T> WithError(Exception that) => new Result<T>(new ErrorValue(that));

    public static implicit operator SuccessValue<T>(T that) => new SuccessValue<T>(that);

    public static implicit operator T(SuccessValue<T> that) => that.Value;
}
