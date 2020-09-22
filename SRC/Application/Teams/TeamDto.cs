namespace BasketballLeague.Application.Teams
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string LogoUrl { get; set; }

        public int Fouls1Qtr { get; set; }
        public int Fouls2Qtr { get; set; }
        public int Fouls3Qtr { get; set; }
        public int Fouls4Qtr { get; set; }
        public int Timeouts1Half { get; set; }
        public int Timeouts2Half { get; set; }
    }
}
