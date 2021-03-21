using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FoodFIghtAdmin.Models
{
    public partial class Setting
    {
        public Setting()
        {
            UserSettings = new HashSet<UserSetting>();
        }

        [Key]
        [Column("SettingsID")]
        public Guid SettingsId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public bool Enabled { get; set; }

        [InverseProperty(nameof(UserSetting.Settings))]
        public virtual ICollection<UserSetting> UserSettings { get; set; }
    }
}
