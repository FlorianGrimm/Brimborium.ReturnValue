namespace Brimborium.ReturnValue;

public class Sample3Result {
    [Fact]
    public void Sample3Result_01() {
        var act = DoSomething3(@"c:\");

        Assert.True(act.TryGetSuccess(out var value));
        Assert.False(act.TryGetError(out var error));
        Assert.NotNull(value);
        Assert.Null(error);
    }

    [Fact]
    public void Sample3Result_02() {
        var act = DoSomething3(@"a:\");

        Assert.False(act.TryGetSuccess(out var value));
        Assert.True(act.TryGetError(out var error));
        Assert.Null(value);
        Assert.NotNull(error);
    }

    private static OptionalResult<int> DoSomething3b(string path) {
        try {
            if (string.IsNullOrEmpty(path)) { return NoValue.Value; }

            var di = new System.IO.DirectoryInfo(path);
            return di.GetDirectories().Length;

            //} catch (System.IO.DirectoryNotFoundException error) {

        } catch (Exception error) {
            return error;
        }
    }
    private static OptionalResult<string> DoSomething3(string path) {
        var d = DoSomething3b(path);
        if (d.TryGetError(out var error, out var dopt)) { return error; }
        if (dopt.TryGetNoValue(out var cnt)) { return NoValue.Value; }
        return cnt.ToString();
    }
}
