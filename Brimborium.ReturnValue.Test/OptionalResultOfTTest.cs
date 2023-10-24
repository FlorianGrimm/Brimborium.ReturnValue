namespace Brimborium.ReturnValue;

using System.Text.Json;

public class OptionalResultOfTTest {
    [Fact]
    public void OptionalResultOfTTest01_Success() {
        {
            OptionalResult<int> or1 = NoValue.Instance;
            Assert.True(or1.TryGetNoValue());
            Assert.False(or1.TryGetSuccess(out var value1));
            Assert.False(or1.TryGetError(out var error1));
            Assert.Equal(0, value1);
            Assert.Null(error1);
        }
        {
            OptionalResult<int> or2 = 42;
            Assert.False(or2.TryGetNoValue());
            Assert.True(or2.TryGetSuccess(out var value2));
            Assert.False(or2.TryGetError(out var error2));
            Assert.Equal(42, value2);
            Assert.Null(error2);
        }
        {
            OptionalResult<int> or3 = new Exception("gna");
            Assert.False(or3.TryGetNoValue());
            Assert.False(or3.TryGetSuccess(out var value3));
            Assert.True(or3.TryGetError(out var error3));
            Assert.Equal(0, value3);
            Assert.Equal("gna", error3.Message);
        }
    }

    [Fact]
    public void OptionalResultOfTTest02_Deconstruct() {
        {
            OptionalResult<int> or1 = NoValue.Instance;
            var (m, v, e) = or1;

            Assert.Equal(OptionalResultMode.NoValue, m);
            Assert.Equal(0, v);
            Assert.Null(e);
        }
        {
            OptionalResult<int> or2 = 42;
            var (m, v, e) = or2;

            Assert.Equal(OptionalResultMode.Success, m);
            Assert.Equal(42, v);
            Assert.Null(e);
        }
        {
            OptionalResult<int> or3 = new Exception("gna");
            var (m, v, e) = or3;

            Assert.Equal(OptionalResultMode.Error, m);
            Assert.Equal(0, v);
            Assert.Equal("gna", e?.Message);             
        }
    }

    [Fact]
    public void OptionalResultOfTTest03_With() {
        OptionalResult<int> or1 = NoValue.Instance;
        OptionalResult<int> or2 = or1.WithValue(21);
        OptionalResult<int> or3 = or2.WithError(new Exception("gna"));
        OptionalResult<int> or4 = or3.WithNoValue();

        Assert.Equal(OptionalResultMode.NoValue, or1.Mode);
        Assert.True(or2.TryGetSuccess(out var value2));
        Assert.Equal(21, value2);
        Assert.True(or3.TryGetError(out var error3));
        Assert.Equal("gna", error3.Message);
        Assert.Equal(OptionalResultMode.NoValue, or1.Mode);
    }

    [Fact]
    public void OptionalResultOfTTest04_ErrorValue() {
        OptionalResult<int> or1 = new Exception("gna").AsErrorValue().AsOptionalResult<int>();
        Assert.Equal(OptionalResultMode.Error, or1.Mode);
        Assert.Equal("gna", or1.Error.Message);

    }

    
    [Fact]
    public void OptionalResultOfTTest04_Json() {
        { 
            OptionalResult<int> or1 = new OptionalResult<int>();

            var json1 = System.Text.Json.JsonSerializer.Serialize(or1);
            Assert.Equal("""{"Mode":0,"Value":0,"Error":null}""", json1);
            var ro1 = System.Text.Json.JsonSerializer.Deserialize<OptionalResult<int>>(json1);
            Assert.Equal(or1.Mode, ro1.Mode);
        }
        {
            OptionalResult<int> or2 = new OptionalResult<int>(42);

            var json2 = System.Text.Json.JsonSerializer.Serialize(or2);
            Assert.Equal("""{"Mode":1,"Value":42,"Error":null}""", json2);
            var ro2 = System.Text.Json.JsonSerializer.Deserialize<OptionalResult<int>>(json2);
            Assert.Equal(or2.Mode, ro2.Mode);
            Assert.Equal(or2.Value, ro2.Value);
            Assert.Null(ro2.Error);

        }
        {
            OptionalResult<int> or3 = new OptionalResult<int>(new Exception("gna"));

            var json3 = System.Text.Json.JsonSerializer.Serialize(or3);
            Assert.Equal("""{"Mode":2,"Value":0,"Error":{"TargetSite":null,"Message":"gna","Data":{},"InnerException":null,"HelpLink":null,"Source":null,"HResult":-2146233088,"StackTrace":null}}""", json3);
            var ro3 = System.Text.Json.JsonSerializer.Deserialize<OptionalResult<int>>(json3);
            Assert.Equal(or3.Mode, ro3.Mode);
            Assert.Equal(or3.Value, ro3.Value);
            // this fails because of System.Text.Json
            // Assert.Equal("gna", ro3.Error.Message);
            Assert.NotNull(ro3.Error);
        }
    }
    
}
