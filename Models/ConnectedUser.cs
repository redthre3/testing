using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FoodFIghtAdmin.Models
{
    [Index(nameof(BaseUserId), Name = "fkIdx_ConnectedUsersBase_UserID")]
    [Index(nameof(FriendUserId), Name = "fkIdx_ConnectedUsersBlocked_UserID")]
    public partial class ConnectedUser
    {
        public ConnectedUser()
        {
            MatchSessions = new HashSet<MatchSession>();
        }

        [Key]
        [Column("ConnectedUserID")]
        public Guid ConnectedUserId { get; set; }
        [Column("BaseUserID")]
        public Guid BaseUserId { get; set; }
        [Column("FriendUserID")]
        public Guid FriendUserId { get; set; }

        [ForeignKey(nameof(BaseUserId))]
        [InverseProperty(nameof(User.ConnectedUserBaseUsers))]
        public virtual User BaseUser { get; set; }
        [ForeignKey(nameof(FriendUserId))]
        [InverseProperty(nameof(User.ConnectedUserFriendUsers))]
        public virtual User FriendUser { get; set; }
        [InverseProperty(nameof(MatchSession.ConnectedUser))]
        public virtual ICollection<MatchSession> MatchSessions { get; set; }
    }
}
