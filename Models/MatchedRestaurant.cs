using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FoodFIghtAdmin.Models
{
    [Table("MatchedRestaurant")]
    [Index(nameof(AcceptedRestaurantId), Name = "fkIdx_MatchedRestaurant_AcceptedRestaurantID")]
    public partial class MatchedRestaurant
    {
        [Key]
        [Column("MatchRestaurantID")]
        public Guid MatchRestaurantId { get; set; }
        [Column("DateTIme", TypeName = "datetime")]
        public DateTime DateTime { get; set; }
        [Column("AcceptedRestaurantID")]
        public Guid AcceptedRestaurantId { get; set; }

        [ForeignKey(nameof(AcceptedRestaurantId))]
        [InverseProperty("MatchedRestaurants")]
        public virtual AcceptedRestaurant AcceptedRestaurant { get; set; }
    }
}
