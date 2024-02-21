namespace BusinessLogic.Classes;

public class GymGoer
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public List<Workout> Workouts { get; private set; }
    
    public GymGoer(string name, string email)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Workouts = new List<Workout>();
    }
    public GymGoer(Guid id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
        Workouts = new List<Workout>();
    }

    public GymGoer(Guid id)
    {
        Id = id;
    }
    public void AddWorkout(Workout workout)
    {
        Workouts.Add(workout);
    }
}