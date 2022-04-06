using Microsoft.EntityFrameworkCore;
using UniversalAPI;

namespace Interfaces.DBModel
{
    public class APIContext : UniversalAPI.APIContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Request>().HasData(new Request[]
            {
                new Request {
                    Id = 1,
                    Name = "mysignup",
                    SelectedTables = "SignUp, LeaderBoard",
                    SelectedColumns = "SignUp.PoolId, LeaderBoard.Rank, LeaderBoard.Owner, LeaderBoard.Amount",
                    JoinCondition = "SignUp.Address = LeaderBoard.Owner"
                },
                new Request {
                    Id = 2,
                    Name = "wallet",
                    SelectedTables = "Wallets",
                    SelectedColumns = "*"
                },
                new Request {
                    Id = 3,
                    Name = "tokenbalanse",
                    SelectedTables = "TokenBalances",
                    SelectedColumns = "Token, Owner, Amount"
                }
            });
        }
    }
}
