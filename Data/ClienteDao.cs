using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; // ADO.net
using System.Data.SqlClient;
using System.Xml.Schema; // ADO para SQL SERVER

namespace Data
{
    public class ClienteDao
    {
        private string _conexao;

        // Metodo Construtor => Inicializa Objeto buscando Conexao
        public ClienteDao(string conexao)
        {
            // RECEBA Conexão 
            _conexao = conexao;
        }

        // Inserir Cliente Vulgo XUXAR
        public void IncluiCliente(Cliente cliente)
        {
            using (SqlConnection conexaoBd = new SqlConnection(_conexao))
            {
                string sql = "insert into Clientes (nome,profissao,setor,obs) values (@nome,@profissao,@setor,@obs)";

                using (SqlCommand comando = new SqlCommand(sql, conexaoBd))
                {
                    comando.Parameters.AddWithValue("@nome", cliente.Nome);
                    comando.Parameters.AddWithValue("@profissao", cliente.Profissao);
                    comando.Parameters.AddWithValue("@setor", cliente.Setor);
                    comando.Parameters.AddWithValue("@obs", cliente.Obs);

                    try
                    {
                        conexaoBd.Open();
                        comando.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao Incluir Cliente:" + ex.Message);
                    }
                }

            }
        }
        public DataSet buscacliente(string pesquisa = "")
        {
            // constante con o Código SQL que faz bussca a partir do texto 
            const string query = "select * From Clientes Where Nome Like @Pesquisa";


            //Validar erros
            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                using (var adaptador = new SqlDataAdapter(comando))
                {
                    string parametroPesquisa = $"%{pesquisa}%";
                    comando.Parameters.AddWithValue("@pesquisa", parametroPesquisa);
                    conexaoBd.Open();
                    var dsclientes = new DataSet();
                    adaptador.Fill(dsclientes, "clientes");
                    return dsclientes;

                }


            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar clientes: {ex.Message}");


            }








        }
        public Cliente ObtemCliente(int codigoCliente)
        {
            // Definir o sql para obter o cliente
            const string query = @"select * from Clientes where
                            CodigoCliente = @CodigoCliente";
            Cliente cliente = null;

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {
                    comando.Parameters.AddWithValue("@CodigoCliente", codigoCliente);
                    conexaoBd.Open();
                    using (var reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cliente = new Cliente
                            {
                                CodigoCliente = Convert.ToInt32(reader["CodigoCliente"]),
                                Nome = reader["Nome"].ToString(),
                                Profissao = reader["Profissao"].ToString(),
                                Setor = reader["Setor"].ToString(),
                                Obs = reader["Obs"].ToString(),
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter o cliente {ex.Message}", ex);
            }
            return cliente;
        }

        public void AlterarCliente(Cliente cliente)
        {
            const string query = @"update Clientes set nome=@Nome,
                             Setor = @Setor,
                             Profissao = @Profissao,
                             Observacao = @Observacao,
                             where CodigoCliente = @CodCliente";

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {
                    comando.Parameters.AddWithValue("@Nome", cliente.Nome);
                    comando.Parameters.AddWithValue("@Setor", cliente.Setor);
                    comando.Parameters.AddWithValue("@Profissao", cliente.Profissao);
                    comando.Parameters.AddWithValue("@Obs", cliente.Obs);
                    conexaoBd.Open();
                    comando.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro{ex}");
            }

        }
        //Exclui cliente 
        public void Excluircliente(int codigoCliente) 
        {
            const string query = @"delete from clientes where codigocliente = @codigocliente";

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {
                    comando.Parameters.AddWithValue("codigocliente", codigoCliente);
                    conexaoBd.Open();
                    comando.ExecuteNonQuery();

                }



            }
            catch (Exception ex)
            {
                throw new Exception ( $"erro ao excluir cliente {ex.Message}" ,ex);
            
            
            
            }

        
        
        
        
        
        }




    }
}

