
    using Microsoft.EntityFrameworkCore;
    
        // Define a model for user login credentials
    public class LoginCredentials
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

    // The User model (for reference, modify as needed)
    public class User
    {
        public long Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }  // You should hash the password in a real-world scenario
    }

    // Define the UserContext (for reference)
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }

