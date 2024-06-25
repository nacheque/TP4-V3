using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace borrador_de_tp4
{
    public class Cola
    {
        private List<ClienteTemporal> clientes;
        private double tiempoEspera = 0;
        //Porcentaje Tiempo Fuera
        private double prcTiempoFuera;

        public List<ClienteTemporal> Clientes
        {
            get { return clientes; }
            set { clientes = value; }
        }

        public double TiempoEspera
        {
            get { return tiempoEspera; }
            set {  tiempoEspera = value; }
        }

        public double PrcTiempoFuera
        {
            get { return prcTiempoFuera; }
            set { prcTiempoFuera = value; }
        }
    }
}
