﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
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
        private int cantClientesTotal;
        private int cantClientesSinServicioAdicional;
        private double proxCorteLuz = 0;
        private int tipoServicio;

        private RK rk;

        public Simulacion(int n, int filaDesde, List<int> listaMedias, bool caja5)
        {
            InitializeComponent();
            this.cantidadFilas = n;
            this.filaDesde = filaDesde;
            this.listaMedias = listaMedias;
            this.caja5 = caja5;

            //generar fila 0



        }
        private void Simulacion_Load(object sender, EventArgs e)
        {

               //llenar la tabla con 300 filas
               for (int i = 0; i < 300; i++)
               {
                   grdSimulacion.Rows.Add();
                   //grdSimulacion.Rows[i].Cells["nroFila"].Value = i;
               }
                
               Fila fila = GenerarFila(0);
          
               FuncionCiclica(this.cantidadFilas, fila, this.filaDesde, 0); 
        }

        public void FuncionCiclica(int cantEventos, Fila fila1, int nroFila, int  nroLinea)
        {
            grdSimulacion.Rows[nroLinea].Cells["nroFila"].Value = fila1.NroFila;
            string proxEvento = "";
            int tipoEvento = 0;


            bool esLlegada = false;
            bool esCorteLuz = false;

            int servidorFin = -1;
            double proxTiempo = 1000000000;


            //fila2.NroFila = fila1.NroFila + 1;

            if (cantEventos != 0)
            {

                for (int i = 0; i < fila1.Llegada.Count; i++)
                {
                    if (proxTiempo > fila1.Llegada[i].ProximaLlegada)
                    {
                        proxTiempo = fila1.Llegada[i].ProximaLlegada;
                        tipoEvento = i;
                        proxEvento = fila1.Llegada[i].GetType().Name.ToString() + "[" + tipoEvento + "]";

                        esLlegada = true;
                    }
                    for(int j = 0; j < fila1.FinesAtencion[i].HoraFinAtencion.Count; j++)
                    {
                        if (fila1.FinesAtencion[i].HoraFinAtencion[j] > 0)
                        {
                            if (proxTiempo > fila1.FinesAtencion[i].HoraFinAtencion[j])
                            {
                                proxTiempo = fila1.FinesAtencion[i].HoraFinAtencion[j];
                                tipoEvento = i;
                                servidorFin = j;
                                proxEvento = fila1.FinesAtencion[i].GetType().Name.ToString() + "[" + tipoEvento + "]";

                                esLlegada = false;
                            }
                        }
                    }
                }

                if(proxTiempo > proxCorteLuz)
                {
                    proxTiempo = proxCorteLuz;
                    esCorteLuz = true;
                    esLlegada = false;
                }
                





                /*
                if(proxTiempoLlegada < proxTiempoFin)
                {
                    proxTiempo = proxTiempoLlegada;
                    tipoEvento = tipoEventoLlegada;
                } else
                {
                    esLlegada = false;
                    proxTiempo = proxTiempoFin;
                    tipoEvento = tipoEventoFin;
                    servidorFin = servidor;
                }*/

                fila1.Reloj = proxTiempo;

                //Fila fila2 = new Fila();
                if (esLlegada)
                {
                    ComienzoLlegada(fila1, tipoEvento);


                    //falta agregar alguna forma de verificar que las columnas se agreguen solo cuando llegue un cliente
                    int ultimaFila = fila1.NroFila;
                    string estadoCliente = "estadoCliente" + ultimaFila;
                    string tomaServicio = "tomaServicio" + ultimaFila;
                    if (!grdSimulacion.Columns.Contains(estadoCliente))
                    {
                        DataGridViewColumn columnaCliente = new DataGridViewColumn();
                        columnaCliente.Name = estadoCliente;
                        columnaCliente.HeaderText = "Estado" + ultimaFila;
                        columnaCliente.DataPropertyName = estadoCliente;
                        columnaCliente.CellTemplate = new DataGridViewTextBoxCell();

                        DataGridViewColumn columnaSA = new DataGridViewColumn();
                        columnaSA.Name = tomaServicio;
                        columnaSA.HeaderText = "Toma Servicio Adicional?" + ultimaFila;
                        columnaSA.CellTemplate = new DataGridViewTextBoxCell();

                        grdSimulacion.Columns.Add(columnaCliente);
                        grdSimulacion.Columns.Add(columnaSA);


                    }

                }
                else
                {
                    if (esCorteLuz)
                    {
                        ComienzoFinCorteLuz(fila1); 
                    } else
                    {
                        ComienzoFin(tipoEvento, servidorFin, fila1);
                    }
                    
                }


                if (nroFila >= this.filaDesde && nroFila <= (this.filaDesde + 10))
                {
                    LlenarTabla(fila1, proxEvento, proxTiempo);
                }

                Fila fila2 = fila1;
                fila2.Reloj = proxTiempo;
                fila2.Evento = proxEvento;
                FuncionCiclica(cantEventos - 1, fila2, nroFila + 1, nroLinea + 1);

            }
            //fila2.Reloj = fila2.Reloj + proxTiempo;

                
                


                


        }
        

        private Fila GenerarFila(double reloj)
        {
            Fila fila = new Fila();
            fila.Reloj = reloj;
            fila.Evento = "Inicialización";

            //primero hay que generar todas las proximas llegadas de los 6 servicios

            Llegada llegadaCaja = new Llegada();
            Llegada llegadaAtencionPersonalizada = new Llegada();
            Llegada llegadaTarjeta = new Llegada();
            Llegada llegadaPlazoFijo = new Llegada();
            Llegada llegadaPrestamos = new Llegada();
            //Llegada llegadaServicioAdicional = new Llegada();

            FinAtencion finAtencionCaja = new FinAtencion();
            FinAtencion finAtencionPersonalizada = new FinAtencion();
            FinAtencion finAtencionTarjetaCredito = new FinAtencion();
            FinAtencion finAtencionPlazoFijo = new FinAtencion();
            FinAtencion finAtencionPrestamos = new FinAtencion();
            FinAtencion finAtencionServicioAdicional = new FinAtencion();


            List<FinAtencion> finesAtencion = new List<FinAtencion> { finAtencionCaja, finAtencionPersonalizada, finAtencionTarjetaCredito, finAtencionPlazoFijo, finAtencionPrestamos, finAtencionServicioAdicional };



            List<Llegada> llegadas = new List<Llegada> { llegadaCaja, llegadaAtencionPersonalizada, llegadaTarjeta, llegadaPlazoFijo, llegadaPrestamos };

            if (fila.Evento == "Inicialización")
            {
                for (int i = 0; i < 5; i++)
                {
                    Random rnd = new Random();
                    double tiempoEntreLlegada = -(listaMedias[i]) * Math.Log(1 - (double)rnd.NextDouble());
                    tiempoEntreLlegada = Math.Round(tiempoEntreLlegada, 2);
                    llegadas[i].TiempoEntreLlegada = tiempoEntreLlegada;
                    llegadas[i].ProximaLlegada = tiempoEntreLlegada + fila.Reloj;
                    llegadas[i].Media = this.listaMedias[i];
                    finesAtencion[i].Media = this.listaMedias[i];
                }
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
            
            List<string> estadoCaja = new List<string> { "Libre", "Libre", "Libre", "Libre" };
            List<string> estadoAtencionPersonalizada = new List<string> { "Libre", "Libre", "Libre" };
            List<string> estadoTarjetaCredito = new List<string> { "Libre", "Libre" };
            List<string> estadoPlazoFijo = new List<string> { "Libre" };
            List<string> estadoPrestamos = new List<string> { "Libre", "Libre" };
            List<string> estadoServicioAdicional = new List<string> { "Libre", "Libre" };
            List<List<string>> estados = new List<List<string>> { estadoCaja, estadoAtencionPersonalizada, estadoTarjetaCredito, estadoPlazoFijo, estadoPrestamos, estadoServicioAdicional };

            List<ClienteTemporal> clientesTemporales = new List<ClienteTemporal>();
            
            fila.Llegada = llegadas;
            fila.Colas = colas;
            fila.FinesAtencion = finesAtencion;
            fila.Estados = estados;
            fila.ClientesTemporales = clientesTemporales;

            //revisar si el cliente quiere la quinta caja
            if (this.caja5)
            {
                if (estadoCaja.Count < 5)
                {
                    estadoCaja.Add("Libre");
                }
                finAtencionCaja.HoraFinAtencion.Add(0);
                finAtencionCaja.Cliente.Add(clienteTemporalNulo);
            }

            GenerarCorteLuz(fila);

            return fila;
        }

        

        private void LlenarTabla(Fila fila, string proximoEvento, double reloj)
        {
            //buscar la primer fila vacia
            for (int ui = 0; ui < grdSimulacion.Rows.Count; ui++)
            {
                var valor = grdSimulacion.Rows[ui].Cells["c1"].Value;

                if (valor == "" || valor == null)
                {
                    fila.NroFila = ui;
                    
                    grdSimulacion.Rows[ui].Cells["c1"].Value = proximoEvento;
                    grdSimulacion.Rows[ui].Cells["c2"].Value = reloj;
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

                    //TIEMPO DE ATENCION DE SERVICIO
                    grdSimulacion.Rows[ui].Cells["c33"].Value = fila.FinesAtencion[0].TiempoAtencion;
                    grdSimulacion.Rows[ui].Cells["c41"].Value = fila.FinesAtencion[1].TiempoAtencion;
                    grdSimulacion.Rows[ui].Cells["c47"].Value = fila.FinesAtencion[2].TiempoAtencion;
                    grdSimulacion.Rows[ui].Cells["c52"].Value = fila.FinesAtencion[3].TiempoAtencion;
                    grdSimulacion.Rows[ui].Cells["c57"].Value = fila.FinesAtencion[4].TiempoAtencion;
                    grdSimulacion.Rows[ui].Cells["c61"].Value = fila.FinesAtencion[5].TiempoAtencion;

                    //FINES DE ATENCION
                    //los fines de atencion de las cajas se guardan en las columnas de la 36 a la 40
                    grdSimulacion.Rows[ui].Cells["c36"].Value = fila.FinesAtencion[0].HoraFinAtencion[0];
                    grdSimulacion.Rows[ui].Cells["c37"].Value = fila.FinesAtencion[0].HoraFinAtencion[1];
                    grdSimulacion.Rows[ui].Cells["c38"].Value = fila.FinesAtencion[0].HoraFinAtencion[2];
                    grdSimulacion.Rows[ui].Cells["c39"].Value = fila.FinesAtencion[0].HoraFinAtencion[3];
                    if(caja5)
                    {
                        grdSimulacion.Rows[ui].Cells["c40"].Value = fila.FinesAtencion[0].HoraFinAtencion[4];
                    }
                    //los fines de atencion de la atencion personalizada se guardan en las columnas de la 44 a la 46
                    grdSimulacion.Rows[ui].Cells["c44"].Value = fila.FinesAtencion[1].HoraFinAtencion[0];
                    grdSimulacion.Rows[ui].Cells["c46"].Value = fila.FinesAtencion[1].HoraFinAtencion[1];
                    //los fines de las tarjetas se guardan en las columnas de la 50 a la 51
                    grdSimulacion.Rows[ui].Cells["c50"].Value = fila.FinesAtencion[2].HoraFinAtencion[0];
                    grdSimulacion.Rows[ui].Cells["c51"].Value = fila.FinesAtencion[2].HoraFinAtencion[1];
                    //los fines de atenion de los plazos fijos se guardan en las columnas de la 55 a la 56
                    grdSimulacion.Rows[ui].Cells["c55"].Value = fila.FinesAtencion[3].HoraFinAtencion[0];
                    //los fines de atenion de los prestamos se guardan en la cloumna 60
                    grdSimulacion.Rows[ui].Cells["c60"].Value = fila.FinesAtencion[4].HoraFinAtencion[0];
                    grdSimulacion.Rows[ui].Cells["c56"].Value = fila.FinesAtencion[4].HoraFinAtencion[1];


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
                    if (fila.Estados[0].Count == 5)
                    {
                        grdSimulacion.Rows[0].Cells["c70"].Value = fila.Estados[0][4];
                    }
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
            cantClientesTotal += 1;

            Random random = new Random();

            // Generar un número decimal aleatorio entre 0.01 y 0.99
            double numeroDecimalAleatorio = random.NextDouble();

            // Redondear a dos decimales
            numeroDecimalAleatorio = Math.Round(numeroDecimalAleatorio, 2);

            fila.Llegada[tipoServicio].TiempoEntreLlegada = -fila.Llegada[tipoServicio].Media * Math.Log(1 - numeroDecimalAleatorio);
            fila.Llegada[tipoServicio].ProximaLlegada = fila.Reloj + fila.Llegada[tipoServicio].TiempoEntreLlegada;
        }

        private void ComienzoLlegada(Fila fila, int tipoServicio)
        {
            
            Random random = new Random();
            ClienteTemporal clienteTemporal = new ClienteTemporal("En espera", 0, random.Next(1, 10000), tipoServicio, false);

            fila.ClientesTemporales.Add(clienteTemporal);
            //this.tipoServicio = tipoServicio;

            ServicioEspecialLlegada(fila, clienteTemporal);            

            GenerarProximaLlegada(fila, tipoServicio);

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
            grdSimulacion.Rows[fila.NroFila].Cells["c13"].Value = clienteTemporal.TomaServicio;
            clienteTemporal.NroFilaCliente = fila.NroFila;
        }

        private void ServicioEspecialFin(Fila fila, ClienteTemporal clienteTemporal){
            Random random = new Random();
            double numeroDecimalAleatorio = random.NextDouble();

            // Redondear a dos decimales
            numeroDecimalAleatorio = Math.Round(numeroDecimalAleatorio, 2);

            if(numeroDecimalAleatorio < 0.33){
                clienteTemporal.TomaServicio = true;

            } else {
                clienteTemporal.TomaServicio = false;
            }
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

                    //los fines de atenion se guardan en las columnas de la 36 a la 40
                    /*
                    for (int i = 0; i < fila.FinesAtencion[tipoServicio].Count; i++)
                    {
                        
                    }
                    */
                    return;
                }
            }
        }

        private void ComienzoFin(int tipoServicio, int servidor, Fila fila)
        {
            ClienteTemporal clienteTemporal = fila.FinesAtencion[tipoServicio].Cliente[servidor];

            if(clienteTemporal.TomaServicio = false)
            {
                ServicioEspecialFin(fila, clienteTemporal);
            }

            fila.FinesAtencion[tipoServicio].HoraFinAtencion[servidor] = 0;
            fila.Estados[tipoServicio][servidor] = "Libre";

            if (fila.Colas[tipoServicio].Clientes.Count != 0) {

                foreach (var cliente in fila.ClientesTemporales) {
                    if (cliente.Id == fila.Colas[tipoServicio].Clientes[0].Id) {
                        cliente.Estado = "Siendo Atendido";
                        cliente.InicioAtencion = fila.Reloj;

                        //Es el mismo cliente que encontramos en el condicional
                        ClienteTemporal clienteCola = fila.Colas[tipoServicio].Clientes[0];

                        EstadisticasColas(fila, tipoServicio, clienteCola);

                        GenerarFin(fila, tipoServicio, clienteCola);

                        fila.Colas[tipoServicio].Clientes.RemoveAt(0);
                        return;
                    }
                }
            }

            if (clienteTemporal.TomaServicio)
            {
                GenerarFinServicioEspecial(fila, clienteTemporal);
            } else
            {
                cantClientesSinServicioAdicional += 1;
                fila.ClientesTemporales.Remove(clienteTemporal);
            }

        }

        private void GenerarFinServicioEspecial(Fila fila, ClienteTemporal clienteTemporal)
        {
            for(int i = 0; i < fila.Estados[5].Count; i++)
            {
                if (fila.Estados[5][i] == "Libre")
                {
                    fila.Estados[5][i] = "Ocupado";
                    clienteTemporal.Estado = "Siendo Atendido";
                    clienteTemporal.InicioAtencion = fila.Reloj;

                    fila.FinesAtencion[5].Cliente[i] = clienteTemporal;

                    int index = fila.ClientesTemporales.IndexOf(clienteTemporal);
                    fila.ClientesTemporales[index].Estado = clienteTemporal.Estado;
                    fila.ClientesTemporales[index].InicioAtencion = clienteTemporal.InicioAtencion;

                    Random random = new Random();

                    double numeroDecimalAleatorio = random.NextDouble();

                    numeroDecimalAleatorio = Math.Round(numeroDecimalAleatorio, 2);

                    fila.FinesAtencion[5].TiempoAtencion = -fila.FinesAtencion[5].Media * Math.Log(1 - numeroDecimalAleatorio);
                    fila.FinesAtencion[5].HoraFinAtencion[i] = fila.Reloj + fila.FinesAtencion[5].TiempoAtencion;
                    fila.FinesAtencion[5].ACtiempoAtencion += fila.FinesAtencion[5].TiempoAtencion;
                    fila.FinesAtencion[5].PRCOcupacion = (fila.FinesAtencion[5].ACtiempoAtencion / fila.Reloj) * 100;

                    return;
                }
            }

            fila.Colas[5].Clientes.Add(clienteTemporal);
        }

        private void ComienzaFinServicioEspecial(int servidor, Fila fila)
        {
            ClienteTemporal clienteTemporal = fila.FinesAtencion[5].Cliente[servidor];

            if (fila.Colas[5].Clientes.Count != 0)
            {
                fila.FinesAtencion[5].HoraFinAtencion[servidor] = 0;
                fila.Estados[5][servidor] = "Libre";
                foreach (var cliente in fila.ClientesTemporales)
                {
                    if (cliente.Id == fila.Colas[5].Clientes[0].Id)
                    {
                        cliente.Estado = "Siendo Atendido";
                        cliente.InicioAtencion = fila.Reloj;

                        //Es el mismo cliente que encontramos en el condicional
                        ClienteTemporal clienteCola = fila.Colas[5].Clientes[0];

                        EstadisticasColas(fila, 5, clienteCola);

                        GenerarFinServicioEspecial(fila, clienteCola);

                        fila.Colas[5].Clientes.RemoveAt(0);
                        return;
                    }
                }
            }
            //Lo dejo en cero para demostrar que no tiene una atencion

            fila.ClientesTemporales.Remove(clienteTemporal);

        }

        private void EstadisticasColas(Fila fila, int tipoServicio, ClienteTemporal clienteTemporal)
        {
            fila.Colas[tipoServicio].CantEsperas += 1;
            fila.Colas[tipoServicio].AcTiempoEspera += fila.Reloj - clienteTemporal.InicioAtencion;
            fila.Colas[tipoServicio].PrmTiempoEspera = fila.Colas[tipoServicio].AcTiempoEspera / fila.Colas[tipoServicio].CantEsperas;
        }

        //Iniciarlo desde el principio
        private void CorteLuz(Fila fila){
            Random random = new Random();

            int t = 2;

            double numeroDecimalAleatorio = random.NextDouble();

            numeroDecimalAleatorio = Math.Round(numeroDecimalAleatorio, 2);

            if(numeroDecimalAleatorio < 0.20){
                proxCorteLuz += fila.Reloj + 4 * t;
            } 

            if(0.20 < numeroDecimalAleatorio && numeroDecimalAleatorio < 0.60){
                proxCorteLuz += fila.Reloj + 6 * t;
            } else {
                proxCorteLuz += fila.Reloj + 8 * t;
            }

        }
        

        private void GenerarCorteLuz(Fila fila){
            CorteLuz(fila);

            if (fila.Estados[3][0] == "Ocupado")
            {
                double nuevoTiempo = fila.FinesAtencion[3].HoraFinAtencion[0] + (double)rk.ejecutarRK(0, (float)fila.Reloj);
                nuevoTiempo = Math.Round(nuevoTiempo, 2);
                fila.FinesAtencion[3].HoraFinAtencion[0] = nuevoTiempo;
            }

            if (fila.Estados[3][0] == "Libre")
            {
                fila.Estados[3][0] = "Suspendido";
            }
        }

        private void ComienzoFinCorteLuz(Fila fila)
        {
            if (fila.Estados[3][0] == "Suspendido")
            {
                fila.Estados[3][0] = "Libre";
                if (fila.Colas[3].Clientes.Count != 0)
                {
                    foreach(var cliente in fila.ClientesTemporales)
                    {
                        if(cliente.Id == fila.Colas[3].Clientes[0].Id)
                        {
                            GenerarFin(fila, 3, cliente);


                            fila.Colas[tipoServicio].Clientes.RemoveAt(0);

                        }
                    }
                }
            }

            GenerarCorteLuz(fila);
        }

    }
}
