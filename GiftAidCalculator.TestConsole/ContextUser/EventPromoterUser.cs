namespace GiftAidCalculator.Context.Users
{
    public sealed class EventPromoterUser : IUser
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
