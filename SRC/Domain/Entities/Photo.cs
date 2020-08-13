namespace BasketballLeague.Domain.Entities
{
    public class Photo
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string AppUserId { get; set; }
        public int? TeamId { get; set; }
        public Team Team { get; set; }
    }
}
