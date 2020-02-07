namespace WriterPlatformApp.BLL.BO
{
    public class UserBO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool isLocked { get; set; }
        public string Role { get; set; }
    }
}
