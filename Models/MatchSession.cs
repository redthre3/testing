using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FoodFIghtAdmin.Models
{
    [Table("MatchSession")]
    [Index(nameof(ConnectedUserId), Name = "fkIdx_MatchSession_ConnectedUserID")]
    [Index(nameof(MatchSessionId), Name = "fkIdx_SwipeList_MatchSessionID")]
    public partial class MatchSession
    {
        public MatchSession()
        {
            SwipeLists = new HashSet<SwipeList>();
        }

        [Key]
        [Column("MatchSessionID")]
        public Guid MatchSessionId { get; set; }
        [Column("ConnectedUserID")]
        public Guid ConnectedUserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateTime { get; set; }
        [StringLength(50)]
        public string Lat { get; set; }
        [StringLength(50)]
        public string Lng { get; set; }

        [ForeignKey(nameof(ConnectedUserId))]
        [InverseProperty("MatchSessions")]
        public virtual ConnectedUser ConnectedUser { get; set; }
        [InverseProperty(nameof(SwipeList.MatchSession))]
        public virtual ICollection<SwipeList> SwipeLists { get; set; }
    }
}
