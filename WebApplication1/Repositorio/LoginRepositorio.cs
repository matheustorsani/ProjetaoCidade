using MySql.Data.MySqlClient;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositorio
{
    public class LoginRepositorio(IConfiguration configuration)
    {
        private readonly string _connectSql = configuration.GetConnectionString("connectSql");


        public Usuario ObterUsuario(string email)
        {
            using (var connection = new MySqlConnection(_connectSql))
            {
                connection.Open();
                MySqlCommand cmd = new("Select * from tbUsuario where email = @email", connection);
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;

                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    Usuario usuario = null;
                    if (dr.Read())
                    {
                        usuario = new Usuario()
                        {
                            id = Convert.ToInt32(dr["id"]),
                            nome = dr["nome"].ToString(),
                            email = dr["email"].ToString(),
                            senha = dr["senha"].ToString()
                        };
                    }
                    return usuario;
                }
            }
        }
    }
}
