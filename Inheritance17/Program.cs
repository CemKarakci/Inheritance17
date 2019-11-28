using System;
using System.Collections.Generic;

namespace Inheritance17
{
    class Program
    {
        static void Main(string[] args)
        {
            var appointment = new Appointment()
            {
                Name = "Bob",
                StartDateTime = DateTime.Now.AddHours(1),
                EndDateTime = DateTime.Now.AddHours(2),
                Price = 100D
            };

            var book = new Book()
            {
                Title = "How to Implement Interfaces",
                Price = 50D,
                TaxRate = 0.0825,
                ShippingRate = 5
            };

            var snack = new Snack()
            {
                Price = 2D
            };

            var tshirt = new TShirt()
            {
                Size = "2X",
                Price = 25D,
                TaxRate = 0.0625,
                ShippingRate = 2
            };

            var items = new List<IPurchasable>();
            items.Add(appointment);
            items.Add(book);
            items.Add(snack);
            items.Add(tshirt);

            var taxebleItems = new List<ITaxable>();
            
                foreach (var item in items)
            {
                if(item is ITaxable)
                {
                    taxebleItems.Add(item as ITaxable);
                }
            }
            var shipableItems = new List<IShippable>();
            foreach (var item in items)
            {
                if(item is IShippable)
                {
                    shipableItems.Add(item as IShippable);
                }
            }

            var shippingAmount = CalculateShipping(shipableItems);
            Console.WriteLine($"Total Shipping Cost: {shippingAmount}");

            var taxAmount = CalculateTax(taxebleItems);
            Console.WriteLine($"    Total Tax Amount: {taxAmount}");
            

            var cost = CompleteTransaction(items);

            //var TotalItemPrices = CalculateItemPrices(items);
            var TotalCost = cost + taxAmount + shippingAmount;
            Console.WriteLine($"      Grand Total: {TotalCost}");

            Console.ReadLine();
        }

        //static double CalculateItemPrices(List<IPurchasable> items)
        //{
        //    double price = 0;
        //    foreach (var item in items)
        //    {
        //        price += item.Price; 
        //    }

        //    return price;
        //}

        static double CalculateTax(List<ITaxable> items)
        {
            double tax = 0D;
            foreach (var item in items)
            {
                tax += item.Tax();
            }

            return tax;

        }

        static double CalculateShipping(List<IShippable> items)
        {
            double shipping = 0D;
            foreach (var item in items)
            {
                shipping += item.Ship();
            }

            return shipping;
        }

        static double CompleteTransaction(List<IPurchasable> items)
        {
            double total = 0;
            items.ForEach(p => p.Purchase());
            items.ForEach(p => total += p.Price);
            return total;
        }
    }

    public class Appointment : IPurchasable
    {
        public string Name { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public double Price { get; set; }

        public void Purchase()
        {
            Console.WriteLine($"Payment for Appointment for {Name} from {StartDateTime} to {EndDateTime} for {Price.ToString()}.");
        }
    }

    public class Book : IPurchasable, ITaxable, IShippable
    {
        public double TaxRate { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public double ShippingRate { get; set; }

        public void Purchase()
        {
            Console.WriteLine($"Purchasing {Title} for {Price.ToString()}.");
        }

        public double Ship()
        {
            double shippingCost = ShippingRate * Price;
            Console.WriteLine($"Shipping Cost: {shippingCost}");
            return shippingCost;
        }

        public double Tax()
        {
            double tax = TaxRate * Price;
            Console.WriteLine($"    TaxRate: {TaxRate} = {tax}");
            return tax;
        }
    }

    public class TShirt : IPurchasable, ITaxable, IShippable
    {
        public double Price { get; set; }
        public string Size { get; set; }
        public double TaxRate { get; set; }
        public double ShippingRate { get; set; }

        public void Purchase()
        {
            Console.WriteLine($"Purchasing TShirt {Size} for {Price.ToString()}.");
        }

        public double Ship()
        {
            double shippingCost = ShippingRate * Price;
            Console.WriteLine($"Shipping Cost: {shippingCost}");
            return shippingCost;

        }

        public double Tax()
        {
            double tax = TaxRate * Price;
            Console.WriteLine($"    TaxRate: {TaxRate} = {tax}");
            return tax;
        }
    }

    public class Snack : IPurchasable
    {
        public double Price { get; set; }

        public void Purchase()
        {
            Console.WriteLine($"Purchasing a snack for {Price.ToString()}.");
        }
    }

    interface IPurchasable
    {
        double Price { get; set; }

        void Purchase();
    }

    interface IShippable
    {
        double ShippingRate { get; set; }
        double Ship();
    }

    interface ITaxable
    {
        double TaxRate { get; set; }
        double Tax();
    }
}
