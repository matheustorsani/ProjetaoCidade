using MySql.Data.MySqlClient;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositorio
{
    public class ProdutoRepositorio(IConfiguration configuration)
    {
        private readonly string _connectSql = configuration.GetConnectionString("connectSql");

        public void Cadastrar (Produto produto)
        {
            using (var connection = new MySqlConnection(_connectSql))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("insert into tbProdutos(nome,descricao,preco,quantidade) values (@nome, @descricao, @preco, @quantidade)");
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.nome;
                cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.descricao;
                cmd.Parameters.Add("@preco", MySqlDbType.Decimal).Value = produto.preco;
                cmd.Parameters.Add("@nome", MySqlDbType.Int32).Value = produto.quantidade;
                cmd.ExecuteNonQuery();
                connection.Close();

            }
        }

        public bool Atualizar (Produto produto)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectSql))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("update tbProdutos set nome=@nome, descricao=@descricao, preco=@preco, quantidade=@quantidade");
                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = produto.id;
                    cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.nome;
                    cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.descricao;
                    cmd.Parameters.Add("@preco", MySqlDbType.Decimal).Value = produto.preco;
                    cmd.Parameters.Add("@nome", MySqlDbType.Int32).Value = produto.quantidade;

                    int lines = cmd.ExecuteNonQuery();

                    return lines > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar produto! {ex.Message}");
                return false;
            }
        }

        public IEnumerable<Produto> TodosProdutos()
        {
            List<Produto> products = new List<Produto>();

            using (var connection = new MySqlConnection(_connectSql))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbProdutos", connection);
                MySqlDataAdapter dataA = new MySqlDataAdapter(cmd);
                DataTable dataT = new DataTable();

                dataA.Fill(dataT);
                connection.Close();

                foreach (DataRow dataR in dataT.Rows)
                {
                    products.Add(
                        new Produto
                        {
                            id = Convert.ToInt32(dataR["id"]),
                            nome = ((string)dataR["nome"]),
                            descricao = ((string)dataR["descricao"]),
                            preco = Convert.ToDecimal(dataR["preco"]),
                            quantidade = Convert.ToInt32(dataR["quantidade"])
                        });
                }
                return products;
            }
        }

        public Produto ObterProdutos (int id)
        {
            using (var connection = new MySqlConnection(_connectSql))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbProdutos where id = @id", connection);
                cmd.Parameters.AddWithValue("id", id);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                Produto product = new Produto();


                while (dr.Read())
                {
                    product.id = Convert.ToInt32(dr["id"]);
                    product.nome = (string)(dr["nome"]);
                    product.descricao = (string)(dr["descricao"]);
                    product.preco = Convert.ToDecimal(dr["preco"]);
                    product.quantidade = Convert.ToInt32(dr["quantidade"]);
                }
                return product;
            }
        }


        public void Excluir (int id)
        {
            using (var connection = new MySqlConnection(_connectSql))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("delete from tbProdutos where id = @id", connection);
                cmd.Parameters.AddWithValue("id", id);
                int i = cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
