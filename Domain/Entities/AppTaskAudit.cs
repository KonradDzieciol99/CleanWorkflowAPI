using Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class AppTaskAudit
    {
        public int Id { get; set; }
        public string ActionType { get; set; }
        public DateTime Date { get; set; }
        public  string Changes { get; set; }
        public string EntityName { get; set; }
        
        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int EntityId { get; set; }
        public AppTask Entity { get; set; }
        public int UpdaterId { get; set; }
        public AppUser Updater { get; set; }

        [NotMapped]
        public List<PropertyEntry> TempProperties { get; set; }
    }
}
