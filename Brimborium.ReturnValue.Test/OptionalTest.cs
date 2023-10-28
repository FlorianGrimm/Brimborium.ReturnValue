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

    [Fact]
    public void Optional04_Mode() {
        var o1 = new Optional<string>(OptionalMode.NoValue, null);
        Assert.Equal(OptionalMode.NoValue, o1.Mode);

        var o2 = new Optional<string>(OptionalMode.Success, "42");
        Assert.Equal(OptionalMode.Success, o2.Mode);
        Assert.Equal("42", o2.Value);

        Assert.Equal("21", o1.GetValueOrDefault("21"));
        Assert.Equal("42", o2.GetValueOrDefault("21"));
    }

    [Fact]
    public void Optional05_TypeCast() {
        Optional<string> o1 = NoValue.Value;
        Assert.Equal(OptionalMode.NoValue, o1.Mode);

        Optional<string> o2 = "42";        
        Assert.Equal(OptionalMode.Success, o2.Mode);
        Assert.Equal("42", o2.Value);
        Assert.Equal("42", o2.GetValueOrDefault("21"));
    }


    [Fact]
    public void Optional06_EmptyCtor() {
        Optional<string> o1 = new();
        Assert.Equal(OptionalMode.NoValue, o1.Mode);
    }
}