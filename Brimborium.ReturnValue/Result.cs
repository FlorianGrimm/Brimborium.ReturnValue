using System.ComponentModel;

namespace Brimborium.ReturnValue;
public static class Result {
    public static NoValue NoValue => new NoValue();

    public static SuccessValue<T> AsSuccessValue<T>(this T value) => new SuccessValue<T>(value);

    public static ErrorValue AsErrorValue(this Exception error) => new ErrorValue(error);

    public static Result<T> AsResult<T>(this T value)
        => new Result<T>(value);

    public static Result<T> AsResult<T>(this SuccessValue<T> value)
        => new Result<T>(value.Value);

    public static Result<T> AsResult<T>(this OptionalResult<T> value) {
        if (value.TryGetSuccess(out var successValue)) {
            return new Result<T>(successValue);
        } else if (value.TryGetError(out var errorValue)) {
            return new Result<T>(errorValue);
        } else if (value.TryGetNoValue()) {
            return new Result<T>(new UninitializedException());
        } else {
            return new Result<T>(new UninitializedException());
        }
    }
    

    public static Result<T> AsResult<T>(this Exception error)
        => new Result<T>(error);

    public static Result<T> AsResult<T>(this ErrorValue value)
        => new Result<T>(value.Error);


    public static OptionalResult<T> AsOptionalResult<T>(this NoValue value)
        => new OptionalResult<T>();

    public static OptionalResult<T> AsOptionalResult<T>(this T value)
        => new OptionalResult<T>(value);

    public static OptionalResult<T> AsOptionalResult<T>(this SuccessValue<T> value) 
        => new OptionalResult<T>(value.Value);

    public static OptionalResult<T> AsOptionalResult<T>(this Exception value)
        => new OptionalResult<T>(value);

    public static OptionalResult<T> AsOptionalResult<T>(this ErrorValue value)
        => new OptionalResult<T>(value.Error);

    public static OptionalResult<T> AsOptionalResult<T>(this Result<T> value) {
        if (value.TryGetSuccess(out var successValue)) {
            return new OptionalResult<T>(successValue);
        } else if (value.TryGetError(out var errorValue)) {
            return new OptionalResult<T>(errorValue);
        } else {
            return new OptionalResult<T>(new InvalidEnumArgumentException($"Invalid enum {value.Mode}."));
        }
    }
}
