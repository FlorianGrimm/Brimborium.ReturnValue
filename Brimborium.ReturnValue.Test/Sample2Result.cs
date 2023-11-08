namespace Brimborium.ReturnValue;

public class Sample2Result {
    [Fact]
    public void Sample2Result_01() {
        var act= DoSomething2(@"c:\");

        Assert.True(act.TryGetValue(out var value));
        Assert.False(act.TryGetError(out var error));
        Assert.NotNull(value);
        Assert.Null(error.Exception);
    }

    [Fact]
    public void Sample2Result_02() {
        var act = DoSomething2(@"a:\");

        Assert.False(act.TryGetValue(out var value));
        Assert.True(act.TryGetError(out var error));
        Assert.Null(value);
        Assert.NotNull(error.Exception);
    }

    private static Result<int> DoSomething2b(string path) 
        => path.TryCatch(
            static (path) => {
                var di = new System.IO.DirectoryInfo(path);
                return di.GetDirectories().Length;
            }
            );
    private static Result<string> DoSomething2(string path) {
        if (!DoSomething2b(path).TryGet(out var cnt, out var error)) { return error; }
        return cnt.ToString();
    }
}
