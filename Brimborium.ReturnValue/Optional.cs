namespace Brimborium.ReturnValue;

public static class Optional {
    public static Optional<T> AsOptional<T>(this T value) {
        return new Optional<T>(value);
    }

    public static Optional<T> If<T>(this Optional<T> value, Func<T, bool> predicate) {
        if (value.TryGetSuccess(out var v) && predicate(v)) {
            return value;
        } else {
            return new Optional<T>();
        }
    }

    public static Optional<T> If<T, A>(this Optional<T> value, A args, Func<T, A, bool> predicate) {
        if (value.TryGetSuccess(out var v) && predicate(v, args)) {
            return value;
        } else {
            return new Optional<T>();
        }
    }

    public static Optional<R> Map<T, A, R>(this Optional<T> value, A args, Func<T, A, Optional<R>> predicate) {
        if (value.TryGetSuccess(out var v)) {
            return predicate(v, args);
        } else {
            return NoValue.Value;
        }
    }

    public static Optional<T> OrDefault<T, A>(this Optional<T> value, A args, Func<A, Optional<T>> fnDefaultValue) {
        if (value.TryGetSuccess(out var _)) {
            return value;
        } else {
            return fnDefaultValue(args);
        }
    }

    public static Optional<T> OrDefault<T>(this Optional<T> value, Optional<T> defaultValue) {
        if (value.TryGetSuccess(out var _)) {
            return value;
        } else {
            return defaultValue;
        }
    }
}