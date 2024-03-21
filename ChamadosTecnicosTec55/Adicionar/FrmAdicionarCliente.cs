using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChamadosTecnicosTec55.Adicionar
{
    public partial class FrmAdicionarCliente : Form
    {
        //Chamada a conexao 
        string _conexao = ChamadosTecnicosTec55.Properties.Settings.Default.Conexao;
        public FrmAdicionarCliente()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txbNome.Clear();
            txbObs.Clear();
            txbProfissao.Clear();
            txbSetor.Clear();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            //cahama o objeto cliente 
            Cliente cliente = new Cliente();
            ClienteDao clientedao = new ClienteDao(_conexao);

            if (string.IsNullOrWhiteSpace(txbNome.Text) ||
                string.IsNullOrWhiteSpace(txbObs.Text) ||
                string.IsNullOrWhiteSpace(txbProfissao.Text) ||
                string.IsNullOrWhiteSpace(txbSetor.Text)) 
            {
                MessageBox.Show("campos em branco obrigatorio");
            }
            else 
            {
                // toda vez que mexer com bd usar try
                try 
                {
                    //preenche o objeto cliente
                    cliente.Nome = txbNome.Text;
                    cliente.Profissao = txbProfissao.Text;
                    cliente.Obs = txbObs.Text;
                    cliente.setor = txbSetor.Text;
                    
                    //chama o dao para incluir o paciente 9
                    clientedao.IncluiCliente(cliente);
                
                
                
                }catch(Exception ex)
                {
                    MessageBox.Show("erro ao cadastrar"+ex,"atencao",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                
                
                
                
                }
                finally 
                {
                    MessageBox.Show("cadastro com sucesso !");
                
                
                
                }
               
                   
                
                
                
            
            }



                  


           
        }
    }
}
