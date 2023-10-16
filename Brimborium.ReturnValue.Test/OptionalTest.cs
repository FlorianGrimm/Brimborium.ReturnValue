namespace Brimborium.ReturnValue;
public class OptionalTest
{
    [Fact]
    public void Optional01_NoValue()
    {
        var o = new Optional<int>();
        Assert.Equal(OptionalMode.NoValue, o.Mode);

        Assert.True(o.TryGetNoValue());
        Assert.False(o.TryGetSuccess(out var result));
    }


    [Fact]
    public void Optional02_Success()
    {
        var o = new Optional<int>(42);
        Assert.Equal(OptionalMode.Success, o.Mode);

        Assert.False(o.TryGetNoValue());
        {
            Assert.True(o.TryGetSuccess(out var result));
            Assert.Equal(42, result);
        }
    }


    [Fact]
    public void Optional03_Success()
    {
        var o = new Optional<string>();
        Assert.Equal(OptionalMode.NoValue, o.Mode);
    }
}