using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FoodFIghtAdmin.Models
{
    public partial class FoodFightContext : DbContext
    {
        public FoodFightContext()
        {
        }

        public FoodFightContext(DbContextOptions<FoodFightContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AcceptedRestaurant> AcceptedRestaurants { get; set; }
        public virtual DbSet<BlockedRestaurant> BlockedRestaurants { get; set; }
        public virtual DbSet<BlockedUser> BlockedUsers { get; set; }
        public virtual DbSet<ConnectedUser> ConnectedUsers { get; set; }
        public virtual DbSet<FavoriteRestaurant> FavoriteRestaurants { get; set; }
        public virtual DbSet<MatchSession> MatchSessions { get; set; }
        public virtual DbSet<MatchedRestaurant> MatchedRestaurants { get; set; }
        public virtual DbSet<RejectedRestaurant> RejectedRestaurants { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<SwipeList> SwipeLists { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserSetting> UserSettings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=AppContextConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AcceptedRestaurant>(entity =>
            {
                entity.Property(e => e.AcceptedRestaurantId).ValueGeneratedNever();

                entity.HasOne(d => d.SwipeList)
                    .WithMany(p => p.AcceptedRestaurants)
                    .HasForeignKey(d => d.SwipeListId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AcceptedRestaurant_SwipeListID");
            });

            modelBuilder.Entity<BlockedRestaurant>(entity =>
            {
                entity.Property(e => e.BlockedRestaurantId).ValueGeneratedNever();

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.BlockedRestaurants)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BlockedRestaurants_RestaurantID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BlockedRestaurants)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BlockedRestaurants_UserID");
            });

            modelBuilder.Entity<BlockedUser>(entity =>
            {
                entity.Property(e => e.BlockUserId).ValueGeneratedNever();

                entity.HasOne(d => d.BaseUser)
                    .WithMany(p => p.BlockedUserBaseUsers)
                    .HasForeignKey(d => d.BaseUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BlockedUsersBase_UserID");

                entity.HasOne(d => d.BlockedUserNavigation)
                    .WithMany(p => p.BlockedUserBlockedUserNavigations)
                    .HasForeignKey(d => d.BlockedUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BlockedUsersID_UserID");
            });

            modelBuilder.Entity<ConnectedUser>(entity =>
            {
                entity.Property(e => e.ConnectedUserId).ValueGeneratedNever();

                entity.HasOne(d => d.BaseUser)
                    .WithMany(p => p.ConnectedUserBaseUsers)
                    .HasForeignKey(d => d.BaseUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ConnectedUsersBase_UserID");

                entity.HasOne(d => d.FriendUser)
                    .WithMany(p => p.ConnectedUserFriendUsers)
                    .HasForeignKey(d => d.FriendUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ConnectedUsersFriend_UserID");
            });

            modelBuilder.Entity<FavoriteRestaurant>(entity =>
            {
                entity.Property(e => e.FavoriteRestaurantId).ValueGeneratedNever();

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.FavoriteRestaurants)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FavoriteRestaurant_RestaurantID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FavoriteRestaurants)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FavoriteRestaurant_UserID");
            });

            modelBuilder.Entity<MatchSession>(entity =>
            {
                entity.Property(e => e.MatchSessionId).ValueGeneratedNever();

                entity.HasOne(d => d.ConnectedUser)
                    .WithMany(p => p.MatchSessions)
                    .HasForeignKey(d => d.ConnectedUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MatchSession_ConnectedID");
            });

            modelBuilder.Entity<MatchedRestaurant>(entity =>
            {
                entity.Property(e => e.MatchRestaurantId).ValueGeneratedNever();

                entity.HasOne(d => d.AcceptedRestaurant)
                    .WithMany(p => p.MatchedRestaurants)
                    .HasForeignKey(d => d.AcceptedRestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MatchedRestaurant_AcceptedRestaurantID");
            });

            modelBuilder.Entity<RejectedRestaurant>(entity =>
            {
                entity.Property(e => e.RejectedRestaurantId).ValueGeneratedNever();

                entity.HasOne(d => d.SwipeList)
                    .WithMany(p => p.RejectedRestaurants)
                    .HasForeignKey(d => d.SwipeListId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RejectedRestaurant_SwipeListID");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.Property(e => e.ZipCode)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.Property(e => e.SettingsId).ValueGeneratedNever();
            });

            modelBuilder.Entity<SwipeList>(entity =>
            {
                entity.Property(e => e.SwipeListId).ValueGeneratedNever();

                entity.HasOne(d => d.MatchSession)
                    .WithMany(p => p.SwipeLists)
                    .HasForeignKey(d => d.MatchSessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SwipeList_MatchSessionId");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.SwipeLists)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SwipeList_RestaurantID");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.ZipCode)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<UserSetting>(entity =>
            {
                entity.Property(e => e.UserSettingsId).ValueGeneratedNever();

                entity.HasOne(d => d.Settings)
                    .WithMany(p => p.UserSettings)
                    .HasForeignKey(d => d.SettingsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSettings_SettingsID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSettings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSettings_UserID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
