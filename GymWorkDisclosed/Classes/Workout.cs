namespace BusinessLogic.Classes;

public class Workout
{
    public Guid Id { get; private set; }
    public DateOnly Date { get; private set; }
    public int Time { get; private set; }
    public GymGoer? GymGoer { get; private set; }
    public List<Set> Sets { get; private set; }
    public Exercise? Exercise { get; private set; }
    
    public Workout(Guid id, DateOnly date, int time, GymGoer gymGoer, Exercise exercise)
    {
        Id = id;
        Date = date;
        Time = time;
        GymGoer = gymGoer;
        Exercise = exercise;
        Sets = new List<Set>();
    }
    public Workout(Guid id, DateOnly date, int time, List<Set> sets, Exercise exercise)
    {
        Id = id;
        Date = date;
        Time = time;
        Sets = sets;
        Exercise = exercise;
    }
    public Workout(DateOnly date, int time, GymGoer gymGoer, Exercise exercise)
    {
        Date = date;
        Time = time;
        GymGoer = gymGoer;
        Exercise = exercise;
        Sets = new List<Set>();
    }

    public Workout()
    {
        Sets = new List<Set>();
    }
}