namespace LookupSystem.DataAccess.Data
{
    public interface IUserRepository
    {
        public void DeleteUserOlderThan(int countOfDays);
    }
}
