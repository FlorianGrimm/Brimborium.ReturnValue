namespace Brimborium.ReturnValue;

public class ResultTest {
    [Fact]
    public void Result01_Result() {
        int value = 1;
        Exception error = new Exception("gna");

        Result<int> result01 = Result.NoValue.WithValue(value);
        Result<int> result02 = Result.AsSuccessValue(value);
        Result<int> result03 = value.AsSuccessValue();
        Result<int> result04 = error.AsErrorValue();
        Result<int> result05 = value.AsSuccessValue().AsResult();
        Result<int> result06 = error.AsErrorValue().AsResult<int>();
        Result<int> result07 = value;
        Result<int> result08 = error;
        Result<int> result09 = value.AsResult();
        Result<int> result10 = error.AsResult<int>();
        Result<int> result11 = result10.WithValue(value);
        Result<int> result12 = result11.WithError(error);

        Assert.True(result01.TryGetSuccess(out var value01));
        Assert.False(result01.TryGetError(out var error01));
        Assert.Equal(value, value01);
        Assert.True(result02.TryGetSuccess(out var value02));
        Assert.False(result02.TryGetError(out var error02));
        Assert.Equal(value, value02);
        Assert.True(result03.TryGetSuccess(out var value03));
        Assert.False(result03.TryGetError(out var error03));
        Assert.Equal(value, value03);
        Assert.False(result04.TryGetSuccess(out var value04));
        Assert.True(result04.TryGetError(out var error04));
        Assert.Equal(error, error04);
        Assert.True(result05.TryGetSuccess(out var value05));
        Assert.False(result05.TryGetError(out var error05));
        Assert.Equal(value, value05);
        Assert.False(result06.TryGetSuccess(out var value06));
        Assert.True(result06.TryGetError(out var error06));
        Assert.Equal(error, error06);
        Assert.True(result07.TryGetSuccess(out var value07));
        Assert.False(result07.TryGetError(out var error07));
        Assert.Equal(value, value07);
        Assert.False(result08.TryGetSuccess(out var value08));
        Assert.True(result08.TryGetError(out var error08));
        Assert.Equal(error, error08);
        Assert.True(result09.TryGetSuccess(out var value09));
        Assert.False(result09.TryGetError(out var error09));
        Assert.Equal(value, value09);
        Assert.False(result10.TryGetSuccess(out var value10));
        Assert.True(result10.TryGetError(out var error10));
        Assert.Equal(error, error10);
        Assert.True(result11.TryGetSuccess(out var value11));
        Assert.False(result11.TryGetError(out var error11));
        Assert.Equal(value, value11);
        Assert.False(result12.TryGetSuccess(out var value12));
        Assert.True(result12.TryGetError(out var error12));
        Assert.Equal(error, error12);
    }


    [Fact]
    public void Result01_OptionalResult() {
        int value = 1;
        Exception error = new Exception("gna");

        OptionalResult<int> result01 = Result.NoValue.AsOptionalResult<int>();
        OptionalResult<int> result02 = value.AsSuccessValue().AsOptionalResult<int>();
        OptionalResult<int> result03 = value.AsOptionalResult<int>();
        OptionalResult<int> result04 = error.AsOptionalResult<int>();
        OptionalResult<int> result05 = value;
        OptionalResult<int> result06 = error;
        OptionalResult<int> result07 = result06.AsResult();
        OptionalResult<int> result08 = result06.AsResult().AsOptionalResult();
        OptionalResult<int> result09 = result05.AsResult();
        OptionalResult<int> result10 = result05.AsResult().AsOptionalResult();

        Assert.Equal(OptionalResultMode.NoValue, result01.Mode);
        Assert.Equal(OptionalResultMode.Success, result02.Mode);
        Assert.True(result02.TryGetSuccess(out var value02));
        Assert.Equal(value, value02);
        Assert.Equal(OptionalResultMode.Success, result03.Mode);
        Assert.True(result03.TryGetSuccess(out var value03));
        Assert.Equal(value, value03);
        Assert.Equal(OptionalResultMode.Error, result04.Mode);
        Assert.True(result04.TryGetError(out var error04));
        Assert.Equal(error, error04);
        Assert.Equal(OptionalResultMode.Success, result05.Mode);
        Assert.True(result05.TryGetSuccess(out var value05));
        Assert.Equal(value, value05);
        Assert.Equal(OptionalResultMode.Error, result06.Mode);
        Assert.True(result06.TryGetError(out var error06));
        Assert.Equal(error, error06);
        Assert.Equal(OptionalResultMode.Error, result07.Mode);
        Assert.True(result07.TryGetError(out var error07));
        Assert.Equal(error, error07);
        Assert.Equal(OptionalResultMode.Error, result08.Mode);
        Assert.True(result08.TryGetError(out var error08));
        Assert.Equal(error, error08);
        Assert.Equal(OptionalResultMode.Success, result09.Mode);
        Assert.True(result09.TryGetSuccess(out var value09));
        Assert.Equal(value, value09);
        Assert.Equal(OptionalResultMode.Success, result10.Mode);
        Assert.True(result10.TryGetSuccess(out var value10));
        Assert.Equal(value, value10);
    }

}
