using Interfaces.DBModel;
using Interfaces.DBModel.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace UniversalAPI.Tests
{
    public static class MockContext
    {
        /* Emulate API DB with data */
        public static APIRequestContext GetTestAPIContext()
        {
            /* Initialize APIRequestList table */
            var Requests = new List<Request>
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
            }.AsQueryable();

            var mockSetRequests = new Mock<DbSet<Request>>();

            mockSetRequests.As<IQueryable<Request>>().Setup(m => m.Provider).Returns(Requests.Provider);
            mockSetRequests.As<IQueryable<Request>>().Setup(m => m.Expression).Returns(Requests.Expression);
            mockSetRequests.As<IQueryable<Request>>().Setup(m => m.ElementType).Returns(Requests.ElementType);
            mockSetRequests.As<IQueryable<Request>>().Setup(m => m.GetEnumerator()).Returns(Requests.GetEnumerator);

            /* Create and setting context */
            var mockContext = new Mock<APIRequestContext>();

            mockContext.Setup(t => t.APIRequests).Returns(mockSetRequests.Object);
            mockContext.Setup(t => t.Set<Request>()).Returns(mockSetRequests.Object);

            return mockContext.Object;
        }

        /* Emulate Data DB with data */
        public static DataContext GetTestDataContext()
        {
            /* Initialize TokenBalance table */
            var tokenBalance = new List<TokenBalance>
            {
                new TokenBalance { Id = 1, Token = "ADH", Amount = "400", Owner = "0x1a01ee5577c9d69c35a77496565b1bc95588b521" },
                new TokenBalance { Id = 2, Token = "Poolz", Amount = "300", Owner = "0x2a01ee5557c9d69c35577496555b1bc95558b552" },
                new TokenBalance { Id = 3, Token = "ETH", Amount = "200", Owner = "0x3a31ee5557c9369c35573496555b1bc93553b553" },
                new TokenBalance { Id = 4, Token = "BTH", Amount = "100", Owner = "0x4a71ee5577c9d79c37577496555b1bc95558b554" }
            }.AsQueryable();

            var mockSetTokenBalance = new Mock<DbSet<TokenBalance>>();

            mockSetTokenBalance.As<IQueryable<TokenBalance>>().Setup(m => m.Provider).Returns(tokenBalance.Provider);
            mockSetTokenBalance.As<IQueryable<TokenBalance>>().Setup(m => m.Expression).Returns(tokenBalance.Expression);
            mockSetTokenBalance.As<IQueryable<TokenBalance>>().Setup(m => m.ElementType).Returns(tokenBalance.ElementType);
            mockSetTokenBalance.As<IQueryable<TokenBalance>>().Setup(m => m.GetEnumerator()).Returns(tokenBalance.GetEnumerator);

            /* Initialize Wallet table */
            var wallets = new List<Wallet>
            {
                new Wallet { Id = 1, Owner = "0x1a01ee5577c9d69c35a77496565b1bc95588b521" },
                new Wallet { Id = 2, Owner = "0x2a01ee5557c9d69c35577496555b1bc95558b552" },
                new Wallet { Id = 3, Owner = "0x3a31ee5557c9369c35573496555b1bc93553b553" },
                new Wallet { Id = 4, Owner = "0x4a71ee5577c9d79c37577496555b1bc95558b554" }
            }.AsQueryable();

            var mockSetWallet = new Mock<DbSet<Wallet>>();

            mockSetWallet.As<IQueryable<Wallet>>().Setup(m => m.Provider).Returns(wallets.Provider);
            mockSetWallet.As<IQueryable<Wallet>>().Setup(m => m.Expression).Returns(wallets.Expression);
            mockSetWallet.As<IQueryable<Wallet>>().Setup(m => m.ElementType).Returns(wallets.ElementType);
            mockSetWallet.As<IQueryable<Wallet>>().Setup(m => m.GetEnumerator()).Returns(wallets.GetEnumerator);

            /* Initialize LeaderBoard table */
            var leaderBoards = new List<LeaderBoard>
            {
                new LeaderBoard { Id = 1, Rank = "1", Owner = "0x1a01ee5577c9d69c35a77496565b1bc95588b521", Amount = "750.505823765680934368"},
                new LeaderBoard { Id = 2, Rank = "2", Owner = "0x2a01ee5557c9d69c35577496555b1bc95558b552", Amount = "251.795264077704686136"},
                new LeaderBoard { Id = 3, Rank = "3", Owner = "0x3a31ee5557c9369c35573496555b1bc93553b553", Amount = "250.02109769151781894"},
                new LeaderBoard { Id = 4, Rank = "4", Owner = "0x4a71ee5577c9d79c37577496555b1bc95558b554", Amount = "233.279855562249360519"}
            }.AsQueryable();

            var mockSetLeaderBoards = new Mock<DbSet<LeaderBoard>>();

            mockSetLeaderBoards.As<IQueryable<LeaderBoard>>().Setup(m => m.Provider).Returns(leaderBoards.Provider);
            mockSetLeaderBoards.As<IQueryable<LeaderBoard>>().Setup(m => m.Expression).Returns(leaderBoards.Expression);
            mockSetLeaderBoards.As<IQueryable<LeaderBoard>>().Setup(m => m.ElementType).Returns(leaderBoards.ElementType);
            mockSetLeaderBoards.As<IQueryable<LeaderBoard>>().Setup(m => m.GetEnumerator()).Returns(leaderBoards.GetEnumerator);

            /* Initialize SignUp table */
            var signUp = new List<SignUp>
            {
                new SignUp { Id = 1, Address = "0x1a01ee5577c9d69c35a77496565b1bc95588b521", PoolId = 1},
                new SignUp { Id = 2, Address = "0x2a01ee5557c9d69c35577496555b1bc95558b552", PoolId = 2},
                new SignUp { Id = 3, Address = "0x3a31ee5557c9369c35573496555b1bc93553b553", PoolId = 3},
                new SignUp { Id = 4, Address = "0x4a71ee5577c9d79c37577496555b1bc95558b554", PoolId = 4}
            }.AsQueryable();

            var mockSetSignUp = new Mock<DbSet<SignUp>>();

            mockSetSignUp.As<IQueryable<SignUp>>().Setup(m => m.Provider).Returns(signUp.Provider);
            mockSetSignUp.As<IQueryable<SignUp>>().Setup(m => m.Expression).Returns(signUp.Expression);
            mockSetSignUp.As<IQueryable<SignUp>>().Setup(m => m.ElementType).Returns(signUp.ElementType);
            mockSetSignUp.As<IQueryable<SignUp>>().Setup(m => m.GetEnumerator()).Returns(signUp.GetEnumerator);

            /* Create and setting context */
            var mockContext = new Mock<DataContext>();

            mockContext.Setup(t => t.TokenBalances).Returns(mockSetTokenBalance.Object);
            mockContext.Setup(t => t.Set<TokenBalance>()).Returns(mockSetTokenBalance.Object);

            mockContext.Setup(t => t.Wallets).Returns(mockSetWallet.Object);
            mockContext.Setup(t => t.Set<Wallet>()).Returns(mockSetWallet.Object);

            mockContext.Setup(t => t.LeaderBoard).Returns(mockSetLeaderBoards.Object);
            mockContext.Setup(t => t.Set<LeaderBoard>()).Returns(mockSetLeaderBoards.Object);

            mockContext.Setup(t => t.SignUp).Returns(mockSetSignUp.Object);
            mockContext.Setup(t => t.Set<SignUp>()).Returns(mockSetSignUp.Object);

            return mockContext.Object;
        }
    }
}
