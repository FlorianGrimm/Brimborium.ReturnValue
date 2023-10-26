namespace Brimborium.ReturnValue;

public static class EnumerableExtensions
{
    public static IEnumerable<R> SelectWhere<T,R>(this IEnumerable<T> source, Func<T, Optional<R>> predicateTransform) {
        foreach (var item in source) {
            var optR = predicateTransform(item);
            if (optR.TryGetSuccess(out var r)){
                yield return r;
            }
        }
    }

    
    public static IEnumerable<R> SelectWhere<T, A, R>(this IEnumerable<T> source, A args, Func<T, A, Optional<R>> predicateTransform) {
        foreach (var item in source) {
            var optR = predicateTransform(item, args);
            if (optR.TryGetSuccess(out var r)){
                yield return r;
            }
        }
    }

    public static IEnumerable<R> SelectWhereMany<T,R>(this IEnumerable<T> source, Func<T, Optional<IEnumerable<R>>> predicateTransform) {
        foreach (var item in source) {
            var optR = predicateTransform(item);
            if (optR.TryGetSuccess(out var r)){
                foreach(var itemInner in r){
                    yield return itemInner;
                }
            }
        }
    }
}
