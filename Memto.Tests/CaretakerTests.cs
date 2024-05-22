namespace Memto.Tests;

public class CaretakerTests
{
    [Fact]
    public void SaveState_AddsMementoToUndoStack()
    {
        var originator = new Originator<string>();
        var caretaker = new Caretaker<string>(originator);
        
        originator.SetState("test 1");
        caretaker.SaveState(originator.SaveState());
        
        Assert.True(caretaker.CanUndo);
    }
    
    [Fact]
    public void SaveState_Clears_RedoStack()
    {
        var originator = new Originator<string>();
        var caretaker = new Caretaker<string>(originator);
        
        originator.SetState("test 1");
        caretaker.SaveState(originator.SaveState());
        
        // Add "test 1" to the redo stack
        originator.GetState(caretaker.Undo());
        
        // add a new state
        originator.SetState("test 2");
        caretaker.SaveState(originator.SaveState());
        
        Assert.False(caretaker.CanRedo);
    }

    [Fact]
    public void Undo_Restores_PreviousState()
    {
        var originator = new Originator<string>();
        var caretaker = new Caretaker<string>(originator);

        const string originalState = "test 1";
        originator.SetState(originalState);
        caretaker.SaveState(originator.SaveState());
        
        // add a new state
        originator.SetState("test 2");
        
        originator.GetState(caretaker.Undo());
        
        Assert.Equal(originalState, originator.State);
    }

    [Fact]
    public void Undo_AddsCurrentState_ToRedoStack()
    {
        var originator = new Originator<string>();
        var caretaker = new Caretaker<string>(originator);

        originator.SetState("test 1");
        caretaker.SaveState(originator.SaveState());
        
        originator.SetState("test 2");
        
        originator.GetState(caretaker.Undo());
        
        Assert.True(caretaker.CanRedo);
    }

    [Fact]
    public void Redo_Restores_NextState()
    {
        var originator = new Originator<string>();
        var caretaker = new Caretaker<string>(originator);

        originator.SetState("test 1");
        caretaker.SaveState(originator.SaveState());
        
        // add a new state
        const string secondState = "test 2";
        originator.SetState(secondState);
        
        originator.GetState(caretaker.Undo());
        originator.GetState(caretaker.Redo());
        
        Assert.Equal(secondState, originator.State);
    }

    [Fact]
    public void Redo_AddsCurrentState_ToUndoStack()
    {
        var originator = new Originator<string>();
        var caretaker = new Caretaker<string>(originator);

        originator.SetState("test 1");
        caretaker.SaveState(originator.SaveState());
        
        originator.SetState("test 2");
        
        originator.GetState(caretaker.Undo());
        originator.GetState(caretaker.Redo());
        
        Assert.True(caretaker.CanUndo);
    }

    [Fact]
    public void Undo_ThrowsException_WhenNothingToUndo()
    {
        var originator = new Originator<string>();
        var caretaker = new Caretaker<string>(originator);

        Assert.Throws<InvalidOperationException>(() => caretaker.Undo());
    }
    
    [Fact]
    public void Redo_ThrowsException_WhenNothingToRedo()
    {
        var originator = new Originator<string>();
        var caretaker = new Caretaker<string>(originator);

        Assert.Throws<InvalidOperationException>(() => caretaker.Redo());
    }
}