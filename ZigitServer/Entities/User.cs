
namespace ZigitApi.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Team { get; set; }
        public string? joined { get; set; }
        public string? Avatar { get; set; }
        public string? Token { get; set; }
        public DateTime? Token_created_at { get; set; }
}
}
