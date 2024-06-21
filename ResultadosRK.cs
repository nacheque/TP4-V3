using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace borrador_de_tp4
{
    public partial class ResultadosRK : Form
    {
        public RK rk { get; set; }
        public ResultadosRK(RK rk)
        {
            InitializeComponent();
            this.rk = rk;
        }

        private void ResultadosRK_Load(object sender, EventArgs e)
        {
            foreach (var resultado in rk.Resultados)
            {
                dataGridView1.Rows.Add(resultado.X0, resultado.X, resultado.Y, resultado.Tiempo);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
