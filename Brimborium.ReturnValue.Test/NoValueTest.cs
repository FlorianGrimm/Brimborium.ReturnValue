namespace Brimborium.ReturnValue;

public class NoValueTest
{
    [Fact]
    public void NoValue01_NoValue()
    {
        Assert.Equal(new NoValue(), new NoValue());
    }
}
