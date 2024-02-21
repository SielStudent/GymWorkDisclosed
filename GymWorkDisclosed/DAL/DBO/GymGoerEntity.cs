using System.ComponentModel.DataAnnotations;
using BusinessLogic.Classes;

namespace DAL.DBO;

public class GymGoerEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Email { get; set; }
    public List<WorkoutEntity> Workouts { get; set; } = new List<WorkoutEntity>();
    public PersonalTrainerEntity? PersonalTrainerEntity { get; set; }
    public Guid? PersonalTrainerEntityId { get; set; }
    
    public GymGoer ToGymGoer()
    {
        
        GymGoer gymGoer = new GymGoer(Id, Name, Email);
        if (Workouts.Count is not 0)
        {
            foreach (WorkoutEntity workoutEntity in Workouts)
            {
                gymGoer.AddWorkout(workoutEntity.ToWorkout(workoutEntity.ExerciseEntity.MuscleGroupExerciseEntities.Select(mge => mge.MuscleGroupEntity).ToList()));
            }
        }
        
        return gymGoer;
    }
}