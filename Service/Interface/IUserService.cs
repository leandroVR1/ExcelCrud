using System.Threading.Tasks;
using ExcelCrudMVC.Responses;

namespace ExcelCrudMVC.Services
{
    public interface IUserService
    {
        Task<Response<string>> Authenticate(string Email, string password);
    }
}
