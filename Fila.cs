using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace borrador_de_tp4
{
    public class Fila
    {
        private double reloj;
        private string evento;

        /*
        private Llegada llegadaCaja;
        private Llegada llegadaAtencionPersonalizada;
        private Llegada llegadaTarjetaCredito;
        private Llegada llegadaPlazoFijo;
        private Llegada llegadaPrestamos;
        private Llegada llegadaServicioAdicional;
        */
        
        private List<Llegada> llegada;
        /*
        private Cola colaCaja;
        private Cola colaAtencionPersonalizada;
        private Cola colaTarjetaCredito;
        private Cola colaPlazoFijo;
        private Cola colaPrestamos;
        private Cola colaServicioAdicional;
        */
        private List<Cola> colas;
        /*
        private FinAtencion finCaja;
        private FinAtencion finAtencionPersonalizada;
        private FinAtencion finTarjetaCredito;
        private FinAtencion finPlazoFijo;
        private FinAtencion finPrestamos;
        private FinAtencion finServicioAdicional;
        */
        private List<FinAtencion> finesAtencion;
        /*
        private List<string> estadoCaja;
        private List<string> estadoAtencionPersonalizada;
        private List<string> estadoTarjetaCredito;
        private List<string> estadoPlazoFijo;
        private List<string> estadoPrestamos;
        private List<string> estadoServicioAdicional;
        */
        private List<List<string>> estados;

        private List<ClienteTemporal> clientesTemporales;

        public List<Llegada> Llegada
        {
            get { return llegada; }
            set { llegada = value; }
        }

        public List<Cola> Colas
        {
            get { return colas; }
            set { colas = value; }
        }
        public List<FinAtencion> FinesAtencion
        {
            get { return finesAtencion; }
            set { finesAtencion = value; }
        }

        public List<List<string>> Estados
        {
            get { return estados; }
            set { estados = value; }
        }

        public List<ClienteTemporal> ClientesTemporales
        {
            get { return clientesTemporales; }
            set {  clientesTemporales = value; }
        }

        public double Reloj
        {
            get { return reloj; }
            set { reloj = value;}
        }

        public string Evento
        {
            get { return evento;}
            set { evento = value; }
        }
    }
}
