// ===========================================
// SOLID PRINCIPLES DEMONSTRATION PROGRAM
// ===========================================
// This program demonstrates all five SOLID principles with practical examples
//
// HOW TO RUN:
// 1. Navigate to this directory: cd SOLIDExamples
// 2. Run the program: dotnet run
// 3. Watch the demonstrations of each SOLID principle
//
// WHAT YOU'LL LEARN:
// - How to identify violations of SOLID principles
// - How to refactor code to follow SOLID principles
// - Practical benefits of applying SOLID principles
// - Real-world examples of good and bad code

namespace SOLIDExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("🎓 SOLID PRINCIPLES LEARNING DEMO");
            Console.WriteLine("==================================");
            Console.WriteLine();
            Console.WriteLine("This program demonstrates the five SOLID principles:");
            Console.WriteLine("1. S - Single Responsibility Principle");
            Console.WriteLine("2. O - Open/Closed Principle");
            Console.WriteLine("3. L - Liskov Substitution Principle");
            Console.WriteLine("4. I - Interface Segregation Principle");
            Console.WriteLine("5. D - Dependency Inversion Principle");
            Console.WriteLine();
            Console.WriteLine("Each principle will be demonstrated with:");
            Console.WriteLine("❌ Bad examples (violations)");
            Console.WriteLine("✅ Good examples (correct implementations)");
            Console.WriteLine();
            Console.WriteLine("Press any key to start the demonstrations...");
            Console.ReadKey();
            Console.Clear();

            // Run all SOLID principle demonstrations
            SOLIDDemoRunner.RunAllDemos();

            Console.WriteLine();
            Console.WriteLine("🎉 DEMONSTRATION COMPLETE!");
            Console.WriteLine("==========================");
            Console.WriteLine();
            Console.WriteLine("Key takeaways:");
            Console.WriteLine("• SOLID principles help create maintainable, testable, and flexible code");
            Console.WriteLine("• Each principle addresses a specific problem in software design");
            Console.WriteLine("• Applying these principles leads to better architecture");
            Console.WriteLine("• These principles work together to create robust systems");
            Console.WriteLine();
            Console.WriteLine("For more information, check the code comments in each file:");
            Console.WriteLine("• SingleResponsibilityPrinciple.cs - Single Responsibility Principle");
            Console.WriteLine("• OpenClosedPrinciple.cs - Open/Closed Principle");
            Console.WriteLine("• LiskovSubstitutionPrinciple.cs - Liskov Substitution Principle");
            Console.WriteLine("• InterfaceSegregationPrinciple.cs - Interface Segregation Principle");
            Console.WriteLine("• DependencyInversionPrinciple.cs - Dependency Inversion Principle");
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}