using BusinessLogic.Classes;

namespace BusinessLogic.Services.Workout;

public interface IWorkoutRepository
{
    public List<Classes.Workout> GetWorkoutsByGymGoerId(Guid id);
    public Classes.Workout AddWorkout(Classes.Workout workout, Guid gymGoerId);
}