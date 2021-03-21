using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FoodFIghtAdmin.Models
{
    [Table("User")]
    public partial class User
    {
        public User()
        {
            BlockedRestaurants = new HashSet<BlockedRestaurant>();
            BlockedUserBaseUsers = new HashSet<BlockedUser>();
            BlockedUserBlockedUserNavigations = new HashSet<BlockedUser>();
            ConnectedUserBaseUsers = new HashSet<ConnectedUser>();
            ConnectedUserFriendUsers = new HashSet<ConnectedUser>();
            FavoriteRestaurants = new HashSet<FavoriteRestaurant>();
            UserSettings = new HashSet<UserSetting>();
        }

        [Key]
        [Column("UserID")]
        public Guid UserId { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(75)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Bio { get; set; }
        [StringLength(25)]
        public string Gender { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [Required]
        [StringLength(255)]
        public string Password { get; set; }
        [StringLength(50)]
        public string Lat { get; set; }
        [StringLength(50)]
        public string Lng { get; set; }
        [StringLength(255)]
        public string Facebook { get; set; }
        [StringLength(255)]
        public string Twitter { get; set; }
        [StringLength(255)]
        public string Instagram { get; set; }
        [StringLength(255)]
        public string Website { get; set; }
        [StringLength(255)]
        public string ProfilePicture { get; set; }
        [StringLength(255)]
        public string Street { get; set; }
        [StringLength(255)]
        public string City { get; set; }
        [StringLength(255)]
        public string State { get; set; }
        [StringLength(10)]
        public string ZipCode { get; set; }
        [StringLength(255)]
        public string Salt { get; set; }

        [InverseProperty(nameof(BlockedRestaurant.User))]
        public virtual ICollection<BlockedRestaurant> BlockedRestaurants { get; set; }
        [InverseProperty(nameof(BlockedUser.BaseUser))]
        public virtual ICollection<BlockedUser> BlockedUserBaseUsers { get; set; }
        [InverseProperty(nameof(BlockedUser.BlockedUserNavigation))]
        public virtual ICollection<BlockedUser> BlockedUserBlockedUserNavigations { get; set; }
        [InverseProperty(nameof(ConnectedUser.BaseUser))]
        public virtual ICollection<ConnectedUser> ConnectedUserBaseUsers { get; set; }
        [InverseProperty(nameof(ConnectedUser.FriendUser))]
        public virtual ICollection<ConnectedUser> ConnectedUserFriendUsers { get; set; }
        [InverseProperty(nameof(FavoriteRestaurant.User))]
        public virtual ICollection<FavoriteRestaurant> FavoriteRestaurants { get; set; }
        [InverseProperty(nameof(UserSetting.User))]
        public virtual ICollection<UserSetting> UserSettings { get; set; }
    }
}
