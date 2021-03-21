using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FoodFIghtAdmin.Models
{
    [Table("RejectedRestaurant")]
    [Index(nameof(SwipeListId), Name = "fkIdx_RejectedRestaurant_SwipeListID")]
    public partial class RejectedRestaurant
    {
        [Key]
        [Column("RejectedRestaurantID")]
        public Guid RejectedRestaurantId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateTime { get; set; }
        [Column("SwipeListID")]
        public Guid SwipeListId { get; set; }
        [Required]
        [Column("UserID")]
        [StringLength(50)]
        public string UserId { get; set; }

        [ForeignKey(nameof(SwipeListId))]
        [InverseProperty("RejectedRestaurants")]
        public virtual SwipeList SwipeList { get; set; }
    }
}
