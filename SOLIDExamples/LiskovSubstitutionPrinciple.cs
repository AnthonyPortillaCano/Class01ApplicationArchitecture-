namespace SOLIDExamples
{
    // ===========================================
    // LISKOV SUBSTITUTION PRINCIPLE (LSP)
    // ===========================================
    // Subtypes must be substitutable for their base types
    //
    // PROBLEM: When inheritance is used incorrectly, subtypes cannot be used
    // in place of their base types without breaking the program
    // This can:
    // - Cause unexpected behavior
    // - Break existing code
    // - Make the code hard to understand
    // - Violate the contract of the base class
    //
    // SOLUTION: Ensure that derived classes can be used anywhere their base class is expected

    // ❌ BAD EXAMPLE: Violating LSP
    // This is a classic example of LSP violation: Square inheriting from Rectangle
    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public virtual int GetArea()
        {
            return Width * Height;
        }
    }

    // This class violates LSP because it changes the behavior of the base class
    public class Square : Rectangle
    {
        private int _side;

        public override int Width
        {
            get => _side;
            set
            {
                _side = value;
                // This violates LSP because changing width also changes height
                // A Rectangle's width and height are independent, but Square's are not
            }
        }

        public override int Height
        {
            get => _side;
            set
            {
                _side = value;
                // This violates LSP because changing height also changes width
                // This breaks the contract of the Rectangle class
            }
        }
    }

    // ❌ BAD EXAMPLE: This will break with LSP violation
    // This method expects a Rectangle but gets unexpected behavior with Square
    public class BadAreaCalculator
    {
        public static void TestRectangle(Rectangle rectangle)
        {
            // This should work for any Rectangle subtype
            rectangle.Width = 4;
            rectangle.Height = 5;
            
            // Expected: 4 * 5 = 20
            // But with Square: 5 * 5 = 25 (because setting height also sets width)
            // This violates the contract - we expect independent width and height
            Console.WriteLine($"Area: {rectangle.GetArea()}");
        }
    }

    // ✅ GOOD EXAMPLE: Following LSP
    // This approach ensures that all shapes can be substituted for each other

    // Abstract base class - defines the contract for all shapes
    public abstract class Shape
    {
        public abstract int GetArea();
    }

    // Rectangle implementation - width and height are independent
    public class GoodRectangle : Shape
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public override int GetArea()
        {
            return Width * Height;
        }
    }

    // Square implementation - side is independent
    public class GoodSquare : Shape
    {
        public int Side { get; set; }

        public override int GetArea()
        {
            return Side * Side;
        }
    }

    // ✅ GOOD EXAMPLE: This works correctly with LSP
    // All shapes can be substituted for each other
    public class GoodAreaCalculator
    {
        public static void TestShape(Shape shape)
        {
            // This works correctly for any Shape subtype
            // No matter what shape we pass, it will work as expected
            Console.WriteLine($"Area: {shape.GetArea()}");
        }
    }

    // ✅ GOOD EXAMPLE: Interface-based approach
    // Using interfaces instead of inheritance for better LSP compliance

    // Interface defines the contract for all shapes
    public interface IShape
    {
        int GetArea();
    }

    // Rectangle implements the interface
    public class RectangleShape : IShape
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public int GetArea()
        {
            return Width * Height;
        }
    }

    // Square implements the interface
    public class SquareShape : IShape
    {
        public int Side { get; set; }

        public int GetArea()
        {
            return Side * Side;
        }
    }

    // Circle implements the interface
    public class CircleShape : IShape
    {
        public int Radius { get; set; }

        public int GetArea()
        {
            return (int)(Math.PI * Radius * Radius);
        }
    }

    // ✅ GOOD EXAMPLE: All shapes can be substituted
    // This method works with any IShape implementation
    public class ShapeCalculator
    {
        public static int CalculateTotalArea(List<IShape> shapes)
        {
            // All shapes can be substituted - they all implement GetArea()
            return shapes.Sum(shape => shape.GetArea());
        }
    }

    // ===========================================
    // HOW TO USE THE LSP EXAMPLE
    // ===========================================
    // Step 1: Create different shapes
    // var rectangle = new RectangleShape { Width = 4, Height = 5 };
    // var square = new SquareShape { Side = 4 };
    // var circle = new CircleShape { Radius = 3 };
    //
    // Step 2: Use them interchangeably
    // var shapes = new List<IShape> { rectangle, square, circle };
    // var totalArea = ShapeCalculator.CalculateTotalArea(shapes);
    //
    // Step 3: All shapes work correctly - no surprises!

    // ===========================================
    // BENEFITS OF APPLYING LSP
    // ===========================================
    // 1. PREDICTABLE BEHAVIOR: Subtypes behave as expected
    // 2. EASIER TESTING: You can test with any subtype
    // 3. BETTER MAINTAINABILITY: Changes to subtypes don't break existing code
    // 4. POLYMORPHISM: You can use subtypes anywhere the base type is expected
    // 5. CONTRACT COMPLIANCE: All subtypes follow the same contract
    // 6. FLEXIBILITY: Easy to add new subtypes without changing existing code
} 