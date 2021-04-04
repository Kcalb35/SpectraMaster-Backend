using SpectraMaster.Controllers;

namespace SpectraMaster.Models
{
    public interface IAuthentiacteService
    {
        bool IsAuthenticate(LoginReq req,out string jwt);
    }
}