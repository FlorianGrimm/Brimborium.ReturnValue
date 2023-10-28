namespace Brimborium.ReturnValue;

public class Sample1Result {
    [Fact]
    public void Sample1Result_01() {
        var act= DoSomething1(@"c:\");

        Assert.True(act.TryGetSuccess(out var value));
        Assert.False(act.TryGetError(out var error));
        Assert.NotNull(value);
        Assert.Null(error.Exception);
    }

    [Fact]
    public void Sample1Result_02() {
        var act = DoSomething1(@"a:\");

        Assert.False(act.TryGetSuccess(out var value));
        Assert.True(act.TryGetError(out var error));
        Assert.Null(value);
        Assert.NotNull(error.Exception);
    }

    private static Result<int> DoSomething1b(string path) {
        try {
            var di = new System.IO.DirectoryInfo(path);
            return di.GetDirectories().Length;
        } catch (Exception error) {
            return error;
        }
    }
    private static Result<string> DoSomething1(string path) {
        var d = DoSomething1b(path);
        if (!d.TryGet(out var cnt, out var error)) { return error; }
        return cnt.ToString();
    }
}
