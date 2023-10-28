namespace Brimborium.ReturnValue;
public class OptionalTest {
    [Fact]
    public void Optional01_NoValue() {
        var o = new Optional<int>();
        Assert.Equal(OptionalMode.NoValue, o.Mode);

        Assert.True(o.TryGetNoValue());
        Assert.False(o.TryGetSuccess(out var result));
    }


    [Fact]
    public void Optional02_Success() {
        var o = new Optional<int>(42);
        Assert.Equal(OptionalMode.Success, o.Mode);

        Assert.False(o.TryGetNoValue());
        {
            Assert.True(o.TryGetSuccess(out var result));
            Assert.Equal(42, result);
        }
    }

    [Fact]
    public void Optional03_Success() {
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
        Optional<string> o1 = NoValue.Instance;
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

    [Fact]
    public void Optional_If() {
        Optional<string> o1 = new();
        t(o1, new());
        Optional<string> o2 = new("a");
        t(o2, new("a"));
        Optional<string> o3 = new("b");
        t(o3, new());

        static void t(Optional<string> o, Optional<string> assert) {
            var act = o.If(v => v == "a");
            Assert.Equal(assert.Mode, act.Mode);
            Assert.Equal(assert.Value, act.Value);
        }
    }

    [Fact]
    public void Optional_IfWithArgs() {
        Optional<string> o1 = new();
        t(o1, new());
        Optional<string> o2 = new("a");
        t(o2, new("a"));
        Optional<string> o3 = new("b");
        t(o3, new());

        static void t(Optional<string> o, Optional<string> assert) {
            var act = o.If("a", (v, a) => v == a);
            Assert.Equal(assert.Mode, act.Mode);
            Assert.Equal(assert.Value, act.Value);
        }
    }

    [Fact]
    public void Optional_Map() {
        Optional<string> o1 = new();
        t(o1, new());

        Optional<string> o2 = new("a");
        t(o2, new("a-1"));

        Optional<string> o3 = new("b");
        t(o3, new("b-1"));

        static void t(Optional<string> o, Optional<string> assert) {
            var act = o.Map("1", (v, a) => ($"{v}-{a}").AsOptional());
            Assert.Equal(assert.Mode, act.Mode);
            Assert.Equal(assert.Value, act.Value);
        }
    }

    [Fact]
    public void Optional_OrDefaultValue() {
        Optional<string> o1 = new();
        t(o1, new("c"));

        Optional<string> o2 = new("a");
        t(o2, new("a"));

        Optional<string> o3 = new("b");
        t(o3, new("b"));

        static void t(Optional<string> o, Optional<string> assert) {
            var act = o.OrDefault("c");
            Assert.Equal(assert.Mode, act.Mode);
            Assert.Equal(assert.Value, act.Value);
        }
    }

    [Fact]
    public void Optional_OrDefaultFn() {
        Optional<string> o1 = new();
        t(o1, new("c"));

        Optional<string> o2 = new("a");
        t(o2, new("a"));

        Optional<string> o3 = new("b");
        t(o3, new("b"));

        static void t(Optional<string> o, Optional<string> assert) {
            var act = o.OrDefault("c", (v) => v.AsOptional());
            Assert.Equal(assert.Mode, act.Mode);
            Assert.Equal(assert.Value, act.Value);
        }
    }
}