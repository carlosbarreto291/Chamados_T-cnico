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
    public partial class frmTecnicoAdicionar : Form
    {
        string _conexao = ChamadosTecnicosTec55.Properties.Settings.Default.Conexao;
        public frmTecnicoAdicionar()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            //cahama o objeto cliente 
            Tecnico tecnico = new Tecnico();
           Tecnicosdao tecnicosdao= new Tecnicosdao(_conexao);

            if (string.IsNullOrWhiteSpace(txtNome.Text) ||
                string.IsNullOrWhiteSpace(txtespecialidade.Text) ||
                string.IsNullOrWhiteSpace(txtemail.Text) ||
                string.IsNullOrWhiteSpace(txtsenha.Text) ||
                string.IsNullOrWhiteSpace(txtobs.Text))
            {
                MessageBox.Show("campos em branco obrigatorio");
            }
            else
            {
                // toda vez que mexer com bd usar try
                try
                {
                    //preenche o objeto cliente
                    tecnico.Nome = txtNome.Text;
                    tecnico.Especialidade = txtespecialidade.Text;
                    tecnico.Email = txtemail.Text;
                    tecnico.Senha = txtsenha.Text;
                    tecnico.obs = txtobs.Text;

                    //chama o dao para incluir o paciente 9
                    tecnicosdao.IncluiTecnico(tecnico);

                    MessageBox.Show("cadastro com sucesso !");

                    this.Close();



                }
                catch (Exception ex)
                {
                    MessageBox.Show("erro ao cadastrar" + ex, "atencao",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);




                }

            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNome.Clear();
            txtobs.Clear();
            txtespecialidade.Clear();
            txtsenha.Clear();
            txtemail.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
