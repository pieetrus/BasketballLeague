using System;


namespace BasketballLeague.Application.Players.Queries.GetPlayersList
{
    public class PlayerListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? Birthdate { get; set; }
        public string PhotoUrl { get; set; }
        public int? Height { get; set; }
        public string Position { get; set; }
    }
}
