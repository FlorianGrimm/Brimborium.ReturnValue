namespace Brimborium.ReturnValue;

/// <summary>
/// Extensions for <see cref="OptionalResult{T}"/>
/// </summary>
public static class OptionalResult {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="that"></param>
    /// <returns></returns>
    public static OptionalResult<T> ToOptionalResult<T>(this T that)
        => new OptionalResult<T>(that);

    public static OptionalResult<T> If<T>(this OptionalResult<T> value, Func<T, bool> predicate) {
        if (value.TryGetSuccess(out var v)) {
            if (predicate(v)) {
                return value;
            }
        } else if (value.TryGetError(out var error)) {
            return new OptionalResult<T>(error);
        }
        return new OptionalResult<T>();
    }

    public static OptionalResult<T> If<T, A>(this OptionalResult<T> value, A args, Func<T, A, bool> predicate) {
        if (value.TryGetSuccess(out var v)) {
            if (predicate(v, args)) {
                return value;
            }
        } else if (value.TryGetError(out var error)) {
            return new OptionalResult<T>(error);
        }
        return new OptionalResult<T>();
    }

    public static OptionalResult<R> Map<T, A, R>(this OptionalResult<T> value, A args, Func<T, A, OptionalResult<R>> predicate) {
        if (value.TryGetSuccess(out var v)) {
            return predicate(v, args);
        } else if (value.TryGetError(out var error)) {
            return new OptionalResult<R>(error);
        } else {
            return new OptionalResult<R>();
        }
    }

    public static OptionalResult<T> OrDefault<T, A>(this OptionalResult<T> value, A args, Func<A, OptionalResult<T>> fnDefaultValue) {
        if (value.TryGetSuccess(out var _)) {
            return value;
        } else if (value.TryGetError(out var error)) {
            return new OptionalResult<T>(error);
        } else {
            return fnDefaultValue(args);
        }
    }

    public static OptionalResult<T> OrDefault<T>(this OptionalResult<T> value, OptionalResult<T> defaultValue) {
        if (value.TryGetSuccess(out var _)) {
            return value;
        } else if (value.TryGetError(out var error)) {
            return new OptionalResult<T>(error);
        } else {
            return defaultValue;
        }
    }


    public static OptionalResult<R> Try<T, A, R>(this OptionalResult<T> value, A args, Func<T, A, OptionalResult<R>> action) {
        try {
            if (value.TryGetSuccess(out var v)) {
                return action(v, args);
            } else if (value.TryGetError(out var error)) {
                return new OptionalResult<R>(error);
            } else {
                return new OptionalResult<R>();
            }
        } catch (Exception ex) {
            return new OptionalResult<R>(ErrorValue.CreateFromCatchedException(ex));
        }
    }

    public static OptionalResult<T> Catch<T>(this OptionalResult<T> value, Func<ErrorValue, OptionalResult<T>> fnDefaultValue) {
        if (value.TryGetError(out var error)) {
            return fnDefaultValue(error);
        } else {
            return value;
        }
    }

    public static OptionalResult<T> Catch<T, A>(this OptionalResult<T> value, A args, Func<ErrorValue, A, OptionalResult<T>> fnDefaultValue) {
        if (value.TryGetError(out var error)) {
            return fnDefaultValue(error, args);
        } else {
            return value;
        }
    }

    public static OptionalResult<R> Chain<T, R>(
        this OptionalResult<T> that,
        OptionalResult<R> defaultValue = default,
        Func<T, OptionalResult<R>, OptionalResult<R>>? onSuccess = default,
        Func<OptionalResult<R>, OptionalResult<R>>? onNoValue = default,
        Func<ErrorValue, OptionalResult<R>, OptionalResult<R>>? onError = default
    ) {
        if (that.TryGetError(out var error, out var opt)) {
            if (onError is null) {
                return new OptionalResult<R>(error);
            } else {
                return onError(error, defaultValue);
            }
        }
        if (opt.TryGetSuccess(out var value)) {
            if (onSuccess is null) {
                return defaultValue;
            } else {
                return onSuccess(value, defaultValue);
            }
        }
        {
            if (onNoValue is null) {
                return defaultValue;
            } else {
                return onNoValue(defaultValue);
            }
        }
    }

    public static OptionalResult<R> Chain<T, A, R>(
        this OptionalResult<T> that,
        A args,
        OptionalResult<R> defaultValue = default,
        Func<T, A, OptionalResult<R>, OptionalResult<R>>? onSuccess = default,
        Func<A, OptionalResult<R>, OptionalResult<R>>? onNoValue = default,
        Func<ErrorValue, A, OptionalResult<R>, OptionalResult<R>>? onError = default
    ) {
        if (that.TryGetError(out var error, out var opt)) {
            if (onError is null) {
                return new OptionalResult<R>(error);
            } else {
                return onError(error, args, defaultValue);
            }
        }
        if (opt.TryGetSuccess(out var value)) {
            if (onSuccess is null) {
                return defaultValue;
            } else {
                return onSuccess(value, args, defaultValue);
            }
        }
        {
            if (onNoValue is null) {
                return defaultValue;
            } else {
                return onNoValue(args, defaultValue);
            }
        }
    }
}