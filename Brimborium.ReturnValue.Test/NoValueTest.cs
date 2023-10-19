namespace Brimborium.ReturnValue;

public class NoValueTest
{
    [Fact]
    public void NoValue01_NoValue()
    {
        Assert.Equal(new NoValue(), new NoValue());
    }

    [Fact]
    public void NoValue02_NoValue() {
        Assert.Equal(new NoValue(), NoValue.Instance);
    }

    [Fact]
    public void NoValue03_ToString() {
        Assert.Equal("", NoValue.Instance.ToString());
    }


    [Fact]
    public void NoValue04_ToOptional() {
        Assert.True(NoValue.Instance.ToOptional<int>().TryGetNoValue());
    }
}
