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
        var result = source.SelectWhere(2,(x,a) => {
            if (x % a == 0) {
                return new Optional<int>(x);
            } else {
                return NoValue.Value;
            }
        });
        Assert.Equal(new int[] { 2, 4 }, result);
    }

    [Fact]
    public void SelectWhereTest3() {
        var source = new int[] { 1, 2, 3, 4, 5 };
        var result = source.SelectWhere(test);
        Assert.Equal(new int[] { 2, 4 }, result);
        
        static Optional<int> test(int x) {
            if (x % 2 == 0) {
                return x;
            } else {
                return NoValue.Value;
            }
        }
    }

    [Fact]
    public void SelectWhereTest4() {
        var source = new int[] { 1, 2, 3, 4, 5 };
        var result = source.SelectWhere(2, test);
        Assert.Equal(new int[] { 2, 4 }, result);

        static Optional<int> test(int x, int a) {
            if (x % 2 == 0) {
                return x;
            } else {
                return NoValue.Value;
            }
        }
    }
}
