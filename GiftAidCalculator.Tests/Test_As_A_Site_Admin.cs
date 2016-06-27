using GiftAidCalculator.Context.TaxRate;
using GiftAidCalculator.Context.Users;
using NUnit.Framework;

namespace GiftAidCalculator.Tests
{
    [TestFixture]
    public class Test_As_A_Site_Admin
    {
        [Test]
        public static void SetTaxRate_To10ByAdmin_Expect_TaxRateChangedTo10()
        {
            decimal input = 10m;
            
            IUser loggedInUser = new SiteAdminUser();
            var trStore = new TaxRateStore(loggedInUser);
            trStore.Set(input);

            Assert.IsTrue(input == TaxRateStore.CurrentRate.Value);
        }
    }
}
