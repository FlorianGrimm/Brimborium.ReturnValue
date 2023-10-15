using System.Diagnostics.CodeAnalysis;

namespace Brimborium.ReturnValue;

public enum ResultMode { Success, Error }
public class Result<T>
{
    public readonly ResultMode Mode;
    public readonly T? Value;
    public readonly Exception? Error;

    public Result(T Value)
    {
        this.Mode = ResultMode.Success;
        this.Value = Value;
        this.Error = default;
    }

    public Result(Exception error)
    {
        this.Mode = ResultMode.Error;
        this.Value = default;
        this.Error = error;
    }

    public bool TryGet(
        [MaybeNullWhen(false)] out T value,
        [MaybeNullWhen(true)] out Exception error)
    {
        if (this.Mode == ResultMode.Success)
        {
            value = this.Value!;
            error=default;
            return true;
        }
        else
        {
            value = default;
            error = this.Error!;
            return false;
        }
    }

    public bool TryGetSuccess([MaybeNullWhen(false)] out T value)
    {
        if (this.Mode == ResultMode.Success)
        {
            value = this.Value!;
            return true;
        }
        else
        {
            value = default;
            return false;
        }
    }

    public bool TryGetError([MaybeNullWhen(false)] out Exception error)
    {
        if (this.Mode == ResultMode.Error)
        {
            error = this.Error!;
            return true;
        }
        else
        {
            error = default;
            return false;
        }
    }

}
