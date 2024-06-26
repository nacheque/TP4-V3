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
        private bool caja5;
        public Simulacion(int n, int filaDesde, List<int> listaMedias, bool caja5)
        {
            InitializeComponent();
            this.cantidadFilas = n;
            this.filaDesde = filaDesde;
            this.listaMedias = listaMedias;

            //generar fila 0



        }

        private Fila GenerarFila0(double reloj)
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
                tiempoEntreLlegada = Math.Round(tiempoEntreLlegada, 2);
                llegadas[i].TiempoEntreLlegada = tiempoEntreLlegada;
                llegadas[i].ProximaLlegada = tiempoEntreLlegada + reloj;
                llegadas[i].Media = this.listaMedias[i];
            }

            Cola colaCaja = new Cola();
            Cola colaAtencionPersonalizada = new Cola();
            Cola colaTarjetaCredito = new Cola();
            Cola colaPlazoFijo = new Cola();
            Cola colaPrestamos = new Cola();
            Cola colaServicioAdicional = new Cola();

            colaCaja.Clientes = new List<ClienteTemporal>();
            colaAtencionPersonalizada.Clientes = new List<ClienteTemporal>();
            colaTarjetaCredito.Clientes = new List<ClienteTemporal>();
            colaPlazoFijo.Clientes = new List<ClienteTemporal>();
            colaPrestamos.Clientes = new List<ClienteTemporal>();
            colaServicioAdicional.Clientes = new List<ClienteTemporal>();

            List<Cola> colas = new List<Cola> { colaCaja, colaAtencionPersonalizada, colaTarjetaCredito, colaPlazoFijo, colaPrestamos, colaServicioAdicional };

            List<string> estadoCaja = new List<string> { "Libre", "Libre", "Libre", "Libre" };
            List<string> estadoAtencionPersonalizada = new List<string> { "Libre", "Libre", "Libre"};
            List<string> estadoTarjetaCredito = new List<string> { "Libre", "Libre"};
            List<string> estadoPlazoFijo = new List<string> { "Libre"};
            List<string> estadoPrestamos = new List<string> { "Libre", "Libre"};
            List<string> estadoServicioAdicional = new List<string> { "Libre", "Libre"};
            List<List<string>> estados = new List<List<string>> { estadoCaja, estadoAtencionPersonalizada, estadoTarjetaCredito, estadoPlazoFijo, estadoPrestamos, estadoServicioAdicional };


            FinAtencion finAtencionCaja = new FinAtencion();
            FinAtencion finAtencionPersonalizada = new FinAtencion();
            FinAtencion finAtencionTarjetaCredito = new FinAtencion();
            FinAtencion finAtencionPlazoFijo = new FinAtencion();
            FinAtencion finAtencionPrestamos = new FinAtencion();
            FinAtencion finAtencionServicioAdicional = new FinAtencion();

            finAtencionCaja.ACtiempoAtencion = 0;
            finAtencionPersonalizada.ACtiempoAtencion = 0;
            finAtencionTarjetaCredito.ACtiempoAtencion = 0;
            finAtencionPlazoFijo.ACtiempoAtencion = 0;
            finAtencionPrestamos.ACtiempoAtencion = 0;
            finAtencionServicioAdicional.ACtiempoAtencion = 0;

            List<FinAtencion> finesAtencion = new List<FinAtencion> { finAtencionCaja, finAtencionPersonalizada, finAtencionTarjetaCredito, finAtencionPlazoFijo, finAtencionPrestamos, finAtencionServicioAdicional };



            List<ClienteTemporal> clientesTemporales = new List<ClienteTemporal>();
            Fila fila = new Fila();
            fila.Reloj = 0.0;
            fila.Evento = "Inicialización";
            fila.Llegada = llegadas;
            fila.Colas = colas;
            fila.Estados = estados;
            fila.ClientesTemporales = clientesTemporales;
            fila.FinesAtencion = finesAtencion;

            return fila;
        }

        private Fila GenerarFilaSiguiente()
        {
            //primero hay que generar todas las proximas llegadas de los 6 servicios

            Llegada llegadaCaja = new Llegada();
            Llegada llegadaAtencionPersonalizada = new Llegada();
            Llegada llegadaTarjeta = new Llegada();
            Llegada llegadaPlazoFijo = new Llegada();
            Llegada llegadaPrestamos = new Llegada();
            //Llegada llegadaServicioAdicional = new Llegada();

            List<Llegada> llegadas = new List<Llegada> { llegadaCaja, llegadaAtencionPersonalizada, llegadaTarjeta, llegadaPlazoFijo, llegadaPrestamos };            

            Cola colaCaja = new Cola();
            Cola colaAtencionPersonalizada = new Cola();
            Cola colaTarjetaCredito = new Cola();
            Cola colaPlazoFijo = new Cola();
            Cola colaPrestamos = new Cola();
            Cola colaServicioAdicional = new Cola();

            colaCaja.Clientes = new List<ClienteTemporal>();
            colaAtencionPersonalizada.Clientes = new List<ClienteTemporal>();
            colaTarjetaCredito.Clientes = new List<ClienteTemporal>();
            colaPlazoFijo.Clientes = new List<ClienteTemporal>();
            colaPrestamos.Clientes = new List<ClienteTemporal>();
            colaServicioAdicional.Clientes = new List<ClienteTemporal>();

            List<Cola> colas = new List<Cola> { colaCaja, colaAtencionPersonalizada, colaTarjetaCredito, colaPlazoFijo, colaPrestamos, colaServicioAdicional };

            FinAtencion finAtencionCaja = new FinAtencion();
            FinAtencion finAtencionPersonalizada = new FinAtencion();
            FinAtencion finAtencionTarjetaCredito = new FinAtencion();
            FinAtencion finAtencionPlazoFijo = new FinAtencion();
            FinAtencion finAtencionPrestamos = new FinAtencion();
            FinAtencion finAtencionServicioAdicional = new FinAtencion();

            ClienteTemporal clienteTemporalNulo = new ClienteTemporal("", 0, 0, 0, false);

            finAtencionCaja.Cliente = new List<ClienteTemporal> { clienteTemporalNulo, clienteTemporalNulo, clienteTemporalNulo, clienteTemporalNulo };
            finAtencionPersonalizada.Cliente = new List<ClienteTemporal> { clienteTemporalNulo, clienteTemporalNulo, clienteTemporalNulo };
            finAtencionTarjetaCredito.Cliente = new List<ClienteTemporal> { clienteTemporalNulo, clienteTemporalNulo };
            finAtencionPlazoFijo.Cliente = new List<ClienteTemporal> { clienteTemporalNulo };
            finAtencionPrestamos.Cliente = new List<ClienteTemporal> { clienteTemporalNulo, clienteTemporalNulo };
            finAtencionServicioAdicional.Cliente = new List<ClienteTemporal> { clienteTemporalNulo, clienteTemporalNulo };

            finAtencionCaja.HoraFinAtencion = new List<double> { 0, 0, 0, 0 };
            finAtencionPersonalizada.HoraFinAtencion = new List<double> { 0, 0, 0 };
            finAtencionTarjetaCredito.HoraFinAtencion = new List<double> { 0, 0 };
            finAtencionPlazoFijo.HoraFinAtencion = new List<double> { 0 };
            finAtencionPrestamos.HoraFinAtencion = new List<double> { 0, 0 };
            finAtencionServicioAdicional.HoraFinAtencion = new List<double> { 0, 0 };

            List<FinAtencion> finesAtencion = new List<FinAtencion> { finAtencionCaja, finAtencionPersonalizada, finAtencionTarjetaCredito, finAtencionPlazoFijo, finAtencionPrestamos, finAtencionServicioAdicional};

            List<string> estadoCaja = new List<string> { "Libre", "Libre", "Libre", "Libre" };
            List<string> estadoAtencionPersonalizada = new List<string> { "Libre", "Libre", "Libre" };
            List<string> estadoTarjetaCredito = new List<string> { "Libre", "Libre" };
            List<string> estadoPlazoFijo = new List<string> { "Libre" };
            List<string> estadoPrestamos = new List<string> { "Libre", "Libre" };
            List<string> estadoServicioAdicional = new List<string> { "Libre", "Libre" };
            List<List<string>> estados = new List<List<string>> { estadoCaja, estadoAtencionPersonalizada, estadoTarjetaCredito, estadoPlazoFijo, estadoPrestamos, estadoServicioAdicional };

            List<ClienteTemporal> clientesTemporales = new List<ClienteTemporal>();
            Fila fila = new Fila();
            fila.Reloj = 0.0;
            fila.Evento = "Inicialización";
            fila.Llegada = llegadas;
            fila.Colas = colas;
            fila.FinesAtencion = finesAtencion;
            fila.Estados = estados;
            fila.ClientesTemporales = clientesTemporales;

            return fila;
        }

        private void Simulacion_Load(object sender, EventArgs e)
        {

            //llenar la tabla con 300 filas
            for (int i = 0; i < 300; i++)
            {
                grdSimulacion.Rows.Add();
                grdSimulacion.Rows[i].Cells["nroFila"].Value = i;
            }

            Fila filaActual = GenerarFila0(0);
            LlenarTabla(filaActual, "Inicializacion");
            Fila filaSiguiente = GenerarFilaSiguiente();
            
            double proximoTiempo = filaActual.Llegada[0].ProximaLlegada;
            int tipoEvento = 0;
            string proximoEvento = "";
            for (int i = 0; i < filaActual.Llegada.Count(); i++)
            {
                filaSiguiente.Llegada[i] = filaActual.Llegada[i];
                if (proximoTiempo > filaActual.Llegada[i].ProximaLlegada)
                {
                    proximoTiempo = filaActual.Llegada[i].ProximaLlegada;
                    tipoEvento = i;
                    proximoEvento = filaActual.Llegada[i].GetType().Name.ToString() + "[" + tipoEvento + "]";
                }

            }

            filaSiguiente.Evento = proximoEvento;
            filaSiguiente.Reloj = proximoTiempo;
            //Crea un nuevo cliente temporal para la proxima llegada
            ComienzoLlegada(filaSiguiente, tipoEvento);

            LlenarTabla(filaSiguiente, filaSiguiente.Evento);

            
        }

        private void LlenarTabla(Fila fila, string proximoEvento)
        {
            //buscar la primer fila vacia
            for (int ui = 0; ui < grdSimulacion.Rows.Count; ui++)
            {
                var valor = grdSimulacion.Rows[ui].Cells["c1"].Value;

                if (valor == "" || valor == null)
                {
                    grdSimulacion.Rows[ui].Cells["c1"].Value = fila.Evento;
                    grdSimulacion.Rows[ui].Cells["c2"].Value = fila.Reloj;
                    //EVENTO LLEGADA CLIENTE
                    //llegada clientes caja
                    grdSimulacion.Rows[ui].Cells["c3"].Value = fila.Llegada[0].TiempoEntreLlegada.ToString();
                    grdSimulacion.Rows[ui].Cells["c4"].Value = fila.Llegada[0].ProximaLlegada.ToString();
                    //llegada clientes atencion personalizada
                    grdSimulacion.Rows[ui].Cells["c5"].Value = fila.Llegada[1].TiempoEntreLlegada.ToString();
                    grdSimulacion.Rows[ui].Cells["c6"].Value = fila.Llegada[1].ProximaLlegada.ToString();
                    //llegada cliente tarjeta
                    grdSimulacion.Rows[ui].Cells["c7"].Value = fila.Llegada[2].TiempoEntreLlegada.ToString();
                    grdSimulacion.Rows[ui].Cells["c8"].Value = fila.Llegada[2].ProximaLlegada.ToString();
                    //llegada cliente plazos fijos
                    grdSimulacion.Rows[ui].Cells["c9"].Value = fila.Llegada[3].TiempoEntreLlegada.ToString();
                    grdSimulacion.Rows[ui].Cells["c10"].Value = fila.Llegada[3].ProximaLlegada.ToString();
                    //llegada cliente prestamos
                    grdSimulacion.Rows[ui].Cells["c11"].Value = fila.Llegada[4].TiempoEntreLlegada.ToString();
                    grdSimulacion.Rows[ui].Cells["c12"].Value = fila.Llegada[4].ProximaLlegada.ToString();

                    //ACUMULADORES DE TIEMPO DE ESPERA
                    grdSimulacion.Rows[ui].Cells["c16"].Value = fila.FinesAtencion[0].ACtiempoAtencion.ToString();
                    grdSimulacion.Rows[ui].Cells["c19"].Value = fila.FinesAtencion[1].ACtiempoAtencion.ToString();
                    grdSimulacion.Rows[ui].Cells["c22"].Value = fila.FinesAtencion[2].ACtiempoAtencion.ToString();
                    grdSimulacion.Rows[ui].Cells["c25"].Value = fila.FinesAtencion[3].ACtiempoAtencion.ToString();
                    grdSimulacion.Rows[ui].Cells["c28"].Value = fila.FinesAtencion[4].ACtiempoAtencion.ToString();
                    grdSimulacion.Rows[ui].Cells["c31"].Value = fila.FinesAtencion[5].ACtiempoAtencion.ToString();

                    //PORCENTAJE DE TIEMPO DE ESPERA

                    //COLAS
                    //cola cajas
                    grdSimulacion.Rows[ui].Cells["c15"].Value = fila.Colas[0].Clientes.Count().ToString();
                    grdSimulacion.Rows[ui].Cells["c18"].Value = fila.Colas[1].Clientes.Count().ToString();
                    grdSimulacion.Rows[ui].Cells["c21"].Value = fila.Colas[2].Clientes.Count().ToString();
                    grdSimulacion.Rows[ui].Cells["c24"].Value = fila.Colas[3].Clientes.Count().ToString();
                    grdSimulacion.Rows[ui].Cells["c27"].Value = fila.Colas[4].Clientes.Count().ToString();
                    grdSimulacion.Rows[ui].Cells["c30"].Value = fila.Colas[0].Clientes.Count().ToString();

                    //Estados de objetos permanentes [1] para el servicio [2] para el servidor
                    grdSimulacion.Rows[ui].Cells["c66"].Value = fila.Estados[0][0];
                    grdSimulacion.Rows[ui].Cells["c67"].Value = fila.Estados[0][1];
                    grdSimulacion.Rows[ui].Cells["c68"].Value = fila.Estados[0][2];
                    grdSimulacion.Rows[ui].Cells["c69"].Value = fila.Estados[0][3];
                    //grdSimulacion.Rows[0].Cells["c70"].Value = fila.Estados[0][4];
                    grdSimulacion.Rows[ui].Cells["c71"].Value = fila.Estados[1][0];
                    grdSimulacion.Rows[ui].Cells["c72"].Value = fila.Estados[1][1];
                    grdSimulacion.Rows[ui].Cells["c73"].Value = fila.Estados[1][2];
                    grdSimulacion.Rows[ui].Cells["c74"].Value = fila.Estados[2][0];
                    grdSimulacion.Rows[ui].Cells["c75"].Value = fila.Estados[2][1];
                    grdSimulacion.Rows[ui].Cells["c76"].Value = fila.Estados[3][0];
                    grdSimulacion.Rows[ui].Cells["c77"].Value = fila.Estados[4][0];
                    grdSimulacion.Rows[ui].Cells["c78"].Value = fila.Estados[4][1];
                    grdSimulacion.Rows[ui].Cells["c79"].Value = fila.Estados[5][0];
                    grdSimulacion.Rows[ui].Cells["c80"].Value = fila.Estados[5][1];
                    break;
                }
            }

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

            fila.ClientesTemporales.Add(clienteTemporal);

            ServicioEspecialLlegada(fila, clienteTemporal);            

            GenerarFin(fila, tipoServicio, clienteTemporal);

        }

        private void ServicioEspecialLlegada(Fila fila, ClienteTemporal clienteTemporal){
            
            Random random = new Random();
            double numeroDecimalAleatorio = random.NextDouble();

            // Redondear a dos decimales
            numeroDecimalAleatorio = Math.Round(numeroDecimalAleatorio, 2);

            if(numeroDecimalAleatorio < 0.18){
                clienteTemporal.TomaServicio = true;

            } else {
                clienteTemporal.TomaServicio = false;
            }
        }

        private void ServicioEspecialFin(Fila fila, ClienteTemporal clienteTemporal){
            Random random = new Random();
            double numeroDecimalAleatorio = random.NextDouble();

            // Redondear a dos decimales
            numeroDecimalAleatorio = Math.Round(numeroDecimalAleatorio, 2);

            if(numeroDecimalAleatorio < 0.18){
                clienteTemporal.TomaServicio = true;

            } else {
                clienteTemporal.TomaServicio = false;
            }
        }

        private void GenerarFinServicioAdicional(){

        }

        private void GenerarFin(Fila fila, int tipoServicio, ClienteTemporal clienteTemporal)
        {
            for (int i = 0; i < fila.Estados[tipoServicio].Count; i++)
            {
                if (fila.Estados[tipoServicio][i] == "Libre")
                {
                    
                    fila.Estados[tipoServicio][i] = "Ocupado";
                    clienteTemporal.Estado = "Siendo Atendido";
                    clienteTemporal.InicioAtencion = fila.Reloj;

                    fila.FinesAtencion[tipoServicio].Cliente[i] = clienteTemporal;
                    int index = fila.ClientesTemporales.IndexOf(clienteTemporal);
                    fila.ClientesTemporales[index].Estado = clienteTemporal.Estado;
                    fila.ClientesTemporales[index].InicioAtencion = clienteTemporal.InicioAtencion;

                    Random random = new Random();

                    double numeroDecimalAleatorio = random.NextDouble();

                    numeroDecimalAleatorio = Math.Round(numeroDecimalAleatorio, 2);

                    fila.FinesAtencion[tipoServicio].TiempoAtencion = -fila.FinesAtencion[tipoServicio].Media * Math.Log(1 - numeroDecimalAleatorio);
                    fila.FinesAtencion[tipoServicio].HoraFinAtencion[i] = fila.Reloj + fila.FinesAtencion[tipoServicio].TiempoAtencion;
                    fila.FinesAtencion[tipoServicio].ACtiempoAtencion += fila.FinesAtencion[tipoServicio].TiempoAtencion;
                    fila.FinesAtencion[tipoServicio].PRCOcupacion = (fila.FinesAtencion[tipoServicio].ACtiempoAtencion / fila.Reloj) * 100;

                    return;
                }
            }

            fila.Colas[tipoServicio].Clientes.Add(clienteTemporal);

        }
        
        private void ComienzoFin(int tipoServicio, int servidor, Fila fila)
        {
            ClienteTemporal clienteTemporal = fila.FinesAtencion[tipoServicio].Cliente[servidor];

            fila.ClientesTemporales.Remove(clienteTemporal);

            if(fila.Colas[tipoServicio].Clientes.Count != 0){

                foreach(var cliente in fila.ClientesTemporales){
                    if(cliente.Id == fila.Colas[tipoServicio].Clientes[0].Id){
                        cliente.Estado = "Siendo Atendido";
                        cliente.InicioAtencion = fila.Reloj;

                        //Es el mismo cliente que encontramos en el condicional
                        ClienteTemporal clienteCola = fila.Colas[tipoServicio].Clientes[0];

                        GenerarFin(fila, tipoServicio, clienteCola);

                        fila.Colas[tipoServicio].Clientes.RemoveAt(0);
                        return;
                    }
                }
            }
            //Lo dejo en cero para demostrar que no tiene una atencion
            fila.FinesAtencion[tipoServicio].HoraFinAtencion[servidor] = 0;
            fila.Estados[tipoServicio][servidor] = "Libre";
        }
    }
}
