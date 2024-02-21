using BusinessLogic.Classes;

namespace BusinessLogic.Services.AuthService;

public interface IAuthRepository
{
    public GymGoer GetGymGoerByEmail(string email);
}