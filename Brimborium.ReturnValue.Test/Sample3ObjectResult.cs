﻿namespace Brimborium.ReturnValue;

public class Sample3ObjectResult {
    [Fact]
    public void Sample3ObjectResult_01() {
        var act = DoSomething3(@"c:\");

        Assert.True(act.TryGetValue(out var value));
        Assert.False(act.TryGetError(out var error));
        Assert.NotNull(value);
        Assert.Null(error.Exception);
    }

    [Fact]
    public void Sample3ObjectResult_02() {
        var act = DoSomething3(@"a:\");

        Assert.False(act.TryGetValue(out var value));
        Assert.True(act.TryGetError(out var error));
        Assert.Null(value);
        Assert.NotNull(error.Exception);
    }

    [Fact]
    public void Sample3ObjectResult_03() {
        var act = DoSomething3(@"");

        Assert.False(act.TryGetValue(out var value));
        Assert.False(act.TryGetError(out var error));
        Assert.True(act.TryGetNoValue());
        Assert.Null(value);
        Assert.Null(error.Exception);
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
        if (!dopt.TryGetValue(out var cnt)) { return NoValue.Value; }
        return cnt.ToString();
    }
}
