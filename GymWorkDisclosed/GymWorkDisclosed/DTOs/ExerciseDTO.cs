using BusinessLogic.Classes;

namespace GymWorkDisclosed.DTOs;

public class ExerciseDTO
{
    public Guid? Guid { get; set; }
    public string Name { get; set; }
    public List<MuscleGroupDTO> MuscleGroups { get; set; }
    public List<WorkoutDTO> Workouts { get; set; }
    
    public ExerciseDTO(Guid guid, string? name)
    {
        Guid = guid;
        Name = name;
        MuscleGroups = new List<MuscleGroupDTO>();
        Workouts = new List<WorkoutDTO>();
    }
}