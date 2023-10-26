namespace Brimborium.ReturnValue;

public class EnumerableExtensionsTest
{
    [Fact]
    public void SelectWhereTest1(){
        var source = new int[] { 1, 2, 3, 4, 5 };
        var result = source.SelectWhere((x) => {
            if (x % 2 == 0) {
                return new Optional<int>(x);
            } else {
                return new Optional<int>();
            }
        });
        Assert.Equal(new int[] { 2, 4 }, result);
    }

    [Fact]
    public void SelectWhereTest2(){
        var source = new int[] { 1, 2, 3, 4, 5 };
        var result = source.SelectWhere((x) => {
            if (x % 2 == 0) {
                return new Optional<int>(x);
            } else {
                return NoValue.Instance;
            }
        });
        Assert.Equal(new int[] { 2, 4 }, result);
    }

    
    [Fact]
    public void SelectWhereTest3() {

        //Optional.ToOptional
        var source = new int[] { 1, 2, 3, 4, 5 };
        var result = source.SelectWhere(static (x) => {
            if (x % 2 == 0) {
                return new Optional<int>(x);
            } else {
                return NoValue.Instance;
            }
        });
        Assert.Equal(new int[] { 2, 4 }, result);

        // static OptionalResult<int> p(int x)
        //     => x % 2 == 0 ? x.AsOptionalResult() : NoValue.Instance;
    }
}
