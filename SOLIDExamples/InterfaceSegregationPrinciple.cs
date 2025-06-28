namespace SOLIDExamples
{
    // ===========================================
    // INTERFACE SEGREGATION PRINCIPLE (ISP)
    // ===========================================
    // Clients should not be forced to depend on interfaces they do not use
    //
    // PROBLEM: When interfaces are too large (fat interfaces), clients are forced
    // to implement methods they don't need or can't use
    // This can:
    // - Force clients to implement unused methods
    // - Create unnecessary coupling
    // - Make interfaces hard to understand
    // - Violate the Single Responsibility Principle
    //
    // SOLUTION: Create smaller, more focused interfaces that clients actually need

    // ❌ BAD EXAMPLE: Fat interface forcing clients to implement unused methods
    // This interface is too large and forces all implementations to handle all methods
    public interface IWorker
    {
        void Work();
        void Eat();
        void Sleep();
        void GetPaid();
        void TakeVacation();
        void AttendMeeting();
        void WriteCode();
        void DesignArchitecture();
        void TestCode();
        void Deploy();
    }

    // ❌ BAD EXAMPLE: Classes forced to implement unused methods
    // Human worker can't write code, design architecture, test, or deploy
    public class HumanWorker : IWorker
    {
        public void Work() => Console.WriteLine("Human working");
        public void Eat() => Console.WriteLine("Human eating");
        public void Sleep() => Console.WriteLine("Human sleeping");
        public void GetPaid() => Console.WriteLine("Human getting paid");
        public void TakeVacation() => Console.WriteLine("Human taking vacation");
        public void AttendMeeting() => Console.WriteLine("Human attending meeting");
        
        // These methods don't make sense for a human worker
        // But we're forced to implement them due to the fat interface
        public void WriteCode() => throw new NotImplementedException("Humans don't write code");
        public void DesignArchitecture() => throw new NotImplementedException("Humans don't design architecture");
        public void TestCode() => throw new NotImplementedException("Humans don't test code");
        public void Deploy() => throw new NotImplementedException("Humans don't deploy");
    }

    // Robot worker can't eat, sleep, get paid, take vacation, or attend meetings
    public class RobotWorker : IWorker
    {
        public void WriteCode() => Console.WriteLine("Robot writing code");
        public void DesignArchitecture() => Console.WriteLine("Robot designing architecture");
        public void TestCode() => Console.WriteLine("Robot testing code");
        public void Deploy() => Console.WriteLine("Robot deploying");
        
        // These methods don't make sense for a robot
        // But we're forced to implement them due to the fat interface
        public void Work() => throw new NotImplementedException("Robots don't work like humans");
        public void Eat() => throw new NotImplementedException("Robots don't eat");
        public void Sleep() => throw new NotImplementedException("Robots don't sleep");
        public void GetPaid() => throw new NotImplementedException("Robots don't get paid");
        public void TakeVacation() => throw new NotImplementedException("Robots don't take vacation");
        public void AttendMeeting() => throw new NotImplementedException("Robots don't attend meetings");
    }

    // ✅ GOOD EXAMPLE: Segregated interfaces
    // Each interface has a single, focused responsibility
    // Clients only implement the interfaces they actually need

    // Interface for entities that can work
    public interface IWorkable
    {
        void Work();
    }

    // Interface for entities that can eat
    public interface IEatable
    {
        void Eat();
    }

    // Interface for entities that can sleep
    public interface ISleepable
    {
        void Sleep();
    }

    // Interface for entities that can get paid
    public interface IPayable
    {
        void GetPaid();
    }

    // Interface for entities that can take vacation
    public interface IVacationable
    {
        void TakeVacation();
    }

    // Interface for entities that can attend meetings
    public interface IMeetingAttendable
    {
        void AttendMeeting();
    }

    // Interface for entities that can write code
    public interface ICodeWritable
    {
        void WriteCode();
    }

    // Interface for entities that can design architecture
    public interface IArchitectureDesignable
    {
        void DesignArchitecture();
    }

    // Interface for entities that can test code
    public interface ICodeTestable
    {
        void TestCode();
    }

    // Interface for entities that can deploy
    public interface IDeployable
    {
        void Deploy();
    }

    // ✅ GOOD EXAMPLE: Human implements only relevant interfaces
    // Human worker only implements interfaces that make sense for humans
    public class GoodHumanWorker : IWorkable, IEatable, ISleepable, IPayable, IVacationable, IMeetingAttendable
    {
        public void Work() => Console.WriteLine("Human working");
        public void Eat() => Console.WriteLine("Human eating");
        public void Sleep() => Console.WriteLine("Human sleeping");
        public void GetPaid() => Console.WriteLine("Human getting paid");
        public void TakeVacation() => Console.WriteLine("Human taking vacation");
        public void AttendMeeting() => Console.WriteLine("Human attending meeting");
    }

    // ✅ GOOD EXAMPLE: Robot implements only relevant interfaces
    // Robot worker only implements interfaces that make sense for robots
    public class GoodRobotWorker : IWorkable, ICodeWritable, IArchitectureDesignable, ICodeTestable, IDeployable
    {
        public void Work() => Console.WriteLine("Robot working");
        public void WriteCode() => Console.WriteLine("Robot writing code");
        public void DesignArchitecture() => Console.WriteLine("Robot designing architecture");
        public void TestCode() => Console.WriteLine("Robot testing code");
        public void Deploy() => Console.WriteLine("Robot deploying");
    }

    // ✅ GOOD EXAMPLE: Developer implements relevant interfaces
    // Developer can do both human and technical tasks
    public class Developer : IWorkable, IEatable, ISleepable, IPayable, IVacationable, IMeetingAttendable, ICodeWritable, ICodeTestable
    {
        public void Work() => Console.WriteLine("Developer working");
        public void Eat() => Console.WriteLine("Developer eating");
        public void Sleep() => Console.WriteLine("Developer sleeping");
        public void GetPaid() => Console.WriteLine("Developer getting paid");
        public void TakeVacation() => Console.WriteLine("Developer taking vacation");
        public void AttendMeeting() => Console.WriteLine("Developer attending meeting");
        public void WriteCode() => Console.WriteLine("Developer writing code");
        public void TestCode() => Console.WriteLine("Developer testing code");
    }

    // ✅ GOOD EXAMPLE: Using specific interfaces
    // Each manager only depends on the interfaces it actually needs

    // Work manager only cares about work
    public class WorkManager
    {
        public void ManageWork(IWorkable worker)
        {
            worker.Work();
        }
    }

    // Code manager only cares about code writing
    public class CodeManager
    {
        public void ManageCode(ICodeWritable coder)
        {
            coder.WriteCode();
        }
    }

    // Human resource manager cares about human-specific activities
    public class HumanResourceManager
    {
        public void ManageHuman(IWorkable worker, IEatable eater, ISleepable sleeper)
        {
            worker.Work();
            eater.Eat();
            sleeper.Sleep();
        }
    }

    // ===========================================
    // HOW TO USE THE ISP EXAMPLE
    // ===========================================
    // Step 1: Create workers with specific capabilities
    // var human = new GoodHumanWorker();
    // var robot = new GoodRobotWorker();
    // var developer = new Developer();
    //
    // Step 2: Use them with specific managers
    // var workManager = new WorkManager();
    // workManager.ManageWork(human);    // Works
    // workManager.ManageWork(robot);    // Works
    // workManager.ManageWork(developer); // Works
    //
    // Step 3: Each worker only implements what it can do
    // No NotImplementedException thrown!

    // ===========================================
    // BENEFITS OF APPLYING ISP
    // ===========================================
    // 1. FOCUSED INTERFACES: Each interface has a single responsibility
    // 2. NO UNUSED METHODS: Clients only implement what they need
    // 3. BETTER COUPLING: Clients depend only on what they use
    // 4. EASIER TESTING: You can mock only the interfaces you need
    // 5. FLEXIBILITY: Easy to combine interfaces for different capabilities
    // 6. MAINTAINABILITY: Changes to one interface don't affect others
} 