using IntegrationTests.JsonSerialiseObjects.Workouts;

namespace IntegrationTests.JsonSerialiseObjects.GymGoer;

internal class GymGoerSerialiseObject
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<WorkoutSerialiseObject> Workouts { get; set; }
}