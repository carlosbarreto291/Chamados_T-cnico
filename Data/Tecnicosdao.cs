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
    public class Tecnicosdao
    {
        private string _conexao;


        // Metodo Construtor => Inicializa Objeto Buscando Conexao 
        public Tecnicosdao(string conexao)
        {
            // Receba conexão
            _conexao = conexao;
        }

        //Inserir Cliente vulgo XUXAR
        public void IncluiTecnico(Tecnico tecnico)
        {
            using (SqlConnection conexaoBd = new SqlConnection(_conexao))
            {
                string sql = "insert into Tecnicos (nome,especialidade,email,senha,obs) values (@nome,@especialidade,@email,@senha,@obs)";
                using (SqlCommand comando = new SqlCommand(sql, conexaoBd))
                {
                    comando.Parameters.AddWithValue("@nome", tecnico.Nome);
                    comando.Parameters.AddWithValue("@especialidade", tecnico.Especialidade);
                    comando.Parameters.AddWithValue("@email", tecnico.Email);
                    comando.Parameters.AddWithValue("@senha", tecnico.Senha);
                    comando.Parameters.AddWithValue("@obs", tecnico.obs);

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
