using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SpectraMaster.Utils
{
    public interface IFileUtil
    {
        Task<string> CopyToServerFileAsync(IFormFile file);
    }
}