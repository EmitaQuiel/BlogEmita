namespace BlogEmi.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } // Propiedad de navegación

        public string UserName { get; set; }
        public string Description { get; set; } = "This is the default description.";
        public byte[]? Image { get; set; }
    }
}
