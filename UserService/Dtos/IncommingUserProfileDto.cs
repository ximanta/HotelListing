namespace UserService.Dtos
{
    public class IncommingUserProfileDto
    {
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public char Gender { get; set; }
        public int Age { get; set; }
    }
}
