using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<AppTask> AppTasks { get; set; }

    }
}
