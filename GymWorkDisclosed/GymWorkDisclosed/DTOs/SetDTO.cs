namespace GymWorkDisclosed.DTOs;

public class SetDTO
{
    public Guid? Guid { get; set; }
    public int Reps { get; set; }
    public int Weight { get; set; }
    public int TimeInSeconds { get; set; }

    public SetDTO(Guid? guid, int reps, int weight, int timeInSeconds)
    {
        Guid = guid;
        Reps = reps;
        Weight = weight;
        TimeInSeconds = timeInSeconds;
    }
}