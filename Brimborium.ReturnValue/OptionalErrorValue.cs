#pragma warning disable IDE0031 // Use null propagation
namespace Brimborium.ReturnValue;

internal static class OptionalErrorValueInstance {
    internal static class Singleton {
        internal static OptionalErrorValue _Uninitialized = CreateUninitialized();
    }
    public static OptionalErrorValue GetUninitialized() => Singleton._Uninitialized;

    public static OptionalErrorValue CreateUninitialized() {
        var error = UninitializedException.Instance;
        var exceptionDispatchInfo = ExceptionDispatchInfo.Capture(error);
        return new OptionalErrorValue(error, exceptionDispatchInfo);
    }

}

public record struct OptionalErrorValue(
    Exception? Exception = default,
    ExceptionDispatchInfo? ExceptionDispatchInfo = default,
    bool IsLogged = false) {

    public static OptionalErrorValue Uninitialized => OptionalErrorValueInstance.GetUninitialized();

    public static OptionalErrorValue CreateFromCatchedException(Exception exception) {
        var exceptionDispatchInfo = ExceptionDispatchInfo.Capture(exception);
        return new OptionalErrorValue(exception, exceptionDispatchInfo, false);
    }

    // [DoesNotReturnIf]
    public readonly void Throw() {
        if (this.ExceptionDispatchInfo is not null) {
            this.ExceptionDispatchInfo.Throw();
        }

        if (this.Exception is not null) {
            throw this.Exception;
        }
    }

    public static implicit operator bool(OptionalErrorValue that)
        => (that.Exception is not null);
    public static implicit operator OptionalErrorValue(Exception exception)
        => new OptionalErrorValue(exception, null, false);
    public static implicit operator OptionalErrorValue(ErrorValue error)
        => new OptionalErrorValue(error.Exception, error.ExceptionDispatchInfo, error.IsLogged);
}
