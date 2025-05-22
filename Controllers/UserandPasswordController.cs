using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace YourNamespace.Controllers  // Replace with your actual namespace

{ 

    [Route("api/[controller]")] // means localhost:5001/api/UserAndPassword
    [ApiController]
    public class UserAndPasswordController : ControllerBase
    {
        private readonly UserContext _context;

        public  UserAndPasswordController(UserContext context)
        {
            _context = context;
        }

        // GET: api/UserAndPassword
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
        
            return await _context.Users.ToListAsync();
        }

        // GET: api/UserAndPassword/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/UserAndPassword/register
        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterUser(User user)
        {
            // Make sure the password is securely hashed before saving (not shown here)
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // POST: api/UserAndPassword/login
        [HttpPost("login")]
        public async Task<ActionResult<User>> LoginUser([FromBody] LoginCredentials credentials)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == credentials.Username);

            if (user == null || user.Password != credentials.Password) // Password comparison should be done securely (e.g., using hashing)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            return Ok(new { message = "Login successful" });
        }

        // PUT: api/UserAndPassword/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(long id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/UserAndPassword/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}




