namespace Memto;

public class Caretaker<T>
{
    private readonly Stack<Memento<T>> _mementos = new();

    public bool IsEmpty => _mementos.Count == 0;

    public void SaveState(Memento<T> memento)
    {
        _mementos.Push(memento);
    }

    public Memento<T> RestoreState()
    {
        return _mementos.Pop();
    }
}