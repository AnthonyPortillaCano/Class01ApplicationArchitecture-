namespace SOLIDExamples
{

    //BAD EXAMPLE
    public class BadUserManager
    {
        public void CreateUser(string name,string email)
        {
            // Responsability 1: Business logic validation
            if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Name and email are required");
            }
            // Responsability 2: Data persistence (database operations)
            var connectionString = "Server=localhost;Database=Users";
            //... database code here

            // Responsability 3: Email notification
            var smtpClient = new System.Net.Mail.SmtpClient();
            // .. email code here

            // Responsability 4: logging
            var logger = new SrpFileLogger();
            logger.Log($"User {name} created");
        }
        
    }

    // Good Example:Classes with single responsabilities
    //Each class now has only one reason to change

    //Responsibility:Represent user data
    public class SrpUser
    {
        public string Name { get; set; }=string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    //Responsibility:Validate user data
    public class SrpUserValidator
    {
        public bool IsValid (SrpUser user)
        {
            return  !string.IsNullOrEmpty(user.Name) && !string.IsNullOrEmpty(user.Email) && user.Email.Contains("@");
        }
    }

    //Responsibility :Define data access contract
    public interface ISrpUserRepository
    {
        void Save(SrpUser user);
    }
    // Responsibility:Handle data persistence
    public class SrpUserRepository : ISrpUserRepository
    {
        public void Save(SrpUser user)
        {
            Console.WriteLine($"Saving user {user.Name} to database");
        }
    }
    //Responsibility:Define email service contract
    public interface ISrpEmailService
    {
        void SendWelcomeEmail(string email);
    }
    //Responsibility:Handle email operations

    public class SrpEmailService : ISrpEmailService
    {
        public void SendWelcomeEmail(string email)
        {
            //Only responsible for sending emails
            Console.WriteLine($"Sending welcome email to {email}");
        }
    }
    // Responsibility:Define Logging Contract
    public interface ISrpLogger 
    { 
       void Log(string message);
    }

    // Responsibility:Handle Logging operations
    public class SrpFileLogger : ISrpLogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"LOG:{message}");
        }
    }

    //GOOD Example :UserManager with single responsibility
    //Reponsibility:Orchestrate user creation process
    //This class coordinates the work but does not do the actual work
    public class SrpUserManager
    {
        private readonly ISrpUserRepository _repository;
        private readonly ISrpEmailService _emailService;
        private readonly ISrpLogger _logger;
        private readonly SrpUserValidator _validator;
        //Contructor injection - dependencies are injected
        public SrpUserManager(ISrpUserRepository repository,ISrpEmailService emailService,ISrpLogger logger)
        {
            _repository = repository;
            _emailService = emailService;
            _logger = logger;
            _validator = new SrpUserValidator();
        }
        public void CreateUser(SrpUser user)
        {
            // Step 1 : Validate user data
             if(!_validator.IsValid(user))
            {
                throw new ArgumentException("Invalid user data");
            }

             // Step 2: Save user to database
             _repository.Save(user);

            // Step 3: Send welcome email

             _emailService.SendWelcomeEmail(user.Email);

            // Step 4: Log the operation
            _logger.Log($"User {user.Name} created successfully");
        }
    }


    // BENEFITS OF APPLYING SRP
    // 1. EASY TO UNDERSTAND: Each class has a clear purpose
    // 2. EASY TO TEST : You can test each responsibility in insolation
    // 3. EASY TO MAINTAIN: Changes to one responsibility don´t affect others
    // 4. EASY TO REUSE: You can reuse classes in different contexts
    // 5. EASY TO EXTEND: You can add new functionality without modifying existing code.

}
