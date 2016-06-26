using GiftAidCalculator.Service;
using GiftAidCalculator.Context.Users;
using GiftAidCalculator.Context.TaxRate;
using System;

namespace GiftAidCalculator.TestConsole
{
    public class Application : IApplication
    {
        readonly IGiftAidCalculatorService _giftAidCalculator;

        public Application(IGiftAidCalculatorService giftAidCalc)
        {
            this._giftAidCalculator = giftAidCalc;
        }

        public void Run(string[] args)
        {
            var user = chooseUser(); 
            if (user is SiteAdminUser)
            {
                Console.WriteLine("Please Enter new Tax Rate:");
                string inputTaxRate = Console.ReadLine();
                decimal taxRate;
                if (decimal.TryParse(inputTaxRate, out taxRate))
                {
                    ITaxRateStore trs = new TaxRateStore(user);
                    trs.Set(taxRate);
                    Console.WriteLine("Tax Rate Updated");
                }
            }
            Console.WriteLine("Logged in as Donor...");
            IEvent anEvent = chooseEvent();
            _giftAidCalculator.SetEventType(anEvent);
            Console.WriteLine("Please Enter donation amount:");
            Console.WriteLine(_giftAidCalculator.GiftAidAmount(decimal.Parse(Console.ReadLine())));
                        
            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }

        private IEvent chooseEvent()
        {
            Console.WriteLine("Select an event: \r\n\t Press 1 for Running (5% supplement added for donations)");
            Console.WriteLine("\r\n\t Press 2 for Swimming (3% supplement added for donations)");
            Console.WriteLine("\r\n\t Press 3 for Cycling");
            Console.WriteLine("\r\n\t Press any other key to donate");
            return eventFactory(Console.ReadLine());
        }

        private IEvent eventFactory(string value)
        {
            IEvent theEvent;
            switch(value)
            {
                case "1":
                    theEvent = new RunningEvent(5) as IEvent;
                    break;
                case "2":
                    theEvent = new SwimmingEvent(3) as IEvent;
                    break;
                default:
                    theEvent = new NoSupplementEvent() as IEvent;
                    break;
            }
            return theEvent;
        }

        private IUser chooseUser()
        {
            Console.WriteLine("Logged in as: \r\n\t Press 1 for Site Admin");
            Console.WriteLine("\r\n\t Press any key for Donor");
            return userFactory(Console.ReadLine());
        }

        private IUser userFactory(string value)
        {
            if(value == "1")
            {
                return new SiteAdminUser();
            }
            return new DonorUser();
        }
    }
}
