namespace Brimborium.ReturnValue;
public class ResultOfTTest
{
    [Fact]
    public void ResultOfT01_Success()
    {
        var r = new Result<int>(42);
        Assert.Equal(ResultMode.Success, r.Mode);

        Assert.True(r.TryGetSuccess(out var a));
        Assert.False(r.TryGetError(out var b));
        Assert.Equal(42, a);

        Assert.True(r.TryGet(out var c, out var d));
        Assert.Equal(42, c);

        var (e, f, g) = r;
        Assert.Equal(ResultMode.Success, e);
        Assert.Equal(42, f);
        Assert.Null(g.Exception);

    }

    [Fact]
    public void ResultOfT02_Error()
    {
        var r = new Result<int>(new Exception("abc"));
        Assert.Equal(ResultMode.Error, r.Mode);

        Assert.False(r.TryGetSuccess(out var a));
        Assert.True(r.TryGetError(out var b));
        Assert.Equal("abc", b.Exception.Message);

        Assert.False(r.TryGet(out var c, out var d));
        Assert.Equal("abc", d.Exception.Message);

        var (e, f, g) = r;
        Assert.Equal(ResultMode.Error, e);
        Assert.Equal(0, f);
        Assert.Equal("abc", g.Exception?.Message);
    }

    [Fact]
    public void ResultOfT03_Error()
    {
        Result<string> act = new();
        var (a, b, c) = act;
        Assert.Equal(ResultMode.Error, a);
        Assert.Null(b);
        Assert.IsType<UninitializedException>(c.Exception);
    }

}
