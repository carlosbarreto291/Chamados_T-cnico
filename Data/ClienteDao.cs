using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;//ADO net
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Security.Principal;// ADD para SQL SERVER



namespace Data
{
    public class ClienteDao
    {
        private string _conexao;


        // Metodo Construtor => Inicializa Objeto Buscando Conexao 
        public ClienteDao(string conexao) 
        {
            // Receba conexão
            _conexao=conexao;
        }

        //Inserir Cliente vulgo XUXAR
        public void IncluiCliente (Cliente cliente) 
        { 
            using(SqlConnection conexaoBd = new SqlConnection(_conexao))
            {
                string sql = "insert into Clientes (nome,profissao,setor) values (@nome,@profissao,@setor,@obs)";
                using (SqlCommand comando = new SqlCommand (sql, conexaoBd)) 
                {
                    comando.Parameters.AddWithValue("@nome", cliente.Nome);
                    comando.Parameters.AddWithValue("@profissao", cliente.Profissao);
                    comando.Parameters.AddWithValue("@setor", cliente.setor);
                    comando.Parameters.AddWithValue("@obs", cliente.Obs);

                    try 
                    {
                        conexaoBd.Open();
                        comando.ExecuteNonQuery();
                    }

                    catch (Exception ex) 
                    {
                        throw new Exception("Erro ao incluir CLiente:" + ex.Message);
                    }
  
                }

                
                
                    
                    
                
                
                
                
            }

        }
    }
}
