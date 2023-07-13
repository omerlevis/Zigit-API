namespace ZigitApi.Entities
{
    public partial class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int DurationInDays { get; set; }
        public int BugsCount { get; set; }
        public bool MadeDeadline { get; set; }
        public int userId { get; set; }

    }

}
