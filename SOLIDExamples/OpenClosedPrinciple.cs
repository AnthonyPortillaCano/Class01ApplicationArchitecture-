using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDExamples
{
    // BADExample: Violing OCP-need to modify existing code
    // This class is closed for extension and OPEN for modification
    // Every time we want to add a new customer type, we must modify this method

    public class BadDiscountCalculator
    {
        public decimal CalculateDiscount(string customerType, decimal amount)
        {
            // If we want to add a new customer type,we must modify this method
            // This violates OCP because we are changing existing code.
            switch (customerType.ToLower())
            {
                case "regular":
                    return amount * 0.05m;// 5% discount
                case "premium":
                    return amount * 0.10m;// 10% discount
                case "vip":
                    return amount * 0.15m;// 15% discount
                default:
                    return 0m;
            }
        }
    }

    // GOOD EXAMPLE:Following OCP -open for extension
    // this approach allows us to add new discount types without modifying existing code.

    //Abstract base class -defines the contract for discount strategies
     public abstract class DiscountStrategy
    {
        //Abstract class that must be implemented by derived classes
         public abstract decimal CalculateDiscount(decimal amount);
    }
    // Concrete strategy for regular customers
    public class RegularCustomerDiscount : DiscountStrategy
    {
        public override decimal CalculateDiscount(decimal amount)
        {
            return amount * 0.05m;
        }
    }
    // Concrete strategy for premium customers
    public class PremiumCustomerDiscount : DiscountStrategy
    {
        public override decimal CalculateDiscount(decimal amount)
        {
            return amount * 0.10m;
        }
    }
    // Concrete strategy for Vip customers
    public class VipCustomerDiscount : DiscountStrategy
    {
        public override decimal CalculateDiscount(decimal amount)
        {
            return amount * 0.15m;
        }
    }
    //GOOD EXAMPLE:Calculator that doesn´t need modification
    //this clas is CLOSED for modification but OPEN for extension
    public class DiscountCalculator
    {
        //Dictionary to store discount strategies
        private readonly Dictionary<string, DiscountStrategy> _strategies;

        //Constructor initializes with existing strategies
        public DiscountCalculator()
        {
            _strategies = new Dictionary<string, DiscountStrategy>
            {
                { "regular",new RegularCustomerDiscount()},
                { "premium",new PremiumCustomerDiscount()},
                { "vip",new VipCustomerDiscount()},
            };
        }
        // Method to calculate discount - doesn´t need modification
        public decimal CalculateDiscount(string customerType,decimal amount) 
        { 
            if(_strategies.TryGetValue(customerType.ToLower(),out var strategy))
            {
                return strategy.CalculateDiscount(amount);
            }
            return 0m;
        }
    }
    // Extension point:Add new discount strategies without modifying existing code
    // this method allows us to add new strategies at runtime
    public class StudentDiscount : DiscountStrategy
    {
        public override decimal CalculateDiscount(decimal amount)
        {
            return amount * 0.20m;
        }
    }
}
