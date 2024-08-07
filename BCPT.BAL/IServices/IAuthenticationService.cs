using BCPT.ABSTACTION;

namespace BCPT.BAL
{
    public interface IAuthenticationService
    {
        Task<RegisterResponse> Register(RegisterRequest registerRequest);
        Task<LoginResponse> Login(LoginRequest loginRequest);

    }
}
