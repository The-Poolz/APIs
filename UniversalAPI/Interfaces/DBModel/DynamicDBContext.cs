using Interfaces.DBModel.Models;
using Interfaces.Helpers;
using Microsoft.EntityFrameworkCore;
using System;

namespace Interfaces.DBModel
{
    public partial class DynamicDBContext : DbContext
    {
        public virtual DbSet<Wallet> Wallets { get; set; }
        public virtual DbSet<LeaderBoard> LeaderBoard { get; set; }
        public virtual DbSet<SignUp> SignUp { get; set; }

        public DynamicDBContext() { }
        public DynamicDBContext(DbContextOptions options) { }
        public DynamicDBContext(DbContextOptions<DynamicDBContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            OnModelCreatingPartial(modelBuilder);

        public void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wallet>().HasData(new Wallet[]
            {
                new Wallet { Id = 1, Owner = "0x1a01ee5577c9d69c35a77496565b1bc95588b521" },
                new Wallet { Id = 2, Owner = "0x2a01ee5557c9d69c35577496555b1bc95558b552" },
                new Wallet { Id = 3, Owner = "0x3a31ee5557c9369c35573496555b1bc93553b553" },
                new Wallet { Id = 4, Owner = "0x4a71ee5577c9d79c37577496555b1bc95558b554" }
            });

            modelBuilder.Entity<LeaderBoard>().HasData(new LeaderBoard[]
            {
                new LeaderBoard { Id = 1, Rank = 1, Owner = "0x1a01ee5577c9d69c35a77496565b1bc95588b521", Amount = Convert.ToDecimal(750.505823765680934368)},
                new LeaderBoard { Id = 2, Rank = 2, Owner = "0x2a01ee5557c9d69c35577496555b1bc95558b552", Amount = Convert.ToDecimal(251.795264077704686136)},
                new LeaderBoard { Id = 3, Rank = 3, Owner = "0x3a31ee5557c9369c35573496555b1bc93553b553", Amount = Convert.ToDecimal(250.02109769151781894)},
                new LeaderBoard { Id = 4, Rank = 4, Owner = "0x4a71ee5577c9d79c37577496555b1bc95558b554", Amount = Convert.ToDecimal(233.279855562249360519)}
            });

            modelBuilder.Entity<SignUp>().HasData(new SignUp[]
            {
                new SignUp { Id = 1, Address = "0x1a01ee5577c9d69c35a77496565b1bc95588b521", PoolId = 1},
                new SignUp { Id = 2, Address = "0x2a01ee5557c9d69c35577496555b1bc95558b552", PoolId = 2},
                new SignUp { Id = 3, Address = "0x3a31ee5557c9369c35573496555b1bc93553b553", PoolId = 3},
                new SignUp { Id = 4, Address = "0x4a71ee5577c9d79c37577496555b1bc95558b554", PoolId = 4}
            });
        }
    }
}
