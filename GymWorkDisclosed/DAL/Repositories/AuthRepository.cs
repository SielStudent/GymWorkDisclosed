using BusinessLogic.Services.AuthService;
using BusinessLogic.Classes;
using DAL.DBO;

namespace DAL.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly GymWorkoutDisclosedDBContext _context;

    public AuthRepository(GymWorkoutDisclosedDBContext context)
    {
        _context = context;
    }
    public GymGoer GetGymGoerByEmail(string email)
    {
        GymGoerEntity gymGoerEntity = _context.gymGoer.FirstOrDefault(gg => gg.Email == email);
        return gymGoerEntity?.ToGymGoer();
    }
}