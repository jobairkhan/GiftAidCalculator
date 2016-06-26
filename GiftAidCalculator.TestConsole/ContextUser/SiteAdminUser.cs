namespace GiftAidCalculator.Context.Users
{
    public sealed class SiteAdminUser : IUser
    {
        public bool IsAdmin
        {
            get
            {
                return true;
            }
        }
    }
}
