namespace Brimborium.ReturnValue;

public class ErrorValueTest {
    [Fact]
    public void ErrorValue01_Catch() {
        try {
            throw new Exception("gna");
        } catch (Exception error) {
            var errorValue = ErrorValue.CreateFromCatchedException(error);
            Assert.Same(error, errorValue.Exception);
            Assert.NotNull(errorValue.ExceptionDispatchInfo);
        }
    }
  
    [Fact]
    public void ErrorValue_Uninitialized() {
        var act = ErrorValue.Uninitialized;
        Assert.NotNull(act.Exception);
        try {
            act.Throw();
        } catch (Exception error){
            Assert.Same(act.Exception, error);
        }
    }

    [Fact]
    public void ErrorValue_Throw() {
        var act = new ErrorValue(new Exception("gna"));
        try {
            act.Throw();
        } catch (Exception error) {
            Assert.Same(act.Exception, error);
            act = ErrorValue.CreateFromCatchedException(error);
        }
        try {
            act.Throw();
        } catch (Exception error) {
            Assert.Same(act.Exception, error);
        }
    }

    [Fact]
    public void ErrorValue_WithIsLogged1() {
        var act = new ErrorValue(new Exception("gna"));
        try {
            act.Throw();
        } catch (Exception error) {
            Assert.Same(act.Exception, error);
            act = ErrorValue.CreateFromCatchedException(error);
        }
        act=act.WithIsLogged();
    }

    [Fact]
    public void ErrorValue_WithIsLogged2() {
        var act = new ErrorValue(new Exception("gna"));
        try {
            act.Throw();
        } catch (Exception error) {
            Assert.Same(act.Exception, error);
            act = ErrorValue.CreateFromCatchedException(error);
        }
        var act2 = ErrorValue.GetAndSetIsLogged(ref act);
    }


}