namespace BusinessLogic.Classes;

public class PersonalTrainer
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    
    public List<GymGoer> GymGoers { get; private set; }
    
    public PersonalTrainer(Guid id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
        GymGoers = new List<GymGoer>();
    }
    
    public void AddGymGoer(GymGoer gymGoer)
    {
        GymGoers.Add(gymGoer);
    }
}