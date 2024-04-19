using ChamadosTecnicosTec55.Adicionar;
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

namespace ChamadosTecnicosTec55
{
    public partial class FrmGerirTecnico : Form
    {
        public FrmGerirTecnico()
        {
            InitializeComponent();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            var frmaddTecnico = new frmTecnicoAdicionar();
            frmaddTecnico.Show();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        { //verifique se alguma linha esta selecionada no dgv
            if (dgvTecnico.SelectedRows.Count > 0)
            {
                //obtem o codico do cliente da linha selecionada 
                int codigo = Convert.ToInt32(dgvTecnico.CurrentRow.Cells[0].Value);

                var frmAlterarTecnico = new frmAlterarTecnico(codigo);
                frmAlterarTecnico.ShowDialog();


                //apos a tela fechar a listar os clientes cadastrados 
                listarTecnico();

            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //botao excluir
            //selecionar data grid, capturar ID, Enviar para o DAO, Excluir
            if (dgvTecnico.SelectedRows.Count > 0)
            {
                int codigo = Convert.ToInt32(dgvTecnico.CurrentRow.Cells[0].Value);

                var resultado = MessageBox.Show("deseja excluir ?", "pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (resultado == DialogResult.Yes)
                {
                    TecnicoDao Tecnico = new TecnicoDao(_conexao);
                    Tecnico.ExcluirTecnico(codigo);
                    listarTecnico();

                }



            }
            else
            {


            }
        }
    }
}
