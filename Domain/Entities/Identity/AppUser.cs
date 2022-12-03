
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public ICollection<TeamMember> TeamMembers { get; set; }
        public ICollection<UserInvited> UsersWhoInvite { get; set; }
        public ICollection<UserInvited> InvitedByUsers { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }
        public ICollection<AppTask> UpdatedAppTasks { get; set; }
        public ICollection<AppTaskAudit> AppTaskAudit { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }

    }
}