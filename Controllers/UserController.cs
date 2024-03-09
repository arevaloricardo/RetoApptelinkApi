using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetoApptelinkApi.Data;
using RetoApptelinkApi.Models;

namespace RetoApptelinkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyDbContext _context;

        public UserController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id_User)
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id_User }, user);
        }

        // DELETE: api/User/5   
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
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

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id_User == id);
        }
        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel credentials)
        {
            var username = credentials.Username;
            var password = credentials.Password;

            if (string.IsNullOrEmpty(username))
            {
                return BadRequest(new { message = "Ingrese su usuario" });
            }

            if (string.IsNullOrEmpty(password))
            {
                return BadRequest(new { message = "Ingrese una contraseña" });
            }


            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email_User == username);

            if (user == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }


            // Aquí se implementaría la revisión del hash de la contraseña

            if (user.Password_User != password)
            {
                user.Login_Attempts_User += 1;

                if (user.Login_Attempts_User >= 3)
                {
                    user.Is_Active_User = 3; // Indica que el usuario está bloqueado
                    await _context.SaveChangesAsync();
                    return StatusCode(StatusCodes.Status429TooManyRequests, new { message = "Tu cuenta ha sido bloqueada. intentaste loguearte muchas veces" });
                }
                else
                {
                    await _context.SaveChangesAsync();
                    return Unauthorized(new { message = "La contraseña es incorrecta" });
                }
            }

            if (user.Password_User == password && user.Is_Active_User < 3)
            {
                user.Login_Attempts_User = 0;
                user.Is_Active_User = 1; // Indica que el usuario está activo
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    login = "ok",
                    data = new
                    {
                        Id_User = user.Id_User,
                        Name_User = user.Name_User,
                        Email_User = user.Email_User,
                        Is_Active_User = user.Is_Active_User
                    }
                });
            }

            return StatusCode(StatusCodes.Status429TooManyRequests, new { message = "Tu cuenta ha sido bloqueada. intentaste loguearte muchas veces" });
        }




    }
}
