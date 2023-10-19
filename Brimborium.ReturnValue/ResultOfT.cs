namespace Brimborium.ReturnValue;

public enum ResultMode { Success, Error }

public class Result<T> {
    public readonly ResultMode Mode;
    [AllowNull] public readonly T Value;
    [AllowNull] public readonly Exception Error;

    public Result() {
        Mode = ResultMode.Error;
        Value = default(T);
        Error = UninitializedException.Instance;
    }

    public Result(T Value) {
        this.Mode = ResultMode.Success;
        this.Value = Value;
        this.Error = default;
    }

    public Result(Exception error) {
        this.Mode = ResultMode.Error;
        this.Value = default;
        this.Error = error;
    }

    public void Deconstruct(out ResultMode mode, out T? value, out Exception? error) {
        mode = this.Mode;

        if (this.Mode == ResultMode.Success) {
            value = this.Value;
            error = default;
        } else {
            value = default;
            error = this.Error;
        }
    }

    public bool TryGet(
        [MaybeNullWhen(false)] out T value,
        [MaybeNullWhen(true)] out Exception error) {
        if (this.Mode == ResultMode.Success) {
            value = this.Value!;
            error = default;
            return true;
        } else {
            value = default;
            error = this.Error!;
            return false;
        }
    }

    public bool TryGetSuccess([MaybeNullWhen(false)] out T value) {
        if (this.Mode == ResultMode.Success) {
            value = this.Value!;
            return true;
        } else {
            value = default;
            return false;
        }
    }

    public bool TryGetError([MaybeNullWhen(false)] out Exception error) {
        if (this.Mode == ResultMode.Error) {
            error = this.Error!;
            return true;
        } else {
            error = default;
            return false;
        }
    }

    public Result<T> WithValue(T value) => new Result<T>(value);

    public Result<T> WithError(Exception error) => new Result<T>(error);

    public static implicit operator Result<T>(T value) => new Result<T>(value);
    public static implicit operator Result<T>(Exception error) => new Result<T>(error);

    public static implicit operator Result<T>(SuccessValue<T> successValue) => new Result<T>(successValue);
    public static implicit operator Result<T>(ErrorValue errorValue) => new Result<T>(errorValue.Error);
}
