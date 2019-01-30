namespace BankingLedger.Biz
{
    public interface IUserBiz
    {
         int userExists(string userName, string password);

         int createUser(string userName, string password, string firstName, string lastName);

        void setUser(int userId);
         void clearUser();

    }
}