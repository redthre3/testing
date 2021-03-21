using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FoodFIghtAdmin.Models
{
    [Index(nameof(RestaurantId), Name = "fkIdx_SwipeList_RestaurantID")]
    public partial class Restaurant
    {
        public Restaurant()
        {
            BlockedRestaurants = new HashSet<BlockedRestaurant>();
            FavoriteRestaurants = new HashSet<FavoriteRestaurant>();
            SwipeLists = new HashSet<SwipeList>();
        }

        [Key]
        [Column("RestaurantID")]
        [StringLength(255)]
        public string RestaurantId { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Street { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [StringLength(255)]
        public string City { get; set; }
        [StringLength(255)]
        public string State { get; set; }
        [StringLength(50)]
        public string Lat { get; set; }
        [StringLength(50)]
        public string Lng { get; set; }
        [StringLength(50)]
        public string OpenNow { get; set; }
        [StringLength(255)]
        public string Website { get; set; }
        public double? Rating { get; set; }
        [StringLength(10)]
        public string ZipCode { get; set; }
        [StringLength(100)]
        public string Photo { get; set; }

        [InverseProperty(nameof(BlockedRestaurant.Restaurant))]
        public virtual ICollection<BlockedRestaurant> BlockedRestaurants { get; set; }
        [InverseProperty(nameof(FavoriteRestaurant.Restaurant))]
        public virtual ICollection<FavoriteRestaurant> FavoriteRestaurants { get; set; }
        [InverseProperty(nameof(SwipeList.Restaurant))]
        public virtual ICollection<SwipeList> SwipeLists { get; set; }
    }
}
