var originator = new Memto.Originator<string>();
var caretaker = new Memto.Caretaker<string>();

originator.SetState("First");
caretaker.SaveState(originator.SaveStateToMemento());

originator.SetState("Second");
caretaker.SaveState(originator.SaveStateToMemento());

originator.SetState("Third");

Console.WriteLine("Current state: " + originator.State);

while (!caretaker.IsEmpty)
{
    originator.GetStateFromMemento(caretaker.RestoreState());
    Console.WriteLine("Restored state: " + originator.State);
}