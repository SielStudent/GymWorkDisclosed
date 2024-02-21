namespace GymWorkDisclosed.DTOs;

public class GymGoerDTO
{
    public Guid? Guid { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    
    public GymGoerDTO(Guid? guid, string name, string email)
    {
        Guid = guid;
        Name = name;
        Email = email;
    }
}