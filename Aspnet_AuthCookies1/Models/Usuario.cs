using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aspnet_AuthCookies1.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public async Task<IEnumerable<Usuario>> GetUsuarios(string connString)
        {
            var users = new List<Usuario>();
            using var connection = new MySqlConnection(connString);
            await connection.OpenAsync();
            using var command = new MySqlCommand("SELECT * FROM `usuarios`", connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var user = new Usuario();
                user.Id = Convert.ToInt32(reader.GetValue(0));
                user.Nome = reader.GetString(1);
                user.Login = reader.GetString(2);
                user.Email = reader.GetString(3);
                user.Password = reader.GetString(4);
                users.Add(user);
            }

            return users;
        }
    }
}
