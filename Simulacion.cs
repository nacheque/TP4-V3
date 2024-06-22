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
    public partial class Simulacion : Form
    {

        private int cantidadFilas;
        private int filaDesde;
        private List<int> listaMedias; //hay 11 medias que tienen correspondencia directa a la asignacion en el incio
        public Simulacion(int n, int filaDesde, List<int> listaMedias)
        {
            InitializeComponent();
            this.cantidadFilas = n;
            this.filaDesde = filaDesde;
            this.listaMedias = listaMedias;

            //generar fila 0



        }

        private Fila GenerarFila0()
        {
            //primero hay que generar todas las proximas llegadas de los 6 servicios

            Llegada llegadaCaja = new Llegada();
            Llegada llegadaAtencionPersonalizada = new Llegada();
            Llegada llegadaTarjeta = new Llegada();
            Llegada llegadaPlazoFijo = new Llegada();
            Llegada llegadaPrestamos = new Llegada();
            //Llegada llegadaServicioAdicional = new Llegada();

            List<Llegada> llegadas = new List<Llegada> { llegadaCaja, llegadaAtencionPersonalizada, llegadaTarjeta, llegadaPlazoFijo, llegadaPrestamos };
            for (int i = 0; i < 5; i++)
            {
                Random rnd = new Random();
                double tiempoEntreLlegada = -(listaMedias[i]) * Math.Log(1 - (double)rnd.NextDouble());
                llegadas[i].TiempoEntreLlegada = tiempoEntreLlegada;
                llegadas[i].ProximaLlegada = tiempoEntreLlegada;
                llegadas[i].Media = this.listaMedias[i];
            }

            Cola colaCaja = new Cola();
            Cola colaAtencionPersonalizada = new Cola();
            Cola colaTarjetaCredito = new Cola();
            Cola colaPlazoFijo = new Cola();
            Cola colaPrestamos = new Cola();
            Cola colaServicioAdicional = new Cola();

            List<Cola> colas = new List<Cola> { colaCaja, colaAtencionPersonalizada, colaTarjetaCredito, colaPlazoFijo, colaPrestamos, colaServicioAdicional };

            List<string> estadoCaja = new List<string> { "Libre", "Libre", "Libre", "Libre" };
            List<string> estadoAtencionPersonalizada = new List<string> { "Libre", "Libre", "Libre"};
            List<string> estadoTarjetaCredito = new List<string> { "Libre", "Libre"};
            List<string> estadoPlazoFijo = new List<string> { "Libre"};
            List<string> estadoPrestamos = new List<string> { "Libre", "Libre"};
            List<string> estadoServicioAdicional = new List<string> { "Libre", "Libre"};
            List<List<string>> estados = new List<List<string>> { estadoCaja, estadoAtencionPersonalizada, estadoTarjetaCredito, estadoPlazoFijo, estadoPrestamos, estadoServicioAdicional };

            List<ClienteTemporal> clientesTemporales = new List<ClienteTemporal>();
            Fila fila = new Fila();
            fila.Reloj = 0.0;
            fila.Evento = "Inicialización";
            fila.Llegada = llegadas;
            fila.Colas = colas;
            fila.Estados = estados;
            fila.ClientesTemporales = clientesTemporales;

            return fila;
        }

        private void Simulacion_Load(object sender, EventArgs e)
        {
            Fila filaActual = GenerarFila0();
            LlenarTabla(filaActual);
            
        }

        private void LlenarTabla(Fila fila)
        {
            grdSimulacion.Rows.Add();
            grdSimulacion.Rows[0].Cells["c1"].Value = fila.Evento;
        }

        private void GenerarProximaLlegada(Fila fila, int tipoServicio)
        {
            Random random = new Random();

            // Generar un número decimal aleatorio entre 0.01 y 0.99
            double numeroDecimalAleatorio = random.NextDouble();

            // Redondear a dos decimales
            numeroDecimalAleatorio = Math.Round(numeroDecimalAleatorio, 2);

            fila.Llegada[tipoServicio].TiempoEntreLlegada = -fila.Llegada[tipoServicio].Media * Math.Log(1 - numeroDecimalAleatorio);
            fila.Llegada[tipoServicio].ProximaLlegada = fila.Reloj + fila.Llegada[tipoServicio].Media;
        }

        private void ComienzoLlegada(Fila fila, int tipoServicio)
        {
            Random random = new Random();
            ClienteTemporal clienteTemporal = new ClienteTemporal("En espera", 0, random.Next(1, 10000), tipoServicio, false);
        }

        private void GenerarFin(Fila fila, int tipoServicio, ClienteTemporal clienteTemporal){
            
        }

        private void ComienzoFin(){
            
        }
    }
}
