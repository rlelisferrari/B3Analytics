using System.Collections.Generic;

namespace Aspnet_AuthCookies1.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public IEnumerable<Usuario> GetUsuarios()
        {
            return new List<Usuario>()
            {
                new Usuario {  Id = 1, Nome = "Emerson",
                    Login = "emerson", Email = "admin@teste.com",
                    Password = "123" },
                new Usuario {  Id = 2, Nome = "Admin",
                    Login = "admin", Email = "emerson@teste.com",
                    Password = "123" }
            };
        }
    }
}
