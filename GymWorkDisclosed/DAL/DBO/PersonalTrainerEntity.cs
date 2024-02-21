using System.ComponentModel.DataAnnotations;
using BusinessLogic.Classes;

namespace DAL.DBO;

public class PersonalTrainerEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Email { get; set; }

    public List<GymGoerEntity> GymGoers { get; set; } = new List<GymGoerEntity>();
    
    public PersonalTrainer ToPersonalTrainer()
    {
        PersonalTrainer personalTrainer = new PersonalTrainer(Id, Name, Email);
        if (GymGoers.Count is not 0)
        {
            foreach (GymGoerEntity gymGoerEntity in GymGoers)
            {
                personalTrainer.AddGymGoer(gymGoerEntity.ToGymGoer());
            }
        }
        return personalTrainer;
    }
}