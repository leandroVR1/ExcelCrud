using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ExcelCrudMVC.Data;
using ExcelCrudMVC.Responses;
using System.Threading.Tasks;

namespace ExcelCrudMVC.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;

        public UserService(ApplicationDbContext context, IConfiguration configuration, ILogger<UserService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

       public async Task<Response<string>> Authenticate(string email, string password)
{
    try
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            return new Response<string>("Email and password must be provided", null, false);
        }

        if (_context == null)
        {
            throw new InvalidOperationException("Database context is not initialized.");
        }

        if (_configuration == null)
        {
            throw new InvalidOperationException("Configuration is not initialized.");
        }

        var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);

        if (user == null || user.Password != password)
        {
            return new Response<string>("Invalid username or password", null, false);
        }

        if (_configuration["Jwt:Key"] == null)
        {
            throw new InvalidOperationException("Jwt:Key configuration missing.");
        }

        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role?.Name ?? throw new InvalidOperationException("User role is null"))
            }),
            Expires = DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:ExpirationHrs"] ?? "1")),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return new Response<string>("Login success", tokenString, true);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error in Authenticate method");
        return new Response<string>("Internal server error", null, false);
    }
}

    }
}
