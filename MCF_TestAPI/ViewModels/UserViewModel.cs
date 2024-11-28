namespace MCF_TestAPI.ViewModels
{
    public class UserViewModel
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public bool isActive { get; set; }
        public string? Token { get; set; }
    }

    public class UserLoginViewModel
    {
        public string user_name { get; set; }
        public string password { get; set; }
    }
}
