namespace Brimborium.ReturnValue;


public interface IValueMeaning<T> {
    T Value { get; }
    ContextName Meaning { get; }
}

public record struct ValueMeaning<T>(
    T Value,
    ContextName Meaning 
    );