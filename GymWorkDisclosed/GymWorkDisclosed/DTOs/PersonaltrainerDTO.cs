using BusinessLogic.Classes;

namespace GymWorkDisclosed.DTOs;

public class PersonaltrainerDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<GymGoerWorkoutsDTO> GymGoers { get; set; }
    
    public PersonaltrainerDTO(Guid id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
        GymGoers = new List<GymGoerWorkoutsDTO>();
    }
}