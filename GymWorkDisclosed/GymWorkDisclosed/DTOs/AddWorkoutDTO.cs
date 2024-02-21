using BusinessLogic.Classes;

namespace GymWorkDisclosed.DTOs;

public class AddWorkoutDTO
{
    public int TimeInSeconds { get; set; }
    public Guid GymGoerId { get; set; }
    public List<SetDTO> Sets { get; set; }
    public Guid ExerciseId { get; set; }

    public AddWorkoutDTO(int timeInSeconds, Guid gymGoerId, Guid exerciseId, List<SetDTO> sets)
    {
        TimeInSeconds = timeInSeconds;
        GymGoerId = gymGoerId;
        Sets = sets;
        ExerciseId = exerciseId;
    }
}