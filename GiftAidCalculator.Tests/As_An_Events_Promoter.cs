using GiftAidCalculator.Context.TaxRate;
using GiftAidCalculator.Service;
using Moq;
using NUnit.Framework;
using System;

namespace GiftAidCalculator.Tests
{
    [TestFixture]
    public class As_An_Events_Promoter
    {
        [Test]
        public static void GiftAidAmount_DonatingForRunningEvent_Except_5PercentSupplimentAdded()
        {
            decimal inputDonationAmount = 100m;
            decimal inputSupplimnet = 5m;

            var trStore = new Mock<ITaxRateStore>();
            trStore.Setup(m => m.Get()).Returns(0);

            var runningEvent = new GiftAidCalculator.Service.RunningEvent(inputSupplimnet);
            var calculator = new GiftAidCalculatorService(trStore.Object);
            calculator.SetEventType(runningEvent);
            var amount = calculator.GiftAidAmount(inputDonationAmount);

            var except = expectedGiftAidAmount(inputDonationAmount, inputSupplimnet);
            Assert.AreEqual(except, amount);

        }

        [Test]
        public static void GiftAidAmount_DonatingForSwimmingEvent_Except_3PercentSupplimentAdded()
        {
            decimal donationAmount = 100m;
            decimal swimmingSupplementRate = 3m;
            decimal taxRate = 0m;
            
            var trStore = new Mock<ITaxRateStore>();
            trStore.Setup(m => m.Get()).Returns(taxRate);
            var swimmingEvent = new GiftAidCalculator.Service.SwimmingEvent(swimmingSupplementRate);
            var calculator = new GiftAidCalculatorService(trStore.Object);
            calculator.SetEventType(swimmingEvent);
            var amount = calculator.GiftAidAmount(donationAmount);

            var except = expectedGiftAidAmount(donationAmount, suppliment: swimmingSupplementRate);
            Assert.AreEqual(except, amount);
        }


        [Test]
        public static void GiftAidAmount_DonatingForSwimmingEvent_Except_onlyGiftAidAmount()
        {
            decimal donationAmount = 100m;

            var otherEvent = new GiftAidCalculator.Service.NoSupplementEvent();
            var taxRate = new Mock<ITaxRateStore>();
            taxRate.Setup(m => m.Get()).Returns(20m);
            var calculator = new GiftAidCalculatorService(taxRate.Object);
            calculator.SetEventType(otherEvent);
            var amount = calculator.GiftAidAmount(donationAmount);

            var except = expectedGiftAidAmount(donationAmount, TaxRateStore.CurrentRate.Value, 0);
            Assert.AreEqual(except, amount);
        }

        private static decimal expectedGiftAidAmount(decimal donationAmount, decimal? currentTaxRate = null, decimal? suppliment = null)
        {
            var giftAidAmount = donationAmount * getRatio(currentTaxRate ?? 0);
            var supplementAmount = donationAmount * getRatio(suppliment ?? 0);
            return Math.Round(giftAidAmount + supplementAmount, 2);
        }

        private static decimal getRatio(decimal percentage)
        {
            return (percentage / (100m - percentage));
        }
    }
}
