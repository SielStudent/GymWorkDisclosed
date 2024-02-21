using BusinessLogic.Classes;

namespace DAL.DBO;

public class WorkoutEntity
{
    public Guid Id { get;  set; } = Guid.NewGuid();
    public DateTime Date { get; set; }
    public int Time { get; set; }
    public Guid GymGoerId { get; set; }
    public GymGoerEntity GymGoerEntity { get; set; }
    public List<SetEntity> Sets { get; set; } = new List<SetEntity>();
    public Guid ExerciseId { get; set; }
    public ExerciseEntity ExerciseEntity { get; set; }
  
    public Workout ToWorkout(List<MuscleGroupEntity> muscleGroupEntity)
    {
        List<Set> sets = new List<Set>();
        foreach (SetEntity setEntity in Sets)
        {
            sets.Add(setEntity.ToSet());
        }
        return new Workout(Id, DateOnly.FromDateTime(Date), Time, sets, ExerciseEntity.ToExercise(muscleGroupEntity));
    }
}