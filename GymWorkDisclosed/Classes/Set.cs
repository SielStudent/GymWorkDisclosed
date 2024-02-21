namespace BusinessLogic.Classes;

public class Set
{
    public Guid? Id { get; private set; }
    public int Reps { get; private set; }
    public int Weight { get; private set; }
    public int Time { get; private set; }
    
    public Set(Guid? id, int reps, int weight, int time)
    {
        Id = id;
        Reps = reps;
        Weight = weight;
        Time = time;
    }
    
}