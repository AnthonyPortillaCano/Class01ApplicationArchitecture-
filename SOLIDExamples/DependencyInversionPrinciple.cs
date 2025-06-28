namespace SOLIDExamples
{
    // ===========================================
    // DEPENDENCY INVERSION PRINCIPLE (DIP)
    // ===========================================
    // High-level modules should not depend on low-level modules. Both should depend on abstractions.
    //
    // PROBLEM: When high-level modules depend directly on low-level modules:
    // - High-level modules become tightly coupled to specific implementations
    // - It's hard to change or test low-level modules
    // - The system becomes rigid and hard to maintain
    // - Violates the Open/Closed Principle
    //
    // SOLUTION: Both high-level and low-level modules should depend on abstractions (interfaces)

    // ❌ BAD EXAMPLE: High-level module depends on low-level module
    // OrderProcessor (high-level) directly depends on SqlServerDatabase, SmtpEmailService, FileLogger (low-level)
    public class BadOrderProcessor
    {
        private readonly SqlServerDatabase _database;
        private readonly SmtpEmailService _emailService;
        private readonly FileLogger _logger;

        public BadOrderProcessor()
        {
            // Direct dependency on concrete implementations
            // This creates tight coupling - OrderProcessor is now tied to these specific classes
            _database = new SqlServerDatabase();
            _emailService = new SmtpEmailService();
            _logger = new FileLogger();
        }

        public void ProcessOrder(Order order)
        {
            _database.Save(order);
            _emailService.SendConfirmation(order.CustomerEmail);
            _logger.Log($"Order {order.Id} processed");
        }
    }

    // Low-level modules (concrete implementations)
    // These are the specific implementations that the high-level module depends on
    public class SqlServerDatabase
    {
        public void Save(Order order)
        {
            Console.WriteLine($"Saving order {order.Id} to SQL Server");
        }
    }

    public class SmtpEmailService
    {
        public void SendConfirmation(string email)
        {
            Console.WriteLine($"Sending confirmation email to {email} via SMTP");
        }
    }

    public class FileLogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"Logging to file: {message}");
        }
    }

    // Data model
    public class Order
    {
        public int Id { get; set; }
        public string CustomerEmail { get; set; } = string.Empty;
    }

    // ✅ GOOD EXAMPLE: Both high and low-level modules depend on abstractions
    // This approach uses dependency injection and interfaces

    // Abstractions (interfaces) that both high and low-level modules depend on
    public interface IOrderRepository
    {
        void Save(Order order);
    }

    public interface IEmailService
    {
        void SendConfirmation(string email);
    }

    public interface IDipLogger
    {
        void Log(string message);
    }

    // High-level module depends on abstractions (interfaces)
    // OrderProcessor now depends on interfaces, not concrete classes
    public class GoodOrderProcessor
    {
        private readonly IOrderRepository _repository;
        private readonly IEmailService _emailService;
        private readonly IDipLogger _logger;

        // Constructor injection - dependencies are injected via interfaces
        public GoodOrderProcessor(IOrderRepository repository, IEmailService emailService, IDipLogger logger)
        {
            _repository = repository;
            _emailService = emailService;
            _logger = logger;
        }

        public void ProcessOrder(Order order)
        {
            _repository.Save(order);
            _emailService.SendConfirmation(order.CustomerEmail);
            _logger.Log($"Order {order.Id} processed");
        }
    }

    // Low-level modules implement abstractions
    // These concrete classes implement the interfaces that the high-level module depends on

    // SQL Server implementation of the repository interface
    public class SqlServerOrderRepository : IOrderRepository
    {
        public void Save(Order order)
        {
            Console.WriteLine($"Saving order {order.Id} to SQL Server");
        }
    }

    // MongoDB implementation of the repository interface
    public class MongoDbOrderRepository : IOrderRepository
    {
        public void Save(Order order)
        {
            Console.WriteLine($"Saving order {order.Id} to MongoDB");
        }
    }

    // SMTP implementation of the email service interface
    public class SmtpEmailServiceImplementation : IEmailService
    {
        public void SendConfirmation(string email)
        {
            Console.WriteLine($"Sending confirmation email to {email} via SMTP");
        }
    }

    // SendGrid implementation of the email service interface
    public class SendGridEmailService : IEmailService
    {
        public void SendConfirmation(string email)
        {
            Console.WriteLine($"Sending confirmation email to {email} via SendGrid");
        }
    }

    // File implementation of the logger interface
    public class FileLoggerImplementation : IDipLogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"Logging to file: {message}");
        }
    }

    // Console implementation of the logger interface
    public class ConsoleLogger : IDipLogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"Console log: {message}");
        }
    }

    // ✅ GOOD EXAMPLE: Dependency injection container setup
    // This shows how to configure dependencies in a real application
    public class ServiceContainer
    {
        public static void ConfigureServices()
        {
            // This is where you would configure your DI container
            // Example with a simple factory pattern
            var repository = new SqlServerOrderRepository();
            var emailService = new SmtpEmailServiceImplementation();
            var logger = new FileLoggerImplementation();

            // Create the high-level module with injected dependencies
            var orderProcessor = new GoodOrderProcessor(repository, emailService, logger);
            
            // Use the order processor
            var order = new Order { Id = 1, CustomerEmail = "customer@example.com" };
            orderProcessor.ProcessOrder(order);
        }
    }

    // ✅ GOOD EXAMPLE: Easy to swap implementations
    // This demonstrates the flexibility of DIP - you can easily swap implementations
    public class TestOrderProcessor
    {
        public static void TestWithDifferentImplementations()
        {
            // Test with SQL Server
            var sqlProcessor = new GoodOrderProcessor(
                new SqlServerOrderRepository(),
                new SmtpEmailServiceImplementation(),
                new FileLoggerImplementation()
            );

            // Test with MongoDB
            var mongoProcessor = new GoodOrderProcessor(
                new MongoDbOrderRepository(),
                new SendGridEmailService(),
                new ConsoleLogger()
            );

            var order = new Order { Id = 2, CustomerEmail = "test@example.com" };
            
            Console.WriteLine("Testing with SQL Server:");
            sqlProcessor.ProcessOrder(order);
            
            Console.WriteLine("\nTesting with MongoDB:");
            mongoProcessor.ProcessOrder(order);
        }
    }

    // ===========================================
    // HOW TO USE THE DIP EXAMPLE
    // ===========================================
    // Step 1: Define abstractions (interfaces)
    // Step 2: Create high-level modules that depend on abstractions
    // Step 3: Create low-level modules that implement abstractions
    // Step 4: Use dependency injection to wire everything together
    //
    // Example:
    // var processor = new GoodOrderProcessor(
    //     new SqlServerOrderRepository(),
    //     new SmtpEmailServiceImplementation(),
    //     new FileLoggerImplementation()
    // );
    // processor.ProcessOrder(order);

    // ===========================================
    // BENEFITS OF APPLYING DIP
    // ===========================================
    // 1. LOOSE COUPLING: High-level modules don't depend on specific implementations
    // 2. EASY TESTING: You can easily mock dependencies for unit testing
    // 3. FLEXIBILITY: Easy to swap implementations without changing high-level code
    // 4. MAINTAINABILITY: Changes to low-level modules don't affect high-level modules
    // 5. FOLLOWS OCP: Easy to extend with new implementations
    // 6. BETTER DESIGN: Forces you to think about abstractions and contracts
} 