namespace Memto;

public class Caretaker<T>(Originator<T> originator)
{
    private readonly Stack<Memento<T>> _undoMementos = new();
    private readonly Stack<Memento<T>> _redoMementos = new();
    private readonly Originator<T> _originator = originator;
    
    public bool CanUndo => _undoMementos.Count > 0;
    public bool CanRedo => _redoMementos.Count > 0;

    public void SaveState(Memento<T> memento)
    {
        _undoMementos.Push(memento);
        
        // new state arrived, clear the redo stack.
        _redoMementos.Clear();
    }

    public Memento<T> Undo()
    {
        if (_undoMementos.Count <= 0)
        {
            throw new InvalidOperationException("No states to undo.");
        }
        
        _redoMementos.Push(_originator.SaveState());
        var memento = _undoMementos.Pop();
        return memento;
    }

    public Memento<T> Redo()
    {
        if (_redoMementos.Count <= 0)
        {
            throw new InvalidOperationException("No states to redo.");
        }

        _undoMementos.Push(_originator.SaveState());
        var memento = _redoMementos.Pop();
        return memento;
    }
}