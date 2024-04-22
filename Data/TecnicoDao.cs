using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; // ADO.net
using System.Data.SqlClient; // ADO para SQL SERVER

namespace Data
{
    public class TecnicoDao
    {
        private string _conexao;

        // Metodo Construtor => Inicializa Objeto buscando Conexao
        public TecnicoDao(string conexao)
        {
            // RECEBA Conexão 
            _conexao = conexao;
        }

        public void IncluiTecnico(Tecnico tecnico)
        {
            using (SqlConnection conexaoBd = new SqlConnection(_conexao))
            {
                string sql = "insert into Tecnicos (nome,especialidade,email,senha,obs) values (@nome,@especialdiade,@email,@senha,@obs)";

                using (SqlCommand comando = new SqlCommand(sql, conexaoBd))
                {
                    comando.Parameters.AddWithValue("@nome", tecnico.Nome);
                    comando.Parameters.AddWithValue("@especialdiade", tecnico.Especialidade);
                    comando.Parameters.AddWithValue("@email", tecnico.Email);
                    comando.Parameters.AddWithValue("@senha", tecnico.Senha);
                    comando.Parameters.AddWithValue("@obs", tecnico.Obs);

                    try
                    {
                        conexaoBd.Open();
                        comando.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao Incluir Tecnico:" + ex.Message);
                    }
                }

            }
        }
        public DataSet buscaTecnico(string pesquisa = "")
        {
            // constante con o Código SQL que faz bussca a partir do texto 
            const string query = "select * From Where Nome Like @Pesquisa";


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
                    var dscTecnico = new DataSet();
                    adaptador.Fill(dscTecnico, "Tecnico");
                    return dscTecnico;

                }


            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar Tecnico: {ex.Message}");

            }

        }
        public Tecnico ObtemTecnico(int codigoTecnico)
        {
            // Definir o sql para obter o cliente
            const string query = @"select * from Tecnico where
                            CodigoTecnico = @CodigoTecnico";
            Tecnico Tecnico = null;

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {
                    comando.Parameters.AddWithValue("@CodigoTecnico", codigoTecnico);
                    conexaoBd.Open();
                    using (var reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Tecnico = new Tecnico
                            {
                                CodigoTecnico = Convert.ToInt32(reader["CodigoCliente"]),
                                Nome = reader["Nome"].ToString(),
                                Especialidade = reader["especialidade"].ToString(),
                                Email = reader["Email"].ToString(),
                                Senha = reader["Senha"].ToString(),
                                Obs = reader["Observação"].ToString(),
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter o Tecnico {ex.Message}", ex);
            }
            return Tecnico;


        }
        public void AlterarTecnico(Tecnico Tecnico)
        {
            const string query = @"update Clientes set nome=@Nome,  
                             Especialidade  = @Especialidade,
                             Email = @Email,
                             Senha = @Senha,
                             Observacao = @Observacao,
                             where CodigoTecnico = @CodTEcnico";


            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {
                    comando.Parameters.AddWithValue("@Nome", Tecnico.Nome);
                    comando.Parameters.AddWithValue("@Email", Tecnico.Email);
                    comando.Parameters.AddWithValue("@Especialidade", Tecnico.Especialidade);
                    comando.Parameters.AddWithValue("@Senha", Tecnico.Senha);
                    comando.Parameters.AddWithValue("@Obs", Tecnico.Obs);
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
        public void ExcluirTecnico(int codigoTecnico)
        {
            const string query = @"delete from Tecnico where codigoTecnico = @codigoTecnico";

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {
                    comando.Parameters.AddWithValue("codigoTecnico", codigoTecnico);
                    conexaoBd.Open();
                    comando.ExecuteNonQuery();

                }



            }
            catch (Exception ex)
            {
                throw new Exception($"erro ao excluir Tecnico {ex.Message}", ex);



            }

        }
}   }