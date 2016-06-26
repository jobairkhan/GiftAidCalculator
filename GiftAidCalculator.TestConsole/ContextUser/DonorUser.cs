namespace GiftAidCalculator.Context.Users
{
    public sealed class DonorUser : IUser
    {
        public bool IsAdmin
        {
            get
            {
                return false;
            }
        }
    }
}
