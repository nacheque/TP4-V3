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
    public partial class pantalla : Form
    {
        public pantalla()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtLLegadaCaja.Text = 30.ToString();
            txtLLegadaAtPers.Text = 12.ToString();
            txtLLegadaTarjeta.Text = 6.ToString();
            txtLLegadaPlazoFijo.Text = 4.ToString();
            txtLLegadaPrestamos.Text = 10.ToString();
            txtFinCaja.Text = 10.ToString();
            txtFinAtPers.Text = 5.ToString();
            txtFinTarjeta.Text = 3.ToString();
            txtFinPlazoFijo.Text = 2.ToString();
            txtFinPrestamo.Text = 4.ToString();
            txtFinServicioAdicional.Text = 5.ToString();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void buttonInicioSim_Click(object sender, EventArgs e)
        {
            Simulacion f = new Simulacion();
            f.Show();
        }
    }
}
