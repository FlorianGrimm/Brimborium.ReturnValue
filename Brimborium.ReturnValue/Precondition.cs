namespace Brimborium.ReturnValue;

public interface IPreconditionValue
{
}

public abstract class Precondition<TValue>
    where TValue : IPreconditionValue
{
    protected Precondition() {
    }
}
