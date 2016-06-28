using GiftAidCalculator.Context.TaxRate;
using GiftAidCalculator.Service;
using Moq;
using NUnit.Framework;
using System;

namespace GiftAidCalculator.Tests
{
    [TestFixture]
    public class Test_As_A_Donor
    {
        [Test]
        public static void GiftAidAmount_TaxRateIsDefault_Expect_CharityMake20PercentExtra([Values(10, 100)]decimal donationAmount)
        {
            decimal inputTaxRate = 20m;

            var taStore = new Mock<ITaxRateStore>();
            taStore.Setup(x => x.Get()).Returns(inputTaxRate);
            IGiftAidCalculatorService calc = new GiftAidCalculatorService(taStore.Object);
            decimal giftAidAmount = calc.GiftAidAmount(donationAmount);

            decimal expected = expectedGiftAidAmount(donationAmount, inputTaxRate);
            Assert.AreEqual(expected, giftAidAmount);
        }

        [Test]
        [TestCase(20, 100)]
        [TestCase(17.5, 10)]
        [TestCase(19.99, 100)]
        public static void GiftAidAmount_ChangingTaxRate_Expect_AmountChangeAccordingly(decimal taxRate, decimal donationAmount)
        {
            var taStore = new Mock<ITaxRateStore>();
            taStore.Setup(x => x.Get()).Returns(taxRate);
            IGiftAidCalculatorService calc = new GiftAidCalculatorService(taStore.Object);
            decimal giftAidAmount = calc.GiftAidAmount(donationAmount);
            decimal expected = expectedGiftAidAmount(donationAmount, taxRate);
            Assert.AreEqual(expected, giftAidAmount);
        }

        [Test]
        [TestCase(85.61, 21.40)]
        [TestCase(26.19, 6.55)]
        public static void GifAidAmount_FractionedToMoreThanTwoDigits_Expect_RoundedValue(decimal donationAmount, decimal expected)
        {
            var taxRate = new Mock<ITaxRateStore>();
            taxRate.Setup(m => m.Get()).Returns(20m);
            IGiftAidCalculatorService calc = new GiftAidCalculatorService(taxRate.Object);
            decimal giftAidAmount = calc.GiftAidAmount(donationAmount);
            
            Assert.AreEqual(expected, giftAidAmount);
        }


        [Test]
        public static void GiftAidAmount_VerifyInjectedTaxRateStoreIsCalled_Expect_AtLeastOnce([Values(10, 100)]decimal donationAmount)
        {
            decimal arbitraryTaxRate = 20m;

            var spyTaStore = new Mock<ITaxRateStore>();
            spyTaStore.Setup(x => x.Get()).Returns(arbitraryTaxRate);
            IGiftAidCalculatorService calc = new GiftAidCalculatorService(spyTaStore.Object);
            decimal giftAidAmount = calc.GiftAidAmount(donationAmount);

            spyTaStore.Verify(m=> m.Get(), Times.AtLeastOnce());
        }

        private static decimal expectedGiftAidAmount(decimal donationAmount, decimal currentTaxRate)
        {
            return Math.Round(donationAmount * (currentTaxRate / (100m - currentTaxRate)), 2);
        }
        
        [Test]
        public static void GifAidAmount_TaxRate100Donate100_Expect_10000AsGiftAid()
        {
            decimal gaTaxRate = 100m;
            decimal donationAmount = 100m;
            decimal expected = 10000m;
            var taxRate = new Mock<ITaxRateStore>();
            taxRate.Setup(m => m.Get()).Returns(gaTaxRate);
            IGiftAidCalculatorService calc = new GiftAidCalculatorService(taxRate.Object);

            decimal giftAidAmount = calc.GiftAidAmount(donationAmount);

            Assert.AreEqual(expected, giftAidAmount);
        }
    }
}
