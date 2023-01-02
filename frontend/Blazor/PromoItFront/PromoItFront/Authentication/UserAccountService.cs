namespace PromoItFront.Authentication
{
    public class UserAccountService
    {
        private List<UserAccount> _users;

        public UserAccountService()
        {
            _users = new List<UserAccount>
            {
                new UserAccount { UserName = "admin", Password = "23243", Email = "admin@mail.ru", TelNumber = 123334, RoleId = 1 },
                new UserAccount { UserName = "NPOuser", Password = "2344345243", Email = "npo@mail.ru", TelNumber = 43334, RoleId = 2 }

            };
        }
        public UserAccount? GetByUserName(string userName)
        {
            return _users.FirstOrDefault(x => x.UserName == userName);
        }

    }
}
