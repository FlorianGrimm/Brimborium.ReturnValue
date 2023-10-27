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
}