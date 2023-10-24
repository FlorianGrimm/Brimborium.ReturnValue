namespace Brimborium.ReturnValue;

public enum OptionalResultMode { NoValue, Success, Error }

public struct OptionalResult<T> {
    [JsonInclude]
    public readonly OptionalResultMode Mode;
    [JsonInclude]
    [AllowNull] public readonly T Value;
    [JsonInclude]
    [AllowNull] public readonly Exception Error;

    public OptionalResult() {
        this.Mode = OptionalResultMode.NoValue;
        this.Value = default;
        this.Error = default;
    }

    public OptionalResult(T Value) {
        this.Mode = OptionalResultMode.Success;
        this.Value = Value;
        this.Error = default;
    }

    public OptionalResult(Exception error) {
        this.Mode = OptionalResultMode.Error;
        this.Value = default;
        this.Error = error;
    }

    [JsonConstructor]
    public OptionalResult(OptionalResultMode mode,[AllowNull] T value,[AllowNull] Exception error) {
        this.Mode = mode;
        this.Value = value;
        this.Error = error;
    }

    public void Deconstruct(out OptionalResultMode mode, out T? value, out Exception? error) {
        switch (this.Mode) {
            case OptionalResultMode.Success:
                mode = OptionalResultMode.Success;
                value = this.Value;
                error = default;
                return;
            case OptionalResultMode.Error:
                mode = OptionalResultMode.Error;
                value = default;
                error = this.Error;
                return;
            default:
                mode = OptionalResultMode.NoValue;
                value = default;
                error = default;
                return;
        }
    }

    public bool TryGetNoValue() {
        if (this.Mode == OptionalResultMode.NoValue) {
            return true;
        } else if (this.Mode == OptionalResultMode.Success) {
            return false;
        } else if (this.Mode == OptionalResultMode.Error) {
            return false;
        } else {
            return true;
        }
    }

    public bool TryGetSuccess([MaybeNullWhen(false)] out T value) {
        if (this.Mode == OptionalResultMode.Success) {
            value = this.Value!;
            return true;
        } else {
            value = default;
            return false;
        }
    }

    public bool TryGetError([MaybeNullWhen(false)] out Exception error) {
        if (this.Mode == OptionalResultMode.Error) {
            error = this.Error!;
            return true;
        } else {
            error = default;
            return false;
        }
    }

    public OptionalResult<T> WithNoValue() => new OptionalResult<T>();

    public OptionalResult<T> WithValue(T value) => new OptionalResult<T>(value);

    public OptionalResult<T> WithError(Exception error) => new OptionalResult<T>(error);


    public static implicit operator OptionalResult<T>(NoValue noValue) => new OptionalResult<T>();
 
    public static implicit operator OptionalResult<T>(T value) => new OptionalResult<T>(value);
    
    public static implicit operator OptionalResult<T>(Exception error) => new OptionalResult<T>(error);


    public static implicit operator OptionalResult<T>(Result<T> value) {
        if (value.TryGetSuccess(out var successValue)) {
            return new OptionalResult<T>(successValue);
        } else if (value.TryGetError(out var errorValue)) {
            return new OptionalResult<T>(errorValue);
        } else {
            return new OptionalResult<T>(new InvalidEnumArgumentException($"Invalid enum {value.Mode}."));
        }
    }
}

/*
https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/converters-how-to?pivots=dotnet-7-0
public class OptionalResultConverter : JsonConverterFactory {
    public override bool CanConvert(Type typeToConvert) {
        if (!typeToConvert.IsGenericType)
            return false;

        Type generic = typeToConvert.GetGenericTypeDefinition();
        return (generic == typeof(OptionalResult<>));
    }

    // [PreserveDependency(".ctor()", "System.Text.Json.Serialization.Converters.JsonKeyValuePairConverter`2")]
    public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options) {
        // Type valueType = type.GetGenericArguments()[0];
        // typeof(OptionalResultConverter<>).MakeGenericType(new Type[] { valueType }),
        var genericArguments = type.GetGenericArguments();
        JsonConverter converter = (JsonConverter)Activator.CreateInstance(
            typeof(OptionalResultConverter<>).MakeGenericType(genericArguments),
            BindingFlags.Instance | BindingFlags.Public,
            binder: null,
            args: null,
            culture: null)!;

        return converter;
    }
}

public class OptionalResultConverter<T> : JsonConverter<T> {
    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) {
        throw new NotImplementedException();
    }
}

*/