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
        private List<int> medias;
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

            List<int> listaMedias = new List<int>();

            listaMedias.Add(int.Parse(txtLLegadaCaja.Text));
            listaMedias.Add(int.Parse(txtLLegadaAtPers.Text));
            listaMedias.Add(int.Parse(txtLLegadaTarjeta.Text));
            listaMedias.Add(int.Parse(txtLLegadaPlazoFijo.Text));
            listaMedias.Add(int.Parse(txtLLegadaPrestamos.Text));
            listaMedias.Add(int.Parse(txtFinCaja.Text));
            listaMedias.Add(int.Parse(txtFinAtPers.Text));
            listaMedias.Add(int.Parse(txtFinTarjeta.Text));
            listaMedias.Add(int.Parse(txtFinPlazoFijo.Text));
            listaMedias.Add(int.Parse(txtFinPrestamo.Text));
            listaMedias.Add(int.Parse(txtFinServicioAdicional.Text));

            this.medias = listaMedias;

            //falta crear un objeto fila con la carga incial que tenga los datos de inicio y mandar eso por
            // parametro a la simulacion.

            Simulacion f = new Simulacion(this.n , this.filaDesde, this.medias);
            f.Show();
        }
    }
}
