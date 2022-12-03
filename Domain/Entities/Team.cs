using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<TeamMember> TeamMembers { get; set; }
        public ICollection<AppTask> AppTasks { get; set; }
        public ICollection<syncfiusionTask> SyncfiusionTasks { get; set; }
        public ICollection<GanttEvent> GanttEvents { get; set; }
        public ICollection<AppTaskAudit> AppTaskAudits { get; set; }


    }
}
