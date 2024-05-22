namespace Memto.Tests;

public class OriginatorTests
{
    [Fact]
    public void SetState_Updates_State()
    {
        const string state = "state";
        var originator = new Originator<string>();

        originator.SetState(state);
        
        Assert.Equal(state, originator.State);
    }

    [Fact]
    public void SaveState_Returns_MementoWithCurrentState()
    {
        const string state = "state";
        var originator = new Originator<string>();
        
        originator.SetState(state);

        var memento = originator.SaveState();
        
        Assert.Equal(state, memento.State);
    }

    [Fact]
    public void GetState_Restores_StateFromMemento()
    {
        const string state1 = "state 1";
        const string state2 = "state 2";
        var originator = new Originator<string>();
        
        originator.SetState(state1);
        var memento = originator.SaveState();

        originator.SetState(state2);
        originator.GetState(memento);
        
        Assert.Equal(state1, originator.State);
    }
}