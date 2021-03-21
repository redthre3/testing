using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FoodFIghtAdmin.Models
{
    [Table("SwipeList")]
    public partial class SwipeList
    {
        public SwipeList()
        {
            AcceptedRestaurants = new HashSet<AcceptedRestaurant>();
            RejectedRestaurants = new HashSet<RejectedRestaurant>();
        }

        [Key]
        [Column("SwipeListID")]
        public Guid SwipeListId { get; set; }
        [Required]
        [Column("RestaurantID")]
        [StringLength(255)]
        public string RestaurantId { get; set; }
        [Column("MatchSessionID")]
        public Guid MatchSessionId { get; set; }

        [ForeignKey(nameof(MatchSessionId))]
        [InverseProperty("SwipeLists")]
        public virtual MatchSession MatchSession { get; set; }
        [ForeignKey(nameof(RestaurantId))]
        [InverseProperty("SwipeLists")]
        public virtual Restaurant Restaurant { get; set; }
        [InverseProperty(nameof(AcceptedRestaurant.SwipeList))]
        public virtual ICollection<AcceptedRestaurant> AcceptedRestaurants { get; set; }
        [InverseProperty(nameof(RejectedRestaurant.SwipeList))]
        public virtual ICollection<RejectedRestaurant> RejectedRestaurants { get; set; }
    }
}
