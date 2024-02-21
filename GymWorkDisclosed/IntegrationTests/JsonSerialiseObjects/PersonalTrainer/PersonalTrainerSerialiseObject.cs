using IntegrationTests.JsonSerialiseObjects.GymGoer;

namespace IntegrationTests.JsonSerialiseObjects.PersonalTrainer;

internal class PersonalTrainerSerialiseObject
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<GymGoerSerialiseObject> GymGoers { get; set; }
}