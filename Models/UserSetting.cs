using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FoodFIghtAdmin.Models
{
    [Index(nameof(SettingsId), Name = "fkIdx_UserSettings_SettingsID")]
    [Index(nameof(UserId), Name = "fkIdx_UserSettings_UserID")]
    public partial class UserSetting
    {
        [Key]
        [Column("UserSettingsID")]
        public Guid UserSettingsId { get; set; }
        [Column("SettingsID")]
        public Guid SettingsId { get; set; }
        [Column("UserID")]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(SettingsId))]
        [InverseProperty(nameof(Setting.UserSettings))]
        public virtual Setting Settings { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserSettings")]
        public virtual User User { get; set; }
    }
}
