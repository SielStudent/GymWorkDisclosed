namespace BusinessLogic.Classes;

public class Exercise
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public List<MuscleGroup> MuscleGroups { get; private set; }
    public List<Workout> Workouts { get; private set; }
    public Exercise(Guid id, string name)
    {
        Id = id;
        Name = name;
        MuscleGroups = new List<MuscleGroup>();
        Workouts = new List<Workout>();
    }
    public Exercise(Guid id, string name, List<MuscleGroup> muscleGroups)
    {
        Id = id;
        Name = name;
        MuscleGroups = muscleGroups;
        Workouts = new List<Workout>();
    }

    public Exercise(Guid guid)
    {
        Id = guid;
    }
    public void AddMuscleGroup(MuscleGroup muscleGroup)
    {
        MuscleGroups.Add(muscleGroup);
    }
}