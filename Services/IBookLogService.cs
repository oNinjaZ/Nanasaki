using System.Threading.Tasks;
using Nanasaki.Models;

namespace Nanasaki.Services;

public interface IBookLogService
{
    public Task<bool> CreateAsync(BookLog bookLog);
    public Task<bool> DeleteAsync(string id);
    
}