
namespace Brimborium.ReturnValue;

public record NoValue {
    public NoValue() { }

    public static NoValue Value => new NoValue();

    //public override bool Equals(object? obj) => (obj is NoValue);

    //public override int GetHashCode() => string.Empty.GetHashCode();

    public override string ToString() => string.Empty;

    public Optional<T> ToOptional<T>() => new Optional<T>();

    public SuccessValue<T> WithValue<T>(T value) => new SuccessValue<T>(value);
}
