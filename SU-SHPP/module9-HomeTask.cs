using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SU_SHPP
{
    // Паттерн Декоратор

    public abstract class Beverage
    {
        public abstract string GetDescription();
        public abstract double Cost();
    }

    public class Espresso : Beverage
    {
        public override string GetDescription()
        {
            return "Эспрессо";
        }

        public override double Cost()
        {
            return 100; 
        }
    }

    public class Tea : Beverage
    {
        public override string GetDescription()
        {
            return "Чай";
        }

        public override double Cost()
        {
            return 80; 
        }
    }

    public abstract class BeverageDecorator : Beverage
    {
        protected Beverage beverage;

        public BeverageDecorator(Beverage beverage)
        {
            this.beverage = beverage;
        }

        public override string GetDescription()
        {
            return beverage.GetDescription();
        }

        public override double Cost()
        {
            return beverage.Cost();
        }
    }

    public class Milk : BeverageDecorator
    {
        public Milk(Beverage beverage) : base(beverage) { }

        public override string GetDescription()
        {
            return base.GetDescription() + ", Молоко";
        }

        public override double Cost()
        {
            return base.Cost() + 50; 
        }
    }

    public class Syrup : BeverageDecorator
    {
        public Syrup(Beverage beverage) : base(beverage) { }

        public override string GetDescription()
        {
            return base.GetDescription() + ", Сироп";
        }

        public override double Cost()
        {
            return base.Cost() + 100; 
        }
    }


    public interface IPaymentProcessor
    {
        void ProcessPayment(double amount);
    }

    public class PayPalPaymentProcessor : IPaymentProcessor
    {
        public void ProcessPayment(double amount)
        {
            Console.WriteLine($"Оплата через PayPal на сумму {amount} тенге успешно выполнена.");
        }
    }

    public class StripePaymentService
    {
        public void MakeTransaction(double totalAmount)
        {
            Console.WriteLine($"Транзакция Stripe на сумму {totalAmount} тенге выполнена.");
        }
    }

    public class StripePaymentAdapter : IPaymentProcessor
    {
        private StripePaymentService stripeService;

        public StripePaymentAdapter(StripePaymentService stripeService)
        {
            this.stripeService = stripeService;
        }

        public void ProcessPayment(double amount)
        {
            stripeService.MakeTransaction(amount); 
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Пример с напитками:");
            Beverage beverage = new Espresso();
            beverage = new Milk(beverage); 
            beverage = new Syrup(beverage); 
            Console.WriteLine($"Ваш напиток: {beverage.GetDescription()}");
            Console.WriteLine($"Общая стоимость: {beverage.Cost()} тенге\n");

            Console.WriteLine("Пример с процессорами оплаты:");
            IPaymentProcessor paypalProcessor = new PayPalPaymentProcessor();
            paypalProcessor.ProcessPayment(500); 

            StripePaymentService stripeService = new StripePaymentService();
            IPaymentProcessor stripeAdapter = new StripePaymentAdapter(stripeService);
            stripeAdapter.ProcessPayment(700); 
        }
    }
}

