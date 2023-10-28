namespace Brimborium.ReturnValue;

using System.Text.Json;

public class OptionalResultOfTTest {
    [Fact]
    public void OptionalResultOfTTest01_Success() {
        {
            OptionalResult<int> or1 = NoValue.Value;
            Assert.True(or1.TryGetNoValue());
            Assert.False(or1.TryGetSuccess(out var value1));
            Assert.False(or1.TryGetError(out var error1));
            Assert.Equal(0, value1);
            Assert.Null(error1.Exception);
        }
        {
            OptionalResult<int> or2 = 42;
            Assert.False(or2.TryGetNoValue());
            Assert.True(or2.TryGetSuccess(out var value2));
            Assert.False(or2.TryGetError(out var error2));
            Assert.Equal(42, value2);
            Assert.Null(error2.Exception);
        }
        {
            OptionalResult<int> or3 = new Exception("gna");
            Assert.False(or3.TryGetNoValue());
            Assert.False(or3.TryGetSuccess(out var value3));
            Assert.True(or3.TryGetError(out var error3));
            Assert.Equal(0, value3);
            Assert.Equal("gna", error3.Exception.Message);
        }
    }

    [Fact]
    public void OptionalResultOfTTest02_Deconstruct() {
        {
            OptionalResult<int> or1 = NoValue.Value;
            var (m, v, e) = or1;

            Assert.Equal(OptionalResultMode.NoValue, m);
            Assert.Equal(0, v);
            Assert.Null(e.Exception);
        }
        {
            OptionalResult<int> or2 = 42;
            var (m, v, e) = or2;

            Assert.Equal(OptionalResultMode.Success, m);
            Assert.Equal(42, v);
            Assert.Null(e.Exception);
        }
        {
            OptionalResult<int> or3 = new Exception("gna");
            var (m, v, e) = or3;

            Assert.Equal(OptionalResultMode.Error, m);
            Assert.Equal(0, v);
            Assert.Equal("gna", e.Exception?.Message);
        }
    }

    [Fact]
    public void OptionalResultOfTTest03_With() {
        OptionalResult<int> or1 = NoValue.Value;
        OptionalResult<int> or2 = or1.WithValue(21);
        OptionalResult<int> or3 = or2.WithError(new Exception("gna"));
        OptionalResult<int> or4 = or3.WithNoValue();

        Assert.Equal(OptionalResultMode.NoValue, or1.Mode);
        Assert.True(or2.TryGetSuccess(out var value2));
        Assert.Equal(21, value2);
        Assert.True(or3.TryGetError(out var error3));
        Assert.Equal("gna", error3.Exception.Message);
        Assert.Equal(OptionalResultMode.NoValue, or1.Mode);
    }

    [Fact]
    public void OptionalResultOfTTest04_ErrorValue() {
        OptionalResult<int> or1 = new Exception("gna").AsErrorValue().AsOptionalResult<int>();
        Assert.Equal(OptionalResultMode.Error, or1.Mode);
        Assert.Equal("gna", or1.Error.Exception.Message);

    }

    [Fact]
    public void OptionalResultOfTTest04_Json() {
        {
            OptionalResult<int> or1 = new OptionalResult<int>();

            var json1 = System.Text.Json.JsonSerializer.Serialize(or1);
            Assert.Equal("""{"Mode":0,"Value":0,"Error":{"Exception":null,"ExceptionDispatchInfo":null,"IsLogged":false}}""", json1);
            var ro1 = System.Text.Json.JsonSerializer.Deserialize<OptionalResult<int>>(json1);
            Assert.Equal(or1.Mode, ro1.Mode);
        }
        {
            OptionalResult<int> or2 = new OptionalResult<int>(42);

            var json2 = System.Text.Json.JsonSerializer.Serialize(or2);
            Assert.Equal("""{"Mode":1,"Value":42,"Error":{"Exception":null,"ExceptionDispatchInfo":null,"IsLogged":false}}""", json2);
            var ro2 = System.Text.Json.JsonSerializer.Deserialize<OptionalResult<int>>(json2);
            Assert.Equal(or2.Mode, ro2.Mode);
            Assert.Equal(or2.Value, ro2.Value);
            Assert.Null(ro2.Error.Exception);

        }
        {
            OptionalResult<int> or3 = new OptionalResult<int>(new Exception("gna"));

            var json3 = System.Text.Json.JsonSerializer.Serialize(or3);
            Assert.Contains("\"Message\":\"gna\"", json3);
            var ro3 = System.Text.Json.JsonSerializer.Deserialize<OptionalResult<int>>(json3);
            Assert.Equal(or3.Mode, ro3.Mode);
            Assert.Equal(or3.Value, ro3.Value);
            // this fails because of System.Text.Json
            // Assert.Equal("gna", ro3.Error.Message);
            Assert.NotNull(ro3.Error.Exception);
        }
    }



    [Fact]
    public void OptionalResultOfTTest10_IfNoValue() {
        var r = new OptionalResult<int>();
        var b = r.If(static (v) => v == 7);
        Assert.False(b);
        Assert.True(b.TryGetNoValue());
    }


    [Fact]
    public void OptionalResultOfTTest11_IfSuccess() {
        var r = new OptionalResult<int>(7);
        var b = r.If(static (v) => v == 7);
        Assert.True(b);
        Assert.True(b.TryGetSuccess(out var act) && act == 7);
    }

    [Fact]
    public void OptionalResultOfTTest12_IfFail() {
        var r = new OptionalResult<int>(5);
        var b = r.If(static (v) => v == 7);
        Assert.False(b);
        Assert.True(b.TryGetNoValue());
    }


    [Fact]
    public void OptionalResultOfTTest20_IfNoValue() {
        var r = new OptionalResult<int>();
        var b = r.If(7, static (v, a) => v == a);
        Assert.False(b);
        Assert.True(b.TryGetNoValue());
    }

    [Fact]
    public void OptionalResultOfTTest21_IfSuccess() {
        var r = new OptionalResult<int>(7);
        var b = r.If(7, static (v, a) => v == a);
        Assert.True(b);
        Assert.True(b.TryGetSuccess(out var act) && act == 7);
    }

    [Fact]
    public void OptionalResultOfTTest22_IfFail() {
        var r = new OptionalResult<int>(5);
        var b = r.If(7, static (v, a) => v == a);
        Assert.False(b);
        Assert.True(b.TryGetNoValue());
    }


    [Fact]
    public void OptionalResultOfTTest30_OrDefaultWithNoValue() {
        var r = new OptionalResult<int>();
        var b = r.OrDefault(6, (a) => a * 7);
        Assert.True(b);
        Assert.True(b.TryGetSuccess(out var act) && act == 42);
    }

    [Fact]
    public void OptionalResultOfTTest31_OrDefaultWithValue() {
        var r = new OptionalResult<int>(21);
        var b = r.OrDefault(6, (a) => a * 7);
        Assert.True(b);
        Assert.True(b.TryGetSuccess(out var act) && act == 21);
    }

    [Fact]
    public void OptionalResultOfTTest40_TryNoError() {
        var r = new OptionalResult<int>(21);
        var b = r.Try(1, static (v, a) => { return (v * a).AsOptionalResult(); });
        Assert.True(b);
        Assert.True(b.TryGetSuccess(out var act) && act == 21);
    }


    [Fact]
    public void OptionalResultOfTTest50_TryError() {
        var r = new OptionalResult<int>(21);
        var b = r.Try(1, static (v, a) => {
            if (a < 0) {
                return (v * a).AsOptionalResult();
            }
            throw new ArgumentException("gna");
        });
        Assert.False(b);
        Assert.True(b.TryGetError(out var act1));
        Assert.True(b.TryGetError(out var act2) && act2.Exception is ArgumentException);
    }
}
