using Domain.Identity.Entities;

namespace Domain.Entities
{
    public class UserInvited
    {
        public AppUser SourceUser { get; set; }
        public int SourceUserId { get; set; }
        public string SourceUserName { get; set; }
        public AppUser InvitedUser { get; set; }
        public int InvitedUserId { get; set; }
        public string InvitedUserName { get; set; }
        public bool Confirmed { get; set; }
    }
}
