using BusinessLogic.Classes;
using DAL.DBO;
using Microsoft.EntityFrameworkCore;
using PersonalTrainerService;

namespace DAL.Repositories;

public class TrainerRepository: ITrainerRepository
{
    private readonly GymWorkoutDisclosedDBContext _context;
    
    public TrainerRepository(GymWorkoutDisclosedDBContext context)
    {
        _context = context;
    }
    
    public PersonalTrainer GetTrainer(Guid id)
    {
        PersonalTrainerEntity personalTrainerEntity = _context.personalTrainers
            .Include(pt => pt.GymGoers)
            .ThenInclude(gg => gg.Workouts)
            .ThenInclude(w => w.ExerciseEntity)
            .ThenInclude(e => e.MuscleGroupExerciseEntities)
            .ThenInclude(mge => mge.MuscleGroupEntity)
            .ThenInclude(mge => mge.BodyPartEntity)
            .Include(pt => pt.GymGoers)
            .ThenInclude(gg => gg.Workouts)
            .ThenInclude(w => w.Sets)
            .FirstOrDefault(pt => pt.Id == id);
        return personalTrainerEntity?.ToPersonalTrainer();
    }
    public PersonalTrainer GetTrainerByEmail(string email)
    {
        PersonalTrainerEntity personalTrainerEntity = _context.personalTrainers
            .FirstOrDefault(pt => pt.Email == email);
        return personalTrainerEntity?.ToPersonalTrainer();
    }
}