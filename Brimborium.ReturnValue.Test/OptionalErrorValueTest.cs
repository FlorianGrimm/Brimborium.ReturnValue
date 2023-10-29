namespace Brimborium.ReturnValue;

public class OptionalErrorValueTest {
    [Fact]
    public void OptionalErrorValue01_Catch() {
        try {
            throw new Exception("gna");
        } catch (Exception error) {
            var errorValue = OptionalErrorValue.CreateFromCatchedException(error);
            Assert.Same(error, errorValue.Exception);
            Assert.NotNull(errorValue.ExceptionDispatchInfo);
        }
    }

    [Fact]
    public void OptionalErrorValue_Uninitialized() {
        var act = OptionalErrorValue.Uninitialized;
        Assert.NotNull(act.Exception);
        try {
            act.Throw();
        } catch (Exception error) {
            Assert.Same(act.Exception, error);
        }
    }

    [Fact]
    public void OptionalErrorValue_Throw() {
        var act = new OptionalErrorValue(new Exception("gna"));
        try {
            act.Throw();
        } catch (Exception error) {
            Assert.Same(act.Exception, error);
            act = OptionalErrorValue.CreateFromCatchedException(error);
        }
        try {
            act.Throw();
        } catch (Exception error) {
            Assert.Same(act.Exception, error);
        }
    }

    [Fact]
    public void OptionalErrorValue_WithIsLogged1() {
        var act = new OptionalErrorValue(new Exception("gna"));
        try {
            act.Throw();
        } catch (Exception error) {
            Assert.Same(act.Exception, error);
            act = OptionalErrorValue.CreateFromCatchedException(error);
        }
        act = act.WithIsLogged();
    }

    [Fact]
    public void OptionalErrorValue_WithIsLogged2() {
        var act = new OptionalErrorValue(new Exception("gna"));
        try {
            act.Throw();
        } catch (Exception error) {
            Assert.Same(act.Exception, error);
            act = OptionalErrorValue.CreateFromCatchedException(error);
        }
        var act2 = OptionalErrorValue.GetAndSetIsLogged(ref act);
    }
}
