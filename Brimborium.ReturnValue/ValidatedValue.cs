namespace Brimborium.ReturnValue;

public record struct ValidatedValue<T>(Optional<T> Value) : IValue<Optional<T>> {
    //T IValue<T>.Value => throw new NotImplementedException();

    //public bool TryGetValue([MaybeNullWhen(false)] out T value) {
    //    throw new NotImplementedException();
    //}
    public bool TryGetValue([MaybeNullWhen(false)] out Optional<T> value) {
        throw new NotImplementedException();
    }
}
