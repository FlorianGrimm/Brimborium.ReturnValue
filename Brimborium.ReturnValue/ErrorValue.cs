#pragma warning disable IDE0031 // Use null propagation
namespace Brimborium.ReturnValue;

internal static class ErrorValueInstance {
    internal static class Singleton {
        internal static ErrorValue _Uninitialized = CreateUninitialized();
    }
    public static ErrorValue GetUninitialized() => Singleton._Uninitialized;

    public static ErrorValue CreateUninitialized() {
        var error = UninitializedException.Instance;
        var exceptionDispatchInfo = ExceptionDispatchInfo.Capture(error);
        return new ErrorValue(error, exceptionDispatchInfo);
    }

}

public record struct ErrorValue(
    Exception Exception,
    ExceptionDispatchInfo? ExceptionDispatchInfo = default,
    bool IsLogged = false) {

    public static ErrorValue Uninitialized => ErrorValueInstance.GetUninitialized();

    public static ErrorValue CreateFromCatchedException(Exception exception) {
        var exceptionDispatchInfo = ExceptionDispatchInfo.Capture(exception);
        return new ErrorValue(exception, exceptionDispatchInfo);
    }

    [DoesNotReturn]
    public void Throw() {
        if (this.ExceptionDispatchInfo is not null) {
            this.ExceptionDispatchInfo.Throw();
        }

        if (this.Exception is not null) {
            throw this.Exception;
        }

        // TODO: better error
        throw new UninitializedException();
    }

    public static implicit operator bool(ErrorValue that) {
        return (that.Exception is not null);
    }
    
    public static implicit operator ErrorValue(Exception error) {
        return new ErrorValue(error, null, false);
    }
}
