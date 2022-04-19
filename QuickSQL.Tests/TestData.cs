﻿using System.Collections.Generic;

namespace QuickSQL.Tests
{
    public class TestData
    {
        public static IEnumerable<object[]> GetTestData()
        {
            var mysignupExpected = "[{\"PoolId\":3,\"Rank\":\"3\",\"Owner\":\"0x3a31ee5557c9369c35573496555b1bc93553b553\",\"Amount\":\"250.02109769151781894\"}]";
            var walletExpected = "[{\"Id\":3,\"Owner\":\"0x3a31ee5557c9369c35573496555b1bc93553b553\"}]";
            var tokenBalanceExpected = "[{\"Token\":\"ADH\",\"Owner\":\"0x1a01ee5577c9d69c35a77496565b1bc95588b521\",\"Amount\":\"400\"},{\"Token\":\"Poolz\",\"Owner\":\"0x2a01ee5557c9d69c35577496555b1bc95558b552\",\"Amount\":\"300\"},{\"Token\":\"ETH\",\"Owner\":\"0x3a31ee5557c9369c35573496555b1bc93553b553\",\"Amount\":\"200\"},{\"Token\":\"BTH\",\"Owner\":\"0x4a71ee5577c9d79c37577496555b1bc95558b554\",\"Amount\":\"100\"}]";
            return new List<object[]>
            {
                new object[] {
                    new Request {
                        SelectedTables = "SignUp, LeaderBoard",
                        SelectedColumns = "SignUp.PoolId, LeaderBoard.Rank, LeaderBoard.Owner, LeaderBoard.Amount",
                        WhereCondition = "SignUp.Id = 3, SignUp.Address = '0x3a31ee5557c9369c35573496555b1bc93553b553'",
                        JoinCondition = "SignUp.Address = LeaderBoard.Owner"
                    },
                    mysignupExpected
                },
                new object[] {
                    new Request {
                        SelectedTables = "Wallets",
                        SelectedColumns = "*",
                        WhereCondition = "Id = 3, Owner = '0x3a31ee5557c9369c35573496555b1bc93553b553'"
                    },
                    walletExpected
                },
                new object[] {
                    new Request {
                        SelectedTables = "TokenBalances",
                        SelectedColumns = "Token, Owner, Amount"
                    },
                    tokenBalanceExpected
                }
            };
        }
    }
}