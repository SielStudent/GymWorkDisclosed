using BusinessLogic.Classes;

namespace BusinessLogic.Services.AuthService;

public class AuthService
{
    private readonly IAuthRepository _authRepository;
    
    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }
    
    public GymGoer GetGymGoerByEmail(string email)
    {
        GymGoer? gymGoer = _authRepository.GetGymGoerByEmail(email);
        return gymGoer;
    }
}