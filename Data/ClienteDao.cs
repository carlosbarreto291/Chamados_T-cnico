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
            using(SqlConnection conexaoBd = new SqlConnection(_conexao))
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
        public DataSet buscacliente (string pesquisa = "") 
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


    }
}
