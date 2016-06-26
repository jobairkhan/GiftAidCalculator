using GiftAidCalculator.Context.TaxRate;
using GiftAidCalculator.Context.Users;
using Moq;
using NUnit.Framework;
using System;

namespace GiftAidCalculator.Tests
{
    [TestFixture]
    public static class Test_As_A_Standard_User
    {
        [Test]
        public static void SetTaxRate_ByNonAdminUser_Expect_UnauthorizedAccessException()
        {
            var loggedInUser = new Mock<IUser>();
            loggedInUser.SetupGet(m => m.IsAdmin).Returns(false);
            var taStore = new TaxRateStore(loggedInUser.Object);

            Assert.That(() => taStore.Set(100),
                Throws.Exception.TypeOf<UnauthorizedAccessException>());
        }

        [Test]
        public static void SetTaxRate_ByNonAdminUser_Verify_UsersAdminProperyChecked()
        {
            var spyUser = new Mock<IUser>();
            spyUser.SetupGet(m => m.IsAdmin).Returns(false);
            var taStore = new TaxRateStore(spyUser.Object);

            var ex = Assert.Throws<UnauthorizedAccessException>(() => taStore.Set(0));
            spyUser.Verify(m => m.IsAdmin);
        }

        [Test]
        public static void SetTaxRate_ByDonor_Expect_AccessDenied()
        {
            IUser loggedInUser = new DonorUser();
            var taStore = new TaxRateStore(loggedInUser);

            var ex = Assert.Throws<UnauthorizedAccessException>(() => taStore.Set(0));
            Assert.That(ex.Message, Is.EqualTo("Access Denied!"));
        }

    }
}
