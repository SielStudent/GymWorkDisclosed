namespace GymWorkDisclosed.DTOs;

public class PersonaltrainerAuthDTO
{
    public Guid? Guid { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    
    public PersonaltrainerAuthDTO(Guid? guid, string name, string email)
    {
        Guid = guid;
        Name = name;
        Email = email;
    }
}