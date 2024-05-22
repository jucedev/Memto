namespace Memto;

public class Memento<T>(T state)
{
    public T State { get; } = state;
}