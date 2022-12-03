using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class AppTaskChanges
    {
        public int Id { get; set; }
        public string EntityName { get; set; }
        public string ActionType { get; set; }
        public string Username { get; set; }
        public DateTime TimeStamp { get; set; }
        public Dictionary<string, object> Changes { get; set; }
        [JsonIgnore]
        public AppTask Entity { get; set; }
        public int EntityId { get; set; }

        [NotMapped]
        public List<PropertyEntry> TempProperties { get; set; }

    }
}
