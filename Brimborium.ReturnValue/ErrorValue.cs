namespace Brimborium.ReturnValue;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public record struct ErrorValue(
    Exception Exception,
    ExceptionDispatchInfo? ExceptionDispatchInfo = default,
    bool IsLogged = false) {


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

    private string GetDebuggerDisplay() {
        if (this.Exception is not null) {
            return $"{this.Exception.GetType().Name} {this.Exception.Message}";
        }
        return this.ToString();
    }

    public readonly ErrorValue WithIsLogged(bool isLogged = true)
        => new ErrorValue(this.Exception, this.ExceptionDispatchInfo, isLogged);

    public static ErrorValue Uninitialized => ErrorValueInstance.GetUninitialized();

    public static ErrorValue CreateFromCatchedException(Exception exception) {
        var exceptionDispatchInfo = ExceptionDispatchInfo.Capture(exception);
        return new ErrorValue(exception, exceptionDispatchInfo);
    }
    
    public static Exception GetAndSetIsLogged(ref ErrorValue that) {
        that = that.WithIsLogged();
        return that.Exception;
    }

    public static implicit operator ErrorValue(Exception error) {
        return new ErrorValue(error, null, false);
    }
}

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
