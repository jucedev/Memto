namespace Memto;

public class Originator<T>
{
    public T State { get; private set; } = default!;

    public void SetState(T state)
    {
        State = state;
    }

    public Memento<T> SaveStateToMemento()
    {
        return new Memento<T>(State);
    }

    public void GetStateFromMemento(Memento<T> memento)
    {
        State = memento.State;
    }
}