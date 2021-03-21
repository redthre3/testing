using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FoodFIghtAdmin.Models
{
    [Table("BlockedRestaurant")]
    [Index(nameof(RestaurantId), Name = "fkIdx_BlockedRestaurants_RestaurantID")]
    [Index(nameof(UserId), Name = "fkIdx_BlockedRestaurants_UserID")]
    public partial class BlockedRestaurant
    {
        [Key]
        [Column("BlockedRestaurantID")]
        public Guid BlockedRestaurantId { get; set; }
        [Required]
        [Column("RestaurantID")]
        [StringLength(255)]
        public string RestaurantId { get; set; }
        [Column("UserID")]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(RestaurantId))]
        [InverseProperty("BlockedRestaurants")]
        public virtual Restaurant Restaurant { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("BlockedRestaurants")]
        public virtual User User { get; set; }
    }
}
