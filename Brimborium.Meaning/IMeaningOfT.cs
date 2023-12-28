//namespace Brimborium.Meaning;

//public interface IMeaning<T> {
//    T Value { get; }
//}

//public record Meaning<T>(T Value) : IMeaning<T> {
//    public static implicit operator Meaning<T>(T Value) { return new Meaning<T>(Value); }
//}

//public static class MeaningExtension {
//    public static TThat Unwrap<TThat, T>(this TThat that)
//        where TThat : IMeaning<T> {
//        return that;
//    }

//    public static TResult Unwrap<TThat, TResult, T>(this TThat that)
//        where TThat : IMeaning<TResult>
//        where TResult : IMeaning<T> {
//        return that.Value;
//    }

//    public static TResult Unwrap<TThat, TStep, TResult, T>(this TThat that)
//        where TThat : IMeaning<TStep>
//        where TStep : IMeaning<TResult>
//        where TResult : IMeaning<T> {
//        return that.Value.Value;
//    }
//}