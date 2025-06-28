namespace SOLIDExamples
{
    // ===========================================
    // SOLID PRINCIPLES DEMO RUNNER
    // ===========================================
    // This class demonstrates all SOLID principles in action
    // 
    // PURPOSE: This runner shows practical examples of how SOLID principles
    // improve code quality, maintainability, and flexibility
    //
    // USAGE: Call SOLIDDemoRunner.RunAllDemos() to see all principles in action
    // Each demo shows both bad and good examples with explanations

    public class SOLIDDemoRunner
    {
        /// <summary>
        /// Runs demonstrations of all five SOLID principles
        /// Shows both violations and correct implementations
        /// </summary>
        public static void RunAllDemos()
        {
            Console.WriteLine("🚀 SOLID PRINCIPLES DEMONSTRATION");
            Console.WriteLine("================================\n");

            // Demonstrate each principle with practical examples
            DemoSRP();
            DemoOCP();
            DemoLSP();
            DemoISP();
            DemoDIP();

            Console.WriteLine("\n✅ All SOLID principles demonstrated successfully!");
            Console.WriteLine("\n📚 KEY TAKEAWAYS:");
            Console.WriteLine("- SRP: Each class has one reason to change");
            Console.WriteLine("- OCP: Open for extension, closed for modification");
            Console.WriteLine("- LSP: Subtypes are substitutable for base types");
            Console.WriteLine("- ISP: Clients depend only on interfaces they use");
            Console.WriteLine("- DIP: Depend on abstractions, not concretions");
        }

        /// <summary>
        /// Demonstrates Single Responsibility Principle (SRP)
        /// Shows how splitting responsibilities improves code quality
        /// </summary>
        private static void DemoSRP()
        {
            Console.WriteLine("📋 SINGLE RESPONSIBILITY PRINCIPLE (SRP)");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Problem: Classes with multiple responsibilities are hard to maintain");
            Console.WriteLine("Solution: Split classes so each has only one responsibility\n");

            // Bad example - shows the problem
            Console.WriteLine("❌ Bad Example (Multiple responsibilities):");
            Console.WriteLine("- BadUserManager handles validation, persistence, email, and logging");
            Console.WriteLine("- This makes it hard to test, maintain, and reuse");
            var badManager = new BadUserManager();
            try
            {
                badManager.CreateUser("John Doe", "john@example.com");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // Good example - shows the solution
            Console.WriteLine("\n✅ Good Example (Single responsibilities):");
            Console.WriteLine("- UserValidator: Only validates user data");
            Console.WriteLine("- UserRepository: Only handles data persistence");
            Console.WriteLine("- EmailService: Only sends emails");
            Console.WriteLine("- FileLogger: Only handles logging");
            Console.WriteLine("- UserManager: Only orchestrates the process");
            
            var user = new SrpUser { Name = "Jane Doe", Email = "jane@example.com" };
            var userManager = new SrpUserManager(
                new SrpUserRepository(),
                new SrpEmailService(),
                new SrpFileLogger()
            );
            userManager.CreateUser(user);

            Console.WriteLine();
        }

        /// <summary>
        /// Demonstrates Open/Closed Principle (OCP)
        /// Shows how to extend functionality without modifying existing code
        /// </summary>
        private static void DemoOCP()
        {
            Console.WriteLine("🔓 OPEN/CLOSED PRINCIPLE (OCP)");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Problem: Adding new functionality requires modifying existing code");
            Console.WriteLine("Solution: Use abstraction to allow extension without modification\n");

            // Bad example - shows the problem
            Console.WriteLine("❌ Bad Example (Need to modify existing code):");
            Console.WriteLine("- BadDiscountCalculator requires modification to add new customer types");
            Console.WriteLine("- This violates OCP and can break existing functionality");
            var badCalculator = new BadDiscountCalculator();
            Console.WriteLine($"Regular customer discount: {badCalculator.CalculateDiscount("regular", 100)}");
            Console.WriteLine($"Premium customer discount: {badCalculator.CalculateDiscount("premium", 100)}");

            // Good example - shows the solution
            Console.WriteLine("\n✅ Good Example (Open for extension):");
            Console.WriteLine("- DiscountCalculator uses strategy pattern");
            Console.WriteLine("- New discount types can be added without modifying existing code");
            Console.WriteLine("- Each strategy class has a single responsibility");
            var goodCalculator = new DiscountCalculator();
            Console.WriteLine($"Regular customer discount: {goodCalculator.CalculateDiscount("regular", 100)}");
            Console.WriteLine($"Premium customer discount: {goodCalculator.CalculateDiscount("premium", 100)}");

            // Extension without modification
            Console.WriteLine("\n🆕 Adding new discount strategy without modifying existing code:");
            Console.WriteLine("- StudentDiscount is added at runtime");
            Console.WriteLine("- No existing code was modified");
            goodCalculator.AddDiscountStrategy("student", new StudentDiscount());
            Console.WriteLine($"Student discount: {goodCalculator.CalculateDiscount("student", 100)}");

            Console.WriteLine();
        }

        /// <summary>
        /// Demonstrates Liskov Substitution Principle (LSP)
        /// Shows how inheritance can violate contracts and how to fix it
        /// </summary>
        private static void DemoLSP()
        {
            Console.WriteLine("🔄 LISKOV SUBSTITUTION PRINCIPLE (LSP)");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Problem: Subtypes that violate the contract of their base types");
            Console.WriteLine("Solution: Ensure subtypes can be used anywhere their base type is expected\n");

            // Bad example - shows the problem
            Console.WriteLine("❌ Bad Example (LSP violation):");
            Console.WriteLine("- Square inherits from Rectangle but changes behavior");
            Console.WriteLine("- Setting width also changes height, violating Rectangle's contract");
            var badRectangle = new Rectangle { Width = 4, Height = 5 };
            var badSquare = new Square();
            badSquare.Width = 4; // This also sets height to 4
            badSquare.Height = 5; // This also sets width to 5

            Console.WriteLine($"Rectangle area: {badRectangle.GetArea()}"); // Expected: 20
            Console.WriteLine($"Square area: {badSquare.GetArea()}"); // Actual: 25 (not 20)

            // Good example - shows the solution
            Console.WriteLine("\n✅ Good Example (LSP compliance):");
            Console.WriteLine("- All shapes implement IShape interface");
            Console.WriteLine("- Each shape has its own independent properties");
            Console.WriteLine("- All shapes can be substituted for each other");
            var shapes = new List<IShape>
            {
                new RectangleShape { Width = 4, Height = 5 },
                new SquareShape { Side = 4 },
                new CircleShape { Radius = 3 }
            };

            foreach (var shape in shapes)
            {
                Console.WriteLine($"Shape area: {shape.GetArea()}");
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Demonstrates Interface Segregation Principle (ISP)
        /// Shows how fat interfaces force clients to implement unused methods
        /// </summary>
        private static void DemoISP()
        {
            Console.WriteLine("🎯 INTERFACE SEGREGATION PRINCIPLE (ISP)");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Problem: Fat interfaces force clients to implement unused methods");
            Console.WriteLine("Solution: Create smaller, focused interfaces that clients actually need\n");

            // Bad example - shows the problem
            Console.WriteLine("❌ Bad Example (Fat interface):");
            Console.WriteLine("- IWorker interface has too many methods");
            Console.WriteLine("- HumanWorker and RobotWorker must implement methods they can't use");
            var humanWorker = new HumanWorker();
            var robotWorker = new RobotWorker();

            Console.WriteLine("Human worker methods:");
            humanWorker.Work();
            humanWorker.Eat();
            humanWorker.Sleep();

            Console.WriteLine("\nRobot worker methods:");
            robotWorker.WriteCode();
            robotWorker.DesignArchitecture();
            robotWorker.TestCode();

            // Good example - shows the solution
            Console.WriteLine("\n✅ Good Example (Segregated interfaces):");
            Console.WriteLine("- Each interface has a single, focused responsibility");
            Console.WriteLine("- Clients only implement interfaces they actually need");
            Console.WriteLine("- No NotImplementedException thrown");
            var goodHuman = new GoodHumanWorker();
            var goodRobot = new GoodRobotWorker();
            var developer = new Developer();

            Console.WriteLine("Good human worker:");
            goodHuman.Work();
            goodHuman.Eat();
            goodHuman.Sleep();

            Console.WriteLine("\nGood robot worker:");
            goodRobot.Work();
            goodRobot.WriteCode();
            goodRobot.Deploy();

            Console.WriteLine("\nDeveloper (implements relevant interfaces):");
            developer.Work();
            developer.WriteCode();
            developer.GetPaid();

            Console.WriteLine();
        }

        /// <summary>
        /// Demonstrates Dependency Inversion Principle (DIP)
        /// Shows how high-level modules should depend on abstractions
        /// </summary>
        private static void DemoDIP()
        {
            Console.WriteLine("🔄 DEPENDENCY INVERSION PRINCIPLE (DIP)");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Problem: High-level modules depend directly on low-level modules");
            Console.WriteLine("Solution: Both should depend on abstractions (interfaces)\n");

            // Bad example - shows the problem
            Console.WriteLine("❌ Bad Example (High-level depends on low-level):");
            Console.WriteLine("- BadOrderProcessor directly depends on concrete implementations");
            Console.WriteLine("- Tight coupling makes it hard to test and change");
            var badProcessor = new BadOrderProcessor();
            var order = new Order { Id = 1, CustomerEmail = "customer@example.com" };
            badProcessor.ProcessOrder(order);

            // Good example - shows the solution
            Console.WriteLine("\n✅ Good Example (Both depend on abstractions):");
            Console.WriteLine("- GoodOrderProcessor depends on interfaces");
            Console.WriteLine("- Dependencies are injected via constructor");
            Console.WriteLine("- Easy to test with mocks and swap implementations");
            var goodProcessor = new GoodOrderProcessor(
                new SqlServerOrderRepository(),
                new SmtpEmailServiceImplementation(),
                new FileLoggerImplementation()
            );
            goodProcessor.ProcessOrder(order);

            // Easy to swap implementations
            Console.WriteLine("\n🔄 Swapping implementations:");
            Console.WriteLine("- Same high-level module works with different implementations");
            Console.WriteLine("- No code changes needed in GoodOrderProcessor");
            TestOrderProcessor.TestWithDifferentImplementations();

            Console.WriteLine();
        }
    }
} 