using Interfaces.DBModel.Models;
using Microsoft.EntityFrameworkCore;

namespace Interfaces.DBModel
{
    /// <summary>
    /// Implement the interface in your context for the UniversalAPI to work.
    /// </summary>
    public interface IUniversalContext
    {
        DbSet<APIRequestList> APIRequestList { get; }
    }
}
