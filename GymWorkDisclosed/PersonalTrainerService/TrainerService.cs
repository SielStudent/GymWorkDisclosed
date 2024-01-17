using BusinessLogic.Classes;

namespace PersonalTrainerService;

public class TrainerService
{
    private readonly ITrainerRepository _trainerRepository;
    public TrainerService(ITrainerRepository trainerRepository)
    {
        _trainerRepository = trainerRepository;
    }
    public PersonalTrainer GetTrainer(Guid id)
    {
        return _trainerRepository.GetTrainer(id);
    }
    public PersonalTrainer GetTrainerByEmail(string email)
    {
        return _trainerRepository.GetTrainerByEmail(email);
    }
}