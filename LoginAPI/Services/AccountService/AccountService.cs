namespace LoginAPI.Services.AccountService;

public partial class AccountService : IAccountService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserHashDataRepository _userHashDataRepository;
    private readonly IJWTService _jwtService;


    public AccountService(
        IUserRepository userRepository,
        IUserHashDataRepository userHashDataRepository,
        IJWTService jwtService
    )
    {
        _userRepository = userRepository;
        _userHashDataRepository = userHashDataRepository;
        _jwtService = jwtService;
    }

    public Task<User> ChangePassword(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<User>> GetUserInfo(Guid id)
    {
        var result = await _userRepository.GetByIdAsync(id);
        return new ServiceResponse<User>
        {
            StatusCode = 200,
            Data = result,
            Message = "User found"
        };
    }

    public async Task<ServiceResponse<LoginDto>> Login(LoginDto user)
    {
        var dBUserData = await _userRepository.GetUserByEmailOrUserName(user.Identifier);
        
        if(dBUserData == null)
        {
            return new ServiceResponse<LoginDto>
            {
                StatusCode = 404,
                Data = null,
                Message = "User not found"
            };
        }

        var result = BCrypt.Net.BCrypt.Verify(
            user.Password,
            dBUserData.Password);


        // TODO: 接下來新增JWT Token 
        user.JwtToken = _jwtService.GenerateToken(dBUserData.UserId.ToString());

        return new ServiceResponse<LoginDto>
        {
            StatusCode = 200,
            Data = user,
            Message = result ? "Login Sucess & Jwt Access" : "Login failed"
        };
    }

    public async Task<ServiceResponse<bool>> Logout(LogoutDto request)
    {
        await _jwtService.AddToken2BlackList(request);

        return new ServiceResponse<bool>
        {
            StatusCode = 200,
            Data = true,
            Message = "Logout success"
        };
    }

    public async Task<ServiceResponse<User>> RegisterUser(User user)
    {
        string userSalt = BCrypt.Net.BCrypt.GenerateSalt(10);
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, userSalt);

        await _userRepository.AddAsync(user);
        await _userHashDataRepository.AddAsync(

            new UserHashData { UserId = user.UserId, Salt = userSalt }
        );


        return new ServiceResponse<User>
        {
            StatusCode = 200,
            Data = user,
            Message = "User created"
        };
    }

    public async Task<ServiceResponse<User>> ValidateUser(string Identifier)
    {
        var result = await _userRepository.GetUserByEmailOrUserName(Identifier);
        if (result != null)
        {
            return new ServiceResponse<User>
            {
                StatusCode = 200,
                Data = result,
                Message = "User found"
            };
        }
        else
        {
            return new ServiceResponse<User>
            {
                StatusCode = 200,
                Data = null,
                Message = "User not found"
            };
        }
    }
}
