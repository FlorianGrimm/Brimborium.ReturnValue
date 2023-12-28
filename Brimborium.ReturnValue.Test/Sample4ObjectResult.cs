namespace Brimborium.ReturnValue;

public class Sample4ObjectResult {
    [Fact]
    public void Sample4ObjectResult_01() {
        var act = DoSomething4(@"c:\");

        Assert.True(act.TryGetValue(out var value));
        Assert.False(act.TryGetError(out var error));
        Assert.NotNull(value);
        Assert.Null(error.Exception);
    }

    [Fact]
    public void Sample4ObjectResult_02() {
        var act = DoSomething4(@"a:\");

        Assert.False(act.TryGetValue(out var value));
        Assert.True(act.TryGetError(out var error));
        Assert.Null(value);
        Assert.NotNull(error.Exception);
    }

    [Fact]
    public void Sample4ObjectResult_03() {
        var act = DoSomething4(@"");

        Assert.False(act.TryGetValue(out var value));
        Assert.False(act.TryGetError(out var error));
        Assert.True(act.TryGetNoValue());
        Assert.Null(value);
        Assert.Null(error.Exception);
    }
    private static OptionalResult<int> DoSomething4b(string path) {
        try {
            if (string.IsNullOrEmpty(path)) { return NoValue.Value; }

            var di = new System.IO.DirectoryInfo(path);
            return di.GetDirectories().Length;

            //} catch (System.IO.DirectoryNotFoundException error) {

        } catch (Exception error) {
            return error;
        }
    }

    private static OptionalResult<string> DoSomething4(string path) {
        var d = DoSomething4b(path);

        OptionalResult<string> result = d.Chain(
            defaultValue: new OptionalResult<string>(),
            onSuccess: static (d, r) => r.WithValue(d.ToString()),
            onNoValue: static (result) => result,
            onError: static (error, r) => r.WithError(error)
        );

        return result;
    }
}
