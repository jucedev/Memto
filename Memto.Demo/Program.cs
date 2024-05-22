var originator = new Memto.Originator<string>();
var caretaker = new Memto.Caretaker<string>(originator);

originator.SetState("First state");

caretaker.SaveState(originator.SaveState());
originator.SetState("Second state");

caretaker.SaveState(originator.SaveState());
originator.SetState("Third state");

Console.WriteLine("Current state: " + originator.State);

if (caretaker.CanUndo)
{
    originator.GetState(caretaker.Undo());
    Console.WriteLine("Restored state: " + originator.State);
}

if (caretaker.CanUndo)
{
    originator.GetState(caretaker.Undo());
    Console.WriteLine("Restored state: " + originator.State);
}

if (caretaker.CanRedo)
{
    originator.GetState(caretaker.Redo());
    Console.WriteLine("Restored state: " + originator.State);
}

if (caretaker.CanRedo)
{
    originator.GetState(caretaker.Redo());
    Console.WriteLine("Restored state: " + originator.State);
}
