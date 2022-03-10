using Interfaces.DBModel.Models;
using Microsoft.EntityFrameworkCore;

namespace Interfaces.DBModel
{
    public interface IUniversalContext
    {
        DbSet<APIRequestList> APIRequestList { get; }
    }
}
