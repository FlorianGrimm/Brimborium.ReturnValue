﻿namespace Brimborium.ReturnValue;

public static class OptionalResult {
    public static OptionalResult<T> ToOptionalResult<T>(this T value) {
        return new OptionalResult<T>(value);
    }

    public static OptionalResult<T> If<T>(this OptionalResult<T> value, Func<T, bool> predicate) {
        if (value.TryGetSuccess(out var v) && predicate(v)) {
            return value;
        } else if (value.TryGetError(out var error)) {
            return new OptionalResult<T>(error);
        } else {
            return new OptionalResult<T>();
        }
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

    public static OptionalResult<R> Select<T, A, R>(this OptionalResult<T> value, A args, Func<T, A, OptionalResult<R>> predicate) {
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
            return new OptionalResult<R>(ex);
        }
    }

    public static OptionalResult<T> Catch<T>(this OptionalResult<T> value, Func<Exception, OptionalResult<T>> fnDefaultValue) {
        if (value.TryGetError(out var error)) {
            return fnDefaultValue(error);
        } else {
            return value;
        }
    }
}