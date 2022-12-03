
namespace Domain.Entities
{
    public class syncfiusionTask
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(7);
        public double? Duration { get; set; }
        public int? Progress { get; set; }
        public string? Description { get; set; }
        public int? ParentID { get; set; }
        public bool IsParent { get; set; }
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}
