using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Chamado
    {
        public  int Codigochamado {  get; set; }
        public DateTime Datasolicitacao { get; set; }
        public string Ocorrencia { get; set; }
        public string Problema { get; set; }
        public bool concluido {  get; set; }
        public int fk_Cliente_CodigoCliente { get; set; }
        public int fk_Tecnicos_CodigoTecnico { get; set; }



    }
}
