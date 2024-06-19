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
        private int n;
        private int filaDesde;
        private List<string> medias;
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



        private void buttonInicioSim_Click(object sender, EventArgs e)
        {
            this.n = int.Parse(txtNroFilaTotal.Text.ToString());
            this.filaDesde = int.Parse(txtNroDesdeFila.Text.ToString());

            this.medias.Clear();

            this.medias.Add(txtLLegadaCaja.Text);
            this.medias.Add(txtLLegadaAtPers.Text);
            this.medias.Add(txtLLegadaTarjeta.Text);
            this.medias.Add(txtLLegadaPlazoFijo.Text);
            this.medias.Add(txtLLegadaPrestamos.Text);
            this.medias.Add(txtFinCaja.Text);
            this.medias.Add(txtFinAtPers.Text);
            this.medias.Add(txtFinTarjeta.Text);
            this.medias.Add(txtFinPlazoFijo.Text);
            this.medias.Add(txtFinPrestamo.Text);
            this.medias.Add(txtFinServicioAdicional.Text);

            Simulacion f = new Simulacion(this.n , this.filaDesde, this.medias);
            f.Show();
        }
    }
}
