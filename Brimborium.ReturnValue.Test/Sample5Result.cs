/*
namespace Brimborium.ReturnValue;

public class Sample5Result {
    [Fact]
    public void Sample5Result_01() {
        var act = DoSomething5(@"c:\");

        Assert.True(act.TryGetSuccess(out var value));
        Assert.False(act.TryGetError(out var error));
        Assert.NotNull(value);
        Assert.Null(error);
    }

    [Fact]
    public void Sample5Result_02() {
        var act = DoSomething5(@"a:\");

        Assert.False(act.TryGetSuccess(out var value));
        Assert.True(act.TryGetError(out var error));
        Assert.Null(value);
        Assert.NotNull(error);
    }

    private static OptionalResult<int> DoSomething5b(string path) {
        try {
            if (string.IsNullOrEmpty(path)) { return NoValue.Value; }

            var di = new System.IO.DirectoryInfo(path);
            return di.GetDirectories().Length;

            //} catch (System.IO.DirectoryNotFoundException error) {

        } catch (Exception error) {
            return error;
        }
    }

    private static OptionalResult<string> DoSomething5(string path) {
        var d = DoSomething5b(path);

        var x = d.Chaining()
            .WithDefault<string>(string.Empty)
            .WithOnSuccess((d, r) => r.WithValue(d.ToString()))
            .Do();

        return NoValue.Value;
    }
}
*/